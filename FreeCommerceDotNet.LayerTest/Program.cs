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
            var reviews = new Reviews();
            reviews = repository.SelectById(5);
            
            
                Console.WriteLine("ReviewId:" + reviews.ReviewId );
                Console.WriteLine("Comment:" + reviews.Text );
                Console.WriteLine("CustomerId:" + reviews.CustomerId);
                Console.WriteLine("CustomerFirstName:" + reviews.customer.Firstname);
            
            
            Console.ReadKey();
        }
    }
}
