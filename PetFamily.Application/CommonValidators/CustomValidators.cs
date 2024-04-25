using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace PetFamily.Application.CommonValidators;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(result.Error.Serialize());
        });
    }

    public static IRuleBuilderOptions<T, TElement> WithError<T, TElement>(
        this IRuleBuilderOptions<T, TElement> ruleBuilderOptions, Error error)
    {
        return ruleBuilderOptions.WithMessage(error.Serialize());
    }
}