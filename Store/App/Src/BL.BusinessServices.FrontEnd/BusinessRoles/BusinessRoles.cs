using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retalix.StoreServices.BusinessServices.FrontEnd.BusinessRoles
{
    [Serializable]
    public class BusinessRoles : IBusinessRoles
    {
        public BusinessRoles() { }
        public virtual int role_id { get; set; }
        public virtual string bus_unit_id { get; set; }
        public virtual string vc_role_code { get; set; }
        public virtual string vc_name { get; set; }
    }
}
