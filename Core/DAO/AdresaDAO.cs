using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class AdresaDAO
    {
        private readonly List<Adresa> _adrese;
        private readonly FileStorage<Adresa> _storage;

        public AdresaDAO()
        {
            _storage = new FileStorage<Adresa>("adrese.csv");
            _adrese = _storage.Load();
        }

        private int GenerateId()
        {
            return _adrese.Count == 0 ? 1 : _adrese.Max(a => a.Id) + 1;
        }

        public List<Adresa> GetAll()
        {
            return _adrese;
        }

        public Adresa? GetById(int id)
        {
            return _adrese.FirstOrDefault(a => a.Id == id);
        }

        public Adresa Add(Adresa adresa)
        {
            adresa.Id = GenerateId();
            _adrese.Add(adresa);
            _storage.Save(_adrese);
            return adresa;
        }

        public Adresa? Update(Adresa adresa)
        {
            Adresa? existing = GetById(adresa.Id);
            if (existing == null) return null;

            existing.Ulica = adresa.Ulica;
            existing.Broj = adresa.Broj;
            existing.Grad = adresa.Grad;
            existing.Drzava = adresa.Drzava;

            _storage.Save(_adrese);
            return existing;
        }

        public Adresa? Delete(int id)
        {
            Adresa? existing = GetById(id);
            if (existing == null) return null;

            _adrese.Remove(existing);
            _storage.Save(_adrese);
            return existing;
        }
    }
}