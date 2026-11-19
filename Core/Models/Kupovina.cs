using System;

namespace Core.Models
{
    public class Kupovina
    {
        public int Id { get; set; }
        public int PosetilacId { get; set; }
        public Posetilac? Posetilac { get; set; }
        public int KnjigaId { get; set; }
        public Knjiga? Knjiga { get; set; }
        public DateTime DatumKupovine { get; set; }
        public int Ocena { get; set; } // 1 - 5
        public string Komentar { get; set; } = string.Empty;

        public Kupovina() { }

        public Kupovina(int id, int posetilacId, int knjigaId, DateTime datumKupovine, int ocena, string komentar)
        {
            Id = id;
            PosetilacId = posetilacId;
            KnjigaId = knjigaId;
            DatumKupovine = datumKupovine;
            Ocena = ocena;
            Komentar = komentar;
        }

        public override string ToString()
        {
            return $"[{Id}] PosetilacId: {PosetilacId} | KnjigaId: {KnjigaId} | Datum: {DatumKupovine:dd.MM.yyyy.} | " +
                   $"Ocena: {Ocena}/5 | Komentar: {Komentar}";
        }
    }
}