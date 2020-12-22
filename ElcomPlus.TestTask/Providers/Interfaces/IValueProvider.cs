using System;
using System.Collections.Generic;

namespace ElcomPlus.TestTask.Providers
{
    /// <summary>
    /// Интерфейс поставщика значений.
    /// </summary>
    /// <typeparam name="TSource">Тип источника, из которого необходимо брать значения.</typeparam>>
    public interface IValueProvider<TSource>
    {
        /// <summary>
        /// Возвращает перечисление значений из указанного источника.
        /// </summary>
        /// <param name="source">Источник, из которого необходимо брать значения.</param>
        /// <exception cref="ArgumentNullException">
        ///     Параметр <paramref name="source"/> равен `NULL`.
        /// </exception>
        IAsyncEnumerable<string> GetValuesAsync(TSource source);
    }
}