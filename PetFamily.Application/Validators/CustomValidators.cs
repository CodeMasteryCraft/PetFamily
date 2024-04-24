using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Common;

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

    //столько мучался в изучении этого FluentValidation, а ответ был настолько прост
    //в 27 строке передавал не IRuleBuilder, а надо было IRuleBuilderOptions
    //принцип работы с FluentValidation, понял,но стоит изучить IRuleBuilder и все его различия
    public static IRuleBuilderOptions<T, TElement> WithError<T, TElement>(
        this IRuleBuilderOptions<T, TElement> ruleBuilder, Error error)
    {
        return ruleBuilder.WithMessage(error.Serialize());
    }
}
