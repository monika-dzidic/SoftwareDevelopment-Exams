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
    public class UputnicaController : Controller
    {
        private MyContext _myContext;
        public UputnicaController(MyContext myContext)
        {
            _myContext = myContext;
        }
        public void Inc()
        {
            MojDBInitializer.Izvrsi(_myContext);
        }
        public IActionResult Index()
        {
            UIndexVM uIndexVM = new UIndexVM();
            uIndexVM.Uputnice = new List<UIndexVM.Row>();
            List<Uputnica> u = _myContext.Uputnica.Include(x => x.UputioLjekar).Include(x => x.Pacijent).Include(x => x.VrstaPretrage).ToList();
            foreach (var item in u)
            {
                UIndexVM.Row row = new UIndexVM.Row();
                row.UputnicaId = item.Id;
                row.Datum = item.DatumUputnice;
                row.UputioLjekar = item.UputioLjekar.Ime;
                row.Pacijent = item.Pacijent.Ime;
                row.VrstaPretrage = item.VrstaPretrage.Naziv;
                row.DatumRezultata = item.DatumRezultata;
                uIndexVM.Uputnice.Add(row);
            }
            return View(uIndexVM);
        }
        public IActionResult Dodaj()
        {
            UDodajVM uDodajVM = new UDodajVM();
            uDodajVM.Ljekari = new List<SelectListItem>();
            uDodajVM.Pacijent = new List<SelectListItem>();
            uDodajVM.VrstaPretrage = new List<SelectListItem>();

            List<Ljekar> lj = _myContext.Ljekar.ToList();
            List<Pacijent> p = _myContext.Pacijent.ToList();
            List<VrstaPretrage> vp = _myContext.VrstaPretrage.ToList();

            foreach (var item in lj)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.Ime;
                uDodajVM.Ljekari.Add(newSLI);
            }

            foreach (var item in p)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.Ime;
                uDodajVM.Pacijent.Add(newSLI);
            }

            foreach (var item in vp)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.Naziv;
                uDodajVM.VrstaPretrage.Add(newSLI);
            }

            return View(uDodajVM);
        }
        [HttpPost]
        public IActionResult Dodaj(int LjekarUputioId, int PacijentId, int VrstaPretrageId, DateTime Datum)
        {
            Uputnica u = new Uputnica();
            u.UputioLjekarId = LjekarUputioId;
            u.PacijentId = PacijentId;
            u.VrstaPretrageId = VrstaPretrageId;
            u.LaboratorijLjekarId = null;
            u.IsGotovNalaz = false;
            _myContext.Uputnica.Add(u);

            List<LabPretraga> lp = _myContext.LabPretraga.Where(x => x.VrstaPretrageId == VrstaPretrageId).ToList();

            foreach (var item in lp)
            {
                RezultatPretrage rp = new RezultatPretrage();
                rp.LabPretragaId = item.Id;
                rp.UputnicaId = u.Id;
                rp.ModalitetId = null;
                rp.NumerickaVrijednost = null;
                _myContext.RezultatPretrage.Add(rp);
            }
            _myContext.SaveChanges();
            return Redirect("/Uputnica/Index");
        }
        public IActionResult Detalji(int UputnicaId)
        {
            UDetaljiVM uDetaljiVM = new UDetaljiVM();
            Uputnica u = _myContext.Uputnica.Include(x => x.Pacijent).Where(x => x.Id == UputnicaId).FirstOrDefault();
            uDetaljiVM.UputnicaId = UputnicaId;
            uDetaljiVM.Datum = u.DatumUputnice;
            uDetaljiVM.Pacijent = u.Pacijent.Ime;
            uDetaljiVM.DatumRezultata = u.DatumRezultata;
            return View(uDetaljiVM);
        }
    }
}