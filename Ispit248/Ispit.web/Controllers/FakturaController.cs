using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ispit.data;
using Ispit.web.ViewModels;
using Ispit.data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ispit.web.Controllers
{
    public class FakturaController : Controller
    {
        private MyContext _myContext;
        public FakturaController(MyContext myContext)
        {
            _myContext = myContext;
        }
        public void Inc()
        {
            MojDBInitializer.Izvrsi(_myContext);
        }
        public IActionResult Index()
        {
            FIndexVM fIndexVM = new FIndexVM();
            fIndexVM.Fakture = new List<FIndexVM.Row>();

            List<Faktura> fakture = _myContext.Faktura.Include(x=>x.Klijent).ToList();
            foreach (var item in fakture)
            {
                FIndexVM.Row row = new FIndexVM.Row();
                row.FakutraId = item.Id;
                row.Datum = item.Datum;
                row.Klijent = item.Klijent.ImePrezime;
                fIndexVM.Fakture.Add(row);
            }
            return View("Index", fIndexVM);
        }
        public IActionResult Dodaj()
        {
            FDodajVM fDodajVM = new FDodajVM();
            fDodajVM.Klijenti = new List<SelectListItem>();
            fDodajVM.PonudeBezFakture = new List<SelectListItem>();

            List<Klijent> k = _myContext.Klijent.ToList();
            List<Ponuda> p = _myContext.Ponuda.Where(x => x.FakturaID == null).Include(x=>x.Klijent).ToList();

            foreach (var item in k)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.ImePrezime;
                fDodajVM.Klijenti.Add(newSLI);
            }

            foreach (var item in p)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.Klijent.ImePrezime + " " + item.Datum;
                fDodajVM.PonudeBezFakture.Add(newSLI);
            }

            return View("Dodaj", fDodajVM);
        }
        public IActionResult SnimiDodavanjeFakture(int KlijentId, int? PonudaId, DateTime Datum)
        {
            Faktura f = new Faktura();
            f.KlijentID = KlijentId;
            f.Datum = Datum;
            _myContext.Faktura.Add(f);

            if(PonudaId != null)
            {
                Ponuda p = _myContext.Ponuda.Where(x => x.Id == PonudaId).FirstOrDefault();
                p.FakturaID = f.Id;
                List<PonudaStavka> ps = _myContext.PonudaStavka.Where(x=>x.PonudaId == PonudaId).ToList();
                foreach (var item in ps)
                {
                    FakturaStavka fs = new FakturaStavka();
                    fs.FakturaID = f.Id;
                    fs.ProizvodID = item.ProizvodId;
                    fs.Kolicina = item.Kolicina;
                    fs.PopustProcenat = (float)0.05;
                    _myContext.FakturaStavka.Add(fs);
                }
            }
            _myContext.SaveChanges();
            return Redirect("Index");
        }
        public IActionResult Detalji(int FakturaId)
        {
            FDetaljiVM fDetaljiVM = new FDetaljiVM();
            Faktura f = _myContext.Faktura.Include(x=>x.Klijent).Where(x => x.Id == FakturaId).FirstOrDefault();
            fDetaljiVM.FakutraId = FakturaId;
            fDetaljiVM.Klijent = f.Klijent.ImePrezime;
            fDetaljiVM.Datum = f.Datum;
            return View("Detalji", fDetaljiVM);
        }
        public void Obrisi (int FakturaId)
        {
            Faktura f = _myContext.Faktura.Where(x => x.Id == FakturaId).FirstOrDefault();
            Ponuda p = _myContext.Ponuda.Where(x => x.FakturaID == FakturaId).FirstOrDefault();
            p.FakturaID = null;
            _myContext.Faktura.Remove(f);
            _myContext.SaveChanges();
        }
    }
}