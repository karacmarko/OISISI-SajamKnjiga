using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class PosetilacDAO
    {
        private readonly List<Posetilac> _posetioci;
        private readonly FileStorage<Posetilac> _storage;

        public PosetilacDAO()
        {
            _storage = new FileStorage<Posetilac>("posetioci.csv");
            _posetioci = _storage.Load();
        }

        private int GenerateId()
        {
            return _posetioci.Count == 0 ? 1 : _posetioci.Max(p => p.Id) + 1;
        }

        public List<Posetilac> GetAll()
        {
            return _posetioci;
        }

        public Posetilac? GetById(int id)
        {
            return _posetioci.FirstOrDefault(p => p.Id == id);
        }

        public Posetilac Add(Posetilac posetilac)
        {
            posetilac.Id = GenerateId();
            _posetioci.Add(posetilac);
            _storage.Save(_posetioci);
            return posetilac;
        }

        public Posetilac? Update(Posetilac posetilac)
        {
            Posetilac? existing = GetById(posetilac.Id);
            if (existing == null) return null;

            existing.Ime = posetilac.Ime;
            existing.Prezime = posetilac.Prezime;
            existing.DatumRodjenja = posetilac.DatumRodjenja;
            existing.AdresaId = posetilac.AdresaId;
            existing.KontaktTelefon = posetilac.KontaktTelefon;
            existing.Email = posetilac.Email;
            existing.BrojClanskeKarte = posetilac.BrojClanskeKarte;
            existing.TrenutnaGodinaClanstva = posetilac.TrenutnaGodinaClanstva;
            existing.Status = posetilac.Status;
            existing.ProsecnaOcena = posetilac.ProsecnaOcena;

            _storage.Save(_posetioci);
            return existing;
        }

        public Posetilac? Delete(int id)
        {
            Posetilac? existing = GetById(id);
            if (existing == null) return null;

            _posetioci.Remove(existing);
            _storage.Save(_posetioci);
            return existing;
        }
    }
}