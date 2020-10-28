using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DurbanJuly.Domain;
using DurbanJuly.Domain.Services;
using DurbanJuly.Models;
using DurbanJuly.Mvc.Ui.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DurbanJuly.Mvc.Ui.Controllers
{
    public class TournamentController : BaseController
    {

        private readonly TournamentService _tournamentService;
        private readonly IMapper _mapper;
        public TournamentController(TournamentService tournamentService, IMapper mapper)
        {
            _tournamentService = tournamentService;
            _mapper = mapper;
        }
        // GET: TournamentController
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        // POST: TournamentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TournamentModel model)
        {
            if (ModelState.IsValid)
            {
                await _tournamentService.InsertAsync(_mapper.Map<Tournament>(model));
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // GET: TournamentController/Edit/5
        public ActionResult Edit(int id)
        {
            var tournament = _tournamentService.Get().FirstOrDefault(x => x.Id == id);
            var model = _mapper.Map<TournamentModel>(tournament);
            return View(model);
        }

        // POST: TournamentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TournamentModel model)
        {

            if (ModelState.IsValid)
            {
                await _tournamentService.UpdateTournamentAsync(model);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // GET: TournamentController/Delete/5
        public ActionResult Delete(long id)
        {
            var tournament = _tournamentService.Get().FirstOrDefault(x => x.Id == id);
            var model = _mapper.Map<TournamentModel>(tournament);
            return View(model);
        }

        // POST: TournamentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(TournamentModel model)
        {
                await _tournamentService.DeleteTournament(model.Id);
                return RedirectToAction("Index", "Home");
        }
    }
}
