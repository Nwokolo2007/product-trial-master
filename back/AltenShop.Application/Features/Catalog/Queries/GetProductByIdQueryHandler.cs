using AltenShop.Application.Common.Exceptions;
using AltenShop.Application.Features.Catalog.DTOs;
using AltenShop.Application.Ports.Repositories;
using AutoMapper;
using MediatR;

namespace AltenShop.Application.Features.Catalog.Queries;

public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
	private readonly IProductRepository _repo;
	private readonly IMapper _mapper;
	public GetProductByIdQueryHandler(IProductRepository repo, IMapper mapper)
	{
		_repo = repo; _mapper = mapper;
	}

	public async Task<ProductDto> Handle(GetProductByIdQuery r, CancellationToken ct)
	{
		var product = await _repo.GetByIdAsync(r.Id, ct)
			?? throw new NotFoundException("Product", r.Id);

		return _mapper.Map<ProductDto>(product);
	}
}
