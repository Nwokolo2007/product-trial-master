using AltenShop.Application.Common.Models;
using AltenShop.Application.Features.Catalog.DTOs;
using MediatR;

namespace AltenShop.Application.Features.Catalog.Queries;

public record GetProductsQuery(
	int PageNumber = 1,
	int PageSize = 10,
	string? Category = null,
	string? Search = null
) : IRequest<PaginatedList<ProductDto>>;
