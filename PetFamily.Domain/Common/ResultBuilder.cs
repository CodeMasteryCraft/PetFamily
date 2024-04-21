using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Common;

public sealed class ResultBuilder
{
    private readonly List<Error> _errors = [];

    private ResultBuilder() {}

    public static ResultBuilder Create() => new();

    /// <summary>
    /// Добавляет условие для ошибки.
    /// </summary>
    /// <param name="condition">Фуктор, возвращающий результат проверки условия</param>
    /// <param name="factory">Фабрика-поставщик ошибки</param>
    /// <remarks>Если ошибочное условие соблюдается, в список ошибок добавляется <see cref="Error"/>,
    /// сгенерированный фабрикой <paramref name="factory"/>.</remarks>
    public ResultBuilder AddErrorCondition(Func<bool> condition, Func<Error> factory)
    {
        if (condition())
            _errors.Add(factory());
        return this;
    }

    /// <summary>
    /// Строит результат по всем правилам...
    /// </summary>
    /// <param name="factory">Фабрика для создания экзепляра <typeparamref name="TValue"/></param>
    /// <returns>Возвращает паттерн Result</returns>
    /// <remarks>Если ниодно условие, добавленное <see cref="ResultBuilder.AddErrorCondition(Func{bool}, Func{Error})"/>
    /// не выполнится,<br/>вызовется фабрика <paramref name="factory"/> для создания экземпляра <typeparamref name="TValue"/>,
    /// котороый будет передан в зачении результата,<br/>в противном случае, вернется результат со списком ошибок</remarks>
    public Result<TValue, IReadOnlyList<Error>> Build<TValue>(Func<TValue> factory) =>
        _errors.Count != 0 ? _errors : factory();
}
