using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployeesWebApi
{
    //Clase que sirve para mapear los datos de la bd y enviarlos segun nuestra necesidad
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //En este caso se obtienen los datos y se envia en una variable fulladdress la combinacion de address y country
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAddress,
                    opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            CreateMap<Employee, EmployeeDto>();

            CreateMap<CompanyForCreationDto, Company>();

            CreateMap<EmployeeForCreationDto, Employee>();

            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();

            CreateMap<CompanyForUpdateDto, Company>();
        }
    }
}
