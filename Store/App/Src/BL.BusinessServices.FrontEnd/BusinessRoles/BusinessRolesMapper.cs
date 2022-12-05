using Retalix.Contracts.Generated.BusinessRoles;
using Retalix.StoreServices.Model.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retalix.StoreServices.BusinessServices.FrontEnd.BusinessRoles
{  
    class BusinessRolesMapper : IMapper<IBusinessRoles, BusinessRolesType>, IMapper<BusinessRolesType, IBusinessRoles>
    {
        public void Map(IBusinessRoles source, BusinessRolesType target, IMappingContext context)
        {
            target.role_id = source.role_id;
            target.bus_unit_id = source.bus_unit_id;
            target.vc_role_code = source.vc_role_code;
            target.vc_name = source.vc_name;
        }
        public void Map(BusinessRolesType source, IBusinessRoles target, IMappingContext context)
        {
            target.role_id = source.role_id;
            target.bus_unit_id = source.bus_unit_id;
            target.vc_role_code = source.vc_role_code;
            target.vc_name = source.vc_name;
        }
    }
}
