using Retalix.CDM.BusinessServices;
using Retalix.CDM.BusinessServices.BackwardCompatibility.CustomerPromotionRegistration;
using Retalix.CDM.BusinessServices.CustomerMerge;
using Retalix.CDM.BusinessServices.New.AccountType;
using Retalix.CDM.BusinessServices.New.AccountType.AccountResetPolicy;
using Retalix.CDM.BusinessServices.New.AccountType.BackwardCompatibility;
using Retalix.CDM.BusinessServices.New.AccountTypeLookup;
using Retalix.CDM.BusinessServices.New.Agreement;
using Retalix.CDM.BusinessServices.New.AutoAddConfiguration;
using Retalix.CDM.BusinessServices.New.CreditCardType;
using Retalix.CDM.BusinessServices.New.CreditCardTypeLookup;
using Retalix.CDM.BusinessServices.New.CustomerAccount;
using Retalix.CDM.BusinessServices.New.CustomerAccountTransactionLookup;
using Retalix.CDM.BusinessServices.New.CustomerAccountTypeLookup;
using Retalix.CDM.BusinessServices.New.CustomerAffiliationLookup;
using Retalix.CDM.BusinessServices.New.CustomerEmailVerification;
using Retalix.CDM.BusinessServices.New.CustomerLookup.BackwardCompatibility;
using Retalix.CDM.BusinessServices.New.CustomerOnlineSubscriptionLookup;
using Retalix.CDM.BusinessServices.New.CustomerOnlineSubscriptionMaintenance;
using Retalix.CDM.BusinessServices.New.CustomerPaymentMeanAdd;
using Retalix.CDM.BusinessServices.New.CustomerPaymentMeanDetailsLookup;
using Retalix.CDM.BusinessServices.New.CustomerPaymentMeanRemove;
using Retalix.CDM.BusinessServices.New.CustomerPaymentMeanUpdate;
using Retalix.CDM.BusinessServices.New.CustomerPaymentMeanWithLTTlookup;
using Retalix.CDM.BusinessServices.New.CustomerRegisteredPromotionLookup;
using Retalix.CDM.BusinessServices.New.CustomerRuleValidationLookup;
using Retalix.CDM.BusinessServices.New.Loyalty.LoyaltyProgramVisualLookup;
using Retalix.CDM.BusinessServices.New.Loyalty.LoyaltyProgramVisualMaintenance;
using Retalix.CDM.BusinessServices.New.Segment;
using Retalix.CDM.BusinessServices.New.SelfService;
using Retalix.CDM.BusinessServices.New.ShoppingList;
using Retalix.Contracts.Generated.Server;
using Retalix.StoreServer.CoreConfiguration.CoreComponents.ConfigBuilder;
using Retalix.StoreService.BusinessServices.Infrastructure.RetentionPolicy;
using Retalix.StoreServices.AccessServices.AccessService;
using Retalix.StoreServices.BusinessComponents.Alert;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication.Command;
using Retalix.StoreServices.BusinessComponents.Organization.TouchPointApplication.KeyboardConfiguration;
using Retalix.StoreServices.BusinessComponents.Reporting.Receipt.Layout;
using Retalix.StoreServices.BusinessComponents.Reporting.Receipt.Strategy;
using Retalix.StoreServices.BusinessServices.FrontEnd.AccessServices;
using Retalix.StoreServices.BusinessServices.FrontEnd.AccessServices.EntityPolicyDelete;
using Retalix.StoreServices.BusinessServices.FrontEnd.AccessServices.EntityPolicyMaintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.Alert;
using Retalix.StoreServices.BusinessServices.FrontEnd.ApplyTaxDiscount;
using Retalix.StoreServices.BusinessServices.FrontEnd.ApplyTaxExemption;
using Retalix.StoreServices.BusinessServices.FrontEnd.ApplyTaxReversal;
using Retalix.StoreServices.BusinessServices.FrontEnd.AutoDeclaration;
using Retalix.StoreServices.BusinessServices.FrontEnd.AutoLoad;
using Retalix.StoreServices.BusinessServices.FrontEnd.AuxiliaryTool;
using Retalix.StoreServices.BusinessServices.FrontEnd.BusinessOffers;
using Retalix.StoreServices.BusinessServices.FrontEnd.BusinessRules.OperationNotify;
using Retalix.StoreServices.BusinessServices.FrontEnd.BusinessUnit;
using Retalix.StoreServices.BusinessServices.FrontEnd.BusinessUnit.NonResettableData;
using Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.Account;
using Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.Account.Shifts;
using Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.AccountProfileValidators;
using Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.Balancing;
using Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.Configuration;
using Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.Declaration;
using Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.FundTransfer;
using Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.OpenDrawer;
using Retalix.StoreServices.BusinessServices.FrontEnd.CertificateSlipGenerator;
using Retalix.StoreServices.BusinessServices.FrontEnd.Configuration.Contract;
using Retalix.StoreServices.BusinessServices.FrontEnd.Configuration.Lookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.Configuration.Maintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.CouponAdd;
using Retalix.StoreServices.BusinessServices.FrontEnd.CouponCancel;
using Retalix.StoreServices.BusinessServices.FrontEnd.CouponInstance;
using Retalix.StoreServices.BusinessServices.FrontEnd.CouponInstance.Coupon;
using Retalix.StoreServices.BusinessServices.FrontEnd.CouponParametersLookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.CouponParametersSave;
using Retalix.StoreServices.BusinessServices.FrontEnd.CouponRewardOverride;
using Retalix.StoreServices.BusinessServices.FrontEnd.CouponSeries;
using Retalix.StoreServices.BusinessServices.FrontEnd.CustomerAdd;
using Retalix.StoreServices.BusinessServices.FrontEnd.CustomerOrders;
using Retalix.StoreServices.BusinessServices.FrontEnd.CustomerOrders.FormData;
using Retalix.StoreServices.BusinessServices.FrontEnd.DataAdd;
using Retalix.StoreServices.BusinessServices.FrontEnd.DataIntegrity;
using Retalix.StoreServices.BusinessServices.FrontEnd.DataIntegrity.TransactionDataIntegrity;
using Retalix.StoreServices.BusinessServices.FrontEnd.DataIntegrity.TransactionDataIntegrity.Validators;
using Retalix.StoreServices.BusinessServices.FrontEnd.DataPattern;
using Retalix.StoreServices.BusinessServices.FrontEnd.DataPattern.GS1;
using Retalix.StoreServices.BusinessServices.FrontEnd.DataPatternMetadataMaintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.DataProtection;
using Retalix.StoreServices.BusinessServices.FrontEnd.Denomination;
using Retalix.StoreServices.BusinessServices.FrontEnd.Denomination.Validators;
using Retalix.StoreServices.BusinessServices.FrontEnd.Department.DepartmentLookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.Department.Deposit;
using Retalix.StoreServices.BusinessServices.FrontEnd.Department.SaveOrUpdate;
using Retalix.StoreServices.BusinessServices.FrontEnd.DepositCashDuringSaleMode;
using Retalix.StoreServices.BusinessServices.FrontEnd.Device;
using Retalix.StoreServices.BusinessServices.FrontEnd.Device.Ctm;
using Retalix.StoreServices.BusinessServices.FrontEnd.DeviceConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.DigitalReceipt;
using Retalix.StoreServices.BusinessServices.FrontEnd.DisposalMethod;
using Retalix.StoreServices.BusinessServices.FrontEnd.DMS;
using Retalix.StoreServices.BusinessServices.FrontEnd.DMS.ColdStart;
using Retalix.StoreServices.BusinessServices.FrontEnd.DMS.EntityConfiguration.Lookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.DMS.EntityConfiguration.Maintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.DMS.GetDmsMessageDataService;
using Retalix.StoreServices.BusinessServices.FrontEnd.DMS.Heartbeat;
using Retalix.StoreServices.BusinessServices.FrontEnd.DMS.Monitoring;
using Retalix.StoreServices.BusinessServices.FrontEnd.DMS.PhysicalToLogicalHierarchy;
using Retalix.StoreServices.BusinessServices.FrontEnd.Document;
using Retalix.StoreServices.BusinessServices.FrontEnd.EligibilityPolicyMaintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.EOD;
using Retalix.StoreServices.BusinessServices.FrontEnd.EventsHistory;
using Retalix.StoreServices.BusinessServices.FrontEnd.ExpressionMetadata;
using Retalix.StoreServices.BusinessServices.FrontEnd.ExtendedPrePriceOverride;
using Retalix.StoreServices.BusinessServices.FrontEnd.ExtraPriceCalculationPolicy;
using Retalix.StoreServices.BusinessServices.FrontEnd.GenerateEndOfTripBarcode;
using Retalix.StoreServices.BusinessServices.FrontEnd.GetPaybackTenders;
using Retalix.StoreServices.BusinessServices.FrontEnd.GiftReceiptRedemption;
using Retalix.StoreServices.BusinessServices.FrontEnd.Globalization;
using Retalix.StoreServices.BusinessServices.FrontEnd.Infrastructure.LocalizedTexts;
using Retalix.StoreServices.BusinessServices.FrontEnd.ItemHierarchy;
using Retalix.StoreServices.BusinessServices.FrontEnd.ItemView;
using Retalix.StoreServices.BusinessServices.FrontEnd.Kit;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.DataWithLineItemAdd;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.ItemLookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemAdd;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemAddBulk;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemCancel;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemGiftReceipt;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemInstruction;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemKitModifier;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemNoteModifier;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemPriceModifier;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemQuantityModifier;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemReturn;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemTareModifier;
using Retalix.StoreServices.BusinessServices.FrontEnd.LogDocument.LogDocumentLookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.Loyalty.PointsAdjustment;
using Retalix.StoreServices.BusinessServices.FrontEnd.Loyalty.SavingAccount;
using Retalix.StoreServices.BusinessServices.FrontEnd.MenuConfiguration.AvailableTimeMaintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.MenuConfiguration.Lookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.MenuConfiguration.Maintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.MessageParameterDefinition;
using Retalix.StoreServices.BusinessServices.FrontEnd.Messages;
using Retalix.StoreServices.BusinessServices.FrontEnd.MessagesQueue;
using Retalix.StoreServices.BusinessServices.FrontEnd.OLAMessage;
using Retalix.StoreServices.BusinessServices.FrontEnd.OLAMessage.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.OnlineItem.BalanceInquery;
using Retalix.StoreServices.BusinessServices.FrontEnd.OnlineItem.OnlineItemLookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.OnlineItem.OnlineItemProviderConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.OnlineItem.UpdateOnlineProduct;
using Retalix.StoreServices.BusinessServices.FrontEnd.OpenCloseDay;
using Retalix.StoreServices.BusinessServices.FrontEnd.OpenCloseDay.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.OrderCalculation;
using Retalix.StoreServices.BusinessServices.FrontEnd.OrderManagment;
using Retalix.StoreServices.BusinessServices.FrontEnd.OwnBagConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.PickUp;
using Retalix.StoreServices.BusinessServices.FrontEnd.PriceQuery;
using Retalix.StoreServices.BusinessServices.FrontEnd.PrintSummary;
using Retalix.StoreServices.BusinessServices.FrontEnd.ProductAvailibility.LookUp;
using Retalix.StoreServices.BusinessServices.FrontEnd.ProductAvailibility.Maintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.ProductPaymentRestriction.Lookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.ProductPaymentRestriction.Maintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.Advertisement;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.ApprovalMessage;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.Continuity;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.ExecutionInformation;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.Group;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.IncentiveMessageRule.LookUP;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.IncentiveMessageRule.Maintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.Instruction;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.MaintenanceObserver;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.ManualPromotionAdd;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.ManualPromotionCancel;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.SecondPE;
using Retalix.StoreServices.BusinessServices.FrontEnd.ReasonCode.Lookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.ReasonCode.Maintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.Reporting;
using Retalix.StoreServices.BusinessServices.FrontEnd.ReportingWarehouse;
using Retalix.StoreServices.BusinessServices.FrontEnd.Rescan;
using Retalix.StoreServices.BusinessServices.FrontEnd.RestrictedCard;
using Retalix.StoreServices.BusinessServices.FrontEnd.Restriction;
using Retalix.StoreServices.BusinessServices.FrontEnd.RetailSegment;
using Retalix.StoreServices.BusinessServices.FrontEnd.RetailTransaction;
using Retalix.StoreServices.BusinessServices.FrontEnd.RetailTransactionLogDirectInsert;
using Retalix.StoreServices.BusinessServices.FrontEnd.RetailTransactionLogFind;
using Retalix.StoreServices.BusinessServices.FrontEnd.Retransmit;
using Retalix.StoreServices.BusinessServices.FrontEnd.Retransmit.Converters;
using Retalix.StoreServices.BusinessServices.FrontEnd.Retransmit.DMS;
using Retalix.StoreServices.BusinessServices.FrontEnd.Retransmit.DMS.Rabbit;
using Retalix.StoreServices.BusinessServices.FrontEnd.Retransmit.Lookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.Retransmit.Model;
using Retalix.StoreServices.BusinessServices.FrontEnd.Retransmit.Model.DMS;
using Retalix.StoreServices.BusinessServices.FrontEnd.Retransmit.Model.DMS.MessageHandlers;
using Retalix.StoreServices.BusinessServices.FrontEnd.Retransmit.Publishers;
using Retalix.StoreServices.BusinessServices.FrontEnd.Retransmit.Strategies;
using Retalix.StoreServices.BusinessServices.FrontEnd.Retransmit.Validators;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.BottleDeposit.BottleDepositConsumablesLookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.CalculateReturnPrice;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.CustomerDetailsSave;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.FindOriginalTransactionDepartments;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.ForcedExchange;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.GetReturnDataFromOriginalTransaction;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.PolicyMaintenance.Copy;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.PolicyMaintenance.Delete;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.PolicyMaintenance.Lookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.PolicyMaintenance.Save;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.PolicyMaintenance.SimulateBusinessDate;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.PolicyMaintenance.Submit;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.PolicyMaintenance.UpdateReturnPolicyEndDateTime;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.RefundSwitchTenderAproval;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.ReturnReasonCodeLookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.Returns.ValidateLineItemReturn;
using Retalix.StoreServices.BusinessServices.FrontEnd.RTIBuilder.General;
using Retalix.StoreServices.BusinessServices.FrontEnd.ScheduledJob;
using Retalix.StoreServices.BusinessServices.FrontEnd.ScoInterventions;
using Retalix.StoreServices.BusinessServices.FrontEnd.ScoInterventions.Configuration;
using Retalix.StoreServices.BusinessServices.FrontEnd.SecurityScale.SecurityWeightTolerance.Lookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.SecurityScale.SecurityWeightTolerance.Maintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.SecurityScale.Validation;
using Retalix.StoreServices.BusinessServices.FrontEnd.Servers;
using Retalix.StoreServices.BusinessServices.FrontEnd.ServiceWrapper;
using Retalix.StoreServices.BusinessServices.FrontEnd.Shifts;
using Retalix.StoreServices.BusinessServices.FrontEnd.Shifts.mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.StoreSalesReport;
using Retalix.StoreServices.BusinessServices.FrontEnd.Supplier;
using Retalix.StoreServices.BusinessServices.FrontEnd.SystemParameters;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tabs;
using Retalix.StoreServices.BusinessServices.FrontEnd.Taxes.ApplyEatIn;
using Retalix.StoreServices.BusinessServices.FrontEnd.Taxes.GetAllExemptableTaxes;
using Retalix.StoreServices.BusinessServices.FrontEnd.Taxes.GetReversalTaxes;
using Retalix.StoreServices.BusinessServices.FrontEnd.TaxExemptionStrategy;
using Retalix.StoreServices.BusinessServices.FrontEnd.TdmConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.DepositedCash;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.GetValidTenders;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.PreTenderAdd;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderAdd;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderAuthorizationConfirmation;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderCancel;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderDefault;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderExchange;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderExchange.Lookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderExchange.SaveOrUpdate;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderRoundingRule;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderRoundingRule.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderType;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderType.Maintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tenders.TenderValidatorLookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.ThirdParty;
using Retalix.StoreServices.BusinessServices.FrontEnd.TicketTotal;
using Retalix.StoreServices.BusinessServices.FrontEnd.TimeAvailabilityTerm;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tips;
using Retalix.StoreServices.BusinessServices.FrontEnd.Tips.TipTransactionLog;
using Retalix.StoreServices.BusinessServices.FrontEnd.TLogThirdPartyIntegration;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.Branding.FileSizeConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.Branding.Images.Lookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.Branding.Images.Maintenance;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.Branding.Images.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.Commands;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.CustomerScreenConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.DataMapping;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.DisplayAddAddLoyaltyConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.KeyboardConfigurations;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.SecurityScaleConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.SecurityScaleConfiguration.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.VirtualKeyboardConfigurations;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.VirtualKeyboardConfigurations.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.VirtualKeyboardConfigurations.Validation;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.AutoVoidAbandonedTransaction;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.AutoVoidAbandonedTransaction.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.Clasification;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.MergeTransaction;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.ResumeTransaction;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.ReturnAllTransaction;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.RTSTransactionStatus;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.SalesTransactionBegin;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.SuspendTransaction;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.Tabs.ResumeTab;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.Tabs.SuspendTab;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.TransactionFinishedWrapper;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.TransactionLog;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.TransactionLookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.TransactionRecovery;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.TransactionStatus;
using Retalix.StoreServices.BusinessServices.FrontEnd.Transaction.VoidTransaction;
using Retalix.StoreServices.BusinessServices.FrontEnd.Upc5FaceValueLookup;
using Retalix.StoreServices.BusinessServices.FrontEnd.Upc5FaceValueModify;
using Retalix.StoreServices.BusinessServices.FrontEnd.XReport;
using Retalix.StoreServices.BusinessServices.IDM;
using Retalix.StoreServices.BusinessServices.IDM.ActivityStatus;
using Retalix.StoreServices.BusinessServices.IDM.ConfigurationServices;
using Retalix.StoreServices.BusinessServices.IDM.ForcedSignOff;
using Retalix.StoreServices.BusinessServices.IDM.Login;
using Retalix.StoreServices.BusinessServices.IDM.ResetUserPassword;
using Retalix.StoreServices.BusinessServices.IDM.RoleServices;
using Retalix.StoreServices.BusinessServices.IDM.UserMaintenance;
using Retalix.StoreServices.BusinessServices.Maintenance.BottleDeposit.Lookup;
using Retalix.StoreServices.BusinessServices.Maintenance.BottleDeposit.Maintenance;
using Retalix.StoreServices.BusinessServices.Maintenance.BusinessRules;
using Retalix.StoreServices.BusinessServices.Maintenance.BusinessRules.Lookup;
using Retalix.StoreServices.BusinessServices.Maintenance.Certificate;
using Retalix.StoreServices.BusinessServices.Maintenance.Consumable.Lookup;
using Retalix.StoreServices.BusinessServices.Maintenance.Consumable.Maintenance;
using Retalix.StoreServices.BusinessServices.Maintenance.ConsumableAssociationGroup.Lookup;
using Retalix.StoreServices.BusinessServices.Maintenance.ConsumableAssociationGroup.Maintenance;
using Retalix.StoreServices.BusinessServices.Maintenance.ConsumableGroup.ItemInConsumableGroupLookup;
using Retalix.StoreServices.BusinessServices.Maintenance.ConsumableGroup.Lookup;
using Retalix.StoreServices.BusinessServices.Maintenance.ConsumableGroup.Maintenance;
using Retalix.StoreServices.BusinessServices.Maintenance.DataIntegrity;
using Retalix.StoreServices.BusinessServices.Maintenance.DisplayReward;
using Retalix.StoreServices.BusinessServices.Maintenance.DMS;
using Retalix.StoreServices.BusinessServices.Maintenance.EOD;
using Retalix.StoreServices.BusinessServices.Maintenance.ForeignCurrencyExchangeRates;
using Retalix.StoreServices.BusinessServices.Maintenance.Indicator;
using Retalix.StoreServices.BusinessServices.Maintenance.ItemPicker;
using Retalix.StoreServices.BusinessServices.Maintenance.Kit;
using Retalix.StoreServices.BusinessServices.Maintenance.LinkGroup;
using Retalix.StoreServices.BusinessServices.Maintenance.LocationConsumableGroup.Lookup;
using Retalix.StoreServices.BusinessServices.Maintenance.LocationConsumableGroup.Lookup.Maintenance;
using Retalix.StoreServices.BusinessServices.Maintenance.MemberAccountsToDisplay;
using Retalix.StoreServices.BusinessServices.Maintenance.Notification.Confirm;
using Retalix.StoreServices.BusinessServices.Maintenance.Notification.Lookup;
using Retalix.StoreServices.BusinessServices.Maintenance.Notification.Maintenance;
using Retalix.StoreServices.BusinessServices.Maintenance.Notification.Retrieve;
using Retalix.StoreServices.BusinessServices.Maintenance.Price.Lookup;
using Retalix.StoreServices.BusinessServices.Maintenance.Price.Maintenance;
using Retalix.StoreServices.BusinessServices.Maintenance.Price.PriceChangeStatusUpdate;
using Retalix.StoreServices.BusinessServices.Maintenance.Product.Lookup;
using Retalix.StoreServices.BusinessServices.Maintenance.Product.Lookup.DataExtractor;
using Retalix.StoreServices.BusinessServices.Maintenance.Product.Maintenance;
using Retalix.StoreServices.BusinessServices.Maintenance.Product.Maintenance.Parts;
using Retalix.StoreServices.BusinessServices.Maintenance.Product.Validity;
using Retalix.StoreServices.BusinessServices.Maintenance.Receipt.Lookup;
using Retalix.StoreServices.BusinessServices.Maintenance.Receipt.Maintenance;
using Retalix.StoreServices.BusinessServices.Maintenance.Receipt.Mapping;
using Retalix.StoreServices.BusinessServices.Maintenance.Receipt.Services;
using Retalix.StoreServices.BusinessServices.Maintenance.ScoIntervention.Lookup;
using Retalix.StoreServices.BusinessServices.Maintenance.ScoIntervention.Maintenance;
using Retalix.StoreServices.BusinessServices.Maintenance.StoreRange;
using Retalix.StoreServices.BusinessServices.Maintenance.TdmDatabase;
using Retalix.StoreServices.Common.DMS;
using Retalix.StoreServices.Connectivity.CDM;
using Retalix.StoreServices.Connectivity.Organization.RetailSegment.Query;
using Retalix.StoreServices.Connectivity.Transaction.ScoIntervention.PosEvent.DataObjects;
using Retalix.StoreServices.Model.Document;
using Retalix.StoreServices.Model.Finance.Account;
using Retalix.StoreServices.Model.Finance.Money;
using Retalix.StoreServices.Model.Finance.Period;
using Retalix.StoreServices.Model.Finance.XZReports;
using Retalix.StoreServices.Model.Infrastructure;
using Retalix.StoreServices.Model.Infrastructure.BPM;
using Retalix.StoreServices.Model.Infrastructure.DataExtractor;
using Retalix.StoreServices.Model.Infrastructure.Legacy.Globalization;
using Retalix.StoreServices.Model.Infrastructure.RtiTracking;
using Retalix.StoreServices.Model.Infrastructure.Security.Identity;
using Retalix.StoreServices.Model.Infrastructure.Service;
using Retalix.StoreServices.Model.Infrastructure.StoreApplication;
using Retalix.StoreServices.Model.MemberAccountsToDisplay;
using Retalix.StoreServices.Model.Organization.Alert;
using Retalix.StoreServices.Model.Organization.Servers;
using Retalix.StoreServices.Model.Organization.TouchPoint.VirtualKeyboardConfigurations;
using Retalix.StoreServices.Model.Receipts.DigitalReceipt;
using Retalix.StoreServices.Model.RetransmitTranscation;
using Retalix.StoreServices.Model.Selling;
using Retalix.StoreServices.Model.Selling.GiftReceipt;
using Retalix.StoreServices.Model.Selling.OrderProcess;
using Retalix.StoreServices.Model.Selling.ScoIntervention;
using Retalix.StoreServices.Model.TouchPointApplication.Command;
using Retalix.StoreServices.Model.TouchPointApplication.KeyboardConfigurations;
using System.Configuration;
using Retalix.Contracts.Generated.Arts.PosLogV6.Source;
using Retalix.Contracts.Generated.Kit;
using Retalix.StoreServices.BusinessServices.Maintenance.MemberAccountsToDisplay.ConfigurationMaintenance;
using Retalix.StoreServices.Model.Organization.TouchPoint.DynamicFormConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.DynamicFormConfiguration.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.DynamicFormConfiguration.Validation;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.DynamicFormConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.PosParameterConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.PosParameterConfiguration.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.PosDepositBusinessConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.TouchPoint.PosDepositBusinessConfiguration.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LinkedItemCancel;
using Retalix.StoreServices.BusinessServices.FrontEnd.Masking;
using Retalix.StoreServices.BusinessServices.FrontEnd.TransactionGapFillerConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.TransactionGapFiller.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.ExternalServices;
using Retalix.StoreServices.BusinessServices.FrontEnd.CodeSignatureVerification;
using Retalix.StoreServices.BusinessServices.Maintenance.DetailedOrderCode;
using Retalix.StoreServices.BusinessServices.Maintenance.DetailedOrderCode.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemAddBulk.Strategy;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.LineItemAddBulk.Handlers.LineItemCreators;
using Retalix.StoreServices.Model.LineItem.LineItemAddBulk;
using Retalix.StoreServices.Model.LineItem.LineItemAddBulk.Handlers.LineItemCreators;
using Retalix.StoreServices.Model.LineItem.LineItemAddBulk.Strategy;
using Retalix.StoreServices.Model.RefundVoucher;
using Retalix.StoreServices.BusinessComponents.RefundVoucher;
using Retalix.StoreServices.BusinessServices.FrontEnd.BusinessConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.RefundVoucher;
using Retalix.StoreServices.BusinessServices.FrontEnd.Taxes.RecalculateReturnTax;
using Retalix.StoreServices.BusinessServices.FrontEnd.Configuration.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.Fiscal.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.Fiscal;
using Retalix.StoreServices.BusinessServices.FrontEnd.EpsReconcile;
using Retalix.StoreServices.BusinessServices.FrontEnd.EnableDevicesApiConfiguration.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.EnableDevicesApiConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.LineItem.DuplicateItem;
using Retalix.StoreServices.Model.Selling.DuplicateItem;
using Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.Configuration.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.DelayedIntervention;
using Retalix.StoreServices.BusinessServices.FrontEnd.Employee;
using Retalix.StoreServices.BusinessServices.FrontEnd.PrintPriceQueries;
using Retalix.StoreServices.BusinessServices.FrontEnd.OnlineItem.HistoryInquiry;
using Retalix.StoreServices.BusinessServices.FrontEnd.EpsConfiguration.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.EpsConfiguration;
using Retalix.StoreServices.BusinessServices.FrontEnd.Kit.Calorie.DataModel;
using Retalix.StoreServices.BusinessServices.FrontEnd.Kit.Calorie.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.RedisCacheTrim;
using Retalix.StoreServices.BusinessServices.FrontEnd.RedisCacheTrim.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.Promotion.SecondPE.Mappers;
using Retalix.StoreServices.Connectivity.Selling;
using Retalix.StoreServices.Model.Selling.RetailTransaction;
using Retalix.StoreServices.Model.Product.Calorie;
using Retalix.StoreServices.Model.Product.Calorie.DataModel;
using Retalix.StoreServices.Model.PayByPoints.Strategy;
using Retalix.StoreServices.BusinessServices.FrontEnd.PayByPoints.Strategy;
using Retalix.StoreServices.BusinessServices.FrontEnd.PayByPoints;
using Retalix.StoreServices.BusinessServices.FrontEnd.CustomerOrders.Salesperson;
using Retalix.StoreServices.BusinessServices.FrontEnd.Loyalty.BalanceInquiry;
using Retalix.StoreServices.BusinessServices.FrontEnd.MultiCustomerBehavior;
using Retalix.StoreServices.BusinessServices.FrontEnd.Customer.MultiCustomerBehavior.Mappers;
using Retalix.StoreServices.BusinessServices.FrontEnd.BusinessRoles;

