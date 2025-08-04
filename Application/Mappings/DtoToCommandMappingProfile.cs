using Application.DTOs;
using Application.Mediator.Users.Commands;
using AutoMapper;

namespace Application.Mappings;

public class DtoToCommandMappingProfile : Profile
{
	public DtoToCommandMappingProfile()
	{
		CreateMap<UserCreateDto, UserCreateCommand>();
		CreateMap<UserUpdateDto, UserUpdateCommand>();
	}
}