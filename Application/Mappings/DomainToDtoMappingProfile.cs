using Application.DTOs;
using Domain.Entities;
using Domain.Pagination;

namespace Application.Mappings;

public class DomainToDtoMappingProfile : AutoMapper.Profile
{
	public DomainToDtoMappingProfile()
	{
		CreateMap<User, UserDto>().ReverseMap();
		CreateMap<User, UserResponseDto>().ReverseMap();

		CreateMap(typeof(PagedResponse<>), typeof(PagedResponseDto<>));
	}
}