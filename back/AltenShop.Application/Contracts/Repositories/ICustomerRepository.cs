using AltenShop.Domain.Entities.Commerce;

namespace AltenShop.Application.Ports.Repositories;

public interface ICustomerRepository
{
	Task AddAsync(Customer customer, CancellationToken ct);
	Task<Customer?> GetByUserIdAsync(Guid userId, CancellationToken ct);
}
