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
    public class EventController : BaseController
    {

        private readonly EventService _eventService;
        private readonly IMapper _mapper;
        public EventController(EventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }
        // GET: EventController
        public ActionResult Index(long id)
        {
            var model = _eventService.GetEvents(id);
            ViewBag.Id = id;
            return View(model);
        }

        // GET: EventController/Create
        public ActionResult Create(int id)
        {
            
            var model = new EventModel
            {
                TournamentId = id
            };

            return View(model);
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EventModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Event>(model);
                result.Id = 0;
                result.Tournament = null;
                await _eventService.InsertAsync(result);
                return RedirectToAction("Index", new { id = model.TournamentId});
            }
            return View(model);
        }

        // GET: EventController/Edit/5
        public ActionResult Edit(int id)
        {
            var eventD = _eventService.Get().FirstOrDefault(x => x.Id == id);
            var model = _mapper.Map<EventModel>(eventD);
            return View(model);
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EventModel model)
        {
            if (ModelState.IsValid)
            {
                await _eventService.UpdateEventAsync(model);
                return RedirectToAction("Index", new { id = model.TournamentId });
            }

            return View();
        }

        // GET: EventController/Delete/5
        public ActionResult Delete(int id)
        {
            var tournament = _eventService.Get().FirstOrDefault(x => x.Id == id);
            var model = _mapper.Map<EventModel>(tournament);
            return View(model);
        }

        // POST: EventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(EventModel model)
        {
            await _eventService.DeleteEvent(model.Id);
            return RedirectToAction("Index", new { id = model.TournamentId });
        }
    }
}
