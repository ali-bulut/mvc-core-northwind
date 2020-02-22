using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors.Autofac
{
    //IInterceptor'ı kullanmamız için autofac.extras.dynamicproxy package'ını kurmamız lazım.
    //classlar'da, methodlar'da ve istenirse bir method için birden fazla ve son olarak inherit edildiği alt classlarda
    //kullanılabilir.
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple = true,Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute:Attribute,IInterceptor
    {
        //attribute'lar yukarıdan aşağıya doğru çalışır fakat biz bu işin sırasını kendimiz belirlemek istiyoruz.
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
