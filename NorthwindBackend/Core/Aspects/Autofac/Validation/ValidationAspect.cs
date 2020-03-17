using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.Messages;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect:MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //gönderilen validatorType bir IValidator türünde değilse
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //<> bu kısım içinde yazılan generic argument'leri getirir.
            //bizde sadece 1 tane argument olduğu için direkt 0 yazdık. Yani ilk ve tek argumenti getirir.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //methodun argument'lerine(verilen parametreye) bak ve eğer onun tipi yukarıda tanımladığımız
            //entityType ile aynıysa listeyi getir.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
