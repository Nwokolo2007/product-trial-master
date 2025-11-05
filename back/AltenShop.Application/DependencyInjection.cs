using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
namespace AltenShop.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			// MediatR v12+
			services.AddMediatR(cfg =>
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			// FluentValidation DI extensions
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}

