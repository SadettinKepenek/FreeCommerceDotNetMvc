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
            var reviews = new List<Reviews>();
            reviews = repository.SelectAll();
            foreach (var item in reviews)
            {
                Console.WriteLine("ReviewId:" + item.ReviewId );
                Console.WriteLine("Comment:" + item.Text );
                Console.WriteLine("CustomerId:" + item.CustomerId);
                Console.WriteLine("CustomerFirstName:" + item.customer.Firstname);
            }
            
            Console.ReadKey();
        }
    }
}
