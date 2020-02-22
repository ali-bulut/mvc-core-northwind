using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Interceptor;

namespace Core.Utilities.Interceptors.Autofac
{
    public abstract class MethodInterception:MethodInterceptionBaseAttribute
    {
        //OnBefore -> Method çalışmadan önce sen çalış demek
        protected virtual void OnBefore(IInvocation invocation) { }
        //OnAfter -> Method çalıştıktan sonra sen çalış demek
        protected virtual void OnAfter(IInvocation invocation) { }
        //OnException -> Method hata verdiğinde sen çalış demek
        protected virtual void OnException(IInvocation invocation) { }
        //OnSuccess -> Method başarılıysa sen çalış demek
        protected virtual void OnSuccess(IInvocation invocation) { }


        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                //invocation methodunu çalıştır demek.
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }

            OnAfter(invocation);
        }
    }
}
