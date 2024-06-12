using Microsoft.AspNetCore.Http;
using PetFamily.Application.DataAccess;
using PetFamily.Application.Models;
using PetFamily.Application.Providers;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.PublishPet;

public class PublishPetHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMinioProvider _minioProvider;

    public PublishPetHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IMinioProvider minioProvider)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _minioProvider = minioProvider;
    }

    public async Task<Result<Guid>> Handle(PublishPetRequest request, CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
        if (volunteer.IsFailure)
            return volunteer.Error;

        var address = Address.Create(request.City, request.Street, request.Building, request.Index).Value;
        var place = Place.Create(request.Place).Value;
        var weight = Weight.Create(request.Weight).Value;
        var contactPhoneNumber = PhoneNumber.Create(request.ContactPhoneNumber).Value;
        var volunteerPhoneNumber = PhoneNumber.Create(request.VolunteerPhoneNumber).Value;

        var photoFiles = GetPhotoFiles(request.Files);
        if (photoFiles.IsFailure)
            return photoFiles.Error;

        var photos = photoFiles.Value.Select(p => p.PetPhoto);

        var pet = Pet.Create(
            request.Nickname,
            request.Description,
            request.BirthDate,
            request.Breed,
            request.Color,
            address,
            place,
            request.Castration,
            request.PeopleAttitude,
            request.AnimalAttitude,
            request.OnlyOneInFamily,
            request.Health,
            request.Height,
            weight,
            contactPhoneNumber,
            volunteerPhoneNumber,
            request.OnTreatment,
            photos);

        if (pet.IsFailure)
            return pet.Error;

        volunteer.Value.PublishPet(pet.Value);

        foreach (var photoFile in photoFiles.Value)
        {
            await using var stream = photoFile.File.OpenReadStream();

            var objectName = await _minioProvider
                .UploadPhoto(stream, photoFile.PetPhoto.Path, ct);

            if (objectName.IsFailure)
                return objectName.Error;
        }

        await _unitOfWork.SaveChangesAsync(ct);
        return volunteer.Value.Id;
    }

    private Result<List<PhotoFile>> GetPhotoFiles(IFormFileCollection fileCollection)
    {
        List<PhotoFile> photos = [];
        foreach (var file in fileCollection)
        {
            var contentType = Path.GetExtension(file.FileName);

            var photo = PetPhoto.Create(contentType, file.Length);
            if (photo.IsFailure)
                return photo.Error;

            photos.Add(new(photo.Value, file));
        }

        return photos;
    }
}