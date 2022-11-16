using AutoMapper;
using MTechSystemApi.Models;

namespace MTechSystemApi.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeEntity, EmployeeRequest>();
        }
    }
}
