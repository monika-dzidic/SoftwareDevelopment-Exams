using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ispit.data;
using Ispit.data.Models;
using Ispit.web.ViewModels;
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
            List<OdrzaniCasDetalj> ocdList = _myContext.OdrzaniCasDetalj.Include(x=>x.UpisUOdjeljenje).Include(x => x.UpisUOdjeljenje.Ucenik).Where(x => x.OdrzaniCasId == OdrzaniCasId).ToList();
            foreach (var item in ocdList)
            {
                UIndexVM.Row row = new UIndexVM.Row();
                row.OdrzaniCasDetaljiId = item.Id;
                row.Ucenik = item.UpisUOdjeljenje.Ucenik.Ime;
                row.Ocjena = item.Ocjena ?? 0;
                if(item.Odsutan == true)
                {
                    row.Prisutan = "Ne";
                }
                else
                {
                    row.Prisutan = "Da";
                }
                if(item.OpravdanoOdsutan == true)
                {
                    row.OpravdanoOdsutan = "Da";
                }
                else
                {
                    row.OpravdanoOdsutan = "Ne";
                }
                uIndexVM.Ucenici.Add(row);
            }
            return View(uIndexVM);
        }
        public IActionResult PromjeniPrisustvo(int OdrzaniCasDetaljiId)
        {
            OdrzaniCasDetalj ocd = _myContext.OdrzaniCasDetalj.Where(x => x.Id == OdrzaniCasDetaljiId).FirstOrDefault();
            if(ocd.Odsutan == true)
            {
                ocd.Odsutan = false;
            }
            else
            {
                ocd.Odsutan = true;
            }
            _myContext.SaveChanges();
            return Redirect("/Ucenik/Index?OdrzaniCasId=" + ocd.OdrzaniCasId);
        }
        public IActionResult Uredi (int OdrzaniCasDetaljiId)
        {
            UUrediVM uUrediVM = new UUrediVM();
            uUrediVM.OdrzaniCasDetaljiId = OdrzaniCasDetaljiId;

            OdrzaniCasDetalj ocd = _myContext.OdrzaniCasDetalj.Include(x => x.UpisUOdjeljenje).Include(x => x.UpisUOdjeljenje.Ucenik).Where(x => x.Id == OdrzaniCasDetaljiId).FirstOrDefault();

            uUrediVM.Ucenik = ocd.UpisUOdjeljenje.Ucenik.Ime;
            uUrediVM.Prisustvo = ocd.Odsutan;
            uUrediVM.Ocjena = ocd.Ocjena??0;
            if(ocd.OpravdanoOdsutan == true)
            {
                uUrediVM.OpravdanoOdsutan = "Da";
            }
            else
            {
                uUrediVM.OpravdanoOdsutan = "Ne";
            }
            return PartialView(uUrediVM);
        }
        [HttpPost]
        public IActionResult Uredi (int OdrzaniCasDetaljiId, int Ocjena, string OpravdanoOdsutan)
        {
            OdrzaniCasDetalj ocd = _myContext.OdrzaniCasDetalj.Where(x => x.Id == OdrzaniCasDetaljiId).FirstOrDefault();
            ocd.Ocjena = Ocjena;
            if(OpravdanoOdsutan == "Da")
            {
                ocd.OpravdanoOdsutan = false;
            }
            else
            {
                ocd.OpravdanoOdsutan = true;
            }
            _myContext.SaveChanges();
            return Redirect("/Ucenik/Index?OdrzaniCasId=" + ocd.OdrzaniCasId);
        }
    }
}