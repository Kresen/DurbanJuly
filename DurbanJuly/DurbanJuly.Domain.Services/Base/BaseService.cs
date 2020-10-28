using DurbanJuly.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DurbanJuly.Domain.Services.Base
{
    public class BaseService<T> : BaseRepository<T>
        where T : class
    {
        public BaseService(DefaultDbContext context)
            : base(context)
        {

        }
    }
}
