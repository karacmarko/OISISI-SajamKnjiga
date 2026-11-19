using System.Collections.Generic;
using Core.Serialization;

namespace Core.Models
{
    public class Izdavac : ISerializable
    {
        public int Id { get; set; }
        public string Sifra { get; set; } = string.Empty;
        public string Naziv { get; set; } = string.Empty;
        public int SefIzdavacaId { get; set; } = -1; // -1 ako još nema postavljenog šefa
        public Autor? SefIzdavaca { get; set; }

        public List<Autor> Autori { get; set; } = new List<Autor>();
        public List<Knjiga> Knjige { get; set; } = new List<Knjiga>();

        public Izdavac() { }

        public Izdavac(int id, string sifra, string naziv, int sefIzdavacaId = -1)
        {
            Id = id;
            Sifra = sifra;
            Naziv = naziv;
            SefIzdavacaId = sefIzdavacaId;
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                Sifra,
                Naziv,
                SefIzdavacaId.ToString()
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Sifra = values[1];
            Naziv = values[2];
            SefIzdavacaId = int.Parse(values[3]);
        }

        public override string ToString()
        {
            string sef = SefIzdavacaId == -1 ? "Nije postavljen" : SefIzdavacaId.ToString();
            return $"[{Id}] Šifra: {Sifra} | Naziv: {Naziv} | Šef (AutorId): {sef}";
        }
    }
}