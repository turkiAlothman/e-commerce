using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.utils;

namespace e_commerce.Middlewares
{
    public class SessionManagement
    {
        private readonly RequestDelegate next;

        public SessionManagement(RequestDelegate next){
            this.next = next;
        }
        public async Task Invoke(HttpContext context){
            
            Debugging.print(context.Request.Headers.Cookie.First());
            context.Response.Cookies.Append("name","turki");
            await next.Invoke(context);

        }
    }
}