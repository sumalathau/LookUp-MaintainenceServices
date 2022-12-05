using Castle.Windsor;
using Retalix.StoreServer.BusinessComponents.Finance.Balancing;
using Retalix.StoreServer.BusinessComponents.Finance.EpsReconcile;
using Retalix.StoreServer.CoreConfiguration.CoreComponents.ConfigBuilder;
using Retalix.StoreServices.BusinessComponents.BRMS;
using Retalix.StoreServices.BusinessComponents.CDM;
using Retalix.StoreServices.BusinessComponents.IDM.Security;
using Retalix.StoreServices.BusinessComponents.Infrastructure.BPM;
using Retalix.StoreServices.BusinessComponents.Infrastructure.Events;
using Retalix.StoreServices.BusinessComponents.Infrastructure.Globalization;
using Retalix.StoreServices.BusinessComponents.Infrastructure.Security;
using Retalix.StoreServices.BusinessComponents.Organization.ApplicationLink;
using Retalix.StoreServices.BusinessComponents.Organization.DeviceConfiguration;
using Retalix.StoreServices.BusinessComponents.Organization.Servers;
using Retalix.StoreServices.BusinessComponents.Product.Prices;
using Retalix.StoreServices.BusinessComponents.RefundVoucher;
using Retalix.StoreServices.BusinessComponents.Reporting.Receipt.Configuration;
using Retalix.StoreServices.BusinessComponents.Selling.BPM.Order;
using Retalix.StoreServices.BusinessComponents.Selling.BPM.Order.Builders;
using Retalix.StoreServices.BusinessComponents.Selling.ConditionalRestriction;
using Retalix.StoreServices.BusinessComponents.Selling.CustomerOrders;
using Retalix.StoreServices.BusinessComponents.Selling.SlipReceipt;
using Retalix.StoreServices.BusinessServices.FrontEnd.Application;
using Retalix.StoreServices.BusinessServices.FrontEnd.ApplicationLink;
using Retalix.StoreServices.BusinessServices.FrontEnd.Globalization;
using Retalix.StoreServices.BusinessServices.FrontEnd.Globalization.Builders;
using Retalix.StoreServices.BusinessServices.FrontEnd.RefundVoucher;
using Retalix.StoreServices.BusinessServices.FrontEnd.TrustedAuthority;
using Retalix.StoreServices.Connectivity.BRMS.Cache;
using Retalix.StoreServices.Connectivity.Cache;
using Retalix.StoreServices.Connectivity.Common;
using Retalix.StoreServices.Connectivity.Common.Executers;
using Retalix.StoreServices.Connectivity.Globalization;
using Retalix.StoreServices.Connectivity.Infrastructure;
using Retalix.StoreServices.Connectivity.Infrastructure.Application;
using Retalix.StoreServices.Connectivity.Infrastructure.Application.Appliers;
using Retalix.StoreServices.Connectivity.Infrastructure.Auditing.Dao;
using Retalix.StoreServices.Connectivity.Infrastructure.BPM;
using Retalix.StoreServices.Connectivity.Infrastructure.BusinessLogger;
using Retalix.StoreServices.Connectivity.Infrastructure.RetentionPolicy;
using Retalix.StoreServices.Connectivity.Infrastructure.TrustedAuthority;
using Retalix.StoreServices.Connectivity.Manager;
using Retalix.StoreServices.Connectivity.Manager.Config;
using Retalix.StoreServices.Connectivity.Manager.Config.Hazelcast;
using Retalix.StoreServices.Connectivity.Manager.Config.Redis;
using Retalix.StoreServices.Connectivity.Manager.ModuleMapping;
using Retalix.StoreServices.Connectivity.Organization.ApplicationLink;
using Retalix.StoreServices.Connectivity.Product.Prices.PriceReason.Dao;
using Retalix.StoreServices.Connectivity.Promotion;
using Retalix.StoreServices.Connectivity.RefundVoucher.Dao;
using Retalix.StoreServices.Connectivity.Selling.BPM;
using Retalix.StoreServices.Infrastructure.Components;
using Retalix.StoreServices.Infrastructure.Components.Encryption;
using Retalix.StoreServices.Infrastructure.DataAccess;
using Retalix.StoreServices.Infrastructure.Encryption;
using Retalix.StoreServices.Infrastructure.Serialization;
using Retalix.StoreServices.Model.ApplicationLink;
using Retalix.StoreServices.Model.Customer;
using Retalix.StoreServices.Model.EpsReconcile;
using Retalix.StoreServices.Model.Infrastructure;
using Retalix.StoreServices.Model.Infrastructure.Audit;
using Retalix.StoreServices.Model.Infrastructure.BPM;
using Retalix.StoreServices.Model.Infrastructure.BusinessLogs;
using Retalix.StoreServices.Model.Infrastructure.Cache;
using Retalix.StoreServices.Model.Infrastructure.DataAccess;
using Retalix.StoreServices.Model.Infrastructure.DataMovement;
using Retalix.StoreServices.Model.Infrastructure.DataMovement.Versioning;
using Retalix.StoreServices.Model.Infrastructure.DomainAction;
using Retalix.StoreServices.Model.Infrastructure.Events;
using Retalix.StoreServices.Model.Infrastructure.Globalization;
using Retalix.StoreServices.Model.Infrastructure.Legacy.Globalization;
using Retalix.StoreServices.Model.Infrastructure.RetentionPolicy;
using Retalix.StoreServices.Model.Infrastructure.Security;
using Retalix.StoreServices.Model.Infrastructure.Security.Authentication;
using Retalix.StoreServices.Model.Infrastructure.Security.Identity;
using Retalix.StoreServices.Model.Infrastructure.Service;
using Retalix.StoreServices.Model.Infrastructure.StoreApplication;
using Retalix.StoreServices.Model.Organization.Application;
using Retalix.StoreServices.Model.Price;
using Retalix.StoreServices.Model.Receipts;
using Retalix.StoreServices.Model.RefundVoucher;
using Retalix.StoreServices.Model.Selling.CustomerOrder;
using Retalix.StoreServices.Model.Selling.OrderProcess;
using Retalix.StoreServices.ServiceExecution;
using Retalix.StoreServices.ServiceExecution.Configuration;
using Retalix.StoreServices.ServiceExecution.Handlers;
using Retalix.StoreServices.ServiceExecution.Handlers.AttributeDataExtractors;
using Retalix.StoreServices.ServiceExecution.Handlers.HeaderDataExtractors;
using Retalix.StoreServices.ServiceExecution.Handlers.Security;
using Retalix.StoreServices.ServiceExecution.Handlers.ServiceExecutionHandlers;
using Retalix.StoreServices.ServiceExecution.HeaderTypes;
using Retalix.StoreServices.ServiceExecution.Notifications;
using Retalix.StoreServices.ServiceExecution.Notifications.Mappers;
using Retalix.StoreServices.ServiceExecution.ServiceWrapper;
using StoreNet.Environment;
using StoreNet.Environment.Castle;
using StoreNet.Environment.Mapping;
using System.Configuration;
using Retalix.StoreServices.Model.PromotionService.SecondPE;
using Retalix.StoreServices.Connectivity.Promotion.SecondPE;
using Retalix.StoreServices.BusinessServices.FrontEnd.BusinessRoles;

