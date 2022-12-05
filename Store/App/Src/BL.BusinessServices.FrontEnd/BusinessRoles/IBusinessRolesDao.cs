using Retalix.StoreServices.Model.Infrastructure.AccessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retalix.StoreServices.BusinessServices.FrontEnd.BusinessRoles
{
   public interface IBusinessRolesDao : IAccessServiceRemotable
    {
        IBusinessRoles Find(string name);
        IBusinessRoles Get(string Key);
        IEnumerable<IBusinessRoles> GetAll();
        void SaveOrUpdate(IBusinessRoles businessroles);
        void Save(IBusinessRoles businessroles);
        void Update(IBusinessRoles businessroles);
        void Delete(IBusinessRoles businessroles);
    }
}
