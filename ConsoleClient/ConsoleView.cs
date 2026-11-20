using System;
using System.Collections.Generic;
using Core.DAO;
using Core.Models;

namespace ConsoleClient
{
    public class ConsoleView
    {
        private readonly AdresaDAO _adresaDao = new AdresaDAO();
        private readonly PosetilacDAO _posetilacDao = new PosetilacDAO();
        private readonly AutorDAO _autorDao = new AutorDAO();
        private readonly KnjigaDAO _knjigaDao = new KnjigaDAO();
        private readonly KupovinaDAO _kupovinaDao = new KupovinaDAO();
        private readonly IzdavacDAO _izdavacDao = new IzdavacDAO();

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n==========================================");
                Console.WriteLine("      SAJAM KNJIGA - GLAVNI MENI");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Adrese");
                Console.WriteLine("2. Posetioci");
                Console.WriteLine("3. Autori");
                Console.WriteLine("4. Knjige");
                Console.WriteLine("5. Kupovine");
                Console.WriteLine("6. Izdavači");
                Console.WriteLine("0. Izlaz");
                Console.WriteLine("------------------------------------------");

                int choice = InputHelper.ReadInt("Izaberite opciju: ", 0, 6);

                switch (choice)
                {
                    case 1:
                        RunEntityMenu("ADRESE", () => PrintAll("Adrese", _adresaDao.GetAll()), AddAdresa, UpdateAdresa, DeleteAdresa);
                        break;
                    case 2:
                        RunEntityMenu("POSETIOCI", () => PrintAll("Posetioci", _posetilacDao.GetAll()), AddPosetilac, UpdatePosetilac, DeletePosetilac);
                        break;
                    case 3:
                        RunEntityMenu("AUTORI", () => PrintAll("Autori", _autorDao.GetAll()), AddAutor, UpdateAutor, DeleteAutor);
                        break;
                    case 4:
                        RunEntityMenu("KNJIGE", () => PrintAll("Knjige", _knjigaDao.GetAll()), AddKnjiga, UpdateKnjiga, DeleteKnjiga);
                        break;
                    case 5:
                        RunEntityMenu("KUPOVINE", () => PrintAll("Kupovine", _kupovinaDao.GetAll()), AddKupovina, UpdateKupovina, DeleteKupovina);
                        break;
                    case 6:
                        RunEntityMenu("IZDAVAČI", () => PrintAll("Izdavači", _izdavacDao.GetAll()), AddIzdavac, UpdateIzdavac, DeleteIzdavac);
                        break;
                    case 0:
                        Console.WriteLine("Doviđenja!");
                        return;
                }
            }
        }

        private void RunEntityMenu(string title, Action showAll, Action add, Action update, Action delete)
        {
            while (true)
            {
                Console.WriteLine($"\n--- {title} ---");
                Console.WriteLine("1. Prikaži sve");
                Console.WriteLine("2. Dodaj");
                Console.WriteLine("3. Izmeni");
                Console.WriteLine("4. Obriši");
                Console.WriteLine("0. Nazad");

                int choice = InputHelper.ReadInt("Izaberite opciju: ", 0, 4);

                switch (choice)
                {
                    case 1: showAll(); break;
                    case 2: add(); break;
                    case 3: update(); break;
                    case 4: delete(); break;
                    case 0: return;
                }
            }
        }

        private void PrintAll<T>(string title, List<T> items)
        {
            Console.WriteLine($"\n--- Lista: {title} ({items.Count}) ---");
            if (items.Count == 0)
            {
                Console.WriteLine("Nema unetih zapisa.");
                return;
            }

            foreach (T item in items)
            {
                Console.WriteLine(item);
            }
        }

        // ==================== ADRESA ====================
        private void AddAdresa()
        {
            Console.WriteLine("\nDodavanje adrese:");
            string ulica = InputHelper.ReadString("Ulica: ");
            string broj = InputHelper.ReadString("Broj: ");
            string grad = InputHelper.ReadString("Grad: ");
            string drzava = InputHelper.ReadString("Država: ");

            Adresa added = _adresaDao.Add(new Adresa(0, ulica, broj, grad, drzava));
            Console.WriteLine($"Uspešno dodato: {added}");
        }

        private void UpdateAdresa()
        {
            PrintAll("Adrese", _adresaDao.GetAll());
            if (_adresaDao.GetAll().Count == 0) return;
            int id = InputHelper.ReadInt("Unesite ID adrese za izmenu: ", 1);

            if (_adresaDao.GetById(id) == null)
            {
                Console.WriteLine("Adresa sa unetim ID-em ne postoji.");
                return;
            }

            string ulica = InputHelper.ReadString("Nova ulica: ");
            string broj = InputHelper.ReadString("Novi broj: ");
            string grad = InputHelper.ReadString("Novi grad: ");
            string drzava = InputHelper.ReadString("Nova država: ");

            Adresa? updated = _adresaDao.Update(new Adresa(id, ulica, broj, grad, drzava));
            Console.WriteLine($"Uspešno izmenjeno: {updated}");
        }

        private void DeleteAdresa()
        {
            PrintAll("Adrese", _adresaDao.GetAll());
            if (_adresaDao.GetAll().Count == 0) return;
            int id = InputHelper.ReadInt("Unesite ID adrese za brisanje: ", 1);

            Adresa? deleted = _adresaDao.Delete(id);
            Console.WriteLine(deleted == null
                ? "Adresa sa unetim ID-em ne postoji."
                : $"Uspešno obrisano: {deleted}");
        }

        // ==================== POSETILAC ====================
        private void AddPosetilac()
        {
            Console.WriteLine("\nDodavanje posetioca:");
            string ime = InputHelper.ReadString("Ime: ");
            string prezime = InputHelper.ReadString("Prezime: ");
            DateTime datumRodjenja = InputHelper.ReadDate("Datum rođenja (dd.MM.yyyy.): ");
            int adresaId = InputHelper.ReadInt("ID adrese: ", 0);
            string telefon = InputHelper.ReadString("Kontakt telefon: ");
            string email = InputHelper.ReadString("Email: ");
            string clanskaKarta = InputHelper.ReadString("Broj članske karte (npr. CK-1-2026): ");
            int godinaClanstva = InputHelper.ReadInt("Trenutna godina članstva: ", 1);
            StatusPosetioca status = InputHelper.ReadStatus("Status (1 - Redovan, 2 - VIP): ");
            double prosecnaOcena = InputHelper.ReadDouble("Prosečna ocena (0-5): ", 0, 5);

            Posetilac added = _posetilacDao.Add(new Posetilac(0, ime, prezime, datumRodjenja, adresaId,
                telefon, email, clanskaKarta, godinaClanstva, status, prosecnaOcena));
            Console.WriteLine($"Uspešno dodato: {added}");
        }

        private void UpdatePosetilac()
        {
            PrintAll("Posetioci", _posetilacDao.GetAll());
            if (_posetilacDao.GetAll().Count == 0) return;
            int id = InputHelper.ReadInt("Unesite ID posetioca za izmenu: ", 1);

            if (_posetilacDao.GetById(id) == null)
            {
                Console.WriteLine("Posetilac sa unetim ID-em ne postoji.");
                return;
            }

            string ime = InputHelper.ReadString("Novo ime: ");
            string prezime = InputHelper.ReadString("Novo prezime: ");
            DateTime datumRodjenja = InputHelper.ReadDate("Novi datum rođenja (dd.MM.yyyy.): ");
            int adresaId = InputHelper.ReadInt("Novi ID adrese: ", 0);
            string telefon = InputHelper.ReadString("Novi kontakt telefon: ");
            string email = InputHelper.ReadString("Novi email: ");
            string clanskaKarta = InputHelper.ReadString("Novi broj članske karte: ");
            int godinaClanstva = InputHelper.ReadInt("Nova trenutna godina članstva: ", 1);
            StatusPosetioca status = InputHelper.ReadStatus("Novi status (1 - Redovan, 2 - VIP): ");
            double prosecnaOcena = InputHelper.ReadDouble("Nova prosečna ocena (0-5): ", 0, 5);

            Posetilac? updated = _posetilacDao.Update(new Posetilac(id, ime, prezime, datumRodjenja, adresaId,
                telefon, email, clanskaKarta, godinaClanstva, status, prosecnaOcena));
            Console.WriteLine($"Uspešno izmenjeno: {updated}");
        }

        private void DeletePosetilac()
        {
            PrintAll("Posetioci", _posetilacDao.GetAll());
            if (_posetilacDao.GetAll().Count == 0) return;
            int id = InputHelper.ReadInt("Unesite ID posetioca za brisanje: ", 1);

            Posetilac? deleted = _posetilacDao.Delete(id);
            Console.WriteLine(deleted == null
                ? "Posetilac sa unetim ID-em ne postoji."
                : $"Uspešno obrisano: {deleted}");
        }

        // ==================== AUTOR ====================
        private void AddAutor()
        {
            Console.WriteLine("\nDodavanje autora:");
            string ime = InputHelper.ReadString("Ime: ");
            string prezime = InputHelper.ReadString("Prezime: ");
            DateTime datumRodjenja = InputHelper.ReadDate("Datum rođenja (dd.MM.yyyy.): ");
            int adresaId = InputHelper.ReadInt("ID adrese: ", 0);
            string telefon = InputHelper.ReadString("Telefon: ");
            string email = InputHelper.ReadString("Email: ");
            string licnaKarta = InputHelper.ReadString("Broj lične karte: ");
            int godineIskustva = InputHelper.ReadInt("Godine iskustva: ", 0);

            Autor added = _autorDao.Add(new Autor(0, ime, prezime, datumRodjenja, adresaId,
                telefon, email, licnaKarta, godineIskustva));
            Console.WriteLine($"Uspešno dodato: {added}");
        }

        private void UpdateAutor()
        {
            PrintAll("Autori", _autorDao.GetAll());
            if (_autorDao.GetAll().Count == 0) return;
            int id = InputHelper.ReadInt("Unesite ID autora za izmenu: ", 1);

            if (_autorDao.GetById(id) == null)
            {
                Console.WriteLine("Autor sa unetim ID-em ne postoji.");
                return;
            }

            string ime = InputHelper.ReadString("Novo ime: ");
            string prezime = InputHelper.ReadString("Novo prezime: ");
            DateTime datumRodjenja = InputHelper.ReadDate("Novi datum rođenja (dd.MM.yyyy.): ");
            int adresaId = InputHelper.ReadInt("Novi ID adrese: ", 0);
            string telefon = InputHelper.ReadString("Novi telefon: ");
            string email = InputHelper.ReadString("Novi email: ");
            string licnaKarta = InputHelper.ReadString("Novi broj lične karte: ");
            int godineIskustva = InputHelper.ReadInt("Nove godine iskustva: ", 0);

            Autor? updated = _autorDao.Update(new Autor(id, ime, prezime, datumRodjenja, adresaId,
                telefon, email, licnaKarta, godineIskustva));
            Console.WriteLine($"Uspešno izmenjeno: {updated}");
        }

        private void DeleteAutor()
        {
            PrintAll("Autori", _autorDao.GetAll());
            if (_autorDao.GetAll().Count == 0) return;
            int id = InputHelper.ReadInt("Unesite ID autora za brisanje: ", 1);

            Autor? deleted = _autorDao.Delete(id);
            Console.WriteLine(deleted == null
                ? "Autor sa unetim ID-em ne postoji."
                : $"Uspešno obrisano: {deleted}");
        }

        // ==================== KNJIGA ====================
        private void AddKnjiga()
        {
            Console.WriteLine("\nDodavanje knjige:");
            string isbn = InputHelper.ReadString("ISBN: ");
            string naziv = InputHelper.ReadString("Naziv: ");
            string zanr = InputHelper.ReadString("Žanr: ");
            int godinaIzdanja = InputHelper.ReadInt("Godina izdanja: ", 1);
            double cena = InputHelper.ReadDouble("Cena (RSD): ", 0);
            int brojStrana = InputHelper.ReadInt("Broj strana: ", 1);
            int izdavacId = InputHelper.ReadInt("ID izdavača: ", 0);

            Knjiga added = _knjigaDao.Add(new Knjiga(0, isbn, naziv, zanr, godinaIzdanja, cena, brojStrana, izdavacId));
            Console.WriteLine($"Uspešno dodato: {added}");
        }

        private void UpdateKnjiga()
        {
            PrintAll("Knjige", _knjigaDao.GetAll());
            if (_knjigaDao.GetAll().Count == 0) return;
            int id = InputHelper.ReadInt("Unesite ID knjige za izmenu: ", 1);

            if (_knjigaDao.GetById(id) == null)
            {
                Console.WriteLine("Knjiga sa unetim ID-em ne postoji.");
                return;
            }

            string isbn = InputHelper.ReadString("Novi ISBN: ");
            string naziv = InputHelper.ReadString("Novi naziv: ");
            string zanr = InputHelper.ReadString("Novi žanr: ");
            int godinaIzdanja = InputHelper.ReadInt("Nova godina izdanja: ", 1);
            double cena = InputHelper.ReadDouble("Nova cena (RSD): ", 0);
            int brojStrana = InputHelper.ReadInt("Novi broj strana: ", 1);
            int izdavacId = InputHelper.ReadInt("Novi ID izdavača: ", 0);

            Knjiga? updated = _knjigaDao.Update(new Knjiga(id, isbn, naziv, zanr, godinaIzdanja, cena, brojStrana, izdavacId));
            Console.WriteLine($"Uspešno izmenjeno: {updated}");
        }

        private void DeleteKnjiga()
        {
            PrintAll("Knjige", _knjigaDao.GetAll());
            if (_knjigaDao.GetAll().Count == 0) return;
            int id = InputHelper.ReadInt("Unesite ID knjige za brisanje: ", 1);

            Knjiga? deleted = _knjigaDao.Delete(id);
            Console.WriteLine(deleted == null
                ? "Knjiga sa unetim ID-em ne postoji."
                : $"Uspešno obrisano: {deleted}");
        }

        // ==================== KUPOVINA ====================
        private void AddKupovina()
        {
            Console.WriteLine("\nDodavanje kupovine:");
            int posetilacId = InputHelper.ReadInt("ID posetioca: ", 1);
            int knjigaId = InputHelper.ReadInt("ID knjige: ", 1);
            DateTime datumKupovine = InputHelper.ReadDate("Datum kupovine (dd.MM.yyyy.): ");
            int ocena = InputHelper.ReadInt("Ocena (1-5): ", 1, 5);
            string komentar = InputHelper.ReadString("Komentar (može prazno): ", allowEmpty: true);

            Kupovina added = _kupovinaDao.Add(new Kupovina(0, posetilacId, knjigaId, datumKupovine, ocena, komentar));
            Console.WriteLine($"Uspešno dodato: {added}");
        }

        private void UpdateKupovina()
        {
            PrintAll("Kupovine", _kupovinaDao.GetAll());
            if (_kupovinaDao.GetAll().Count == 0) return;
            int id = InputHelper.ReadInt("Unesite ID kupovine za izmenu: ", 1);

            if (_kupovinaDao.GetById(id) == null)
            {
                Console.WriteLine("Kupovina sa unetim ID-em ne postoji.");
                return;
            }

            int posetilacId = InputHelper.ReadInt("Novi ID posetioca: ", 1);
            int knjigaId = InputHelper.ReadInt("Novi ID knjige: ", 1);
            DateTime datumKupovine = InputHelper.ReadDate("Novi datum kupovine (dd.MM.yyyy.): ");
            int ocena = InputHelper.ReadInt("Nova ocena (1-5): ", 1, 5);
            string komentar = InputHelper.ReadString("Novi komentar (može prazno): ", allowEmpty: true);

            Kupovina? updated = _kupovinaDao.Update(new Kupovina(id, posetilacId, knjigaId, datumKupovine, ocena, komentar));
            Console.WriteLine($"Uspešno izmenjeno: {updated}");
        }

        private void DeleteKupovina()
        {
            PrintAll("Kupovine", _kupovinaDao.GetAll());
            if (_kupovinaDao.GetAll().Count == 0) return;
            int id = InputHelper.ReadInt("Unesite ID kupovine za brisanje: ", 1);

            Kupovina? deleted = _kupovinaDao.Delete(id);
            Console.WriteLine(deleted == null
                ? "Kupovina sa unetim ID-em ne postoji."
                : $"Uspešno obrisano: {deleted}");
        }

        // ==================== IZDAVAČ ====================
        private void AddIzdavac()
        {
            Console.WriteLine("\nDodavanje izdavača:");
            string sifra = InputHelper.ReadString("Šifra: ");
            string naziv = InputHelper.ReadString("Naziv: ");
            int sefId = InputHelper.ReadInt("ID šefa izdavača (-1 ako nije postavljen): ", -1);

            Izdavac added = _izdavacDao.Add(new Izdavac(0, sifra, naziv, sefId));
            Console.WriteLine($"Uspešno dodato: {added}");
        }

        private void UpdateIzdavac()
        {
            PrintAll("Izdavači", _izdavacDao.GetAll());
            if (_izdavacDao.GetAll().Count == 0) return;
            int id = InputHelper.ReadInt("Unesite ID izdavača za izmenu: ", 1);

            if (_izdavacDao.GetById(id) == null)
            {
                Console.WriteLine("Izdavač sa unetim ID-em ne postoji.");
                return;
            }

            string sifra = InputHelper.ReadString("Nova šifra: ");
            string naziv = InputHelper.ReadString("Novi naziv: ");
            int sefId = InputHelper.ReadInt("Novi ID šefa izdavača (-1 ako nije postavljen): ", -1);

            Izdavac? updated = _izdavacDao.Update(new Izdavac(id, sifra, naziv, sefId));
            Console.WriteLine($"Uspešno izmenjeno: {updated}");
        }

        private void DeleteIzdavac()
        {
            PrintAll("Izdavači", _izdavacDao.GetAll());
            if (_izdavacDao.GetAll().Count == 0) return;
            int id = InputHelper.ReadInt("Unesite ID izdavača za brisanje: ", 1);

            Izdavac? deleted = _izdavacDao.Delete(id);
            Console.WriteLine(deleted == null
                ? "Izdavač sa unetim ID-em ne postoji."
                : $"Uspešno obrisano: {deleted}");
        }
    }
}