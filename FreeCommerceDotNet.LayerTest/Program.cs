using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.DAL.Concrete;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.LayerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestAttributeGroupRepository();
            //TestReview();
            //TestBrandRepository();
        }

        private static void TestBrandRepository()
        {
            IRepository<Brand> brandRepository=new BrandRepository();
            var brandEntity=new Brand(){BrandName = "Test Brand Procedure Layer Insert Inserted",BrandDescription = "Test Brand Description"};
            var result = brandRepository.Insert(brandEntity);
            Console.WriteLine(String.Format("Id : {0} Message {1}", result.Id, result.Message));
            brandEntity.BrandId = result.Id;
            //brandEntity.BrandName = "Brand Name Updated by Repo";
            //result = brandRepository.Update(brandEntity);
            //Console.WriteLine(String.Format("Id : {0} Message {1}", result.Id, result.Message));
            //result = brandRepository.Delete(brandEntity.BrandId);
            //Console.WriteLine(String.Format("Id : {0} Message {1}", result.Id, result.Message));
            Console.ReadKey();
        }

        private static void TestAttributeGroupRepository()
        {
            IRepository<AttributeGroup> repository = new AttributeGroupRepository();
            var attributeGroup = new AttributeGroup() {AttributeGroupName = "DB Layer Test Group Name", AttributeGroupId = 15};
            DBResult result = repository.Delete(attributeGroup.AttributeGroupId);
            Console.WriteLine("Deleted Entity ID " + result.Id + " Message " + result.Message);
            Console.ReadKey();
        }

        private static void TestReview()
        {
            IRepository<Reviews> repository = new ReviewRepository();
            var reviews = new Reviews() { CustomerId = 14, ProductId = 3, Title = "test", Text = "test", Date = "test", LikeCount = 5, DislikeCount = 0, Rating = 5, Status = true };
            DBResult result = repository.Insert(reviews);
            Console.WriteLine("Added Entity ID " + result.Id + " Message " + result.Message);
            Console.ReadKey();
        }
    }
}
