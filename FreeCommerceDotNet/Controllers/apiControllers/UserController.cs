using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FreeCommerceDotNet.Models.Abstracts;
using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.WebApi;
using FreeCommerceDotNet.Models.WebApi.ResponseModels;

namespace FreeCommerceDotNet.Controllers.apiControllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        [WebApiAuthorize(Roles = "Admin")]
        public List<UsersBM> Get()
        {
            using (UsersBusinessManager bm = new UsersBusinessManager())
            {
                return bm.Get();
            }
        }

        // GET: api/User/5

        [WebApiAuthorize]
        public UsersBM Get(int id)
        {
            using (UsersBusinessManager bm=new UsersBusinessManager())
            {
                return bm.GetById(id);
            }
        }

        // POST: api/User
        [WebApiAuthorize(Roles="Admin")]
        public HttpResponseModel<Users> Post([FromBody]Users user)
        {
            using (UsersManager m = new UsersManager())
            {
                int id=m.Add(user);
                user.UserId = id;
            }

            HttpResponseModel<Users> responseModel = new UserResponseModel()
            {
                ResponseObject = user,
                ResponseText = "Kullanıcı Başarı İle Eklendi",
                StatusCode = HttpStatusCode.OK
            };
            return responseModel;
        }

        // PUT: api/User/5
        [WebApiAuthorize(Roles = "Admin")]

        public HttpResponseModel<Users> Put(int id, [FromBody]Users user)
        {
            Users updated;
            using (UsersManager m=new UsersManager())
            {
                m.Update(user);
                
            }

            HttpResponseModel<Users> responseModel=new UserResponseModel()
            {
                ResponseObject =user,
                ResponseText = "Kullanıcı Başarı İle Güncellendi",
                StatusCode = HttpStatusCode.OK
            };
            return responseModel;
        }

        // DELETE: api/User/5
        [WebApiAuthorize(Roles = "Admin")]
        public HttpResponseModel<Users> Delete(int id)
        {
            using (UsersManager m = new UsersManager())
            {
                m.Delete(id);

            }
            HttpResponseModel<Users> responseModel = new UserResponseModel()
            {
                ResponseObject = null,
                ResponseText = "Kullanıcı Başarı İle Silindi",
                StatusCode = HttpStatusCode.OK
            };
            return responseModel;
        }
    }
}