namespace Retalix.StoreServer.CoreConfiguration.CoreComponents
{
    internal class Infrastructure : CastleConfigurationInstaller
    {
        public override void Install(IComponentInstaller builder)
        {

            builder.Register(Component.For<IImageFormatter>().Named("ImageFormatter").ImplementedBy<ImageFormatter>().LifeStyle.Transient);

            builder.Register(Component.For<ReceiptParameters>().Named("ReceiptParameters").ImplementedBy<ReceiptParameters>().LifeStyle.Transient);

            builder.Register(Component.For<ReceiptDeliveryChannel>().Named("ReceiptDeliveryChannel").ImplementedBy<ReceiptDeliveryChannel>().LifeStyle.Transient);

            builder.Register(Component.For<ISlipPrintManager>().Named("SlipPrintManager").ImplementedBy<SlipPrintManager>().LifeStyle.Singleton);

            builder.Register(Component.For<ISlipTypesPrintSelector>().Named("SlipTypesPrintSelector").ImplementedBy<SlipTypesPrintSelector>().LifeStyle.Singleton);

            builder.Register(Component.For<IAuditDataSerializer>().Named("RestrictionAuditLogSerializer").ImplementedBy<ConditionalRestrictionAuditLogSerializer>().LifeStyle.Transient);

            builder.Register(Component.For<IAuditDataSerializer>().Named("AuditDataDefaultSerializer").ImplementedBy<AuditDataDefaultSerializer>().LifeStyle.Transient);

            builder.Register(Component.For<IAuditLogDao>().Named("AuditLogDao").ImplementedBy<AuditLogDao>().LifeStyle.Singleton);

            builder.RegisterSingleton<ICultureProvider, CultureProvider>();

            builder.RegisterSingleton<ITimeZoneParser, TimeZoneParser>();

            builder.Register(Component.For<IDomainEventsDispatcher>().Named("DomainEventsDispatcher").ImplementedBy<DomainEventsDispatcher>().LifeStyle.Singleton);

            builder.Register(Component.For<ICacheProvider>().Named("MemoryCacheProvider").ImplementedBy<MemoryCacheProvider>().LifeStyle.Singleton);

            builder.Register(Component.For<IServiceSettingsProvider>().ImplementedBy<ServiceSettingsProvider>().LifeStyle.Singleton);

            RegisterBusinessProcessComponents(builder);

            RegisterSearchCriteriaComponents(builder);

            builder.Register(Component.For<ICustomerToPayDataProvider>().Named("OrderBusinessProcessAdapter").ImplementedBy<OrderBusinessProcessAdaptor>().LifeStyle.Transient);

            RegisterSearchEvaluatorStrategyComponents(builder);

            builder.Register(Component.For<IEventHandler<ReportDeviceErrorEvent>>().Named(typeof(ReportDeviceErrorHandler).Name).ImplementedBy<ReportDeviceErrorHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IEventHandler<ReportDeviceErrorSolvedEvent>>().Named(typeof(ReportDeviceErrorSolvedHandler).Name).ImplementedBy<ReportDeviceErrorSolvedHandler>().LifeStyle.Transient);

            builder.Register(Component.For<ILocalizedResource>().Named("IlocalizedResource").ImplementedBy<LocalizedResource>().LifeStyle.Transient);

            builder.Register(Component.For<ILocalizedResourceData>().Named("IlocalizedResourceData").ImplementedBy<LocalizedResourceData>().LifeStyle.Transient);

            RegisterCustomerComponents(builder);

            builder.Register(Component.For<IStateMachineLogic>().Named("StateMachineLogic").ImplementedBy<StateMachineLogic>().LifeStyle.Transient);

            RegisterServicesInfrastructureComponents(builder);

            builder.RegisterSingleton<SessionFactoryProvider>();
            builder.RegisterSingleton<IDatabaseModuleMapping, ConfigDatabaseModuleMapping>();
            builder.Register(Component.For<IInternalServiceExecuter>().Named("InternalServiceExecuter").ImplementedBy<InternalServiceExecuter>().LifeStyle.Transient);
            builder.RegisterSingleton<ISessionProvider, SessionProvider>();
            builder.RegisterSingleton<SecurityIdentifierFactory>();
            builder.RegisterSingleton<IStoreNetRequestScopeProvider, StoreNetRequestScopeProvider>();

            builder.RegisterSingleton<IUnitOfWorkCommittedObserver, ApplicationStateUnitOfWorkObserver>("ApplicationStateUnitOfWorkObserver");
            builder.RegisterSingleton<IUnitOfWorkCommittedObserver, AccountBalanceIdCachingObserver>("AccountBalanceIdCachingObserver");

            if (ConfigurationManager.AppSettings["UseRedisCache"] != null &&
                ConfigurationManager.AppSettings["UseRedisCache"].ToLower() == "true")
            {
                builder.RegisterSingleton<IUnitOfWorkCommittedObserver, PromotionRedisCachingObserver>(
                    "PromotionRedisCachingObserver");
                builder.RegisterSingleton<IStreamObserver, PromotionStreamObserver>("PromotionStreamObserver");

                builder.RegisterSingleton<IUnitOfWorkCommittedObserver, BusinessRuleRedisCachingObserver>("BusinessRuleRedisCachingObserver");
                builder.RegisterSingleton<IStreamObserver, BusinessRuleStreamObserver>("BusinessRuleStreamObserver");

                builder.RegisterSingleton<IUnitOfWorkCommittedObserver, ConditionalRestrictionRedisCachingObserver>("ConditionalRestrictionRedisCachingObserver");
                builder.RegisterSingleton<IStreamObserver, ConditionalRestrictionStreamObserver>("ConditionalRestrictionStreamObserver");
            }
            else
            {
                builder.RegisterSingleton<IUnitOfWorkCommittedObserver, PromotionCachingObserver>(
                    "PromotionCachingObserver");

                builder.RegisterSingleton<IUnitOfWorkCommittedObserver, BusinessRuleCachingObserver>("BusinessRuleCachingObserver");
                builder.RegisterSingleton<IUnitOfWorkCommittedObserver, ConditionalRestrictionCachingObserver>("ConditionalRestrictionCachingObserver");
            }

            builder.RegisterSingleton<ISessionWrapper, RedisSessionWrapper>("RedisSessionWrapper");
            builder.RegisterSingleton<IDataCompressor, GZipCompressor>("GZip");
            builder.RegisterSingleton<IDataCompressor, SnappyCompressor>("Snappy");
            builder.RegisterSingleton<IDataCompressor, LZ4Compressor>("LZ4");
            builder.RegisterSingleton<IExtensionNotifier, ExtensionNotifier>("ExtensionNotifier");
            builder.RegisterSingleton<ITransactionalRedisSessionWrapper, RedisSessionWrapper>("RedisTransactionalSessionWrapper");
            builder.RegisterSingleton<IDbCommandExecuter, DbCommandExecuter>();
            builder.RegisterSingleton<IDbConnectionExecuter, DbConnectionExecuter>();
            builder.RegisterInstance<IServicesBusinessActivityProvider>(new ServiceBusinessActivitiesProvider(builder.GetRegistrationFacility<IWindsorContainer>()));
            builder.RegisterSingleton<IServiceNotificationContext, ServiceNotificationContext>();

            builder.RegisterSingleton<ITrustedAuthorityDao, ITrustedAuthorityProvider, TrustedAuthorityDao>();
            builder.RegisterService<TrustedAuthorityRegistrationService>();
            builder.RegisterService<TrustedAuthorityRevokeService>();
            builder.RegisterFactoryForDefaultConstructor<ITrustedAuthority, TrustedAuthority>();
            builder.RegisterSingleton<IIdentityProvider, ServerIdentityProvider>();
            builder.RegisterFactory<IAuthenticatedClaim>(r => new AuthenticatedClaim(r.Resolve<ITrustedAuthorityProvider>(), r.Resolve<IAccessServicesIdentityProvider>()));
            builder.RegisterSingleton<IAccessServicesIdentityProvider, AccessServicesIdentityProvider>();
            builder.RegisterMapper<AuthenticatedClaimMapper>();

            builder.RegisterSingleton<IResXBuilder, ResXBuilder>();
            builder.Register(Component.For<IResourceRequestReader>().ImplementedBy<ResourceRequestReader>().LifeStyle.Transient);

            builder.RegisterSingleton<TrustedAuthorityMovableDao>();
            builder.RegisterSingleton<IMovableServicesResolver, TrustedAuthorityMovableResolver>("TrustedAuthority");
            builder.RegisterSingleton<IEntityToDtoConverter<ITrustedAuthority>, TrustedAuthorityConverter>();
            builder.Register(Component.For<IJsonDotNetSerializer>().Named("JsonDotNetSeralizer").ImplementedBy<JsonDotNetSerializer>().LifeStyle.Singleton);
            builder.Register(Component.For<IBsonDotNetSeralizer>().Named("BsonDotNetSeralizer").ImplementedBy<JsonDotNetSerializer>().LifeStyle.Singleton);
            builder.Register(Component.For<IProtobufSerialization>().Named("ProtobufSerializer").ImplementedBy<ProtobufSerializer>().LifeStyle.Singleton);

            builder.Register(Component.For<IEncryptor>().Named("Encryptor").ImplementedBy<AesEncryptor>().LifeStyle.Singleton);
            builder.Register(Component.For<IEncryptionKeyProvider>().Named("EncryptionKeyProvider").ImplementedBy<ConfigurationEncryptionKeyProvider>().LifeStyle.Singleton);
            builder.Register(Component.For<IRefreshCacheService>().Named("RefreshCacheService").ImplementedBy<RefreshCacheService>().LifeStyle.Transient);

            builder.Register(Component.For<StoreServices.Infrastructure.Cache.ICacheProvider>().ImplementedBy<StoreServices.Model.Cache.CacheProvider>().LifeStyle.Singleton);

            builder.RegisterSingleton<IServerAuthority, ServerAuthority>();
            builder.Register(Component.For<IRefundVoucher>().ImplementedBy<RefundVoucher>().LifeStyle.Transient);
            builder.RegisterService<RefundVoucherLookupService>();
            builder.RegisterService<RefundVoucherMaintenanceService>();
            builder.RegisterSingleton<IRefundVoucherDao, RefundVoucherDao>();
            builder.RegisterMapper<RefundVoucherMapper>();
            builder.RegisterFactoryForDefaultConstructor<IRefundVoucherDetails, RefundVoucherDetails>();
            builder.RegisterFactoryForDefaultConstructor<IRefundVoucherCriteriaSearch, RefundVoucherCriteriaSearch>();

            builder.Register(Component.For<IApplicationLink>().ImplementedBy<ApplicationLink>().LifeStyle.Transient);
            builder.RegisterService<ApplicationLinkMaintenanceService>();
            builder.RegisterService<ApplicationLinkLookupService>();
            builder.RegisterSingleton<IApplicationLinkDao, ApplicationLinkDao>();
            builder.RegisterMapper<ApplicationLinkMapper>();
            // BusinessRoles 
            builder.Register(Component.For<IBusinessRoles>().ImplementedBy<BusinessRoles>().LifeStyle.Transient);
            //builder.RegisterService<BusinessRolesMaintainanceService>();
            builder.RegisterMapper<BusinessRolesMapper>();
            builder.RegisterSingleton<IBusinessRolesDao, BusinessRolesDao>();
            //
            builder.RegisterSingleton<IPriceReasonDao, PriceReasonDao>();

            builder.RegisterSingleton<ITranReferenceDataDao, TranReferenceDataDao>();
            //application

            builder.RegisterFactoryForDefaultConstructor<IApplication, StoreServices.BusinessComponents.Organization.Application.Application>();
            builder.RegisterSingleton<IApplicationDao, IIdentityProvider, ApplicationDao>();
            builder.RegisterSingleton<ApplicationMovableDao>();
            builder.RegisterSingleton<IMovableServicesResolver, ApplicationMovableResolver>("Application");
            builder.RegisterSingleton<IEntityToDtoConverter<IApplication>, ApplicationMovableConverter>();

            builder.RegisterQuery<ApplicationDefaultQuery>();
            builder.RegisterQueryCriterionApplier<ApplicationIdCriterionApplier>();

            builder.RegisterMapper<ApplicationMapper>();
            builder.RegisterService<ApplicationMaintenanceService>();
            builder.RegisterService<ApplicationLookupService>();

            builder.RegisterService<CreateSessionService>();
            builder.RegisterService<GetClaimService>();

            builder.RegisterSingleton<IBulkPessimisticExecutionStrategy, DefaultBulkPessimisticExecutionStrategy>();

            builder.RegisterSingleton<DomainAction>();

            builder.RegisterFactoryForDefaultConstructor<IManualEpsReconcileDto, ManualEpsReconcileDto>();
            builder.RegisterFactoryForDefaultConstructor<IEpsOperationalControlLog, EpsOperationalControlLog>();
        }

