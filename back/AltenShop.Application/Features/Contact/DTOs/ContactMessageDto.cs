namespace AltenShop.Application.Features.Contact.DTOs;

public record ContactMessageDto(Guid Id, string Email, string Message, DateTime CreatedAtUtc);
