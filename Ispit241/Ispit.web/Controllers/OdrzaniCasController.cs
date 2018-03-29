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
    public class OdrzaniCasController : Controller
    {
        private MyContext _myContext;
        public OdrzaniCasController(MyContext myContext)
        {
            _myContext = myContext;
        }
        public IActionResult Index(int NastavnikId)
        {
            OCIndexVM oCIndexVM = new OCIndexVM();
            oCIndexVM.NastavnikId = NastavnikId;
            oCIndexVM.OdrzaniCasovi = new List<OCIndexVM.Row>();
            List<Angazovan> a = _myContext.Angazovan.Include(x=>x.Predmet).Include(x => x.Odjeljenje).Where(x=>x.NastavnikId == NastavnikId).ToList();
            
            foreach (var item in a)
            {
                OCIndexVM.Row row = new OCIndexVM.Row();
                OdrzaniCas oc = _myContext.OdrzaniCas.Where(x => x.AngazovanId == item.Id).FirstOrDefault();
                row.OdrzaniCasId = oc.Id;
                row.Datum = oc.Datum;
                row.Predmet = item.Predmet.Naziv;
                row.Odjeljenje = item.Odjeljenje.Razred + " " + item.Odjeljenje.Oznaka;
                row.UkupnoUcenika = _myContext.UpisUOdjeljenje.Where(x => x.OdjeljenjeId == item.OdjeljenjeId).Count();
                row.PrisutnihUcenika = _myContext.OdrzaniCasDetalj.Where(x => x.UpisUOdjeljenje.OdjeljenjeId == item.OdjeljenjeId).Count(x=>x.Odsutan == false);
                row.NajboljiUcenik = "M.DZ.";
                oCIndexVM.OdrzaniCasovi.Add(row);
            }
            return View(oCIndexVM);
        }
        public IActionResult Dodaj(int NastavnikId)
        {
            OCDodajVM oCDodajVM = new OCDodajVM();
            Nastavnik n = _myContext.Nastavnik.Where(x => x.Id == NastavnikId).FirstOrDefault();
            oCDodajVM.NastavnikId = n.Id;
            oCDodajVM.Nastavnik = n.Ime;

            oCDodajVM.Predmeti = new List<SelectListItem>();
            oCDodajVM.Odjeljenja = new List<SelectListItem>();

            List<Predmet> p = _myContext.Predmet.ToList();
            List<Odjeljenje> o = _myContext.Odjeljenje.ToList();

                       foreach (var item in p)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.Naziv;
                oCDodajVM.Predmeti.Add(newSLI);
            }

            foreach (var item in o)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.Razred + "-" + item.Oznaka;
                oCDodajVM.Odjeljenja.Add(newSLI);
            }

            return View(oCDodajVM);
        }
        [HttpPost]
        public IActionResult Dodaj(int NastavnikId, int OdjeljenjeId, int PredmetId, DateTime Datum)
        {
            Angazovan a = new Angazovan();
            a.NastavnikId = NastavnikId;
            a.OdjeljenjeId = OdjeljenjeId;
            a.PredmetId = PredmetId;
            _myContext.Angazovan.Add(a);

            OdrzaniCas oc = new OdrzaniCas();
            oc.AngazovanId = a.Id;
            oc.Datum = Datum;
            _myContext.OdrzaniCas.Add(oc);

            List<UpisUOdjeljenje> ucenici = _myContext.UpisUOdjeljenje.Where(x => x.OdjeljenjeId == OdjeljenjeId).ToList();

            foreach (var item in ucenici)
            {
                OdrzaniCasDetalj ocd = new OdrzaniCasDetalj();
                ocd.OdrzaniCasId = oc.Id;
                ocd.UpisUOdjeljenjeId = item.Id;
                _myContext.OdrzaniCasDetalj.Add(ocd);
            }
            _myContext.SaveChanges();
            return Redirect("/OdrzaniCas/Index?NastavnikId=" + NastavnikId);
        }
        public IActionResult Detalji(int OdrzaniCasId)
        {
            OCDetaljiVM oCDetaljiVM = new OCDetaljiVM();
            oCDetaljiVM.OdrzaniCasId = OdrzaniCasId;
            OdrzaniCas oc = _myContext.OdrzaniCas.Include(x=>x.Angazovan).Include(x => x.Angazovan.Predmet).Include(x => x.Angazovan.Nastavnik).Include(x => x.Angazovan.Odjeljenje).Where(x => x.Id == OdrzaniCasId).FirstOrDefault();
            oCDetaljiVM.Nastavnik = oc.Angazovan.Nastavnik.Ime;
            oCDetaljiVM.Datum = oc.Datum;
            oCDetaljiVM.Predmet = oc.Angazovan.Predmet.Naziv;
            oCDetaljiVM.Odjeljenje = oc.Angazovan.Odjeljenje.Razred + "-"+ oc.Angazovan.Odjeljenje.Oznaka;
            return View(oCDetaljiVM);
        }
    }
}