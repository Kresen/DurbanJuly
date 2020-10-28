using AutoMapper;
using DurbanJuly.Domain.Services.Base;
using DurbanJuly.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace DurbanJuly.Domain.Services
{
    public class EventDetailService : BaseService<EventDetail>
    {
        private readonly IMapper _mapper;
        private readonly DefaultDbContext _Dbcontext;
        public EventDetailService(IMapper mapper, DefaultDbContext context)
            : base(context)
        {
            _mapper = mapper;
            _Dbcontext = context;
        }

        public List<Models.EventDetailModel> GetEventDetails(long eventId)
        {

            var result = _mapper.Map<List<Models.EventDetailModel>>(_Dbcontext.EventDetails.Include(p => p.EventDetailStatus).Where(x => x.EventId == eventId).ToList());
            return result;
        }

        public List<EventDetailStatus> GetEventStatuses()
        {
            return _Dbcontext.EventDetailStatus.ToList();
        }

        public void SaveEventDetails(EventDetail eventDetail)
        {
            eventDetail.Id = 0;
            eventDetail.EventDetailStatus = null;
            eventDetail.Event = null;
           _Dbcontext.EventDetails.Add(eventDetail);
            _Dbcontext.SaveChanges();
        }


        public async System.Threading.Tasks.Task UpdateEventAsync(Models.EventDetailModel eventModel)
        {
            var eventd = Get().FirstOrDefault(x => x.Id == eventModel.Id);
            if (eventd != null)
            {
                eventd.Name = eventModel.Name;
                eventd.Number = eventModel.Number;
                eventd.Odd = eventModel.Odd;
                eventd.EventDetailStatusId = eventModel.EventDetailStatusId;
                eventd.FirstTimer = eventModel.FirstTimer;
                await _Dbcontext.SaveChangesAsync();
            }
        }


        public async System.Threading.Tasks.Task DeleteEventDetail(long id)
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
