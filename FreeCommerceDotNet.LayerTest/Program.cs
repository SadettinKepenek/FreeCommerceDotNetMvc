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
            TestReview();
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
            // var reviews = new Reviews() { ReviewId = 4,CustomerId = 14, ProductId = 3, Title = "test1", Text = "reviewUpdateTest", Date = "test", LikeCount = 5, DislikeCount = 0, Rating = 5, Status = true };
            //  DBResult result = repository.Delete(4);
            // Console.WriteLine("Updated Entity ID " + result.Id + " Message " + result.Message);
            foreach (var items in repository.SelectAll())
            {
                Console.Write("ReviewId:"+items.ReviewId+"\n");
                Console.Write("Comment:" + items.Text + "\n");
                Console.Write("Title:" + items.Title + "\n");
                Console.Write("Status:" + items.Status + "\n");
            }
            Console.ReadKey();
        }
    }
}
