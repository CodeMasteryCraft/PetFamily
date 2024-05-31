using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Common;

namespace PetFamily.Application.CommonValidators;

public static class CustomValidators
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(result.Error.Serialize());
        });
    }

    public static IRuleBuilderOptions<T, TProperty> NotEmptyWithError<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }

    public static IRuleBuilderOptions<T, string> MaximumLengthWithError<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int maxLength)
    {
        return ruleBuilder
            .MaximumLength(maxLength)
            .WithError(Errors.General.InvalidLength());
    }

    public static IRuleBuilderOptions<T, TProperty> GreaterThanWithError<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
    {
        return ruleBuilder
            .GreaterThan(valueToCompare)
            .WithError(Errors.General.InvalidLength());
    }

    public static IRuleBuilderOptions<T, TProperty?> GreaterThanWithError<T, TProperty>(
        this IRuleBuilder<T, TProperty?> ruleBuilder, TProperty valueToCompare)
        where TProperty : struct, IComparable<TProperty>, IComparable
    {
        return ruleBuilder
            .GreaterThan(valueToCompare)
            .WithError(Errors.General.InvalidLength());
    }

    public static IRuleBuilderOptions<T, TProperty> LessThanWithError<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
    {
        return ruleBuilder
            .LessThan(valueToCompare)
            .WithError(Errors.General.InvalidLength());
    }
    // public static IRuleBuilderOptions<T, TProperty> MustBePhoto<T, TProperty>(
    //     this IRuleBuilder<T, TProperty> ruleBuilder, UploadVolunteerPhotoRequest file )
    //     where TProperty : IComparable<TProperty>, IComparable
    // {
    //     string[] allowedContentTypes = { "image/jpeg", "image/png", "image/png" };
    //
    //     var res = allowedContentTypes.Contains(file.File.ContentType);
    //     return ruleBuilder
    //         .Must(p => res)
    //         .WithError(Errors.General.ValueIsInvalid());
    //     
    // }
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, Error error)
    {
        return rule
            .WithMessage(error.Serialize());
    }
}