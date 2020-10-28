using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DurbanJuly.Models;
using DurbanJuly.Mvc.Ui.Controllers.Base;

namespace DurbanJuly.Mvc.Ui.Controllers
{
    public class EventDetailController : BaseController
    {
        private readonly Domain.Services.EventDetailService _eventDetailService;
        private readonly IMapper _mapper;
        public EventDetailController(Domain.Services.EventDetailService eventDetailService, IMapper mapper)
        {
            _eventDetailService = eventDetailService;
            _mapper = mapper;
        }
        public ActionResult Index(long id)
        {
            var model = _eventDetailService.GetEventDetails(id);
            ViewBag.Id = id;
            return View(model);
        }

        // GET: EventDetailController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventDetailController/Create
        public ActionResult Create(int id)
        {
            var statuses = _eventDetailService.GetEventStatuses().Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            var model = new EventDetailModel
            {
                EventId = id,
                Statuses = statuses
            };

            return View(model);
        }


        // POST: EventDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventDetailModel model)
        {

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Domain.EventDetail>(model);
                _eventDetailService.SaveEventDetails(result);
                return RedirectToAction("Index", new { id = model.EventId });
            }
            var statuses = _eventDetailService.GetEventStatuses().Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            model.Statuses = statuses;
            return View(model);
        }

        // GET: EventDetailController/Edit/5
        public ActionResult Edit(int id)
        {
            var eventD = _eventDetailService.Get().FirstOrDefault(x => x.Id == id);
            var statuses = _eventDetailService.GetEventStatuses().Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            var model = _mapper.Map<EventDetailModel>(eventD);
            model.Statuses = statuses;
            return View(model);
        }

        // POST: EventDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EventDetailModel model)
        {
            if (ModelState.IsValid)
            {
                await _eventDetailService.UpdateEventAsync(model);
                return RedirectToAction("Index", new { id = model.EventId });
            }

            return View(model);
        }

        // GET: EventDetailController/Delete/5
        public ActionResult Delete(int id)
        {
            var eventD = _eventDetailService.Get().FirstOrDefault(x => x.Id == id);
            var model = _mapper.Map<EventDetailModel>(eventD);
            return View(model);
        }

        // POST: EventDetailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(EventDetailModel model)
        {
            await _eventDetailService.DeleteEventDetail(model.Id);
            return RedirectToAction("Index", new { id = model.EventId });
        }
    }
}
