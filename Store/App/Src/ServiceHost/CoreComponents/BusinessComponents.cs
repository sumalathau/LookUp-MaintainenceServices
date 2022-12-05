using Castle.Windsor;
using RabbitMQ.Client;
using Retalix.Contract.R1.CustomerOrder.Model;
using Retalix.Contracts.Generated.Arts.PosLogV4;
using Retalix.Contracts.Generated.IDM;
using Retalix.Contracts.Generated.ProductDomain.Price;
using Retalix.DPOS.DataAccess.ForeignCurrencyExchangeRate;
using Retalix.DPOS.SystemIntegrity;
using Retalix.StoreServer.BC.PaymentTransaction;
using Retalix.StoreServer.BusinessComponents.BusinessActivity;
using Retalix.StoreServer.BusinessComponents.Finance;
using Retalix.StoreServer.BusinessComponents.Finance.Account;
using Retalix.StoreServer.BusinessComponents.Finance.Account.DMS;
using Retalix.StoreServer.BusinessComponents.Finance.Account.Profile;
using Retalix.StoreServer.BusinessComponents.Finance.Activity;
using Retalix.StoreServer.BusinessComponents.Finance.Aggregators;
using Retalix.StoreServer.BusinessComponents.Finance.ArtsPosLog60;
using Retalix.StoreServer.BusinessComponents.Finance.ArtsPosLog60.Declaration;
using Retalix.StoreServer.BusinessComponents.Finance.ArtsPosLog60.FundTransfer;
using Retalix.StoreServer.BusinessComponents.Finance.ArtsPosLog60.FundTransfer.Readers;
using Retalix.StoreServer.BusinessComponents.Finance.ArtsPosLog60.StoreBalancePeriod;
using Retalix.StoreServer.BusinessComponents.Finance.Balancing;
using Retalix.StoreServer.BusinessComponents.Finance.Balancing.ApprovalValidators;
using Retalix.StoreServer.BusinessComponents.Finance.Balancing.Handlers;
using Retalix.StoreServer.BusinessComponents.Finance.Balancing.SettlementValidators;
using Retalix.StoreServer.BusinessComponents.Finance.Balancing.TransactionAccumulators;
using Retalix.StoreServer.BusinessComponents.Finance.BusinessRules;
using Retalix.StoreServer.BusinessComponents.Finance.Configuration;
using Retalix.StoreServer.BusinessComponents.Finance.Configuration.Strategies;
using Retalix.StoreServer.BusinessComponents.Finance.Declaration;
using Retalix.StoreServer.BusinessComponents.Finance.FundTransfer;
using Retalix.StoreServer.BusinessComponents.Finance.FundTransfer.Adjustment;
using Retalix.StoreServer.BusinessComponents.Finance.FundTransfer.Adjustment.Validators;
using Retalix.StoreServer.BusinessComponents.Finance.OpenDrawer;
using Retalix.StoreServer.BusinessComponents.Finance.Period;
using Retalix.StoreServer.BusinessComponents.Finance.Receipt;
using Retalix.StoreServer.CoreConfiguration.CoreComponents.ConfigBuilder;
using Retalix.StoreServer.DataAccess.ReasonCode;
using Retalix.StoreServices.BusinessComponents.Alert;
using Retalix.StoreServices.BusinessComponents.Alert.Message;
using Retalix.StoreServices.BusinessComponents.BRMS;
using Retalix.StoreServices.BusinessComponents.CDM;
using Retalix.StoreServices.BusinessComponents.CDM.Agreements;
using Retalix.StoreServices.BusinessComponents.CDM.Legacy;
using Retalix.StoreServices.BusinessComponents.CDM.Legacy.Message;
using Retalix.StoreServices.BusinessComponents.CDM.ShoppingList;
using Retalix.StoreServices.BusinessComponents.Customer.OnlineAccount;
using Retalix.StoreServices.BusinessComponents.Customer.RetailTransactionLog;
using Retalix.StoreServices.BusinessComponents.Customer.SavingAccount;
using Retalix.StoreServices.BusinessComponents.Customer.SavingAccount.Provider;
using Retalix.StoreServices.BusinessComponents.DMS;
using Retalix.StoreServices.BusinessComponents.DMS.Apply;
using Retalix.StoreServices.BusinessComponents.DMS.ColdStart.States;
using Retalix.StoreServices.BusinessComponents.DMS.DataCompression;
using Retalix.StoreServices.BusinessComponents.DMS.Filter;
using Retalix.StoreServices.BusinessComponents.DMS.Heartbeat;
using Retalix.StoreServices.BusinessComponents.DMS.MessageHandlers;
using Retalix.StoreServices.BusinessComponents.DMS.Monitoring;
using Retalix.StoreServices.BusinessComponents.DMS.PayloadView;
using Retalix.StoreServices.BusinessComponents.DMS.PhysicalToLogical;
using Retalix.StoreServices.BusinessComponents.DMS.Rabbit;
using Retalix.StoreServices.BusinessComponents.DMS.Rabbit.Management;
using Retalix.StoreServices.BusinessComponents.DMS.Rabbit.TransportationConsumers.Download;
using Retalix.StoreServices.BusinessComponents.DMS.Routing;
using Retalix.StoreServices.BusinessComponents.DMS.ServerHealth;
using Retalix.StoreServices.BusinessComponents.DMS.SingularService;
using Retalix.StoreServices.BusinessComponents.DMS.UploadProcessingStatus;
using Retalix.StoreServices.BusinessComponents.DMS.Versioning;
using Retalix.StoreServices.BusinessComponents.Document.Archive;
using Retalix.StoreServices.BusinessComponents.Document.DMS;
using Retalix.StoreServices.BusinessComponents.Document.PerformanceCounters;
using Retalix.StoreServices.BusinessComponents.Document.Retransmit;
using Retalix.StoreServices.BusinessComponents.EOD;
using Retalix.StoreServices.BusinessComponents.EOD.Configuration;
using Retalix.StoreServices.BusinessComponents.EOD.Validators;
using Retalix.StoreServices.BusinessComponents.IDM;
using Retalix.StoreServices.BusinessComponents.IDM.Authentication;
using Retalix.StoreServices.BusinessComponents.IDM.Authentication.Credentials;
using Retalix.StoreServices.BusinessComponents.IDM.DTO;
using Retalix.StoreServices.BusinessComponents.IDM.Encryption;
using Retalix.StoreServices.BusinessComponents.IDM.Handlers;
using Retalix.StoreServices.BusinessComponents.IDM.Identity;
using Retalix.StoreServices.BusinessComponents.IDM.Receipt;
using Retalix.StoreServices.BusinessComponents.IDM.UserConfigurationPermissions;
using Retalix.StoreServices.BusinessComponents.IDM.UserValidators;
using Retalix.StoreServices.BusinessComponents.Infrastructure.BusinessConfiguration;
using Retalix.StoreServices.BusinessComponents.Infrastructure.Counter;
using Retalix.StoreServices.BusinessComponents.Infrastructure.DataIntegrity;
using Retalix.StoreServices.BusinessComponents.Infrastructure.DataPattern;
using Retalix.StoreServices.BusinessComponents.Infrastructure.DataPattern.Factory;
using Retalix.StoreServices.BusinessComponents.Infrastructure.DataPattern.Recognizer;
using Retalix.StoreServices.BusinessComponents.Infrastructure.DataProtection;
using Retalix.StoreServices.BusinessComponents.Infrastructure.ETW;
using Retalix.StoreServices.BusinessComponents.Infrastructure.Globalization;
using Retalix.StoreServices.BusinessComponents.Infrastructure.LocalizedTexts;
using Retalix.StoreServices.BusinessComponents.Infrastructure.PerformanceCounters;
using Retalix.StoreServices.BusinessComponents.Infrastructure.RetentionPolicy;
using Retalix.StoreServices.BusinessComponents.Infrastructure.RetentionPolicy.Scheduler;
using Retalix.StoreServices.BusinessComponents.Infrastructure.RTIAuditTrail;
using Retalix.StoreServices.BusinessComponents.Infrastructure.Security;
using Retalix.StoreServices.BusinessComponents.Message;
using Retalix.StoreServices.BusinessComponents.ODS.DAO;
using Retalix.StoreServices.BusinessComponents.ODS.Document;
using Retalix.StoreServices.BusinessComponents.ODS.Document.Doc2Ods;
using Retalix.StoreServices.BusinessComponents.ODS.Document.Extractors;
using Retalix.StoreServices.BusinessComponents.ODS.Job;
using Retalix.StoreServices.BusinessComponents.Organization;
using Retalix.StoreServices.BusinessComponents.Organization.DeviceConfiguration;
using Retalix.StoreServices.BusinessComponents.Organization.ExtraPriceCalculationPolicy;
using Retalix.StoreServices.BusinessComponents.Organization.Notification;
using Retalix.StoreServices.BusinessComponents.Organization.OpenCloseDay;
using Retalix.StoreServices.BusinessComponents.Organization.RetailSegment;
using Retalix.StoreServices.BusinessComponents.Organization.Servers;
using Retalix.StoreServices.BusinessComponents.Organization.StoredCredentials;
using Retalix.StoreServices.BusinessComponents.Organization.TimeAvailabilityTerm;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication.Branding.FileSizeConfiguration;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication.Branding.Images;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication.KeyboardConfiguration;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication.Kit;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication.OwnBagConfiguration;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication.SecurityScaleConfiguration;
using Retalix.StoreServices.BusinessComponents.Organization.VenueShift;
using Retalix.StoreServices.BusinessComponents.Organization.VerificationMark;
using Retalix.StoreServices.BusinessComponents.Product.Legacy.Item;
using Retalix.StoreServices.BusinessComponents.Product.Legacy.Item.ConsumableGroup;
using Retalix.StoreServices.BusinessComponents.Product.Legacy.Item.Product;
using Retalix.StoreServices.BusinessComponents.Product.StoreRange;
using Retalix.StoreServices.BusinessComponents.Promotion.Advertisement;
using Retalix.StoreServices.BusinessComponents.Promotion.Coupons;
using Retalix.StoreServices.BusinessComponents.Promotion.Coupons.BusinessRules.CouponLimits;
using Retalix.StoreServices.BusinessComponents.Promotion.Coupons.BusinessRules.CouponOverride;
using Retalix.StoreServices.BusinessComponents.Promotion.CustomerOrder.BuisnessRules;
using Retalix.StoreServices.BusinessComponents.Promotion.CustomerOrder.Rewards.Message;
using Retalix.StoreServices.BusinessComponents.Reporting;
using Retalix.StoreServices.BusinessComponents.Reporting.Endorsement.Factory;
using Retalix.StoreServices.BusinessComponents.Reporting.Indicator;
using Retalix.StoreServices.BusinessComponents.Reporting.Receipt.Configuration;
using Retalix.StoreServices.BusinessComponents.Reporting.Receipt.Engine;
using Retalix.StoreServices.BusinessComponents.Reporting.Receipt.Layout;
using Retalix.StoreServices.BusinessComponents.Reporting.Receipt.PrintableDataBuilder;
using Retalix.StoreServices.BusinessComponents.Selling;
using Retalix.StoreServices.BusinessComponents.Selling.ActiveTransaction.ActiveTransactionAutoVoidAbandonJob;
using Retalix.StoreServices.BusinessComponents.Selling.ActiveTransaction.ActiveTransactionSuspendJob;
using Retalix.StoreServices.BusinessComponents.Selling.AutoLoad;
using Retalix.StoreServices.BusinessComponents.Selling.Behaviors.OnlineItem;
using Retalix.StoreServices.BusinessComponents.Selling.BPM.Order;
using Retalix.StoreServices.BusinessComponents.Selling.BRM;
using Retalix.StoreServices.BusinessComponents.Selling.BRM.ExpressionAdaptor;
using Retalix.StoreServices.BusinessComponents.Selling.ColdstartActivity;
using Retalix.StoreServices.BusinessComponents.Selling.ConditionalRestriction;
using Retalix.StoreServices.BusinessComponents.Selling.ConditionalRestriction.Interventions;
using Retalix.StoreServices.BusinessComponents.Selling.CustomerOrders;
using Retalix.StoreServices.BusinessComponents.Selling.CustomerOrders.FormData;
using Retalix.StoreServices.BusinessComponents.Selling.CustomerOrders.Lines;
using Retalix.StoreServices.BusinessComponents.Selling.CustomerOrders.Lines.Kit;
using Retalix.StoreServices.BusinessComponents.Selling.Drawer;
using Retalix.StoreServices.BusinessComponents.Selling.Drawer.OpenDrawerOnCashOfficeActivities;
using Retalix.StoreServices.BusinessComponents.Selling.GiftReceipt;
using Retalix.StoreServices.BusinessComponents.Selling.Legacy.Department;
using Retalix.StoreServices.BusinessComponents.Selling.Legacy.RetailTransaction.DataPattern;
using Retalix.StoreServices.BusinessComponents.Selling.Legacy.RetailTransaction.Reporting.Builders;
using Retalix.StoreServices.BusinessComponents.Selling.Legacy.RetailTransaction.Reporting.PrintableSourceDataRetrievers;
using Retalix.StoreServices.BusinessComponents.Selling.Legacy.RetailTransaction.ScoInterventionConfiguration.Maintenance;
using Retalix.StoreServices.BusinessComponents.Selling.OrderCalculation;
using Retalix.StoreServices.BusinessComponents.Selling.Payments;
using Retalix.StoreServices.BusinessComponents.Selling.Payments.Change;
using Retalix.StoreServices.BusinessComponents.Selling.Payments.RestrictionRule;
using Retalix.StoreServices.BusinessComponents.Selling.ProductAvailability;
using Retalix.StoreServices.BusinessComponents.Selling.ReasonCode;
using Retalix.StoreServices.BusinessComponents.Selling.Rescan.PredictiveRescan;
using Retalix.StoreServices.BusinessComponents.Selling.RetailTransactionLog;
using Retalix.StoreServices.BusinessComponents.Selling.RetailTransactionLog.ExternalOrder;
using Retalix.StoreServices.BusinessComponents.Selling.RetailTransactionLog.ObjectModel;
using Retalix.StoreServices.BusinessComponents.Selling.RetailTransactionLog.ObjectModel.Converters;
using Retalix.StoreServices.BusinessComponents.Selling.Returns;
using Retalix.StoreServices.BusinessComponents.Selling.Returns.Actions;
using Retalix.StoreServices.BusinessComponents.Selling.Returns.Actions.ApprovalCredentials;
using Retalix.StoreServices.BusinessComponents.Selling.Returns.Actions.ForcedExchange;
using Retalix.StoreServices.BusinessComponents.Selling.Returns.BottleDeposit;
using Retalix.StoreServices.BusinessComponents.Selling.Returns.Conditions;
using Retalix.StoreServices.BusinessComponents.Selling.Returns.ForcedExchange;
using Retalix.StoreServices.BusinessComponents.Selling.Returns.Handlers;
using Retalix.StoreServices.BusinessComponents.Selling.Returns.Refunds;
using Retalix.StoreServices.BusinessComponents.Selling.Returns.Rules;
using Retalix.StoreServices.BusinessComponents.Selling.Returns.SaleLineFilter;
using Retalix.StoreServices.BusinessComponents.Selling.ScoIntervention;
using Retalix.StoreServices.BusinessComponents.Selling.SecurityScale;
using Retalix.StoreServices.BusinessComponents.Selling.SelfScan;
using Retalix.StoreServices.BusinessComponents.Selling.SelfWeigh;
using Retalix.StoreServices.BusinessComponents.Selling.Suspends;
using Retalix.StoreServices.BusinessComponents.Selling.Tab;
using Retalix.StoreServices.BusinessComponents.Selling.Tabs;
using Retalix.StoreServices.BusinessComponents.Selling.Tips;
using Retalix.StoreServices.BusinessComponents.Selling.Tips.TipTransactionLog;
using Retalix.StoreServices.BusinessComponents.Selling.Totals;
using Retalix.StoreServices.BusinessComponents.Selling.TransactionLog.ControlTransactionLog;
using Retalix.StoreServices.BusinessComponents.Selling.TransactionLog.ControlTransactionLog.Mappers;
using Retalix.StoreServices.BusinessComponents.Selling.TransactionLog.TenderControlTransactionLog;
using Retalix.StoreServices.BusinessComponents.Selling.TransactionLog.Validators;
using Retalix.StoreServices.BusinessComponents.Store.BusinessUnit;
using Retalix.StoreServices.BusinessComponents.Store.EOD;
using Retalix.StoreServices.BusinessComponents.Store.MenuConfiguration;
using Retalix.StoreServices.BusinessComponents.Store.MenuConfiguration.DMS;
using Retalix.StoreServices.BusinessComponents.Store.Parameters;
using Retalix.StoreServices.BusinessComponents.Store.Parameters.SecurityWeightTolerance;
using Retalix.StoreServices.BusinessComponents.Store.RunningNumber;
using Retalix.StoreServices.BusinessComponents.Supplier;
using Retalix.StoreServices.BusinessComponents.Tender.BC;
using Retalix.StoreServices.BusinessComponents.Tender.BRM;
using Retalix.StoreServices.BusinessComponents.Tender.Reporting.PrintableDataAdaptors;
using Retalix.StoreServices.BusinessComponents.Tender.Reporting.PrintableSourceDataRetrievers;
using Retalix.StoreServices.BusinessComponents.Tender.RetailTransactionLog;
using Retalix.StoreServices.BusinessComponents.Tender.TenderRoundingRule;
using Retalix.StoreServices.BusinessComponents.Tender.TenderRoundingRule.TenderRoundingValidators;
using Retalix.StoreServices.BusinessComponents.Tender.TenderType;
using Retalix.StoreServices.BusinessComponents.Tender.Validators;
using Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.Balancing.mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.OpenDrawer.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.Configuration.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.CouponInstance.Coupon;
using Retalix.StoreServices.BusinessServices.FrontEnd.CouponInstance.DMS;
using Retalix.StoreServices.BusinessServices.FrontEnd.CouponSeries.DMS;
using Retalix.StoreServices.BusinessServices.FrontEnd.DataProtection.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.DMS.GetDmsMessageDataService;
using Retalix.StoreServices.BusinessServices.FrontEnd.EOD.Handlers;
using Retalix.StoreServices.BusinessServices.FrontEnd.EOD.Validators;
using Retalix.StoreServices.BusinessServices.FrontEnd.ExtraPriceCalculationPolicy.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemGiftReceipt;
using Retalix.StoreServices.BusinessServices.FrontEnd.MenuConfiguration.ContextFormatting.Formatters.Command;
using Retalix.StoreServices.BusinessServices.FrontEnd.MenuConfiguration.ContextFormatting.Formatters.ItemLookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.MenuConfiguration.ContextFormatting.Formatters.Tender;
using Retalix.StoreServices.BusinessServices.FrontEnd.OLAMessage;
using Retalix.StoreServices.BusinessServices.FrontEnd.OLAMessage.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.OnlineItem.OnlineItemProviderConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.OrderCalculation.Validators;
using Retalix.StoreServices.BusinessServices.FrontEnd.PickUp.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.ProductAvailibility.LookUp;
using Retalix.StoreServices.BusinessServices.FrontEnd.Retransmit.Publishers;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.PolicyMaintenance.DMS;
using Retalix.StoreServices.BusinessServices.FrontEnd.SystemParameters;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tabs.mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.GetValidTenders.Handlers;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderAdd;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderRoundingRule.TenderRoundingMessage;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderRoundingRule.Validators;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tips.mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.Branding.FileSizeConfiguration.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.Branding.Images.Validators;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.DisplayAddAddLoyaltyConfiguration.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.Mappers;
using Retalix.StoreServices.BusinessServices.IDM.Authentication.Credentials;
using Retalix.StoreServices.BusinessServices.IDM.ChangePassword;
using Retalix.StoreServices.BusinessServices.IDM.ForcedSignOff;
using Retalix.StoreServices.BusinessServices.IDM.Request;
using Retalix.StoreServices.BusinessServices.Maintenance.Certificate;
using Retalix.StoreServices.BusinessServices.Maintenance.Certificate.V2;
using Retalix.StoreServices.BusinessServices.Maintenance.Price.Maintenance;
using Retalix.StoreServices.BusinessServices.Maintenance.Tabs;
using Retalix.StoreServices.Common.DMS;
using Retalix.StoreServices.Common.DMS.Coldstart;
using Retalix.StoreServices.Common.DMS.Configuration;
using Retalix.StoreServices.Common.DMS.DeleteAllProviders;
using Retalix.StoreServices.Common.DMS.DomainEvents;
using Retalix.StoreServices.Common.DMS.EntityConfiguration;
using Retalix.StoreServices.Common.DMS.ETW;
using Retalix.StoreServices.Common.DMS.Filter;
using Retalix.StoreServices.Common.DMS.Heartbeat;
using Retalix.StoreServices.Common.DMS.Message;
using Retalix.StoreServices.Common.DMS.Monitoring;
using Retalix.StoreServices.Common.DMS.PerformanceCounter;
using Retalix.StoreServices.Common.DMS.PhysicalToLogical;
using Retalix.StoreServices.Common.DMS.Rabbit;
using Retalix.StoreServices.Common.DMS.ServerHealth;
using Retalix.StoreServices.Common.DMS.SingularService;
using Retalix.StoreServices.Common.DMS.Snapshot;
using Retalix.StoreServices.Common.DMS.Versioning;
using Retalix.StoreServices.Connectivity;
using Retalix.StoreServices.Connectivity.Alert;
using Retalix.StoreServices.Connectivity.BI;
using Retalix.StoreServices.Connectivity.Cashier.AccessServices;
using Retalix.StoreServices.Connectivity.Cashier.AccessServices.PasswordHistory;
using Retalix.StoreServices.Connectivity.Cashier.User;
using Retalix.StoreServices.Connectivity.CashOffice.Dao;
using Retalix.StoreServices.Connectivity.CashOffice.Queries;
using Retalix.StoreServices.Connectivity.CDM;
using Retalix.StoreServices.Connectivity.Common;
using Retalix.StoreServices.Connectivity.Coupons;
using Retalix.StoreServices.Connectivity.Coupons.Upc5;
using Retalix.StoreServices.Connectivity.CouponSeries;
using Retalix.StoreServices.Connectivity.Customer.SavingAccount;
using Retalix.StoreServices.Connectivity.DataPattern.Dao.Cache;
using Retalix.StoreServices.Connectivity.DataPattern.DataPatternMetadata;
using Retalix.StoreServices.Connectivity.Denomination;
using Retalix.StoreServices.Connectivity.DeviceConfiguration.Repository;
using Retalix.StoreServices.Connectivity.DisposalMethod;
using Retalix.StoreServices.Connectivity.DMS;
using Retalix.StoreServices.Connectivity.DMS.Certificate;
using Retalix.StoreServices.Connectivity.DMS.DeleteAllProviders;
using Retalix.StoreServices.Connectivity.DMS.DmsServerInfo;
using Retalix.StoreServices.Connectivity.DMS.Routing;
using Retalix.StoreServices.Connectivity.DMS.ServerHealth;
using Retalix.StoreServices.Connectivity.DMS.Versioning;
using Retalix.StoreServices.Connectivity.Document;
using Retalix.StoreServices.Connectivity.EPS.OLAMessage;
using Retalix.StoreServices.Connectivity.Globalization;
using Retalix.StoreServices.Connectivity.Infrastructure;
using Retalix.StoreServices.Connectivity.Infrastructure.BPM;
using Retalix.StoreServices.Connectivity.Infrastructure.BusinessLogger;
using Retalix.StoreServices.Connectivity.Infrastructure.Counters;
using Retalix.StoreServices.Connectivity.Infrastructure.LocalizedTexts;
using Retalix.StoreServices.Connectivity.Infrastructure.Queue;
using Retalix.StoreServices.Connectivity.Infrastructure.Queue.CretrionAppliers;
using Retalix.StoreServices.Connectivity.Infrastructure.Queue.FailuresMessagesHandlers;
using Retalix.StoreServices.Connectivity.Infrastructure.Queue.Mappers;
using Retalix.StoreServices.Connectivity.Infrastructure.Queue.SpecificationApplier;
using Retalix.StoreServices.Connectivity.Infrastructure.Queue.ThirdParty;
using Retalix.StoreServices.Connectivity.Item;
using Retalix.StoreServices.Connectivity.Item.BulkPersister;
using Retalix.StoreServices.Connectivity.Manager.Extensions;
using Retalix.StoreServices.Connectivity.Manager.TextSearch.ExpressionProvider;
using Retalix.StoreServices.Connectivity.MenuConfiguration;
using Retalix.StoreServices.Connectivity.MenuConfiguration.PluRetentionDaos;
using Retalix.StoreServices.Connectivity.Message;
using Retalix.StoreServices.Connectivity.Organization.Notification;
using Retalix.StoreServices.Connectivity.Organization.OpenCloseDay;
using Retalix.StoreServices.Connectivity.Organization.RetailSegment;
using Retalix.StoreServices.Connectivity.Organization.TimeAvailabilityTerm;
using Retalix.StoreServices.Connectivity.Organization.VenueShift;
using Retalix.StoreServices.Connectivity.PaymentTransaction;
using Retalix.StoreServices.Connectivity.Pos.AccessServices;
using Retalix.StoreServices.Connectivity.Product.Associations;
using Retalix.StoreServices.Connectivity.Product.Prices;
using Retalix.StoreServices.Connectivity.Product.Prices.PriceBulk;
using Retalix.StoreServices.Connectivity.Product.ProductAvailability;
using Retalix.StoreServices.Connectivity.Product.ProductAvailability.DMS;
using Retalix.StoreServices.Connectivity.Product.ProductClassification.Department;
using Retalix.StoreServices.Connectivity.Product.StoreRange;
using Retalix.StoreServices.Connectivity.Promotion;
using Retalix.StoreServices.Connectivity.Reporting.Receipt;
using Retalix.StoreServices.Connectivity.Reporting.Receipt.Configuration;
using Retalix.StoreServices.Connectivity.Reporting.Receipt.Indicator;
using Retalix.StoreServices.Connectivity.ReturnDefinition.Repository;
using Retalix.StoreServices.Connectivity.Returns;
using Retalix.StoreServices.Connectivity.Returns.Queries;
using Retalix.StoreServices.Connectivity.Selling;
using Retalix.StoreServices.Connectivity.Selling.BPM;
using Retalix.StoreServices.Connectivity.Selling.DMS;
using Retalix.StoreServices.Connectivity.Servers;
using Retalix.StoreServices.Connectivity.Servers.Query;
using Retalix.StoreServices.Connectivity.Store.AccessServices;
using Retalix.StoreServices.Connectivity.Store.DTO.SecurityWeightTolerance;
using Retalix.StoreServices.Connectivity.Supplier;
using Retalix.StoreServices.Connectivity.Tenders;
using Retalix.StoreServices.Connectivity.Tenders.TenderRoundingRule;
using Retalix.StoreServices.Connectivity.Tenders.TenderType;
using Retalix.StoreServices.Connectivity.TouchPoint.Branding;
using Retalix.StoreServices.Connectivity.TouchPoint.DataObjects;
using Retalix.StoreServices.Connectivity.TouchPoint.Repository;
using Retalix.StoreServices.Connectivity.TouchPoint.SecurityScaleMaintenance;
using Retalix.StoreServices.Connectivity.Transaction.CashOfficeActivityLog;
using Retalix.StoreServices.Connectivity.Transaction.ControlAndRetailTransactionLog;
using Retalix.StoreServices.Connectivity.Transaction.ControlTransactionLog;
using Retalix.StoreServices.Connectivity.Transaction.Declaration;
using Retalix.StoreServices.Connectivity.Transaction.FundTransfer;
using Retalix.StoreServices.Connectivity.Transaction.FundTransfer.Mappers;
using Retalix.StoreServices.Connectivity.Transaction.GenericTransactionLog;
using Retalix.StoreServices.Connectivity.Transaction.Helper;
using Retalix.StoreServices.Connectivity.Transaction.RetailTransactionLog;
using Retalix.StoreServices.Connectivity.Transaction.ScoIntervention.Configuration;
using Retalix.StoreServices.Connectivity.Transaction.ScoIntervention.Definition.Basic;
using Retalix.StoreServices.Connectivity.Transaction.ScoIntervention.PosEvent;
using Retalix.StoreServices.Connectivity.Transaction.StoreSalesReport;
using Retalix.StoreServices.Connectivity.Transaction.TipTransactionLog;
using Retalix.StoreServices.Connectivity.Transaction.TransactionLog.DAO;
using Retalix.StoreServices.Infrastructure.DataAccess;
using Retalix.StoreServices.Infrastructure.NHibernate.Queries;
using Retalix.StoreServices.Infrastructure.NHibernate.TextSearch;
using Retalix.StoreServices.Model.Certificate;
using Retalix.StoreServices.Model.Customer;
using Retalix.StoreServices.Model.Customer.Agreement;
using Retalix.StoreServices.Model.Customer.EmailVerification;
using Retalix.StoreServices.Model.Customer.Legacy.OnlineAccount;
using Retalix.StoreServices.Model.Customer.Legacy.SavingAccount;
using Retalix.StoreServices.Model.Customer.Legacy.SavingAccount.Provider;
using Retalix.StoreServices.Model.Customer.ShoppingList;
using Retalix.StoreServices.Model.DataProtection;
using Retalix.StoreServices.Model.DMS;
using Retalix.StoreServices.Model.DMS.Versioning;
using Retalix.StoreServices.Model.Document;
using Retalix.StoreServices.Model.Document.ControlTransaction;
using Retalix.StoreServices.Model.Document.ControlTransactionLog;
using Retalix.StoreServices.Model.Document.Indicator;
using Retalix.StoreServices.Model.Document.TDM;
using Retalix.StoreServices.Model.Document.TDM.Managment;
using Retalix.StoreServices.Model.Document.TransactionLog;
using Retalix.StoreServices.Model.Finance;
using Retalix.StoreServices.Model.Finance.Account;
using Retalix.StoreServices.Model.Finance.Account.DMS;
using Retalix.StoreServices.Model.Finance.Activity;
using Retalix.StoreServices.Model.Finance.Balancing;
using Retalix.StoreServices.Model.Finance.Balancing.TransactionAccumulating;
using Retalix.StoreServices.Model.Finance.Configuration;
using Retalix.StoreServices.Model.Finance.Declaration;
using Retalix.StoreServices.Model.Finance.FundTransfer;
using Retalix.StoreServices.Model.Finance.Log;
using Retalix.StoreServices.Model.Finance.Money;
using Retalix.StoreServices.Model.Finance.OpenDrawer;
using Retalix.StoreServices.Model.Finance.Period;
using Retalix.StoreServices.Model.Finance.XZReports;
using Retalix.StoreServices.Model.Infrastructure;
using Retalix.StoreServices.Model.Infrastructure.AccessServices;
using Retalix.StoreServices.Model.Infrastructure.Audit;
using Retalix.StoreServices.Model.Infrastructure.BPM;
using Retalix.StoreServices.Model.Infrastructure.BusinessActivity;
using Retalix.StoreServices.Model.Infrastructure.BusinessRules;
using Retalix.StoreServices.Model.Infrastructure.BusinessRules.DAO;
using Retalix.StoreServices.Model.Infrastructure.Configuration;
using Retalix.StoreServices.Model.Infrastructure.Counter;
using Retalix.StoreServices.Model.Infrastructure.DataMovement;
using Retalix.StoreServices.Model.Infrastructure.DataPattern;
using Retalix.StoreServices.Model.Infrastructure.DataPattern.DatapatternMetadata;
using Retalix.StoreServices.Model.Infrastructure.Entity;
using Retalix.StoreServices.Model.Infrastructure.Events;
using Retalix.StoreServices.Model.Infrastructure.Globalization;
using Retalix.StoreServices.Model.Infrastructure.GuidGenerator;
using Retalix.StoreServices.Model.Infrastructure.JobManagement;
using Retalix.StoreServices.Model.Infrastructure.Legacy;
using Retalix.StoreServices.Model.Infrastructure.Legacy.Bulk;
using Retalix.StoreServices.Model.Infrastructure.Legacy.Configuration;
using Retalix.StoreServices.Model.Infrastructure.Legacy.Globalization;
using Retalix.StoreServices.Model.Infrastructure.Legacy.Globalization.Country;
using Retalix.StoreServices.Model.Infrastructure.LocalizedTexts;
using Retalix.StoreServices.Model.Infrastructure.Message;
using Retalix.StoreServices.Model.Infrastructure.OLAMessage;
using Retalix.StoreServices.Model.Infrastructure.Queue;
using Retalix.StoreServices.Model.Infrastructure.Queue.FailuresMessagesHandlers;
using Retalix.StoreServices.Model.Infrastructure.ReasonCode;
using Retalix.StoreServices.Model.Infrastructure.RetentionPolicy;
using Retalix.StoreServices.Model.Infrastructure.Security;
using Retalix.StoreServices.Model.Infrastructure.Security.Authentication;
using Retalix.StoreServices.Model.Infrastructure.Security.Authentication.Credentials;
using Retalix.StoreServices.Model.Infrastructure.Security.DirectAuthentication;
using Retalix.StoreServices.Model.Infrastructure.Security.Encryption;
using Retalix.StoreServices.Model.Infrastructure.Security.Identity;
using Retalix.StoreServices.Model.Infrastructure.Security.Identity.Configuration;
using Retalix.StoreServices.Model.Infrastructure.Security.Identity.Extenders;
using Retalix.StoreServices.Model.Infrastructure.Security.Identity.Roles;
using Retalix.StoreServices.Model.Infrastructure.Service;
using Retalix.StoreServices.Model.Infrastructure.StoreApplication;
using Retalix.StoreServices.Model.Organization.Alert;
using Retalix.StoreServices.Model.Organization.BusinessUnit;
using Retalix.StoreServices.Model.Organization.Coldstart;
using Retalix.StoreServices.Model.Organization.Device;
using Retalix.StoreServices.Model.Organization.EOD;
using Retalix.StoreServices.Model.Organization.IDM;
using Retalix.StoreServices.Model.Organization.Notification;
using Retalix.StoreServices.Model.Organization.OpenCloseDay;
using Retalix.StoreServices.Model.Organization.RetailSegment;
using Retalix.StoreServices.Model.Organization.SecurityScaleMaintenance;
using Retalix.StoreServices.Model.Organization.Servers;
using Retalix.StoreServices.Model.Organization.TimeAvailabilityTerm;
using Retalix.StoreServices.Model.Organization.TouchPoint;
using Retalix.StoreServices.Model.Organization.TouchPoint.Branding.FileSizeConfiguration;
using Retalix.StoreServices.Model.Organization.TouchPoint.Branding.Images;
using Retalix.StoreServices.Model.Organization.User;
using Retalix.StoreServices.Model.Organization.User.Events;
using Retalix.StoreServices.Model.Organization.VenueShift;
using Retalix.StoreServices.Model.PaymentTransaction;
using Retalix.StoreServices.Model.Product;
using Retalix.StoreServices.Model.Product.Calorie;
using Retalix.StoreServices.Model.Product.Hierarchy.Department;
using Retalix.StoreServices.Model.Product.ProductAvailability;
using Retalix.StoreServices.Model.Product.StoreRange;
using Retalix.StoreServices.Model.Promotion.Advertisement;
using Retalix.StoreServices.Model.Promotion.Coupons;
using Retalix.StoreServices.Model.ReportingWarehouse.ODS;
using Retalix.StoreServices.Model.ReportingWarehouse.ODS.Document;
using Retalix.StoreServices.Model.RetransmitTranscation;
using Retalix.StoreServices.Model.Selling;
using Retalix.StoreServices.Model.Selling.Behavior.Kit;
using Retalix.StoreServices.Model.Selling.Behavior.OnlineItem;
using Retalix.StoreServices.Model.Selling.BRM;
using Retalix.StoreServices.Model.Selling.BRM.Expression;
using Retalix.StoreServices.Model.Selling.ConditionalRestriction.Interventions;
using Retalix.StoreServices.Model.Selling.CustomerOrder;
using Retalix.StoreServices.Model.Selling.CustomerOrder.Line;
using Retalix.StoreServices.Model.Selling.CustomerOrder.Validators;
using Retalix.StoreServices.Model.Selling.CustomerPayment;
using Retalix.StoreServices.Model.Selling.DataAccess;
using Retalix.StoreServices.Model.Selling.Drawer;
using Retalix.StoreServices.Model.Selling.EligibilityPolicy;
using Retalix.StoreServices.Model.Selling.ExtraPriceCalculationPolicy;
using Retalix.StoreServices.Model.Selling.GiftReceipt;
using Retalix.StoreServices.Model.Selling.OrderCalculation;
using Retalix.StoreServices.Model.Selling.OrderProcess;
using Retalix.StoreServices.Model.Selling.Rescan.PredictiveRescan;
using Retalix.StoreServices.Model.Selling.RetailTransaction;
using Retalix.StoreServices.Model.Selling.RetailTransaction.AutoVoidAbandonedTransaction;
using Retalix.StoreServices.Model.Selling.RetailTransaction.RetailTransactionLog;
using Retalix.StoreServices.Model.Selling.RetailTransaction.Totals;
using Retalix.StoreServices.Model.Selling.Returns;
using Retalix.StoreServices.Model.Selling.Returns.BottleDeposit;
using Retalix.StoreServices.Model.Selling.Returns.ForceExchange;
using Retalix.StoreServices.Model.Selling.Returns.Queries;
using Retalix.StoreServices.Model.Selling.Returns.Refunds;
using Retalix.StoreServices.Model.Selling.Returns.Rules;
using Retalix.StoreServices.Model.Selling.ScoIntervention;
using Retalix.StoreServices.Model.Selling.ScoIntervention.Configuration;
using Retalix.StoreServices.Model.Selling.ScoIntervention.Definition;
using Retalix.StoreServices.Model.Selling.ScoIntervention.Maintenance;
using Retalix.StoreServices.Model.Selling.SecurityScale;
using Retalix.StoreServices.Model.Selling.SecurityScale.SecurityWeightTolerance;
using Retalix.StoreServices.Model.Selling.SelfScan;
using Retalix.StoreServices.Model.Selling.Tab;
using Retalix.StoreServices.Model.Selling.Tips;
using Retalix.StoreServices.Model.Selling.Tips.TipTransactionLog;
using Retalix.StoreServices.Model.Supplier;
using Retalix.StoreServices.Model.Tax.Definitions;
using Retalix.StoreServices.Model.Tender;
using Retalix.StoreServices.Model.Tender.ReturnDefinition;
using Retalix.StoreServices.Model.Tender.SuggestedAmount;
using Retalix.StoreServices.Model.Tender.TenderExchange;
using Retalix.StoreServices.Model.Tender.TenderRoundingRule;
using Retalix.StoreServices.Model.Tender.TenderType;
using Retalix.StoreServices.Model.Tender.TenderType.ChangeOptions;
using Retalix.StoreServices.Model.Tender.TenderType.PaymentOptions;
using Retalix.StoreServices.Model.TouchPointApplication;
using Retalix.StoreServices.Model.TouchPointApplication.Command;
using Retalix.StoreServices.Model.TouchPointApplication.CustomerScreenConfiguration;
using Retalix.StoreServices.Model.TouchPointApplication.KeyboardConfigurations;
using Retalix.StoreServices.Model.TouchPointApplication.KitDisplayLayout;
using Retalix.StoreServices.Model.TouchPointApplication.MenuConfiguration;
using Retalix.StoreServices.Model.TouchPointApplication.MenuConfiguration.DMS;
using Retalix.StoreServices.Model.TouchPointApplication.MenuConfiguration.ProductGroup;
using Retalix.StoreServices.Model.TouchPointApplication.OwnBagConfiguration;
using StoreNet.Environment;
using System.Collections.Generic;
using System.Configuration;
using Retalix.StoreServices.BusinessComponents.Organization.R10Version;
using Retalix.StoreServices.BusinessComponents.Reporting.Receipt.PreBindProcessors;
using Retalix.StoreServices.Model.Receipts;
// ReSharper disable once CSharpWarnings::CS0618
using IUserDao = Retalix.StoreServices.Model.Organization.User.V10_5.IUserDao;
using SchemaObjects = Retalix.Contract.Schemas.Schema.ARTS.PosLog_V6.Objects.SchemaObjects;
using Retalix.StoreServices.BusinessComponents.Selling.TransactionLog.ControlTransactionLog.Converters;
using Retalix.StoreServices.Model.DepositCashDuringSaleMode;
using Retalix.StoreServices.BusinessServices.FrontEnd.DepositCashDuringSaleMode.Mappers;
using Retalix.StoreServices.Model.Organization.TouchPoint.VirtualKeyboardConfigurations;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication.VirtualKeyboardConfigurations;
using Retalix.StoreServices.Connectivity.TouchPoint.VirtualKeyboardConfigurations;
using Retalix.StoreServices.Model.MemberAccountsToDisplay;
using Retalix.StoreServices.BusinessServices.Maintenance.MemberAccountsToDisplay.ConfigurationMaintenance;
using Retalix.StoreServices.BusinessComponents.Promotion.Coupons.BusinessRules;
using Retalix.StoreServices.BusinessComponents.Promotion.Coupons.BusinessRules.UnusedCoupon;
using Retalix.StoreServices.Model.Organization.TouchPoint.DynamicFormConfiguration;
using Retalix.StoreServices.Connectivity.TouchPoint.DynamicFormConfiguration;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication.DynamicFormConfiguration;
using Retalix.StoreServices.Model.Organization.TouchPoint.PosParameterConfiguration;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication.PosParameterConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.Masking;
using Retalix.StoreServices.BusinessServices.FrontEnd.Masking.Mappers;
using Retalix.StoreServices.Common.DMS.MobileFarmStoreDistribution;
using Retalix.StoreServices.Model.Infrastructure.Cache;
using Retalix.StoreServices.Connectivity.Cache;
using Retalix.StoreServices.BusinessComponents.Selling.TransactionGapFilling;
using Retalix.StoreServices.Connectivity.Infrastructure.Security;
using Retalix.StoreServices.Model.Infrastructure.Security.Masking;
using Retalix.StoreServices.Model.Organization.TouchPoint.PosDepositBusinessConfiguration;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication.PosDepositBusinessConfiguration;
using Retalix.StoreServices.Model.Organization.R10Version;
using Retalix.StoreServices.BusinessServices.Maintenance.DetailedOrderCode;
using TenderTypeHelper = Retalix.StoreServices.BusinessComponents.Selling.Payments.TenderTypeHelper;
using Retalix.StoreServices.BusinessServices.FrontEnd.CodeSignatureVerification.Mappers;
using Retalix.StoreServices.Model.CodeSignatureVerification;
using Retalix.StoreServices.BusinessComponents.IDM.CodeSignatureVerification;
using Retalix.StoreServices.Connectivity.Transaction.TransactionLog;
using Retalix.StoreServices.Model.Promotion;
using Retalix.StoreServices.Model.RefundVoucher;
using Retalix.StoreServices.BusinessServices.FrontEnd.RefundVoucher;
using Retalix.StoreServices.Model.DetailedOrderCode;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemAddBulk.Handlers;
using Retalix.StoreServices.BusinessComponents.RefundVoucher;
using Retalix.StoreServices.Model;
using Retalix.StoreServices.BusinessServices.FrontEnd.BusinessConfiguration.Mappers;
using Retalix.StoreServices.Model.Tax;
using Retalix.StoreServices.BusinessServices.FrontEnd.Taxes.RecalculateReturnTax;
using Retalix.StoreServices.Model.Finance.Fiscal;
using Retalix.StoreServices.BusinessComponents.Organization.Fiscal;
using Retalix.StoreServer.BusinessComponents.Finance.Fiscal;
using Retalix.StoreServices.Model.EpsReconcile;
using Retalix.StoreServer.BusinessComponents.Finance.EpsReconcile;
using Retalix.StoreServices.Model.Infrastructure.Serialization;
using Retalix.StoreServices.Connectivity.RefundVoucher.Dao;
using Retalix.StoreServices.Model.EnableDevicesApiConfiguration;
using Retalix.StoreServices.BusinessComponents.Organization.EnableDevicesApiConfiguration;
using Retalix.Contracts.Generated.Arts.RtiV4;
using Retalix.Contracts.Generated.Notification;
using Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.Configuration.Validators;
using Retalix.StoreServices.BusinessServices.FrontEnd.ApplicationLink;
using Retalix.StoreServices.Model.ApplicationLink;
using Retalix.StoreServices.Model.Price;
using Retalix.StoreServices.BusinessServices.Maintenance.Price.Mappers;
using Retalix.StoreServices.BusinessComponents.Customer;
using Retalix.StoreServices.BusinessComponents.Product.Prices;
using Retalix.StoreServices.BusinessComponents.Selling.Intervention;
using Retalix.StoreServices.BusinessServices.FrontEnd.DelayedIntervention.Mappers;
using Retalix.StoreServices.Model.DelayedIntervention;
using Retalix.StoreServices.BusinessComponents.Selling.PriceQueries;
using Retalix.StoreServices.Model.Configuration;
using Retalix.StoreServices.BusinessComponents.Organization.Configuration;
using Retalix.StoreServices.Model.EpsConfiguration;
using Retalix.StoreServices.BusinessComponents.Selling.RedisCacheTrim;
using Retalix.StoreServices.Model.Document.TDM.SearchExtensibility;
using Retalix.StoreServer.BusinessComponents.Finance.ArtsPosLog60.StoreSettlement;
using Retalix.StoreServices.BusinessServices.Maintenance.Notification.Maintenance.Adaptors;
using Retalix.StoreServices.BusinessComponents.Selling.BPM;
using Retalix.StoreServices.BusinessServices.Maintenance.Notification.Maintenance.Validators;
using Retalix.StoreServices.Model.Promotion.SecondPE;
using Retalix.StoreServices.Model.Promotion.Mapper;
using Retalix.StoreServices.BusinessComponents.Promotion.SecondPE;
using Retalix.StoreServices.Connectivity.Selling.VatSeal;
using Retalix.StoreServices.Model.Customer.MultipleCustomer;
using Retalix.StoreServices.BusinessComponents.Customer.MultiCustomer;
using Retalix.StoreServices.BusinessComponents.Customer.Handlers;
using Retalix.StoreServices.Model.Customer.Handlers;
using Retalix.StoreServices.BusinessServices.FrontEnd.BusinessRoles;

