using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ElcomPlus.TestTask.Readers
{
    /// <summary>
    /// Реализация методов чтения значений из данных с форматом `XML`.
    /// </summary>
    public sealed class XmlValueReader : IValueReader
    {
        /// <inheritdoc />
        public async IAsyncEnumerable<string> ReadValuesAsync(StreamReader streamReader)
        {
            if (streamReader == null)
                throw new ArgumentNullException(nameof(streamReader));

            using var xmlReader = XmlReader.Create(streamReader, new XmlReaderSettings { Async = true });

            while (await xmlReader.ReadAsync())
            {
                if (xmlReader.NodeType == XmlNodeType.Element &&
                    xmlReader.Name == "Value")
                {
                    yield return await xmlReader.ReadInnerXmlAsync();
                }
            }
        }
    }
}