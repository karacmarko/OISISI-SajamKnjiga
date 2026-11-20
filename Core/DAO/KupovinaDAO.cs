using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class KupovinaDAO
    {
        private readonly List<Kupovina> _kupovine;
        private readonly FileStorage<Kupovina> _storage;

        public KupovinaDAO()
        {
            _storage = new FileStorage<Kupovina>("kupovine.csv");
            _kupovine = _storage.Load();
        }

        private int GenerateId()
        {
            return _kupovine.Count == 0 ? 1 : _kupovine.Max(k => k.Id) + 1;
        }

        public List<Kupovina> GetAll()
        {
            return _kupovine;
        }

        public Kupovina? GetById(int id)
        {
            return _kupovine.FirstOrDefault(k => k.Id == id);
        }

        public Kupovina Add(Kupovina kupovina)
        {
            kupovina.Id = GenerateId();
            _kupovine.Add(kupovina);
            _storage.Save(_kupovine);
            return kupovina;
        }

        public Kupovina? Update(Kupovina kupovina)
        {
            Kupovina? existing = GetById(kupovina.Id);
            if (existing == null) return null;

            existing.PosetilacId = kupovina.PosetilacId;
            existing.KnjigaId = kupovina.KnjigaId;
            existing.DatumKupovine = kupovina.DatumKupovine;
            existing.Ocena = kupovina.Ocena;
            existing.Komentar = kupovina.Komentar;

            _storage.Save(_kupovine);
            return existing;
        }

        public Kupovina? Delete(int id)
        {
            Kupovina? existing = GetById(id);
            if (existing == null) return null;

            _kupovine.Remove(existing);
            _storage.Save(_kupovine);
            return existing;
        }
    }
}