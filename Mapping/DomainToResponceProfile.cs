using AuctionsAPI.Contracts.Responses;
using AuctionsAPI.Models;
using AutoMapper;

namespace AuctionsAPI.Mapping
{
    public class DomainToResponceProfile : Profile
    {
        public DomainToResponceProfile() 
        {
            CreateMap<Item, AuctionsResponse>();
            CreateMap<Sale, SaleResponce>();
        }
    }
}
