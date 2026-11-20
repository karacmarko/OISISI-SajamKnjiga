using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class AutorDAO
    {
        private readonly List<Autor> _autori;
        private readonly FileStorage<Autor> _storage;

        public AutorDAO()
        {
            _storage = new FileStorage<Autor>("autori.csv");
            _autori = _storage.Load();
        }

        private int GenerateId()
        {
            return _autori.Count == 0 ? 1 : _autori.Max(a => a.Id) + 1;
        }

        public List<Autor> GetAll()
        {
            return _autori;
        }

        public Autor? GetById(int id)
        {
            return _autori.FirstOrDefault(a => a.Id == id);
        }

        public Autor Add(Autor autor)
        {
            autor.Id = GenerateId();
            _autori.Add(autor);
            _storage.Save(_autori);
            return autor;
        }

        public Autor? Update(Autor autor)
        {
            Autor? existing = GetById(autor.Id);
            if (existing == null) return null;

            existing.Ime = autor.Ime;
            existing.Prezime = autor.Prezime;
            existing.DatumRodjenja = autor.DatumRodjenja;
            existing.AdresaId = autor.AdresaId;
            existing.Telefon = autor.Telefon;
            existing.Email = autor.Email;
            existing.BrojLicneKarte = autor.BrojLicneKarte;
            existing.GodineIskustva = autor.GodineIskustva;

            _storage.Save(_autori);
            return existing;
        }

        public Autor? Delete(int id)
        {
            Autor? existing = GetById(id);
            if (existing == null) return null;

            _autori.Remove(existing);
            _storage.Save(_autori);
            return existing;
        }
    }
}