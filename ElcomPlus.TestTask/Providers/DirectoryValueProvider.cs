using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ElcomPlus.TestTask.Readers;

namespace ElcomPlus.TestTask.Providers
{
    /// <inheritdoc />
    public sealed class DirectoryValueProvider : IDirectoryValueProvider
    {
        private readonly Dictionary<string, IValueReader> _valueReaders = new Dictionary<string, IValueReader>();

        /// <inheritdoc />
        public void RegisterReader(string fileExtension, IValueReader valueReader)
        {
            if (valueReader == null)
                throw new ArgumentNullException(nameof(valueReader));
            if (string.IsNullOrWhiteSpace(fileExtension))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileExtension));

            _valueReaders.Add(fileExtension, valueReader);
        }

        /// <summary>
        /// Возвращает перечисление значений из всех файлов по указанному пути.
        /// </summary>
        /// <param name="path">Путь до файлов, из которых необходимо считывать значения.</param>
        /// <exception cref="ArgumentException">
        ///     Параметр <paramref name="path"/> равен `NULL` или содержит пустую строку.
        /// </exception>
        public async IAsyncEnumerable<string> GetValuesAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));

            if (_valueReaders.Count == 0)
            {
                yield break;
            }

            foreach (var (fileName, fileExtension) in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                .Select(fileName => (FileName: fileName, FileExtension: Path.GetExtension(fileName)))
                .Where(fileInfo => _valueReaders.ContainsKey(fileInfo.FileExtension)))
            {
                var valueReader = _valueReaders[fileExtension];

                await using var stream = File.OpenRead(fileName);
                using var streamReader = new StreamReader(stream);

                await foreach (var value in valueReader.ReadValuesAsync(streamReader))
                {
                    yield return value;
                }
            }
        }
    }
}