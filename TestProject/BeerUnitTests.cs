using Api.Controllers;
using Api.Models;
using Application;
using Application.Classes;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class BeerUnitTests
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly Mock<ILogger<VendorBeerManager>> _logger;
        private readonly IContext _context;
        private IVendorBeerManager? _vendorBeerManager;

        public BeerUnitTests()
        {

            IServiceCollection services = new ServiceCollection();
            services.AddApplicationServices();
            services.AddInfrastructureServices();
            _serviceProvider = services.BuildServiceProvider();
            _logger = new Mock<ILogger<VendorBeerManager>>();
            _context = new Context();
        }

        [Fact]
        public void ShouldEstimateOrder_VendorNotExist()
        {
            //Arrange
            EstimateDataModel estimateData = new EstimateDataModel
            {
                VendorId = 22,
                Beers = new List<EstimateDataBeerModel>
                {
                    new EstimateDataBeerModel
                    {
                        BeerId = 1,
                        Quantity = 2
                    }
                }
            };

            _vendorBeerManager = new VendorBeerManager(_logger.Object, _context);

            //Action & Assert
            var actionResult = Assert.Throws<VendorNotExistException>(() => _vendorBeerManager.Estimate(estimateData));
        }

        [Fact]
        public void ShouldEstimateOrder_BeerNotSellByVendor()
        {
            //Arrange
            EstimateDataModel estimateData = new EstimateDataModel
            {
                VendorId = 1,
                Beers = new List<EstimateDataBeerModel>
                {
                    new EstimateDataBeerModel
                    {
                        BeerId = 22,
                        Quantity = 2
                    }
                }
            };

            _vendorBeerManager = new VendorBeerManager(_logger.Object, _context);

            //Action & Assert
            var actionResult = Assert.Throws<BeerNotSellByVendorException>(() => _vendorBeerManager.Estimate(estimateData));
        }

        [Fact]
        public void ShouldEstimateOrder_DuplicatesInCommand()
        {
            //Arrange
            EstimateDataModel estimateData = new EstimateDataModel
            {
                VendorId = 1,
                Beers = new List<EstimateDataBeerModel>
                {
                    new EstimateDataBeerModel
                    {
                        BeerId = 1,
                        Quantity = 2
                    },
                          new EstimateDataBeerModel
                    {
                        BeerId = 1,
                        Quantity = 1
                    }
                }
            };

            _vendorBeerManager = new VendorBeerManager(_logger.Object, _context);

            //Action & Assert
            var actionResult = Assert.Throws<DuplicatesInCommandException>(() => _vendorBeerManager.Estimate(estimateData));
        }


        [Fact]
        public void ShouldEstimateOrder()
        {
            //Arrange
            EstimateDataModel estimateData = new EstimateDataModel
            {
                VendorId = 1,
                Beers = new List<EstimateDataBeerModel>
                {
                    new EstimateDataBeerModel
                    {
                        BeerId = 1,
                        Quantity = 2
                    }
                }
            };

            _vendorBeerManager = new VendorBeerManager(_logger.Object, _context);

            //Action & Assert
            var res = _vendorBeerManager.Estimate(estimateData);
            Assert.Equal(100, res.Amount);
        }
    }
}
