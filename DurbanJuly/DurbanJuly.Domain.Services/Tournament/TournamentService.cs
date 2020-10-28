using AutoMapper;
using DurbanJuly.Domain.Services.Base;
using DurbanJuly.Infrastructure.Data.Contexts;
using DurbanJuly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DurbanJuly.Domain.Services
{
    public class TournamentService : BaseService<Tournament>
    {
        private readonly IMapper _mapper;
        private readonly DefaultDbContext _Dbcontext;
        public TournamentService(IMapper mapper, DefaultDbContext context)
            : base(context)
        {
            _mapper = mapper;
            _Dbcontext = context;
        }

        public List<TournamentModel> GetAllTournaments()
        {
            var result = _Dbcontext.Tournaments.Include(i => i.Events).ToList();
            return _mapper.Map<List<Tournament>, List<TournamentModel>>(result);
        }

        public async Task UpdateTournamentAsync(TournamentModel tournamentModel )
        {
            var tournament = Get().FirstOrDefault(x => x.Id == tournamentModel.Id);
            if(tournament != null)
            {
                tournament.Name = tournamentModel.Name;
                await _Dbcontext.SaveChangesAsync();
            }
        }

        public async Task DeleteTournament(long id)
        {
            var tournament = Get().FirstOrDefault(x => x.Id == id);
            if (tournament != null)
            {
                _Dbcontext.Remove(tournament);
                await _Dbcontext.SaveChangesAsync();
            }
        }

    }
}
