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

            //TestBrandRepository(); //sen şimdi ne pull ettin bana filterları mı  tamamdır
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
                Console.WriteLine(category.CategoryName);//çok bieşy pamdım
                foreach (Category subCategory in category.SubCategories)
                {
                    Console.WriteLine(subCategory.CategoryName);
                }

                Console.ReadKey();
            }

            Console.ReadKey();
            // TestBrandRepository();
            TestSegment();

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
            // var reviews = new Reviews() { ReviewId = 4,CustomerId = 14, ProductId = 3, Title = "test1", Text = "reviewUpdateTest", Date = "test", LikeCount = 5, DislikeCount = 0, Rating = 5, Status = true };
            //  DBResult result = repository.Delete(4);
            // Console.WriteLine("Updated Entity ID " + result.Id + " Message " + result.Message);
            foreach (var items in repository.SelectAll())
            {

                Console.Write("ReviewId:" + items.ReviewId + "\n");
                Console.Write("Comment:" + items.Text + "\n");
                Console.Write("Title:" + items.Title + "\n");
                Console.Write("Status:" + items.Status + "\n");

            }


            Console.ReadKey();
        }

        private static void TestSegment()
        {
            IRepository<Segment> repository = new SegmentRepository();
            var segments = repository.SelectById(3);
            
            
            
                Console.WriteLine("Id:"+ segments.SegmentId);
            Console.WriteLine("Name:" + segments.SegmentName);


            Console.ReadKey();
        }
    }
}
