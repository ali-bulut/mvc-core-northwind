using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Autofac;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    //invocation denilen şey aslında attribute'u verdiğimiz methodun içindeki kodlardır.
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (Exception)
                {
                    //yapılan işlemleri geri al
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
