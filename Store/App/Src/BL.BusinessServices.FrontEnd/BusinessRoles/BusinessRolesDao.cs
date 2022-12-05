using NHibernate;
using NHibernate.Criterion;
using Retalix.StoreServices.Model.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retalix.StoreServices.BusinessServices.FrontEnd.BusinessRoles
{
    class BusinessRolesDao : IBusinessRolesDao
    {
        private readonly ISessionProvider<ISession> _sessionProvider;
        public BusinessRolesDao(ISessionProvider<ISession> sessionProvider, IDataAccessManager dataAccessManager)
        {
            _sessionProvider = sessionProvider;
        }
        private ISession Session
        {
            get { return _sessionProvider.Session; }
        }
        public IBusinessRoles Find(string name)
        {
            var query = Session.QueryOver<IBusinessRoles>().Where(u => u.vc_role_code == name);
            var returnedApplicationLink = query.SingleOrDefault();
            return returnedApplicationLink;
        }
        public IBusinessRoles Get(string Key)
        {
            var criteria = Session.CreateCriteria(typeof(IBusinessRoles)).Add(Restrictions.Eq("role_id", Key));
            var result = criteria.List<IBusinessRoles>();
            return result.FirstOrDefault();
        }
        public IEnumerable<IBusinessRoles> GetAll()
        {
            List<IBusinessRoles> records = Session.Query<IBusinessRoles>().ToList();
            return records;
        }
        public IBusinessRoles GetItemById(string role_id, string bus_unit_Id)
        {
            //Log.Info(message => message("BundleItemDao.GetBundleItemById : entered"));

            return Session.CreateCriteria(typeof(IBusinessRoles))
                .Add(Restrictions.Eq("role_id", role_id))
                .Add(Restrictions.Eq("BusinessUnitId", bus_unit_Id)).UniqueResult<IBusinessRoles>();
        }
        public void SaveOrUpdate(IBusinessRoles BusinessRole)
        {
            var ExitingBusinessRole = Get(BusinessRole.role_id.ToString());
            if (ExitingBusinessRole != null)
            {
                ExitingBusinessRole.vc_role_code = BusinessRole.vc_role_code;
                ExitingBusinessRole.vc_name = BusinessRole.vc_name;
                ExitingBusinessRole.bus_unit_id = BusinessRole.bus_unit_id;
            }
            else
            {
                ExitingBusinessRole = BusinessRole;
            }
            Session.SaveOrUpdate("BusinessRoles", ExitingBusinessRole);
            Session.Flush();
        }
        public void Save(IBusinessRoles BusinessRole)
        {
            Session.Save(BusinessRole);
            Session.Flush();
        }
        public void Update(IBusinessRoles BusinessRole)
        {
            var _BusinessRole = Find(BusinessRole.vc_role_code);
            if (_BusinessRole == null)
                Session.SaveOrUpdate(_BusinessRole);
            else
                Session.Merge(BusinessRole);
            Session.Flush();
        }
        public void Delete(IBusinessRoles BusinessRole)
        {
            Session.Delete(BusinessRole);
        }
    }
}
