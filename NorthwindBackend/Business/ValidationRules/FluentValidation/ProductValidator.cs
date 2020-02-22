using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    //bu classın bir validator classı olması için fluentvalidation içindeki AbstractValidator classından inherit
    //edilmesi gerekir.
    public class ProductValidator : AbstractValidator<Product>
    {
        //burada kesinlikle business işlemleri yapılmamalı.
        //yani db'ye bağlanıp business'ta yapılması gereken işlemleri burada db'ye bağlanıp yapmamalıyız!
        public ProductValidator()
        {
            //bu şekilde aynı anda iki kural da koyabiliriz.
            //RuleFor(p => p.ProductName).NotEmpty().Length(2, 30);

            //ama önerilmez. Bu şekilde ayrı ayrı tanımlama yapmak daha iyidir. Yoksa withMessage ile mesaj döndüremezsin.
            //productName empty olamaz. Ve eğer boş döndürülürse withMessage ile yazılan hata mesajını döndür.
            //withMessage kullanmak yerine FluentValidation'ın base mesajlarını da kullanabiliriz.
            //Türkçe dahil 17 dile uyumludur ve ekstra bizim withMessage ile hata mesajı yazdırmamıza çok da
            //gerek yok.
            //RuleFor(p => p.ProductName).NotEmpty().WithMessage("Product Name is required!");
            RuleFor(p => p.ProductName).NotEmpty();
            //productName 2 ile 30 karakter arasında olmak zorunda.
            RuleFor(p => p.ProductName).Length(2, 30);
            RuleFor(p => p.UnitPrice).NotEmpty();
            //unitPrice 1'den büyük veya 1e eşit olmalı. Yani min 1 olmalı.
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1);
            //fakat eğer ürünün categoryId'si 1 ise unitPrice min 10 olmalı.
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            //bu şekilde kendi validation'ımızı da oluşturabiliriz.
            //Method olarak göstermedik yani StartWithA("blabla") şeklinde çünkü fluent bunu bizim için yapıyor.
            //parametre olarak direkt p.ProductName'i gönderiyor.
            //RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Product Name must be start with 'A' !");
        }

        private bool StartWithA(string arg)
        {
            //gönderilen arg(yani p.ProductName olarak tanımladığımız db'den gelen string ifade)
            //A ile başlıyorsa true başlamıyorsa false döndür.
            return arg.StartsWith("A");
        }
    }
}
