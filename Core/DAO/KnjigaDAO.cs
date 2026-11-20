using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class KnjigaDAO
    {
        private readonly List<Knjiga> _knjige;
        private readonly FileStorage<Knjiga> _storage;

        public KnjigaDAO()
        {
            _storage = new FileStorage<Knjiga>("knjige.csv");
            _knjige = _storage.Load();
        }

        private int GenerateId()
        {
            return _knjige.Count == 0 ? 1 : _knjige.Max(k => k.Id) + 1;
        }

        public List<Knjiga> GetAll()
        {
            return _knjige;
        }

        public Knjiga? GetById(int id)
        {
            return _knjige.FirstOrDefault(k => k.Id == id);
        }

        public Knjiga Add(Knjiga knjiga)
        {
            knjiga.Id = GenerateId();
            _knjige.Add(knjiga);
            _storage.Save(_knjige);
            return knjiga;
        }

        public Knjiga? Update(Knjiga knjiga)
        {
            Knjiga? existing = GetById(knjiga.Id);
            if (existing == null) return null;

            existing.ISBN = knjiga.ISBN;
            existing.Naziv = knjiga.Naziv;
            existing.Zanr = knjiga.Zanr;
            existing.GodinaIzdanja = knjiga.GodinaIzdanja;
            existing.Cena = knjiga.Cena;
            existing.BrojStrana = knjiga.BrojStrana;
            existing.IzdavacId = knjiga.IzdavacId;

            _storage.Save(_knjige);
            return existing;
        }

        public Knjiga? Delete(int id)
        {
            Knjiga? existing = GetById(id);
            if (existing == null) return null;

            _knjige.Remove(existing);
            _storage.Save(_knjige);
            return existing;
        }
    }
}