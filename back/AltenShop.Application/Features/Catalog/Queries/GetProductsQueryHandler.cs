using AltenShop.Application.Common.Models;
using AltenShop.Application.Features.Catalog.DTOs;
using AltenShop.Application.Ports.Repositories;
using AutoMapper;
using MediatR;

namespace AltenShop.Application.Features.Catalog.Queries;

public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedList<ProductDto>>
{
	private readonly IProductRepository _repo;
	private readonly IMapper _mapper;
	public GetProductsQueryHandler(IProductRepository repo, IMapper mapper)
	{ _repo = repo; _mapper = mapper; }

	public async Task<PaginatedList<ProductDto>> Handle(GetProductsQuery r, CancellationToken ct)
	{
		var (products, total) = await _repo.GetPaginatedAsync(r.PageNumber, r.PageSize, r.Category, r.Search, ct);
		var items = _mapper.Map<List<ProductDto>>(products);
		return new PaginatedList<ProductDto>(items, total, r.PageNumber, r.PageSize);
	}
}
