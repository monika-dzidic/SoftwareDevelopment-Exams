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
    public class RezultatController : Controller
    {
        private MyContext _myContext;
        public RezultatController(MyContext myContext)
        {
            _myContext = myContext;
        }
        public IActionResult Index(int UputnicaId)
        {
            RIndexVM rIndexVM = new RIndexVM();
            rIndexVM.UputnicaId = UputnicaId;
            rIndexVM.Rezultati = new List<RIndexVM.Row>();

            Uputnica u = _myContext.Uputnica.Where(x => x.Id == UputnicaId).FirstOrDefault();

            List<RezultatPretrage> rp = _myContext.RezultatPretrage.Where(x => x.UputnicaId == UputnicaId).ToList(); 

            foreach (var item in rp)
            {
                RIndexVM.Row row = new RIndexVM.Row();
                row.RezultatId = item.Id;
                LabPretraga lp = _myContext.LabPretraga.Where(x => x.Id == item.LabPretragaId).FirstOrDefault();
                row.Pretrage = lp.Naziv;
                if(item.NumerickaVrijednost == null)
                {
                    row.IsNumericka = false;
                    List<Modalitet> m = _myContext.Modalitet.Where(x => x.LabPretragaId == lp.Id).ToList();
                    row.Modaliteti = new List<SelectListItem>();
                    Modalitet m1 = _myContext.Modalitet.Where(x => x.Id == item.ModalitetId).FirstOrDefault();
                    SelectListItem modalitet = new SelectListItem();
                    modalitet.Value = m1.Id.ToString();
                    modalitet.Text = m1.Opis;
                    row.Modaliteti.Add(modalitet);
                    foreach (var mod in m)
                    {
                        if(m1.Id != mod.Id)
                        {
                            SelectListItem newSLI = new SelectListItem();
                            newSLI.Value = mod.Id.ToString();
                            newSLI.Text = mod.Opis;
                            row.Modaliteti.Add(newSLI);
                        }
                        row.ModalitetiNabrajanja += mod.Opis + ", ";
                    }
                    row.IzmjerenaVrijednost = null;
                    row.ReferentnaVrijednostMax = null;
                    row.ReferentnaVrijednostMin = null;
                }
                else
                {
                    row.IsNumericka = true;
                    row.IzmjerenaVrijednost = item.NumerickaVrijednost;
                    row.JMJ = lp.MjernaJedinica;
                    row.ReferentnaVrijednostMax = lp.ReferentnaVrijednostMax;
                    row.ReferentnaVrijednostMin = lp.ReferentnaVrijednostMin;
                }
                rIndexVM.Rezultati.Add(row);
            }
            return PartialView(rIndexVM);
        }
        public IActionResult PromjeniIzracunatuVrijednost(int RezultatId, double Vrijednost)
        {
            RezultatPretrage rp = _myContext.RezultatPretrage.Where(x => x.Id == RezultatId).FirstOrDefault();
            rp.NumerickaVrijednost = Vrijednost;
            _myContext.SaveChanges();
            return Redirect("/Rezultat/Index?UputnicaId=" + rp.UputnicaId);
        }
        public IActionResult PromjeniModalitet(int RezultatId, int ModalitetId)
        {
            RezultatPretrage rp = _myContext.RezultatPretrage.Where(x => x.Id == RezultatId).FirstOrDefault();
            rp.ModalitetId = ModalitetId;
            _myContext.SaveChanges();
            return Redirect("/Rezultat/Index?UputnicaId=" + rp.UputnicaId);
        }
        public bool IsRefVr(int RezultatId, int ModalitetId)
        {
            RezultatPretrage rp = _myContext.RezultatPretrage.Include(x=>x.Modalitet).Where(x => x.Id == RezultatId).FirstOrDefault();
            if(rp.Modalitet.IsReferentnaVrijednost == true)
            {
                return true;
            }
            return false;
        }
    }
}