        private static void RegisterCustomerComponents(IComponentInstaller builder)
        {
            builder.Register(
                Component
                    .For<ICustomerOrder>()
                    .Named("CustomerOrder")
                    .ImplementedBy<CustomerOrder>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<ICustomer>()
                    .Named("ICustomer")
                    .ImplementedBy<AnonymousCustomer>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<ICustomerOrderProxyCentralPrice>()
                    .Named("ICustomerOrderProxyCentralPrice")
                    .ImplementedBy<CustomerOrderProxyCentralPrice>()
                    .LifeStyle.Transient);

            builder.Register(
               Component
                   .For<ICustomerOrderProxyCentralPriceProvider>()
                   .Named("ICustomerOrderProxyCentralPriceProvider")
                   .ImplementedBy<CustomerOrderProxyCentralPriceProvider>()
                   .LifeStyle.Transient);

            builder.RegisterSingleton<IRetentionPolicyDao, RetentionPolicyDao>();

            builder.RegisterSingleton<IRetentionPolicyConfigurationDao, RetentionPolicyConfigurationDao>();

        }

        private static void RegisterSearchCriteriaComponents(IComponentInstaller builder)
        {
            builder.Register(
                Component
                    .For<ICustomerToPayOrderSearchCriteria>()
                    .Named("CustomerToPayOrderSearchCriteria")
                    .ImplementedBy<DefaultCustomerToPayOrderSearchCriteria>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<ISuspendOrderSearchCriteria>()
                    .Named("SuspendOrderSearchCriteria")
                    .ImplementedBy<DefaultSuspendOrderSearchCriteria>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IOrderSearchCriteria>()
                    .Named("OrderSearchCriteria")
                    .ImplementedBy<OrderSearchCriteria>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IClickAndCollectSearchCriteria>()
                    .Named("CustomerOrderProcessSearchCriteria")
                    .ImplementedBy<ClickAndCollectSearchCriteria>()
                    .LifeStyle.Transient);
        }

