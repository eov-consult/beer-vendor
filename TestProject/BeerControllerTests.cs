using Api.Controllers;
using Api.Mapper;
using Api.Models;
using Application;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestProject
{
    public class BeerControllerTests
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly Mock<ILogger<BeerController>> _beerControllerLogger;
        private readonly Mock<ILogger<VendorBeerController>> _vendorBeerControllerLogger;
        private readonly Mock<IBeerManager> _beerManager;
        private readonly Mock<IVendorBeerManager> _vendorBeerManager;
        private BeerController _beerController;
        private VendorBeerController _vendorBeerController;
        private readonly IMapper _mapper;

        public BeerControllerTests()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile()); //your automapperprofile 
            });
            _mapper = mockMapper.CreateMapper();

            IServiceCollection services = new ServiceCollection();
            services.AddApplicationServices();
            services.AddInfrastructureServices();
            _serviceProvider = services.BuildServiceProvider();

            _beerControllerLogger = new Mock<ILogger<BeerController>>();
            _vendorBeerControllerLogger = new Mock<ILogger<VendorBeerController>>();
            _beerManager = new Mock<IBeerManager>();
            _vendorBeerManager = new Mock<IVendorBeerManager>();
        }

        [Fact]
        public void ShouldReturnAllBeersBrewerAndVendors()
        {
            //Arrange
            Beer beer = new Beer
            {
                Id = 1,
                AlcoholDegree = 5.5f,
                Amount = 20,
                Name = "Jupiler",
                BrewerId = 1
            };
            Brewer brewer = new Brewer
            {
                Id = 1,
                Name = "AB Inbev",
                Beers = new List<Beer> { beer }
            };
            beer.Brewer = brewer;

            Vendor vendor = new Vendor
            {
                Id = 1,
                Name = "GeneDrinks",
            };
            vendor.VendorBeers = new List<VendorBeer> { new VendorBeer
            {
                BeerId = 1,
                Beer = beer,
                Vendor = vendor,
                Id = 1,
                VendorId = 1,
                Quantity = 50
            } };
            beer.VendorBeers = vendor.VendorBeers;

            _beerManager.Setup(a => a.Get()).Returns(new List<Beer> { beer });
            _beerController = new BeerController(_beerControllerLogger.Object, _beerManager.Object, _mapper);

            //Act
            var res = _beerController.GetBeersSoldList();

            //Assert
            var actionResult = Assert.IsType<List<BeerSoldDto>>(res);
            Assert.Equal(1, res.Count());
        }

    }
}