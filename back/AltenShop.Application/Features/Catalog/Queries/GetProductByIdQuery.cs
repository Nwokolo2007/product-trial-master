using AltenShop.Application.Features.Catalog.DTOs;
using MediatR;

namespace AltenShop.Application.Features.Catalog.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
