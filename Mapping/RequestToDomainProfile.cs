using AuctionsAPI.Contracts.Queries;
using AuctionsAPI.Models;
using AutoMapper;

namespace AuctionsAPI.Mapping
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile() 
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<GetAllAuctionsQuery, GetAllAuctionsFilter>();
        }
    }
}
