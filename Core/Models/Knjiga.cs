using System.Collections.Generic;
using System.Globalization;
using Core.Serialization;

namespace Core.Models
{
    public class Knjiga : ISerializable
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Naziv { get; set; } = string.Empty;
        public string Zanr { get; set; } = string.Empty;
        public int GodinaIzdanja { get; set; }
        public double Cena { get; set; }
        public int BrojStrana { get; set; }
        public int IzdavacId { get; set; }
        public Izdavac? Izdavac { get; set; }

        public List<Autor> Autori { get; set; } = new List<Autor>();
        public List<Posetilac> PosetiociKupili { get; set; } = new List<Posetilac>();
        public List<Posetilac> PosetiociListaZelja { get; set; } = new List<Posetilac>();

        public Knjiga() { }

        public Knjiga(int id, string isbn, string naziv, string zanr, int godinaIzdanja,
            double cena, int brojStrana, int izdavacId)
        {
            Id = id;
            ISBN = isbn;
            Naziv = naziv;
            Zanr = zanr;
            GodinaIzdanja = godinaIzdanja;
            Cena = cena;
            BrojStrana = brojStrana;
            IzdavacId = izdavacId;
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                ISBN,
                Naziv,
                Zanr,
                GodinaIzdanja.ToString(),
                Cena.ToString(CultureInfo.InvariantCulture),
                BrojStrana.ToString(),
                IzdavacId.ToString()
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ISBN = values[1];
            Naziv = values[2];
            Zanr = values[3];
            GodinaIzdanja = int.Parse(values[4]);
            Cena = double.Parse(values[5], CultureInfo.InvariantCulture);
            BrojStrana = int.Parse(values[6]);
            IzdavacId = int.Parse(values[7]);
        }

        public override string ToString()
        {
            return $"[{Id}] ISBN: {ISBN} | \"{Naziv}\" | Žanr: {Zanr} | Godina: {GodinaIzdanja} | " +
                   $"Cena: {Cena:F2} RSD | Strana: {BrojStrana} | IzdavacId: {IzdavacId}";
        }
    }
}