namespace Retalix.StoreServer.CoreConfiguration.CoreComponents 
{
    internal class BusinessServices : CastleConfigurationInstaller
    {
        public override void Install(IComponentInstaller builder)
        {
            BusinessServicesFuel.Register(builder);

            builder.Register(Component.For<IBusinessService>().Named("BusinessRolesMaintainanceService").ImplementedBy<BusinessRolesMaintainanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("BusinessRolesLookUpService").ImplementedBy<BusinessRolesLookUpService>().LifeStyle.Transient);
            //builder.Register(Component.For<IBusinessRolesDao>().Named("IBusinessRolesDao").ImplementedBy<BusinessRolesDao>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessRoles>().Named("BusinessRoles").ImplementedBy<BusinessRoles>().LifeStyle.Transient);

            builder.Register(Component.For<IRtiEventsHistory>().Named("RtiEventsHistory")
                            .ImplementedBy<RtiEventsHistory>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BatchDataCleanupService").ImplementedBy
                      <BatchDataCleanupService>().LifeStyle.Transient);

            builder.RegisterService<SetTrainingModeService>();

            builder.Register(Component.For<IBusinessService>().Named("IdmLoginService").ImplementedBy<IdmLoginService>().LifeStyle.
                    Transient);

            builder.Register(Component.For<IBusinessService>().Named("ActivityStatusService")
                         .ImplementedBy<ActivityStatusService>().LifeStyle.Transient);

            builder.RegisterService<ReceiptConfigurationParametersLookupService>();
            builder.RegisterMapper<ReceiptConfigurationParameterMapper>();

            builder.Register(Component.For<IBusinessService>().Named("ReceiptConfigurationParametersMaintenanceService")
                .ImplementedBy<ReceiptConfigurationParametersMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmLogoffService").ImplementedBy<IdmLogoffService>().LifeStyle.
                      Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmTrainingModeOffService").ImplementedBy
                      <IdmTrainingModeOffService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmResetAllPasswordsService").ImplementedBy
                      <IdmResetAllPasswordsService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmLockUserService").ImplementedBy
                      <IdmLockUserService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("EligibilityPolicyMaintenanceService").ImplementedBy
                      <EligibilityPolicyMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ProductPaymentRestrictionMaintenanceService").ImplementedBy
                      <ProductPaymentRestrictionMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ProductPaymentRestrictionLookupService").ImplementedBy
                     <ProductPaymentRestrictionLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MovableEntityConfigurationMaintenanceService").ImplementedBy
                    <MovableEntityConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MovableRoutingMethodMaintenanceService").ImplementedBy
                    <MovableEntityConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MovableRoutingMethodLookupService").ImplementedBy
                    <MovableEntityConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MovableEntityConfigurationLookupService").ImplementedBy
                    <MovableEntityConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IScoHardwareIntervention>().Named("ScoHardwareIntervention").ImplementedBy
                    <ScoHardwareIntervention>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("RetailTransactionLogDirectInsertService").ImplementedBy
                    <RetailTransactionLogDirectInsertService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("EligibilityPolicyLookupService").ImplementedBy
                     <EligibilityPolicyLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmGetUserByUsernameService").ImplementedBy
                      <IdmGetUserByUsernameService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmUpdateUserService").ImplementedBy<IdmUpdateUserService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmAddUserService").ImplementedBy<IdmAddUserService>().
                      LifeStyle.Transient);

            builder.RegisterService<UserMaintenanceService>();

