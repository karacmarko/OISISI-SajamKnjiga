using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class IzdavacDAO
    {
        private readonly List<Izdavac> _izdavaci;
        private readonly FileStorage<Izdavac> _storage;

        public IzdavacDAO()
        {
            _storage = new FileStorage<Izdavac>("izdavaci.csv");
            _izdavaci = _storage.Load();
        }

        private int GenerateId()
        {
            return _izdavaci.Count == 0 ? 1 : _izdavaci.Max(i => i.Id) + 1;
        }

        public List<Izdavac> GetAll()
        {
            return _izdavaci;
        }

        public Izdavac? GetById(int id)
        {
            return _izdavaci.FirstOrDefault(i => i.Id == id);
        }

        public Izdavac Add(Izdavac izdavac)
        {
            izdavac.Id = GenerateId();
            _izdavaci.Add(izdavac);
            _storage.Save(_izdavaci);
            return izdavac;
        }

        public Izdavac? Update(Izdavac izdavac)
        {
            Izdavac? existing = GetById(izdavac.Id);
            if (existing == null) return null;

            existing.Sifra = izdavac.Sifra;
            existing.Naziv = izdavac.Naziv;
            existing.SefIzdavacaId = izdavac.SefIzdavacaId;

            _storage.Save(_izdavaci);
            return existing;
        }

        public Izdavac? Delete(int id)
        {
            Izdavac? existing = GetById(id);
            if (existing == null) return null;

            _izdavaci.Remove(existing);
            _storage.Save(_izdavaci);
            return existing;
        }
    }
}