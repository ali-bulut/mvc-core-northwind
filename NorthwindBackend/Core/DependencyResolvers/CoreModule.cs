using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
    //artık startupda erişebildiğimiz services.Addblabla şeklindeki methodlara bu classtan da erişebiliriz.
    public class CoreModule:ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
        }
    }
}
