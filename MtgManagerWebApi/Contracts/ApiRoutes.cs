using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgManagerWebApi.Contracts
{
    public class ApiRoutes
    {
        public const string BaseUrl = "https://api.magicthegathering.io/v1";

        public static class Cards
        {
            public const string GetAll = BaseUrl + "/cards";

        }
    }
}
