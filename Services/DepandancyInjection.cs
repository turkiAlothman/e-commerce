using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Services
{
    public static class DepandancyInjection
    {
        public static void AddServices(this IServiceCollection collection){
            collection.AddTransient<JwtManagement>();
        }
    }
}