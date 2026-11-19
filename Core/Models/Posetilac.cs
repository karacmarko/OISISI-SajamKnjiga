using System;
using System.Collections.Generic;
using System.Globalization;
using Core.Serialization;

namespace Core.Models
{
    public class Posetilac : ISerializable
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public DateTime DatumRodjenja { get; set; }
        public int AdresaId { get; set; }
        public Adresa? Adresa { get; set; }
        public string KontaktTelefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BrojClanskeKarte { get; set; } = string.Empty;
        public int TrenutnaGodinaClanstva { get; set; }
        public StatusPosetioca Status { get; set; }
        public double ProsecnaOcena { get; set; }

        public List<Kupovina> KupljeneKnjige { get; set; } = new List<Kupovina>();
        public List<Knjiga> ListaZelja { get; set; } = new List<Knjiga>();

        public Posetilac() { }

        public Posetilac(int id, string ime, string prezime, DateTime datumRodjenja, int adresaId,
            string kontaktTelefon, string email, string brojClanskeKarte, int trenutnaGodinaClanstva,
            StatusPosetioca status, double prosecnaOcena = 0.0)
        {
            Id = id;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            AdresaId = adresaId;
            KontaktTelefon = kontaktTelefon;
            Email = email;
            BrojClanskeKarte = brojClanskeKarte;
            TrenutnaGodinaClanstva = trenutnaGodinaClanstva;
            Status = status;
            ProsecnaOcena = prosecnaOcena;
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                Ime,
                Prezime,
                DatumRodjenja.ToString("dd.MM.yyyy.", CultureInfo.InvariantCulture),
                AdresaId.ToString(),
                KontaktTelefon,
                Email,
                BrojClanskeKarte,
                TrenutnaGodinaClanstva.ToString(),
                Status.ToString(),
                ProsecnaOcena.ToString(CultureInfo.InvariantCulture)
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            DatumRodjenja = DateTime.ParseExact(values[3], "dd.MM.yyyy.", CultureInfo.InvariantCulture);
            AdresaId = int.Parse(values[4]);
            KontaktTelefon = values[5];
            Email = values[6];
            BrojClanskeKarte = values[7];
            TrenutnaGodinaClanstva = int.Parse(values[8]);
            Status = Enum.Parse<StatusPosetioca>(values[9]);
            ProsecnaOcena = double.Parse(values[10], CultureInfo.InvariantCulture);
        }

        public override string ToString()
        {
            return $"[{Id}] {BrojClanskeKarte} | {Ime} {Prezime} | Rođen: {DatumRodjenja:dd.MM.yyyy.} | " +
                   $"Tel: {KontaktTelefon} | Email: {Email} | God. članstva: {TrenutnaGodinaClanstva} | " +
                   $"Status: {Status} | Prosečna ocena: {ProsecnaOcena:F2} | AdresaId: {AdresaId}";
        }
    }
}