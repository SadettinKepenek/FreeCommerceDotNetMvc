using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.DAL.Concrete;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace FreeCommerceDotNet.LayerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestAttributeGroupRepository();

            //TestReview();
            //TestBrandRepository();
            CategoryDalTest();
        }

        private static void CategoryDalTest()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            List<DBFilter> filters = new List<DBFilter>();
            filters.Add(new DBFilter()
            {
                ParamName = "@CategoryId",
                ParamValue = 1
            });
            foreach (Category category in categoryRepository.SelectByFilter(filters))
            {
                Console.WriteLine(category.CategoryName);
                foreach (Category subCategory in category.SubCategories)
                {
                    Console.WriteLine(subCategory.CategoryName);
                }

                Console.ReadKey();
            }

            Console.ReadKey();
        }

        private static void TestBrandRepository()
        {
            IRepository<Brand> brandRepository = new BrandRepository();
            //var brandEntity=new Brand(){BrandName = "Test Brand Procedure Layer Insert Insert Procedure",BrandDescription = "Test Brand Description"};
            //var result = brandRepository.Insert(brandEntity);
            //Console.WriteLine(String.Format("Id : {0} Message {1}", result.Id, result.Message));
            //brandEntity.BrandId = result.Id;
            //brandEntity.BrandName = "Brand Name Updated by Repo";
            //result = brandRepository.Update(brandEntity);
            //Console.WriteLine(String.Format("Id : {0} Message {1}", result.Id, result.Message));
            //result = brandRepository.Delete(brandEntity.BrandId);
            //Console.WriteLine(String.Format("Id : {0} Message {1}", result.Id, result.Message));
            foreach (Brand brand in brandRepository.SelectAll())
            {
                Console.WriteLine(brand.BrandName);
                Console.WriteLine(brand.BrandDescription);
            }
            Console.ReadKey();

            TestReview();

        }


        private static void TestReview()
        {
            IRepository<Reviews> repository = new ReviewRepository();
            var reviews = new List<Reviews>();
            reviews = repository.SelectAll();
            foreach (var item in reviews)
            {
                Console.WriteLine("ReviewId:" + item.ReviewId);
                Console.WriteLine("Comment:" + item.Text);
                Console.WriteLine("CustomerId:" + item.CustomerId);
                Console.WriteLine("CustomerFirstName:" + item.customer.Firstname);
            }

            Console.ReadKey();
        }
    }
}
