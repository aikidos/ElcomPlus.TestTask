using System;
using System.Collections.Generic;
using System.IO;

namespace ElcomPlus.TestTask.Readers
{
    /// <summary>
    /// Интерфейс методов чтения значений.
    /// </summary>
    public interface IValueReader
    {
        /// <summary>
        /// Возвращает список значений, считанных из указанного потока.
        /// </summary>
        /// <param name="streamReader">Поток.</param>
        /// <exception cref="ArgumentNullException">
        ///     Параметр <paramref name="streamReader"/> равен `NULL`.
        /// </exception>
        IAsyncEnumerable<string> ReadValuesAsync(StreamReader streamReader);
    }
}