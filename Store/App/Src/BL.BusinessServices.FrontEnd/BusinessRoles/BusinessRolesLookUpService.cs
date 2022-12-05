using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Retalix.Contracts.Generated.Arts.PosLogV6.Source;
using Retalix.Contracts.Generated.BusinessRoles;
using Retalix.Contracts.Generated.Common;
using Retalix.DPOS.SystemIntegrity;
using Retalix.StoreServices.BusinessServices.Common.Services;
using Retalix.StoreServices.BusinessServices.FrontEnd.Exceptions;
using Retalix.StoreServices.Model.Infrastructure.Exceptions;
using Retalix.StoreServices.Model.Infrastructure.Service;

namespace Retalix.StoreServices.BusinessServices.FrontEnd.BusinessRoles
{
    class BusinessRolesLookUpService : BusinessServiceBase<BusinessRolesLookupRequest, BusinessRolesLookupResponse>
    {
        private readonly IBusinessRolesDao _bussinessRolesDao;
        private readonly IMapper _mapper;
        public string MessageId { get { return Request.Header.MessageId.Value; } }
        public BusinessRolesType[] Lookup(string name)
        {
            if (name.IsNullOrEmpty())
            {
                var result = _bussinessRolesDao.GetAll()
                                                .Select(a => _mapper.Map<BusinessRolesType>(a))
                                                .ToArray();
                return result;
            }
            else
            {
                var returnedApplicationLink = _bussinessRolesDao.Find(name);
                if (returnedApplicationLink == null)
                    throw new BusinessException("The requested Role not found", BusinessExceptionErrorCodes.ApplicationLinkNotFound);

                var mappedApplicationLink = _mapper.Map<BusinessRolesType>(returnedApplicationLink);
                BusinessRolesType[] result = new BusinessRolesType[] { mappedApplicationLink };
                return result;
            }
        }
        protected override BusinessRolesLookupResponse InternalExecute()
        {
            if (Request == null || Request.role_id.ToString() == null)
                return null;

            Lookup(Request.role_id.ToString());
            return BuildResponse();
        }
        private IEnumerable<IBusinessRoles> GetBusinessRoles(IEnumerable<string> businessroleIds)
        {
            var businessUnits = new List<IBusinessRoles>();
            foreach (var role_id in businessroleIds)
            {
                var businessrole = _bussinessRolesDao.Get(role_id);
                if (null == businessrole)
                {
                    throw new BusinessUnitIdNotFoundException(role_id);
                }
                businessUnits.Add(businessrole);
            }
            return businessUnits;
        }
        private BusinessRolesLookupResponse BuildResponse()
        {
            return new BusinessRolesLookupResponse
            {
                Header = new RetalixCommonHeaderType
                {
                    Response = new RetalixResponseCommonData
                    {
                        ResponseCode = "OK",
                        RequestID = MessageId,
                        ResponseTimestamp = DateTimeService.Instance.Now,
                    }
                }
            };
        }
        public override IDocumentResponse FormatErrorResponse(IDocumentRequest request, Exception exception)
        {
            var returnResponse = new BusinessRolesMaintenanceResponse
            {
                Header = new RetalixCommonHeaderType
                {
                    Response = new RetalixResponseCommonData
                    {
                        ResponseCode = "Rejected",
                        BusinessError = GetContractBusinessError(exception)
                    }
                }
            };

            return new DocumentResponse(returnResponse);
        }
        private static RetalixBusinessErrorCommonData[] GetContractBusinessError(Exception exception)
        {
            var contractError = new RetalixBusinessErrorCommonData
            {
                Description = new DescriptionCommonData
                {
                    Value = exception.Message
                }
            };
            if (exception is BusinessException)
            {
                var code = ((BusinessException)exception).ErrorCode;
                contractError.Code = code;
            }
            return new[] { contractError };
        }

    }
}
