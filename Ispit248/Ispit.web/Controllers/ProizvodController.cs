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
    public class ProizvodController : Controller
    {
        private MyContext _myContext;
        public ProizvodController(MyContext myContext)
        {
            _myContext = myContext;
        }
        public IActionResult Index(int FakturaId)
        {
            PIndexVM pIndexVM = new PIndexVM();
            pIndexVM.FakturaId = FakturaId;
            pIndexVM.Proizvodi = new List<PIndexVM.Row>();

            List<FakturaStavka> fs = _myContext.FakturaStavka.Include(x=>x.Proizvod).Where(x => x.FakturaID == FakturaId).ToList();

            foreach (var item in fs)
            {
                PIndexVM.Row row = new PIndexVM.Row();
                row.StavkaId = item.Id;
                row.Proizvod = item.Proizvod.Naziv;
                row.Cijena = item.Proizvod.Cijena;
                row.Kolicina = (int)item.Kolicina;
                row.Popust = item.PopustProcenat;
                row.Iznos = row.Kolicina * row.Cijena * (1 - row.Popust);
                pIndexVM.Proizvodi.Add(row);
            }
            return PartialView("Index", pIndexVM);
        }
        public IActionResult Uredi(int StavkaId)
        {
            PUrediVM pUrediVM = new PUrediVM();
            pUrediVM.StavkaId = StavkaId;
            PonudaStavka ps = _myContext.PonudaStavka.Include(x=>x.Proizvod).Where(x => x.Id == StavkaId).FirstOrDefault();
            pUrediVM.Proizvod = ps.Proizvod.Naziv;
            return PartialView("Uredi", pUrediVM);
        }
        public IActionResult SnimiPromjene(int StavkaId, int Kolicina)
        {
            FakturaStavka fs = _myContext.FakturaStavka.Where(x => x.Id == StavkaId).FirstOrDefault();
            fs.Kolicina = Kolicina;
            _myContext.SaveChanges();
            return Redirect("/Proizvod/Index?FakturaId=" + fs.FakturaID);
        }
        public IActionResult Obrisi(int StavkaId)
        {
            FakturaStavka fs = _myContext.FakturaStavka.Where(x => x.Id == StavkaId).FirstOrDefault();
            int id = fs.FakturaID;
            _myContext.FakturaStavka.Remove(fs);
            _myContext.SaveChanges();
            return Redirect("/Proizvod/Index?FakturaId=" + id);
        }
        public IActionResult Dodaj(int FakturaId)
        {
            PDodajVM pDodajVM = new PDodajVM();
            pDodajVM.FakturaId = FakturaId;
            pDodajVM.Proizvodi = new List<SelectListItem>();

            List<Proizvod> p = _myContext.Proizvod.ToList();

            foreach (var item in p)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.Naziv;
                pDodajVM.Proizvodi.Add(newSLI);
            }

            return PartialView("Dodaj", pDodajVM);
        }
        public IActionResult SnimiDodavanje(int FakturaId, int ProizvodId, int Kolicina)
        {
            List<FakturaStavka> list = _myContext.FakturaStavka.Where(x => x.FakturaID == FakturaId).ToList();

            foreach (var item in list)
            {
                if(item.ProizvodID == ProizvodId)
                {
                    item.Kolicina += Kolicina;
                     _myContext.SaveChanges();
                    return Redirect("/Proizvod/Index?FakturaId=" + FakturaId);
                }
            }

            FakturaStavka fs = new FakturaStavka();
            fs.FakturaID = FakturaId;
            fs.ProizvodID = ProizvodId;
            fs.Kolicina = Kolicina;
            fs.PopustProcenat = 0;
            _myContext.FakturaStavka.Add(fs);
            return Redirect("/Proizvod/Index?FakturaId=" + FakturaId);
        }
    }
}