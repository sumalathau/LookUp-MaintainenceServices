using Retalix.StoreServices.Model.Infrastructure.DataMovement;
using Retalix.StoreServices.Model.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retalix.StoreServices.BusinessServices.FrontEnd.BusinessRoles
{
   public interface IBusinessRoles 
    {
        int role_id { get; set; }
        string bus_unit_id { get; set; }
        string vc_role_code { get; set; }
        string vc_name { get; set; }
    }
}
