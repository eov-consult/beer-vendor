using Api.Models;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeerController : ControllerBase
    {
        private readonly ILogger<BeerController> _logger;
        private readonly IBeerManager _beerManager;
        private readonly IMapper _mapper;

        public BeerController(ILogger<BeerController> logger, IBeerManager beerManager, IMapper mapper)
        {
            _logger = logger;
            _beerManager = beerManager;
            _mapper = mapper;
        }

        [HttpGet("GetBeersSoldList")]
        public IEnumerable<BeerSoldDto> GetBeersSoldList()
        {
            var beers = _beerManager.Get();
            var beersSold = _mapper.Map<IEnumerable<BeerSoldDto>>(beers);

            return beersSold;
        }

        [HttpPost("CreateBeer")]
        public void CreateBeer(CreateBeerModelDto data)
        {
            var createBeerModel = _mapper.Map<CreateBeerModel>(data);

            _beerManager.Add(createBeerModel);
        }

        [HttpDelete("DeleteBeer/{id}")]
        public void DeleteBeer(int id)
        {
            _beerManager.Delete(id);
        }
    }
}