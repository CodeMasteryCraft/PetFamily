﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Entities;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Features.SocialMedias;

public interface ISocialMediasRepository
{
    Task<Result<SocialMedia, Error>> GetById(Guid id);
}