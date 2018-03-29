using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ispit.data;
using Ispit.web.ViewModels;
using Ispit.data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ispit.web.Controllers
{
    public class OdjeljenjeController : Controller
    {
        private MyContext _myContext;
        public OdjeljenjeController(MyContext myContext)
        {
            _myContext = myContext;
        }
        public void Inc()
        {
            MojDBInitializer.Izvrsi(_myContext);
        }
        public IActionResult Index()
        {
            OIndexVM oIndexVM = new OIndexVM();
            oIndexVM.Odjeljenja = new List<OIndexVM.Row>();
            List<Odjeljenje> o = _myContext.Odjeljenje.Include(x => x.Nastavnik).ToList();
            foreach (var item in o)
            {
                OIndexVM.Row row = new OIndexVM.Row();
                row.OdjeljenjeId = item.Id;
                row.SkolskaGodina = item.SkolskaGodina;
                row.Razred = item.Razred;
                row.Oznaka = item.Oznaka;
                if(item.IsPrebacenuViseOdjeljenje == true)
                {
                    row.PrebacenUViseOdjeljenje = "Da";
                }
                else
                {
                    row.PrebacenUViseOdjeljenje = "Ne";
                }
                row.Razrednik = item.Nastavnik.ImePrezime;
                oIndexVM.Odjeljenja.Add(row);
            }
            return View("Index", oIndexVM);
        }
        public IActionResult Dodaj()
        {
            ODodajVM oDodajVM = new ODodajVM();
            oDodajVM.Nastavnici = new List<SelectListItem>();
            oDodajVM.NizaOdjeljenja= new List<SelectListItem>();

            List<Nastavnik> n = _myContext.Nastavnik.ToList();
            List<Odjeljenje> o = _myContext.Odjeljenje.Where(x => x.IsPrebacenuViseOdjeljenje == false).ToList();

            foreach (var item in n)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.NastavnikID.ToString();
                newSLI.Text = item.ImePrezime;
                oDodajVM.Nastavnici.Add(newSLI);
            }

            foreach (var item in o)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.Razred + "-" + item.Oznaka;
                oDodajVM.NizaOdjeljenja.Add(newSLI);
            }

            return View("Dodaj", oDodajVM);
        }
        public IActionResult SnimiDodavanje(string SkolskaGodina, int Razred, string Oznaka, int RazrednikId, int OdjeljenjeId)
        {
            Odjeljenje o = new Odjeljenje();
            o.SkolskaGodina = SkolskaGodina;
            o.Razred = Razred;
            o.Oznaka = Oznaka;
            o.NastavnikID = RazrednikId;
            _myContext.Odjeljenje.Add(o);
            _myContext.SaveChanges();

            if (OdjeljenjeId != 0) {
                Odjeljenje staro = _myContext.Odjeljenje.Where(x => x.Id == OdjeljenjeId).FirstOrDefault();
                staro.IsPrebacenuViseOdjeljenje = true;
                List<OdjeljenjeStavka> os = _myContext.OdjeljenjeStavka.Where(x => x.OdjeljenjeId == OdjeljenjeId).ToList();
                foreach (var item in os)
                {
                    if (_myContext.DodjeljenPredmet.Where(x => x.OdjeljenjeStavkaId == item.Id).Count(x => x.ZakljucnoKrajGodine != 0) >= 1)
                    {
                        OdjeljenjeStavka newOS = new OdjeljenjeStavka();
                        newOS.OdjeljenjeId = o.Id;
                        newOS.UcenikId = item.UcenikId;
                        newOS.BrojUDnevniku = 0;
                        _myContext.OdjeljenjeStavka.Add(newOS);
                    }
                }
                _myContext.SaveChanges();
            }
            return Redirect("/Odjeljenje/Index");
        }
        public IActionResult Izbrisi(int OdjeljenjeId)
        {
            Odjeljenje o = _myContext.Odjeljenje.Where(x => x.Id == OdjeljenjeId).FirstOrDefault();
            _myContext.Odjeljenje.Remove(o);
            _myContext.SaveChanges();
            return Redirect("/Odjeljenje/Index");
        }
        public IActionResult Detalji(int OdjeljenjeId)
        {
            ODetaljiVM oDetaljiVM = new ODetaljiVM();
            oDetaljiVM.OdjeljenjeId = OdjeljenjeId;

            Odjeljenje o = _myContext.Odjeljenje.Include(x => x.Nastavnik).Where(x => x.Id == OdjeljenjeId).FirstOrDefault();

            oDetaljiVM.SkolskaGodina = o.SkolskaGodina;
            oDetaljiVM.Razred = o.Razred;
            oDetaljiVM.Oznaka = o.Oznaka;
            oDetaljiVM.Razrednik = o.Nastavnik.ImePrezime;
            oDetaljiVM.BrojPredmeta = _myContext.Predmet.Count(x => x.Razred == o.Razred);
            
            return View("Detalji", oDetaljiVM);
        }
    }
}