using Microsoft.AspNetCore.Http;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Models;

public record PhotoFile(PetPhoto PetPhoto, IFormFile File);