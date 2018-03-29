using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ispit.data;
using Ispit.web.ViewModels;
using Ispit.data.Models;
using Microsoft.EntityFrameworkCore;

namespace Ispit.web.Controllers
{
    public class UcenikController : Controller
    {
        private MyContext _myContext;
        public UcenikController(MyContext myContext)
        {
            _myContext = myContext;
        }
        public IActionResult Index(int MaturskiIspitId)
        {
            UIndexVM uIndexVM = new UIndexVM();
            uIndexVM.MaturskiIspitId = MaturskiIspitId;
            uIndexVM.Ucenici = new List<UIndexVM.Row>();
            List<MaturskiIspitStavka> mis = _myContext.MaturskiIspitiStavke.Include(x=>x.UpisUOdjeljenje).Include(x => x.UpisUOdjeljenje.Ucenik).Where(x => x.MaturskiIspitId == MaturskiIspitId).ToList();
            foreach (var item in mis)
            {
                UIndexVM.Row row = new UIndexVM.Row();
                row.MaturskiIspitStavkaId = item.Id;
                row.Ucenik = item.UpisUOdjeljenje.Ucenik.ImePrezime;
                row.OpciUspjeh = item.UpisUOdjeljenje.OpciUspjeh;
                if(item.Bodovi == null)
                {
                    row.Bodovi = "(oslobodjen)";
                }
                else
                {
                    row.Bodovi = item.Bodovi.ToString();
                }
                if (item.Oslobodjen == true)
                {
                    row.Oslobodjen = "Da";
                }
                else
                {
                    row.Oslobodjen = "Ne";
                }
                uIndexVM.Ucenici.Add(row);
            }
            return PartialView("Index", uIndexVM);
        }
        public IActionResult Uredi(int MaturskiIspitStavkaId)
        {
            MaturskiIspitStavka mis = _myContext.MaturskiIspitiStavke.Include(x => x.UpisUOdjeljenje).Include(x => x.UpisUOdjeljenje.Ucenik).Where(x => x.Id == MaturskiIspitStavkaId).FirstOrDefault();
            UUrediVM uurediVM = new UUrediVM();
            uurediVM.MaturskiIspitStavkaId = MaturskiIspitStavkaId;
            uurediVM.Ucenik = mis.UpisUOdjeljenje.Ucenik.ImePrezime;
            uurediVM.Bodovi = mis.Bodovi??0;
            return PartialView("Uredi", uurediVM);
        }
        public IActionResult SnimiDodavanje(int MaturskiIspitStavkaId, int Bodovi)
        {
            MaturskiIspitStavka mis = _myContext.MaturskiIspitiStavke.Where(x => x.Id == MaturskiIspitStavkaId).FirstOrDefault();
            mis.Bodovi = Bodovi;
            _myContext.SaveChanges();
            return Redirect("/Ucenik/Index?MaturskiIspitId=" + mis.MaturskiIspitId);
        }
        public IActionResult PromjeniBodove(int MaturskiIspitStavkaId, int Bodovi)
        {
            MaturskiIspitStavka mis = _myContext.MaturskiIspitiStavke.Where(x => x.Id == MaturskiIspitStavkaId).FirstOrDefault();
            mis.Bodovi = Bodovi;
            _myContext.SaveChanges();
            return Redirect("/Ucenik/Index?MaturskiIspitId=" + mis.MaturskiIspitId);
        }
    }
}