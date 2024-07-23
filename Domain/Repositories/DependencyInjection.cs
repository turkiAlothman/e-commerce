

using e_commerce.Domain.Repositories.Implementations;
using e_commerce.Domain.Repositories.Interfaces;

namespace e_commerce.Domain.Repositories
{
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services){
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        }
    }
}