        private static void RegisterSearchEvaluatorStrategyComponents(IComponentInstaller builder)
        {
            builder.Register(
                Component
                    .For<ISearchEvaluatorStrategy<ICustomerToPayOrderSearchCriteria>>()
                    .Named("DefaultOrderSearchEvaluatorStrategy")
                    .ImplementedBy<DefaultOrderSearchEvaluatorStrategy>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<ISearchEvaluatorStrategy<IOrderSearchCriteria>>()
                    .Named("OrderSearchEvaluatorStrategy")
                    .ImplementedBy<OrderSearchEvaluatorStrategy>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<ISearchEvaluatorStrategy<ISuspendOrderSearchCriteria>>()
                    .ImplementedBy<DefaultSuspendedOrderSearchEvaluatorStrategy>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<ISearchEvaluatorStrategy<IClickAndCollectSearchCriteria>>()
                    .ImplementedBy<CustomerOrderProcessEvaluatorStrategy>()
                    .LifeStyle.Transient);
        }

        private static void RegisterServicesInfrastructureComponents(IComponentInstaller builder)
        {
            builder.Register(Component.For<IMapper>().Named("DefaultMapper").ImplementedBy<Mapper>().LifeStyle.Singleton);
            builder.RegisterSingleton<IContractMapper, ContractMapper>();

            builder.RegisterSingleton<IFactory, WindsorFactory>();

            builder.RegisterSingleton<IServiceFactory, ServiceFactory>();

            builder.RegisterService<ServiceWrapperService>();

            builder.RegisterMapper<ServiceWrapperContractMapper>();
            builder.RegisterMapper<ObsoleteServiceWrapperContractMapper>();

            builder.RegisterMapper<ARTSCommonHeaderTypeMapper>();
            builder.RegisterMapper<RetalixCommonHeaderTypeMapper>();
            builder.RegisterMapper<ExceptionToBusinessErrorMapper>();
            builder.RegisterMapper<XmlExceptionToBusinessErrorMapper>();
            builder.RegisterMapper<OperationExecutionResultsToBusinessErrorMapper>();
            builder.RegisterMapper<OperationExecutionResultsToRetalixBusinessErrorMapper>();
            builder.RegisterMapper<ReportableEventToBusinessErrorMapper>();
            builder.RegisterMapper<UnknownElementWarningToBusinessErrorMapper>();
            builder.RegisterMapper<UnknownAttributeWarningToBusinessErrorMapper>();

            builder.RegisterMapper<RetalixCommonHeaderTypeMessageIdExtractor>();
            builder.RegisterMapper<RetalixCommonHeaderTypeReferenceIdExtractor>();
            builder.RegisterMapper<RetalixCommonHeaderTypeBulkSettingsExtractor>();
            builder.RegisterMapper<ARTSCommonHeaderTypeMessageIdExtractor>();
            builder.RegisterMapper<ARTSCommonHeaderTypeReferenceIdExtractor>();
            builder.RegisterMapper<ARTSCommonHeaderTypeBulkSettingsExtractor>();
            builder.RegisterMapper<ARTSCommonHeaderTypeLocationExtractor>();

            builder.RegisterMapper<MaintenanceServiceAttributeDataExtractor>();
            builder.RegisterMapper<ObsoleteAttributeDataExtractor>();

            builder.RegisterMapper<NotificationContainerToArtsCommonHeaderType>();
            builder.RegisterMapper<NotificationContainerToRetalixCommonHeaderType>();

            builder.RegisterMapper<RetalixCommonHeaderTypeCredentialsMapper>();

            builder.RegisterMapper<ARTSCommonHeaderTypeV4Mapper>();
            builder.RegisterMapper<OperationExecutionResultsToRetalixBusinessErrorV4Mapper>();

            //builder.RegisterMapper<ArtsCommonHeaderTypeBulkSettingsV4Extractor>();
            builder.RegisterMapper<ArtsCommonHeaderTypeLocationV4Extractor>();
            builder.RegisterMapper<ArtsCommonHeaderTypeMessageIdV4Extractor>();
            builder.RegisterMapper<ArtsCommonHeaderTypeReferenceIdV4Extractor>();
            builder.RegisterMapper<PriceReasonMapper>();

        }

