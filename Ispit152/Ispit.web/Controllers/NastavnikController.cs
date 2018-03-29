using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ispit.data;
using Ispit.web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ispit.data.Models;

namespace Ispit.web.Controllers
{
    public class NastavnikController : Controller
    {
        private MyContext _myContext;
        public NastavnikController(MyContext myContext)
        {
            _myContext = myContext;
        }
        public IActionResult Index()
        {
            NIndexVM nIndexVM = new NIndexVM();
            nIndexVM.Nastavnici = new List<SelectListItem>();
            List<Nastavnik> n = _myContext.Nastavnik.ToList();
            foreach (var item in n)
            {
                SelectListItem newSLI = new SelectListItem();
                newSLI.Value = item.Id.ToString();
                newSLI.Text = item.Ime + " " + item.Prezime;
                nIndexVM.Nastavnici.Add(newSLI);
            }
            return View("Index", nIndexVM);
        }
    }
}