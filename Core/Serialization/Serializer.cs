using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Serialization
{
    public class Serializer<T> where T : ISerializable, new()
    {
        private const char Delimiter = '|';

        public void ToCSV(string fileName, List<T> objects)
        {
            StringBuilder sb = new StringBuilder();

            foreach (T obj in objects)
            {
                string line = string.Join(Delimiter, obj.ToCSV());
                sb.AppendLine(line);
            }

            string? directory = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(fileName, sb.ToString());
        }

        public List<T> FromCSV(string fileName)
        {
            List<T> objects = new List<T>();

            if (!File.Exists(fileName))
            {
                return objects;
            }

            foreach (string line in File.ReadLines(fileName))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] csvValues = line.Split(Delimiter);
                T obj = new T();
                obj.FromCSV(csvValues);
                objects.Add(obj);
            }

            return objects;
        }
    }
}