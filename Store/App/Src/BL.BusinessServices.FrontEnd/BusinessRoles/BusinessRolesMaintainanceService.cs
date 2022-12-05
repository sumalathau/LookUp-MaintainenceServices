using Retalix.Contracts.Generated.Arts.PosLogV6.Source;
using Retalix.Contracts.Generated.BusinessRoles;
using Retalix.Contracts.Generated.Common;
using Retalix.DPOS.SystemIntegrity;
using Retalix.StoreServices.BusinessServices.Common.Services;
using Retalix.StoreServices.Model.Infrastructure.Exceptions;
using Retalix.StoreServices.Model.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retalix.StoreServices.BusinessServices.FrontEnd.BusinessRoles
{
    public class BusinessRolesMaintainanceService : BusinessServiceBase<BusinessRolesMaintenanceRequest, BusinessRolesMaintenanceResponse>
    {
        private readonly IBusinessRolesDao _bussinessRolesDao;
        private readonly IMapper _mapper;
        public string MessageId { get { return Request.Header.MessageId.Value; } }
        public BusinessRolesMaintainanceService(IMapper mapper, IBusinessRolesDao businessrolesDao)
        {
            _mapper = mapper;
            _bussinessRolesDao = businessrolesDao;
        }
        protected override BusinessRolesMaintenanceResponse InternalExecute()
        {
            if (Request == null || Request.BusinessRolesType == null)
                return null;

            switch (Request.BusinessRolesType.Action)
            {
                case ActionTypeCodes.Add:
                    Add(Request);
                    break;
                case ActionTypeCodes.AddOrUpdate:
                    AddOrUpdate(Request);
                    break;
                case ActionTypeCodes.Delete:
                    Delete(Request);
                    break;
            }
            return BuildResponse();
        }
        private void Add(BusinessRolesMaintenanceRequest _businessRolesMaintenanceRequest)
        {
            var mappedLink = _mapper.Map<IBusinessRoles>(_businessRolesMaintenanceRequest.BusinessRolesType);
            var existing = _bussinessRolesDao.Find(_businessRolesMaintenanceRequest.BusinessRolesType.role_id.ToString());
            if (existing == null)
            {
                _bussinessRolesDao.Save(mappedLink);
            }
            else
            {
                throw new BusinessException("Role with this Code already exist", BusinessExceptionErrorCodes.DuplicateApplicationLink);
            }
        }
        private void AddOrUpdate(BusinessRolesMaintenanceRequest _businessRolesMaintenanceRequest)
        {
            //var configuration = GetOrCreateConfigurationContainer(_businessRolesMaintenanceRequest);
            //var mappingContext = _mapper.CreateMappingContext();
            //mappingContext.ContextData.Set(ActionTypeCodes.AddOrUpdate);
            //_mapper.ExecuteMappers(_businessRolesMaintenanceRequest, configuration, mappingContext);
            //_bussinessRolesDao.SaveOrUpdate(configuration);

            var mappedLink = _mapper.Map<IBusinessRoles>(_businessRolesMaintenanceRequest.BusinessRolesType);

            var existing = _bussinessRolesDao.Find(_businessRolesMaintenanceRequest.BusinessRolesType.role_id.ToString());
            if (existing == null)
            {
                _bussinessRolesDao.Save(mappedLink);
            }
            else if (existing != null)
            {
                _bussinessRolesDao.Update(mappedLink);
            }
            else
            {
                throw new BusinessException("The requested Data not found", BusinessExceptionErrorCodes.ApplicationLinkNotFound);
            }
        }
        private void Delete(BusinessRolesMaintenanceRequest _businessRolesMaintenanceRequest)
        {
            var businessRoleToDelete = _bussinessRolesDao.Find(_businessRolesMaintenanceRequest.BusinessRolesType.role_id.ToString());
            if (businessRoleToDelete != null)
            {
                _bussinessRolesDao.Delete(businessRoleToDelete);
            }
            else
            {
                throw new BusinessException("The requested Data not found", BusinessExceptionErrorCodes.ApplicationLinkNotFound);
            }
        }
        private BusinessRolesMaintenanceResponse BuildResponse()
        {
            return new BusinessRolesMaintenanceResponse
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
