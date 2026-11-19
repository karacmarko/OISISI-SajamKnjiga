using System.Collections.Generic;

namespace Core.Models
{
    public class Knjiga
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

        public override string ToString()
        {
            return $"[{Id}] ISBN: {ISBN} | \"{Naziv}\" | Žanr: {Zanr} | Godina: {GodinaIzdanja} | " +
                   $"Cena: {Cena:F2} RSD | Strana: {BrojStrana} | IzdavacId: {IzdavacId}";
        }
    }
}