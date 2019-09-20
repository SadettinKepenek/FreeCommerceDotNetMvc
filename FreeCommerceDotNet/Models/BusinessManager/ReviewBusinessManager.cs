using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.Interfaces;
using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class ReviewBusinessManager : IBusinessOperations<ReviewBM>
    {
        public void Dispose()
        {

        }

        public List<ReviewBM> Get()
        {
            using (var m = new ReviewsManager())
            {
                var reviews = m.GetAll();
                List<ReviewBM> list = new List<ReviewBM>();
                foreach (var reviewse in reviews)
                {
                    list.Add(new ReviewBM(reviewse.ReviewId));
                }

                return list;
            }

        }

        public ReviewBM GetById(int id)
        {
            return new ReviewBM(id);
        }

        public int Add(ReviewBM entry)
        {
            using (var m = new ReviewsManager())
            {
                entry.Reviews.ProductId = entry.Products.ProductId;
                entry.Reviews.CustomerId = entry.Customers.CustomerId;
                return m.Add(entry.Reviews);
            }

        }

        public bool Update(ReviewBM entry)
        {
            using (var m = new ReviewsManager())
            {
                entry.Reviews.ProductId = entry.Products.ProductId;
                entry.Reviews.CustomerId = entry.Customers.CustomerId;
                m.Update(entry.Reviews);
                return true;
            }
        }

        public bool Delete(ReviewBM entry)
        {
            using (var m = new ReviewsManager())
            {
                return m.Delete(entry.Reviews.ReviewId);
            }
        }
    }
}