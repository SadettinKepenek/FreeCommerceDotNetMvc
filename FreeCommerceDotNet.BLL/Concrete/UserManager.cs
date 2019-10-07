using System.Collections.Generic;
using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class UserManager:IUserService
    {
        private IUserDal _userDal;

        public UserManager()
        {
            
        }

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public ServiceResult Insert(User entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_userDal.Insert(entity));
        }

        public ServiceResult Update(User entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_userDal.Update(entity));
        }

        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_userDal.Delete(id));
        }

        public User SelectById(int id)
        {
            return _userDal.SelectById(id);
        }

        public List<User> SelectByFilter(List<DBFilter> filters)
        {
            return _userDal.SelectByFilter(filters);
        }

        public List<User> SelectAll()
        {
            return _userDal.SelectAll();
        }

        public void Dispose()
        {
        }
    }
}