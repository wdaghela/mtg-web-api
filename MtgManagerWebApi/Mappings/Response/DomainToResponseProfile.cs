using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MtgApiManager.Lib.Model;
using MtgManagerWebApi.Contracts.Responses;

namespace MtgManagerWebApi.Mappings.Response
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Card, CardResponse>().ConvertUsing(src => new CardResponse
            {
                Id = src.Id,
                Title = src.Name,
                Description = src.Text,
                ImageUrl = src.ImageUrl,
                ManaCost = src.ManaCost
            });
        }
    }
}
