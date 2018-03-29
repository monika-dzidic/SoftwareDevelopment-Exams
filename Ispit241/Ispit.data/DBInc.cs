using Ispit.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ispit.data
{
    public class DBInc
    {
        static int NID = 0;
        static char[] Oznake = { 'a', 'b', 'c', 'd'};
        static int OznakaBr = 0;
        static int Razred = 0;
        static int Odjeljenje = 0;

        public static void Inc(MyContext _myContext)
        {
            //var nastavnik1 = new Nastavnik { Ime = "Jasmin Azemovic", Username = "ja", Password = "123" };
            //var nastavnik2 = new Nastavnik { Ime = "Denis Music", Username = "dm", Password = "123" };
            //var nastavnik3 = new Nastavnik { Ime = "Emina Junuz", Username = "ej", Password = "123" };
            //var nastavnik4 = new Nastavnik { Ime = "Elmir Babovic", Username = "eb", Password = "123" };
            //var nastavnik5 = new Nastavnik { Ime = "Zanin Vejzovic", Username = "zv", Password = "123" };

            //_myContext.Nastavnik.Add(nastavnik1);
            //_myContext.Nastavnik.Add(nastavnik2);
            //_myContext.Nastavnik.Add(nastavnik3);
            //_myContext.Nastavnik.Add(nastavnik4);
            //_myContext.Nastavnik.Add(nastavnik5);

            //_myContext.SaveChanges();

            //for (int i = 1; i <= 250; i++)
            //{
            //    var ucenik = new Ucenik();
            //    ucenik.Ime = "Ucenik " + i;
            //    _myContext.Ucenik.Add(ucenik);
            //}
            //_myContext.SaveChanges();

            //var predmet1 = new Predmet() { Naziv = "Arhitektura računara" };
            //var predmet2 = new Predmet() { Naziv = "Digitalna ekonomija" };
            //var predmet3 = new Predmet() { Naziv = "Diskretna matematika" };
            //var predmet4 = new Predmet() { Naziv = "Engleski jezik I" };
            //var predmet5 = new Predmet() { Naziv = "Informacijske tehnologije" };
            //var predmet6 = new Predmet() { Naziv = "Inženjerska matematika" };
            //var predmet7 = new Predmet() { Naziv = "Operativni sistemi" };
            //var predmet8 = new Predmet() { Naziv = "Programiranje I" };
            //var predmet9 = new Predmet() { Naziv = "Programiranje II" };
            //var predmet10 = new Predmet() { Naziv = "Računarske mreže" };

            //var predmet11 = new Predmet() { Naziv = "Algoritmi i strukture podataka" };
            //var predmet12 = new Predmet() { Naziv = "Analiza i dizajn softvera" };
            //var predmet13 = new Predmet() { Naziv = "Baze podataka I" };
            //var predmet14 = new Predmet() { Naziv = "Baze podataka II" };
            //var predmet15 = new Predmet() { Naziv = "Engleski jezik II" };
            //var predmet16 = new Predmet() { Naziv = "Engleski jezik III" };
            //var predmet17 = new Predmet() { Naziv = "Primjenjena statistika" };
            //var predmet18 = new Predmet() { Naziv = "Programiranje III" };
            //var predmet19 = new Predmet() { Naziv = "Sistemska i mrežna administracija" };
            //var predmet20 = new Predmet() { Naziv = "Web razvoj i dizajn" };

            //var predmet21 = new Predmet() { Naziv = "Digitalna forenzika" };
            //var predmet22 = new Predmet() { Naziv = "Etika u IT" };
            //var predmet23 = new Predmet() { Naziv = "Kompjuterska grafika" };
            //var predmet24 = new Predmet() { Naziv = "Poslovna inteligencija" };
            //var predmet25 = new Predmet() { Naziv = "Prezentiranje stručnog rada" };
            //var predmet26 = new Predmet() { Naziv = "Razvoj informacijskih sistema" };
            //var predmet27 = new Predmet() { Naziv = "Razvoj korisničkih sučelja" };
            //var predmet28 = new Predmet() { Naziv = "Razvoj softvera I" };
            //var predmet29 = new Predmet() { Naziv = "Razvoj softvera II" };
            //var predmet30 = new Predmet() { Naziv = "Sigurnost infromacijskih sistema" };
            //var predmet31 = new Predmet() { Naziv = "Upravljanje projektom" };
            //var predmet32 = new Predmet() { Naziv = "Uvod u osiguranje kvalitete softvera" };

            //_myContext.Predmet.Add(predmet1);
            //_myContext.Predmet.Add(predmet2);
            //_myContext.Predmet.Add(predmet3);
            //_myContext.Predmet.Add(predmet4);
            //_myContext.Predmet.Add(predmet5);
            //_myContext.Predmet.Add(predmet6);
            //_myContext.Predmet.Add(predmet7);
            //_myContext.Predmet.Add(predmet8);
            //_myContext.Predmet.Add(predmet9);
            //_myContext.Predmet.Add(predmet10);
            //_myContext.Predmet.Add(predmet11);
            //_myContext.Predmet.Add(predmet12);
            //_myContext.Predmet.Add(predmet13);
            //_myContext.Predmet.Add(predmet14);
            //_myContext.Predmet.Add(predmet15);
            //_myContext.Predmet.Add(predmet16);
            //_myContext.Predmet.Add(predmet17);
            //_myContext.Predmet.Add(predmet18);
            //_myContext.Predmet.Add(predmet19);
            //_myContext.Predmet.Add(predmet20);
            //_myContext.Predmet.Add(predmet21);
            //_myContext.Predmet.Add(predmet22);
            //_myContext.Predmet.Add(predmet23);
            //_myContext.Predmet.Add(predmet24);
            //_myContext.Predmet.Add(predmet25);
            //_myContext.Predmet.Add(predmet26);
            //_myContext.Predmet.Add(predmet27);
            //_myContext.Predmet.Add(predmet28);
            //_myContext.Predmet.Add(predmet29);
            //_myContext.Predmet.Add(predmet30);
            //_myContext.Predmet.Add(predmet31);
            //_myContext.Predmet.Add(predmet32);

            //_myContext.SaveChanges();

            //for (int i = 0; i < 10; i++)
            //{
            //    var odjeljenje = new Odjeljenje();
            //    odjeljenje.NastavnikId = GetNastavnikId();
            //    odjeljenje.Oznaka = GetOznakaRazreda();
            //    odjeljenje.Razred = GetRazred();
            //    _myContext.Odjeljenje.Add(odjeljenje);
            //}

            //foreach (var item in _myContext.Ucenik)
            //{
            //    UpisUOdjeljenje uod = new UpisUOdjeljenje();
            //    uod.UcenikId = item.Id;
            //    uod.OdjeljenjeId = GetOdjeljenje();
            //    _myContext.UpisUOdjeljenje.Add(uod);
            //}

            _myContext.SaveChanges();
        }
        public static int GetNastavnikId()
        {
            if(NID >= 5)
            {
                NID = 0;
            }
            return ++NID;
        }
        public static string GetOznakaRazreda()
        {
            if(OznakaBr >= 3)
            {
                OznakaBr = 0;
            }
            return Oznake[OznakaBr++].ToString();
        }
        public static int GetRazred()
        {
            if(Razred >= 3)
            {
                Razred = 0;
            }
            return ++Razred;
        }
        public static int GetOdjeljenje()
        {
            if(Odjeljenje >= 9)
            {
                Odjeljenje = 0;
            }
            return ++Odjeljenje;
        }
    }
}
