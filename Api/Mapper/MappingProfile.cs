using Api.Models;
using Application.Models;
using Domain.Entities;

namespace Api.Mapper
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Vendor, VendorDto>()
            .ReverseMap();

            CreateMap<Brewer, BrewerDto>()
            .ReverseMap();

            CreateMap<Beer, BeerSoldDto>();

            CreateMap<CreateBeerModelDto, CreateBeerModel>();

            CreateMap<VendorBeer, VendorBeerDto>();
            CreateMap<EstimateDataModelDto,EstimateDataModel>().ReverseMap();
            CreateMap<EstimateDataBeerModelDto,EstimateDataBeerModel>().ReverseMap();
            CreateMap<ReturnedEstimateModel, ReturnedEstimateModelDto>();
        }
    }
}
