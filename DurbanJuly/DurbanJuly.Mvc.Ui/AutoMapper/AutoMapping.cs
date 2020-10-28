using AutoMapper;
using DurbanJuly.Domain;
using DurbanJuly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DurbanJuly.Mvc.Ui.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Tournament, TournamentModel>().ForMember(x => x.EventCount, opt => opt.MapFrom(src => src.Events.Count)).ReverseMap();
            CreateMap<Event, EventModel>().ForMember(x => x.TournamentName, opt => opt.MapFrom(src => src.Tournament.Name)).ReverseMap();
            CreateMap<EventDetail, EventDetailModel>().ForMember(x => x.StatusName, opt => opt.MapFrom(src => src.EventDetailStatus.Name)).ReverseMap();

        }
       
    }
}
