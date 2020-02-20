using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //startupta yaptığımız dependency injection'ı business'a taşıdık çünkü
            //yarın bir gün web api yerine direkt mvc projesinde kullanılması istenirse
            //bir daha uğraşmamıza gerek kalmaz.
            //autofac'i web apiye tanıtmamız için web api projesi altında program.cs'de işlem yaparız.
            //Web api içerisine autofac.dependency package'ını nuget'tan yüklemek lazım!
            //eğer birisi ctor'da IProductService isterse ona ProductManager'ı ver.
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();


        }
    }
}
