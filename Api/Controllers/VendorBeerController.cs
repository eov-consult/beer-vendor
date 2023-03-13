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
    public class VendorBeerController : ControllerBase
    {
        private readonly ILogger<VendorBeerController> _logger;
        private readonly IVendorBeerManager _vendorBeerManager;
        private readonly IMapper _mapper;

        public VendorBeerController(ILogger<VendorBeerController> logger, IVendorBeerManager vendorBeerManager, IMapper mapper)
        {
            _logger = logger;
            _vendorBeerManager = vendorBeerManager;
            _mapper = mapper;
        }

        [HttpPost("AddUpdateBeerVendor")]
        public void AddUpdateBeerVendor(AddBeerToVendorDto data)
        {
            _vendorBeerManager.AddOrUpdateBeerVendor(data.BeerId, data.VendorId, data.Quantity);
        }

        [HttpPost("GetEstimate")]
        public ReturnedEstimateModelDto GetEstimate(EstimateDataModelDto data)
        {
            var model = _mapper.Map<EstimateDataModel>(data);

            var retModel = _vendorBeerManager.Estimate(model);

            return _mapper.Map<ReturnedEstimateModelDto>(retModel);
        }
    }
}