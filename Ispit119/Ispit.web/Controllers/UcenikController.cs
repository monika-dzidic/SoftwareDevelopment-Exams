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
    public class UcenikController : Controller
    {
        private MyContext _myContext;
        public UcenikController(MyContext myContext)
        {
            _myContext = myContext;
        }
        public IActionResult Index(int OdjeljenjeId)
        {
            UIndexVM uIndexVM = new UIndexVM();
            uIndexVM.OdjeljenjeId = OdjeljenjeId;
            uIndexVM.Ucenici = new List<UIndexVM.Row>();
            List<OdjeljenjeStavka> ucenici = _myContext.OdjeljenjeStavka.Include(x => x.Ucenik).Where(x => x.OdjeljenjeId == OdjeljenjeId).ToList();
            foreach (var item in ucenici)
            {
                UIndexVM.Row row = new UIndexVM.Row();
                row.OdjeljenjeStavkaId = item.Id;
                row.BrojUDnevniku = item.BrojUDnevniku;
                row.Ucenik = item.Ucenik.ImePrezime;
                row.BrojZakljucenihOcjena = _myContext.DodjeljenPredmet.Where(x => x.OdjeljenjeStavka.UcenikId == item.UcenikId).Count(x=>x.ZakljucnoKrajGodine != 0);
                uIndexVM.Ucenici.Add(row);
            }
            return View("Index", uIndexVM);
        }
        public IActionResult Dodaj(int OdjeljenjeId)
        {
            List<Ucenik> u = _myContext.Ucenik.ToList();
            UDodajVM uDodajVM = new UDodajVM();
            uDodajVM.OdjeljenjeId = OdjeljenjeId;
            uDodajVM.Ucenici = new List<SelectListItem>();
            foreach (var item in u)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.ImePrezime;
                uDodajVM.Ucenici.Add(newSLI);
            }
            return View("Dodaj", uDodajVM);
        }
        public IActionResult SnimiDodavanje(int OdjeljenjeId, int UcenikId, int BrojUDnevniku)
        {
            OdjeljenjeStavka os = new OdjeljenjeStavka();
            os.OdjeljenjeId = OdjeljenjeId;
            os.UcenikId = UcenikId;
            os.BrojUDnevniku = BrojUDnevniku;
            _myContext.OdjeljenjeStavka.Add(os);
            _myContext.SaveChanges();
            return Redirect("/Ucenik/Index?OdjeljenjeId=" + OdjeljenjeId);
        }
        public IActionResult Obrisi(int UpisUOdjeljenjeId)
        {
            OdjeljenjeStavka os = _myContext.OdjeljenjeStavka.Where(x => x.Id == UpisUOdjeljenjeId).FirstOrDefault();
            int OdjeljenjeId = os.OdjeljenjeId;
            _myContext.OdjeljenjeStavka.Remove(os);
            _myContext.SaveChanges();
            return Redirect("/Ucenik/Index?OdjeljenjeId=" + OdjeljenjeId);
        }
        public IActionResult RekonstruirajBrojeve(int OdjeljenjeId)
        {
            int b = 0;
            List<OdjeljenjeStavka> os = _myContext.OdjeljenjeStavka.Where(x => x.OdjeljenjeId == OdjeljenjeId).ToList();
            foreach (var item in os)
            {
                item.BrojUDnevniku = ++b;
            }
            _myContext.SaveChanges();
            return Redirect("/Ucenik/Index?OdjeljenjeId=" + OdjeljenjeId);
        }
    }
}