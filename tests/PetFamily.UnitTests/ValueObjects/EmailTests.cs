using FluentAssertions;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.UnitTests.ValueObjects;

public class EmailTests
{
    [Theory]
    [InlineData("kirill@gmail.com")]
    [InlineData("fdlskfj321@gmail.com")]
    [InlineData("kirill-vcnm@gmail.com")]
    [InlineData("kid321321rill-vcnm@gmail.com")]
    [InlineData("kid321321rill-vcn@yandex.ru")]
    [InlineData("DSAD_vcn@yandex.ru")]
    [InlineData("DSAD_vcn@yandex.ru       ")]
    [InlineData("     DSAD_vcn@yandex.ru       ")]
    [InlineData("     DSAD_vcn@yandex.ru")]
    public void Create_with_valid_parameters_return_email(string email)
    {
        //act
        var result = Email.Create(email);

        //assert
        result.IsSuccess.Should().Be(true);
        result.IsFailure.Should().Be(false);
        result.Error.Should().Be(Error.None);
    }

    [Theory]
    [InlineData("kirillgmail.com")]
    [InlineData("fdlskfj321gmail.com")]
    [InlineData("kirill-vcnmgmail.com")]
    [InlineData("kid321321rill-vcnmgmail.com")]
    [InlineData("kid321321rill-vcnyandex.ru")]
    [InlineData("DSAD_vcnyandex.ru")]
    [InlineData("DSAD_vcnyandex.ru       ")]
    [InlineData("     DSAD_vcnyandex.ru       ")]
    [InlineData("     DSAD_vcnyandex.ru")]
    public void Create_without_at_return_error(string email)
    {
        //act
        var result = Email.Create(email);

        //assert
        result.IsSuccess.Should().Be(false);
        result.IsFailure.Should().Be(true);
        result.Error.Should().NotBe(Error.None);
    }

    [Theory]
    [InlineData("")]
    [InlineData("          ")]
    public void Create_with_empty_string_at_return_error(string email)
    {
        //act
        var result = Email.Create(email);

        //assert
        result.IsSuccess.Should().Be(false);
        result.IsFailure.Should().Be(true);
        result.Error.Should().NotBe(Error.None);
    }
}