            builder.RegisterService<Gs1ApplicationIdentifierMaintenanceService>();

            builder.RegisterService<Gs1ApplicationIdentifierLookupService>();

            builder.RegisterService<AccountBalanceSummaryLookupService>();

            builder.RegisterService<IdmUserBulkMaintenanceService>();

            builder.Register(Component.For<IBusinessService>().Named("ShiftStartService").ImplementedBy<ShiftStartService>().
LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ShiftEndService").ImplementedBy<ShiftEndService>().
      LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("IdmChangePasswordService").ImplementedBy
                      <IdmChangePasswordService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmRetrieveUsersService").ImplementedBy
                      <IdmRetrieveUsersService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmGetRoleService").ImplementedBy<IdmGetRoleService>().
                       LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmUpdateRoleService").ImplementedBy<IdmUpdateRoleService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmAddRoleService").ImplementedBy<IdmAddRoleService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmFindRoleActionsService").ImplementedBy
                      <IdmFindRoleActionsService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmFindRolesService").ImplementedBy<IdmFindRolesService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmDeleteRoleService")
                         .ImplementedBy<IdmDeleteRoleService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmGetIdmConfigurationService")
                         .ImplementedBy<IdmGetIdmConfigurationService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmSetIdmConfigurationService")
                         .ImplementedBy<IdmSetIdmConfigurationService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SetDmsConfigurationService").ImplementedBy
                    <SetDmsConfigurationService>().LifeStyle.Transient);

            builder.RegisterService<SkipDownloadStuckTokenService>();

            builder.Register(Component.For<TransportFailureDownloadMaintenanceService>().Named("TransportFailureDownloadMaintenanceService").ImplementedBy
                    <TransportFailureDownloadMaintenanceService>().LifeStyle.Singleton);

            builder.Register(Component.For<IBusinessService>().Named("CheckServerLogicalNameUniquenessService").ImplementedBy
                    <CheckServerLogicalNameUniquenessService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("RemoveChildService")
                                        .ImplementedBy<RemoveChildService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetDmsConfigurationService").ImplementedBy
                    <GetDmsConfigurationService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetDmsMessageDataService").ImplementedBy
                    <GetDmsMessageDataService>().LifeStyle.Transient);

            builder.Register(Component.For<GetFailedDmsTokenDataService>().Named("GetFailedDmsTokenDataService").ImplementedBy
                    <GetFailedDmsTokenDataService>().LifeStyle.Transient);

            builder.Register(Component.For<FailedTokenMoveToInboxService>().Named("FailedTokenMoveToInboxService").ImplementedBy
                    <FailedTokenMoveToInboxService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("HeartbeatMonitorService").ImplementedBy
                    <HeartbeatMonitorService>().LifeStyle.Singleton);

            builder.Register(Component.For<IBusinessService>().Named("MarkServerNotOutdateService").ImplementedBy
                    <MarkServerNotOutdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("RefreshChildrenOnlineStatusService").ImplementedBy
                    <RefreshChildrenOnlineStatusService>().LifeStyle.Singleton);

            builder.Register(Component.For<IBusinessService>().Named("GetServerInfoService").ImplementedBy
                    <GetServerInfoService>().LifeStyle.Singleton);

            builder.Register(Component.For<IBusinessService>().Named("GetLastProviderInfoService").ImplementedBy
                    <GetLastProviderInfoService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SetLastProviderInfoService").ImplementedBy
                    <SetLastProviderInfoService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetServerStatusInfoService").ImplementedBy
                    <GetServerStatusInfoService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SetEventDataService").ImplementedBy
                    <SetEventDataService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BackupDatabaseService").ImplementedBy
                    <BackupDatabaseService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("NonResettableDataLookupService").ImplementedBy
                    <NonResettableDataLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CreateColdStartRecoveryBackupService").ImplementedBy
                    <CreateColdStartRecoveryBackupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ColdstartRegistrationService").ImplementedBy
                      <ColdstartRegistrationService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ColdstartStatusLookupService").ImplementedBy
                     <ColdstartStatusLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("NonResettableDataSaveOrUpdateService").ImplementedBy
                    <NonResettableDataSaveOrUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("InitializeServerNodeService").ImplementedBy
                    <InitializeServerNodeService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetMonitoringInfoService").ImplementedBy
                    <GetMonitoringInfoService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SetMonitoringInfoService").ImplementedBy
                    <SetMonitoringInfoService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LoanAddService").ImplementedBy<LoanAddService>().LifeStyle.
                      Transient);

            builder.Register(Component.For<IBusinessService>().Named("PayInService").ImplementedBy<PayInService>().LifeStyle.
                      Transient);

            builder.Register(Component.For<IBusinessService>().Named("PayOutService").ImplementedBy<PayOutService>().LifeStyle.
                      Transient);

            builder.Register(Component.For<IBusinessService>().Named("DeclarationService").ImplementedBy<DeclarationService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TillClosingService").ImplementedBy<TillClosingService>()
                      .LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PickupService").ImplementedBy<PickupService>().LifeStyle.
                      Transient);

            builder.Register(Component.For<IBusinessService>().Named("BankDepositService").ImplementedBy<BankDepositService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BankReceiptService").ImplementedBy<BankReceiptService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BalancePeriodSchedulingConfigurationDeleteService")
                         .ImplementedBy<BalancePeriodSchedulingConfigurationDeleteService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BalancePeriodSchedulingConfigurationLookupService")
                         .ImplementedBy<BalancePeriodSchedulingConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BalancePeriodSchedulingConfigurationSaveOrUpdateService").
                          ImplementedBy<BalancePeriodSchedulingConfigurationSaveOrUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OpeningFundsConfigurationLookupService").ImplementedBy
                      <OpeningFundsConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OpeningFundsConfigurationDeleteService").ImplementedBy
                    <OpeningFundsConfigurationDeleteService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DrawerCashOfficeConfigurationMaintenanceService")
                                      .ImplementedBy<DrawerCashOfficeConfigurationMaintenanceService>()
                                      .LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CashierBalancePeriodSaveOrUpdateService")
                .ImplementedBy<CashierBalancePeriodSaveOrUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DrawerBalancePeriodSaveOrUpdateService")
                .ImplementedBy<DrawerBalancePeriodSaveOrUpdateService>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("DrawerCashOfficeConfigurationLookupService")
                                 .ImplementedBy<DrawerCashOfficeConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TenderMaintenanceService").ImplementedBy
                    <TenderTypeMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TenderLookupService").ImplementedBy<TenderLookupService>().
                    LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TenderDefaultLookupService")
                         .ImplementedBy<TenderDefaultLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TenderTypeLookupService").ImplementedBy
                    <TenderTypeLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SellableTenderLookupService").ImplementedBy
                    <SellableTenderLookupExtService>().
                    LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ItemLookupService").ImplementedBy<ItemLookupService>().
                    LifeStyle.Transient);


            builder.Register(Component.For<IExtensionPartOfProductMaintenanceService>().Named("PromotionPartOfProductMaintenanceService")
                         .ImplementedBy<ManufacturerPartOfProductMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LineItemAddService").ImplementedBy<LineItemAddService>().
                    LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LineItemAddBulkService").ImplementedBy<LineItemAddBulkService>().
                    LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LineItemAddBulkWrapperService").ImplementedBy<LineItemAddBulkWrapperService>().
                    LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LineItemPriceModifierService").ImplementedBy
                    <LineItemPriceModifierService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataAddService").ImplementedBy<DataAddService>().LifeStyle.
                      Singleton);

            builder.Register(Component.For<IBusinessService>().Named("SalesTransactionBeginService").ImplementedBy
                      <SalesTransactionBeginService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TransactionFinishedWrapperService").ImplementedBy
                      <TransactionFinishedWrapperService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetPaybackSellableTendersService")
                         .ImplementedBy<GetPaybackSellableTendersService>().LifeStyle.Transient);

            builder.Register(
                 Component.For<IBusinessService>().Named("ApplyTaxExemptionService").ImplementedBy
                      <ApplyTaxExemptionService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LineItemReturnService").ImplementedBy<LineItemReturnService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ValidateLineItemReturnService")
                         .ImplementedBy<ValidateLineItemReturnService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named(ProductLookupServiceDispatcher.ServicePublicKey).ImplementedBy
                    <ProductLookupServiceDispatcher>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named(ProductLookupServiceDispatcher.ServiceV2Key).ImplementedBy
                    <ProductLookupServiceV200>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ProductLookupByPolicyService")
                         .ImplementedBy<ProductLookupByPolicyService>().LifeStyle.Transient);

            #region ProductMaintenanceService dispatching
            builder.RegisterService<ProductMaintenanceService>();
            #endregion

            builder.Register(Component.For<IProductMaintenanceContractValidation>().Named("LinkGroupIdCoreContractValidation").ImplementedBy
                      <LinkGroupIdCoreContractValidation>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ConsumableLookupService").ImplementedBy
                      <ConsumableLookupService>().LifeStyle.Transient);

            builder.RegisterService<ConsumableMaintenanceService>();

            builder.RegisterService<ItemHierarchyMaintenanceService>();

            builder.RegisterService<BusinessUnitMaintenanceService>();

            builder.Register(Component.For<IBusinessService>().Named("ItemHierarchyLookupService").ImplementedBy
                      <ItemHierarchyLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LocationConsumableGroupLookupService").ImplementedBy
                      <LocationConsumableGroupLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LocationConsumableGroupMaintenanceService").ImplementedBy
                      <LocationConsumableGroupMaintenanceService>().LifeStyle.Transient);

            builder.RegisterService<StoreServices.BusinessServices.Maintenance.Price.Lookup.V1.PriceLookupService>();

            builder.RegisterService<StoreServices.BusinessServices.Maintenance.Price.Lookup.V2.PriceLookupService>();

            builder.RegisterService<StoreServices.BusinessServices.Maintenance.Price.Lookup.V2.PriceChangeLookupService>();

            builder.RegisterService<StoreServices.BusinessServices.Maintenance.Price.Lookup.V2.PriceReasonLookupService>();

            builder.Register(Component.For<IBusinessService>().Named("UnitOfMeasureLookupService").ImplementedBy
                      <UnitOfMeasureLookupService>().LifeStyle.Transient);

            builder.RegisterService<StoreServices.BusinessServices.Maintenance.Price.Maintenance.v1_0_0.PriceMaintenanceService>();

            builder.RegisterService<StoreServices.BusinessServices.Maintenance.Price.Maintenance.v2_0_0.PriceMaintenanceService>();

            builder.RegisterService<StoreServices.BusinessServices.Maintenance.Price.Maintenance.v2_0_0.PriceReasonMaintenanceService>();

            builder.RegisterService<ConsumablePriceBulkMaintenanceService>();

            builder.Register(Component.For<IBusinessService>().Named("PriceMaintenanceRemotableService").ImplementedBy<PriceMaintenanceRemotableService>().LifeStyle.Transient);

            builder.Register(Component.For<IPriceMaintenance>().Named("IPriceMaintenance").ImplementedBy<PriceMaintenanceHelper>().LifeStyle.Transient);

            builder.Register(Component.For<IRefundVoucherChange>().Named("RefundVoucherChange").ImplementedBy<RefundVoucherChange>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ProductLinkGroupMaintenanceService").ImplementedBy
                      <ProductLinkGroupMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ProductLinkGroupLookupService").ImplementedBy
                      <ProductLinkGroupLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ConsumableAssociationGroupMaintenanceService").ImplementedBy
                      <ConsumableAssociationGroupMaintenanceService>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("ConsumableAssociationGroupLookupService").ImplementedBy
                      <ConsumableAssociationGroupLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ConsumableGroupLookupService").ImplementedBy
                      <ConsumableGroupLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerDetailsSaveService").ImplementedBy
                      <CustomerDetailsSaveService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("KitConfigurationMaintenanceService").ImplementedBy
                    <KitConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("KitConfigurationLookupService").ImplementedBy
                    <KitConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ConsumableGroupMaintenanceService").ImplementedBy
                      <ConsumableGroupMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ItemInConsumableGroupLookupService").ImplementedBy
                      <ItemInConsumableGroupLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LineItemCancelService").ImplementedBy<LineItemCancelService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("VoidLinkedItemService").ImplementedBy<VoidLinkedItemService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LineItemInstructionService").ImplementedBy<LineItemInstructionService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReprintTransactionService").ImplementedBy
                      <ReprintTransactionService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LineTenderCancelService").ImplementedBy
                      <LineTenderCancelService>().LifeStyle.Transient);

            builder.Register(Component.For<ReportDeviceErrorService>().Named("ReportDeviceErrorService").ImplementedBy
                    <ReportDeviceErrorService>().LifeStyle.Transient);

            builder.Register(Component.For<ReportDeviceErrorSolvedService>().Named("ReportDeviceErrorSolvedService").ImplementedBy
                    <ReportDeviceErrorSolvedService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DeviceConfigurationAddService").ImplementedBy
                      <DeviceConfigurationAddService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DeviceConfigurationDeleteService").ImplementedBy
                    <DeviceConfigurationDeleteService>().LifeStyle.Transient);

            builder.Register(Component.For<DeviceTypeMaintenanceService>().Named("DeviceTypeMaintenanceService").ImplementedBy
                      <DeviceTypeMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<DeviceTypeLookupService>().Named("DeviceTypeLookupService").ImplementedBy
                      <DeviceTypeLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReasonCodeMaintenanceService").ImplementedBy
                      <ReasonCodeMaintenanceNewService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReasonCodeLookupService").ImplementedBy
                      <ReasonCodeLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReturnReasonCodeLookupService").ImplementedBy
                      <ReturnReasonCodeLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DeviceConfigurationUpdateService").ImplementedBy
                      <DeviceConfigurationUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DeviceConfigurationFindService").ImplementedBy
                      <DeviceConfigurationFindService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointAddService").ImplementedBy<TouchPointAddService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointGroupParametersLookupService")
                         .ImplementedBy<TouchPointGroupParameterLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointGroupParametersMaintenanceService")
                         .ImplementedBy<TouchPointGroupParameterMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointUpdateService").ImplementedBy
                      <TouchPointUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointFindService").ImplementedBy<TouchPointFindService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointGetByComputerNameService").ImplementedBy
                      <TouchPointGetByComputerNameService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointGroupAddService").ImplementedBy
                      <TouchPointGroupAddService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointGroupUpdateService").ImplementedBy
                      <TouchPointGroupUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointGroupFindService").ImplementedBy
                      <TouchPointGroupFindService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointCommandsMaintenanceService").ImplementedBy
                <TouchPointCommandsMaintenanceService>().LifeStyle.Transient);

            builder.RegisterSingleton<ITouchPointCommandFactory, TouchPointCommandFactory>();


            builder.Register(Component.For<IBusinessService>().Named("TouchPointNotificationsGetService")
                .ImplementedBy<TouchPointNotificationsGetService>().LifeStyle.Transient);

            builder.Register(Component.For<IRemoteableTouchPointRegistration>().Named("IRemoteableTouchPointRegistration")
                .ImplementedBy<RemoteableTouchPointRegistration>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataPatternAddService").ImplementedBy<DataPatternAddService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataPatternDeleteService").ImplementedBy
                      <DataPatternDeleteService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataPatternUpdateService").ImplementedBy
                      <DataPatternUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataPatternFindService")
                         .ImplementedBy<DataPatternFindService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataPatternEncodeService").ImplementedBy
                      <DataPatternEncodeService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TenderAddService").ImplementedBy<TenderAddService>().LifeStyle.
                      Transient);

            builder.Register(Component.For<IBusinessService>().Named("TenderAuthorizationConfirmationService")
                         .ImplementedBy<TenderAuthorizationConfirmationService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TenderExchangeService").ImplementedBy<TenderExchangeService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CouponAddService").ImplementedBy<CouponAddService>().LifeStyle.
                      Transient);

            builder.Register(Component.For<IBusinessService>().Named("CouponCancelService").ImplementedBy<CouponCancelService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IRtsRetailTransaction>().Named("RtsRetailTransaction")
                         .ImplementedBy<RtsRetailTransaction>().LifeStyle.Transient);

            builder.Register(Component.For<IRtiRetailTransaction>().Named("RtiRetailTransaction")
                         .ImplementedBy<RtsRetailTransaction>().LifeStyle.Transient);

            builder.Register(Component.For<IDigitalReceiptModeStrategy>().Named("DigitalReceiptModeStrategy")
                         .ImplementedBy<DigitalReceiptModeStrategy>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerAddService").ImplementedBy<CustomerAddService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LoyaltyBalanceInquiryService").ImplementedBy<LoyaltyBalanceInquiryService>().
                      LifeStyle.Transient);

            builder.RegisterService<PromotionMaintenanceService>();
            builder.RegisterService<StoreServices.BusinessServices.FrontEnd.Promotion.BackwardCompatibility.PromotionMaintenanceService>();

            builder.Register(Component.For<IPromotionMaintenanceObserver>().Named("PromotionAccountTypesMaintenance").ImplementedBy
        <PromotionAccountTypesMaintenance>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IncentiveMessageRuleMaintenanceService").ImplementedBy
                    <IncentiveMessageRuleMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PromotionGroupMaintenanceService").ImplementedBy
            <PromotionGroupMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PromotionGroupLookUpService").ImplementedBy
            <PromotionGroupLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IncentiveMessageRuleLookUpService").ImplementedBy
                    <IncentiveMessageRuleLookUpService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetPromotionByIdService").ImplementedBy
                      <GetPromotionByIdService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TicketRewardsCalculateService").ImplementedBy
                     <TicketRewardsCalculateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ManualPromotionAddService").ImplementedBy
                                  <ManualPromotionAddService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ManualPromotionCancelService").ImplementedBy
                                  <ManualPromotionCancelService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("UpdatePromotionApprovalService").ImplementedBy
                      <UpdatePromotionApprovalService>().LifeStyle.Transient);

            builder.Register(Component.For<AdvertisementMaintenanceService>().Named("AdvertisementMaintenanceService").ImplementedBy
              <AdvertisementMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<AdvertisementPromotionLookUpService>().Named("AdvertisementPromotionLookUpService").ImplementedBy
          <AdvertisementPromotionLookUpService>().LifeStyle.Transient);

            builder.Register(Component.For<AdvertisementCategoryMaintenanceService>().Named("AdvertisementCategoryMaintenanceService").ImplementedBy
          <AdvertisementCategoryMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<AdvertisementChannelMaintenanceService>().Named("AdvertisementChannelMaintenanceService").ImplementedBy
        <AdvertisementChannelMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<GetAdvertisementChannelsService>().Named("GetAdvertisementChannelsService").ImplementedBy
        <GetAdvertisementChannelsService>().LifeStyle.Transient);

            builder.Register(Component.For<GetAdvertisementCategoriesService>().Named("GetAdvertisementCategoriesService").ImplementedBy
        <GetAdvertisementCategoriesService>().LifeStyle.Transient);

            builder.Register(Component.For<AdvertisementCategoryLookUpService>().Named("AdvertisementCategoryLookUpService").ImplementedBy
        <AdvertisementCategoryLookUpService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BusinessUnitLookupService")
                         .ImplementedBy<BusinessUnitLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BusinessUnitInfoLookupService")
                         .ImplementedBy<BusinessUnitInfoLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OfferCategoryForViewLookupService").ImplementedBy
                    <OfferCategoryForViewLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OffersByCategoryLookupService").ImplementedBy
                    <OffersByCategoryLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BusinessUnitImageMaintenanceService")
                         .ImplementedBy<BusinessUnitImageMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BusinessUnitImageLookupService")
                         .ImplementedBy<BusinessUnitImageLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("VoidTransactionService")
                         .ImplementedBy<VoidTransactionService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SuspendTransactionService").ImplementedBy
                      <SuspendTransactionService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SuspendTabService").ImplementedBy
                      <SuspendTabService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MoveTabToVenueService").ImplementedBy
                      <MoveTabToVenueService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ResumeTransactionService").ImplementedBy
                      <ResumeTransactionService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ResumeTabService").ImplementedBy
                      <ResumeTabService>().LifeStyle.Transient);

            builder.Register(Component.For<IResumedOrderConsistencyValidator>().Named("ResumedOrderStoreValidator").ImplementedBy
                      <ResumedOrderStoreValidator>().LifeStyle.Transient);

            builder.Register(Component.For<IMergeTransactionValidator>().Named("MergeTransactionValidator").ImplementedBy
                      <MergeTransactionValidator>().LifeStyle.Transient);
            builder.Register(Component.For<IMergeTransactionValidator>().Named("OrderModeMergedTransactionValidator").ImplementedBy
                      <OrderModeMergedTransactionValidator>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TransactionLookupService").ImplementedBy
                      <TransactionLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TransactionListingService").ImplementedBy
                      <TransactionListingService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TransactionStatusService").ImplementedBy
                      <TransactionStatusService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ResubmitApprovalRedemptionService").ImplementedBy
                      <ResubmitApprovalRedemptionService>().LifeStyle.Transient);

            builder.RegisterService<TransactionRecoveryService>();


            builder.Register(Component.For<IBusinessService>().Named("DepartmentLookupService").ImplementedBy
                      <DepartmentLookupService>().LifeStyle.Transient);

            builder.RegisterService<DepartmentSaveOrUpdateService>();

            builder.Register(Component.For<IBusinessService>().Named("WarmUpWrapperService").ImplementedBy<WarmUpWrapperService>().
                      LifeStyle.Transient);

            ConfigureRetailTransactionLogServices(builder);

            builder.Register(Component.For<IBusinessService>().Named("LogDocumentLookupService").ImplementedBy
                      <LogDocumentLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DocumentLookupService").ImplementedBy
                    <DocumentLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DrawerOpenService").ImplementedBy
                      <StoreServices.BusinessServices.FrontEnd.DrawerOpen.DrawerOpenService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ColdstartActivityService").ImplementedBy
                      <StoreServices.BusinessServices.FrontEnd.Coldstart.ColdstartActivityService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("EndOfDayLookupService").ImplementedBy<EndOfDayLookupService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("EndOfDayMaintenanceService").ImplementedBy
                      <EndOfDayMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("EndOfDayStatusService").ImplementedBy<EndOfDayStatusService>().
                      LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("EpsReconcileService")
                .ImplementedBy<EpsReconcileService>().LifeStyle.Transient);


            builder.Register(Component.For<IEndOfDayProcessObserver>().Named("EndOfDayShiftClosing").ImplementedBy<EndOfDayShiftClosing>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("EndOfDayExecuteService")
                         .ImplementedBy<EndOfDayExecuteService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PreTenderAddService").ImplementedBy<PreTenderAddService>().
                      LifeStyle.Transient);

            builder.RegisterMapper<AuthorizationExtendedDataToPaymentInfoMapper>();
            builder.RegisterMapper<PaymentInfoToAuthorizationExtendedDataMapper>();
            builder.RegisterMapper<ConnectedPaymentToPaymentInfoExtendedDataMapper>();

            builder.RegisterMapper<ThirdPartySessionIdToPaymentInfoMapper>();
            builder.RegisterMapper<PaymentInfoToThirdPartySessionIdMapper>();

            builder.Register(Component.For<IBusinessService>().Named("PreTenderUpdateService").ImplementedBy<PreTenderUpdateService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetValidSellableTendersService")
                         .ImplementedBy<GetValidSellableTendersService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TenderSuggestedAmountsLookupService")
                    .ImplementedBy<TenderSuggestedAmountsLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetAllExemptableTaxesService").ImplementedBy
                      <GetAllExemptableTaxesService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TaxExemptionStrategyService").ImplementedBy
                      <TaxExemptionStrategyService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ApplyTaxDiscountService").ImplementedBy
                      <ApplyTaxDiscountService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ApplyEatInService").ImplementedBy<ApplyEatInService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ApplyEmployeeSaleService").ImplementedBy<ApplyEmployeeSaleService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TenderValidatorLookupService").ImplementedBy
                      <TenderValidatorLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DepositedCashMaintenanceService").ImplementedBy
                <DepositedCashMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DepositedCashLookUpService").ImplementedBy
                <DepositedCashLookUpService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("Upc5FaceValueLookupService").ImplementedBy
                      <Upc5FaceValueLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("Upc5FaceValueModifyService").ImplementedBy
                      <Upc5FaceValueModifyService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CouponParametersLookupService").ImplementedBy
                      <CouponParametersLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CouponParametersSaveService").ImplementedBy
                      <CouponParametersSaveService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AccountSaveOrUpdateService").ImplementedBy
                      <AccountSaveOrUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AccountAvailableActivitiesLookupService").ImplementedBy
                      <AccountAvailableActivitiesLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AccountProfileNameLookupService").ImplementedBy
                  <AccountProfileNameLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AccountProfileMaintenanceService").ImplementedBy
                  <AccountProfileMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AccountProfileLookupService").ImplementedBy
                  <AccountProfileLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AccountLookupService").ImplementedBy<AccountLookupService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TillBalancePeriodCalculateService").ImplementedBy
                      <TillBalancePeriodCalculateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SafeBalancePeriodCalculateService").ImplementedBy
                      <SafeBalancePeriodCalculateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BankingSafeBalancePeriodCalculateService").ImplementedBy
                     <BankingSafeBalancePeriodCalculateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OpeningFundsConfigurationSaveOrUpdateService").ImplementedBy
                      <OpeningFundsConfigurationSaveOrUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ActivityReferenceConfigurationMaintenanceService").ImplementedBy<ActivityReferenceConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ActivityReferenceConfigurationLookupService").ImplementedBy<ActivityReferenceConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TicketTotalService").ImplementedBy<TicketTotalService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("UpdateOnlineItemsAuthorizationService")
                         .ImplementedBy<UpdateOnlineItemsAuthorizationService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OnlineItemLookupService")
                         .ImplementedBy<OnlineItemLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SystemParametersAddService").ImplementedBy
                      <SystemParametersAddService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SystemParametersGetService").ImplementedBy
                      <SystemParametersGetService>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("AutoSuspendParameterAddService").ImplementedBy
                 <AutoSuspendParameterAddService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointFullGetByComputerNameService").ImplementedBy
                      <TouchPointFullGetByComputerName>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MessageAddService").ImplementedBy<MessageAddService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MessageUpdateService").ImplementedBy<MessageUpdateService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MessageDeleteService").ImplementedBy<MessageDeleteService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MessageFindService").ImplementedBy<MessageFindService>().
                      LifeStyle.Transient);





            builder.Register(Component.For<IBusinessService>().Named("ExpressionMetadataFindService").ImplementedBy
                      <ExpressionMetadataFindService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TenderExchangeSaveOrUpdateService").ImplementedBy
                      <TenderExchangeSaveOrUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TenderExchangeLookupService").ImplementedBy
                      <TenderExchangeLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ChangeCustomerOrderProcessStatusService").ImplementedBy
            <ChangeCustomerOrderProcessStatusService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SubmitCustomerOrderService").ImplementedBy
            <SubmitCustomerOrderService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OwnBagConfigurationMaintenanceService").ImplementedBy
           <OwnBagConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OwnBagConfigurationLookupService").ImplementedBy
           <OwnBagConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerOrderUpdatePickupTimeService").ImplementedBy
            <CustomerOrderUpdatePickupTimeService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataWithLineItemAddService").ImplementedBy
                      <DataWithLineItemAddService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("XReportPrintService").ImplementedBy<XReportPrintService>().
                      LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("XReportPrintV2Service").ImplementedBy<XReportPrintV2Service>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ZReportPrintV2Service").ImplementedBy<ZReportPrintV2Service>().LifeStyle.Transient);

            builder.Register(Component.For<IXZReportData>().Named("IXZReportData").ImplementedBy<XZReportDataFetcher>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("XZReportConfigurationService").ImplementedBy<XZReportConfigurationService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("XZReportStatusService").ImplementedBy<XZReportStatusService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerOrderProcessListingService").ImplementedBy
            <CustomerOrderProcessListingService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AccountMonetaryActivitiesLookupService")
                      .ImplementedBy<AccountMonetaryActivitiesLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CashOfficeActivitiesLookupService")
                      .ImplementedBy<CashOfficeActivitiesLookupService>().LifeStyle.Transient);

            builder.RegisterService<CashOfficeActivityLogAuditTrailService>();

            builder.Register(Component.For<IBusinessService>().Named("ReceiptFormattingService").ImplementedBy
                 <ReceiptFormattingService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PrintSelfWeighSlipService").ImplementedBy
                <PrintSelfWeighSlipService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReceiptLayoutPreviewService").ImplementedBy
                 <ReceiptLayoutPreviewService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReceiptLayoutLookupService")
                      .ImplementedBy<ReceiptLayoutLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReceiptDescriptorsLookupService")
                      .ImplementedBy<ReceiptDescriptorsLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReceiptLayoutMaintenanceService")
                      .ImplementedBy<ReceiptLayoutMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("EndorsementMaintenanceService")
                      .ImplementedBy<EndorsementMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("EndorsementService").ImplementedBy
               <EndorsementService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LayoutTemplateLookupService")
                      .ImplementedBy<LayoutTemplateLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReceiptSlipTypeMaintenanceService")
              .ImplementedBy<ReceiptSlipTypeMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReceiptSlipTypeLookupService")
                      .ImplementedBy<ReceiptSlipTypeLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReceiptLineDefinitionMaintenanceService")
            .ImplementedBy<ReceiptLineDefinitionMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReceiptLineDefinitionLookupService")
                 .ImplementedBy<ReceiptLineDefinitionLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LayoutTemplateMaintenanceService")
                      .ImplementedBy<LayoutTemplateMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MonetaryActivitiesAdjustmentService").ImplementedBy
                      <MonetaryActivitiesAdjustmentService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OpenAccountBalanceFindService").ImplementedBy
          <OpenAccountBalanceFindService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ItemViewService")
                      .ImplementedBy<ItemViewService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PriceQueryService")
                      .ImplementedBy<PriceQueryService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ItemGroupingByHierarchyLookupService")
                      .ImplementedBy<ItemGroupingByHierarchyLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ItemsSearchLookupService")
                      .ImplementedBy<ItemsSearchService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SetAllProvidersDataService")
                         .ImplementedBy<SetAllProvidersDataService>().LifeStyle.Transient);

            builder.RegisterService<IdmAuthorizationService>();

            builder.Register(Component.For<IBusinessService>().Named("IdmDeleteUserService")
                         .ImplementedBy<IdmDeleteUserService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ConfigurationLookupService")
                         .ImplementedBy<ConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TransactionGapFillerConfigurationLookupService").ImplementedBy<TransactionGapFillerConfigurationLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("TransactionGapFillerConfigurationMaintenanceService").ImplementedBy<TransactionGapFillerConfigurationMaintenanceService>().LifeStyle.Transient);
            builder.RegisterMapper<TransactionGapFillerJobConfigurationContractToModelMapper>();
            builder.RegisterMapper<TransactionGapFillerJobConfigurationModelToContractMapper>();


            builder.Register(Component.For<IBusinessService>().Named("MultiCustomerBehaviorLookupService").ImplementedBy<MultiCustomerBehaviorLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("MultiCustomerBehaviorMaintenanceService").ImplementedBy<MultiCustomerBehaviorMaintenanceService>().LifeStyle.Transient);
            builder.RegisterMapper<MultiCustomerBehaviorContractToModelMapper>();
            builder.RegisterMapper<MultiCustomerBehaviorModelToContractMapper>();


            builder.Register(Component.For<IBusinessService>().Named("RedisCacheTrimConfigurationLookupService").ImplementedBy<RedisCacheTrimConfigurationLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("RedisCacheTrimConfigurationMaintenanceService").ImplementedBy<RedisCacheTrimConfigurationMaintenanceService>().LifeStyle.Transient);
            builder.RegisterMapper<RedisCacheTrimJobConfigurationContractToModelMapper>();
            builder.RegisterMapper<RedisCacheTrimJobConfigurationModelToContractMapper>();

            builder.Register(Component.For<IBusinessService>().Named("ConfigurationMaintenanceService")
                         .ImplementedBy<ConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SavingAccountMaintenanceService").ImplementedBy
                      <SavingAccountMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SavingAccountLookupService").ImplementedBy
                      <SavingAccountLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SavingAccountProviderLookupService").ImplementedBy
                    <SavingAccountProviderLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SavingAccountDepositService").ImplementedBy
                    <SavingAccountDepositService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetCustomerSavingAccountDetailsService").ImplementedBy
                    <GetCustomerSavingAccountDetailsService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetPersonalAccountsService").ImplementedBy
                    <GetPersonalAccountsService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("StoreBalancePeriodStartDateGetService")
                      .ImplementedBy<StoreBalancePeriodStartDateGetService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("StoreBalancePeriodLookupService")
                      .ImplementedBy<StoreBalancePeriodLookupService>().LifeStyle.Transient);

            builder.RegisterService<SettleConfigurationLookupService>();
            builder.RegisterService<SettleConfigurationMaintenanceService>();

            builder.Register(Component.For<IServicePerformanceCountersFactory>().Named("ServicePerformanceCountersFactory")
                         .ImplementedBy<ServicePerformanceCountersFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IServicePerformanceCountersFactory>().Named("ServicePerformanceNullCountersFactory")
                         .ImplementedBy<ServicePerformanceNullCountersFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IServicePerformanceCountersFactory>().Named("GroceryServicePerformanceNullCountersFactory")
                         .ImplementedBy<GroceryServicePerformanceNullCountersFactory>().LifeStyle.Transient);

            builder.Register(Component.For<IMovableFactory>().Named("MovableFactory")
                     .ImplementedBy<MovableFactory>().LifeStyle.Singleton);


            builder.Register(Component.For<IBusinessService>().Named("TillBalancePeriodSaveOrUpdateService")
                      .ImplementedBy<TillBalancePeriodSaveOrUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SafeBalancePeriodSaveOrUpdateService")
                    .ImplementedBy<SafeBalancePeriodSaveOrUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BankingSafeBalancePeriodSaveOrUpdateService")
              .ImplementedBy<BankingSafeBalancePeriodSaveOrUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ForcedSignOffService")
                .ImplementedBy<ForcedSignOffService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SecurityWeightToleranceLookupService")
                      .ImplementedBy<SecurityWeightToleranceLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SecurityWeightToleranceMaintenanceService")
                      .ImplementedBy<SecurityWeightToleranceMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SecurityScaleValidationService")
                      .ImplementedBy<SecurityScaleValidationService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CouponRewardOverrideService").ImplementedBy
                      <CouponRewardOverrideService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReturnPolicySaveService")
                      .ImplementedBy<ReturnPolicySaveService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetActiveReturnPolicyService")
                      .ImplementedBy<GetActiveReturnPolicyService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReturnPolicyLookupService")
                      .ImplementedBy<ReturnPolicyLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReturnPolicyCopyService")
                      .ImplementedBy<ReturnPolicyCopyService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReturnPolicySubmitService")
                      .ImplementedBy<ReturnPolicySubmitService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReturnPolicyDeleteService")
                      .ImplementedBy<ReturnPolicyDeleteService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("UpdateReturnPolicyEndDateTimeService")
                      .ImplementedBy<UpdateReturnPolicyEndDateTimeService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("RefundSwitchTenderAprovalService")
                      .ImplementedBy<RefundSwitchTenderAprovalService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DefaultResourcesManagementService").ImplementedBy
                 <DefaultResourcesManagement>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LocalizedResourceInfoService").ImplementedBy
                 <LocalizedResourceInfoService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LocalizedResourceLookupService").ImplementedBy
                 <LocalizedResourceLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LocalizedResourceExportService").ImplementedBy
                    <LocalizedResourceExportService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LocalizedResourceMaintenanceService").ImplementedBy
                    <LocalizedResourceMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("HierarchyCultureMaintenanceService").ImplementedBy
                    <HierarchyCultureMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<LocalizedListMaintenanceService>().Named("LocalizedListMaintenanceService").ImplementedBy
                    <LocalizedListMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<LocalizedListLookupService>().Named("LocalizedListLookupService").ImplementedBy
                    <LocalizedListLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("HierarchyCultureLookupService").ImplementedBy
                    <HierarchyCultureLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<ILocalizedDescriptionShared>().Named("LocalizedDescriptionShared").ImplementedBy
                    <LocalizedDescriptionShared>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("FindRetailTransactionLogsBySaleLineLinksService").ImplementedBy
                      <FindRetailTransactionLogsBySaleLineLinksService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetPhysicalToLogicalHierarchyService").ImplementedBy
                      <GetPhysicalToLogicalHierarchyService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("UpdatePhysicalToLogicalHierrarchyService").ImplementedBy
             <UpdatePhysicalToLogicalHierrarchyService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PhysicalToLogicalHierarchyMaintenanceService").ImplementedBy
             <PhysicalToLogicalHierarchyMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CurrencyRateMaintenanceService")
                  .ImplementedBy<CurrencyRateMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CurrencyRateLookupService")
                  .ImplementedBy<CurrencyRateLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DenominationMaintenanceService")
                      .ImplementedBy<DenominationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DenominationLookupService")
                  .ImplementedBy<DenominationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IDenominationsValidator>().Named("AllIdsOrNoIdsAreSpecifiedValidator").ImplementedBy<AllIdsOrNoIdsAreSpecifiedValidator>().LifeStyle.Transient);
            builder.Register(Component.For<IDenominationsValidator>().Named("UniqueAmountPerDenominationValidator").ImplementedBy<UniqueAmountPerDenominationValidator>().LifeStyle.Transient);
            builder.Register(Component.For<IDenominationsValidator>().Named("DescriptionLengthDenominationValidator").ImplementedBy<DescriptionLengthDenominationValidator>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("GetReversalTaxesService").ImplementedBy
                     <GetReversalTaxesService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PriceBatchLookupService").ImplementedBy
                 <PriceBatchLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ConsumablePriceChangeStatusUpdateService").ImplementedBy
                 <ConsumablePriceChangeStatusUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ApplyTaxReversalService").ImplementedBy
            <ApplyTaxReversalService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataPatternMetadataMaintenanceService")
            .ImplementedBy<DataPatternMetadataMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BalanceInqueryService").ImplementedBy<BalanceInqueryService>().
                  LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("HistoryInquiryService").ImplementedBy<HistoryInquiryService>().
                LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SimulateBusinessDateService")
                         .ImplementedBy<SimulateBusinessDateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LanguageLookupService").ImplementedBy
                 <LanguageLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CouponSeriesGetByIdService")
                         .ImplementedBy<CouponSeriesGetByIdService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CouponSeriesLookUpService")
                         .ImplementedBy<CouponSeriesLookUpService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CouponSeriesMaintenanceService")
                         .ImplementedBy<CouponSeriesMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CouponInstanceGetByIdService")
                         .ImplementedBy<CouponInstanceGetByIdService>().LifeStyle.Transient);

            builder.RegisterService<CouponMaintenanceService>();

            builder.Register(Component.For<IBusinessService>().Named("CouponLookupService").ImplementedBy<CouponLookupService>().
               LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CouponInstanceLookUpService")
                         .ImplementedBy<CouponInstanceLookUpService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CouponInstanceMaintenanceService")
                         .ImplementedBy<CouponInstanceMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ScoInterventionLookupService").ImplementedBy
                <ScoInterventionLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ScoInterventionConfigurationLookupService").ImplementedBy
                <ScoInterventionConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ScoInterventionConfigurationMaintenanceService").ImplementedBy
                <ScoInterventionConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ScoInterventionConfigurationMaintenanceLookupService")
                         .ImplementedBy<ScoInterventionConfigurationMaintenanceLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ScoInterventionMaintenanceService").ImplementedBy
            <ScoInterventionMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ThirdPartySessionStartingService")
                         .ImplementedBy<ThirdPartySessionStartingService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ThirdPartySessionUpdateService")
                         .ImplementedBy<ThirdPartySessionUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ThirdPartySessionFinishedService")
                         .ImplementedBy<ThirdPartySessionFinishedService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ThirdPartySessionCancelledService")
                         .ImplementedBy<ThirdPartySessionCancelledService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("StoreSalesReportService")
                         .ImplementedBy<StoreSalesReportService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetLogicalServerOnlinePercentageService")
                         .ImplementedBy<GetLogicalServerOnlinePercentageService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetLogicalServerHierarchyHealthStatusService")
                         .ImplementedBy<GetLogicalServerHierarchyHealthStatusService>().LifeStyle.Transient);

            builder.Register(Component.For<IEnvironmentProvider>().Named("EnvironmentProvider")
                          .ImplementedBy<EnvironmentProvider>().LifeStyle.Singleton);

            builder.Register(Component.For<IBusinessService>().Named("LineItemGiftReceiptService")
                         .ImplementedBy<LineItemGiftReceiptService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AccessServicesService")
                         .ImplementedBy<AccessServicesService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetVersionService").ImplementedBy
                                 <GetVersionService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("EntityPolicyMaintenanceService")
                          .ImplementedBy<EntityPolicyMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("EntityPolicyDeleteService")
                          .ImplementedBy<EntityPolicyDeleteService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DirectInvocationService")
                           .ImplementedBy<DirectInvocationService>().LifeStyle.Singleton);

            builder.Register(Component.For<IBusinessService>().Named("ReturnAllTransactionService").ImplementedBy
                    <ReturnAllTransactionService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetUnhandledReturnAllLinesService").ImplementedBy
                    <GetUnhandledReturnAllLinesService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetUnreturnableReturnAllLinesService").ImplementedBy
                    <GetUnreturnableReturnAllLinesService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named(typeof(SafeTransferService).Name)
                    .ImplementedBy<SafeTransferService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GiftReceiptNoSalePrintService")
                    .ImplementedBy<GiftReceiptNoSalePrintService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GiftReceiptRedemptionService")
                    .ImplementedBy<GiftReceiptRedemptionService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetFailedTokenInfoForServerService")
                    .ImplementedBy<GetFailedTokenInfoForServerService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetStuckTokenService")
                    .ImplementedBy<GetStuckTokenService>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("GiftReceiptSelectionPriorityService")
                    .ImplementedBy<GiftReceiptSelectionPriorityService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named(MenuConfigurationMaintenanceServiceDispatcher.ServiceGenericKey).ImplementedBy
           <MenuConfigurationMaintenanceServiceDispatcher>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named(MenuConfigurationMaintenanceServiceDispatcher.ServiceV1Key).ImplementedBy
                    <StoreServices.BusinessServices.FrontEnd.MenuConfiguration.Maintenance.V1.MenuConfigurationMaintenanceServiceV1>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named(MenuConfigurationMaintenanceServiceDispatcher.ServiceV2Key).ImplementedBy
                    <MenuConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named(MenuDisplayTermsMaintenanceServiceDispatcher.ServiceGenericKey).ImplementedBy
           <MenuDisplayTermsMaintenanceServiceDispatcher>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named(MenuDisplayTermsMaintenanceServiceDispatcher.ServiceV1Key).ImplementedBy
                    <StoreServices.BusinessServices.FrontEnd.MenuConfiguration.Maintenance.V1.MenuDisplayTermsMaintenanceServiceV1>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named(MenuDisplayTermsMaintenanceServiceDispatcher.ServiceV2Key).ImplementedBy
                    <MenuDisplayTermsMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AvailableTimeMaintenanceService").ImplementedBy
                    <AvailableTimeMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("EntityBehaviorMaintenanceService").ImplementedBy
                    <EntityBehaviorMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MemberAccountsToDisplayMaintenanceService").ImplementedBy
                  <MemberAccountsToDisplayMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MemberAccountsToDisplayConfigurationLookupService").ImplementedBy
                <MemberAccountsToDisplayConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MemberAccountsToDisplayConfigurationMaintenanceService").ImplementedBy
                <MemberAccountsToDisplayConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MemberAccountsToDisplayLookupService").ImplementedBy
                <MemberAccountsToDisplayLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("EntityBehaviorLookupService").ImplementedBy
                    <EntityBehaviorLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MobileFarmStoreDistributionLookupService").ImplementedBy
                <MobileFarmStoreDistributionLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MobileFarmStoreDistributionMaintenanceService").ImplementedBy
                <MobileFarmStoreDistributionMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MessageParameterDefinitionMaintenanceService")
                .ImplementedBy<MessageParameterDefinitionMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MessageParameterDefinitionLookupService")
                .ImplementedBy<MessageParameterDefinitionLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ServerFailedTokenLookupService")
                .ImplementedBy<ServerFailedTokenLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named(MenuConfigurationServiceDispatcher.ServiceV1Key).ImplementedBy
                    <StoreServices.BusinessServices.FrontEnd.MenuConfiguration.Lookup.V1.MenuConfigurationLookupServiceV1>().LifeStyle.Transient);

            builder.RegisterService<MenuConfigurationLookupService>();

            builder.Register(Component.For<IBusinessService>().Named("MenuVersionLookupService").ImplementedBy
                    <MenuVersionLookupService>().LifeStyle.Transient);

            // register menu display terms lookup services 
            builder.Register(Component.For<IBusinessService>().Named(MenuDisplayTermsLookupServiceDispatcher.ServiceGenericKey).ImplementedBy
           <MenuDisplayTermsLookupServiceDispatcher>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named(MenuDisplayTermsLookupServiceDispatcher.ServiceV1Key).ImplementedBy
                    <StoreServices.BusinessServices.FrontEnd.MenuConfiguration.Lookup.V1.MenuDisplayTermsLookupServiceV1>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named(MenuDisplayTermsLookupServiceDispatcher.ServiceV2Key).ImplementedBy
                    <MenuDisplayTermsLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CashierBarcodePrintService")
                   .ImplementedBy<CashierBarcodePrintService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IdmAuthorizedChangePasswordService")
                    .ImplementedBy<IdmAuthorizedChangePasswordService>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("IdmForceSignInService")
                .ImplementedBy<IdmForceSignInService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("HandshakeService")
                    .ImplementedBy<HandshakeService>().LifeStyle.Transient);

            if (ConfigurationManager.AppSettings["UseClaim"] == "true")
            {
                builder.RegisterSingleton<IHandshakeProvider, ClaimHandshakeProvider>();
            }
            else
            {
                builder.Register(Component.For<IHandshakeProvider>().Named("Handshake")
                    .ImplementedBy<HandshakeProvider>().LifeStyle.Transient);
            }

            builder.Register(Component.For<IBusinessService>().Named("TouchPointShutdownService")
                .ImplementedBy<TouchPointShutdownService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointHeartBeatService")
                .ImplementedBy<TouchPointHeartBeatService>().LifeStyle.Transient);

            builder.RegisterService<TouchPointStatusService>();

            builder.Register(Component.For<IBusinessService>().Named("UserRoleMaintenanceService")
                .ImplementedBy<UserRoleMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DepositLineAddService")
                .ImplementedBy<DepositLineAddService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointStartupService")
                .ImplementedBy<TouchPointStartupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BottleDepositMaintenanceService")
                .ImplementedBy<BottleDepositMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BottleDepositLookupService")
            .ImplementedBy<BottleDepositLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BottleDepositConsumablesLookupService")
                .ImplementedBy<BottleDepositConsumablesLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetForcedExchangeDetailsService").ImplementedBy
                    <GetForcedExchangeDetailsService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ForcedExchangeOverrideService")
                .ImplementedBy<ForcedExchangeOverrideService>()
                .LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AuxiliaryInfoLookupService").ImplementedBy
                    <AuxiliaryInfoLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("RetailTransactionLogsFilterService").ImplementedBy
                    <RetailTransactionLogsFilterService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("FindOriginalTransactionDepartmentsService").ImplementedBy
                    <FindOriginalTransactionDepartmentsService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerDisplayLayoutLookupService").ImplementedBy
                    <CustomerDisplayLayoutLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerToPayListingService").ImplementedBy
                    <CustomerToPayListingService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerDisplayLayoutMaintenanceService").ImplementedBy
                    <CustomerDisplayLayoutMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PosStateLookUpService").ImplementedBy<PosStateLookUpService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PosStateMaintenanceService").ImplementedBy<PosStateMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerSignupService").ImplementedBy<CDM.BusinessServices.New.CustomerSignup.CustomerSignupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerUnregisterMaintenanceService").ImplementedBy<CDM.BusinessServices.New.CustomerUnregistration.CustomerUnregisterMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerSigninService").ImplementedBy<CDM.BusinessServices.New.CustomerSignin.CustomerSigninService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerUpdatePasswordService").ImplementedBy<CustomerUpdatePasswordService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerAccountMaintenanceService").ImplementedBy<CustomerAccountMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetReturnDataFromOriginalTransactionService").ImplementedBy<GetReturnDataFromOriginalTransactionService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerPasswordResetService").ImplementedBy<CustomerPasswordResetService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("KeyboardConfigurationLookupService").ImplementedBy<KeyboardConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerLookupService").ImplementedBy<CDM.BusinessServices.New.CustomerLookup.CustomerLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GetCustomerService").ImplementedBy<CDM.BusinessServices.New.GetCustomer.GetCustomerService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerLookupLegacyService").ImplementedBy<CustomerLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LineItemTareModifierService").ImplementedBy<LineItemTareModifierService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AlertTemplateMaintenanceService").ImplementedBy<AlertTemplateMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AlertTemplateLookupService").ImplementedBy<AlertTemplateLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AlertRaiseService").ImplementedBy<AlertRaiseService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PrePayByPointsPromotionService").ImplementedBy<PrePayByPointsPromotionService>().LifeStyle.Transient);
           
            builder.Register(Component.For<IBusinessService>().Named("VoidPayByPointsPromotionService").ImplementedBy<VoidPayByPointsPromotionService>().LifeStyle.Transient);

           
            builder.Register(Component.For<IBusinessService>().Named("PayByPointsPromotionService").ImplementedBy<PayByPointsPromotionService>().LifeStyle.Transient);

            builder.Register(Component.For<IAlertRemotableProxy>().Named("IAlertRemotableProxy").ImplementedBy<AlertRemotableProxy>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AlertLookupService").ImplementedBy<AlertLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IAddBulkServiceExecuter>().Named("IAddBulkServiceExecuter").ImplementedBy<AddBulkServiceExecuter>().LifeStyle.Transient);
            builder.Register(Component.For<IBulkStrategyInputData>().Named("IBulkStrategyData").ImplementedBy<BulkStrategyInputData>().LifeStyle.Singleton);
            builder.Register(Component.For<ILineItemAddBulkWrapperResponseBuilder>().Named("ILineItemAddBulkWrapperResponseBuilder").ImplementedBy<LineItemAddBulkWrapperResponseBuilder>().LifeStyle.Transient);
            builder.Register(Component.For<IBulkErrorStrategyType>().Named("FailOnFirstError").ImplementedBy<FailOnFirstErrorStrategy>().LifeStyle.Transient);
            builder.Register(Component.For<IBulkErrorStrategyType>().Named("FailWithAllErrors").ImplementedBy<FailWithAllErrorsStrategy>().LifeStyle.Transient);
            builder.Register(Component.For<IBulkErrorStrategyType>().Named("ResumeOnError").ImplementedBy<ResumeOnErrorStrategy>().LifeStyle.Transient);
            builder.Register(Component.For<IBulkErrorStrategyType>().Named("ResumeWithInterventions").ImplementedBy<ResumeWithInterventionsStrategy>().LifeStyle.Transient);
            builder.Register(Component.For<ILineItemCreator>().Named("ItemLineItemCreator").ImplementedBy<ItemLineItemCreator>().LifeStyle.Transient);
            builder.Register(Component.For<ILineItemCreator>().Named("CustomerLineItemCreator").ImplementedBy<CustomerLineItemCreator>().LifeStyle.Transient);
            builder.Register(Component.For<ILineItemAddBulkWrapperCommand>().Named("ILineItemAddBulkWrapperCommand").ImplementedBy<LineItemAddBulkWrapperCommand>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TransactionLogLookupService").ImplementedBy<TransactionLogLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TransactionDisplayHeaderLookupService").ImplementedBy<TransactionDisplayHeaderLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("KeyboardConfigurationMaintenanceService").ImplementedBy<KeyboardConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<ITouchPointKeyboardConfiguration>().Named("ITouchPointKeyboardConfiguration").ImplementedBy<TouchPointKeyboardConfiguration>().LifeStyle.Transient);

            builder.Register(Component.For<IKeyboardConfigurationEnvironment>().Named("IKeyboardConfigurationEnvironment").ImplementedBy<KeyboardConfigurationEnvironment>().LifeStyle.Transient);

            builder.Register(Component.For<IKeyboardConfigurationKeyBinding>().Named("IKeyboardConfigurationKeyBinding").ImplementedBy<KeyboardConfigurationKeyBinding>().LifeStyle.Transient);

            builder.Register(Component.For<IReceiptLayout>().Named("IReceiptLayout").ImplementedBy<ReceiptLayout>().LifeStyle.Transient);

            builder.Register(Component.For<IReceiptLineDefinition>().Named("IReceiptLineDefinition").ImplementedBy<ReceiptLineDefinition>().LifeStyle.Transient);

            builder.Register(Component.For<IReceiptLayoutLineTemplate>().Named("IReceiptLayoutLineTemplate").ImplementedBy<LayoutLineTemplate>().LifeStyle.Transient);

            builder.Register(Component.For<IReceiptSlipType>().Named("IReceiptSlipType").ImplementedBy<ReceiptSlipType>().LifeStyle.Transient);

            builder.Register(Component.For<IReceiptOrganizationConsolidationStrategy>().Named("ConsolidateReturnItemSlipStrategy").ImplementedBy<ConsolidateReturnItemSlipStrategy>().LifeStyle.Transient);

            builder.Register(Component.For<IReceiptOrganizationConsolidationStrategy>().Named("DefaultStrategy").ImplementedBy<DefaultStrategy>().LifeStyle.Transient);

            builder.Register(Component.For<IReceiptLayoutLine>().Named("IReceiptLayoutLine").ImplementedBy<ReceiptLayoutLine>().LifeStyle.Transient);
            builder.Register(Component.For<IReceiptEnvironment>().Named("IReceiptEnvironment").ImplementedBy<ReceiptEnvironment>().LifeStyle.Transient);

            builder.RegisterService<CDM.BusinessServices.BackwardCompatibility.CustomerGroupMaintenanceService>();

            builder.Register(Component.For<IBusinessService>().Named("AlertGroupMaintenanceService").ImplementedBy
                <AlertGroupMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AlertGroupLookupService").ImplementedBy
                <AlertGroupLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AlertSeverityLookupService").ImplementedBy
                <AlertSeverityLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AlertSeverityMaintenanceService").ImplementedBy
                <AlertSeverityMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AlertDeleteService").ImplementedBy
                <AlertDeleteService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SelfServiceSubscriptionMaintenanceService").ImplementedBy
               <SelfServiceSubscriptionMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointCommandsLookupService")
                .ImplementedBy<TouchPointCommandsLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerAccountLookupService").ImplementedBy
                <AccountTypeLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerAccountTypeLookupService").ImplementedBy<CustomerAccountTypeLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerSegmentLookupService").ImplementedBy
                    <CDM.BusinessServices.New.SegmentLookup.CustomerSegmentLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerAuthenticationMailingSettingsLookupService")
                         .ImplementedBy<CustomerAuthenticationMailingSettingsLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerAuthenticationMailingSettingsMaintenanceService")
                         .ImplementedBy<CustomerAuthenticationMailingSettingsMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("RetailSegmentMaintenanceService").ImplementedBy
              <RetailSegmentMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("RetailSegmentLookupService").ImplementedBy
                <RetailSegmentLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerGroupMembersService").ImplementedBy
                <CustomerGroupMembersService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IndicatorGroupMaintenanceService")
            .ImplementedBy<IndicatorGroupMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IndicatorMaintenanceService")
                .ImplementedBy<IndicatorMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("IndicatorLookupService")
                .ImplementedBy<IndicatorLookupService>().LifeStyle.Transient);

            builder.RegisterService<CustomerGroupStoreMaintenanceService>();

            builder.Register(Component.For<IBusinessService>().Named("DisposalMethodMaintenanceService")
                .ImplementedBy<DisposalMethodMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CertificateSlipGeneratorService").ImplementedBy
                    <CertificateSlipGeneratorService>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("DisposalMethodLookupService")
                .ImplementedBy<DisposalMethodLookupService>().LifeStyle.Transient);

            builder.RegisterService<CDM.BusinessServices.New.CustomerSegmentRelation.CustomerSegmentRelationService>();

            builder.Register(Component.For<IBusinessService>().Named("ApproveNewServerMaintenanceService")
                                 .ImplementedBy<ApproveNewServerMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PenndingApprovalServersLookupService")
                                 .ImplementedBy<PenndingApprovalServersLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TransportFailureDownloadLookupService")
                                 .ImplementedBy<TransportFailureDownloadLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TransportFailureUploadLookupService")
                                 .ImplementedBy<TransportFailureUploadLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerAgreementMaintenanceService").ImplementedBy
                                 <CustomerAgreementMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerAgreementLookupService").ImplementedBy
                                 <CustomerAgreementLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerOrderRestrictedProductsLookupService").ImplementedBy
                                <CustomerOrderRestrictedProductsLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ProductEligibilityCheckService").ImplementedBy
                                <ProductEligibilityCheckService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReversibleEligibilityTendersLookupService").ImplementedBy
                                <ReversibleEligibilityTendersLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ReverseTendersEligibilityService").ImplementedBy
                                <ReverseTendersEligibilityService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerPromotionRegistrationService").ImplementedBy
                                 <CustomerPromotionRegistrationService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BusinessRuleMaintenanceService").ImplementedBy
                                <BusinessRuleMaintenanceService>().LifeStyle.Transient);

            builder.RegisterService<TouchPointRegisterService>();

            builder.Register(Component.For<IBusinessService>().Named("TouchPointUnregisterService").ImplementedBy
                                 <TouchPointUnregisterService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointApplicationMaintenanceService").ImplementedBy
                     <TouchPointApplicationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TouchPointApplicationLookupService").ImplementedBy
                     <TouchPointApplicationLookupService>().LifeStyle.Transient);

            builder.RegisterMapper<TouchPointApplicationToTypeMapper>();

            builder.Register(Component.For<IBusinessService>().Named("PromotionInstructionService").ImplementedBy
                     <PromotionInstructionService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("RestrictionMaintenanceService")
                .ImplementedBy<RestrictionMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("RestrictionLookupService")
                .ImplementedBy<RestrictionLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SendTransactionLogToExternalQueueService").ImplementedBy
                <SendTransactionLogToExternalQueueService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("RetransmitTransactionLogService").ImplementedBy
                <RetransmitTransactionLogService>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitValidator>().Named("RetransmitSearchCriteriaValidator").ImplementedBy
               <RetransmitSearchCriteriaValidator>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitTdmLookupFacade>().Named("RetransmitTdmLookupFacade").ImplementedBy
                          <RetransmitTdmLookupFacade>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitContractToModelMapper>().Named("RetransmitContractToModelMapper").ImplementedBy
                         <RetransmitContractToModelMapper>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitPagingHelper>().Named("RetransmitPagingHelper").ImplementedBy
                        <RetransmitPagingHelper>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitTdmLookup>().Named("RetransmitTdmLookup").ImplementedBy
                        <RetransmitTdmLookup>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitStrategy>().Named("RetransmitToRabbitStrategy").ImplementedBy
                       <RetransmitToRabbitStrategy>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitStrategy>().Named("RetransmitToSqlStrategy").ImplementedBy
                      <RetransmitToSqlStrategy>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitMessagePublisher>().Named("RetransmitMessagePublisher").ImplementedBy
                       <RetransmitMessagePublisher>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitTransportationMessageConverter>().Named("RetransmitTransportationMessageConverter").ImplementedBy
                     <RetransmitTransportationMessageConverter>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitContextPropertiesPromoter>().Named("RetransmitContextPropertiesPromoter").ImplementedBy
                    <RetransmitContextPropertiesPromoter>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitContextPropertyOriginatorHandler>().Named("RetransmitContextPropertyOriginatorHandler").ImplementedBy
                  <RetransmitContextPropertyOriginatorHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitMovableToPayloadConverter>().Named("RetransmitMovableToPayloadConverter").ImplementedBy
                 <RetransmitMovableToPayloadConverter>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitDocumemtsConverter<ITransportationMessage>>().Named("RetransmitDmsDocumentConverter").ImplementedBy
                <RetransmitDmsDocumentConverter>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitMessageSender>().Named("RetransmitRabbitUploadPublisher").ImplementedBy
                <RetransmitRabbitUploadPublisher>().LifeStyle.Transient);

            builder.Register(Component.For<IRetransmitTransportationMessageProtobuffConverter>().Named("RetransmitTransportationMessageProtobuffConverter").ImplementedBy
               <RetransmitTransportationMessageProtobuffConverter>().LifeStyle.Transient);





            builder.Register(Component.For<IBusinessService>().Named("BusinessRuleLookupService")
              .ImplementedBy<BusinessRuleLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<ITouchPointCommandParameter>().Named("ITouchPointCommandParameter")
                         .ImplementedBy<TouchPointCommandParameter>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SetParentServerInitialVersionDataService").ImplementedBy
                      <SetParentServerInitialVersionDataService>().LifeStyle.Transient);

            builder.RegisterService<CDM.BusinessServices.New.CustomerMaintenance.CustomerMaintenanceService>();
            builder.RegisterService<CDM.BusinessServices.New.CustomerMaintenance.BackwardCompatibility.CustomerMaintenanceService>();

            builder.Register(Component.For<IBusinessService>().Named("LoyaltyAccountEnrollService").ImplementedBy
                    <CDM.BusinessServices.New.Loyalty.LoyaltyAccountEnrollService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LoyaltyProgramMaintenanceService").ImplementedBy
                    <CDM.BusinessServices.New.Loyalty.LoyaltyProgramMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerOnlineServiceMaintenanceService").ImplementedBy
                    <CDM.BusinessServices.New.CustomerOnlineServiceMaintenance.CustomerOnlineServiceMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SelfServiceProgramMaintenanceService").ImplementedBy
                    <SelfServiceProgramMaintenanceService>().LifeStyle.Transient);

            builder.RegisterService<CustomerAccountBalanceUpdateService>();
            builder.RegisterService<CDM.BusinessServices.BackwardCompatibility.CustomerGroupAccountBalanceUpdateService>();

            builder.RegisterService<CustomerSegmentMaintenanceService>();
            builder.RegisterService<CDM.BusinessServices.New.Segment.BackwardCompatibility.CustomerSegmentMaintenanceService>();

            builder.Register(Component.For<IBusinessService>().Named("CustomerBISegmentMaintenanceService").ImplementedBy
                   <CDM.BusinessServices.New.Segment.BI.CustomerBISegmentMaintenanceService>().LifeStyle.Transient);

            builder.RegisterService<CustomerAccountTypeMaintenanceService>();

            builder.Register(Component.For<IBusinessService>().Named("CreditCardTypeMaintenanceService").ImplementedBy
                    <CreditCardTypeMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CreditCardTypeLookupService").ImplementedBy
                    <CreditCardLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerPaymentMeanAddService").ImplementedBy
                    <CustomerPaymentMeanAddService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerPaymentMeanWithSttLookupService").ImplementedBy
                    <CustomerPaymentMeanWithSttLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerRuleValidationLookupService").ImplementedBy
                    <CustomerRuleValidationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerPaymentMeanDetailsLookupService").ImplementedBy
                    <CustomerPaymentMeanDetailsLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerPaymentMeanUpdateService").ImplementedBy
                    <CustomerPaymentMeanUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerPaymentMeanRemoveService").ImplementedBy
                    <CustomerPaymentMeanRemoveService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LoginService").ImplementedBy<LoginService>().LifeStyle.
                   Transient);

            builder.Register(Component.For<IBusinessService>().Named("CalculateReturnPriceService").ImplementedBy
                     <CalculateReturnPriceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OperationNotifyService").ImplementedBy
                     <OperationNotifyService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AlertStatusLookupService").ImplementedBy
                    <AlertStatusLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AlertUpdateService").ImplementedBy
                    <AlertUpdateService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OposErrorsCodesLookupService").ImplementedBy
                    <OposErrorsCodesLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerAuthenticationTokenEmailSendService").ImplementedBy
                  <CustomerAuthenticationTokenEmailSendService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AlertDeviceTypeLookupService").ImplementedBy
                    <AlertDeviceTypeLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ChangeLogicalServerNameService").ImplementedBy
                    <ChangeLogicalServerNameService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerEmailVerificationService").ImplementedBy
                    <CustomerEmailVerificationService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerOnlineSubscriptionLookupService").ImplementedBy
                    <CustomerOnlineSubscriptionLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerOnlineSubscriptionMaintenanceService").ImplementedBy
                    <CustomerOnlineSubscriptionMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PromotionExecutionInformationService").ImplementedBy
                    <PromotionExecutionInformationService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerAffiliationLookupService").ImplementedBy
                   <CustomerAffiliationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerRegisteredPromotionLookupService").ImplementedBy
                  <CustomerRegisteredPromotionLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LoyaltyProgramLookupService").ImplementedBy
                                 <CDM.BusinessServices.New.Loyalty.Lookup.LoyaltyProgramLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<ICustomerOrderClassificationRequestParser>().Named("SaleClassificationRequestParser")
                .ImplementedBy<SaleClassificationRequestParser>().LifeStyle.Singleton);

            builder.Register(Component.For<ICustomerOrderClassificationRequestParser>().Named("TabClassificationRequestParser")
                .ImplementedBy<TabClassificationRequestParser>().LifeStyle.Singleton);

            builder.Register(Component.For<ICustomerOrderClassificationRequestParser>().Named("TipClassificationRequestParser")
                .ImplementedBy<TipClassificationRequestParser>().LifeStyle.Singleton);

            builder.Register(Component.For<ICustomerOrderClassificationRequestParser>().Named("OrderManagmentClassificationRequestParser")
                .ImplementedBy<OrderManagmentClassificationRequestParser>().LifeStyle.Singleton);

            builder.Register(Component.For<ICustomerOrderClassificationRequestParser>().Named("ReturnClassificationRequestParser")
              .ImplementedBy<ReturnClassificationRequestParser>().LifeStyle.Singleton);

            builder.Register(Component.For<ICustomerOrderClassificationRequestParser>().Named("MobileSaleClassificationRequestParser")
              .ImplementedBy<MobileSaleClassificationRequestParser>().LifeStyle.Singleton);

            builder.Register(Component.For<IBusinessService>().Named("LoyaltyProgramVisualLookupService").ImplementedBy
                   <LoyaltyProgramVisualLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<UserConfigurationPermissionsMaintenanceService>().Named("UserConfigurationPermissionsMaintenanceService")
            .ImplementedBy<UserConfigurationPermissionsMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<UserConfigurationPermissionsLookupService>().Named("UserConfigurationPermissionsLookupService")
            .ImplementedBy<UserConfigurationPermissionsLookupService>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("ServerGroupMaintenanceService")
            .ImplementedBy<ServerGroupMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ServerGroupLookupService")
            .ImplementedBy<ServerGroupLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ServerMaintenanceService")
            .ImplementedBy<ServerMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ServerLookupService")
            .ImplementedBy<ServerLookupService>().LifeStyle.Transient);

            builder.RegisterService<ServerCertificateLookupService>();
            builder.RegisterService<ServerSetupService>();
            builder.RegisterService<ServerInformationService>();

            builder.Register(Component.For<IMapper<IServer, ServerType>>().Named("ServerToContractMapper")
            .ImplementedBy<ServerToContractMapper>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("KitLookupService")
            .ImplementedBy<KitLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PrintSummaryService")
            .ImplementedBy<PrintSummaryService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LineItemKitModifierService")
           .ImplementedBy<LineItemKitModifierService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LineItemQuantityModifierService")
           .ImplementedBy<LineItemQuantityModifierService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OrderLineNoteModifyService")
           .ImplementedBy<OrderLineNoteModifyService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerOrderNoteModifyService")
           .ImplementedBy<CustomerOrderNoteModifyService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SalespersonModifyService")
                .ImplementedBy<SalespersonModifyService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LoyaltyProgramVisualMaintenanceService").ImplementedBy
                   <LoyaltyProgramVisualMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<RetentionPolicyMaintenanceService>().Named("RetentionPolicyMaintenanceService").ImplementedBy
                 <RetentionPolicyMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<RetentionPolicyLookupService>().Named("RetentionPolicyLookupService").ImplementedBy
                <RetentionPolicyLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<RetentionPolicyExecuteService>().Named("RetentionPolicyExecuteService").ImplementedBy
                <RetentionPolicyExecuteService>().LifeStyle.Transient);


            builder.Register(Component.For<RetentionPolicyConfigurationLookupService>().Named("RetentionPolicyConfigurationLookupService").ImplementedBy
                           <RetentionPolicyConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<RetentionPolicyConfigurationMaintenanceService>().Named("RetentionPolicyConfigurationMaintenanceService").ImplementedBy
                               <RetentionPolicyConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataProviderBulkSizeMaintenanceService").ImplementedBy
                <DataProviderBulkSizeMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TdmDataBaseMaintenanceService").ImplementedBy
                <TdmDataBaseMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ShoppingListMaintenanceService").ImplementedBy
                                 <ShoppingListMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ShoppingListLookupService").ImplementedBy
                                 <ShoppingListLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerAutomaticCreationConfigurationMaintenanceService").ImplementedBy
                                 <CustomerAutomaticCreationConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("SegmentationCriteriaGetPopulationService").ImplementedBy
                                 <SegmentationCriteriaGetPopulationService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerAccountTransactionLookupService").ImplementedBy
                                 <CustomerAccountTransactionLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ChangeCustomerOrderProcessAssignedUserService").ImplementedBy
                                 <ChangeCustomerOrderProcessAssignedUserService>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("AccountResetPolicyMaintenanceService").ImplementedBy
                                 <AccountResetPolicyMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AccountResetPolicyLookupService").ImplementedBy
                                 <AccountResetPolicyLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerMergeService").ImplementedBy<CustomerMergeService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerSplitService").ImplementedBy<CustomerSplitService>().LifeStyle.Transient);

            builder.Register(
                Component.For<RetrieveOpenDeviceErrorService>()
                .Named("RetrieveOpenDeviceErrorService")
                .ImplementedBy<RetrieveOpenDeviceErrorService>()
                .LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("LoyaltyPointsAdjustmentService").ImplementedBy
                                 <LoyaltyPointsAdjustmentService>().LifeStyle.Transient);

            builder.Register(Component.For<ISuspendedOrderLookupHelperForService>().Named("SuspendedTransactionLookupHelperForService").ImplementedBy
                    <SuspendedOrderLookupHelperForServiceTransactions>().LifeStyle.Transient);
            builder.Register(Component.For<ISuspendedOrderLookupHelperForServiceTabsLocalMode>().Named("SuspendedOrderLookupHelperForTabsLocalMode").ImplementedBy
                    <SuspendedOrderLookupHelperForServiceTabs>().LifeStyle.Transient);
            builder.Register(Component.For<ISuspendedOrderLookupHelperForServiceTabsRemoteMode>().Named("SuspendedOrderLookupHelperForTabsRemoteMode").ImplementedBy
                    <SuspendedOrderLookupHelperForServiceTabs>().LifeStyle.Transient);
            builder.Register(Component.For<ISuspendedOrderLookupHelperForServiceTripEnded>().Named("SuspendedOrderLookupHelperForServiceTripEnded").ImplementedBy
                    <SuspendedOrderLookupHelperForServiceTransactions>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CustomerContinuityPromotionsLookUpService").ImplementedBy
                    <CustomerContinuityPromotionsLookUpService>().LifeStyle.Transient);

            builder.Register(Component.For<IActivity>().Named(OrderProcessTransitionActivities.BuildEventHistoryActivityName).ImplementedBy
                                 <BuildEventHistoryActivity>().LifeStyle.Transient);

            builder.Register(Component.For<IActivity>().Named("DeleteEventHistoryActivity").ImplementedBy
                     <DeleteEventHistoryActivity>().LifeStyle.Transient);

            builder.Register(
                Component.For<IBusinessService>()
                         .Named("SetSellingModeService")
                         .ImplementedBy<SetSellingModeService>()
                         .LifeStyle.Transient);

            builder.RegisterService<ColdStartRecoveryService>();

            builder.Register(Component.For<IBusinessService>().Named("MessagesQueueErrorsResubmitService").ImplementedBy<MessagesQueueErrorsResubmitService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MessagesQueueErrorsDeleteService").ImplementedBy<MessagesQueueErrorsDeleteService>().LifeStyle.Transient);

            builder.RegisterService<BusinessUnitUrlLookupService>();

            builder.Register(Component.For<IBusinessService>().Named("MessagesQueueErrorsGetService").ImplementedBy<MessagesQueueErrorsGetService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CalculatePriceOverrideService").ImplementedBy<CalculatePriceOverrideService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ServiceAgentRegistrationService").ImplementedBy<ServiceAgentRegistrationService>().LifeStyle.Transient);

            builder.RegisterService<NotificationMaintenanceService>();

            builder.RegisterService<NotificationLookupService>();

            builder.RegisterService<ConfirmNotificationsService>();

            builder.RegisterService<RetrievePendingNotificationsService>();

            builder.RegisterService<StoreRangeLookupService>();

            builder.RegisterService<CodeSignatureVerificationConfigurationLookupService>();
            builder.RegisterService<CodeSignatureVerificationConfigurationMaintenanceService>();

            builder.Register(Component.For<IBusinessService>().Named("ValidateSessionService").ImplementedBy<ValidateSessionService>().LifeStyle.Transient);

            builder.Register(Component.For<ILinkAssociationExtractor>().Named("LinkAssociationExtractor")
                .ImplementedBy<LinkAssociationExtractor>().LifeStyle.Singleton);

            builder.Register(Component.For<ILinkGroupDataExtractor>().Named("LinkGroupDataExtractor")
                .ImplementedBy<LinkGroupDataExtractor>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ProductAvailabilityMaintenanceService")
                .ImplementedBy<ProductAvailabilityMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<ProductAvailabilityLookupService>().Named("ProductAvailabilityLookupService")
                .ImplementedBy<ProductAvailabilityLookupService>().LifeStyle.Transient);

            builder
                .Register(Component.For<IBusinessService>()
                .Named("ProductAvailabilityConfigurationMaintenanceService")
                .ImplementedBy<ProductAvailabilityConfigurationMaintenanceService>().LifeStyle.Transient);

            builder
                .Register(Component.For<IBusinessService>()
                .Named("ProductAvailabilityConfigurationLookupService")
                .ImplementedBy<ProductAvailabilityConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AddFormDataService")
                    .ImplementedBy<AddFormDataService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TransactionClasificationService")
                 .ImplementedBy<TransactionClasificationService>().LifeStyle.Transient);

            builder.Register(Component.For<IRestHandler>()
                          .ImplementedBy<RestHandler>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>()
                .Named("SupplierMaintenanceService")
                .ImplementedBy<SupplierMaintenanceService>()
                .LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>()
                .Named("SupplierLookupService")
                .ImplementedBy<SupplierLookupService>()
                .LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("RwhConfigurationExportService").ImplementedBy
                 <RwhConfigurationExportService>().LifeStyle.Transient);

            builder.RegisterQuery<RetailSegmentLookupQuery>();

            builder.RegisterService<TabsConfigurationMaintenanceService>();
            builder.RegisterService<TabsConfigurationLookupService>();
            builder.RegisterService<TabInfoLookupService>();
            builder.RegisterService<TipsConfigurationMaintenanceService>();
            builder.RegisterService<TipsConfigurationLookupService>();
            builder.RegisterService<VenueShiftsMaintenanceService>();
            builder.RegisterService<OpenCloseDayMaintenanceService>();
            builder.RegisterService<OpenCloseDayLookupService>();
            builder.RegisterService<TenderRoundingRuleLookupService>();
            builder.RegisterService<TenderRoundingRuleMaintenanceService>();
            builder.RegisterService<PredictiveRescanServiceConfigurationMaintenanceService>();
            builder.RegisterService<PredictiveRescanServiceConfigurationLookupService>();

            builder.RegisterService<BrandingFileSizeConfigurationMaintenanceService>();
            builder.RegisterService<BrandingFileSizeConfigurationLookupService>();

            builder.Register(Component.For<IBusinessService>().Named("DetailedOrderCodeConfigurationMaintenanceService").ImplementedBy<DetailedOrderCodeConfigurationMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("DetailedOrderCodeConfigurationLookupService").ImplementedBy<DetailedOrderCodeConfigurationLookupService>().LifeStyle.Transient);
            builder.RegisterMapper<DetailedOrderCodeConfigurationModelToContractMapper>();
            builder.RegisterMapper<DetailedOrderCodeConfigurationContractToModelMapper>();

            builder.Register(Component.For<IBusinessService>().Named("PosParametersConfigurationMaintenanceService").ImplementedBy<PosParametersConfigurationMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("PosParametersConfigurationLookupService").ImplementedBy<PosParametersConfigurationLookupService>().LifeStyle.Transient);
            builder.RegisterMapper<PosParametersConfigurationModelToContractMapper>();
            builder.RegisterMapper<PosParametersConfigurationContractToModelMapper>();

            builder.RegisterMapper<PosDepositBusinessConfigurationContractToModelMapper>();
            builder.RegisterMapper<PosDepositBusinessConfigurationModelToContractMapper>();
            builder.RegisterService<PosDepositBusinessConfigurationLookupService>();
            builder.RegisterService<PosDepositBusinessConfigurationMaintenanceService>();

            builder.RegisterMapper<FiscalServerConfigurationContractToModelMapper>();
            builder.RegisterMapper<FiscalServerConfigurationModelToContractMapper>();
            builder.RegisterService<FiscalConfigurationMaintenanceService>();
            builder.RegisterService<FiscalConfigurationLookupService>();

            builder.RegisterMapper<SecondPeTranReferenceDataModelToContractsMapper>();
            builder.RegisterService<SecondPeTranReferenceDataLookupService>();

            builder.RegisterMapper<EnableDevicesApiConfigurationContractToModelMapper>();
            builder.RegisterMapper<EnableDevicesApiConfigurationModelToContractMapper>();
            builder.RegisterService<EnableDevicesApiConfigurationMaintenanceService>();
            builder.RegisterService<EnableDevicesApiConfigurationLookupService>();

            builder.Register(Component.For<IBusinessService>().Named("AutoVoidAbandonedTransactionConfigurationMaintenanceService").ImplementedBy<AutoVoidAbandonedTransactionConfigurationMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("AutoVoidAbandonedTransactionConfigurationLookupService").ImplementedBy<AutoVoidAbandonedTransactionConfigurationLookupService>().LifeStyle.Transient);
            builder.RegisterMapper<AutoVoidAbandonedTransactionConfigurationContractToModel>();
            builder.RegisterMapper<AutoVoidAbandonedTransactionConfigurationModelToContract>();

            builder.RegisterService<VenueShiftsLookupService>();
            builder.RegisterService<AutoLoadMaintenanceService>();
            builder.RegisterService<AutoLoadLookupService>();
            builder.RegisterService<CtmCashManagementLookupService>();
            builder.RegisterService<AutoDeclarationLookupService>();
            builder.RegisterService<ValidateTenderAmountAgainstRoundingRuleLookupService>();

            builder.RegisterService<OnlineItemProviderConfigurationMaintenanceService>();
            builder.RegisterService<OnlineItemProviderConfigurationLookupService>();

            builder.Register(Component.For<IBusinessService>().Named("TdmConfigurationLookupService").ImplementedBy<TdmConfigurationLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("TdmConfigurationMaintenanceService").ImplementedBy<TdmConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OLAMessageDetailedLookupService").ImplementedBy<OLAMessageDetailedLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("OLAMessageLookupService").ImplementedBy<OLAMessageLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("OLAMessageMaintenanceService").ImplementedBy<OLAMessageMaintenanceService>().LifeStyle.Transient);
            builder.RegisterMapper<OLAMessageContractToModelMapper>();
            builder.RegisterMapper<OLAMessageModelToContractMapper>();

            #region TouchPoint Security Scale Configuration
            builder.Register(Component.For<IBusinessService>().Named("SecurityScaleConfigurationMaintenanceService").ImplementedBy<SecurityScaleConfigurationMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("SecurityScaleConfigurationLookupService").ImplementedBy<SecurityScaleConfigurationLookupService>().LifeStyle.Transient);

            builder.RegisterMapper<SecurityScaleConfigurationMapper>();
            builder.RegisterMapper<SecurityScaleConfigurationTypeMapper>();
            #endregion

            #region Virtual Keyboard Configuration
            builder.RegisterMapper<VirtualKeyboardConfigurationContractToModelMapper>();
            builder.RegisterMapper<VirtualKeyboardConfigurationModelToContractMapper>();
            builder.Register(Component.For<IBusinessService>().Named("VirtualKeyboardConfigurationsMaintenanceService").ImplementedBy<VirtualKeyboardConfigurationsMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("VirtualKeyboardConfigurationsLookupService").ImplementedBy<VirtualKeyboardConfigurationsLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IVirtualKeyboardConfigurationValidator>().Named("VirtualKeyboardConfigurationXMLValidator").ImplementedBy<VirtualKeyboardConfigurationXMLValidator>().LifeStyle.Transient);
            builder.Register(Component.For<IVirtualKeyboardConfigurationValidator>().Named("VirtualKeyboardConfigurationRequiredFieldsValidator").ImplementedBy<VirtualKeyboardConfigurationRequiredFieldsValidator>().LifeStyle.Transient);
            #endregion

            #region Dynamic Forms Configuration
            builder.RegisterMapper<DynamicFormsConfigurationContractToModelMapper>();
            builder.RegisterMapper<DynamicFormsConfigurationModelToContractMapper>();
            builder.Register(Component.For<IBusinessService>().Named("DynamicFormsConfigurationMaintenanceService").ImplementedBy<DynamicFormsConfigurationMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("DynamicFormsConfigurationLookupService").ImplementedBy<DynamicFormsConfigurationLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IDynamicFormsConfigurationValidator>().Named("DynamicFormsConfigurationXMLValidator").ImplementedBy<DynamicFormsConfigurationXMLValidator>().LifeStyle.Transient);
            builder.Register(Component.For<IDynamicFormsConfigurationValidator>().Named("DynamicFormsConfigurationRequiredFieldsValidator").ImplementedBy<DynamicFormsConfigurationRequiredFieldsValidator>().LifeStyle.Transient);
            #endregion

            builder.Register(Component.For<IBusinessService>().Named("POSBrandingImagesMaintenanceService").ImplementedBy<POSBrandingImagesMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("POSBrandingImagesLookupService").ImplementedBy<POSBrandingImagesLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("OfficeBrandingImagesLookupService").ImplementedBy<OfficeBrandingImagesLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("OrderCalculationService").ImplementedBy<OrderCalculationService>().LifeStyle.Transient);
            builder.RegisterMapper<POSBrandingImagesContractToModelMapper>();
            builder.RegisterMapper<POSBrandingImagesModelToContractMapper>();

            builder.RegisterMapper<TenderRoundingRuleTypeMapper>();
            builder.RegisterMapper<TenderRoundingRuleMapper>();
            builder.RegisterMapper<VenueDayShiftsMapper>();
            builder.RegisterMapper<OpenCloseDayMapper>();

            builder.RegisterMapper<ActivityReferenceConfigurationContractToModelMapper>();
            builder.RegisterMapper<ActivityReferenceConfigurationModelToContractMapper>();

            builder.Register(Component.For<IBusinessService>().Named("TabActionService")
                .ImplementedBy<TabActionService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>()
              .Named("PaymentAmountsCalculateService")
              .ImplementedBy<PaymentAmountsCalculateService>()
              .LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PreTipInService").ImplementedBy<PreTipInService>().LifeStyle.
                      Transient);

            builder.Register(Component.For<IBusinessService>().Named("TipInService").ImplementedBy<TipInService>().LifeStyle.
                      Transient);

            builder.Register(Component.For<IBusinessService>().Named("ActivityIndicationTimeoutMaintenanceService").ImplementedBy<ActivityIndicationTimeoutMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("GenerateEndOfTripBarcodeService").ImplementedBy<GenerateEndOfTripBarcodeService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ActivityIndicationTimeoutLookupService").ImplementedBy<ActivityIndicationTimeoutLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("AvailableTimeLookupService").ImplementedBy<AvailableTimeLookupService>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("TipDisplayHeaderLookupService").ImplementedBy<TipDisplayHeaderLookupService>().LifeStyle.
                      Transient);

            builder.Register(Component.For<IBusinessService>().Named("ThirdPartyUndeliveredTlogLookupService").ImplementedBy<ThirdPartyUndeliveredTlogLookupService>().LifeStyle.
                Transient);

            builder.Register(Component.For<IBusinessService>().Named("TimeAvailabilityTermLookupService").ImplementedBy<TimeAvailabilityTermLookupService>().LifeStyle.
                Transient);

            builder.Register(Component.For<IBusinessService>().Named("TimeAvailabilityTermMaintenanceService").ImplementedBy<TimeAvailabilityTermMaintenanceService>().LifeStyle.
                Transient);

            builder.RegisterService<DomainActionLookupService>();
            builder.RegisterMapper<DomainActionMapper>();

            builder.Register(Component.For<IBusinessService>().Named("DataProtectionConfigurationMaintenanceService").ImplementedBy<DataProtectionConfigurationMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("DataProtectionConfigurationLookupService").ImplementedBy<DataProtectionConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("OpenDrawerConfigurationMaintenanceService").ImplementedBy<OpenDrawerConfigurationMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("OpenDrawerConfigurationLookupService").ImplementedBy<OpenDrawerConfigurationLookupService>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("MergeTransactionService")
                .ImplementedBy<MergeTransactionService>().LifeStyle.Transient);

            builder.Register(Component.For<IAccountProfileValidator>().Named("AccountProfileValidatorValidateAccountAllowedUpdate").ImplementedBy<AccountProfileValidatorValidateAccountAllowedUpdate>().LifeStyle.Transient);
            builder.Register(Component.For<IAccountProfileValidator>().Named("AccountProfileValidatorValidateProfilesWhenDefaultExist").ImplementedBy<AccountProfileValidatorValidateProfilesWhenDefaultExist>().LifeStyle.Transient);
            builder.Register(Component.For<IAccountProfileValidator>().Named("AccountProfileValidatorValidateProfilesWhenDefaultNotExist").ImplementedBy<AccountProfileValidatorValidateProfilesWhenDefaultNotExist>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("RunExternalAppService")
                .ImplementedBy<RunExternalAppService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ExternalTransactionLookupService").ImplementedBy<ExternalTransactionLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("ExternalTransactionListingService").ImplementedBy<ExternalTransactionListingService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("VerificationMarkLookupService").ImplementedBy<VerificationMarkLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("VerificationMarkMaintenanceService").ImplementedBy<VerificationMarkMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("ExtraPriceCalculationPolicyMaintenanceService").ImplementedBy<ExtraPriceCalculationPolicyMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("ExtraPriceCalculationPolicyLookupService").ImplementedBy<ExtraPriceCalculationPolicyLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BusinessUnitUrlConfigurationMaintenanceService").ImplementedBy<BusinessUnitUrlConfigurationMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("BusinessUnitUrlConfigurationLookupService").ImplementedBy<BusinessUnitUrlConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataIntegrityConfigurationMaintenanceService").ImplementedBy
                    <DataIntegrityConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DisplayRewardConfigurationMaintenanceService").ImplementedBy
                <DisplayRewardConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DisplayRewardConfigurationLookupService").ImplementedBy
                <DisplayRewardConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataIntegrityExecutionService").ImplementedBy
                    <DataIntegrityExecutionService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataIntegrityLookupService").ImplementedBy
                                <DataIntegrityLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DataIntegrityCompareExecutionService").ImplementedBy
                    <DataIntegrityCompareExecutionService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("TransactionDataIntegrityService").ImplementedBy
                   <TransactionDataIntegrityService>().LifeStyle.Transient);

            builder.Register(Component.For<ITransactionDataIntegrityRequestValidator>().Named("TransactionDataIntegrityRequestValidator").ImplementedBy
                   <TransactionDataIntegrityRequestValidator>().LifeStyle.Transient);


            builder.RegisterService<CertificateConfigurationMaintenanceService>();
            builder.RegisterService<Retalix.StoreServices.BusinessServices.Maintenance.Certificate.V2.CertificateConfigurationMaintenanceService>();

            builder.Register(Component.For<IBusinessService>().Named("CertificateConfigurationLookupService").ImplementedBy
                <CertificateConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("CertificateProviderValidateService").ImplementedBy
                <CertificateProviderValidateService>().LifeStyle.Transient);

            builder.RegisterService<DepositCashDuringSaleModeConfigurationMaintenanceService>();
            builder.RegisterService<DepositCashDuringSaleModeConfigurationLookupService>();

            builder.RegisterService<RefundVoucherBusinessConfigurationLookupService>();
            builder.RegisterService<RefundVoucherBusinessConfigurationMaintenanceService>();

            builder.RegisterService<OfficeBusinessConfigurationMaintenanceService>();
            builder.RegisterService<OfficeBusinessConfigurationLookupService>();

            builder.RegisterService<SecondPeConnectionConfigurationMaintenanceService>();
            builder.RegisterService<SecondPeConnectionConfigurationLookupService>();

            builder.Register<IAccountToDisplayDao, AccountToDisplayDao>();

            builder.Register(Component.For<IBusinessService>().Named("DisplayAddLoyaltyConfigurationMaintenanceService").ImplementedBy<DisplayAddLoyaltyConfigurationMaintenanceService>().LifeStyle.Transient);


            builder.Register(Component.For<IBusinessService>().Named("MaskingConfigurationLookupService").ImplementedBy
                <MaskingConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("MaskingConfigurationMaintenanceService").ImplementedBy
                <MaskingConfigurationMaintenanceService>().LifeStyle.Transient);




            builder.RegisterMapper<Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.Declaration.ModelToContractConverter>();
            builder.RegisterMapper<Retalix.StoreServices.BusinessServices.FrontEnd.CashOffice.FundTransfer.ModelContractConverter>();

            builder.RegisterService<MenuItemLeafConfigurationMaintenanceService>();


            builder.Register(Component.For<IBusinessService>().Named("MaskingParametersConfigurationLookupService").ImplementedBy<MaskingParametersConfigurationLookupService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("MaskingParametersConfigurationMaintenanceService").ImplementedBy<MaskingParametersConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BusinessConfigurationMaintenanceService").ImplementedBy<BusinessConfigurationMaintenanceService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("BusinessConfigurationLookupService").ImplementedBy<BusinessConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("RecalculateReturnTaxConfigurationMaintenanceService").ImplementedBy<RecalculateReturnTaxConfigurationMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("RecalculateReturnTaxConfigurationLookupService").ImplementedBy<RecalculateReturnTaxConfigurationLookupService>().LifeStyle.Transient);

            builder.RegisterMapper<BusinessConfigurationResponseMapper>();

            builder.Register(Component.For<IBusinessService>().Named("DuplicateItemService").ImplementedBy
                <DuplicateItemService>().LifeStyle.Transient);
            builder.Register(Component.For<IDuplicateItemStrategy>().Named("DuplicateItemStrategy").ImplementedBy
                <DuplicateItemStrategy>().LifeStyle.Transient);
            builder.Register(Component.For<IDuplicateItemValidator>().Named("DuplicateItemValidator").ImplementedBy
                <DuplicateItemValidator>().LifeStyle.Transient);

            builder.Register(Component.For<IKitHelper>().Named("KitHelper").ImplementedBy
                <KitHelper>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("DelayedInterventionConfigurationMaintenanceService").ImplementedBy<DelayedInterventionConfigurationMaintenanceService>().LifeStyle.Transient);
            builder.Register(Component.For<IBusinessService>().Named("DelayedInterventionConfigurationLookupService").ImplementedBy<DelayedInterventionConfigurationLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IBusinessService>().Named("PrintPriceQueriesService").ImplementedBy
                <PrintPriceQueriesService>().LifeStyle.Transient);

            builder.RegisterMapper<EpsConfigurationContractToModelMapper>();
            builder.RegisterMapper<EpsConfigurationModelToContractMapper>();
            builder.RegisterService<EpsConfigurationMaintenanceService>();
            builder.RegisterService<EpsConfigurationLookupService>();

            builder.Register(Component.For<ICaloriesQuantity>().Named("CaloriesQuantity").ImplementedBy<CaloriesQuantity>().LifeStyle.Transient);
            builder.Register(Component.For<ICalories>().Named("CaloriesDataModel").ImplementedBy<Calories>().LifeStyle.Transient);

            builder.Register(Component.For<IMapper<ICaloriesQuantity, QuantityCommonData>>().Named("CalorieQuantityModelToContractMapper").ImplementedBy<CaloriesQuantityModelToContractMapper>().LifeStyle.Transient);
            builder.Register(Component.For<IMapper<ICalories, CaloriesType>>().Named("CaloriesModelToContractMapper").ImplementedBy<CaloriesModelToContractMapper>().LifeStyle.Transient);

            builder.Register(Component.For<ICaloriesCalculator>().Named("CaloriesCalculator").ImplementedBy<CaloriesCalculator>().LifeStyle.Singleton);

            builder.Register(Component.For<IRetailTransactionSerializationStrategy>().Named("RetailTransactionSerializationStrategy").ImplementedBy
                <RetailTransactionSerializationStrategy>().LifeStyle.Singleton);
            builder.Register(Component.For<IPayByPointsStrategy>().ImplementedBy
               <PayByPointsStrategy>().LifeStyle.Transient);
        }

        private void ConfigureRetailTransactionLogServices(IComponentInstaller builder)
        {
            builder.Register(Component.For<IBusinessService>().Named("RetailTransactionLogLookupService")
                .ImplementedBy<RetailTransactionLogLookupService>().LifeStyle.Transient);

            builder.Register(Component.For<IGiftReceiptSelectionPriorityHandlerFactory>().Named("IGiftReceiptSelectionPriorityHandlerFactory")
                .ImplementedBy<GiftReceiptSelectionPriorityHandlerFactory>().LifeStyle.Transient);
        }
    }
}
