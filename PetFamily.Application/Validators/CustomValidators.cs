using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Validators;

public static class CustomValidators
{
    // https://docs.fluentvalidation.net/en/latest/custom-validators.html?highlight=custom#writing-a-custom-validator
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, IReadOnlyList<Error>>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            var result = factoryMethod(value);

            if (result.IsSuccess)
                return;

            foreach (var error in result.Error)
                context.AddFailure(error.Serialize());
        });
    }

    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, Error error)
    {
        return rule.WithMessage(error.Serialize());
    }
}