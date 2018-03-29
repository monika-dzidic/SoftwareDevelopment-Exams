using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ispit.data;
using Ispit.data.Models;
using Ispit.web.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ispit.web.Controllers
{
    public class OdrzaniCasController : Controller
    {
        private MyContext _myContext;
        public OdrzaniCasController(MyContext myContext)
        {
            _myContext = myContext;
        }
        public void Inc()
        {
            MojDBInitializer.Izvrsi(_myContext);
        }
        public IActionResult Index(int NastavnikId)
        {
            OCIndexVM oCIndexVM = new OCIndexVM();
            oCIndexVM.NastavnikId = NastavnikId;
            oCIndexVM.OdrzaniCasovi = new List<OCIndexVM.Row>();

            List<OdrzaniCas> ocList = _myContext.OdrzaniCasovi.Include(x => x.Angazovan).Include(x => x.Angazovan.Predmet).Include(x => x.Angazovan.AkademskaGodina).Where(x => x.Angazovan.NastavnikId == NastavnikId).ToList();

            foreach (var item in ocList)
            {
                OCIndexVM.Row row = new OCIndexVM.Row();
                row.OdrzaniCasId = item.Id;
                row.Datum = item.Datum;
                row.AkademskaGodina = item.Angazovan.AkademskaGodina.Opis;
                row.Predmet = item.Angazovan.Predmet.Naziv;
                row.BrojPrisutnih = _myContext.OdrzaniCasDetalji.Where(x => x.OdrzaniCasId == item.Id).Count(x => x.Prisutan == true);
                row.BrojURazredu = _myContext.SlusaPredmet.Where(x => x.Angazovan.PredmetId == item.Angazovan.PredmetId).Count();
                row.ProsjecnaOcjena = _myContext.SlusaPredmet.Where(x => x.Angazovan.PredmetId == item.Angazovan.PredmetId).Average(x => x.Ocjena)??0;
                oCIndexVM.OdrzaniCasovi.Add(row);
            }

            return View("Index", oCIndexVM);
        }
        public IActionResult Dodaj(int NastavnikId)
        {
            OCDodajVM oCDodajVM = new OCDodajVM();
            oCDodajVM.NastavnikId = NastavnikId;
            oCDodajVM.AkademskeGodine = new List<SelectListItem>();
            oCDodajVM.Predmeti = new List<SelectListItem>();

            List<AkademskaGodina> ag = _myContext.AkademskaGodina.ToList();
            List<Predmet> p = _myContext.Predmet.ToList();
            Nastavnik n = _myContext.Nastavnik.Where(x => x.Id == NastavnikId).FirstOrDefault();

            oCDodajVM.Nastavnik = n.Ime + " " + n.Prezime;

            foreach (var item in ag)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.Opis;
                oCDodajVM.AkademskeGodine.Add(newSLI);
            }

            foreach (var item in p)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.Naziv;
                oCDodajVM.Predmeti.Add(newSLI);
            }

            return View("Dodaj", oCDodajVM);
        }
        public IActionResult Snimi (int NastavnikId, int AkademskaGodinaId, int PredmetId, DateTime Datum)
        {
            Angazovan a = new Angazovan();
            a.NastavnikId = NastavnikId;
            a.PredmetId = PredmetId;
            a.AkademskaGodinaId = AkademskaGodinaId;
            _myContext.Angazovan.Add(a);

            OdrzaniCas oc = new OdrzaniCas();
            oc.AngazovanId = a.Id;
            oc.Datum = Datum;
            _myContext.OdrzaniCasovi.Add(oc);

            List<SlusaPredmet> spList = _myContext.SlusaPredmet.Include(x => x.Angazovan).ToList();

            foreach (var item in spList)
            {
                if(item.Angazovan.PredmetId == PredmetId)
                {
                    OdrzaniCasDetalji ocd = new OdrzaniCasDetalji();
                    ocd.OdrzaniCasId = oc.Id;
                    ocd.SlusaPredmetId = item.Id;
                    ocd.Prisutan = true;
                    ocd.BodoviNaCasu = 0;
                    _myContext.OdrzaniCasDetalji.Add(ocd);
                }
            }
            _myContext.SaveChanges();
            return Redirect("/OdrzaniCas/Index?NastavnikId=" + NastavnikId);
        }
        public IActionResult Uredi(int OdrzaniCasId)
        {
            OCUrediVM oCUrediVM = new OCUrediVM();
            OdrzaniCas oc = _myContext.OdrzaniCasovi.Include(x => x.Angazovan).Include(x => x.Angazovan.Predmet).Include(x => x.Angazovan.AkademskaGodina).Include(x => x.Angazovan.Nastavnik).Where(x => x.Id == OdrzaniCasId).FirstOrDefault();
            oCUrediVM.OdrzaniCasId = OdrzaniCasId;
            oCUrediVM.Nastavnik = oc.Angazovan.Nastavnik.Ime + " " + oc.Angazovan.Nastavnik.Prezime;
            oCUrediVM.Datum = oc.Datum;
            oCUrediVM.AkademskaGodina = oc.Angazovan.AkademskaGodina.Opis;
            oCUrediVM.Predmet = oc.Angazovan.Predmet.Naziv;
            return View("Uredi", oCUrediVM);
        }
        public void SnimiUredjivanje(int OdrzaniCasId, DateTime Datum)
        {
            OdrzaniCas oc = _myContext.OdrzaniCasovi.Where(x => x.Id == OdrzaniCasId).FirstOrDefault();
            oc.Datum = Datum;
            _myContext.SaveChanges();
        }
    }
}