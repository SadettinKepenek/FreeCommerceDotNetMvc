using System.Collections.Generic;
using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class WishlistManager:IWishlistService
    {
        private readonly IWishlistDal _wishlistDal;

        public WishlistManager()
        {
            
        }

        public WishlistManager(IWishlistDal wishlistDal)
        {
            _wishlistDal = wishlistDal;
        }
        public void Dispose()
        {
        }

        public ServiceResult Insert(Wish entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_wishlistDal.Insert(entity));
        }

        public ServiceResult Update(Wish entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_wishlistDal.Update(entity));
        }

        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_wishlistDal.Delete(id));
        }

        public Wish SelectById(int id)
        {
            return _wishlistDal.SelectById(id);
        }

        public List<Wish> SelectByFilter(List<DBFilter> filters)
        {
            return _wishlistDal.SelectByFilter(filters);
        }

        public List<Wish> SelectAll()
        {
            return _wishlistDal.SelectAll();
        }
    }
}