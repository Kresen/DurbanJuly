using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DurbanJuly.Domain.Services;
using DurbanJuly.Mvc.Ui.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DurbanJuly.Mvc.Ui.Controllers
{
  
    public class HomeController : BaseController
    {
        private readonly TournamentService _tournamentService;
        public HomeController(TournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }
       
        public IActionResult Index()
        {
            return View(_tournamentService.GetAllTournaments());
        }
    }
}
