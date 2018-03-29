using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ispit.data;
using Ispit.data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ispit.web.ViewModels;

namespace Ispit.web.Controllers
{
    public class MaturskiIspitController : Controller
    {
        private MyContext _myContext;
        public MaturskiIspitController(MyContext myContext)
        {
            _myContext = myContext;
        }
        public void Inc()
        {
            MojDBInitializer.Seed(_myContext);
        }
        public IActionResult Index()
        {
            IIndexVM iindexVM = new IIndexVM();
            iindexVM.MaturskiIspiti = new List<IIndexVM.Row>();
            List<MaturskiIspit> mi = _myContext.MaturskiIspiti.Include(x => x.Ispitivac).Include(x => x.Odjeljenje).ToList();
            foreach (var item in mi)
            {
                IIndexVM.Row row = new IIndexVM.Row();
                row.IspitId = item.Id;
                row.Datum = item.Datum;
                row.Odjeljenje = item.Odjeljenje.Naziv;
                row.Ispitivac = item.Ispitivac.ImePrezime;
                row.ProsjecniBodovi = _myContext.MaturskiIspitiStavke.Where(x => x.MaturskiIspitId == item.Id).Average(x => x.Bodovi) ?? 0;
                iindexVM.MaturskiIspiti.Add(row);
            }
            return View("Index", iindexVM);
        }
        public IActionResult Dodaj()
        {
            IDodajVM idodajVM = new IDodajVM();
            idodajVM.Nastavnici = new List<SelectListItem>();
            idodajVM.Odjeljenja = new List<SelectListItem>();

            List<Nastavnik> n = _myContext.Nastavnik.ToList();
            List<Odjeljenje> o = _myContext.Odjeljenje.ToList();

            foreach (var item in n)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.ImePrezime;
                idodajVM.Nastavnici.Add(newSLI);
            }

            foreach (var item in o)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.Naziv;
                idodajVM.Odjeljenja.Add(newSLI);
            }
            return View("Dodaj", idodajVM);
        }
        public IActionResult SnimiDodavanje(int NastavnikId, int OdjeljenjeId, DateTime Datum)
        {
            MaturskiIspit mi = new MaturskiIspit();
            mi.IspitivacId = NastavnikId;
            mi.OdjeljenjeId = OdjeljenjeId;
            mi.Datum = Datum;
            _myContext.MaturskiIspiti.Add(mi);

            List<UpisUOdjeljenje> ucenici = _myContext.UpisUOdjeljenje.Where(x => x.OdjeljenjeId == OdjeljenjeId).ToList();

            foreach (var item in ucenici)
            {
                if (item.OpciUspjeh > 1)
                {
                    MaturskiIspitStavka mis = new MaturskiIspitStavka();
                    mis.MaturskiIspitId = mi.Id;
                    mis.UpisUOdjeljenjeId = item.Id;
                    if(item.OpciUspjeh == 5)
                    {
                        mis.Oslobodjen = true;
                        mis.Bodovi = null;
                    }
                    else
                    {
                        mis.Oslobodjen = false;
                        mis.Bodovi = 0;
                    }
                    _myContext.MaturskiIspitiStavke.Add(mis);
                }
            }
            _myContext.SaveChanges();
            return Redirect("/MaturskiIspit/Index");
        }
        public IActionResult Detalji(int MaturskiIspitId)
        {
            IDetaljiVM idetaljiVM = new IDetaljiVM();
            idetaljiVM.MaturskiIspitId = MaturskiIspitId;
            MaturskiIspit mi = _myContext.MaturskiIspiti.Include(x => x.Ispitivac).Include(x => x.Odjeljenje).Where(x => x.Id == MaturskiIspitId).FirstOrDefault();
            idetaljiVM.Ispitivac = mi.Ispitivac.ImePrezime;
            idetaljiVM.Datum = mi.Datum;
            idetaljiVM.Odjeljenje = mi.Odjeljenje.Naziv;
            return View("Detalji", idetaljiVM);
        }
    }
}