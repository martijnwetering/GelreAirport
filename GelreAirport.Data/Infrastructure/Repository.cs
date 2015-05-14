using System.Collections.Generic;
using System.Data;

namespace GelreAirport.Data.Infrastructure
{
    public abstract class Repository<T> where T : new()
    {
        protected AdoNetContext Context { get; }

        protected Repository(AdoNetContext context)
        {
            Context = context;
        }

    }
}