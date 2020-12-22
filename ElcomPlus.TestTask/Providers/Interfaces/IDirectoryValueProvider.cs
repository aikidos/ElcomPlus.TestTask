using System;
using ElcomPlus.TestTask.Readers;

namespace ElcomPlus.TestTask.Providers
{
    /// <summary>
    /// Поставщик значений из директорий.
    /// </summary>
    public interface IDirectoryValueProvider : IValueProvider<string>
    {
        /// <summary>
        /// Регистрирует реализацию методов чтения значений для файлов с указанным расширением.
        /// </summary>
        /// <param name="fileExtension">
        ///     Расширение файлов, для которых необходимо применять указанную реализацию <paramref name="valueReader"/>.
        /// </param>
        /// <param name="valueReader">Реализация методов чтения значений.</param>
        /// <exception cref="ArgumentException">
        ///     Параметр <paramref name="fileExtension"/> равен `NULL` или содержит пустую строку.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Параметр <paramref name="valueReader"/> равен `NULL`.
        /// </exception>
        void RegisterReader(string fileExtension, IValueReader valueReader);
    }
}