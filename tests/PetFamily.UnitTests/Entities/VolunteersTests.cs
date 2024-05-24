using FluentAssertions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.UnitTests.Entities;

public class VolunteersTests
{
    [Theory]
    [MemberData(nameof(CreateValidVolunteerData))]
    public void Create_with_valid_parameters_return_volunteer(
        FullName fullName,
        string description,
        int yearsExperience,
        int numberPetsFoundHome,
        string donationInfo,
        bool fromShelter,
        List<SocialMedia> socialMedias)
    {
        //act
        var result = Volunteer.Create(
            fullName,
            description,
            yearsExperience,
            numberPetsFoundHome,
            donationInfo,
            fromShelter,
            socialMedias);

        //assert
        result.IsSuccess.Should().Be(true);
        result.IsFailure.Should().Be(false);
        result.Error.Should().Be(Error.None);
        result.Value.Should().NotBeNull();
    }

    [Theory]
    [MemberData(nameof(CreateInValidVolunteerData))]
    public void Create_with_invalid_parameters_return_error(
        FullName fullName,
        string description,
        int yearsExperience,
        int numberPetsFoundHome,
        string donationInfo,
        bool fromShelter,
        List<SocialMedia> socialMedias)
    {
        //act
        var result = Volunteer.Create(
            fullName,
            description,
            yearsExperience,
            numberPetsFoundHome,
            donationInfo,
            fromShelter,
            socialMedias);

        //assert
        result.IsSuccess.Should().Be(false);
        result.IsFailure.Should().Be(true);
        result.Error.Should().NotBe(Error.None);
        result.Error.Should().NotBeNull();
        result.Invoking(x => x.Value).Should()
            .Throw<InvalidOperationException>()
            .WithMessage("The value of a failure result can not be accessed.");
    }

    [Fact]
    public void AddPhoto_when_volunteer_has_zero_photos()
    {
        //arrange
        var volunteer = Volunteer.Create(
            FullName.Create("Kirill", "Sachkov", "Olegovich").Value,
            "a",
            1,
            1,
            "a",
            false,
            []).Value;

        var volunteerPhoto = VolunteerPhoto.CreateAndActivate("file.png").Value;

        //act
        var result = volunteer.AddPhoto(volunteerPhoto);

        //assert
        result.IsSuccess.Should().Be(true);
        volunteer.Photos.Should().Contain(volunteerPhoto);
        volunteer.Photos.Should().HaveCount(1);
    }

    [Fact]
    public void AddPhoto_when_volunteer_has_five_photos()
    {
        //arrange
        var volunteer = Volunteer.Create(
            FullName.Create("Kirill", "Sachkov", "Olegovich").Value,
            "a",
            1,
            1,
            "a",
            false,
            []).Value;

        var volunteerPhoto1 = VolunteerPhoto.CreateAndActivate("1").Value;
        var volunteerPhoto2 = VolunteerPhoto.CreateAndActivate("2").Value;
        var volunteerPhoto3 = VolunteerPhoto.CreateAndActivate("3").Value;
        var volunteerPhoto4 = VolunteerPhoto.CreateAndActivate("4").Value;
        var volunteerPhoto5 = VolunteerPhoto.CreateAndActivate("5").Value;
        var volunteerPhoto6 = VolunteerPhoto.CreateAndActivate("6").Value;

        volunteer.AddPhoto(volunteerPhoto1);
        volunteer.AddPhoto(volunteerPhoto2);
        volunteer.AddPhoto(volunteerPhoto3);
        volunteer.AddPhoto(volunteerPhoto4);
        volunteer.AddPhoto(volunteerPhoto5);

        //act
        var result = volunteer.AddPhoto(volunteerPhoto6);

        //assert
        result.IsSuccess.Should().Be(false);
        volunteer.Photos.Should().NotContain(volunteerPhoto6);
        volunteer.Photos.Should().HaveCount(5);
    }

    public static IEnumerable<object[]> CreateValidVolunteerData =>
        new List<object[]>
        {
            new object[]
            {
                FullName.Create("Kirill", "Sachkov", "Olegovich").Value,
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                1,
                1,
                "",
                false,
                new List<SocialMedia>
                {
                    SocialMedia.Create("https://t.me/KSachkov", Social.Telegram).Value
                }
            },
            new object[]
            {
                FullName.Create("Kirill", "Sachkov", null).Value,
                "L",
                1,
                100,
                "sdfffdaf",
                false,
                new List<SocialMedia>
                {
                    SocialMedia.Create("https://t.me/KSachkov", Social.Telegram).Value,
                    SocialMedia.Create("https://t.me/KSachkov", Social.Telegram).Value,
                    SocialMedia.Create("https://t.me/KSachkov", Social.Telegram).Value,
                    SocialMedia.Create("https://t.me/KSachkov", Social.Telegram).Value
                }
            },
            new object[]
            {
                FullName.Create("Kirill", "Sachkov", null).Value,
                "L",
                1,
                100,
                "sdfffdaf",
                false,
                new List<SocialMedia>()
            },
        };

    public static IEnumerable<object[]> CreateInValidVolunteerData =>
        new List<object[]>
        {
            new object[]
            {
                FullName.Create("Kirill", "Sachkov", "Olegovich").Value,
                "",
                -1,
                -2,
                "",
                false,
                new List<SocialMedia>
                {
                    SocialMedia.Create("https://t.me/KSachkov", Social.Telegram).Value
                }
            },
            new object[]
            {
                FullName.Create("Kirill", "Sachkov", null).Value,
                "",
                1000000000,
                -2,
                "",
                false,
                new List<SocialMedia>
                {
                    SocialMedia.Create("https://t.me/KSachkov", Social.Telegram).Value,
                    SocialMedia.Create("https://t.me/KSachkov", Social.Telegram).Value,
                    SocialMedia.Create("https://t.me/KSachkov", Social.Telegram).Value,
                    SocialMedia.Create("https://t.me/KSachkov", Social.Telegram).Value
                }
            },
            new object[]
            {
                FullName.Create("Kirill", "Sachkov", null).Value,
                "L",
                1000,
                100,
                "sdfffdaf",
                false,
                new List<SocialMedia>()
            },
        };
}