using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class ProductManager:IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        //eğer tepki verme süresi 5 saniye'yi geçerse PerformanceAspect'i çalıştır.
        [PerformanceAspect(5)]
        public IDataResult<List<Product>> GetList()
        {
            //PerformanceAspect'in çalışmasını görmek için methodun 5 saniye sonra çalışmasını söyledik.
            //Thread.Sleep(5000);
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        //[SecuredOperation("Product.List,Admin")]
        [LogAspect(typeof(FileLogger))]
        [CacheAspect(duration:10)]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

        //Cross Cutting Concerns -> Validation, Cache, Log, Performance, Auth, Transaction
        //AOP -> Aspect Orianted Programming  Cross Cutting Concern'ler için kullanılır.
        //Cross Cutting Concerns işlemleri dışında kesinlikle AOP kullanılmamalıdır.

        //bu tarz işlemlerin hepsi business'ta yapılmalı


        [ValidationAspect(typeof(ProductValidator),Priority = 1)]
        //eğer Add işlemi başarıyla gerçekleştiyse içinde IProductService.Get geçen tüm cache'leri siler.
        //örn; getbyid, getlistbycategory, getlist gibi (yukarıdaki methodlar)
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //tabiki bunu her method için ayrı ayrı uygulamamak için base bir sınıfa çekiyoruz.
            //Core layer'ı altında ValidationTool classına çektik.
            //ProductValidator productValidator = new ProductValidator();
            //var result = productValidator.Validate(product);
            //if (!result.IsValid)
            //{
            //    burada FluentValidation'ının base hata mesajları gösterilir. 
            //    eğer biz withMessage ile kendimiz hata mesajı eklemişsek o gözükür.
            //    throw new ValidationException(result.Errors);
            //}

            //bu şekilde de kullanabiliriz fakat direkt methodun üzerinde attribute olarak vermek daha iyidir.
            //ValidationTool.Validate(new ProductValidator(), product);



            //Business codes
            //örn daha önce eklenen ismin bir daha eklenmemesi gibi
            //yada validation kodları business katmanına yazılır.
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded); 
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            //transaction'ın kullanım amacı;
            //eğer db işlemlerinde insert update delete gibi 
            //birinde herhangi bir hata alınırsa öncesinde yapılan tüm işlemleri geri alır.
            //örn bu durumda önce ürünü hatasız güncelleyip daha sonra ekleme yaparken hata alırsa
            //güncellenmiş ürünü eski haline çevirir yani güncelleme geçersiz sayılır.
            _productDal.Update(product);
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
