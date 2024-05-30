// using FluentAssertions;
// using Microsoft.AspNetCore.Http;
// using Moq;
// using PetFamily.Application.DataAccess;
// using PetFamily.Application.Features.Volunteers;
// using PetFamily.Application.Features.Volunteers.UploadPhoto;
// using PetFamily.Application.Providers;
// using PetFamily.Domain.Common;
// using PetFamily.Domain.Entities;
// using PetFamily.Domain.ValueObjects;
//
// namespace PetFamily.UnitTests.Features;
//
// public class UploadVolunteerPhotoTests
// {
//     private readonly Mock<IMinioProvider> _minioProviderMock = new();
//     private readonly Mock<IVolunteersRepository> _volunteersRepositoryMock = new();
//     private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
//     private readonly Mock<IFormFile> _formFileMock = new();
//
//     public UploadVolunteerPhotoTests()
//     {
//         var fileName = "file.png";
//         _formFileMock.Setup(x => x.FileName).Returns(fileName);
//     }
//
//     [Fact]
//     public async Task UploadVolunteerPhoto_with_valid_photo()
//     {
//         //arrange
//         var ct = new CancellationToken();
//
//         var volunteerId = Guid.NewGuid();
//         var request = new UploadVolunteerPhotoRequest(volunteerId, _formFileMock.Object);
//
//         var volunteer = Volunteer.Create(
//             FullName.Create("Kirill", "Sachkov", "Olegovich").Value,
//             "a",
//             1,
//             1,
//             "a",
//             false,
//             []);
//
//         _volunteersRepositoryMock.Setup(x => x.GetById(volunteerId, ct))
//             .ReturnsAsync(volunteer);
//
//         _unitOfWorkMock.Setup(x => x.SaveChangesAsync(ct))
//             .ReturnsAsync(1);
//
//         _minioProviderMock
//             .Setup(x => x.UploadPhoto(_formFileMock.Object, It.IsAny<string>()))
//             .ReturnsAsync(Result<string>.Success("path"));
//
//         var sut = new UploadVolunteerPhotoHandler(
//             _minioProviderMock.Object,
//             _volunteersRepositoryMock.Object,
//             _unitOfWorkMock.Object);
//
//         //act
//         var result = await sut.Handle(request, ct);
//
//         //assert
//         _volunteersRepositoryMock.Verify(x => x.GetById(volunteerId, ct), Times.Once);
//         _unitOfWorkMock.Verify(x => x.SaveChangesAsync(ct), Times.Once);
//         _minioProviderMock
//             .Verify(x => x.UploadPhoto(_formFileMock.Object, It.IsAny<string>()), Times.Once);
//
//         result.IsSuccess.Should().Be(true);
//         result.Value.Should().BeOfType<string>();
//         result.Value.Should().NotBeEmpty();
//     }
// }