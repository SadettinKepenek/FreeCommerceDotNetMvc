using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.DAL.Concrete;
using FreeCommerceDotNet.Entities.Concrete;
using System;

using System.Collections.Generic;
using System.Reflection;
using Attribute = FreeCommerceDotNet.Entities.Concrete.Attribute;

namespace FreeCommerceDotNet.LayerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestAttributeGroupRepository();

            //TestReview();

            //TestBrandRepository(); //sen şimdi ne pull ettin bana filterları mı  tamamdır
            // CategoryDalTest();
            //  TestOrderDetail();
            //TestBrandRepository();
            //CategoryDalTest();
            //TestOrderMaster();
           
            //TestOrderDetail();
            //TestBrandRepository();
            //CategoryDalTest();
           // TestProductAttributeRepository();
            //TestCustomer();
            TestOrderReturnsRepository();
            Console.ReadKey();

        }

        private static void TestOrderReturnsRepository()
        {
            OrderReturnRepository orderReturnRepository=new OrderReturnRepository();
            foreach (OrderReturn orderReturn in orderReturnRepository.SelectAll())
            {
                foreach (PropertyInfo info in orderReturn.GetType().GetProperties())
                {
                   
                    Console.WriteLine(info.GetValue(orderReturn));
                }

                foreach (PropertyInfo info in orderReturn.CustomerBm.GetType().GetProperties())
                {

                    Console.WriteLine(info.GetValue(orderReturn.CustomerBm));
                }
            }
        }

        private static void TestProductAttributeRepository()
        {
            ProductAttributeRepository productAttributeRepository=new ProductAttributeRepository();
            foreach (ProductAttribute attribute in productAttributeRepository.SelectAll())
            {
                Console.WriteLine(attribute.ProductId+" "+attribute.AttributeId+" "+attribute.AttributeDescription);
            }

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
            foreach (Product product in categoryRepository.SelectById(1).Products)
            {

                Console.WriteLine(product.ProductName);
                foreach (var productProductPrice in product.ProductPrices)
                {
                    Console.WriteLine(productProductPrice.Price);
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
        private static void TestOrderDetail()
        {
            IRepository<OrderDetail> repo = new OrderDetailRepository();
            OrderDetail orderdetail = new OrderDetail();
          /*  orderdetail.ProductBm = new Product();
            orderdetail.ProductBm.ProductPrices = new List<ProductPrice>();
            orderdetail.ProductBm.ProductPrices.Add(new ProductPrice { Price = 50});
            orderdetail.Quantity = 5;
            orderdetail.isDiscountedPrice = false;
            orderdetail.OrderId = 7;
            orderdetail.ProductId = 3;  */
            var result = repo.SelectAll();

            foreach (var item in result)
            {
                Console.WriteLine("order id:"+item.OrderId);
                Console.WriteLine("ProductPrice:"+item.isDiscountedPrice);
            }
            Console.ReadKey();
        }
        private static void TestOrderMaster()
        {
            IRepository<OrderMaster> repository = new OrderMasterRepository();
           // OrderMaster om = new OrderMaster() { OrderId = 11,DeliveryComment = "test1" ,TrackNumber = "test",ShippingId = 1,PaymentGatewayId = 1,DeliveryDate = "test",DeliveryStatus = "test",OrderDate = "test",CustomerId = 13};
            DBResult result = repository.Delete(11);


            Console.ReadKey();
        }

        private static void TestCustomer()
        {
            IRepository<Customer> customer = new CustomerRepositorycs();
            Customer cus = new Customer();
          /*  cus.TaxAddress = "test";
            cus.Firstname = "sadettin";
            cus.Lastname = "kepenek";
            cus.CustomerId = 20;
            cus.Password = "sifre";
            cus.Telephone = "test";
            cus.Status = false;
            cus.SegmentId = 1;
            cus.Address1 = "test";
            cus.Address2 = "test";
            cus.Email = "test";
            cus.TaxAddress = "test";
            cus.UserId = 3;*/
            DBResult result = customer.Delete(20);
            Console.WriteLine("Updated Entity ID " + result.Id + " Message " + result.Message);


        }
    }
}
