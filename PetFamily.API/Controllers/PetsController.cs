using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Dtos;
using PetFamily.Application.Features.Pets.GetPets;
using PetFamily.Infrastructure.Queries.Pets;

namespace PetFamily.API.Controllers;

public class PetsController : ApplicationController
{
    [HttpGet]
    public IActionResult GetAll()
    {
        List<PetDto> petDtos =
        [
            new(Guid.NewGuid(),
                "Кот",
                "Описание",
                "Город",
                "Улица",
                "Строение",
                "Индекс",
                "Номер телефона",
                DateTimeOffset.Now),
            new(Guid.NewGuid(),
                "Кот",
                "Описание",
                "Город",
                "Улица",
                "Строение",
                "Индекс",
                "Номер телефона",
                DateTimeOffset.Now),
            new(Guid.NewGuid(),
                "Кот",
                "Описание",
                "Город",
                "Улица",
                "Строение",
                "Индекс",
                "Номер телефона",
                DateTimeOffset.Now),
            new(Guid.NewGuid(),
                "Кот",
                "Описание",
                "Город",
                "Улица",
                "Строение",
                "Индекс",
                "Номер телефона",
                DateTimeOffset.Now),
            new(Guid.NewGuid(),
                "Кот",
                "Описание",
                "Город",
                "Улица",
                "Строение",
                "Индекс",
                "Номер телефона",
                DateTimeOffset.Now),
            new(Guid.NewGuid(),
                "Кот",
                "Описание",
                "Город",
                "Улица",
                "Строение",
                "Индекс",
                "Номер телефона",
                DateTimeOffset.Now),
            new(Guid.NewGuid(),
                "Кот",
                "Описание",
                "Город",
                "Улица",
                "Строение",
                "Индекс",
                "Номер телефона",
                DateTimeOffset.Now),
            new(Guid.NewGuid(),
                "Кот",
                "Описание",
                "Город",
                "Улица",
                "Строение",
                "Индекс",
                "Номер телефона",
                DateTimeOffset.Now),
            new(Guid.NewGuid(),
                "Кот",
                "Описание",
                "Город",
                "Улица",
                "Строение",
                "Индекс",
                "Номер телефона",
                DateTimeOffset.Now)
        ];

        return Ok(petDtos);
    }
}