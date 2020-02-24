using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace Core.Utilities.Interceptors.Autofac
{
    public class AspectInterceptorSelector:IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);

            //tüm methodların üstüne ayrı ayrı attribute yazar gibi yazmak yerine direkt olarak 
            //eğer tüm methodlarımız için kullanmamız gereken bir aspect varsa direkt olarak buradan ekleyebiliriz.
            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));

            //priority'ye göre sıralı gelsin
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
