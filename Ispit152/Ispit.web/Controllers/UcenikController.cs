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
        public IActionResult Index(int OdrzaniCasId)
        {
            UIndexVM uIndexVM = new UIndexVM();
            uIndexVM.OdrzaniCasId = OdrzaniCasId;
            uIndexVM.Ucenici = new List<UIndexVM.Row>();
            List<OdrzaniCasDetalji> ocdList = _myContext.OdrzaniCasDetalji.Include(x => x.SlusaPredmet).Include(x => x.SlusaPredmet.UpisGodine).Include(x => x.SlusaPredmet.UpisGodine.Student).Where(x => x.OdrzaniCasId == OdrzaniCasId).ToList();
            foreach (var item in ocdList)
            {
                UIndexVM.Row row = new UIndexVM.Row();
                row.OdrzaniCasDetaljiId = item.Id;
                row.Ucenik = item.SlusaPredmet.UpisGodine.Student.Ime + " " + item.SlusaPredmet.UpisGodine.Student.Prezime;
                row.Bodovi = item.BodoviNaCasu;
                if (item.Prisutan)
                {
                    row.Prisutan = "Prisutan";
                }
                else
                {
                    row.Prisutan = "Odsutan";
                }
                uIndexVM.Ucenici.Add(row);
            }
            return PartialView("Index", uIndexVM);
        }
        public IActionResult Prisustvo(int OdrzaniCasDetaljiId)
        {
            OdrzaniCasDetalji ocd = _myContext.OdrzaniCasDetalji.Where(x => x.Id == OdrzaniCasDetaljiId).FirstOrDefault();
            if (ocd.Prisutan == true)
            {
                ocd.Prisutan = false;
            }
            else
            {
                ocd.Prisutan = true;
            }
            _myContext.SaveChanges();
            return Redirect("/Ucenik/Index?OdrzaniCasId=" + ocd.OdrzaniCasId);
        }
        public IActionResult Uredi(int OdrzaniCasDetaljiId)
        {
            OdrzaniCasDetalji ocd = _myContext.OdrzaniCasDetalji.Include(x => x.SlusaPredmet).Include(x => x.SlusaPredmet.UpisGodine).Include(x => x.SlusaPredmet.UpisGodine.Student).Where(x => x.Id == OdrzaniCasDetaljiId).FirstOrDefault();
            UUrediVM uUrediVM = new UUrediVM();
            uUrediVM.OdrzaniCasDetaljiId = OdrzaniCasDetaljiId;
            uUrediVM.Ucenik = ocd.SlusaPredmet.UpisGodine.Student.Ime + " " + ocd.SlusaPredmet.UpisGodine.Student.Prezime;
            uUrediVM.Bodovi = ocd.BodoviNaCasu;
            return PartialView("Uredi", uUrediVM);
        }
        public IActionResult SnimiUredjivanje(int OdrzaniCasDetaljiId, int Bodovi)
        {
            OdrzaniCasDetalji ocd = _myContext.OdrzaniCasDetalji.Where(x => x.Id == OdrzaniCasDetaljiId).FirstOrDefault();
            ocd.BodoviNaCasu = Bodovi;
            _myContext.SaveChanges();
            return Redirect("/Ucenik/Index?OdrzaniCasId=" + ocd.OdrzaniCasId);
        }
    }
}