namespace Retalix.StoreServer.CoreConfiguration.CoreComponents
{
    internal class BusinessComponents : CastleConfigurationInstaller
    {
        private void RegisterFuelBusinessComponents(IComponentInstaller builder)
        {
            builder.Register(Component.For<IEntityDeleteCriteria>().Named("IEntityDeleteCriteria").ImplementedBy<EntityDeleteCriteria>().LifeStyle.Transient);

            builder.RegisterSingleton<IEndorsementDao, EndorsementDao>();

            builder.Register(Component.For<IEndorsementFactory>().Named("IEndorsementFactory").ImplementedBy<EndorsementFactory>().LifeStyle.Transient);
        }

        public override void Install(IComponentInstaller builder)
        {
            var entityRegistry = builder.GetRegistrationFacility<IEntityRegistrationFacility>();

            builder.Register(Component.For<IBusinessRolesDao>().Named("BusinessRolesDao").ImplementedBy<BusinessRolesDao>().LifeStyle.Transient);

            builder.Register(Component.For<IQueueMessageFilter>().Named("QueueMessageFilterBase").ImplementedBy<QueueMessageFilterBase>().LifeStyle.Singleton);

            builder.Register(Component.For<IUpdateBusinessProcessUponUploadStrategy>().Named("UpdateBusinessProcessUponUploadStrategy").ImplementedBy<UpdateBusinessProcessUponUploadStrategy>());

            builder.Register(Component.For<IEntityExtender<IUser>>().Named("ClientSessionExtender").ImplementedBy<ClientSessionExtender>().LifeStyle.Singleton);

            builder.Register(Component.For<ICounterDao>().Named("CounterDao").ImplementedBy<CounterDao>().LifeStyle.Singleton);

            builder.Register(Component.For<IVerificationMark>().Named("IVerificationMark").ImplementedBy<VerificationMark>().LifeStyle.Transient);
            builder.RegisterMapper<VerificationMarkMapper>();

            builder.Register(Component.For<IRefundVoucherBusinessConfiguration>().Named("IRefundVoucherBusinessConfiguration").ImplementedBy<RefundVoucherBusinessConfiguration>().LifeStyle.Transient);

            builder.Register(Component.For<ISkipWeightInTBRConfig>().Named("ISkipWeightInTBRConfig").ImplementedBy<SkipWeightInTBRConfig>().LifeStyle.Singleton);

            builder.Register(Component.For<IOfficeBusinessConfiguration>().Named("IOfficeBusinessConfiguration").ImplementedBy<OfficeBusinessConfiguration>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessUnitUrlConfiguration>().Named("IBusinessUnitUrlConfiguration").ImplementedBy<BusinessUnitUrlConfiguration>().LifeStyle.Transient);
            builder.RegisterMapper<BusinessUnitUrlConfigurationMapper>();

            builder.Register(Component.For<ICounterType>().Named("CounterType").ImplementedBy<CounterType>().LifeStyle.Transient);

            builder.Register(Component.For<ICounter>().Named("Counter").ImplementedBy<Counter>().LifeStyle.Transient);

            builder.Register(Component.For<IDmsFilterExecuter>().Named("IDmsFilterExecuter").ImplementedBy<DmsFilterExecuter>().LifeStyle.Singleton);

            builder.RegisterSingleton<IMessageFactory, MessageFactory>();

            entityRegistry.RegisterEntity(EntityRegistration.For<IMessage, int>().WithDefaultDao().WithDefaultDaoResources());

            entityRegistry.RegisterEntityImplementation(EntityImplementationRegistration.For<IMessage, int>().WithDefaultInterface().Named("Message")
                .ImplementedBy<Message>().WithDefaultDaoResources());

            builder.Register(Component.For<IExpressionResolver>().Named("IExpressionResolver").ImplementedBy<ExpressionResolver>().LifeStyle.Singleton);

            builder.Register(Component.For<IMessageDao>().Named("IMessageDao").ImplementedBy<MessageDao>().LifeStyle.Singleton);

            builder.RegisterSingleton<IDatabaseManagementBc, DatabaseManagementBc>();

            builder.Register(Component.For<IDmsConfigurationRepository>().Named("IDmsConfigurationRepository").ImplementedBy<DmsConfigurationRepository>().LifeStyle.Singleton);

            builder.Register(Component.For<IDmsContextRepository>().Named("IDmsContextRepository").ImplementedBy<DmsContextRepository>().LifeStyle.Singleton);

            builder.Register(Component.For<IScoInterventionBasicDefinitionDao>().Named("IScoInterventionBasicDefinitionDao").ImplementedBy<ScoInterventionBasicDefinitionDao>().LifeStyle.Singleton);

            builder.Register(Component.For<IScoInterventionConfigurationMaintenance>().Named("IScoInterventionConfigurationMaintenance").ImplementedBy<ScoInterventionConfigurationMaintenance>().LifeStyle.Singleton);

            builder.Register(Component.For<IScoInterventionBasicDefinitionStrategy>().Named("IScoInterventionBasicDefinitionStrategy").ImplementedBy<ScoInterventionBasicDefinitionStrategy>().LifeStyle.Singleton);

            builder.RegisterSingleton<IScoInterventionConfigurationDao, ScoInterventionConfigurationDao>();

            builder.Register(Component.For<IDmsConfigurationParameter>().Named("DmsConfigurationParameter").ImplementedBy<DmsConfigurationParameter>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityExtender<IMessage>>().Named("AlertTemplateExtender").ImplementedBy<AlertTemplateExtender>().LifeStyle.Singleton);

            builder.Register(Component.For<IDmsConfigurationParameter>().Named("DmsContextParameter").ImplementedBy<DmsContextParameter>().LifeStyle.Transient);

            builder.Register(Component.For<ITouchPointCredentials>().Named("ITouchPointCredentials").ImplementedBy<TouchPointCredentials>().LifeStyle.Transient);

            builder.Register(Component.For<IDmsConfigurationParameter>().Named("DmsConfigurationParameterLocal").ImplementedBy<DmsConfigurationParameterLocal>().LifeStyle.Transient);

            builder.Register(Component.For<IDataProvidersOrder>().Named("IDataProvidersOrder").ImplementedBy<DataProvidersOrder>().LifeStyle.Transient);

            builder.Register(Component.For<IServerComponentInfo>().Named("IServerComponentInfo").ImplementedBy<ServerComponentInfo>().LifeStyle.Transient);

            builder.RegisterSingleton<IServerComponentsInfoDao, ServerComponentsInfoCachedDao>();

            builder.Register(Component.For<IServerComponentsInfoDao>().Named("ServerComponentsInfoDao").ImplementedBy<ServerComponentsInfoDao>().LifeStyle.Transient);

            builder.RegisterSingleton<IServerComponentsInfoSerializer, ServerComponentsInfoSerializer>();

            builder.Register(Component.For<IServerInfo>().Named("IServerInfo").ImplementedBy<ServerInfo>().LifeStyle.Transient);

            builder.Register(Component.For<ISession>().Named("ISession").ImplementedBy<Session>().LifeStyle.Transient);

            builder.Register(Component.For<IPriceReason>().Named("IPriceReason").ImplementedBy<PriceReason>().LifeStyle.Transient);

            builder.RegisterSingleton<IRoleBc, RoleBc>();

            builder.RegisterSingleton<ITransportationInfoRepository, TransportationInfoRepository>();

            builder.RegisterSingleton<IMonitoringInfoRepository, MonitoringInfoRepository>();

            builder.RegisterSingleton<IServerInfoRepository, ServerInfoRepository>();

            builder.RegisterSingleton<ICurrentBusinessDay, CurrentBusinessDay>();

            builder.RegisterSingleton<IEventMonitorProvider, EventLogMonitorProvider>();

            builder.Register(Component.For<IBusinessDayUnit>().Named("IBusinessDayUnit").ImplementedBy<BusinessDayUnit>().LifeStyle.Transient);

            builder.Register(
                Component.For<ISellableTenderLocator>().Named("ISellableTenderLocator").ImplementedBy
                    <SellableTenderLocator>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<ITouchPointDao>().Named("ITouchPointDao").ImplementedBy
                    <TouchPointDao>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ITouchPoint>().Named("ITouchPoint").ImplementedBy
                    <TouchPoint>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ITouchPointRegistration>().Named("ITouchPointRegistration").ImplementedBy
                    <TouchPointRegistration>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ITouchPointUpdateIsClustered>().Named("ITouchpointUpdateIsClustered").ImplementedBy
                    <TouchpointUpdateIsClustered>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ITouchPointApplicationDao>().Named("ITouchPointApplicationDao").ImplementedBy
                    <TouchPointApplicationDao>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ITouchPointApplication>().Named("ITouchPointApplication").ImplementedBy
                    <TouchPointApplication>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDataPatternDecoder>().Named("IDataPatternDecoder").ImplementedBy<DataPatternDecoder>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<IDataPatternConflictResolver>().Named("IDataPatternConflictResolver").ImplementedBy
                    <DataPatternConflictResolver>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<IDataPatternEncoder>().Named("IDataPatternEncoder").ImplementedBy<DataPatternEncoder>().
                          LifeStyle.Transient);


            builder.Register(
                Component.For<IDataPatternDao>().Named("IDataPatternDao").ImplementedBy<DataPatternDaoProxy>().LifeStyle.
                          Singleton);

            builder.Register(
                Component.For<IDataPatternFactory>().Named("IDataPatternFactory").ImplementedBy<DataPatternFactory>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<IDecoderProvider>().Named("IDecoderProvider").ImplementedBy<DecoderProvider>().LifeStyle.
                          Singleton);

            builder.Register(
                Component.For<IReturnDefinitionRepository>().Named("IReturnDefinitionRepository").ImplementedBy
                    <ReturnDefinitionRepository>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDeviceTypeSearchCriteria>().Named("IDeviceTypeSearchCriteria").ImplementedBy
                    <DeviceTypeSearchCriteria>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDeviceType>().Named("IDeviceType").ImplementedBy
                    <DeviceType>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDeviceTypeDao>().Named("IDeviceTypeDao").ImplementedBy<DeviceTypeDao>().
                          LifeStyle.Singleton);

            builder.Register(
                            Component.For<IGeographicLocation>().Named("IGeographicLocation").ImplementedBy
                                <GeographicLocation>().LifeStyle.Transient);

            builder.Register(
                Component.For<IAddress>().Named("IAddress").ImplementedBy
                    <Address>().LifeStyle.Transient);

            builder.Register(
                Component.For<ICurrencyInfoProvider>().Named("ICurrencyInfoProvider").ImplementedBy
                    <CurrencyInfoProvider>().LifeStyle.Transient);

            builder.RegisterSingleton<IDeviceProfileDao, DeviceConfigurationRepository>("IDeviceConfigurationRepository");

            builder.RegisterSingleton<IDataValidator<IUser>, UserFirstNameValidator>();
            builder.RegisterSingleton<IDataValidator<IUser>, UserLastNameValidator>();
            builder.RegisterSingleton<IDataValidator<IUser>, UserUserNameValidator>();
            builder.RegisterSingleton<IDataValidator<IUser>, UserEmailValidator>();
            builder.RegisterSingleton<IDataValidator<IUser>, UserNameInActiveDirectoryValidator>();
            builder.RegisterSingleton<IDataValidator<IUser>, UserPhoneValidator>();

            builder.Register(Component.For<IUserDao>().Named("ObsoleteUserDao").ImplementedBy<ObsoleteUserDao>().LifeStyle.Singleton);

            builder.Register(Component.For<IPasswordHistoryDao>().Named("IPasswordHistoryDao").ImplementedBy<PasswordHistoryDao>().LifeStyle.Singleton);

            builder.Register(Component.For<IRoleDao>().Named("IRoleDao").ImplementedBy<RoleDao>().LifeStyle.Singleton);

            builder.Register(Component.For<ICustomerAgreementDao>().Named("ICustomerAgreementDao").ImplementedBy<CustomerAgreementDao>().LifeStyle.Singleton);

            builder.RegisterSingleton<ICustomerAgreementFactory, CustomerAgreementFactory>();

            builder.RegisterSingleton<ICustomerAgreementConversionRateFactory, CustomerAgreementConversionRateFactory>();

            builder.Register(
                Component.For<ISavingAccount>().Named("ISavingAccount").ImplementedBy<SavingAccount>().
                          LifeStyle.Transient);

            builder.Register(
                Component.For<ISavingAccountProviderAdaptor>().Named("LoyaltySavingAccountProviderAdaptor").
                          ImplementedBy
                    <LoyaltySavingAccountProviderAdaptor>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<ISavingAccountProviderAdaptor>().Named("EpsSavingAccountProviderAdaptor").ImplementedBy
                    <EpsSavingAccountProviderAdaptor>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<ISavingAccountProvider>().Named("ISavingAccountProvider").ImplementedBy
                    <SavingAccountProviderReferenceImplementation>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<ISavingAccountDepositManager>().Named("ISavingAccountDepositManager").ImplementedBy
                    <SavingAccountDepositManager>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<ISavingAccountTransactionLogDao>().Named("ISavingAccountTransactionLogDao").ImplementedBy
                    <SavingAccountTransactionLogDao>().
                          LifeStyle.Singleton);


            builder.Register(Component.For<IActivityLogProvider>().Named("ActivityLogProvider").ImplementedBy<ActivityLogProvider>().LifeStyle.Transient);

            builder.RegisterSingleton<IRunningNumberService, RunningNumberService>();

            builder.RegisterSingleton<IDocumentFormatter, IXReportReceiptGenerator, ReceiptFormatter>();

            builder.Register(
                Component.For<ITransactionContentDataBuilder>().Named("ITransactionContentDataBuilder").ImplementedBy
                    <TransactionContentDataBuilder>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<IReceiptLayoutFactory>().Named("IReceiptLayoutFactory").ImplementedBy
                    <ReceiptLayoutSelecter>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<IReceiptEngine>().Named("IReceiptEngine").ImplementedBy<ReceiptEngine>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<IReceiptLayoutRepository>().Named("IReceiptLayoutRepository").ImplementedBy
                    <ReceiptLayoutRepository>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<ILayoutLineTemplateRepository>().Named("ILayoutLineTemplateRepository").ImplementedBy
                    <LayoutLineTemplateRepository>().
                          LifeStyle.Transient);

            builder.Register(
                Component.For<ISlipTypeRepository>().Named("ISlipTypeRepository").ImplementedBy<SlipTypeRepository>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<ILineDefinitionRepository>().Named("ILineDefinitionRepository").ImplementedBy
                    <LineDefinitionRepository>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<IReceiptConfigurationRepository>().Named("IReceiptConfigurationRepository").ImplementedBy
                    <ReceiptConfigurationRepository>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<ILocalizedListSearchCriteria>().Named("ILocalizedListSearchCriteria").ImplementedBy
                    <LocalizedListSearchCriteria>().LifeStyle.Transient);

            builder.Register(
                Component.For<ILocalizedList>().Named("ILocalizedList").ImplementedBy
                    <LocalizedList>().LifeStyle.Transient);

            builder.Register(
                Component.For<ILocalizedListDao>().Named("ILocalizedListDao").ImplementedBy<LocalizedListDao>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<IRetailTransactionLookupStrategy>().Named(typeof(RetailTransactionLookupStrategy).Name).ImplementedBy<RetailTransactionLookupStrategy>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<IReceiptConfigurationParameterFactory>().Named("IReceiptConfigurationParameterFactory").
                          ImplementedBy<ReceiptConfigurationParameterFactory>().
                          LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IPrintableSourceDataRetriever<IRetailTransaction>>()
                                 .Named("RetailTransactionSourceDataRetriever")
                                 .ImplementedBy<RetailTransactionSourceDataRetriever>().
                                  LifeStyle.Singleton);

            builder.Register(
                Component.For<IPrintableSourceDataRetriever<IRetailTransaction>>().Named("KitLineSourceDataRetriever")
                         .ImplementedBy
                    <StoreServices.BusinessComponents.Selling.KitReceipt.KitIngredientLineSourceDataRetriever>().
                          LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IPrintableSourceDataRetriever<IRetailTransaction>>()
                                 .Named("VoidedOrderLinesSourceDataRetriever")
                                 .ImplementedBy<VoidedOrderLinesSourceDataRetriever>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IPrintableSourceDataRetriever<IRetailTransaction>>()
                                 .Named("BalanceInquirySourceDataRetriever")
                                 .ImplementedBy<BalanceInquirySourceDataRetriever>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IPrintableSourceDataRetriever<IRetailTransaction>>()
                                 .Named("OrderLineSourceDataRetriever")
                                 .ImplementedBy<OrderLineSourceDataRetriever>().
                                  LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IPrintableSourceDataRetriever<IRetailTransaction>>()
                                 .Named("OrderLineInstructionsSourceDataRetriever")
                                 .ImplementedBy<OrderLineInstructionsSourceDataRetriever>().
                                  LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IPrintableSourceDataRetriever<IRetailTransaction>>()
                                 .Named("GiftReceiptLineSourceDataRetriever")
                                 .ImplementedBy<GiftReceiptLineSourceDataRetriever>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IPrintableSourceDataRetriever<IRetailTransaction>>()
                                 .Named("ReturnOrderLineSourceDataRetriever")
                                 .ImplementedBy<ReturnOrderLineSourceDataRetriever>()
                                 .LifeStyle.Singleton);

            builder.Register(
                Component.For<IPrintableSourceDataRetriever<IRetailTransaction>>().Named("LoyaltySourceDataRetriever").
                          ImplementedBy<LoyaltySourceDataRetriever>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<IPrintableSourceDataRetriever<IRetailTransaction>>().Named("PaymentSourceDataRetriever").
                          ImplementedBy<PaymentSourceDataRetriever>().
                          LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IPrintableSourceDataRetriever<IRetailTransaction>>()
                                 .Named("CustomerToPayDataSourceDataRetriever")
                                 .ImplementedBy<CustomerToPayDataSourceDataRetriever>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IPrintableDataBuilder>()
                                 .Named("PrintableDataBuilder")
                                 .ImplementedBy<PrintableDataBuilder>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                .For<IPrintableSourceDataRetriever<IRetailTransaction>>().Named("DeclinedPaymentSourceDataRetriever")
                                .ImplementedBy<DeclinedPaymentSourceDataRetriever>()
                                .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IPrintableSourceDataRetriever<IRetailTransaction>>()
                                 .Named("FormsPrintableSourceDataRetriever")
                                 .ImplementedBy<FormsPrintableSourceDataRetriever>()
                                 .LifeStyle.Singleton);

            builder.RegisterSingleton<IEndOfDayRepository, EndOfDayRepository>();

            builder.Register(Component.For<IFiscalComponent>().Named("FiscalComponent").ImplementedBy<FiscalComponent>().LifeStyle.Singleton);
            builder.Register(Component.For<IFiscalValidator>().Named("FiscalValidator").ImplementedBy<FiscalValidator>().LifeStyle.Singleton);
            builder.Register(Component.For<IJsonSerializerSettings>().Named("FiscalJsonSerializerSettings").ImplementedBy<FiscalJsonSerializerSettings>().LifeStyle.Transient);
            builder.Register(Component.For<IFiscalData>().Named("FiscalData").ImplementedBy<FiscalData>().LifeStyle.Transient);

            builder.Register(Component.For<IControlTransactionLogDocumentWriter>().Named("IControlTransactionLogDocumentWriter").ImplementedBy<ControlTransactionLogDocumentWriter>().LifeStyle.Singleton);
            builder.Register(Component.For<IXZReportTransactionLogGenerator>().Named("IXZReportTransactionLogGenerator").ImplementedBy<XZReportTransactionLogGenerator>().LifeStyle.Singleton);
            builder.Register(Component.For<IControlTransactionLogCreator>().Named("IControlTransactionLogCreator").ImplementedBy<ControlTransactionLogCreator>().LifeStyle.Singleton);

            builder.Register(Component.For<ITenderControlTransactionLogCreator>().Named("ITenderControlTransactionLogCreator").ImplementedBy<TenderControlTransactionLogCreator>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessUnitDaoCacheUtil>().ImplementedBy<BusinessUnitDaoCacheUtil>().LifeStyle.Singleton);

            builder.Register(Component.For<IBusinessUnitDao>().ImplementedBy<BusinessUnitDao>().LifeStyle.Singleton);

            builder.Register(Component.For<IApplicationParameters>().Named("IApplicationParameters").ImplementedBy<ApplicationParameters>().LifeStyle.Singleton);

            builder.Register(Component.For<IFundTransferTransaction>().Named("IFundTransferTransaction").ImplementedBy<FundTransferTransaction>().LifeStyle.Transient);

            builder.Register(
                Component.For<IAutoReconcileFundTransferTransaction>().Named("AutoReconcileFundTransferTransaction").ImplementedBy
                    <AutoReconcileFundTransferTransaction>().LifeStyle.Transient);

            builder.Register(Component.For<IFundTransferTransactionObserver>().Named("LogBuilderFundTransferTransactionObserver")
                         .ImplementedBy<LogBuilderFundTransferTransactionObserver>().LifeStyle.Transient);

            builder.Register(Component.For<IStoreBalancePeriodObserver>().Named("LogBuilderStoreBalancePeriodObserver")
                                      .ImplementedBy<LogBuilderStoreBalancePeriodObserver>().LifeStyle.Transient);

            builder.Register(Component.For<IExtensionDataExtractor>().Named("StoreBalancePeriodDataExtractor")
                .ImplementedBy<StoreBalancePeriodDataExtractor>().LifeStyle.Transient);

            builder.Register(Component.For<IStoreBalancePeriodSettleValidator>().Named("AllAccountsApprovedValidator")
                                      .ImplementedBy<AllAccountsApprovedValidator>().LifeStyle.Transient);

            builder.Register(Component.For<IStoreBalancePeriodSettleValidator>().Named("AllAccountBalancesHasEndedValidator")
                                      .ImplementedBy<AllAccountBalancesHasEndedValidator>().LifeStyle.Transient);

            builder.Register(Component.For<IStoreBalancePeriodSettleValidator>().Named("PreviousPeriodSettledValidator")
                                      .ImplementedBy<PreviousPeriodSettledValidator>().LifeStyle.Transient);

            builder.Register(Component.For<IStoreBalancePeriodSettleValidator>().Named("MatchingPeriodsAreSettledValidator")
                                      .ImplementedBy<MatchingPeriodsAreSettledValidator>().LifeStyle.Transient);

            builder.Register(Component.For<IOpenAccountBalanceFinder>().Named("OpenAccountBalanceFinder")
                                    .ImplementedBy<OpenAccountBalanceFinder>().LifeStyle.Transient);

            builder.Register(Component.For<IFundTransferTransactionLogDao>().Named("FundTransferTransactionLogDao").ImplementedBy<FundTransferTransactionLogDao>().LifeStyle.Transient);
            builder.Register(Component.For<IEntityDao>().Named("FTLogs").ImplementedBy<FundTransferTransactionLogDao>().LifeStyle.Transient);

            builder.RegisterMapper<FundTransferTransactionDocumentToTransactionDisplayHeaderDtoMapper>();

            builder.RegisterMapper<DeclarationTransactionDocumentToTransactionDisplayHeaderDtoMapper>();

            builder.Register(Component.For<IFundTransferTLogReaderFactory>().Named("FundTransferTLogReaderFactory")
                         .ImplementedBy<FundTransferTLogReaderFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IFundTransferTransactionLogBuilder>().Named("FundTransferTransactionLogBuilder")
                         .ImplementedBy<FundTransferTransactionLogBuilder>().LifeStyle.Transient);

            builder.Register(
                Component.For<IFundTransferCounterIncrementer>().Named("FundTransferCounterIncrementer")
                        .ImplementedBy<FundTransferCounterIncrementer>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDeclarationTransaction>().Named("IDeclarationTransaction").ImplementedBy
                    <DeclarationTransaction>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDeclarationTransactionObserver>().Named("IDeclarationTransactionObserver").ImplementedBy
                    <LogBuilderDeclarationTransactionObserver>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDeclarationTransactionLogDao>().Named("IDeclarationTransactionLogDao").ImplementedBy
                    <DeclarationTransactionLogDao>().LifeStyle.Transient);

            builder.Register(Component.For<IDocumentDao>().ImplementedBy<FundTransferTransactionLogDao>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDocumentDao>().Named("DeclarationTransactionLogDao").ImplementedBy
                    <DeclarationTransactionLogDao>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDeclarationTransactionLogReader>().Named("DeclarationTransactionLogReader").ImplementedBy
                    <DeclarationTransactionLogReader>().LifeStyle.Transient);

            builder.Register(
                Component.For<IActivityConfiguration>().Named("IActivityConfiguration").ImplementedBy
                    <ActivityConfiguration>().LifeStyle.Transient);

            builder.Register<IActivityReferenceConfiguration, ActivityReferenceConfiguration>();

            builder.Register(Component.For<IReceiptFormatterAdaptor<IFundTransferTransactionLog>>()
                                      .Named("FundTransferReceiptAdaptor").ImplementedBy<FundTransferReceiptAdaptor>().
                                       LifeStyle.Transient);

            builder.Register(Component.For<IReceiptFormatterAdaptor<Dictionary<string, string>>>()
                                      .Named("CashierReceiptFormatterAdaptor")
                                      .ImplementedBy<CashierReceiptFormatterAdaptor>()
                                      .LifeStyle.Transient);

            builder.Register(Component.For<IDrawer>().Named("IDrawer").ImplementedBy<Drawer>().LifeStyle.Transient);

            builder.Register(Component.For<IColdstartActivity>().Named("IColdstartActivity").ImplementedBy<ColdstartActivity>().LifeStyle.Transient);

            builder.RegisterSingleton<IUserFactory, IFactory<IUser>, UserFactory>();

            builder.Register<IConsumableGroupDao, ConsumableGroupDao>();


            builder.Register(Component.For<IProductRemotableDao>().Named("IProductRemotableDao").ImplementedBy<ProductRemotableDao>().LifeStyle.Singleton);
            builder.Register(Component.For<IMenuconfigurationProductRemotableDao>().Named("IMenuconfigurationProductRemotableDao").ImplementedBy<MenuconfigurationProductRemotableDao>().LifeStyle.Singleton);
            builder.Register(Component.For<IMenuConfigurationProductDao>().Named("IMenuConfigurationProductDao").ImplementedBy<MenuConfigurationProductDao>().LifeStyle.Singleton);

            builder.Register(Component.For<StoreServices.Model.Infrastructure.Legacy.Bulk.ILinkGroupRemotableDao>().Named("ILinkGroupRemotableDao").ImplementedBy<LinkGroupRemotableDao>().LifeStyle.Singleton);
            builder.Register(Component.For<StoreServices.Connectivity.Product.Associations.ILinkGroupRemotableDao>().Named("ILinkGroupRemotableDao_Obselete").ImplementedBy<LinkGroupRemotableDao>().LifeStyle.Singleton);

            builder.Register(
              Component.For<IAccessServicesEntityLoader>().Named("ProductEntityLoader").ImplementedBy<ProductEntityLoader>().LifeStyle.
                        Singleton);

            builder.RegisterSingleton<IProductDao, IProductMovableDao, IEntityDao, ProductDao>("products");

            builder.Register(
                Component.For<ISecurityScaleDataDao>().Named("ISecurityScaleDataDao").ImplementedBy
                    <SecurityScaleDataDao>().LifeStyle.
                          Singleton);

            builder.Register(
                Component.For<ISecurityScaleData>().Named("ISecurityScaleData").ImplementedBy
                    <SecurityScaleData>().LifeStyle.
                          Transient);

            builder.Register(
                Component.For<IProduct>().Named("ConsumableProduct").ImplementedBy
                    <Consumable>().LifeStyle.
                          Singleton);

            builder.RegisterSingleton<ICalorieValueProvider, CalorieValueProvider>("ICalorieValueProvider");
            builder.RegisterSingleton<ICalorieValueRounding, CalorieValueRounding>("ICalorieValueRounding");

            builder.Register(
                Component.For<IKitGroupMemberSorter>().Named("IKitGroupMemberSorter").ImplementedBy
                    <KitGroupMemberSorter>().LifeStyle.
                          Singleton);

            builder.Register(
                Component.For<IBulkPersisterProvider>().Named("IBulkPersisterProvider").ImplementedBy
                    <BulkPersisterProvider>().LifeStyle.
                          Singleton);

            builder.Register(
                Component.For<IProductBulkPersister>()
                         .Named(typeof(IProductBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlProductBulkPersister>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IProductRestrictionBulkPersister>()
                         .Named(typeof(IProductRestrictionBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlProductRestrictionBulkPersister>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IStoreRangeBulkPersister>()
                         .Named(typeof(IStoreRangeBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlStoreRangeBulkPersister>().LifeStyle.Transient);

            builder.Register(
                Component.For<IPriceBulkPersister>()
                         .Named(typeof(IPriceBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlPriceBulkPersister>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ILegacyProductBulkPersister>()
                         .Named(typeof(ILegacyProductBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlLegacyProductBulkPersister>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ICategoryBulkPersister>()
                         .Named(typeof(ICategoryBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlCategoryBulkPersister>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IManufacturerBulkPersister>()
                         .Named(typeof(IManufacturerBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlManufacturerBulkPersister>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IProductClassificationBulkPersister>()
                         .Named(typeof(IProductClassificationBulkPersister).Name +
                                "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlProductClassificationBulkPersister>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IProductToGroupBulkPersister>()
                         .Named(typeof(IProductToGroupBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlProductToGroupBulkPersister>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ISecurityScaleBulkPersister>()
                         .Named(typeof(ISecurityScaleBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlSecurityScaleBulkPersister>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ILinkGroupBulkPersister>()
                         .Named(typeof(ILinkGroupBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlLinkGroupBulkPersister>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IProductToLinkGroupBulkPersister>()
                         .Named(typeof(IProductToLinkGroupBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlProductToLinkGroupBulkPersister>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ITouchPointBulkPersister>()
                    .Named(typeof(ITouchPointBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                    .ImplementedBy<SqlTouchPointBulkPersister>().LifeStyle.Singleton);

            builder.Register(
               Component.For<IAccountTillSqlBulkPersister>()
                   .Named(typeof(IAccountTillSqlBulkPersister).Name + "_System.Data.SqlClient.SqlConnection")
                   .ImplementedBy<AccountTillSqlBulkPersister>().LifeStyle.Singleton);

            builder.Register(Component.For<IBulkPersisterBehaviour>()
              .Named("IBulkPersisterBehaviour")
              .ImplementedBy<BulkPersisterBehaviour>().LifeStyle.Transient);

            builder.RegisterSingleton<ISearchCriterionProvider, ContainsSearchCriterionProvider>();

            builder.Register(
                Component.For<ICommandProvider>()
                         .Named(typeof(ICommandProvider).Name + "_System.Data.SqlClient.SqlConnection")
                         .ImplementedBy<SqlCommandProvider>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ICommandProvider>()
                         .Named(typeof(ICommandProvider).Name + "_System.Data.SQLite.SQLiteConnection")
                         .ImplementedBy<SqlLiteCommandProvider>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IReasonCodeDao>().Named("IReasonCodeDao").ImplementedBy<ReasonCodeDao>().LifeStyle.
                          Singleton);

            builder.Register(
                Component.For<IReasonCodeFactory>().Named("IReasonCodeFactory").ImplementedBy<ReasonCodeFactory>().
                          LifeStyle.
                          Singleton);

            builder.Register(
                Component.For<INackTokenStatusDao>().Named("INackTokenStatusDao").ImplementedBy<NackTokenStatusDao>().
                          LifeStyle.
                          Singleton);

            builder.Register(
                Component.For<IEntityBehaviorDao>().Named("IEntityBehaviorDao").ImplementedBy<EntityBehaviorDao>().
                          LifeStyle.
                          Singleton);
            builder.Register(
                Component.For<IMobileFarmStoreDistributionDao>().Named("IMobileFarmStoreDistributionDao").ImplementedBy<MobileFarmStoreDistributionDao>().LifeStyle.Singleton);

            builder.RegisterSingleton<IProductFactory, ConsumableFactory>();

            builder.RegisterSingleton<ISecurityScaleDataFactory, SecurityScaleDataFactory>();

            builder.Register(
                Component.For<IConsumableGroupFactory>().Named("IConsumableGroupFactory").ImplementedBy
                    <ConsumableGroupFactory>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IAdvertisementPromotionDao>().Named("IAdvertisementPromotionDao").ImplementedBy
                    <AdvertisementPromotionDao>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IAdvertisementChannelDao>().Named("IAdvertisementChannelDao").ImplementedBy
                    <AdvertisementChannelDao>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IAdvertisementCategoryDao>().Named("IAdvertisementCategoryDao").ImplementedBy
                    <AdvertisementCategoryDao>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IAdvertisementFactory>().Named("IAdvertisementFactory").ImplementedBy
                    <AdvertisementFactory>().LifeStyle.Transient);

            builder.Register(
                Component.For<ICouponLineFactory>().Named("ICouponLineFactory").ImplementedBy
                    <CouponLineFactory>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("CouponLimitsRuleExpressionAdaptor").ImplementedBy
                    <CouponLimitsRuleExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("GiftCardRuleExpressionAdaptor").ImplementedBy
                    <GiftCardRuleExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleBehavior>().Named("GiftCardVoidedRestrictionBehavior").ImplementedBy
                    <GiftCardVoidedRestrictionBehavior>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("CouponOverrideRuleExpressionAdaptor").ImplementedBy
                    <CouponOverrideRuleExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("CouponErrorExpressionAdaptor").ImplementedBy
                    <CouponErrorExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("IncentiveMessageExpressionAdaptor").ImplementedBy
                    <IncentiveMessageExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("AlertRuleExpressionAdaptor").ImplementedBy
                    <AlertRuleExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("AlertDeviceRuleExpressionAdaptor").ImplementedBy
                    <AlertDeviceRuleExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("PromotionFailureAlertRuleExpressionAdaptor").ImplementedBy
                    <PromotionFailureAlertRuleExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("CustomerExpressionAdaptor").ImplementedBy
                    <CustomerExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("AlertTouchPointRuleExpressionAdaptor").ImplementedBy
                    <AlertTouchPointRuleExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IEncryptor>().Named("PasswordEncryptor").ImplementedBy<SecurityProvider>().LifeStyle.Transient);

            builder.Register(
                Component.For<IEncryptor>().Named("PinEncryptor").ImplementedBy<SecurityProvider>().LifeStyle.Transient);

            builder.Register(
                Component.For<IEncryptor>().Named("BarcodeEncryptor").ImplementedBy<SHA512Encryptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IHashAlgorithm>().Named("IHashAlgorithm").ImplementedBy<DefaultHashAlgorithm>().LifeStyle.
                          Transient);

            builder.Register(
                Component.For<ISingleSignOnIndicator>().Named("ISingleSignOnIndicator").
                          ImplementedBy<SingleSignOnValidator>().LifeStyle.
                          Transient);

            builder.Register(
                Component.For<IClientSessionDao>().Named("IClientSessionDao").ImplementedBy<ClientSessionDao>()
                         .LifeStyle.Singleton);

            builder.Register(
                Component.For<IDateTimeService>().Named("IDateTimeService").ImplementedBy<SystemDateTimeService>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<INumericMonetaryAmountToWordsConverter>().Named("INumericMonetaryAmountToWordsConverter").ImplementedBy<NumericMonetaryAmountToWordsConverter>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<IPerformanceMonitorService>().Named("IPerformanceMonitorService").ImplementedBy
                    <PerformanceMonitorService>().LifeStyle.Singleton);

            builder.Register<IDepartmentDao, DepartmentDao>();

            builder.Register(Component.For<IRetailTransactionLogReader>().Named("RetailTransactionLogReader")
                .ImplementedBy<RetailTransactionLogContractReader>().LifeStyle.Transient);

            builder.Register(Component.For<ITransactionTypeNameStrategy>().Named("ControlTransactionTypeNameStrategy")
                .ImplementedBy<TransactionTypeNameStrategy>().LifeStyle.Transient);

            builder.Register(Component.For<IReturnEngine>().Named("ReturnEngine").ImplementedBy<ReturnEngine>().LifeStyle.Transient);

            builder.RegisterSingleton<IUpc5FaceValueDao, Upc5FaceValueDao>();

            builder.RegisterSingleton<ICouponParametersDao, CouponParametersDao>();

            builder.RegisterSingleton<ICouponSeriesDao, CouponSeriesDao>();

            builder.RegisterSingleton<ICouponProvider, CouponProvider>();

            builder.RegisterSingleton<ICouponParametersFactory, CouponParametersFactory>();

            builder.RegisterSingleton<ISecondPeConnectionConfigurationSpe, PromotionSecondPeConnectionConfigurationSpe>("PromotionSecondPeConnectionConfiguration");
            builder.RegisterSingleton<IPromotionSecondPeConnectionConfigurationSpe, PromotionSecondPeConnectionConfigurationSpe>();

            builder.RegisterSingleton<IAccountDao, AccountDao>("IAccountDao");

            builder.RegisterSingleton<ICashOfficeActivityLogDao, CashOfficeActivityLogDao>("ICashOfficeActivityLogDao");

            builder.Register<IEventHandler<OnLoginFinishedEvent>, DrawerLimitBalanceCalculatorEventHandler>("OnLoginFinishedEvent");

            builder.Register<IEventHandler<OnEndOfDayFinishedEvent>, DrawerLimitBalanceCalculatorEventHandler>("OnEndOfDayFinishedEvent");

            builder.Register<IEventHandler<ShiftAccountSavedEvent>, DrawerLimitOnShiftStartEndedEventHandler>("ShiftAccountSavedEvent");

            builder.Register(Component.For<IDrawerLimitOpeningFundsConfigurationHandler>().Named("IDrawerLimitOpeningFundsConfigurationHandler").ImplementedBy
                   <DrawerLimitOpeningFundsConfigurationHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IDrawerLimitBalanceCalculatorHandlerFactory>().Named("IDrawerLimitBalanceCalculatorHandlerFactory").ImplementedBy
                   <DrawerLimitBalanceCalculatorHandlerFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IDrawerLimitBalanceCalculatorHandler>().Named("CashierModeDrawerLimitBalanceCalculatorHandler").ImplementedBy
                   <CashierModeDrawerLimitBalanceCalculatorHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IDrawerLimitBalanceCalculatorHandler>().Named("DrawerModeDrawerLimitBalanceCalculatorHandler").ImplementedBy
                   <DrawerModeDrawerLimitBalanceCalculatorHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IDrawerLimitBalanceCalculatorHandler>().Named("PosModeDrawerLimitBalanceCalculatorHandler").ImplementedBy
                   <PosModeDrawerLimitBalanceCalculatorHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZSalesAggregator").ImplementedBy<XZSalesAggregator>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZTaxAggregator").ImplementedBy<XZTaxAggregator>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZPromotionsAggregator").ImplementedBy<XZPromotionsAggregator>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZVoidsAggregator").ImplementedBy<XZVoidsAggregator>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZItemVoidsAggregator").ImplementedBy<XZItemVoidsAggregator>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZReturnsAggregator").ImplementedBy<XZReturnsAggregator>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZReportsTaxAmountDataAggregator").ImplementedBy<XZReportsTaxAmountDataAggregator>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZReportsNetAmountAggregator").ImplementedBy<XZReportsNetAmountAggregator>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZNoSalesAggregator").ImplementedBy<XZNoSalesAggregator>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZLoginsAggregator").ImplementedBy<XZLoginsAggregator>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZCashbacksAggregator").ImplementedBy<XZCashbacksAggregator>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZTransactionTotalsAggregator").ImplementedBy<XZTransactionTotalsAggregator>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsDataAggregator>().Named("XZCashOfficeActivitiesAggregator").ImplementedBy<XZCashOfficeActivitiesAggregator>().LifeStyle.Transient);

            builder.RegisterQuery<AccountQuery>();

            builder.RegisterQuerySpecificationApplier<AccountProfileExternalIdsSpecficationApplier>();

            builder.RegisterQuerySpecificationApplier<AccountPagingSpecificationApplier>();

            builder.RegisterSingleton<IAccountFactory, AccountFactory>("IAccountFactory");

            builder.Register(
                Component.For<IUnitOfWorkCompleteObserver>().Named("AccountAddedUnitOfWorkComplete").
                          ImplementedBy<AccountAddedUnitOfWorkComplete>().LifeStyle.Transient);
            builder.Register(
                Component.For<IAccountAvailableAcitvitiesProvider>().Named("IAccountAvailableAcitvitiesProvider").
                    ImplementedBy<AccountAvailableAcitvitiesProvider>().LifeStyle.
                    Singleton);

            builder.RegisterSingleton<IAccountMovableConverter, AccountMovableConverter>("IAccountMovableConverter");

            builder.Register(
                Component.For<IStoreBalancePeriodDao>().Named("IStoreBalancePeriodDao").ImplementedBy
                    <StoreBalancePeriodDao>().LifeStyle.Transient);

            builder.Register(Component.For<IXZReportCountersDao>().Named("IXZReportCountersDao").ImplementedBy<XZReportDataDao>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportAggregationDao>().Named("IXZReportAggregationDao").ImplementedBy<XZReportsAggregationSPDao>().LifeStyle.Transient);
            builder.Register(Component.For<IXZReportsConfigurationDao>().Named("IXZReportsConfigurationDao").ImplementedBy<XZReportsConfigurationDao>().LifeStyle.Transient);

            builder.Register(Component.For<IReportTypeSelection>().Named("IReportTypeSelection").ImplementedBy<ReportTypeSelection>().LifeStyle.Transient);

            builder.Register(
                Component.For<IShiftAccountDao>().Named("IShiftAccountDao").ImplementedBy
                    <ShiftAccountDao>().LifeStyle.Singleton);

            builder.Register<ISettleConfiguration, SettleConfiguration>();
            builder.RegisterMapper<SettleConfigurationMapper>();
            builder.RegisterMapper<SettleConfigurationTypeMapper>();

            builder.RegisterMapper<ConnectionConfigurationTypeMapperSpe>();
            builder.RegisterMapper<ConnectionConfigurationMapperSpe>();

            builder.Register(
                Component.For<ICashOfficeActivityAuthorizationStrategy>()
                    .Named("SourceCashOfficeActivityAuthorizationStartegy")
                    .ImplementedBy<SourceCashOfficeActivityAuthorizationStartegy>()
                    .LifeStyle.Singleton
                );

            builder.Register(
                Component.For<ICashOfficeActivityAuthorizationStrategy>()
                    .Named("DestinationCashOfficeActivityAuthorizationStartegy")
                    .ImplementedBy<DestinationCashOfficeActivityAuthorizationStartegy>()
                    .LifeStyle.Singleton
                );
            builder.Register(
                Component.For<IStoreBalancePeriodConfigurationDao>()
                    .Named("IStoreBalancePeriodConfigurationDao")
                    .ImplementedBy
                    <StoreBalancePeriodConfigurationDao>().LifeStyle.Singleton);

            builder.Register(
             Component.For<IBalanceTenderAmountsHandler>().Named("IBalancTenderAmountsHandler").ImplementedBy
                   <BalanceTenderAmountsHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IAccountBalanceIdSetterFactory>().Named("IAccountBalanceIdSetterFactory")
                .ImplementedBy<AccountBalanceIdSetterFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IAccountBalanceIdSetter>().Named("Pos").ImplementedBy<TillAccountBalanceIdSetter>().LifeStyle.Transient);

            builder.Register(Component.For<IAccountBalanceIdSetter>().Named("Cashier").ImplementedBy<CashierAccountBalanceIdSetter>().LifeStyle.Transient);

            builder.Register(Component.For<IAccountBalanceIdSetter>().Named("Drawer").ImplementedBy<DrawerAccountBalanceIdSetter>().LifeStyle.Transient);

            builder.Register(
                Component.For<IFundTransferTransactionLogAdjustmentTransaction>()
                    .Named("IFundTransferTransactionLogAdjustmentTransaction")
                    .ImplementedBy
                    <FundTransferTransactionLogAdjustmentTransaction>().LifeStyle.Transient);
            builder.Register(
                Component.For<IFundTransferTransactionLogValidator>()
                    .Named("LogIsNotInSettledPeriodValidator")
                    .ImplementedBy
                        <LogIsNotInSettledPeriodValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IFundTransferTransactionLogValidator>()
                    .Named("NotAutoReconcileValidator")
                    .ImplementedBy
                        <NotAutoReconcileValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IFundTransferTransactionLogValidator>()
                    .Named("PositiveAmountValidator")
                    .ImplementedBy
                        <PositiveAmountValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IDeclarationTransactionValidator>()
                    .Named("DeclarationNotAlreadyClosedValidator")
                    .ImplementedBy
                        <StoreServer.BusinessComponents.Finance.Declaration.Validators.NotAlreadyClosedValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IDeclarationTransactionValidator>()
                    .Named("DeclarationPositiveAmountValidator")
                    .ImplementedBy
                        <StoreServer.BusinessComponents.Finance.Declaration.Validators.PositiveAmountValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IFundTransferTransactionValidator>()
                    .Named("FundTransferPositiveAmountValidator")
                    .ImplementedBy
                        <StoreServer.BusinessComponents.Finance.FundTransfer.Validators.PositiveAmountValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IFundTransferTransactionValidator>()
                    .Named("FundTransferNotAlreadyClosedValidator")
                    .ImplementedBy
                        <StoreServer.BusinessComponents.Finance.FundTransfer.Validators.NotAlreadyClosedValidator>().LifeStyle.Transient);
            builder.Register(Component.For<IFundTransferTransactionClosingValidator>()
                    .Named("ValidateNotAlreadyClosed").ImplementedBy<StoreServer.BusinessComponents.Finance.FundTransfer.Validators.ValidateNotAlreadyClosed>().LifeStyle.Transient);
            builder.Register(Component.For<IFundTransferTransactionClosingValidator>()
                .Named("ValidateLinesIsNotEmpty").ImplementedBy<StoreServer.BusinessComponents.Finance.FundTransfer.Validators.ValidateLinesIsNotEmpty>().LifeStyle.Transient);
            builder.Register(Component.For<IFundTransferTransactionClosingValidator>()
                .Named("ValidateCreationDateNotInFuture").ImplementedBy<StoreServer.BusinessComponents.Finance.FundTransfer.Validators.ValidateCreationDateNotInFuture>().LifeStyle.Transient);
            builder.Register(Component.For<IFundTransferTransactionClosingValidator>()
                .Named("ValidateAccounts").ImplementedBy<StoreServer.BusinessComponents.Finance.FundTransfer.Validators.ValidateAccounts>().LifeStyle.Transient);
            builder.Register(Component.For<IFundTransferTransactionClosingValidator>()
               .Named("ValidateTillBusinessDate").ImplementedBy<StoreServer.BusinessComponents.Finance.FundTransfer.Validators.ValidateTillBusinessDate>().LifeStyle.Transient);
            builder.Register(Component.For<IFundTransferTransactionClosingValidator>()
                .Named("ValidateSafeBusinessDate").ImplementedBy<StoreServer.BusinessComponents.Finance.FundTransfer.Validators.ValidateSafeBusinessDate>().LifeStyle.Transient);
            builder.Register(Component.For<IFundTransferTransactionClosingValidator>().Named("ValidateBankingSafeBusinessDate").ImplementedBy<StoreServer.BusinessComponents.Finance.FundTransfer.Validators.ValidateBankingSafeBusinessDate>().LifeStyle.Transient);

            builder.Register(Component.For<IFundTransferTransactionClosingValidator>()
                .Named("ValidateNotSettledPeriodForBusinessDate").ImplementedBy<StoreServer.BusinessComponents.Finance.FundTransfer.Validators.ValidateNotSettledPeriodForBusinessDate>().LifeStyle.Transient);

            builder.Register(Component.For<IBalanceTenderMovement>().Named("IBalanceTenderAmount").ImplementedBy<PersistBalanceTenderMovement>().LifeStyle.Transient);

            builder.Register(
                Component.For<IBalanceDtoDao>().Named("IBalanceDtoDao").
                ImplementedBy<BalanceDtoDao>().LifeStyle.Transient);

            builder.Register(
                Component.For<IAccountBalancingDao>().Named("IAccountBalancingDao").ImplementedBy
                    <AccountBalancingDao>().LifeStyle.Transient);
            builder.Register(
                Component.For<ITenderExchangeDao>().Named("ITenderExchangeDao").ImplementedBy<TenderExchangeDao>().
                          LifeStyle.Transient);

            builder.Register(
               Component.For<IRequiredPaymentParametersValidator>().Named("DefaultCheckValidator").ImplementedBy<DefaultCheckValidator>().
                         LifeStyle.Transient);

            builder.Register(
                Component.For<ITenderRoundingValidator>().Named("TenderRoundingValidator").ImplementedBy<TenderRoundingValidator>().
                    LifeStyle.Transient);

            builder.Register(
              Component.For<ICouponIssueWarningBuilder>().Named("CouponIssueWarningBuilder").ImplementedBy<CouponIssueWarningBuilder>().
                  LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleFactory>().Named("IRuleFactory").ImplementedBy<RuleFactory>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("ItemRuleExpressionAdaptor").ImplementedBy
                    <ItemRuleExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("IDepartmentExpressionAdaptor").ImplementedBy
                    <DepartmentRuleExpressionAdaptor>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("TenderRuleExpressionAdapter").ImplementedBy
                    <TenderRuleExpressionAdapter>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("TransactionRuleExpressionAdaptor").ImplementedBy
                    <TransactionRuleExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("DateTimeRuleExpressionAdaptor").ImplementedBy
                    <DateTimeRuleExpressionAdaptor>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("BRMInputRuleExpressionAdaptor").ImplementedBy
                    <BRMInputRuleExpressionAdaptor>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named("ApplicationUserRuleExpressionAdaptor").ImplementedBy
                    <ApplicationUserRuleExpressionAdaptor>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IRuleExpressionAdaptor>().Named(typeof(ReturnRuleExpressionAdaptor).Name).ImplementedBy
                    <ReturnRuleExpressionAdaptor>().LifeStyle.Transient);

            builder.Register(
                Component.For<IIdmConfigurationDao>().Named("IIdmConfigurationDao").ImplementedBy
                    <IdmConfigurationDao>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IGuidGenerator>().Named("IGuidGenerator").ImplementedBy
                    <NHibernateGuidCombGenerator>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IUnitOfWorkCompleteObserver>().Named("DmsUnitOfWorkCompleteObserver").ImplementedBy
                    <DmsUnitOfWorkCompleteObserver>().LifeStyle.Transient);

            builder.Register(
                Component.For<IUnitOfWorkCompleteObserver>().Named("UnitOfWorkCompleteWarningsLoggerObserver").ImplementedBy
                    <UnitOfWorkCompleteWarningsLoggerObserver>().LifeStyle.Transient);
            builder.Register(
                Component.For<IMessagePublisher>().Named("IMessagePublisher").ImplementedBy
                    <MessagePublisher>().LifeStyle.Transient);

            builder.Register(
                Component.For<IMessageApplier>().Named("IMessageApplier").ImplementedBy
                    <MessageApplier>().LifeStyle.Transient);

            builder.Register(
                Component.For<IMessageApplyHandler>().Named("IMessageApplyHandler").ImplementedBy
                    <MessageApplyHandler>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IMessageForTransferConverter>().Named("IMessageForTransferConverter").ImplementedBy
                    <MessageForTransferConverter>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IMovableToPayloadConverter>().Named("IMovableToPayloadConverter").ImplementedBy
                    <MovableToPayloadConverter>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IContextPropertyOriginatorHandler>().Named("IContextPropertyOriginatorHandler").ImplementedBy
                    <ContextPropertyOriginatorHandler>().LifeStyle.Singleton);

            builder.RegisterSingleton<IAffectedServersCalculator, AffectedServersCalculator>();

            builder.Register(Component.For<INackCreator>().Named("INackCreator").ImplementedBy<NackCreator>().LifeStyle.Singleton);

            builder.Register(Component.For<IFailedTokenErrorHandler>().Named("FailedTokenErrorHandler").ImplementedBy<FailedTokenErrorHandler>().LifeStyle.Singleton);

            builder.Register(Component.For<IContextPropertiesPromoter>().Named("IContextPropertiesPromoter").ImplementedBy<ContextPropertiesPromoter>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IMessageSender>().Named("IMessageSender").ImplementedBy
                    <UploadDownloadTransportAdaptor>().LifeStyle.Singleton);

            builder.Register(
               Component.For<IMessageSender>().Named("RabbitUploadPublisher").ImplementedBy
                   <RabbitUploadPublisher>().LifeStyle.Singleton);

            builder.Register(
               Component.For<IMessageSender>().Named("RabbitDownloadPublisher").ImplementedBy
                   <RabbitDownloadPublisher>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ITransportationMessageConverter>().Named("ITransportationMessageConverter").ImplementedBy
                    <TransportationMessageProtobuffConverter>().LifeStyle.Singleton);

            builder.Register(Component.For<ITransportInboxFailureHandler>().Named("ITransportInboxFailureHandler").ImplementedBy
                    <TransportInboxFailureHandler>().LifeStyle.Singleton);

            builder.RegisterSingleton<IChannelCreator, IChannelCreator<IModel, QueueDeclareOk>, ChannelCreator>("ChannelCreator");

            builder.Register(
               Component.For<IChannelConfiguration>().Named("ChannelConfiguration").ImplementedBy
                   <ChannelConfiguration>().LifeStyle.Transient);

            builder.Register(
               Component.For<IConnectionCreator>().Named("ConnectionCreator").ImplementedBy
                   <ConnectionCreator>().LifeStyle.Singleton);

            builder.Register(
               Component.For<IRabbitServersMapper>().Named("RabbitServersMapper").ImplementedBy
                   <RabbitServersMapper>().LifeStyle.Singleton);

            builder.RegisterSingleton<MapperConverterStrategy>();

            builder.RegisterSingleton<ConverterPerVersionStrategy>();

            builder.Register(
                Component.For<IConverterResolver>().Named("IConverterResolver").ImplementedBy
                    <ConverterResolver>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IMovableResolverCache>().Named("IMovableResolverCache").ImplementedBy
                    <MovableResolverCache>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IMultiVersionPayloadConverter>().Named("IPayloadConverter").ImplementedBy
                    <MultiVersionPayloadConverter>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IEntityChangeMessageBuilder>().Named("EntityChangeMessageBuilder").ImplementedBy
                    <EntityChangeMessageBuilder>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IOrganizationInfoRepository>().Named("IOrganizationInfoRepository").ImplementedBy
                    <OrganizationInfoRepository>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IMovableApplier>().Named("IMovableApplier").ImplementedBy
                    <MovableApplier>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ITenderChangeCalculator>().Named("ITenderChangeCalculator").ImplementedBy<TenderChangeCalculator>().LifeStyle.Singleton);

            builder.RegisterSingleton<IBusinessActivityPermissionsHandler, BusinessActivityPermissionsHandler>("IBusinessActivityPermissionsHandler");

            builder.Register(
                Component.For<IBusinessActivityController>().Named("IBusinessActivityController").ImplementedBy
                    <BusinessActivityController>().LifeStyle.Singleton);

            builder.Register(Component.For<IEntityConfigurationProvider>().Named("IEntityConfigurationProvider").ImplementedBy<EntityConfigurationProvider>().LifeStyle.Singleton);

            builder.Register(Component.For<ITokenStatusFactory>().Named("ITokenStatusFactory").ImplementedBy<TokenStatusFactory>().LifeStyle.Singleton);

            builder.Register(Component.For<ICountryInfo>().Named("ICountryInfo").ImplementedBy<CountryInfo>().LifeStyle.Singleton);

            builder.Register(Component.For<IStoreBalancingStrategy>().Named("TillBalancingStrategy").ImplementedBy<TillBalancingStrategy>().LifeStyle.Transient);

            builder.Register(Component.For<IStoreBalancingStrategy>().Named("SafeBalancingStrategy").ImplementedBy<SafeBalancingStrategy>().LifeStyle.Transient);

            builder.Register(
               Component.For<IBusinessDaySelector<IFundTransferTransactionLog>>().Named("BusinessDaySelector").ImplementedBy
                   <BusinessDaySelector>().LifeStyle.Transient);

            builder.Register(Component.For<ITouchPointOnSaveObserver>().Named("ITouchPointOnSaveObserver").ImplementedBy<TouchPointRepositoryObserver>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDeclarationTransactionLogDaoObserver>().Named(
                    "UpdateAccountBalanceDeclarationTransactionLogDaoObserver")
                         .ImplementedBy<UpdateAccountBalanceDeclarationTransactionLogDaoObserver>().LifeStyle.Transient);

            builder.Register(Component.For<IShiftAccount>().Named("ShiftAccount").ImplementedBy<ShiftAccount>().LifeStyle.Transient);

            builder.Register(Component.For<IBalancePos>().Named("BalancePos").ImplementedBy<BalancePos>().LifeStyle.Transient);
            builder.Register(Component.For<IBalanceShiftAccount>().Named("BalanceShiftAccount").ImplementedBy<BalanceShiftAccount>().LifeStyle.Transient);


            builder.RegisterSingleton<IBalancePeriodSchedulingConfigurationFactory, BalancePeriodSchedulingConfigurationFactory>("IBalancePeriodSchedulingConfigurationFactory");

            builder.RegisterSingleton<IBalancePeriodSchedulingConfigurationDao, BalancePeriodSchedulingConfigurationDao>("IBalancePeriodSchedulingConfigurationDao");

            builder.Register(
                Component.For<IConnectionStringConfigurationKey>().Named("IConnectionStringConfigurationKey").
                          ImplementedBy
                    <ConnectionStringConfigurationKey>().LifeStyle.Transient);

            builder.Register(
                Component.For<IStoreBalancePeriodFactory>().Named("IStoreBalancePeriodFactory").ImplementedBy
                    <StoreBalancePeriodFactory>().LifeStyle.Transient);

            builder.Register(
                Component.For<IAuthenticationPolicyFactory>().Named("IAuthenticationPolicyFactory").ImplementedBy
                    <AuthenticationPolicyFactory>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IUsernamePasswordCredentials>().Named("UsernamePasswordCredentials").ImplementedBy
                    <UsernamePasswordCredentials>().LifeStyle.Transient);

            builder.Register(
                Component.For<ICredentialProvider>().Named("CredentialProvider").ImplementedBy
                    <CredentialProvider>().LifeStyle.Transient);

            builder.Register(
                Component.For<IUsernameCertificateCredentials>().Named("UsernameCertificateCredentials").ImplementedBy
                    <UsernameCertificateCredentials>().LifeStyle.Transient);

            builder.Register(
                Component.For<IPasswordHistory>().Named("PasswordHistoryDto").ImplementedBy
                    <PasswordHistory>().LifeStyle.Transient);

            builder.Register(
                Component.For<IUsernamePinCredentials>().Named("UsernamePinCredentials").ImplementedBy
                    <UsernamePinCredentials>().LifeStyle.Transient);

            builder.Register(
                Component.For<IBarcodeCredentials>().Named("BarcodeCredentials").ImplementedBy
                    <BarcodeCredentials>().LifeStyle.Transient);

            builder.Register(
                Component.For<IActiveDirectoryCredentials>().Named("ActiveDirectoryCredentials").ImplementedBy
                    <ActiveDirectoryCredentials>().LifeStyle.Transient);

            builder.Register(
                Component.For<IStoredCredentialsFactory>().Named("IPrincipleFactory").ImplementedBy
                    <StoredCredentialsFactory>().LifeStyle.Singleton);

            builder.Register(
               Component.For<IStoredCredentials>().Named("Barcode").ImplementedBy
                   <BarcodeStoredCredentials>().LifeStyle.Transient);

            builder.Register(
                Component.For<IStoredCredentials>().Named("Pin").ImplementedBy
                    <PinStoredCredentials>().LifeStyle.Transient);

            builder.Register(
                Component.For<IStoredCredentials>().Named("Password").ImplementedBy
                    <PasswordStoredCredentials>().LifeStyle.Transient);

            builder.RegisterSingleton<ISecurityContextFactory, SecurityContextFactory>("ISecurityContextFactory");

            builder.RegisterSingleton<ISecurityWeightToleranceDao, SecurityWeightToleranceDao>();

            builder.RegisterSingleton<ISecurityWeightToleranceFactory, SecurityWeightToleranceFactory>();

            builder.Register(
                Component.For<IQuerySpecificationApplier<IReturnPolicy, ReturnPolicyReasonCodeIdSpecification>>()
                    .Named("ReturnPolicyReasonCodeIdSpecificationApplier")
                    .ImplementedBy<ReturnPolicyReasonCodeIdSpecificationApplier>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IEventHandler<ReasonCodeDeletingEvent>>()
                .Named("SettlePeriodReasonCodeDeletingEventHandler")
                .ImplementedBy<SettlePeriodReasonCodeDeletingEventHandler>().LifeStyle.Transient);

            builder.Register(
                Component.For<IEventHandler<ReasonCodeDeletingEvent>>()
                .Named("ReturnPolicyReasonCodeDeletingEventHandler")
                .ImplementedBy<ReturnPolicyReasonCodeDeletingEventHandler>().LifeStyle.Transient);

            builder.Register(
                Component.For<IEventHandler<MessageDeleteEvent>>()
                .Named("ReturnPolicyMessageDeleteEventHandler")
                .ImplementedBy<ReturnPolicyMessageDeleteEventHandler>().LifeStyle.Transient);

            builder.Register(
                Component.For<IQuery<IReturnPolicy, IReturnPolicy, IEventHandler<ReasonCodeDeletingEvent>>>()
                .Named("ReturnPolicyReasonCodeDeletingQuery")
                .ImplementedBy<ReturnPolicyReasonCodeDeletingQuery>().LifeStyle.Transient);

            builder.Register(
                Component.For<IReturnPolicyFactory>().Named("IReturnPolicyFactory").ImplementedBy<ReturnPolicyFactory>()
                         .LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleActionFactory>().Named("IRuleActionFactory").ImplementedBy<RuleActionFactory>()
                         .LifeStyle.Transient);

            builder.Register(
                Component.For<IReturnPolicyDao>().Named("IReturnPolicyDao").ImplementedBy<ReturnPolicyDao>()
                         .LifeStyle.Singleton);

            builder.Register(
                Component.For<ITenderRefundCalculator>().Named("ITenderRefundCalculator").ImplementedBy<TenderRefundCalculator>()
                    .LifeStyle.Singleton);

            builder.Register(
                Component.For<IPriceCalculationStrategy>().Named("PriceCalculationStrategy").ImplementedBy
                    <RefundByNetAmountStrategy>()
                         .LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExecuteStrategy>().Named("DenyReturnRuleExecuteStrategy").ImplementedBy
                    <DenyRuleExecuteStrategy>()
                         .LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExecuteStrategy>().Named("AllowReturnRuleExecuteStrategy").ImplementedBy
                    <AllowRuleExecuteStrategy>()
                         .LifeStyle.Transient);

            builder.Register(
                Component.For<IRuleExecuteStrategy>().Named(ForcedExchangeItemRuleExecuteStrategy.ConfigurationName).
                          ImplementedBy
                    <ForcedExchangeItemRuleExecuteStrategy>()
                         .LifeStyle.Transient);

            builder.RegisterSingleton<ILocalizedResourceDao, LocalizedResourceDao>();

            builder.RegisterSingleton<IHierarchyCultureDao, HierarchyCultureDao>();

            builder.RegisterSingleton<IPhysicalToLogicalRepository, PhysicalToLogicalRepository>();

            builder.Register(
                Component.For<IServerToBusinessUnitDao>().Named("IServerToBusinessUnitDao").ImplementedBy
                    <ServerToBusinessUnitDao>().
                          LifeStyle.Singleton);

            builder.Register(
                Component.For<IPhysicalToLogicalEntity>().Named("IPhysicalToLogicalEntity").ImplementedBy
                    <PhysicalToLogicalEntity>().LifeStyle.
                          Transient);

            builder.Register(
                Component.For<IDbSnapshotCreator>().Named("DbSnapshotCreator").
                          ImplementedBy<DbSnapshotCreator>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDeleteDmsDataExecuter>().Named("DeleteDmsDataExecuter").ImplementedBy
                    <DeleteDmsDataExecuter>().LifeStyle.Transient);

            builder.Register(
                Component.For<IForeignCurrencyExchangeRateDao>().Named("IForeignCurrencyExchangeRateDao").
                          ImplementedBy<ForeignCurrencyExchangeRateDao>().LifeStyle.Singleton);

            builder.RegisterSingleton<IDenominationSchemaRepository, DenominationSchemaRepository>();

            builder.Register(
                Component.For<IDataProvidersOrderRepository>().Named("IDataProvidersOrderRepository").ImplementedBy
                    <DataProvidersOrderRepository>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDeltaMessageRepository>().Named("IDeltaMessageRepository").ImplementedBy
                    <DeltaMessageRepository>().LifeStyle.Transient);

            builder.Register(
                Component.For<IColdStartHouseKeepingDao>().Named("IColdStartHouseKeepingDao").ImplementedBy
                    <ColdStartHouseKeepingDao>().LifeStyle.Transient);

            builder.RegisterSingleton<IOpeningFundsConfigurationFactory, OpeningFundsConfigurationFactory>();


            builder.RegisterSingleton<IOpeningFundsConfigurationDao, OpeningFundsConfigurationDao>();

            builder.Register(Component.For<IActivityReferenceConfigurationDao>().Named("IActivityReferenceConfigurationDao").ImplementedBy<ActivityReferenceConfigurationDao>().LifeStyle.Transient);

            builder.Register(Component.For<IDrawerCashOfficeConfiguration>().Named("IDrawerCashOfficeConfiguration")
                                      .ImplementedBy<DrawerCashOfficeConfiguration>().LifeStyle.Transient);

            builder.RegisterSingleton<IDrawerCashOfficeConfigurationDao, DrawerCashOfficeConfigurationDao>();

            builder.Register(
                Component.For<ISecurityScaleWeightLearningHandler>().Named("ISecurityScaleWeightLearningHandler").
                          ImplementedBy
                    <SecurityScaleWeightLearningHandler>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ISecurityScaleMeasurementDao>().Named("ISecurityScaleMeasurementDao").ImplementedBy
                    <SecurityScaleMeasurementDao>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IEntityDao>().Named("SecurityScale").ImplementedBy
                                  <SecurityScaleMeasurementDao>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ISecurityScaleMeasurement>().Named("ISecurityScaleMeasurement").ImplementedBy
                    <SecurityScaleMeasurement>().LifeStyle.Transient);

            builder.Register(
                Component.For<ISecurityScaleWeightValidator>().Named("ISecurityScaleWeightValidator").ImplementedBy
                    <SecurityScaleWeightValidator>().LifeStyle.Transient);

            builder.Register(
                Component.For<ISecurityScaleAvgWeightCalculationAlgorithm>().Named("ISecurityScaleAvgCalculationAlgorithm").ImplementedBy
                    <SecurityScaleAvgCalculationAlgorithm>().LifeStyle.Singleton);

            builder.Register(
                Component.For<IActivityLocationProvider>().Named(typeof(IActivityLocationProvider).Name)
                         .ImplementedBy<ActivityLocationProvider>().LifeStyle.Transient);

            builder.Register(
                Component.For<IAutomatedOpeningFundsExecuter>().Named(typeof(AutomatedOpeningFundsExecuter).Name)
                         .ImplementedBy<AutomatedOpeningFundsExecuter>().LifeStyle.Transient);

            builder.Register(
                Component.For<IOpenLoanOpeningFundsStrategy>().Named(
                    typeof(OpenLoanByOpenLoanPerBusinessDayStrategy).Name)
                         .ImplementedBy<OpenLoanByOpenLoanPerBusinessDayStrategy>().LifeStyle.Transient);

            builder.Register(
                Component.For<ICarryOverOpeningFundsStrategy>().Named(
                    typeof(CarryOverBySystemPickupOpenLoanStrategy).Name)
                         .ImplementedBy<CarryOverBySystemPickupOpenLoanStrategy>().LifeStyle.Transient);

            builder.Register(
                Component.For<IManualLoanFundsStrategy>().Named(
                    typeof(ManualLoanFundsStrategy).Name)
                         .ImplementedBy<ManualLoanFundsStrategy>().LifeStyle.Transient);

            builder.Register(
                Component.For<IBalancingDataProviderFactory>().Named(typeof(IBalancingDataProviderFactory).Name)
                         .ImplementedBy<BalancingDataProviderFactory>().LifeStyle.Transient);

            builder.Register(Component.For<ICashbackTransactionsActivitySummariesAccumulator>().Named(typeof(ICashbackTransactionsActivitySummariesAccumulator).Name)
                      .ImplementedBy<CashbackTransactionsActivitySummariesAccumulator>().LifeStyle.Transient);

            builder.Register(Component.For<ISaleTransactionsActivitySummariesAccumulator>().Named(typeof(ISaleTransactionsActivitySummariesAccumulator).Name)
                                  .ImplementedBy<SaleTransactionsActivitySummariesAccumulator>().LifeStyle.Transient);

            builder.RegisterSingleton<IAccountProfileDao, AccountProfileDao>(typeof(AccountProfileDao).Name);

            builder.Register(Component.For<IEventHandler<UserSavedEvent>>().Named("UserSavedCreateAccountEventHandler")
                .ImplementedBy<UserSavedCreateAccountEventHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IEventHandler<ShiftAccountSavedEvent>>().Named("BalanceByShiftAccountBuilder")
                .ImplementedBy<BalanceByShiftAccountBuilder>().LifeStyle.Transient);

            builder.Register(Component.For<IEventHandler<ShiftAccountSavedEvent>>().Named("CashierResetCountersHandler")
                .ImplementedBy<CashierResetCountersHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IEventHandler<ShiftAccountSavedEvent>>().Named("DrawerResetCountersHandler")
                .ImplementedBy<DrawerModeDrawerLimitBalanceCalculatorHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IOpenFundsPerCashierStrategy>().Named("OpenLoanByOpenLoanPerCashierStrategy")
                .ImplementedBy<OpenLoanByOpenLoanPerCashierStrategy>().LifeStyle.Transient);

            builder.Register(
                Component.For<IAccountProfileFactory>().Named(typeof(AccountProfileFactory).Name)
                         .ImplementedBy<AccountProfileFactory>().LifeStyle.Singleton);

            builder.Register(Component.For<IAccountNamingStrategy>().Named("AccountNamingStrategy")
               .ImplementedBy<AccountNamingStrategy>().LifeStyle.Transient);

            builder.Register(
                Component.For<IMovableServicesResolver>().Named("ReturnPolicy")
                         .ImplementedBy<ReturnPolicyServicesResolver>().LifeStyle.Transient);

            builder.RegisterSingleton<IDataPatternMetadataRepositoryManager, DataPatternMetadataRepositoryManager>();
            builder.RegisterSingleton<IDataPatternMetadataRepository, DataPatternMetadataRepository>();
            builder.RegisterSingleton<IDataPatternMetadataExtRepository, DataPatternMetadataExtRepository>();

            builder.RegisterSingleton<IServerHealthStatusProvider, ServerHealthStatusProvider>();

            builder.RegisterSingleton<IDataPatternMetadataDao, DataPatternMetadataDao>();

            builder.Register(
                Component.For<IColdStartState>().Named(ColdStartStateEnum.InitCycle.ToString()).ImplementedBy
                    <ColdStartInitCycleState>().LifeStyle.Transient);

            builder.Register(
                Component.For<IColdStartState>().Named(ColdStartStateEnum.Backup.ToString()).ImplementedBy
                    <ColdStartBackupState>().LifeStyle.Transient);

            builder.Register(
                Component.For<IColdStartState>().Named(ColdStartStateEnum.Working.ToString()).ImplementedBy
                    <ColdStartWorkingState>().LifeStyle.Transient);

            builder.Register(
                Component.For<IColdStartState>().Named(ColdStartStateEnum.Registering.ToString()).ImplementedBy
                    <ColdStartRegisteringState>().LifeStyle.Transient);

            builder.Register(
                Component.For<IColdStartState>().Named(ColdStartStateEnum.Stopping.ToString()).ImplementedBy
                    <ColdStartStoppingState>().LifeStyle.Transient);

            builder.Register(
                Component.For<IColdStartState>().Named(ColdStartStateEnum.GetBasicData.ToString()).ImplementedBy
                    <ColdStartGetBasicDataState>().LifeStyle.Transient);

            builder.Register(
                Component.For<IColdStartState>().Named(ColdStartStateEnum.Stopped.ToString()).ImplementedBy
                    <ColdStartStopDoneState>().LifeStyle.Transient);

            builder.Register(Component.For<IColdStartState>().Named(ColdStartStateEnum.Deleting.ToString()).ImplementedBy<ColdStartDeleteState>().LifeStyle.Transient);

            builder.Register(Component.For<IReturnPolicyLocator>().Named("ReturnPolicyLocator").ImplementedBy<ReturnPolicyLocator>().LifeStyle.Singleton);

            builder.Register(Component.For<ILinesByReturnContextFilter>().Named("LinesByReturnContextFilter").ImplementedBy<LinesByReturnContextFilter>().LifeStyle.Transient);

            if (ConfigurationManager.AppSettings["TransactionRepository"] != null && ConfigurationManager.AppSettings["TransactionRepository"].ToLower() == "distributedcacheredis")
                builder.RegisterSingleton<IScoInterventionRepository, ScoInterventionRedisRepository>("ScoInterventionRepository");
            else
                builder.RegisterSingleton<IScoInterventionRepository, ScoInterventionRepository>("ScoInterventionRepository");

            builder.Register(Component.For<IMovableServicesResolver>().Named("CouponSeries").ImplementedBy<CouponSeriesMovableServicesResolver>().LifeStyle.Singleton);

            builder.Register(Component.For<IMovableServicesResolver>().Named("CouponInstance").ImplementedBy<CouponInstacneMovableServicesResolver>().LifeStyle.Singleton);

            builder.Register(Component.For<IMovableServicesResolver>().Named("Coupon").ImplementedBy<CouponMovableServicesResolver>().LifeStyle.Singleton);

            builder.RegisterSingleton<ICouponInstanceDao, CouponInstanceDao>();

            builder.Register(Component.For<ICouponDao>().Named("ICouponDao").ImplementedBy<CouponDao>().LifeStyle.Transient);

            builder.Register(Component.For<IVenueShiftDao>().Named("IVenueShiftDao").ImplementedBy<VenueShiftDao>().LifeStyle.Transient);

            builder.Register(Component.For<IVenueDayShiftFactory>().Named("IVenueDayShiftFactory").ImplementedBy<VenueDayShiftFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IOpenCloseDayDao>().Named("IOpenCloseDayDao").ImplementedBy<OpenCloseDayDao>().LifeStyle.Transient);

            builder.Register(Component.For<ITenderRoundingRuleDao>().Named("ITenderRoundingRuleDao").ImplementedBy<TenderRoundingRuleDao>().LifeStyle.Transient);

            builder.Register(Component.For<ISecurityScaleConfigurationDao>().Named("ISecurityScaleConfigurationDao").ImplementedBy<SecurityScaleConfigurationDao>().LifeStyle.Transient);

            builder.Register(Component.For<IPOSBrandingImageDao>().Named("IPOSBrandingImageDao").ImplementedBy<POSBrandingImageDao>().LifeStyle.Transient);

            builder.Register(Component.For<IMenuOpenCloseDayDao>().Named("IMenuOpenCloseDayDao").ImplementedBy<MenuOpenCloseDayDao>().LifeStyle.Transient);

            builder.Register(Component.For<IMenuOpenClosDayTimeFactory>().Named("IMenuOpenClosDayTimeFactory").ImplementedBy<MenuOpenClosDayTimeFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IOpenCloseDayFactory>().Named("IOpenCloseDayFactory").ImplementedBy<OpenCloseDayFactory>().LifeStyle.Transient);

            builder.Register(Component.For<ITimeAvailabilityTermDao>().Named("TimeAvailabilityTermDao").ImplementedBy<TimeAvailabilityTermDao>().LifeStyle.Singleton);

            builder.Register(Component.For<ITimeAvailabilityTermFactory>().Named("ITimeAvailabilityTermFactory").ImplementedBy<TimeAvailabilityTermFactory>().LifeStyle.Transient);

            builder.Register(Component.For<ICouponFactory>().Named("ICouponFactory").ImplementedBy<CouponFactory>().LifeStyle.Singleton);

            builder.Register(Component.For<ICouponDataExtractor>().Named("ICouponDataExtractor").ImplementedBy<CouponDataExtractor>().LifeStyle.Singleton);

            builder.RegisterSingleton<IBusinessUnitFactory, BusinessUnitFactory>();

            builder.Register(Component.For<IApplicationParametersRepository>().Named("IApplicationParametersRepository").ImplementedBy<ApplicationParametersRepository>().LifeStyle.Singleton);

            builder.Register(Component.For<ISystemParameter>().Named("SystemParameter").ImplementedBy<SystemParameter>().LifeStyle.Transient);

            builder.Register(Component.For<IAutoReconcileTransactionAdaptor>().Named("AutoReconcileTransactionAdapter").ImplementedBy<AutoReconcileTransactionAdapter>().LifeStyle.Transient);

            builder.Register(Component.For<IRemovePaymentForDepositFromAutoReconciledTenders>().Named("RemovePaymentForDepositFromAutoReconciledTenders").ImplementedBy<RemovePaymentForDepositFromAutoReconciledTenders>().LifeStyle.Transient);

            builder.Register(Component.For<IFundTransferTransactionLogDaoObserver>().Named("AccountBalanceRecalculationAdaptorForFundTransferTLog")
                .ImplementedBy<AccountBalanceRecalculationAdapter>().LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IRetailTransactionFiltersFactory>()
                                 .Named("RetailTransactionFiltersFactory")
                                 .ImplementedBy<RetailTransactionFiltersFactory>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<ISavingAccountDao>()
                                 .Named("ISavingAccountDao")
                                 .ImplementedBy<SavingAccountDao>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IStoreBalancePeriodByBusinessDayCreator>()
                                 .Named(typeof(StoreBalancePeriodByBusinessDayCreator).Name)
                                 .ImplementedBy<StoreBalancePeriodByBusinessDayCreator>()
                                 .LifeStyle.Transient);

            RegisterFuelBusinessComponents(builder);

            builder.Register(
                             Component
                                 .For<IRetailTransactionShouldOpenDrawer>()
                                 .ImplementedBy<RetailTransactionShouldOpenDrawer>()
                                 .LifeStyle.Singleton);
            builder.Register(
                           Component
                               .For<IFundTransferTransactionShouldOpenDrawer>()
                               .ImplementedBy<FundTransferTransactionShouldOpenDrawer>()
                               .LifeStyle.Transient);

            builder.Register(
                     Component
                         .For<IOpenDrawerOnCashOfficeActivities>()
                         .ImplementedBy<OpenDrawerOnAddLoan>()
                         .Named("OpenDrawerOnAddLoan")
                         .LifeStyle.Transient);

            builder.Register(
                     Component
                         .For<IOpenDrawerOnCashOfficeActivities>()
                         .ImplementedBy<OpenDrawerOnPaidIn>()
                         .Named("OpenDrawerOnPaidIn")
                         .LifeStyle.Transient);

            builder.Register(
                     Component
                         .For<IOpenDrawerOnCashOfficeActivities>()
                         .ImplementedBy<OpenDrawerOnPaidOut>()
                         .Named("OpenDrawerOnPaidOut")
                         .LifeStyle.Transient);

            builder.Register(
                Component.For<IDepositTenderLineAmountCalculator>()
                .Named("DepositTenderAmountCalculatorForAccountBalance")
                .ImplementedBy<DepositTenderLineAmountCalculator>().LifeStyle.Transient);

            builder.Register(
                Component.For<ITenderTypeHelper>().Named("TenderTypeHelper").
                    ImplementedBy<TenderTypeHelper>().LifeStyle.Transient);

            builder.Register(
                Component.For<IOpenAmountCalculator>()
                    .Named("Constant")
                    .ImplementedBy<ConstantOpenAmountCalculator>().LifeStyle.Transient);

            builder.Register(
                Component.For<IOpenAmountCalculator>()
                    .Named("LatestDeclaration")
                    .ImplementedBy<LatestDeclarationOpenAmountCalculator>().LifeStyle.Transient);

            builder.Register(
                Component.For<ITenderLineAmountCalculator>()
                .Named("TenderAmountCalculatorForAccountBalance")
                .ImplementedBy<TenderLineAmountCalculator>().LifeStyle.Transient);

            builder.Register(
                Component.For<IFundTransferAutoReconcileLinesCollector>().
                Named("FundTransferAutoReconcileLinesCollector").
                ImplementedBy<FundTransferAutoReconcileLinesCollector>().LifeStyle.Transient);
            builder.Register(Component.For<IAccountBalancingSummaryProvider>()
                    .Named("AccountBalancingSummaryProvider")
                    .ImplementedBy<AccountBalancingSummaryProvider>().LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IMessageParameterDefinitionDao>()
                                 .Named("MessageParameterDefinitionDao")
                                 .ImplementedBy<MessageParameterDefinitionDao>()
                                 .LifeStyle.Transient);

            builder.RegisterSingleton<IMenuItemFactory, MenuItemFactory>();

            builder.RegisterSingleton<IMenuItemMovableConverter, MenuItemMovableConverter>();

            builder.RegisterSingleton<IMenuDisplayTermsMovableConverter, MenuDisplayTermsMovableConverter>();

            builder.RegisterSingleton<IMenuItemDao, MenuItemDao>();

            builder.Register(Component.For<IMenuDisplayTerms>().Named(typeof(MenuDisplayTerms).Name).ImplementedBy<MenuDisplayTerms>()
                .LifeStyle.Transient);

            builder.Register(Component.For<IDateRange>().Named(typeof(DateRange).Name).ImplementedBy<DateRange>()
                   .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IMenuItemDaoPrefetchHint>()
                                 .Named(typeof(MenuItemDaoPrefetchHintDto).Name)
                                 .ImplementedBy<MenuItemDaoPrefetchHintDto>().LifeStyle.Transient);

            builder.RegisterSingleton<IMenuDisplayTermsDao, MenuDisplayTermsDao>();


            builder.Register(
                             Component
                                 .For<IMenuMultiVersionMapper>()
                                 .Named(typeof(MenuMultiVersionMapper).Name)
                                 .ImplementedBy<MenuMultiVersionMapper>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IMenuContextFormatter>()
                                 .Named(MenuItemType.ItemLookup)
                                 .ImplementedBy<ItemLookupContextFormatter>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IMenuContextFormatter>()
                                 .Named(MenuItemType.ItemLookup + "V1")
                                 .ImplementedBy<ItemLookupContextFormatterV1>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IMenuContextFormatter>()
                                 .Named(MenuItemType.Tender)
                                 .ImplementedBy<TenderContextFormatter>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IMenuContextFormatter>()
                                 .Named(MenuItemType.Tender + "V1")
                                 .ImplementedBy<TenderContextFormatterV1>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IMenuContextFormatter>()
                                 .Named(MenuItemType.Command)
                                 .ImplementedBy<CommandContextFormatter>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IMenuContextFormatter>()
                                 .Named(MenuItemType.Command + "V1")
                                 .ImplementedBy<CommandContextFormatterV1>()
                                 .LifeStyle.Transient);

            builder.Register(Component.For<IControlTransactionLogReader>().Named("ControlTransactionLogReader").ImplementedBy<ControlTransactionLogReader>().LifeStyle.Singleton);

            builder.RegisterSingleton<IDataPatternFinder, DataPatternFinder>();

            builder.Register(Component.For<ICashierBarcode>().Named("CashierBarcode").ImplementedBy<CashierBarcode>().LifeStyle.Transient);

            builder.Register(Component.For<ICashierBarcodeUser>().Named("CashierBarcodeUser").ImplementedBy<CashierBarcodeUser>().LifeStyle.Transient);

            builder.Register(Component.For<IServerHealthStatusFilter>().Named("IServerHealthStatusFilter").ImplementedBy<ServerHealthStatusFilter>().LifeStyle.Singleton);

            builder.Register(Component.For<IBottleDepositGroupDao>().Named("BottleDepositGroupDao").ImplementedBy<BottleDepositGroupDao>().LifeStyle.Transient);

            builder.Register(Component.For<IBottleDepositGroupFactory>().Named("BottleDepositGroupFactory").ImplementedBy<BottleDepositGroupFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IThresholdRuleFactory>().Named("ThresholdRuleFactory").ImplementedBy<ThresholdRuleFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IThresholdCondition>().Named("ThresholdCondition").ImplementedBy<ThresholdCondition>().LifeStyle.Transient);

            builder.Register(Component.For<IAnalyticsConnectionStringFactory>().Named("IAnalyticsConnectionStringFactory").ImplementedBy<AnalyticsConnectionStringFactory>().LifeStyle.Singleton);

            builder.Register(Component.For<IBIRepository>().Named("IBIRepository").ImplementedBy<BIRepository>().LifeStyle.Singleton);

            builder.Register(Component.For<IForcedExchangeCalculator>().Named("ForcedExchangeCalculator").ImplementedBy<ForcedExchangeCalculator>().LifeStyle.Transient);

            builder.Register(Component.For<ICredentialsApprover>().Named("CredentialsApprover").ImplementedBy<CredentialsApprover>().LifeStyle.Transient);

            builder.RegisterSingleton<IScoInterventionAddUpdateHandler, ScoInterventionAddUpdateHandler>();

            builder.Register(Component.For<IScoInterventionDeleteHandler>().Named("IScoInterventionDeleteHandler").ImplementedBy<ScoInterventionDeleteHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IBottleDepositGroupLocator>().Named("IBottleDepositGroupLocator").ImplementedBy<BottleDepositGroupLocator>().LifeStyle.Transient);

            builder.Register(Component.For<IMovableEntityConfigurationFactory>().Named("IMovableEntityConfigurationFactory").ImplementedBy<MovableEntityConfigurationFactory>().LifeStyle.Transient);

            builder.RegisterSingleton<IMovableEntityConfigurationDao, MovableEntityConfigurationDao>();

            builder.RegisterSingleton<ICustomerIdGenerator, CustomerIdGenerator>();

            builder.Register(Component.For<ITdmConfigurationDao>().Named("TdmConfigurationDao").ImplementedBy<TdmConfigurationDao>().LifeStyle.Singleton);
            builder.Register(Component.For<ITdmConfigurationParametersValidator>().Named("TdmConfigurationParametersValidator").ImplementedBy<TdmConfigurationParametersValidator>().LifeStyle.Singleton);

            builder.Register(Component.For<ITransactionLogDao>().Named("TransactionLogDao").ImplementedBy<TransactionLogDao>().LifeStyle.Singleton);

            builder.Register(Component.For<ITdmDatabaseFactory>().Named("TdmDatabaseFactory").ImplementedBy<TdmDatabaseFactory>().LifeStyle.Singleton);

            builder.Register(Component.For<IDocumentDao>().Named("GenericLogDocumentDao").ImplementedBy<GenericLogDocumentDao>().LifeStyle.Singleton);

            builder.Register(Component.For<IDocumentDao>().Named("RetailTransactionLogDao").ImplementedBy<RetailTransactionLogDao>().LifeStyle.Transient);

            builder.Register(Component.For<IDocumentDao>().Named("ControlTransactionLogDao").ImplementedBy<ControlTransactionLogDao>().LifeStyle.Singleton);

            builder.Register(Component.For<IDocumentDao>().Named("ControlAndRetailTransactionLogDao").ImplementedBy<ControlAndRetailTransactionLogDao>().LifeStyle.Singleton);

            builder.Register(Component.For<IDocumentDao>().Named("TipTransactionLogDao").ImplementedBy<TipTransactionLogDao>().LifeStyle.Transient);

            builder.Register(Component.For<ITipTransactionLogDao>().ImplementedBy<TipTransactionLogDao>().LifeStyle.Transient);

            builder.Register(Component.For<ITranLogXmlToDocumentConverter>().Named("RetailTranLogXmlToDocumentConverter")
                .ImplementedBy<RetailTranLogXmlToDocumentConverter>().LifeStyle.Transient);

            builder.Register(Component.For<ITranLogXmlToDocumentConverter>().Named("ControlTranLogXmlToDocumentConverter")
                .ImplementedBy<ControlTranLogXmlToDocumentConverter>().LifeStyle.Transient);

            builder.Register(Component.For<ITranLogXmlToDocumentConverter>().Named("DeclarationTranLogXmlToDocumentConverter")
                .ImplementedBy<DeclarationTranLogXmlToDocumentConverter>().LifeStyle.Transient);

            builder.Register(Component.For<ITipTransactionLogReader>().ImplementedBy<TipTransactionLogReader>().LifeStyle.Transient);

            builder.Register(Component.For<ICustomerDisplayLayoutDao>().Named("CustomerDisplayLayoutDao").ImplementedBy<CustomerDisplayLayoutDao>().LifeStyle.Transient);

            builder.Register(Component.For<ICustomerScreenConfigurationFactory>().Named("CustomerScreenConfigurationFactory").ImplementedBy<CustomerScreenConfigurationFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IPosStateDao>().Named("PosStateDao").ImplementedBy<PosStateDao>().LifeStyle.Transient);

            builder.Register(Component.For<IPaymentTransactionDao>().Named("PaymentTransactionDao").ImplementedBy<PaymentTransactionDao>().LifeStyle.Transient);

            builder.Register(Component.For<IPaymentTransaction>().Named("PaymentTransaction").ImplementedBy<PaymentTransaction>().LifeStyle.Transient);

            builder.Register(Component.For<IStoreSalesReportsDao>().Named("StoreSalesReportsDao").ImplementedBy<StoreSalesReportsDao>().LifeStyle.Singleton);

            builder.Register(Component.For<IMailServiceProvider>().Named("CustomerMailServiceProvider").ImplementedBy<CustomerMailServiceProvider>().LifeStyle.Transient);

            builder.RegisterSingleton<IAlertDao, AlertDao>("AlertDao");

            builder.Register(Component.For<IRestrictionInterventionHandler>().Named("ConditionalRestrictionRuleInteventionHandler").ImplementedBy<ConditionalRestrictionRuleInterventionHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IInterventionStrategyFactory>().Named("InterventionStrategyFactory").ImplementedBy<InterventionStrategyFactory>().LifeStyle.Transient);

            builder.RegisterSingleton<IAlertFactory, AlertFactory>();

            builder.Register(Component.For<IAlertTemplateSearchCriteria>().Named("AlertTemplateSearchCriteria").ImplementedBy<AlertTemplateSearchCriteria>().LifeStyle.Transient);

            builder.Register(Component.For<IAlertSearchCriteria>().Named("AlertSearchCriteria").ImplementedBy<AlertSearchCriteria>().LifeStyle.Transient);

            builder.Register(Component.For<ISingularServiceFactory>().ImplementedBy<SingularServiceFactory>().LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IAccountBalanceApprovalValidator>()
                                 .Named("OverShortThresholdAccountBalanceApprovalValidator")
                                 .ImplementedBy<OverShortThresholdAccountBalanceApprovalValidator>()
                                 .LifeStyle.Transient);

            builder.RegisterSingleton<ITouchPointKeyboardConfigurationDao, TouchPointKeyboardConfigurationDao>();

            builder.RegisterSingleton<IKeyboardConfigurationFactory, KeyboardConfigurationFactory>();

            builder.Register(
                             Component
                                 .For<ITenderControlTransactionCreationVisitor>()
                                 .Named("FinanceControlTransactionLogVisitor")
                                 .ImplementedBy<TenderControlTransactionCreationVisitor>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<ITenderControlTransactionCreationVisitor>()
                                 .Named("BusinessRuleTenderControlTransactionCreationVisitor")
                                 .ImplementedBy<BusinessRuleTenderControlTransactionCreationVisitor>()
                                 .LifeStyle.Singleton);
            builder.Register(
                Component
                    .For<IQueueMessageHeaderVisitor>()
                    .Named("ExternalOrdersVisitor")
                    .ImplementedBy<ExternalOrdersVisitor>()
                    .LifeStyle.Singleton);


            builder.RegisterSingleton<ITouchPointCommandDao, TouchPointCommandDao>();

            builder.RegisterSingleton<IDeviceProfileFactory, DeviceConfigurationFactory>();

            builder.Register(Component.For<ITenderTypeDao>().Named("TenderTypeDao").ImplementedBy<TenderTypeDaoProxy>().LifeStyle.Singleton);

            builder.Register(Component.For<ITenderTypeFactory>().Named("ITenderTypeFactory").ImplementedBy<TenderTypeFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IPaymentInfoContractToModelAdapter<RetailTransactionLineItem, RetailTransactionTender>>().Named("IPaymentInfoContractToModelAdapter").ImplementedBy<PaymentInfoContractToModelAdapter<RetailTransactionLineItem, RetailTransactionTender>>().LifeStyle.Transient);

            builder.Register(Component.For<ILimitTenders>().Named("PaymentTermLimitTenders").ImplementedBy<PaymentTermLimitTenders>().LifeStyle.Transient);

            builder.Register(Component.For<ITenderCreator>().Named("ITenderCreator").ImplementedBy<TenderCreator>().LifeStyle.Transient);

            builder.Register(Component.For<IRemoveLoyaltyByCouponRule>().Named("RemoveLoyaltyRule").ImplementedBy<RemoveLoyaltyRule>().LifeStyle.Transient);

            builder.RegisterSingleton<IRetailSegmentFactory, RetailSegmentFactory>("IRetailSegmentFactory");

            builder.RegisterSingleton<IVersionProvider, VersionProvider>("IVersionProvider");

            builder.RegisterSingleton<IVenueSegmentUtility, VenueSegmentUtility>("IVenueSegmentUtility");

            builder.RegisterSingleton<ICtmSegmentUtility, CtmSegmentUtility>("ICtmSegmentUtility");

            builder.RegisterSingleton<ITouchPointProfileTypeResolver, TouchPointProfileTypeResolver>("ITouchPointProfileTypeResolver");

            builder.RegisterSingleton<IRetailSegmentDao, RetailSegmentDao>("RetailSegmentDao");

            builder.RegisterSingleton<IAuthenticationMethodPolicyDao, AuthenticationMethodPolicyDao>();

            builder.RegisterSingleton<ITransactionDataPatternEncoder, TransactionDataPatternEncoder>();

            builder.RegisterSingleton<ISelfScanTransactionDataPatternEncoder, SelfScanTransactionDataPatternEncoder>();

            builder.RegisterSingleton<IExternalConsumptionBarcodeDataPatternEncoder, ExternalConsumptionBarcodeDataPatternEncoder>();

            builder.RegisterSingleton<IIndicatorAdaptor, IndicatorAdaptor>();

            builder.RegisterSingleton<IIndicatorExtractor, TaxIndicatorExtractor>("TaxIndicatorExtractor");

            builder.RegisterSingleton<IExtractorTaxIndicatorsFromTaxRate, ExtractorTaxIndicatorsFromTaxRate>("ExtractorTaxIndicatorsFromTaxRate");

            builder.Register(Component.For<IIndicatorDao>().Named("IndicatorDao").ImplementedBy<IndicatorDaoProxy>().LifeStyle.Singleton);

            builder.Register(Component.For<IIndicatorGroupDao>().Named("IndicatorGroupDao").ImplementedBy<IndicatorGroupDao>().LifeStyle.Transient);

            builder.Register(Component.For<IIndicatorGroupFactory>().Named("IndicatorGroupFactory").ImplementedBy<IndicatorGroupFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IIndicatorFactory>().Named("IndicatorFactory").ImplementedBy<IndicatorFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IStoreRangeFactory>().Named("StoreRangeFactory").ImplementedBy<StoreRangeFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IProductInStoreFactory>().Named("ProductInStoreFactory").ImplementedBy<ProductInStoreFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IIncludeInRangeFactory>().Named("IncludeInRangeFactory").ImplementedBy<IncludeInRangeFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IOnlineAccountCommandFactory>().Named("IOnlineAccountCommandFactory").ImplementedBy<OnlineAccountCommandFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IDisposalMethodDao>().Named("DisposalMethodDao").ImplementedBy<DisposalMethodDao>().LifeStyle.Transient);

            builder.Register(Component.For<IDisposalMethodFactory>().Named("DisposalMethodFactory").ImplementedBy<DisposalMethodFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IServerInfoFactory>().ImplementedBy<ServerInfoFactory>().LifeStyle.Singleton);

            builder.Register(Component.For<IServerComponentsInfoFactory>().ImplementedBy<ServerComponentsInfoFactory>().LifeStyle.Singleton);

            builder.RegisterSingleton<IServerApprovalRequestDao, ServerApprovalRequestDao>();

            builder.Register(Component.For<IEntityDao>().Named("RefundVoucher").ImplementedBy<RefundVoucherDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("StoreRangeRetention").ImplementedBy<StoreRangeRetentionPolicy>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("ExpiredPluMenus").ImplementedBy<ExpiredPluMenuEntityDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("OrphanPluMenus").ImplementedBy<OrphanPluMenuEntityDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("OrphanPluGroups").ImplementedBy<OrphanPluGroupEntityDao>().LifeStyle.Transient);

            builder.Register(Component.For<IOnlineAccountDepositObserver>().Named("OnlineAccountDepositObserver").ImplementedBy<OnlineAccountDepositObserver>().LifeStyle.Transient);

            builder.Register(Component.For<IOnlineAccountHandler>().Named("OnlineAccountHandler").ImplementedBy<OnlineAccountHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IOnlineAccountCommand>().Named("OnlineAccountEpsDepositCommand").ImplementedBy<OnlineAccountEpsDepositCommand>().LifeStyle.Transient);

            builder.Register(Component.For<IOnlineAccountCommand>().Named("OnlineAccountLoyaltyDepositCommand").ImplementedBy<OnlineAccountLoyaltyDepositCommand>().LifeStyle.Transient);

            builder.Register(Component.For<ISecurityTokenGenerator>().Named("SecurityTokenGenerator").ImplementedBy<SecurityTokenGenerator>().LifeStyle.Singleton);

            builder.Register(Component.For<IMovableDocumentConverter>().Named("IMovableLogDocumentFactory").ImplementedBy<MovableDocumentConverter>().LifeStyle.Transient);

            builder.Register<IPredictiveRescanServiceConfiguration, PredictiveRescanServiceConfiguration>();

            builder.Register(Component.For<ICustomerOrderPredictiveRescanDataProvider>()
                .Named("ICustomerOrderPredictiveRescanDataProvider")
                .ImplementedBy<CustomerOrderPredictiveRescanDataProvider>().LifeStyle.Singleton);

            builder.RegisterSingleton<IReturnCustomerOrderInfoProvider, ReturnCustomerOrderInfoProvider>();

            builder.RegisterSingleton<IRetailTransactionLinkProvider, RetailTransactionLinkProvider>();

            builder.RegisterSingleton<ICustomerOrderLinkProvider, CustomerOrderLinkProvider>();

            builder.Register(Component.For<ICurrencyConvertor>().Named("ICurrencyConvertor").ImplementedBy<CurrencyConvertor>().LifeStyle.Singleton);

            builder.Register(Component.For<IServerHealthCache>().Named("ServerHealthCache").ImplementedBy<ServerHealthCache>().LifeStyle.Singleton);

            builder.Register(Component.For<IGiftReceiptBuilder>().Named("IGiftReceiptBuilder").ImplementedBy<GiftReceiptBuilder>().LifeStyle.Transient);

            builder.Register(Component.For<ISuggestedAmountsCalculator>().Named("ISuggestedAmountsCalculator").ImplementedBy<SuggestedAmountsCalculator>().LifeStyle.Singleton);

            builder.Register(Component.For<ITdmArchive>().Named("TdmArchive").ImplementedBy<TdmArchive>().LifeStyle.Singleton);

            builder.Register(Component.For<IBlindPickupDataProvider>().Named("BlindPickupDataProvider").ImplementedBy<BlindPickupDataProvider>().LifeStyle.Transient);

            builder.Register(Component.For<ITdmRetransmitLookup>().Named("TdmRetransmit").ImplementedBy<TdmRetransmitLookup>().LifeStyle.Singleton);

            builder.Register(Component.For<IRetransmitPublisher>().Named("RetransmitToRabbitPublisher").ImplementedBy<RetransmitToRabbitPublisher>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitPublisher>().Named("RetransmitToSqlPublisher").ImplementedBy<RetransmitToSqlPublisher>().LifeStyle.Transient);

            builder.RegisterSingleton<ITdmPerformanceCountersFactory, TdmPerformanceCountersFactory>();

            builder.RegisterSingleton<ICounterCreator, CounterCreator>();

            builder.Register(Component.For<IDocumentValidator>().Named("DocumentValidator").ImplementedBy<Arts6LogValidator>().LifeStyle.Singleton);

            builder.Register(Component.For<IChangeOptions>().Named("ChangeOptions").ImplementedBy<ChangeOptions>().LifeStyle.Transient);

            builder.Register(Component.For<IPaymentOptions>().Named("PaymentOptions").ImplementedBy<PaymentOptions>().LifeStyle.Transient);

            builder.Register(Component.For<IUserConfigurationPermissions>().Named("UserConfigurationPermissionsEntity").ImplementedBy<UserConfigurationPermissions>().LifeStyle.Transient);

            builder.RegisterSingleton<IUserConfigurationPermissionsDao, UserConfigurationPermissionsDao>();

            builder.Register(Component.For<IChangeOptionsDao>().Named("ChangeOptionsDao").ImplementedBy<ChangeOptionsDaoProxy>().LifeStyle.Singleton);

            builder.Register(Component.For<IPaymentOptionsDao>().Named("PaymentOptionsDao").ImplementedBy<PaymentOptionsDaoProxy>().LifeStyle.Singleton);

            builder.Register(Component.For<IClientSessionManager>().Named("ManagerIdmClientSession").ImplementedBy<ClientSessionManager>().LifeStyle.Transient);

            builder.Register(Component.For<IPasswordStrengthValidator>().Named("PasswordStrengthValidator").ImplementedBy<PasswordStrengthValidator>().LifeStyle.Singleton);

            builder.Register(Component.For<IServer>().Named("ServerEntity").ImplementedBy<Server>().LifeStyle.Transient);

            builder.Register(Component.For<IServerGroup>().Named("ServerGroupEntity").ImplementedBy<ServerGroup>().LifeStyle.Transient);

            builder.Register(Component.For<IServerDao>().Named("ServerDao").ImplementedBy<ServerDao>().LifeStyle.Singleton);

            builder.RegisterSingleton<IServerGroupDao, ServerGroupDao>();

            builder.RegisterQuery<ServerDefaultQuery>();
            builder.RegisterQuery<ServerFullyEagerQuery>();
            builder.RegisterQueryCriterionApplier<ServerBusinessUnitIdCriterionApplier>();
            builder.RegisterQueryCriterionApplier<ServerNameCriterionApplier>();
            builder.RegisterQueryCriterionApplier<ServerTagsCriterionApplier>();
            builder.RegisterQueryCriterionApplier<ServerIdCriterionApplier>();

            builder.RegisterSingleton<IAlertTemplateDao, AlertTemplateDao>();

            builder.Register(Component.For<IRetailTransactionLogDocumentWriter>().Named("RetailTransactionLogDocument").ImplementedBy<RetailTransactionLogDocument>().LifeStyle.Transient);

            builder.Register(Component.For<IJob>().Named("RetentionPolicyJob").ImplementedBy<RetentionPolicyJob>().LifeStyle.Transient);

            builder.Register(Component.For<IJobScheduleProvider>().Named("RetentionPolicyJobScheduler").ImplementedBy<RetentionPolicyJobScheduler>().LifeStyle.Singleton);

            builder.Register(Component.For<IRetentionPolicyExecutor>().Named("RetentionPolicyExecutor").ImplementedBy<RetentionPolicyExecutor>().LifeStyle.Transient);

            builder.RegisterSingleton<IDocumentQueueHandler, DocumentQueueHandler>("DocumentQueueHandler");

            builder.Register(Component.For<IMessageQueueHandler>().Named("MessageQueueHandler").ImplementedBy<MessageQueueHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IMessageQueue>().Named("MessageQueue").ImplementedBy<MessageQueue>().LifeStyle.Singleton);

            builder.Register(Component.For<IThirdPartyPublisherInitializer>().ImplementedBy<ThirdPartyPublisherInitializer>().LifeStyle.Singleton);

            builder.Register(Component.For<ISendPublisherErrorMessagesToDbHandler>().ImplementedBy<SendPublisherErrorMessagesToDbHandler>().LifeStyle.Transient);
            builder.Register(Component.For<ISendPublisherErrorMessagesToIntegrationExchangeHandler>().ImplementedBy<SendPublisherErrorMessagesToIntegrationExchangeHandler>().LifeStyle.Transient);

            builder.Register(Component.For<ISendReportingWarehouseErrorMessagesToDeadLetterExchangeHandler>().ImplementedBy<SendReportingWarehouseErrorMessagesToDeadLetterExchangeHandler>().LifeStyle.Transient);
            builder.Register(Component.For<ISendReportingWarehouseErrorMessagesToDbHandler>().ImplementedBy<SendReportingWarehouseErrorMessagesToDbHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IMapper<List<IQueueMessage>, List<IReportingWarehouseErrorMessage>>>().ImplementedBy<QueueMessageToReportingWarehouseErrorMessageMapper>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<Dictionary<string, string>, IReportingWarehouseErrorMessage>>().ImplementedBy<MessageQueueHeaderToReportingWarehouseErrorMessageMapper>().LifeStyle.Transient);

            builder.RegisterSingleton<IThirdPartyUowDataContainerSetter, ThirdPartyUowDataContainerSetter>();

            builder.Register(Component.For<IRetentionPolicy>().Named("IRetentionPolicy").ImplementedBy<RetentionPolicy>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("Promotions").ImplementedBy<PromotionDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("Coupons").ImplementedBy<CouponDao>().LifeStyle.Transient);

            builder.Register(Component.For<IRetentionPolicyConfiguration>().Named("IRetentionPolicyConfiguration").ImplementedBy<RetentionPolicyConfiguration>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("TLogs").ImplementedBy<RetailTransactionLogDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("DTLogs").ImplementedBy<DeclarationTransactionLogDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("CTLogs").ImplementedBy<ControlTransactionLogDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("Users").ImplementedBy<UserDaoProxy>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("OpenAlerts").ImplementedBy<OpenAlertEntityDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("ClosedAlerts").ImplementedBy<ClosedAlertEntityDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("Prices").ImplementedBy<PriceDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("UnPaid").ImplementedBy<FuelSuspendedBusinessProcessTreatment>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("AuditLogDel").ImplementedBy<AuditLogRetentionPolicy>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("Suspend").ImplementedBy<SuspendedBusinessProcessTreatment>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("SelfScanSuspend").ImplementedBy<SuspendedSelfScanBusinessProcessTreatment>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("SuspendTab").ImplementedBy<SuspendedTabBusinessProcessTreatment>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("SuspendPickup").ImplementedBy<SuspendedPickupBusinessProcessTreatment>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("SuspendEcommerce").ImplementedBy<ClickAndCollectBusinessProcessTreatment>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("Customers").ImplementedBy<CustomerDaoRetentionPolicy>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("CustomerTokens").ImplementedBy<CustomerTokenDao>().LifeStyle.Transient);

            if (ConfigurationManager.AppSettings["TransactionRepository"] != null && ConfigurationManager.AppSettings["TransactionRepository"].ToLower() == "distributedcache")
                builder.Register(Component.For<IEntityDao>().Named("FinishedProcesses").ImplementedBy<BusinessProcessHazelcastDao>().LifeStyle.Transient);
            else
                builder.Register(Component.For<IEntityDao>().Named("FinishedProcesses").ImplementedBy<BusinessProcessDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("Notifications").ImplementedBy<NotificationDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("NotificationStatus").ImplementedBy<NotificationInstanceDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("QueueErrors").ImplementedBy<QueueErrorMessageRetentionPolicy>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("WarehouseQueueErrors").ImplementedBy<ReportingWarehouseQueueErrorMessageRetentionPolicy>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("FailedMessages").ImplementedBy<FailedDownloadTransportMessageRetentionPolicy>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("XZReportsAggregation").ImplementedBy<XZReportsAggregationSPDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("RetailTips").ImplementedBy<TipTransactionLogDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("SavingAccountTLogs").ImplementedBy<SavingAccountTransactionLogDao>().LifeStyle.Transient);

            builder.Register(Component.For<IEntityDao>().Named("Counters").ImplementedBy<CounterDao>().LifeStyle.Transient);

            builder.Register(Component.For<IDataCompression>().Named("DmsDataCompression").ImplementedBy<DataCompression>().LifeStyle.Singleton);

            builder.Register(Component.For<IJobScheduleProvider>().Named("SelfScanUnregisteredJobScheduler").ImplementedBy<SelfScanUnregisteredJobScheduler>().LifeStyle.Singleton);

            builder.Register(Component.For<ITipArts6LogWriter>().Named("ITipArts6LogWriter").ImplementedBy<TipArts6LogWriter>().LifeStyle.Transient);

            builder.Register<SelfScanUnregisteredJob>();

            builder.Register(Component.For<IJobScheduleProvider>().Named("ActiveTransactionSuspendJobScheduler").ImplementedBy<ActiveTransactionSuspendJobScheduler>().LifeStyle.Singleton);

            builder.Register(Component.For<IJobScheduleProvider>().Named("RedisCacheTrimJobScheduler").ImplementedBy<RedisCacheTrimJobScheduler>().LifeStyle.Singleton);

            builder.Register<ActiveTransactionSuspendJob>();

            builder.Register<RedisCacheTrimJob>();

            builder.Register<TabInfoSynchronizationJob>();

            builder.Register(Component.For<IJobScheduleProvider>().Named("TabInfoSynchronizationJobScheduler").ImplementedBy<TabInfoSynchronizationJobScheduler>().LifeStyle.Singleton);

            builder.Register(Component.For<IJobScheduleProvider>().Named("ProductAvailabilityJobScheduler").ImplementedBy<ProductAvailabilityJobScheduler>().LifeStyle.Transient);

            builder.Register(Component.For<IJobScheduleProvider>().Named("AutoVoidAbandonJobScheduler").ImplementedBy<AutoVoidAbandonJobScheduler>().LifeStyle.Transient);

            builder.Register<ProductAvailabilityJob>();

            builder.Register<AutoVoidAbandonJob>();

            builder.RegisterMapper<JsonPayloadViewMapper>().WithAlias<JsonPayloadView, object>();

            builder.RegisterMapper<XmlPayloadViewMapper>().WithAlias<XmlPayloadView, object>();

            builder.RegisterSingleton<IPayloadViewFormatter, PayloadViewFormatter>();

            builder.RegisterSingleton<IPayloadViewFormatter<EntityDefaultDto>, EntityDefaultDtoPayloadViewFormatter>();

            builder.Register(
                             Component
                                 .For<IShoppingListDao>()
                                 .Named("ShoppingListDao")
                                 .ImplementedBy<ShoppingListDao>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IShoppingListFactory>()
                                 .Named("ShoppingListFactory")
                                 .ImplementedBy<ShoppingListFactory>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IShoppingListLineFactory>()
                                 .Named("ShoppingListLineFactory")
                                 .ImplementedBy<ShoppingListLineFactory>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IDmsEventSource>()
                                 .Named("IDmsEventSource")
                                 .ImplementedBy<DmsEventSource>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IDmsRabbitEventSource>()
                                 .Named("IDmsRabbitEventSource")
                                 .ImplementedBy<DmsRabbitEventSource>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IProductEventSource>()
                                 .Named("IProductEventSource")
                                 .ImplementedBy<ProductEventSource>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IOwnBagConfiguration>()
                                 .Named("IOwnBagConfiguration")
                                 .ImplementedBy<OwnBagConfiguration>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IOwnBagConfigurationProvider>()
                                 .Named("OwnBagConfigurationProvider")
                                 .ImplementedBy<OwnBagConfigurationProvider>()
                                 .LifeStyle.Transient);

            builder.RegisterSingleton<IOwnBagConfigurationDao, OwnBagConfigurationDao>();

            builder.RegisterSingleton<IKitDisplayLayoutDao, KitDisplayLayoutDao>();

            builder.Register(
                             Component
                                 .For<IKitDisplayLayout>()
                                 .Named(typeof(KitDisplayLayout).FullName)
                                 .ImplementedBy<KitDisplayLayout>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IKitGroupLayout>()
                                 .Named(typeof(KitGroupLayout).FullName)
                                 .ImplementedBy<KitGroupLayout>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IKitIngredientLayout>()
                                 .Named(typeof(KitIngredientLayout).FullName)
                                 .ImplementedBy<KitIngredientLayout>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IKitDisplayTemplate>()
                                 .Named(typeof(KitDisplayTemplate).FullName)
                                 .ImplementedBy<KitDisplayTemplate>()
                                 .LifeStyle.Transient);

            builder.Register(
                Component.For<IPrintableDataAdaptor>().Named("DeclinedTendersAuditLogAdaptor").ImplementedBy
                    <DeclinedTendersAuditLogAdaptor>().LifeStyle.Singleton);

            builder.Register(Component.For<IPrintableDataAdaptor>().Named("SelfWeighTransactionPrintableDataAdaptor").ImplementedBy<SelfWeighTransactionPrintableDataAdaptor>().LifeStyle.Singleton);
            builder.Register(Component.For<IPrintableDataAdaptor>().Named("SelfWeighProductPrintableDataAdaptor").ImplementedBy<SelfWeighProductPrintableDataAdaptor>().LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<ISingleLoginManager>()
                                 .Named("ManagerSingleLogin")
                                 .ImplementedBy<SingleLoginManager>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IValidTendersStrategyFilter>()
                                 .Named("IValidTendersStrategyFilter")
                                 .ImplementedBy<AllValidTendersStrategyFilter>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<IPerformanceCounterMultipleInstancesFactory>()
                                 .Named(typeof(IPerformanceCounterMultipleInstancesFactory).FullName)
                                 .ImplementedBy<PerformanceCounterMultipleInstancesFactory>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<ICustomerOrderSerializer>()
                                 .Named("CustomerOrderSerializer")
                                 .ImplementedBy<CustomerOrderSerializer>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IServerStatusCalculator>()
                                 .Named("IServerStatusCalculator")
                                 .ImplementedBy<ServerStatusCalculator>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<ISuspendedOrderLookupHelper>()
                                 .Named("ISuspendedOrderLookupHelper")
                                 .ImplementedBy<SuspendedOrderLookupHelper>()
                                 .LifeStyle.Singleton);
            builder.Register(
                             Component
                                 .For<ISuspendedOrderLookupHelperForTabsLocalMode>()
                                 .Named("ISuspendedOrderLookupHelperForTabsLocalMode")
                                 .ImplementedBy<SuspendedOrderLookupHelper>()
                                 .LifeStyle.Singleton);
            builder.Register(
                             Component
                                 .For<ISuspendedOrderLookupHelperForTabsRemoteMode>()
                                 .Named("ISuspendedOrderLookupHelperForTabsRemoteMode")
                                 .ImplementedBy<SuspendedOrderLookupHelper>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<ISuspendedOrderLookupHelperForTripEnded>()
                                 .Named("ISuspendedOrderLookupHelperForTripEnded")
                                 .ImplementedBy<SuspendedOrderLookupHelper>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IColdstartStatisticsCollector>()
                                 .ImplementedBy<ColdstartStatisticsCollector>()
                                 .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<ILoginRecordDao>()
                                 .Named("ILoginRecordDao")
                                 .ImplementedBy<LoginRecordDao>()
                                 .LifeStyle.Singleton);

            builder.RegisterSingleton<ICredentialFactory, CredentialFactory>();

            builder.Register(Component.For<IProductMovableConvertor>().Named("IProductMovableConvertor").ImplementedBy<ProductMovableConvertor>().LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<ISecurityScaleParametersProvider>()
                                 .Named("SecurityScaleParametersProvider")
                                 .ImplementedBy<SecurityScaleParameters>()
                                 .LifeStyle.Singleton);
            builder.Register(
             Component.For<IUnitLineLogPrioritizationStrategy>().Named("ILineValueCalculationStrategy").ImplementedBy
                 <UnitLineLogPrioritizationStrategy>().LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<IQueueMessage>()
                                 .Named("QueueMessage")
                                 .ImplementedBy<QueueMessage>()
                                 .LifeStyle.Transient);

            builder.Register(
                             Component
                                 .For<ITransportationMessage>()
                                 .Named("RabbitTransportationMessage")
                                 .ImplementedBy<RabbitTransportationMessage>()
                                 .LifeStyle.Transient);
            builder.Register(
                Component
                    .For<IRabbitTransportationMessageFactory>()
                    .Named("RabbitTransportationMessageFactory")
                    .ImplementedBy<RabbitTransportationMessageFactory>()
                    .LifeStyle.Singleton);

            builder.Register(
                             Component
                                 .For<ITransportationEntityChange>()
                                 .Named("RabbitEntityChange")
                                 .ImplementedBy<RabbitEntityChange>()
                                 .LifeStyle.Transient);

            builder.Register(Component.For<IQuery<IQueueErrorMessage, IQueueErrorMessage, IQueueErrorMessage>>().ImplementedBy<QueueQuery>().LifeStyle.Transient);
            builder.Register(Component.For<IQueryCriterionApplier<IQueueErrorMessage, DateRangeCriterion>>().ImplementedBy<DateRangeCriterionApplier>().LifeStyle.Transient);
            builder.Register(Component.For<IQueryCriterionApplier<IQueueErrorMessage, IdCriterion>>().ImplementedBy<IdCriterionApplier>().LifeStyle.Transient);
            builder.Register(Component.For<IQuerySpecificationApplier<IQueueErrorMessage, QueueErrorMessagesPagingSpecification>>().ImplementedBy<QueueErrorMessagesPagingSpecificationApplier>().LifeStyle.Transient);

            builder.Register(Component.For<ITransactionProcessor>().Named("TransactionLogHeaderExec2Ods").ImplementedBy<TransactionLogHeaderExec2Ods>().LifeStyle.Transient);
            builder.Register(Component.For<ITransactionProcessor>().Named("RetailTransactionLogCashierProductivity2Ods").ImplementedBy<RetailTransactionLogCashierProductivity2Ods>().LifeStyle.Transient);
            builder.Register(Component.For<ITransactionProcessor>().Named("RetailTransactionLogItemTax2Ods").ImplementedBy<RetailTransactionLogItemTax2Ods>().LifeStyle.Transient);
            builder.Register(Component.For<ITransactionProcessor>().Named("RetailTransactionLogTenderSales2Ods").ImplementedBy<RetailTransactionLogTenderSales2Ods>().LifeStyle.Transient);
            builder.Register(Component.For<ITransactionProcessor>().Named("RetailTransactionLogItemReturn2Ods").ImplementedBy<RetailTransactionLogItemReturn2Ods>().LifeStyle.Transient);
            builder.Register(Component.For<ITransactionProcessor>().Named("RetailTransactionLogItemSales2Ods").ImplementedBy<RetailTransactionLogItemSales2Ods>().LifeStyle.Transient);
            builder.Register(Component.For<ITransactionProcessor>().Named("RetailTransactionLogPriceModifier2Ods").ImplementedBy<RetailTransactionLogPriceModifier2Ods>().LifeStyle.Transient);
            builder.Register(Component.For<ITransactionProcessor>().Named("RetailTransactionPromotionsSummary2Ods").ImplementedBy<RetailTransactionPromotionsSummary2Ods>().LifeStyle.Transient);
            builder.Register(Component.For<ITransactionProcessor>().Named("ControlTransactionLogEod2Ods").ImplementedBy<ControlTransactionLogEod2Ods>().LifeStyle.Transient);
            builder.Register(Component.For<ITransactionProcessor>().Named("TenderControlTransactionLogCashOfficeBalance2Ods").ImplementedBy<TenderControlTransactionLogCashOfficeBalance2Ods>().LifeStyle.Transient);
            builder.Register(Component.For<ITransactionProcessor>().Named("TenderControlTransactionLogCashOfficeBalanceAdjustment2Ods").ImplementedBy<TenderControlTransactionLogCashOfficeBalanceAdjustment2Ods>().LifeStyle.Transient);
            builder.Register(Component.For<ITransactionProcessor>().Named("TenderControlTransactionLogTipIn2Ods").ImplementedBy<TenderControlTransactionLogTipIn2Ods>().LifeStyle.Transient);

            builder.Register(Component.For<IOdsDao>().ImplementedBy<OdsDao>().LifeStyle.Transient);
            builder.Register(Component.For<IOdsConfigurationDataDao>().ImplementedBy<OdsConfigurationDataDao>().LifeStyle.Transient);
            builder.Register<IJobScheduleProvider, OdsUpdateJobScheduleProvider>("OdsUpdateJobScheduleProvider");
            builder.Register<OdsUpdateJob>();
            JobBehaviour.RegisterBehaviourFor<OdsUpdateJob>(new JobBehaviour
            {
                LockConfiguration = new JobLockConfiguration { Type = LockType.LongRunning }
            });

            builder.Register<IJobScheduleProvider, DataIntegrityJobScheduleProvider>("DataIntegrityJobScheduleProvider");
            builder.Register<DataIntegrityJob>();

            builder.Register(Component.For<IBulkTransactionsProcessor>().Named("BulkTransactionsProcessor").ImplementedBy<BulkTransactionsProcessor>().LifeStyle.Transient);
            builder.Register(Component.For<ITransactionTypeExtractor>().Named("TransactionTypeExtractor").ImplementedBy<TransactionTypeExtractor>().LifeStyle.Transient);
            builder.RegisterSingleton<INotificationDao, NotificationDao>();
            builder.RegisterSingleton<INotificationInstanceDao, NotificationInstanceDao>();
            builder.Register<INotification, Notification>();
            builder.Register<INotificationContent, NotificationContent>();
            builder.Register<IDisplayTiming, DisplayTiming>();
            builder.Register<INotificationInstance, NotificationInstance>();
            builder.Register(Component.For<INotificationTypeValidator>().Named("NotificationValidator").ImplementedBy<NotificationValidator>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<NotificationType, INotification>>().ImplementedBy<NotificationContractToModelMapper>().LifeStyle.Transient);

            builder.Register<IIssuingGiftReceiptValidator, DefaultIssuingGiftReceiptValidator>();

            builder.Register(Component.For<IPriceMaintenanceControlTransaction>().ImplementedBy<PriceMaintenanceControlTransaction>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<PriceMaintenanceRemotableRequest, IPriceMaintenanceControlTransaction>>().ImplementedBy<PriceMaintenanceMapper>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<IPriceMaintenanceControlTransaction, List<SchemaObjects.ControlTransactionDomainSpecific>>>().ImplementedBy<PriceMaintenanceControlTransactionLogMapper>().LifeStyle.Transient);

            builder.Register(Component.For<IProductAvailabilityControlTransaction>().ImplementedBy<ProductAvailabilityControlTransaction>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<IProductAvailabilityControlTransaction, List<SchemaObjects.ControlTransactionDomainSpecific>>>().ImplementedBy<ProductAvailabilityControlTransactionLogMapper>().LifeStyle.Transient);

            builder.Register(Component.For<IForcedSignOffControlTransaction>().ImplementedBy<ForcedSignOffControlTransaction>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<WorkerType, IForcedSignOffControlTransaction>>().ImplementedBy<ForcedSignOffServiceMapper>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<IForcedSignOffControlTransaction, List<SchemaObjects.ControlTransactionDomainSpecific>>>().ImplementedBy<ForcedSignOffControlTransactionLogMapper>().LifeStyle.Transient);

            builder.Register(Component.For<IPasswordChangeControlTransaction>().ImplementedBy<PasswordChangeControlTransaction>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<IdmLoginRequest, IPasswordChangeControlTransaction>>().ImplementedBy<IdmLoginServiceMapper>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<IPasswordChangeControlTransaction, List<SchemaObjects.ControlTransactionDomainSpecific>>>().ImplementedBy<PasswordChangeControlTransactionLogMapper>().LifeStyle.Transient);

            builder.Register(Component.For<IIdmAuthorizationControlTransaction>().ImplementedBy<IdmAuthorizationControlTransaction>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<IDMRequest, IIdmAuthorizationControlTransaction>>().ImplementedBy<IdmAuthorizationMapper>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<IIdmAuthorizationControlTransaction, List<SchemaObjects.ControlTransactionDomainSpecific>>>().ImplementedBy<IdmAuthorizationControlTransactionLogMapper>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<IEnumerable<IPriceChangeTracker>, List<PriceChangeCriteriaResponseType>>>().ImplementedBy<PriceLocalChangeModelToContractMapper>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<List<PriceMaintenanceDTO>, List<IPriceChangeTracker>>>().ImplementedBy<PriceLocalChangeModelToContractMapper>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<PriceChangeSearchCriteriaType, IPriceChangeTracker>>().ImplementedBy<PriceLocalChangeModelToContractMapper>().LifeStyle.Transient);
            builder.Register(Component.For<IEntityDao>().Named("PriceChangeTracker").ImplementedBy<PriceChangeTrackerDao>().LifeStyle.Transient);
            
            builder.Register(Component.For<IDataSecurityFilter>().Named("PasswordDataSecurityFilter").ImplementedBy<PasswordDataSecurityFilter>().LifeStyle.Transient);
            builder.Register(Component.For<IDataSecurityFilter>().Named("RequestDataSecurityFilter").ImplementedBy<RequestDataSecurityFilter>().LifeStyle.Transient);
            builder.Register(Component.For<IResponseDataSecurityFilter>().ImplementedBy<ResponseDataSecurityFilter>().LifeStyle.Transient);

            builder.Register(Component.For<IMaskingConfigurationProvider>().ImplementedBy<MaskingConfigurationProvider>());
            builder.Register(Component.For<IDataMasker>().ImplementedBy<DataMasker>().LifeStyle.Transient);

            builder.Register(Component.For<IRemoteBusinessProcessHandler>().Named("RemoteBusinessProcessHandler").ImplementedBy<RemoteBusinessProcessHandler>().LifeStyle.Transient);
            builder.Register(Component.For<IUnitOfWorkCommittedObserver>().Named("ResumeMobileTransactionUnitOfWorkObserver").ImplementedBy<ResumeMobileTransactionUnitOfWorkObserver>().LifeStyle.Singleton);



            builder.Register(Component.For<IRTIAuditLogWriter>().Named("IRTIAuditLogWriter").ImplementedBy<Log4NetRTIAuditLogWriter>().LifeStyle.Singleton);
            builder.Register(Component.For<IRTIAuditLogBuilder>().Named("IRTIAuditLogBuilder").ImplementedBy<RTIAuditLogBuilder>().LifeStyle.Singleton);

            builder.Register(Component.For<IServiceAgentDao>().Named("IServiceAgentDao").ImplementedBy<ServiceAgentDao>().LifeStyle.Singleton);

            builder.Register(Component.For<IServiceAgentCredentials>().Named("ServiceAgentCredentials").ImplementedBy<ServiceAgentCredentials>().LifeStyle.Transient);

            builder.Register(Component.For<IContextDataParameterFactory>().ImplementedBy<ContextDataParameterFactory>().LifeStyle.Singleton);
            builder.Register(Component.For<IProductSaleLineLogBuilder>().ImplementedBy<ProductSaleLineLogBuilder>().LifeStyle.Transient);
            builder.Register(Component.For<ICustomerPaymentLineLogBuilder>().ImplementedBy<CustomerPaymentLineLogBuilder>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessConfigurationDao>().ImplementedBy<StoreServices.Connectivity.Pos.BusinessConfigurationDao>().LifeStyle.Transient);

            builder.Register<IProductAvailabilityMovableFactory, ProductAvailabilityMovableFactory>();

            builder.RegisterSingleton<IProductAvailabilityFactory, ProductAvailabilityFactory>();

            builder.Register(
                Component.For<IProductAvailabilityStatusFactory>().Named(typeof(ProductAvailabilityStatusFactory).Name)
                         .ImplementedBy<ProductAvailabilityStatusFactory>().LifeStyle.Singleton);

            builder.Register(Component.For<IProductAvailabilityLookupCriteriaFactory>().ImplementedBy<ProductAvailabilityLookupCriteriaFactory>().LifeStyle.Singleton);

            builder
               .Register(Component.For<IProductAvailabilityControlLogBuilder>()
               .Named("ProductAvailabilityControlLogBuilder")
               .ImplementedBy<ProductAvailabilityControlLogBuilder>().LifeStyle.Transient);

            builder.Register(Component.For<ITdmArchiveEventListener>()
                .Named(typeof(DocumentQueueWriter).Name)
                    .ImplementedBy<DocumentQueueWriter>().LifeStyle.Transient);

            builder.Register<ITenderRoundingRule, TenderRoundingRule>();
            builder.Register<IPOSBrandingImage, POSBrandingImage>();
            builder.Register<ISecurityScaleConfiguration, SecurityScaleConfiguration>();

            builder.Register<IBrandingFileSizeConfiguration, BrandingFileSizeConfiguration>();
            builder.RegisterMapper<BrandingFileSizeConfigurationContractToModelMapper>();
            builder.RegisterMapper<BrandingFileSizeConfigurationModelToContractMapper>();

            builder.Register<IPosParametersConfiguration, PosParametersConfiguration>();
            builder.Register<IDetailedOrderCodeDivider, DetailedOrderCodeDivider>();
            builder.Register(Component.For<IMapper<string, RTSLineItemAddRequest>>().Named("DetailedOrderCodeToPOSLogMapper").ImplementedBy<DetailedOrderCodeToPOSLogMapper>().LifeStyle.Transient);

            builder.Register<IPosDepositBusinessConfiguration, PosDepositBusinessConfiguration>();
            builder.Register<IFiscalConfiguration, FiscalConfiguration>();
            builder.Register<IEnableDevicesApiConfiguration, EnableDevicesApiConfiguration>();
            builder.Register(Component.For<IDevicesApiConfigurationSegmentUtility>().Named("DevicesApiConfigurationSegmentUtility").ImplementedBy<DevicesApiConfigurationSegmentUtility>().LifeStyle.Transient);

            builder.Register<IEpsConfiguration, EpsConfiguration>();

            builder.Register(Component.For<IRoundingExecuter>().Named(typeof(RoundingExecuter).Name).ImplementedBy<RoundingExecuter>().LifeStyle.Transient);

            builder.Register<IRoundingInfo, RoundingInfo>();
            builder.Register<IRoundingStrategyFactory, RoundingStrategyFactory>();
            builder.Register(Component.For<IRoundingStrategy>().Named("RoundingStrategyForReturnTransaction").ImplementedBy<RoundingStrategyForReturnTransaction>().LifeStyle.Transient);
            builder.Register(Component.For<IRoundingStrategy>().Named("RoundingStrategyForSalesTransaction").ImplementedBy<RoundingStrategyForSalesTransaction>().LifeStyle.Transient);

            builder.Register<IAutoVoidAbandonedTransactionJobConfiguration, AutoVoidAbandonedTransactionJobConfiguration>();
            builder.Register<ITabsConfiguration, TabsConfiguration>();
            builder.Register<ITipsConfiguration, TipsConfiguration>();
            builder.Register<IMemberAccountsToDisplayConfiguration, MemberAccountsToDisplayConfiguration>();
            builder.RegisterMapper<MemberAccountsToDisplayConfigurationMapper>();
            builder.RegisterMapper<MemberAccountsToDisplayConfigurationTypeMapper>();

            builder.RegisterMapper<TabsConfigurationMapper>();
            builder.RegisterMapper<TabsConfigurationTypeMapper>();
            builder.RegisterMapper<TipsConfigurationMapper>();
            builder.RegisterMapper<ActivityIndicationTimeoutConfigurationMapper>();
            builder.RegisterMapper<TipsConfigurationTypeMapper>();
            builder.RegisterMapper<ActivityIndicationTimeoutConfigurationTypeMapper>();
            builder.Register(Component.For<ITabsInfoExchangeService>().ImplementedBy<TabsInfoExchangeService>().LifeStyle.Transient);
            builder.Register(Component.For<IUpdateTabStatusService>().ImplementedBy<UpdateTabStatusService>().LifeStyle.Transient);
            builder.Register(Component.For<ITabInfo>().Named("TabInfo").ImplementedBy<TabInfo>().LifeStyle.Transient);

            builder.Register(Component.For<IAutoLoadConfiguration>().ImplementedBy<AutoLoadConfiguration>().LifeStyle.Transient);
            builder.Register(Component.For<IAutoLoadConfigurationLookupStrategy>().ImplementedBy<AutoLoadConfigurationLookupStrategy>().LifeStyle.Transient);

            builder.Register(Component.For<IActivityIndicationTimeoutConfiguration>().Named("IActivityIndicationTimeoutConfiguration").ImplementedBy<ActivityIndicationTimeoutConfiguration>().LifeStyle.Transient);

            var container = builder.GetRegistrationFacility<IWindsorContainer>();

            container
                .Register(Castle.MicroKernel.Registration.Component.For(
                    typeof(IRecipientVersionResolver),
                    typeof(IEventHandler<ParentNodeVersionChangedEvent>),
                    typeof(IEventHandler<ChildNodeVersionChangedEvent>))
                    .ImplementedBy(typeof(RecipientVersionResolver)).LifeStyle.Singleton);

            builder.RegisterSingleton<IUploadProcessingManager, UploadProcessingManager>();

            builder.Register(
                Component.For<ISupplierDao>().Named("ISupplierDao").ImplementedBy
                    <SupplierDao>().LifeStyle.Singleton);

            builder.Register(
                Component.For<ISupplierFactory>().Named("ISupplierFactory").ImplementedBy
                    <SupplierFactory>().LifeStyle.Singleton);

            builder.Register(Component.For<IDataProtectionConfiguration>().Named("IDataProtectionConfiguration").ImplementedBy<DataProtectionConfiguration>().LifeStyle.Transient);
            builder.RegisterMapper<DataProtectionConfigurationMapper>();
            builder.RegisterMapper<DataProtectionConfigurationTypeMapper>();
            builder.RegisterMapper<DataProtectionBusinessConfigurationMapper>();

            builder.RegisterMapper<CodeSignatureVerificationConfigurationMapper>();
            builder.RegisterMapper<CodeSignatureVerificationConfigurationTypeMapper>();
            builder.Register(Component.For<ICodeSignatureVerificationConfiguration>().Named("CodeSignatureVerificationConfiguration").ImplementedBy<CodeSignatureVerificationConfiguration>().LifeStyle.Transient);

            builder.Register(Component.For<IExtraPriceCalculationPolicy>().Named("IExtraPriceCalculationPolicy").ImplementedBy<ExtraPriceCalculationPolicy>().LifeStyle.Transient);
            builder.RegisterMapper<ExtraPriceCalculationPolicyMapper>();
            builder.RegisterMapper<ExtraPriceCalculationPolicyToContractMapper>();
            builder.RegisterMapper<ExtraPriceCalculationPolicyModelToContractMapper>();

            builder.Register(Component.For<IOpenDrawer>().Named("IOpenDrawer").ImplementedBy<OpenDrawer>().LifeStyle.Transient);
            builder.RegisterMapper<OpenDrawerConfigurationMapper>();
            builder.RegisterMapper<OpenDrawerConfigurationMapperType>();
            builder.RegisterMapper<OpenDrawerBusinessConfigurationMapper>();

            builder.Register(
                Component.For<IDataProtector>().Named("MaskOnRightByPercentDataProtector").ImplementedBy<MaskOnRightByPercentDataProtector>().LifeStyle.Transient);

            builder.Register(
                Component.For<IDataProtector>().Named("MaskOnLeftFixLengthDataProtector").ImplementedBy<MaskOnLeftFixLengthDataProtector>().LifeStyle.Transient);

            builder.Register(Component.For<ITenderRoundingRuleValidator>().Named("TenderRoundingRuleAlreadyExistsValidator").ImplementedBy<TenderRoundingRuleAlreadyExistsValidator>().LifeStyle.Transient);
            builder.Register(Component.For<ITenderRoundingRuleValidator>().Named("TenderRoundingRuleDoesNotExistValidator").ImplementedBy<TenderRoundingRuleDoesNotExistValidator>().LifeStyle.Transient);
            builder.Register(Component.For<ITenderRoundingRuleValidator>().Named("TenderRoundingRuleNameAndRoundingTenderValidator").ImplementedBy<TenderRoundingRuleNameAndRoundingTenderValidator>().LifeStyle.Transient);
            builder.Register(Component.For<ITenderRoundingRuleValidator>().Named("TenderRoundingRuleValuesValidator").ImplementedBy<TenderRoundingRuleValuesValidator>().LifeStyle.Transient);
            builder.Register(Component.For<ITenderRoundingRuleValidator>().Named("TenderRoundingRuleAssociationValidator").ImplementedBy<TenderRoundingRuleAssociationValidator>().LifeStyle.Transient);
            builder.Register(Component.For<ITenderRoundingRuleValidator>().Named("TenderRoundingRuleMessageValidator").ImplementedBy<TenderRoundingRuleMessageValidator>().LifeStyle.Transient);
            builder.Register(Component.For<ITenderRoundingMessageBuilder>().Named("TenderRoundingMessageBuilder").ImplementedBy<TenderRoundingMessageBuilder>().LifeStyle.Transient);

            builder.Register(
                Component.For<IRefundVoucherValidator>().Named("RefundVoucherValidator").ImplementedBy<RefundVoucherValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IDefaultRefundVoucherCreator>().Named("DefaultRefundVoucherCreator").ImplementedBy<DefaultRefundVoucherCreator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IRefundVoucherChangeStatusValidator>().Named("RefundVoucherChangeStatusValidator").ImplementedBy<RefundVoucherChangeStatusValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IRefundVoucherBusinessConfigurationValidator>().Named("RefundVoucherBusinessConfigurationValidator").ImplementedBy<RefundVoucherBusinessConfigurationValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IRefundVoucherHelper>().Named("RefundVoucherHelper").ImplementedBy<RefundVoucherHelper>().LifeStyle.Transient);
            builder.Register(
                Component.For<IApplicationLinkValidator>().Named("ApplicationLinkValidator").ImplementedBy<ApplicationLinkValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IReturnOnlyProductLocator>().Named("ReturnOnlyProductLocator").ImplementedBy<ReturnOnlyProductLocator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IPOSBrandingImageValidator>().Named("FileValidator").ImplementedBy<FileValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IPOSBrandingImageValidator>().Named("SubDisplayValidator").ImplementedBy<SubDisplayValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IPOSBrandingImageValidator>().Named("BusinessUnitValidator").ImplementedBy<BusinessUnitValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IPOSBrandingImageValidator>().Named("TouchpointTypeValidator").ImplementedBy<TouchpointTypeValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IPOSBrandingImageValidator>().Named("RetailSegmentValidator").ImplementedBy<RetailSegmentValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IPOSBrandingImageValidator>().Named("PositionValidator").ImplementedBy<PositionValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IPOSBrandingImageValidator>().Named("POSBrandingImageAlreadyExistsValidator").ImplementedBy<POSBrandingImageAlreadyExistsValidator>().LifeStyle.Transient);
            builder.Register(
                Component.For<IPOSBrandingImageValidator>().Named("POSBrandingImageDoesNotExistValidator").ImplementedBy<POSBrandingImageDoesNotExistValidator>().LifeStyle.Transient);

            builder.Register(
              Component.For<IActivityReferenceConfigurationValidator>().Named("ActivityNameMandatoryValidator").ImplementedBy<ActivityNameMandatoryValidator>().LifeStyle.Transient);

            builder.Register(
             Component.For<IActivityReferenceConfigurationValidator>().Named("ActivityNameAlreadyExistsValidator").ImplementedBy<ActivityNameAlreadyExistsValidator>().LifeStyle.Transient);

            builder.Register(
              Component.For<IActivityReferenceConfigurationValidator>().Named("ActivityNameDoesNotExistValidator").ImplementedBy<ActivityNameDoesNotExistValidator>().LifeStyle.Transient);

            builder.Register(Component.For<IOrderCalculationValidator>().Named("OrderCalculationLineItemValidator").ImplementedBy<OrderCalculationLineItemValidator>().LifeStyle.Transient);
            builder.Register(Component.For<IOrderCalculationCustomerDataProvider>().Named("OrderCalculationCustomerDataProvider").ImplementedBy<OrderCalculationCustomerDataProvider>().LifeStyle.Transient);

            builder.RegisterSingleton<IRetailSegmentTagValueComparer, RetailSegmentTagValueComparer>("IRetailSegmentTagValueComparer");

            builder.Register(Component.For<IEndOfDayValidator>().Named("EndOfDayNotExpectedValidator").ImplementedBy<EndOfDayNotExpectedValidator>().LifeStyle.Transient);
            builder.Register(Component.For<IEndOfDayValidator>().Named("EndOfDayInProgressTransactionValidator").ImplementedBy<EndOfDayInProgressTransactionValidator>().LifeStyle.Transient);

            builder.RegisterMapper<OrderViewDataToTransactionMapper>();
            builder.Register(Component.For<IMapper<OrderViewData, ICustomerOrder>>().Named("OrderToCustomerOrderMapper").ImplementedBy<OrderToCustomerOrderMapper>());
            builder.RegisterMapper<CustomerOrderToOrderDataMapper>();
            builder.RegisterMapper<OrderViewDataToOrderDataMapper>();
            builder.Register(Component.For<IRetailTransactionLogDocumentCreationCoreVisitor>().Named("ExternalTransactionLogVisitor").ImplementedBy<ExternalTransactionLogVisitor>().LifeStyle.Transient);
            builder.Register(Component.For<IOrderLineLogDocumentCreationVisitor>().Named("ExternalOrderLineLogVisitor").ImplementedBy<ExternalOrderLineLogVisitor>().LifeStyle.Transient);
            builder.RegisterSingleton<ICustomerScanDataTLogProvider, CustomerScanDataTLogProvider>();


            RegisterMobileFarmEndOfDayServices(builder);

            builder.Register(Component.For<IExternalOrderDeadLetterExchangePublisher>().ImplementedBy<ExternalOrderDeadLetterExchangePublisher>().LifeStyle.Transient);

            builder.Register(Component.For<IMissingTlogsDaoHelper>().ImplementedBy<MissingTlogsDaoHelper>().LifeStyle.Singleton);

            builder.RegisterSingleton<IBusinessRuleCacheFactory, BusinessRuleCacheFactory>();
            builder.RegisterSingleton<IConditionalRestrictionCacheFactory, ConditionalRestrictionCacheFactory>();

            builder.Register(Component.For<ICertificateProviderDao>().Named("ICertificateProviderDao").ImplementedBy<CertificateProviderDao>().LifeStyle.Singleton);
            builder.Register(Component.For<ICertificateProvider>().Named("CertificateProvider").ImplementedBy<CertificateProvider>().LifeStyle.Singleton);
            builder.Register(Component.For<ICertificateData>().Named("ICertificateData").ImplementedBy<CertificateData>().LifeStyle.Transient);
            builder.Register(Component.For<ICertificateConfiguration>().Named("ICertificateConfiguration").ImplementedBy<CertificateConfiguration>().LifeStyle.Transient);
            builder.Register(Component.For<ICertificateStoreFactory>().Named("ICertificateStoreFactory").ImplementedBy<CertificateStoreFactory>().LifeStyle.Singleton);
            builder.RegisterMapper<CertificateConfigurationToCertificateDataMapper>();
            builder.RegisterMapper<CertificateConfigurationTypeToCertificateConfigurationMapper>();
            builder.RegisterMapper<CertificateConfigurationTypeV2ToCertificateConfigurationMapper>();

            builder.Register(Component.For<IItemConsolidator>().Named("ItemConsolidator").ImplementedBy<ItemConsolidator>().LifeStyle.Singleton);

            builder.Register<IDepositCashDuringSaleModeConfiguration, DepositCashDuringSaleModeConfiguration>();
            builder.RegisterMapper<DepositCashDuringSaleModeConfigurationMapper>();
            builder.RegisterMapper<DepositCashDuringSaleModeConfigurationTypeMapper>();

            builder.Register<IOnlineItemProviderConfiguration, OnlineItemProviderConfiguration>();
            builder.RegisterMapper<OnlineItemProviderConfigurationMapper>();
            builder.RegisterMapper<OnlineItemProviderConfigurationTypeMapper>();

            builder.Register(Component.For<IDisplayAddLoyaltyConfiguration>().ImplementedBy<DisplayAddAddLoyaltyConfiguration>().LifeStyle.Transient);
            builder.RegisterMapper<DisplayAddLoyaltyConfigurationMapper>();

            builder.Register<IVirtualKeyboardConfiguration, VirtualKeyboardConfiguration>();
            builder.Register<IVirtualKeyboardConfigurationsDao, VirtualKeyboardConfigurationsDao>();

            builder.Register<IDynamicFormsConfiguration, DynamicFormsConfiguration>();
            builder.Register<IDynamicFormsConfigurationDao, DynamicFormsConfigurationDao>();

            builder.Register<IOLAMessageMovableDao, OLAMessageMovableDao>();
            builder.Register<IOLAMessage, OLAMessage>();
            builder.Register<IOLAMessageSearchCriteria, OLAMessageSearchCriteria>();
            builder.Register<IOLAMessageValidator, OLAMessageValidator>();


            builder.Register(Component.For<Retalix.StoreServices.Model.Finance.BusinessDate.IBusinessDayPeriodEndDateLocator>().Named("BusinessDayPeriodEndDateLocator").ImplementedBy<BusinessDayPeriodEndDateLocator>().LifeStyle.Transient);
            builder.Register(Component.For<Retalix.StoreServices.Model.Finance.BusinessDate.IPeriodIsNotSettledValidator>().Named("PeriodIsNotSettledValidator").ImplementedBy<BusinessDateValidator>().LifeStyle.Transient);
            builder.Register(Component.For<Retalix.StoreServices.Model.Finance.BusinessDate.ITillBusinessDateValidator>().Named("TillBusinessDateValidator").ImplementedBy<BusinessDateValidator>().LifeStyle.Transient);
            builder.Register(Component.For<Retalix.StoreServices.Model.Finance.BusinessDate.ISafeBusinessDateValidator>().Named("SafeBusinessDateValidator").ImplementedBy<BusinessDateValidator>().LifeStyle.Transient);
            builder.Register(Component.For<Retalix.StoreServices.Model.Finance.BusinessDate.IBankingSafeBusinessDateValidator>().Named("BankingSafeBusinessDateValidator").ImplementedBy<BusinessDateValidator>().LifeStyle.Transient);
            //BusinessDateValidator


            builder.Register(Component.For<IMaskingParametersConfiguration>().Named("IMaskingParametersConfiguration").ImplementedBy<MaskingParametersConfiguration>().LifeStyle.Singleton);
            builder.RegisterMapper<MaskingParametersConfigurationMapperType>();
            builder.RegisterMapper<MaskingParametersConfigurationToContractMapper>();
            builder.RegisterMapper<MaskingParametersConfigurationToModelMapper>();
            builder.Register<IMaskingConfigurationMovableDao, MaskingConfigurationMovableDao>();
            builder.Register<IMaskingConfiguration, MaskingConfiguration>();

            builder.Register(Component.For<IAmountEmbeddedWeightConfig>().Named("AmountEmbeddedWeightConfig").ImplementedBy<AmountEmbeddedWeightConfig>().LifeStyle.Transient);

            builder.Register<TransactionGapFillerJob>();
            builder.Register(Component.For<IJobScheduleProvider>().Named("TransactionGapFillerJobScheduler").ImplementedBy<TransactionGapFillerJobScheduler>().LifeStyle.Transient);

            builder.Register(Component.For<IGenericBusinessConfiguration>().Named("IBusinessConfigurationEntity").ImplementedBy<GenericBusinessConfiguration>().LifeStyle.Transient);
            builder.RegisterMapper<BusinessConfigurationMapper>();

            builder.Register(Component.For<IRecalculateReturnTaxConfiguration>().Named("RecalculateReturnTaxConfiguration").ImplementedBy<RecalculateReturnTaxConfiguration>().LifeStyle.Transient);
            builder.RegisterMapper<RecalculateReturnTaxMapper>();

            builder.Register(Component.For<IEpsOperationalControlLogHandler>().Named("EpsOperationalControlLogHandler").ImplementedBy<EpsOperationalControlLogHandler>().LifeStyle.Transient);
            builder.RegisterSingleton<ICustomerConfigurationProviderNameResolver, CustomerProviderNameResolver>("CustomerProviderNameResolver");

            builder.Register(Component.For<IDelayedInterventionConfiguration>().Named("IDelayedInterventionConfiguration").ImplementedBy<DelayedInterventionConfiguration>().LifeStyle.Transient);
            builder.RegisterMapper<DelayedInterventionConfigurationMapper>();
            builder.RegisterMapper<DelayedInterventionConfigurationTypeMapper>();
            builder.RegisterMapper<DelayedInterventionBusinessConfigurationMapper>();

            builder.Register(Component.For<IPrintableDataAdaptor>().Named("PriceQueriesPrintoutProductPrintableDataAdaptor").ImplementedBy<PriceQueriesPrintoutProductPrintableDataAdaptor>().LifeStyle.Singleton);
            builder.Register(Component.For<IPrintableDataAdaptor>().Named("PriceQueriesPrintoutPrintableDataAdaptor").ImplementedBy<PriceQueriesPrintoutPrintableDataAdaptor>().LifeStyle.Singleton);

            builder.Register(Component.For<IPrintableSourceDataRetriever<List<PriceQueriesPrintoutReceiptData>>>().Named("PriceQueriesPrintoutSourceDataRetriever")
                                 .ImplementedBy<PriceQueriesPrintoutSourceDataRetriever>().LifeStyle.Singleton);

            builder.Register<IEntityDao, TranVatSealRecordDao>("TranVatSealRecord");

            builder.Register(Component.For<ICustomerHandler>().Named("CustomerHandler").ImplementedBy<CustomerHandler>().LifeStyle.Transient);
            builder.Register(Component.For<IMultiCustomerBehaviorProvider>().Named("MultiCustomerBehaviorProvider").ImplementedBy<MultiCustomerBehaviorProvider>().LifeStyle.Transient);
        }

        private static void RegisterMobileFarmEndOfDayServices(IComponentInstaller builder)
        {
            builder.Register(new[] { typeof(IMobileFarmEndOfDayConfiguration), typeof(IConfigurationDescriptor) },
                typeof(MobileFarmEndOfDayConfiguration), "MobileFarmEndOfDayConfiguration", true);
            builder.Register(Component.For<IMobileFarmEndOfDayValidator>().Named("MobileFarmEndOfDayInProgressTransactionValidator").ImplementedBy<MobileFarmEndOfDayInProgressTransactionValidator>().LifeStyle.Singleton);
            builder.Register(Component.For<IMobileFarmEodTouchPointsProvider>().ImplementedBy<MobileTouchPointsToRunSelector>().LifeStyle.Singleton);
            builder.Register(Component.For<IEndOfDayExecutorAsync>().ImplementedBy<MobileFarmEndOfDayExecuter>().LifeStyle.Transient);
            builder.Register(Component.For<IMobileTouchPointsProvider>().ImplementedBy<MobileFarmTouchPointsProvider>().LifeStyle.Singleton);
            builder.Register(Component.For<IJobScheduleProvider>().Named("MobileFarmEndOfDayJobScheduleProvider").ImplementedBy<MobileFarmEndOfDayJobScheduleProvider>().LifeStyle.Singleton);
            builder.Register(Component.For<IBusinessDayManyUnits>().ImplementedBy<BusinessDayManyUnits>().LifeStyle.Singleton);
            builder.Register(Component.For<IEndOfDayExecutor>().ImplementedBy<EndOfDayExecutor>().LifeStyle.Transient);
            builder.Register(Component.For<IEndOfDayObserversHandler>().Named("EndOfDayObserversHandler").ImplementedBy<EndOfDayObserversHandler>().LifeStyle.Singleton);

            builder.Register<MobileFarmEndOfDayJob>();
        }
    }
}
