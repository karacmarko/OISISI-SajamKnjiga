using System.Collections.Generic;
using System.IO;
using Core.Serialization;

namespace Core.Storage
{
    public class FileStorage<T> where T : ISerializable, new()
    {
        private readonly string _filePath;
        private readonly Serializer<T> _serializer = new Serializer<T>();

        public FileStorage(string fileName)
        {
            _filePath = Path.Combine("Data", fileName);
        }

        public List<T> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<T> objects)
        {
            _serializer.ToCSV(_filePath, objects);
        }
    }
}