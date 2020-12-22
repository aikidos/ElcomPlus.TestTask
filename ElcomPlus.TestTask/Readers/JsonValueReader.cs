using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ElcomPlus.TestTask.Readers
{
    /// <summary>
    /// Реализация методов чтения значений из данных с форматом `JSON`.
    /// </summary>
    public sealed class JsonValueReader : IValueReader
    {
        /// <inheritdoc />
        public async IAsyncEnumerable<string> ReadValuesAsync(StreamReader streamReader)
        {
            if (streamReader == null)
                throw new ArgumentNullException(nameof(streamReader));

            using var jsonReader = new JsonTextReader(streamReader);

            while (await jsonReader.ReadAsync())
            {
                if (jsonReader.Value == null)
                {
                    continue;
                }

                var token = await JToken.ReadFromAsync(jsonReader);

                if (!(token is JProperty { Name: "Values", First: JArray array }))
                {
                    continue;
                }

                foreach (var arrayToken in array)
                {
                    yield return arrayToken.ToObject<string>();
                }
            }
        }
    }
}