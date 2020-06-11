using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MtgApiManager.Lib.Model;
using MtgManagerWebApi.Contracts.Responses;
using MtgManagerWebApi.Service;

namespace MtgManagerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;

        public CardsController(ICardService cardService,IMapper mapper)
        {
            _cardService = cardService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CardResponse>), 200)]
        public async Task<IActionResult> Get()
        {
            var cards = await _cardService.AllAsync();

            return Ok(_mapper.Map<List<Card>, List<CardResponse>>(cards));
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(typeof(CardResponse),200)]
        public async Task<IActionResult> Get([FromRoute]string id)
        {
            return Ok(_mapper.Map <Card,CardResponse>(await _cardService.GetAsync(id)));
        }

        [HttpGet("FindAll")]
        [ProducesResponseType(typeof(CardResponse),200)]
        public async Task<IActionResult> FindAll([FromBody]Dictionary<string,string> param)
        {
            var cards = await _cardService.AllAsync(param);
            return Ok(_mapper.Map<List<Card>, List<CardResponse>>(cards));
        }

    }
}
