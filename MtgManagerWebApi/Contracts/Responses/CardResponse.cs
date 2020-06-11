using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgManagerWebApi.Contracts.Responses
{
    public class CardResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri ImageUrl { get; set; }
        public string ManaCost { get; set; }
    }
}
