using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Common;
using System.Globalization;

namespace PetFamily.Application.Validators;

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
    public static IRuleBuilderOptions<T, TError> WithError<T, TError>(
        this IRuleBuilder<T, TError> rule)
    {
        return (IRuleBuilderOptions<T, TError>)rule.Custom((value, context) =>
        {
            
        });
    }
}