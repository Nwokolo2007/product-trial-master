using AltenShop.Application.Features.Catalog.DTOs;
using AltenShop.Domain.Entities.Commerce;
using AutoMapper;

namespace AltenShop.Application.Mapping
{
	public sealed class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductDto>()
				.ForMember(d => d.InventoryStatus, o => o.MapFrom(s => s.InventoryStatus.Value));
		}
	}
}