        private static void RegisterBusinessProcessComponents(IComponentInstaller builder)
        {
            builder.Register(
                Component
                    .For<IBusinessProcess>()
                    .Named("BusinessProcess")
                    .ImplementedBy<BusinessProcess>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBusinessProcess>()
                    .Named("OrderProcess")
                    .ImplementedBy<OrderProcess>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBusinessProcess>()
                    .Named(OrderProcessNames.UnpaidOrderProcessName)
                    .ImplementedBy<UnpaidOrderProcess>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBusinessProcess>()
                    .Named(OrderProcessNames.ClickAndCollectOrderProcessName)
                    .ImplementedBy<ClickAndCollectOrderProcess>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBusinessProcess>()
                    .Named(OrderProcessNames.DriveOffOrderProcessName)
                    .ImplementedBy<DriveOffOrderProcess>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBusinessProcess>()
                    .Named(SuspendOrderBusinessProcessMetadata.SuspendOrderProcessName)
                    .ImplementedBy<SuspendOrderProcess>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBusinessProcess>()
                    .Named(OrderProcessNames.PickUpOrderProcessName)
                    .ImplementedBy<PickUpOrderProcess>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBusinessProcess>()
                    .Named(OrderProcessNames.SuspendSelfScanOrderProcessName)
                    .ImplementedBy<SuspendResumeSelfScanOrderProcess>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBusinessProcess>()
                    .Named(OrderProcessNames.SuspendTabOrderProcessName)
                    .ImplementedBy<SuspendResumeTabOrderProcess>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBuilder<IBusinessProcess>>()
                    .Named(OrderProcessBuilders.SuspendResumeTransactionProcessBuilderName)
                    .ImplementedBy<SuspendResumeTransactionProcessBuilder>()
                    .LifeStyle.Transient);

            builder.Register(
                 Component
                    .For<IBuilder<IBusinessProcess>>()
                    .Named(SuspendResumeSelfScanTransactionProcessBuilder.BuilderRegistrationName)
                    .ImplementedBy<SuspendResumeSelfScanTransactionProcessBuilder>()
                    .LifeStyle.Transient);

            builder.Register(
                 Component
                    .For<IBuilder<IBusinessProcess>>()
                    .Named(SuspendResumeTabTransactionProcessBuilder.BuilderRegistrationName)
                    .ImplementedBy<SuspendResumeTabTransactionProcessBuilder>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBuilder<IBusinessProcess>>()
                    .Named("UnpaidOrderProcessBuilder")
                    .ImplementedBy<UnpaidOrderProcessBuilder>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBuilder<IBusinessProcess>>()
                    .Named("DriveOffOrderProcessBuilder")
                    .ImplementedBy<DriveOffOrderProcessBuilder>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBuilder<IBusinessProcess>>()
                    .Named("ClickAndCollectOrderProcessBuilder")
                    .ImplementedBy<ECommerceBusinessProcessBuilder>()
                    .LifeStyle.Transient);

            builder.Register(
                Component
                    .For<IBuilder<IBusinessProcess>>()
                    .Named(OrderProcessBuilders.PickUpOrderProcessBuilderName)
                    .ImplementedBy<PickUpOrderProcessBuilder>()
                    .LifeStyle.Transient);

            if (ConfigurationManager.AppSettings["TransactionRepository"] != null && ConfigurationManager.AppSettings["TransactionRepository"].ToLower() == "distributedcache")
            {
                builder.RegisterSingleton<IBusinessProcessDao, BusinessProcessHazelcastDao>();
                builder.Register(Component.For<IUnitOfWorkCommittedObserver>().ImplementedBy<HazelcastSessionWrapper>().LifeStyle.Singleton);
                builder.Register(Component.For<IUnitOfWorkFailedObserver>().ImplementedBy<HazelcastSessionWrapper>().LifeStyle.Singleton);
            }
            else if (ConfigurationManager.AppSettings["TransactionRepository"] != null && ConfigurationManager.AppSettings["TransactionRepository"].ToLower() == "distributedcacheredis")
            {
                builder.RegisterSingleton<IBusinessProcessDao, BusinessProcessRedisDao>();
                builder.Register(Component.For<IUnitOfWorkCompleteObserver>().ImplementedBy<RedisSessionWrapper>().LifeStyle.Singleton);
                builder.Register(Component.For<IUnitOfWorkFailedObserver>().ImplementedBy<RedisSessionWrapper>().LifeStyle.Singleton);
            }
            else
                builder.RegisterSingleton<IBusinessProcessDao, BusinessProcessDao>();


            builder.Register(
               Component.For<IBusinessWarningLogHandler>()
              .Named("BusinessWarningLogHandler")
              .ImplementedBy<BusinessWarningLogHandler>()
              .LifeStyle.Transient);

        }
    }
}
