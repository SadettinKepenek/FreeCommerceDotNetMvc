using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.Util
{
    public class DeleteResponseModel
    {
        public List<string> msg { get; set; }  =new List<string>();
        public bool removable { get; set; }
    }

}