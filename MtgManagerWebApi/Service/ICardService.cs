using MtgApiManager.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MtgManagerWebApi.Service
{
    public interface ICardService
    {
        Task<List<Card>> AllAsync();

        Task<Card> GetAsync(string id);

        Task<List<Card>> AllAsync(IDictionary<string,string> predicate);
    }
}
