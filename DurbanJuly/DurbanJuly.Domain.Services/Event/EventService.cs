using AutoMapper;
using DurbanJuly.Domain.Services.Base;
using DurbanJuly.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DurbanJuly.Models;
using System.Threading.Tasks;

namespace DurbanJuly.Domain.Services
{
   public class EventService : BaseService<Event>
    {
        private readonly IMapper _mapper;
        private readonly DefaultDbContext _Dbcontext;
        public EventService(IMapper mapper, DefaultDbContext context)
            : base(context)
        {
            _mapper = mapper;
            _Dbcontext = context;
        }

        public List<EventModel> GetEvents(long tournamentId)
        {
           return _mapper.Map<List<EventModel>>(_Dbcontext.Events.Include(i => i.Tournament).Where(x => x.TournamentId == tournamentId));
        }

        public async Task UpdateEventAsync(EventModel eventModel)
        {
            var eventd = Get().FirstOrDefault(x => x.Id == eventModel.Id);
            if (eventd != null)
            {
                eventd.Name = eventModel.Name;
                eventd.Number = eventModel.Number;
                eventd.EventDateTime = eventModel.EventDateTime;
                eventd.EventEndDateTime = eventModel.EventEndDateTime;
                eventd.AutoClose = eventModel.AutoClose;
                await _Dbcontext.SaveChangesAsync();
            }
        }

        public async Task DeleteEvent(long id)
        {
            var eventD = Get().FirstOrDefault(x => x.Id == id);
            if (eventD != null)
            {
                _Dbcontext.Remove(eventD);
                await _Dbcontext.SaveChangesAsync();
            }
        }
    }
}
