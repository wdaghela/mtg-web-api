using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.VisualBasic;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;
using MtgManagerWebApi.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MtgManagerWebApi.Service
{
    
    public class CardsService : ICardService
    {
        private readonly HttpClient _client;
        public CardsService()
        {
            _client = new HttpClient();
        }
        public async Task<List<Card>> AllAsync()
        {
        
            var response = await _client.GetAsync(ApiRoutes.Cards.GetAll);

            var result = await response.Content.ReadAsStringAsync();
            var ser = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var cards = JsonConvert.DeserializeObject<RootCardListDto>(result,ser);
            return cards.Cards.Select(x => new Card(x)).ToList();
          
        }

        public async Task<List<Card>> AllAsync(IDictionary<string, string> predicate)
        {
            var uri = new UriBuilder(ApiRoutes.BaseUrl + "/cards");
            var query = HttpUtility.ParseQueryString(uri.Query);
            foreach (KeyValuePair<string,string> exp in predicate)
            {
                query[exp.Key] = exp.Value;
            }
            uri.Query = query.ToString();
            var response = await _client.GetAsync(uri.ToString());
            var result = await response.Content.ReadAsStringAsync();
            var ser = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var cards = JsonConvert.DeserializeObject<RootCardListDto>(result, ser);
            return cards.Cards.Select(x => new Card(x)).ToList();
        }

        public async Task<Card> GetAsync(string id)
        {
            
            var uri = new UriBuilder(ApiRoutes.BaseUrl + $"/cards/{id}");
            var test = uri.ToString();
            var response = await _client.GetAsync(test);
            var result = await response.Content.ReadAsStringAsync();
            var ser = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var card = JsonConvert.DeserializeObject<RootCardDto>(result, ser);
            return new Card(card.Card);
           
        }

        
    }
}
