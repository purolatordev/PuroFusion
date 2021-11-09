using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using context = System.Web.HttpContext;
/// <summary>
/// Summary description for PuroTouchRepository
/// </summary>
/// 

public class PuroTouchRepository : IPuroTouch,IDisposable
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();


        public List<ClsTaskType> GetTaskTypes()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsTaskType> oType = (from data in puroTouchContext.GetTable<tblTaskType>()
                                       join phase in puroTouchContext.GetTable<tblOnboardingPhase>() on data.idOnboardingPhase equals phase.idOnboardingPhase
                                       //where data.ActiveFlag != false
                                       orderby data.TaskType
                                       select new ClsTaskType
                                       {
                                           idTaskType = data.idTaskType,
                                           TaskType = data.TaskType,
                                           idOnboardingPhase = Convert.ToInt16(data.idOnboardingPhase),
                                           OnboardingPhase = phase.OnboardingPhase,
                                           CreatedBy = data.CreatedBy,
                                           CreatedOn = data.CreatedOn,
                                           UpdatedBy = data.UpdatedBy,
                                           UpdatedOn = data.UpdatedOn,
                                           ActiveFlag = data.ActiveFlag

                                       }).ToList<ClsTaskType>();
            return oType;
        }

        public List<ClsTaskType> GetTaskTypesInactiveNoted()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsTaskType> oType = (from data in puroTouchContext.GetTable<tblTaskType>()
                                       //where data.ActiveFlag != false
                                       orderby data.TaskType
                                       select new ClsTaskType
                                       {
                                           idTaskType = data.idTaskType,
                                           //TaskType = data.TaskType,
                                           TaskType = Convert.ToBoolean(data.ActiveFlag) == false ? data.TaskType + " (Inactive)" : data.TaskType.ToUpper(),
                                           CreatedBy = data.CreatedBy,
                                           CreatedOn = data.CreatedOn,
                                           UpdatedBy = data.UpdatedBy,
                                           UpdatedOn = data.UpdatedOn,
                                           ActiveFlag = data.ActiveFlag

                                       }).ToList<ClsTaskType>();
            return oType;
        }

        public List<ClsDataEntryMethods> GetDataEntryMethods()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsDataEntryMethods> oMethods = (from data in puroTouchContext.GetTable<tblDataEntryMethod>()
                                                  //where data.ActiveFlag != false
                                                  orderby data.DataEntry
                                               select new ClsDataEntryMethods
                                       {
                                           idDataEntry = Convert.ToInt16(data.idDataEntry),
                                           DataEntry = data.DataEntry,
                                           CreatedBy = data.CreatedBy,
                                           CreatedOn = data.CreatedOn,
                                           UpdatedBy = data.UpdatedBy,
                                           UpdatedOn = data.UpdatedOn,
                                           ActiveFlag = data.ActiveFlag

                                       }).ToList<ClsDataEntryMethods>();
            return oMethods;
        }
        public List<ClsDataEntryMethods> GetDataEntryMethodsInactiveNoted()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsDataEntryMethods> oMethods = (from data in puroTouchContext.GetTable<tblDataEntryMethod>()
                                                  //where data.ActiveFlag != false
                                                  orderby data.DataEntry
                                                  select new ClsDataEntryMethods
                                                  {
                                                      idDataEntry = Convert.ToInt16(data.idDataEntry),
                                                      //DataEntry = data.DataEntry,
                                                      DataEntry = Convert.ToBoolean(data.ActiveFlag) == false ? data.DataEntry + " (Inactive)" : data.DataEntry.ToUpper(),
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      ActiveFlag = data.ActiveFlag

                                                  }).ToList<ClsDataEntryMethods>();
            return oMethods;
        }

        public List<ClsThirdPartyVendor> GetThirdPartyVendors()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsThirdPartyVendor> oVendors = (from data in puroTouchContext.GetTable<tblThirdPartyVendor>()
                                                  //where data.ActiveFlag != false
                                                  orderby data.VendorName
                                                  select new ClsThirdPartyVendor
                                                  {
                                                      idThirdPartyVendor = Convert.ToInt16(data.idThirdPartyVendor),
                                                      VendorName = data.VendorName,
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      ActiveFlag = data.ActiveFlag

                                                  }).ToList<ClsThirdPartyVendor>();
            return oVendors;
        }

        public List<ClsThirdPartyVendor> GetThirdPartyVendorsInactiveNoted()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsThirdPartyVendor> oVendors = (from data in puroTouchContext.GetTable<tblThirdPartyVendor>()
                                                  //where data.ActiveFlag != false
                                                  orderby data.VendorName
                                                  select new ClsThirdPartyVendor
                                                  {
                                                      idThirdPartyVendor = Convert.ToInt16(data.idThirdPartyVendor),
                                                      //VendorName = data.VendorName,
                                                      VendorName = Convert.ToBoolean(data.ActiveFlag) == false ? data.VendorName + " (Inactive)" : data.VendorName.ToUpper(),
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      ActiveFlag = data.ActiveFlag

                                                  }).ToList<ClsThirdPartyVendor>();
            return oVendors;
        }



        public List<ClsBroker> GetBrokers()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsBroker> oBrokers = (from data in puroTouchContext.GetTable<tblBroker>()
                                        //where data.ActiveFlag != false
                                        orderby data.Broker
                                        select new ClsBroker
                                                  {
                                                      idBroker = Convert.ToInt16(data.idBroker),
                                                      //Broker = data.Broker,
                                                      Broker = Convert.ToBoolean(data.ActiveFlag) == false ? data.Broker + " (Inactive)" : data.Broker.ToUpper(),
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      ActiveFlag = data.ActiveFlag

                                                  }).ToList<ClsBroker>();
            return oBrokers;
        }
        public List<ClsBroker> GetActiveBrokers()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsBroker> oBrokers = (from data in puroTouchContext.GetTable<tblBroker>()
                                        where data.ActiveFlag != false
                                        orderby data.Broker
                                        select new ClsBroker
                                        {
                                            idBroker = Convert.ToInt16(data.idBroker),
                                            Broker = data.Broker,
                                            //Broker = Convert.ToBoolean(data.ActiveFlag) == false ? data.Broker + " (Inactive)" : data.Broker.ToUpper(),
                                            CreatedBy = data.CreatedBy,
                                            CreatedOn = data.CreatedOn,
                                            UpdatedBy = data.UpdatedBy,
                                            UpdatedOn = data.UpdatedOn,
                                            ActiveFlag = data.ActiveFlag

                                        }).ToList<ClsBroker>();
            return oBrokers;
        }
        public List<ClsVendorType> GetVendorTypes()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsVendorType> oVendorTypes = (from data in puroTouchContext.GetTable<tblVendorType>()
                                        //where data.ActiveFlag != false
                                        orderby data.VendorType
                                        select new ClsVendorType
                                        {
                                            idVendorType = Convert.ToInt16(data.idVendorType),
                                            //Broker = data.Broker,
                                            VendorType = Convert.ToBoolean(data.ActiveFlag) == false ? data.VendorType + " (Inactive)" : data.VendorType.ToUpper(),
                                            CreatedBy = data.CreatedBy,
                                            CreatedOn = data.CreatedOn,
                                            UpdatedBy = data.UpdatedBy,
                                            UpdatedOn = data.UpdatedOn,
                                            ActiveFlag = data.ActiveFlag

                                        }).ToList<ClsVendorType>();
            return oVendorTypes;
        }
        public List<ClsVendorType> GetActiveVendorTypes()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsVendorType> oVendorTypes = (from data in puroTouchContext.GetTable<tblVendorType>()
                                                where data.ActiveFlag != false
                                                orderby data.VendorType
                                                select new ClsVendorType
                                                {
                                                    idVendorType = Convert.ToInt16(data.idVendorType),
                                                    VendorType = data.VendorType,
                                                    //VendorType = Convert.ToBoolean(data.ActiveFlag) == false ? data.VendorType + " (Inactive)" : data.VendorType.ToUpper(),
                                                    CreatedBy = data.CreatedBy,
                                                    CreatedOn = data.CreatedOn,
                                                    UpdatedBy = data.UpdatedBy,
                                                    UpdatedOn = data.UpdatedOn,
                                                    ActiveFlag = data.ActiveFlag

                                                }).ToList<ClsVendorType>();
            return oVendorTypes;
        }

        public List<ClsTargetDates> GetTargetChangeReasons()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsTargetDates> oTargetDates = (from data in puroTouchContext.GetTable<tblDiscoveryRequestTargetDate>()
                                                 join req in puroTouchContext.GetTable<tblDiscoveryRequest>() on data.idRequest equals req.idRequest
                                                 orderby req.CustomerName
                                                select new ClsTargetDates
                                                {
                                                    idTargetDate = Convert.ToInt16(data.idTargetDate),
                                                    idRequest = Convert.ToInt16(data.idRequest),
                                                    TargetDate = data.TargetDate,
                                                    ChangeReason = data.ChangeReason,
                                                    CreatedBy = data.CreatedBy,
                                                    CreatedOn = data.CreatedOn,
                                                    CustomerName = req.CustomerName
                                                }).ToList<ClsTargetDates>();
            return oTargetDates;
        }

        public List<ClsShippingVendor> GetShippingVendors()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsShippingVendor> oVend = (from data in puroTouchContext.GetTable<tblShippingVendor>()
                                        where data.ActiveFlag != false
                                        orderby data.SortFlag,data.VendorName
                                             select new ClsShippingVendor
                                        {
                                            idShippingVendor = Convert.ToInt16(data.idShippingVendor),
                                            VendorName = data.VendorName,
                                            //Broker = Convert.ToBoolean(data.ActiveFlag) == false ? data.Broker + " (Inactive)" : data.Broker.ToUpper(),
                                            CreatedBy = data.CreatedBy,
                                            CreatedOn = data.CreatedOn,
                                            UpdatedBy = data.UpdatedBy,
                                            UpdatedOn = data.UpdatedOn,
                                            ActiveFlag = data.ActiveFlag

                                        }).ToList<ClsShippingVendor>();
            return oVend;
        }

        public List<ClsShippingVendor> GetAllShippingVendors()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsShippingVendor> oVend = (from data in puroTouchContext.GetTable<tblShippingVendor>()
                                             //where data.ActiveFlag != false
                                             orderby data.VendorName
                                             select new ClsShippingVendor
                                             {
                                                 idShippingVendor = Convert.ToInt16(data.idShippingVendor),
                                                 VendorName = data.VendorName,
                                                 //Broker = Convert.ToBoolean(data.ActiveFlag) == false ? data.Broker + " (Inactive)" : data.Broker.ToUpper(),
                                                 CreatedBy = data.CreatedBy,
                                                 CreatedOn = data.CreatedOn,
                                                 UpdatedBy = data.UpdatedBy,
                                                 UpdatedOn = data.UpdatedOn,
                                                 ActiveFlag = data.ActiveFlag

                                             }).ToList<ClsShippingVendor>();
            return oVend;
        }

        public List<ClsEquipment> GetEquipmentList()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsEquipment> oList = (from data in puroTouchContext.GetTable<tblEquipment>()
                                        //where data.ActiveFlag != false
                                        orderby data.Equipment
                                           select new ClsEquipment
                                        {
                                            idEquipment = Convert.ToInt16(data.idEquipment),
                                            //Equipment = data.Equipment,
                                            Equipment = Convert.ToBoolean(data.ActiveFlag) == false ? data.Equipment + " (Inactive)" : data.Equipment.ToUpper(),
                                            CreatedBy = data.CreatedBy,
                                            CreatedOn = data.CreatedOn,
                                            UpdatedBy = data.UpdatedBy,
                                            UpdatedOn = data.UpdatedOn,
                                            ActiveFlag = data.ActiveFlag

                                        }).ToList<ClsEquipment>();
            return oList;
        }

        public List<ClsInvoiceType> GetInvoiceType()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsInvoiceType> oList = (from data in puroTouchContext.GetTable<tblInvoiceType>()
                                          //where data.ActiveFlag != false
                                          orderby data.InvoiceType
                                          select new ClsInvoiceType
                                        {
                                            idInvoiceType = Convert.ToInt16(data.idInvoiceType),
                                            //InvoiceType = data.InvoiceType,
                                            InvoiceType = Convert.ToBoolean(data.ActiveFlag) == false ? data.InvoiceType + " (Inactive)" : data.InvoiceType.ToUpper(),
                                            CreatedBy = data.CreatedBy,
                                            CreatedOn = data.CreatedOn,
                                            UpdatedBy = data.UpdatedBy,
                                            UpdatedOn = data.UpdatedOn,
                                            ActiveFlag = data.ActiveFlag

                                        }).ToList<ClsInvoiceType>();
            return oList;
        }

        public List<ClsCommunicationMethod> GetCommunicationMethods()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsCommunicationMethod> oList = (from data in puroTouchContext.GetTable<tblCommunicationMethod>()
                                                  //where data.ActiveFlag != false
                                                  orderby data.CommunicationMethod
                                                  select new ClsCommunicationMethod
                                          {
                                              idCommunicationMethod = Convert.ToInt16(data.idCommunicationMethod),
                                              //CommunicationMethod = data.CommunicationMethod,
                                              CommunicationMethod = Convert.ToBoolean(data.ActiveFlag) == false ? data.CommunicationMethod + " (Inactive)" : data.CommunicationMethod.ToUpper(),
                                              CreatedBy = data.CreatedBy,
                                              CreatedOn = data.CreatedOn,
                                              UpdatedBy = data.UpdatedBy,
                                              UpdatedOn = data.UpdatedOn,
                                              ActiveFlag = data.ActiveFlag

                                          }).ToList<ClsCommunicationMethod>();
            return oList;
        }

        public List<ClsFileType> GetFileTypes()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsFileType> oList = (from data in puroTouchContext.GetTable<tblFileType>()
                                       //where data.ActiveFlag != false
                                       orderby data.FileType
                                       select new ClsFileType
                                                  {
                                                      idFileType = Convert.ToInt16(data.idFileType),
                                                      //FileType = data.FileType,
                                                      FileType = Convert.ToBoolean(data.ActiveFlag) == false ? data.FileType + " (Inactive)" : data.FileType.ToUpper(),
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      ActiveFlag = data.ActiveFlag

                                                  }).ToList<ClsFileType>();
            return oList;
        }

        public List<ClsCloseReason> GetCloseReasons()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsCloseReason> oList = (from data in puroTouchContext.GetTable<tblCloseReason>()
                                          //where data.ActiveFlag != false
                                          orderby data.CloseReason
                                       select new ClsCloseReason
                                       {
                                           idCloseReason = Convert.ToInt16(data.idCloseReason),
                                           CloseReason = data.CloseReason,
                                           CreatedBy = data.CreatedBy,
                                           CreatedOn = data.CreatedOn,
                                           UpdatedBy = data.UpdatedBy,
                                           UpdatedOn = data.UpdatedOn,
                                           ActiveFlag = data.ActiveFlag

                                       }).ToList<ClsCloseReason>();
            return oList;
        }

        public List<ClsCloseReason> GetCloseReasonsInactiveNoted()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsCloseReason> oList = (from data in puroTouchContext.GetTable<tblCloseReason>()
                                          //where data.ActiveFlag != false
                                          orderby data.CloseReason
                                          select new ClsCloseReason
                                          {
                                              idCloseReason = Convert.ToInt16(data.idCloseReason),
                                              //CloseReason = data.CloseReason,
                                              CloseReason = Convert.ToBoolean(data.ActiveFlag) == false ? data.CloseReason + " (Inactive)" : data.CloseReason.ToUpper(),
                                              CreatedBy = data.CreatedBy,
                                              CreatedOn = data.CreatedOn,
                                              UpdatedBy = data.UpdatedBy,
                                              UpdatedOn = data.UpdatedOn,
                                              ActiveFlag = data.ActiveFlag

                                          }).ToList<ClsCloseReason>();
            return oList;
        }

        public List<ClsEDISolution> GetEDISolutions()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        List<ClsEDISolution> oList = (from data in puroTouchContext.GetTable<tblEDISolution>()
                                      orderby data.Solution
                                      select new ClsEDISolution
                                      {
                                          idSolution = Convert.ToInt16(data.idSolution),
                                          Solution = Convert.ToBoolean(data.ActiveFlag) == false ? data.Solution + " (Inactive)" : data.Solution.ToUpper(),
                                          CreatedBy = data.CreatedBy,
                                          CreatedOn = data.CreatedOn,
                                          UpdatedBy = data.UpdatedBy,
                                          UpdatedOn = data.UpdatedOn,
                                          ActiveFlag = data.ActiveFlag
                                      }).ToList<ClsEDISolution>();
        return oList;
        }

        public List<ClsInductionPoint> GetInductionPoints()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsInductionPoint> oList = (from data in puroTouchContext.GetTable<tblInductionPoint>()
                                          orderby data.Description
                                          select new ClsInductionPoint
                                          {
                                              idInduction = Convert.ToInt16(data.idInduction),
                                              Description = Convert.ToBoolean(data.ActiveFlag) == false ? data.Description + " (Inactive)" : data.Description.ToUpper(),
                                              Address = data.Address,
                                              City = data.City,
                                              State = data.State,
                                              Zip = data.Zip,
                                              Country = data.Country,
                                              CreatedBy = data.CreatedBy,
                                              CreatedOn = data.CreatedOn,
                                              UpdatedBy = data.UpdatedBy,
                                              UpdatedOn = data.UpdatedOn,
                                              ActiveFlag = data.ActiveFlag,
                                              PuroPostFlag = data.PuroPostFlag

                                          }).ToList<ClsInductionPoint>();
            return oList;
        }

        public ClsInductionPoint GetInductionPointDetails(Int16 idInduction)
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            ClsInductionPoint oList = (from data in puroTouchContext.GetTable<tblInductionPoint>()
                                             where data.idInduction == idInduction
                                             select new ClsInductionPoint
                                             {
                                                 idInduction = Convert.ToInt16(data.idInduction),
                                                 Description = Convert.ToBoolean(data.ActiveFlag) == false ? data.Description + " (Inactive)" : data.Description.ToUpper(),
                                                 Address = data.Address,
                                                 City = data.City,
                                                 State = data.State,
                                                 Zip = data.Zip,
                                                 Country = data.Country,
                                                 CreatedBy = data.CreatedBy,
                                                 CreatedOn = data.CreatedOn,
                                                 UpdatedBy = data.UpdatedBy,
                                                 UpdatedOn = data.UpdatedOn,
                                                 ActiveFlag = data.ActiveFlag,
                                                 PuroPostFlag = data.PuroPostFlag

                                             }).FirstOrDefault();
            return oList;
        }

        public List<ClsInductionPoint> GetInductionPointsPuroPost()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsInductionPoint> oList = (from data in puroTouchContext.GetTable<tblInductionPoint>()
                                             where data.PuroPostFlag == true
                                             orderby data.Description
                                             select new ClsInductionPoint
                                             {
                                                 idInduction = Convert.ToInt16(data.idInduction),
                                                 Description = Convert.ToBoolean(data.ActiveFlag) == false ? data.Description + " (Inactive)" : data.Description.ToUpper(),
                                                 Address = data.Address,
                                                 City = data.City,
                                                 State = data.State,
                                                 Zip = data.Zip,
                                                 Country = data.Country,
                                                 CreatedBy = data.CreatedBy,
                                                 CreatedOn = data.CreatedOn,
                                                 UpdatedBy = data.UpdatedBy,
                                                 UpdatedOn = data.UpdatedOn,
                                                 ActiveFlag = data.ActiveFlag,
                                                 PuroPostFlag = data.PuroPostFlag

                                             }).ToList<ClsInductionPoint>();
            return oList;
        }

        public List<ClsInductionPoint> GetInductionPointsNoPuroPost()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsInductionPoint> oList = (from data in puroTouchContext.GetTable<tblInductionPoint>()
                                             where data.PuroPostFlag == false || data.PuroPostFlag == null
                                             orderby data.Description
                                             select new ClsInductionPoint
                                             {
                                                 idInduction = Convert.ToInt16(data.idInduction),
                                                 Description = Convert.ToBoolean(data.ActiveFlag) == false ? data.Description + " (Inactive)" : data.Description.ToUpper(),
                                                 Address = data.Address,
                                                 City = data.City,
                                                 State = data.State,
                                                 Zip = data.Zip,
                                                 Country = data.Country,
                                                 CreatedBy = data.CreatedBy,
                                                 CreatedOn = data.CreatedOn,
                                                 UpdatedBy = data.UpdatedBy,
                                                 UpdatedOn = data.UpdatedOn,
                                                 ActiveFlag = data.ActiveFlag,
                                                 PuroPostFlag = data.PuroPostFlag

                                             }).ToList<ClsInductionPoint>();
            return oList;
        }

        public List<ClsShippingProducts> GetShippingProducts()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsShippingProducts> oProducts = (from data in puroTouchContext.GetTable<tblShippingProduct>()
                                                   join svc in puroTouchContext.GetTable<tblShippingService>() on data.idShippingSvc equals svc.idShippingSvc
                                                  orderby data.ShippingProduct
                                                   select new ClsShippingProducts
                                                  {
                                                      idShippingProduct = Convert.ToInt16(data.idShippingProduct),
                                                      idShippingSvc = Convert.ToInt16(data.idShippingSvc),
                                                      ShippingProduct = data.ShippingProduct,
                                                      ShippingService = svc.serviceDesc,
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      ActiveFlag = data.ActiveFlag

                                                  }).ToList<ClsShippingProducts>();
            return oProducts;
        }
        public List<ClsShippingProducts> GetShippingProductsInactiveNoted()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsShippingProducts> oProducts = (from data in puroTouchContext.GetTable<tblShippingProduct>()
                                                   join svc in puroTouchContext.GetTable<tblShippingService>() on data.idShippingSvc equals svc.idShippingSvc
                                                   orderby data.ShippingProduct
                                                   select new ClsShippingProducts
                                                   {
                                                       idShippingProduct = Convert.ToInt16(data.idShippingProduct),
                                                       idShippingSvc = Convert.ToInt16(data.idShippingSvc),
                                                       ShippingProduct = Convert.ToBoolean(data.ActiveFlag) == false ? data.ShippingProduct + " (Inactive)" : data.ShippingProduct.ToUpper(),
                                                       ShippingService = svc.serviceDesc,
                                                       CreatedBy = data.CreatedBy,
                                                       CreatedOn = data.CreatedOn,
                                                       UpdatedBy = data.UpdatedBy,
                                                       UpdatedOn = data.UpdatedOn,
                                                       ActiveFlag = data.ActiveFlag
                                                   }).ToList<ClsShippingProducts>();
            return oProducts;
        }

        public DataTable GetShippingProducts(List<ClsDiscoveryRequestSvcs> SvcList)
        {
            string shippingList = "";
            foreach (ClsDiscoveryRequestSvcs ss in SvcList)
            {
                if (shippingList != "") shippingList = shippingList + ",";
                shippingList = shippingList + ss.idShippingSvc.ToString();
            }

            String strConnString = ConfigurationManager.ConnectionStrings["PuroTouchDBSQLConnectionString"].ConnectionString;
            SqlConnection cnn;
            cnn = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("sp_GetProductsForSvcs", cnn);
                cmd.Parameters.Add(new SqlParameter("@SvcList", shippingList));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 10800;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cnn.Close();
            }
            return dt;
        }

        public List<ClsITBA> GetITBAs()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsITBA> oITBA = (from data in puroTouchContext.GetTable<vw_ITBA>()
                                   where data.ActiveFlag != false && data.EDIFlag == false
                                   orderby data.ITBA
                                   select new ClsITBA
                                   {
                                       idITBA = data.idITBA,
                                       ITBAName = data.ITBA,
                                       ITBA = data.ITBA,
                                       ITBAEmail = data.ITBAemail,
                                       CreatedBy = data.CreatedBy,
                                       CreatedOn = data.CreatedOn,
                                       UpdatedBy = data.UpdatedBy,
                                       UpdatedOn = data.UpdatedOn,
                                       ActiveFlag = data.ActiveFlag,
                                       idEmployee = data.idEmployee,
                                       ReceiveNewReqEmail = data.ReceiveNewReqEmail,
                                       login = data.login,
                                       EDIFlag = data.EDIFlag
                                   }).ToList<ClsITBA>();
            return oITBA;
        }
    public List<ClsITBA> GetITBAandEDICombined()
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        List<ClsITBA> oITBA = (from data in puroTouchContext.GetTable<vw_EDIandITBACombined>()
                               where data.ActiveFlag != false
                               orderby data.ITBA
                               select new ClsITBA
                               {
                                   idITBA = data.idITBA,
                                   ITBAName = data.ITBA,
                                   ITBA = data.ITBA,
                                   ITBAEmail = data.ITBAemail,
                                   CreatedBy = data.CreatedBy,
                                   CreatedOn = data.CreatedOn,
                                   UpdatedBy = data.UpdatedBy,
                                   UpdatedOn = data.UpdatedOn,
                                   ActiveFlag = data.ActiveFlag,
                                   idEmployee = data.idEmployee,
                                   ReceiveNewReqEmail = data.ReceiveNewReqEmail,
                                   login = data.login
                               }).ToList<ClsITBA>();
        return oITBA;
    }
    public string GetNewReqEmailList()
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        string emailList = "";
        List<ClsITBA> oITBA = (from data in puroTouchContext.GetTable<tblITBA>()
                               where data.ReceiveNewReqEmail == true
                               where data.ActiveFlag != false
                               orderby data.ITBAemail
                               select new ClsITBA
                               {
                                   ITBAEmail = data.ITBAemail
                               }).ToList<ClsITBA>();

        //put into string
        foreach (ClsITBA itba in oITBA)
        {
            if (emailList != "")
                emailList = emailList + ",";
            emailList = emailList + itba.ITBAEmail;
        }
        return emailList;
    }

    public List<ClsDiscoveryRequest> GetAllDiscoveryRequests()
        {
             //orderby CreatedOn descending
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsDiscoveryRequest> oReq = (from data in puroTouchContext.GetTable<vw_DiscoveryRequestSummary>()
                                              where data.ActiveFlag != false
                                             
                                              select new ClsDiscoveryRequest
                                              {
                                                  idRequest = data.idRequest,
                                                  flagNewRequest = Convert.ToBoolean(data.isNewRequest),
                                                  SalesRepName = data.SalesRepName,
                                                  SalesRepEmail = data.SalesRepEmail,
                                                  idOnboardingPhase = (int?)data.idOnboardingPhase,
                                                  District = data.District,
                                                  CustomerName = data.CustomerName,
                                                  Address = data.Address,
                                                  City = data.City,
                                                  State = data.State,
                                                  Zipcode = data.Zipcode,
                                                  Country = data.Country,
                                                  Commodity = data.Commodity,
                                                  ProjectedRevenue = (decimal)data.ProjectedRevenue,
                                                  CurrentSolution = data.CurrentSolution,
                                                  ProposedCustoms = data.ProposedCustoms,
                                                  CallDate1 = (DateTime?)data.CallDate1,
                                                  CallDate2 = (DateTime?)data.CallDate2,
                                                  CallDate3 = (DateTime?)data.CallDate3,
                                                  UpdatedBy = data.UpdatedBy,
                                                  UpdatedOn = (DateTime?)data.UpdatedOn,
                                                  CreatedBy = data.CreatedBy,
                                                  CreatedOn = (DateTime?)data.CreatedOn,
                                                  ActiveFlag = Convert.ToBoolean(data.ActiveFlag),
                                                  CurrentGoLive = (DateTime?)data.CurrentGoLive,
                                                  TargetGoLive = (DateTime?)data.TargetGoLive,
                                                  ActualGoLive = (DateTime?)data.ActualGoLive,
                                                  OnboardingPhase = data.OnboardingPhase,
                                                  ShippingChannel = data.ShippingChannel,
                                                  ITBA = data.ITBA,
                                                  TotalTimeSpent = data.TotalTimeSpent,
                                                  worldpakFlag = data.worldpakFlag,
                                                  NewRequestYesNo = Convert.ToBoolean(data.isNewRequest) == false ? "No" : "Yes",
                                                  WorldpakYesNo = Convert.ToBoolean(data.worldpakFlag) == false ? "No" : "Yes",
                                                  RequestType = data.RequestType,
                                                  VendorType = data.VendorType,
                                                  VendorName = data.VendorName,
                                                  SolutionType = data.SolutionType,
                                                  EDITargetGoLive = (DateTime?)data.EDITargetGoLive,
                                                  EDICurrentGoLive = (DateTime?)data.EDICurrentGoLive,
                                                  EDIActualGoLive = (DateTime?)data.EDIActualGoLive,
                                                  EDIOnboardingPhaseType = data.EDIOnboardingPhaseType,
                                                  EDISpecialistName = data.EDISpecialistName
                                              }).ToList<ClsDiscoveryRequest>();
            var newoReq = oReq.OrderByDescending(x => x.CreatedOn).ToList();
            return newoReq;
        }

        public List<ClsDiscoveryRequest> GetAllDiscoveryRequests(string itbaLogin)
        {
            //orderby CreatedOn descending
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsDiscoveryRequest> oReq = (from data in puroTouchContext.GetTable<vw_DiscoveryRequestSummary>()                                             
                                              where data.ActiveFlag != false
                                              where data.ActiveDirectoryName == itbaLogin || data.EDISpecActiveDir == itbaLogin

                                              select new ClsDiscoveryRequest
                                              {
                                                  idRequest = data.idRequest,
                                                  flagNewRequest = Convert.ToBoolean(data.isNewRequest),
                                                  SalesRepName = data.SalesRepName,
                                                  SalesRepEmail = data.SalesRepEmail,
                                                  idOnboardingPhase = (int?)data.idOnboardingPhase,
                                                  District = data.District,
                                                  CustomerName = data.CustomerName,
                                                  Address = data.Address,
                                                  City = data.City,
                                                  State = data.State,
                                                  Zipcode = data.Zipcode,
                                                  Country = data.Country,
                                                  Commodity = data.Commodity,
                                                  ProjectedRevenue = (decimal)data.ProjectedRevenue,
                                                  CurrentSolution = data.CurrentSolution,
                                                  ProposedCustoms = data.ProposedCustoms,
                                                  CallDate1 = (DateTime?)data.CallDate1,
                                                  CallDate2 = (DateTime?)data.CallDate2,
                                                  CallDate3 = (DateTime?)data.CallDate3,
                                                  UpdatedBy = data.UpdatedBy,
                                                  UpdatedOn = (DateTime?)data.UpdatedOn,
                                                  CreatedBy = data.CreatedBy,
                                                  CreatedOn = (DateTime?)data.CreatedOn,
                                                  ActiveFlag = Convert.ToBoolean(data.ActiveFlag),
                                                  idITBA = (int?)data.idITBA,
                                                  TargetGoLive = (DateTime?)data.TargetGoLive,
                                                  CurrentGoLive = (DateTime?)data.CurrentGoLive,
                                                  ActualGoLive = (DateTime?)data.ActualGoLive,
                                                  OnboardingPhase = data.OnboardingPhase,
                                                  ShippingChannel = data.ShippingChannel,
                                                  ITBA = data.ITBA,
                                                  TotalTimeSpent = data.TotalTimeSpent,
                                                  NewRequestYesNo = Convert.ToBoolean(data.isNewRequest) == false ? "No" : "Yes",
                                                  WorldpakYesNo = Convert.ToBoolean(data.worldpakFlag) == false ? "No" : "Yes",
                                                  RequestType = data.RequestType,
                                                  VendorType = data.VendorType,
                                                  VendorName = data.VendorName,
                                                  SolutionType = data.SolutionType,
                                                  EDITargetGoLive = (DateTime?)data.EDITargetGoLive,
                                                  EDICurrentGoLive = (DateTime?)data.EDICurrentGoLive,
                                                  EDIActualGoLive = (DateTime?)data.EDIActualGoLive,
                                                  EDIOnboardingPhaseType = data.EDIOnboardingPhaseType,
                                                  EDISpecialistName = data.EDISpecialistName

                                              }).ToList<ClsDiscoveryRequest>();
            var newoReq = oReq.OrderByDescending(x => x.CreatedOn).ToList();
            return newoReq;
        }

        public List<ClsDiscoveryRequest> GetUnassignedDiscoveryRequests()
        {
            //orderby CreatedOn descending
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsDiscoveryRequest> oReq = (from data in puroTouchContext.GetTable<vw_DiscoveryRequestSummary>()
                                              where data.ActiveFlag != false
                                              where data.idITBA == null

                                              select new ClsDiscoveryRequest
                                              {
                                                  idRequest = data.idRequest,
                                                  flagNewRequest = Convert.ToBoolean(data.isNewRequest),
                                                  SalesRepName = data.SalesRepName,
                                                  SalesRepEmail = data.SalesRepEmail,
                                                  idOnboardingPhase = (int?)data.idOnboardingPhase,
                                                  District = data.District,
                                                  CustomerName = data.CustomerName,
                                                  Address = data.Address,
                                                  City = data.City,
                                                  State = data.State,
                                                  Zipcode = data.Zipcode,
                                                  Country = data.Country,
                                                  Commodity = data.Commodity,
                                                  ProjectedRevenue = (decimal)data.ProjectedRevenue,
                                                  CurrentSolution = data.CurrentSolution,
                                                  ProposedCustoms = data.ProposedCustoms,
                                                  CallDate1 = (DateTime?)data.CallDate1,
                                                  CallDate2 = (DateTime?)data.CallDate2,
                                                  CallDate3 = (DateTime?)data.CallDate3,
                                                  UpdatedBy = data.UpdatedBy,
                                                  UpdatedOn = (DateTime?)data.UpdatedOn,
                                                  CreatedBy = data.CreatedBy,
                                                  CreatedOn = (DateTime?)data.CreatedOn,
                                                  ActiveFlag = Convert.ToBoolean(data.ActiveFlag),
                                                  idITBA = (int?)data.idITBA,
                                                  TargetGoLive = (DateTime?)data.TargetGoLive,
                                                  ActualGoLive = (DateTime?)data.ActualGoLive,
                                                  CurrentGoLive = (DateTime?)data.CurrentGoLive,
                                                  OnboardingPhase = data.OnboardingPhase,
                                                  ShippingChannel = data.ShippingChannel,
                                                  ITBA = data.ITBA,
                                                  TotalTimeSpent = data.TotalTimeSpent,
                                                  NewRequestYesNo = Convert.ToBoolean(data.isNewRequest) == false ? "No" : "Yes",
                                                  WorldpakYesNo = Convert.ToBoolean(data.worldpakFlag) == false ? "No" : "Yes",
                                                  RequestType = data.RequestType,
                                                  SolutionType = data.SolutionType,
                                                  VendorType = data.VendorType,
                                                  VendorName = data.VendorName,
                                                  EDITargetGoLive = (DateTime?)data.EDITargetGoLive,
                                                  EDICurrentGoLive = (DateTime?)data.EDICurrentGoLive,
                                                  EDIActualGoLive = (DateTime?)data.EDIActualGoLive,
                                                  EDIOnboardingPhaseType = data.EDIOnboardingPhaseType,
                                                  EDISpecialistName = data.EDISpecialistName
                                              }).ToList<ClsDiscoveryRequest>();
            var newoReq = oReq.OrderByDescending(x => x.CreatedOn).ToList();
            return newoReq;
        }

        public List<ClsDiscoveryRequest> GetAllDiscoveryRequestsForSP(string spLogin)
        {
            //orderby CreatedOn descending
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsDiscoveryRequest> oReq = (from data in puroTouchContext.GetTable<vw_DiscoveryRequestSummary>()
                                              where data.ActiveFlag != false
                                              where data.CreatedBy == spLogin

                                              select new ClsDiscoveryRequest
                                              {
                                                  idRequest = data.idRequest,
                                                  flagNewRequest = Convert.ToBoolean(data.isNewRequest),
                                                  SalesRepName = data.SalesRepName,
                                                  SalesRepEmail = data.SalesRepEmail,
                                                  idOnboardingPhase = (int?)data.idOnboardingPhase,
                                                  District = data.District,
                                                  CustomerName = data.CustomerName,
                                                  Address = data.Address,
                                                  City = data.City,
                                                  State = data.State,
                                                  Zipcode = data.Zipcode,
                                                  Country = data.Country,
                                                  Commodity = data.Commodity,
                                                  ProjectedRevenue = (decimal)data.ProjectedRevenue,
                                                  CurrentSolution = data.CurrentSolution,
                                                  ProposedCustoms = data.ProposedCustoms,
                                                  CallDate1 = (DateTime?)data.CallDate1,
                                                  CallDate2 = (DateTime?)data.CallDate2,
                                                  CallDate3 = (DateTime?)data.CallDate3,
                                                  UpdatedBy = data.UpdatedBy,
                                                  UpdatedOn = (DateTime?)data.UpdatedOn,
                                                  CreatedBy = data.CreatedBy,
                                                  CreatedOn = (DateTime?)data.CreatedOn,
                                                  ActiveFlag = Convert.ToBoolean(data.ActiveFlag),
                                                  idITBA = (int?)data.idITBA,
                                                  TargetGoLive = (DateTime?)data.TargetGoLive,
                                                  ActualGoLive = (DateTime?)data.ActualGoLive,
                                                  CurrentGoLive = (DateTime?)data.CurrentGoLive,
                                                  OnboardingPhase = data.OnboardingPhase,
                                                  ShippingChannel = data.ShippingChannel,
                                                  ITBA = data.ITBA,
                                                  TotalTimeSpent = data.TotalTimeSpent,
                                                  NewRequestYesNo = Convert.ToBoolean(data.isNewRequest) == false ? "No" : "Yes",
                                                  WorldpakYesNo = Convert.ToBoolean(data.worldpakFlag) == false ? "No" : "Yes",
                                                  RequestType = data.RequestType,
                                                  VendorType = data.VendorType,
                                                  VendorName = data.VendorName,
                                                  SolutionType = data.SolutionType,
                                                  EDITargetGoLive = (DateTime?)data.EDITargetGoLive,
                                                  EDICurrentGoLive = (DateTime?)data.EDICurrentGoLive,
                                                  EDIActualGoLive = (DateTime?)data.EDIActualGoLive,
                                                  EDIOnboardingPhaseType = data.EDIOnboardingPhaseType,
                                                  EDISpecialistName = data.EDISpecialistName
                                              }).ToList<ClsDiscoveryRequest>();
            var newoReq = oReq.OrderByDescending(x => x.CreatedOn).ToList();
            return newoReq;
        }

        public List<ClsDiscoveryRequest> GetAllDiscoveryRequestsForDistrict(string district)
        {
            //orderby CreatedOn descending
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsDiscoveryRequest> oReq = (from data in puroTouchContext.GetTable<vw_DiscoveryRequestSummary>()
                                              where data.ActiveFlag != false
                                              where data.District == district

                                              select new ClsDiscoveryRequest
                                              {
                                                  idRequest = data.idRequest,
                                                  flagNewRequest = Convert.ToBoolean(data.isNewRequest),
                                                  SalesRepName = data.SalesRepName,
                                                  SalesRepEmail = data.SalesRepEmail,
                                                  idOnboardingPhase = (int?)data.idOnboardingPhase,
                                                  District = data.District,
                                                  CustomerName = data.CustomerName,
                                                  Address = data.Address,
                                                  City = data.City,
                                                  State = data.State,
                                                  Zipcode = data.Zipcode,
                                                  Country = data.Country,
                                                  Commodity = data.Commodity,
                                                  ProjectedRevenue = (decimal)data.ProjectedRevenue,
                                                  CurrentSolution = data.CurrentSolution,
                                                  ProposedCustoms = data.ProposedCustoms,
                                                  CallDate1 = (DateTime?)data.CallDate1,
                                                  CallDate2 = (DateTime?)data.CallDate2,
                                                  CallDate3 = (DateTime?)data.CallDate3,
                                                  UpdatedBy = data.UpdatedBy,
                                                  UpdatedOn = (DateTime?)data.UpdatedOn,
                                                  CreatedBy = data.CreatedBy,
                                                  CreatedOn = (DateTime?)data.CreatedOn,
                                                  ActiveFlag = Convert.ToBoolean(data.ActiveFlag),
                                                  idITBA = (int?)data.idITBA,
                                                  TargetGoLive = (DateTime?)data.TargetGoLive,
                                                  ActualGoLive = (DateTime?)data.ActualGoLive,
                                                  OnboardingPhase = data.OnboardingPhase,
                                                  ShippingChannel = data.ShippingChannel,
                                                  ITBA = data.ITBA,
                                                  TotalTimeSpent = data.TotalTimeSpent,
                                                  NewRequestYesNo = Convert.ToBoolean(data.isNewRequest) == false ? "No" : "Yes",
                                                  WorldpakYesNo = Convert.ToBoolean(data.worldpakFlag) == false ? "No" : "Yes",
                                                  RequestType = data.RequestType,
                                                  VendorType = data.VendorType,
                                                  VendorName = data.VendorName,
                                                  SolutionType = data.SolutionType,
                                                  EDITargetGoLive = (DateTime?)data.EDITargetGoLive,
                                                  EDICurrentGoLive = (DateTime?)data.EDICurrentGoLive,
                                                  EDIActualGoLive = (DateTime?)data.EDIActualGoLive,
                                                  EDIOnboardingPhaseType = data.EDIOnboardingPhaseType,
                                                  EDISpecialistName = data.EDISpecialistName
                                              }).ToList<ClsDiscoveryRequest>();
            var newoReq = oReq.OrderByDescending(x => x.CreatedOn).ToList();
            return newoReq;
        }




        public List<ClsAppUsers> GetListClsAppUsers(int idPIApplication)
        {
            PurolatorReportingSQLDataContext prContext = new PurolatorReportingSQLDataContext();
            List<ClsAppUsers> oAppUserlist = (from data in prContext.GetTable<vw_PI_ApplicationUser>()
                                              where data.idPI_Application == idPIApplication
                                              orderby data.UserName
                                              select new ClsAppUsers
                                              {
                                                  UserName = data.UserName,
                                                  ActiveDirectoryName = data.ActiveDirectoryName,
                                                  idPI_ApplicationUserRole = data.idPI_ApplicationUserRole,
                                                  idPI_ApplicationUser = data.idPI_ApplicationUser,
                                                  RoleName = data.RoleName,
                                                  role_UpdatedBy = data.role_UpdatedBy,
                                                  role_UpdatedOn = data.role_UpdatedOn,
                                                  idEmployee = data.idEmployee,
                                                  idPI_ApplicationRole = data.idPI_ApplicationRole
                                              }).ToList();
            return oAppUserlist;
        }

        public List<ClsAppUsers> GetListClsAppUsers(string appName)
        {
            PurolatorReportingSQLDataContext prContext = new PurolatorReportingSQLDataContext();
            List<ClsAppUsers> oAppUserlist = (from data in prContext.GetTable<vw_PI_ApplicationUser>()
                                              where data.ApplicationName == appName
                                              orderby data.UserName
                                              select new ClsAppUsers
                                              {
                                                  UserName = data.UserName,
                                                  ActiveDirectoryName = data.ActiveDirectoryName,
                                                  idPI_ApplicationUserRole = data.idPI_ApplicationUserRole,
                                                  idPI_ApplicationUser = data.idPI_ApplicationUser,
                                                  RoleName = data.RoleName,
                                                  role_UpdatedBy = data.role_UpdatedBy,
                                                  role_UpdatedOn = data.role_UpdatedOn,
                                                  idEmployee = data.idEmployee,
                                                  idPI_ApplicationRole = data.idPI_ApplicationRole

                                              }).ToList();
            return oAppUserlist;
        }

     
        public List<ClsEmployee> GetListClsEmployees()
        {
            PurolatorReportingSQLDataContext prContext = new PurolatorReportingSQLDataContext();
            List<ClsEmployee> oEmployeelist = (from data in prContext.GetTable<tblEmployee>()
                                               orderby data.FirstName
                                               select new ClsEmployee
                                               {
                                                   idEmployee = data.idEmployee,
                                                   FirstName = data.FirstName,
                                                   LastName = data.LastName,
                                                   UserName = data.FirstName + " " + data.LastName
                                               }).ToList();
            return oEmployeelist;
        }

        public List<ClsApp> GetListClsApps()
        {
            PurolatorReportingSQLDataContext prContext = new PurolatorReportingSQLDataContext();
            List<ClsApp> oApplist = (from data in prContext.GetTable<tblPI_Application>()
                                     orderby data.ApplicationName
                                     select new ClsApp
                                     {
                                         idPIApplication = data.idPI_Application,
                                         AppName = data.ApplicationName
                                     }).ToList();
            return oApplist;
        }

        public List<ClsDiscoveryRequestSvcs> GetProposedServices(int idRequest)
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsDiscoveryRequestSvcs> oSvcs = (from data in puroTouchContext.GetTable<tblDiscoveryRequestService>()
                                                   join svc in puroTouchContext.GetTable<tblShippingService>() on data.idShippingSvc equals svc.idShippingSvc
                                                   where data.idRequest == idRequest
                                                   orderby svc.serviceDesc
                                                   select new ClsDiscoveryRequestSvcs
                                                   {
                                                       idRequestSvc = data.idRequestSvc,
                                                       idShippingSvc = data.idShippingSvc,
                                                       serviceDesc = svc.serviceDesc,
                                                       volume = (Int32)data.volume,
                                                       CreatedBy = data.CreatedBy,
                                                       CreatedOn = data.CreatedOn,
                                                       UpdatedBy = data.UpdatedBy,
                                                       UpdatedOn = data.UpdatedOn
                                                   }).ToList<ClsDiscoveryRequestSvcs>();
            return oSvcs;
        }

        public List<ClsDiscoveryRequestProds> GetProposedProducts(int idRequest)
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsDiscoveryRequestProds> oProds = (from data in puroTouchContext.GetTable<tblDiscoveryRequestProduct>()
                                                   join prod in puroTouchContext.GetTable<tblShippingProduct>() on data.idShippingProduct equals prod.idShippingProduct
                                                   where data.idRequest == idRequest
                                                   orderby prod.ShippingProduct
                                                    select new ClsDiscoveryRequestProds
                                                   {
                                                       idDRProduct = data.idDRProduct,
                                                       idShippingProduct = data.idShippingProduct,
                                                       productDesc = prod.ShippingProduct,
                                                       CreatedBy = data.CreatedBy,
                                                       CreatedOn = data.CreatedOn,
                                                       UpdatedBy = data.UpdatedBy,
                                                       UpdatedOn = data.UpdatedOn
                                                   }).ToList<ClsDiscoveryRequestProds>();
            return oProds;
        }

        public List<ClsDiscoveryRequestEquip> GetProposedEquipment(int idRequest)
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsDiscoveryRequestEquip> oEquip = (from data in puroTouchContext.GetTable<tblDiscoveryRequestEquipment>()
                                                     where data.idRequest == idRequest
                                                     orderby data.EquipmentDesc
                                                     select new ClsDiscoveryRequestEquip
                                                     {
                                                         idDREquipment = data.idDREquipment,
                                                         EquipmentDesc = data.EquipmentDesc,
                                                         number = (Int32)data.number,
                                                         CreatedBy = data.CreatedBy,
                                                         CreatedOn = data.CreatedOn,
                                                         UpdatedBy = data.UpdatedBy,
                                                         UpdatedOn = data.UpdatedOn
                                                     }).ToList<ClsDiscoveryRequestEquip>();
            return oEquip;
        }

        public List<ClsDiscoveryRequestEDI> GetProposedEDI(int idRequest)
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsDiscoveryRequestEDI> oEDI = (from data in puroTouchContext.GetTable<tblDiscoveryRequestEDI>()
                                                     where data.idRequest == idRequest
                                                     orderby data.Solution
                                                     select new ClsDiscoveryRequestEDI
                                                     {
                                                         idDREDI = data.idDREDI,
                                                         Solution = data.Solution,
                                                         FileFormat = data.FileFormat,
                                                         CommunicationMethod = data.CommunicationMethod,
                                                         CreatedBy = data.CreatedBy,
                                                         CreatedOn = data.CreatedOn,
                                                         UpdatedBy = data.UpdatedBy,
                                                         UpdatedOn = data.UpdatedOn
                                                     }).ToList<ClsDiscoveryRequestEDI>();
            return oEDI;
        }

        public List<ClsCurrency> GetCurrency()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsCurrency> oCurrency = (from data in puroTouchContext.GetTable<vw_Currency>()
                                           orderby data.CurrencyCode
                                           select new ClsCurrency
                                           {
                                               Currency = data.CurrencyCode

                                           }).ToList<ClsCurrency>();
            return oCurrency;
        }
        public List<ClsDistrict> GetDistricts()
        {
            //string excld = "Canada";
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsDistrict> oDistrict = (from data in puroTouchContext.GetTable<vw_District>()
                                           //where data.District != excld
                                           orderby data.District
                                           select new ClsDistrict
                                           {
                                               District = data.District.Trim()

                                           }).ToList<ClsDistrict>();
            return oDistrict;
        }

        public List<ClsRegions> GetAllRegions()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsRegions> oRegion = (from data in puroTouchContext.GetTable<vw_Region>()                                        
                                        orderby data.Airport
                                         select new ClsRegions
                                           {
                                               Region = data.Airport

                                           }).ToList<ClsRegions>();
            return oRegion;
        }

        public List<ClsRegions> GetRegionsForDistrict(string district)
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsRegions> oRegion = (from data in puroTouchContext.GetTable<vw_Region>()
                                        where data.District == district
                                        orderby data.Airport
                                        select new ClsRegions
                                        {
                                            Region = data.Airport

                                        }).ToList<ClsRegions>();
            return oRegion;
        }

        public List<ClsNotes> GetNotes(int DRid)
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsNotes> oNote = (from data in puroTouchContext.GetTable<tblDiscoveryRequestNote>()
                                    join tt in puroTouchContext.GetTable<tblTaskType>() on data.idTaskType equals tt.idTaskType
                                    where data.idRequest == DRid
                                    where data.ActiveFlag != false
                                    orderby data.noteDate descending
                                    orderby data.idNote descending
                                    select new ClsNotes
                                    {
                                        idNote = data.idNote,
                                        idRequest = data.idRequest,
                                        idTaskType = data.idTaskType,
                                        TaskType = tt.TaskType,
                                        noteDate = data.noteDate,
                                        timeSpent = data.timeSpent,
                                        publicNote = data.publicNote,
                                        privateNote = data.privateNote,
                                        CreatedBy = data.CreatedBy

                                    }).ToList<ClsNotes>();
            return oNote;
        }

        public List<ClsFileUpload> GetFileList(int DRid)
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsFileUpload> oFiles = (from data in puroTouchContext.GetTable<tblDiscoveryRequestUpload>()
                                    where data.idRequest == DRid
                                    where data.ActiveFlag != false
                                    orderby data.UploadDate descending
                                    orderby data.idFileUpload descending
                                    select new ClsFileUpload
                                    {
                                        idFileUpload = data.idFileUpload,
                                        idRequest = data.idRequest,
                                        UploadDate = data.UploadDate,
                                        Description = data.Description,
                                        FilePath = data.FilePath,
                                        CreatedBy = data.CreatedBy,
                                        CreatedOn = (DateTime)data.CreatedOn,
                                        UpdatedBy = data.UpdatedBy,
                                        UpdatedOn = data.UpdatedOn,
                                        ActiveFlag = (bool)data.ActiveFlag

                                    }).ToList<ClsFileUpload>();
            return oFiles;
        }
    public List<ClsFileUpload> GetFileList(int DRid, string strDir)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<ClsFileUpload> qFiles = new List<ClsFileUpload>();
        try
        {
            qFiles = o.GetTable<tblDiscoveryRequestUpload>()
                                    .Where(d => d.idRequest == DRid && d.ActiveFlag != false && d.FilePath.Contains(strDir))
                                    .OrderByDescending(f => f.UpdatedBy)
                                    .ThenByDescending(f => f.idFileUpload)
                                    .Select(data => new ClsFileUpload() { idFileUpload = data.idFileUpload, idRequest = data.idRequest, UploadDate = data.UploadDate, Description = data.Description, FilePath = data.FilePath, CreatedBy = data.CreatedBy, CreatedOn = (DateTime)data.CreatedOn, UpdatedBy = data.UpdatedBy, UpdatedOn = data.UpdatedOn, ActiveFlag = (bool)data.ActiveFlag }).ToList();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now };
            SrvExceptionLogging.Insert(error, out lnewID);
        }

        return qFiles;
    }
    public int GetTotalTimeSpent(int requestID)
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsNotes> oNote = (from data in puroTouchContext.GetTable<tblDiscoveryRequestNote>()
                                    where data.idRequest == requestID
                                    where data.ActiveFlag != false
                                    select new ClsNotes
                                    {
                                        timeSpent = data.timeSpent,

                                    }).ToList<ClsNotes>();
            int sum = 0;
            foreach (ClsNotes noteRow in oNote)
            {
                sum = sum + (int)noteRow.timeSpent;
            }
            return sum;
        }
        public List<ClsOnboardingPhase> GetOnboardingPhasesOnlyActive()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsOnboardingPhase> oPhase = (from data in puroTouchContext.GetTable<tblOnboardingPhase>()
                                               where data.ActiveFlag != false
                                               orderby data.SortValue,data.OnboardingPhase
                                               select new ClsOnboardingPhase
                                               {
                                                   idOnboardingPhase = data.idOnboardingPhase,
                                                   OnboardingPhase = data.OnboardingPhase,                                                  
                                                    CreatedOn = data.CreatedOn,
                                                    UpdatedBy = data.UpdatedBy,
                                                    UpdatedOn = data.UpdatedOn,
                                                    ActiveFlag = data.ActiveFlag,
                                                    SortValue = data.SortValue

                                               }).ToList<ClsOnboardingPhase>();
            return oPhase;
        }
    public List<ClsOnboardingPhase> GetOnboardingPhasesAll()
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        List<ClsOnboardingPhase> oPhase = (from data in puroTouchContext.GetTable<tblOnboardingPhase>()
                                           orderby data.SortValue, data.OnboardingPhase
                                           select new ClsOnboardingPhase
                                           {
                                               idOnboardingPhase = data.idOnboardingPhase,
                                               OnboardingPhase = data.OnboardingPhase,
                                               CreatedOn = data.CreatedOn,
                                               UpdatedBy = data.UpdatedBy,
                                               UpdatedOn = data.UpdatedOn,
                                               ActiveFlag = data.ActiveFlag,
                                               SortValue = data.SortValue

                                           }).ToList<ClsOnboardingPhase>();
        return oPhase;
    }
    public List<ClsOnboardingPhase> GetOnboardingPhasesInactiveNoted()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsOnboardingPhase> oPhase = (from data in puroTouchContext.GetTable<tblOnboardingPhase>()
                                               //where data.ActiveFlag != false
                                               orderby data.SortValue, data.OnboardingPhase
                                               select new ClsOnboardingPhase
                                               {
                                                   idOnboardingPhase = data.idOnboardingPhase,
                                                   //OnboardingPhase = data.OnboardingPhase,
                                                   OnboardingPhase = Convert.ToBoolean(data.ActiveFlag) == false ? data.OnboardingPhase + " (Inactive)" : data.OnboardingPhase.ToUpper(),
                                                   CreatedOn = data.CreatedOn,
                                                   UpdatedBy = data.UpdatedBy,
                                                   UpdatedOn = data.UpdatedOn,
                                                   ActiveFlag = data.ActiveFlag,
                                                   SortValue = data.SortValue

                                               }).ToList<ClsOnboardingPhase>();
            return oPhase;
        }

        public List<ClsRelationshipName> GetRelationships()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsRelationshipName> oRelationship = (from data in puroTouchContext.GetTable<vw_RelationshipName>()
                                                       where data.RelationshipName != ""
                                                       orderby data.RelationshipName
                                                       select new ClsRelationshipName
                                                       {
                                                           RelationshipName = data.RelationshipName

                                                       }).ToList<ClsRelationshipName>();
            return oRelationship;
        }
        public List<ClsShippingChannel> GetShippingChannels()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsShippingChannel> oShipping = (from data in puroTouchContext.GetTable<tblShippingChannel>()
                                                  //where data.ActiveFlag != false
                                                  orderby data.ShippingChannel
                                                  select new ClsShippingChannel
                                                  {
                                                      idShippingChannel = data.idShippingChannel,
                                                      ShippingChannel = data.ShippingChannel,                                                     
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      ActiveFlag = data.ActiveFlag

                                                  }).ToList<ClsShippingChannel>();
            return oShipping;
        }

        public List<ClsShippingChannel> GetShippingChannelsInactiveNoted()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsShippingChannel> oShipping = (from data in puroTouchContext.GetTable<tblShippingChannel>()
                                                  //where data.ActiveFlag != false
                                                  orderby data.ShippingChannel
                                                  select new ClsShippingChannel
                                                  {
                                                      idShippingChannel = data.idShippingChannel,
                                                      //ShippingChannel = data.ShippingChannel,
                                                      ShippingChannel = Convert.ToBoolean(data.ActiveFlag) == false ? data.ShippingChannel + " (Inactive)" : data.ShippingChannel.ToUpper(),
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      ActiveFlag = data.ActiveFlag

                                                  }).ToList<ClsShippingChannel>();
            return oShipping;
        }
        public List<ClsShippingService> GetServices()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsShippingService> oSvcs = (from data in puroTouchContext.GetTable<tblShippingService>()
                                             // where data.ActiveFlag != false
                                              orderby data.serviceDesc
                                              select new ClsShippingService
                                              {
                                                  idShippingSvc = data.idShippingSvc,
                                                  serviceDesc = data.serviceDesc,         
                                                  CreatedBy = data.CreatedBy,
                                                  CreatedOn = data.CreatedOn,
                                                  UpdatedBy = data.UpdatedBy,
                                                  UpdatedOn = data.UpdatedOn,
                                                  ActiveFlag = data.ActiveFlag

                                              }).ToList<ClsShippingService>();
            return oSvcs;
        }

        public List<ClsShippingService> GetServicesInactiveNoted()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsShippingService> oSvcs = (from data in puroTouchContext.GetTable<tblShippingService>()
                                              // where data.ActiveFlag != false
                                              orderby data.serviceDesc
                                              select new ClsShippingService
                                              {
                                                  idShippingSvc = data.idShippingSvc,
                                                  //serviceDesc = data.serviceDesc,
                                                  serviceDesc = Convert.ToBoolean(data.ActiveFlag) == false ? data.serviceDesc + " (Inactive)" : data.serviceDesc.ToUpper(),
                                                  CreatedBy = data.CreatedBy,
                                                  CreatedOn = data.CreatedOn,
                                                  UpdatedBy = data.UpdatedBy,
                                                  UpdatedOn = data.UpdatedOn,
                                                  ActiveFlag = data.ActiveFlag

                                              }).ToList<ClsShippingService>();
            return oSvcs;
        }
       
        public ClsZipCode GetZipInfo(string zipcode)
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            ClsZipCode oReq = (from data in puroTouchContext.GetTable<ZIPCode>()
                               where data.ZipCode1 == zipcode
                               select new ClsZipCode
                               {
                                   City = data.City,
                                   State = data.State,
                                   Country = "US"
                               }).FirstOrDefault();
            //hard coding US for now
            return oReq;
        }

        public List<ClsCustomsType> GetListCustomsTypes()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsCustomsType> oCustomslist = (from data in puroTouchContext.GetTable<tblCustomsType>()
                                                  //where data.ActiveFlag != false
                                                  orderby data.CustomsType
                                                  select new ClsCustomsType
                                     {
                                         idCustomsType = data.idCustomsType,
                                         CustomsType = data.CustomsType,
                                         CreatedBy = data.CreatedBy,
                                         CreatedOn = data.CreatedOn,
                                         UpdatedBy = data.UpdatedBy,
                                         UpdatedOn = data.UpdatedOn,
                                         ActiveFlag = data.ActiveFlag
                                     }).ToList();
            return oCustomslist;
        }

        public List<ClsCustomsType> GetListCustomsTypesInactiveNoted()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsCustomsType> oCustomslist = (from data in puroTouchContext.GetTable<tblCustomsType>()
                                                 //where data.ActiveFlag != false
                                                 orderby data.CustomsType
                                                 select new ClsCustomsType
                                                 {
                                                     idCustomsType = data.idCustomsType,
                                                     CustomsType = Convert.ToBoolean(data.ActiveFlag) == false ? data.CustomsType + " (Inactive)" : data.CustomsType.ToUpper(),
                                                     CreatedBy = data.CreatedBy,
                                                     CreatedOn = data.CreatedOn,
                                                     UpdatedBy = data.UpdatedBy,
                                                     UpdatedOn = data.UpdatedOn,
                                                     ActiveFlag = data.ActiveFlag
                                                 }).ToList();
            return oCustomslist;
        }

    public List<ClsSolutionType> GetSolutionTypes()
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        List<ClsSolutionType> otypes = (from data in puroTouchContext.GetTable<tblSolutionType>()
                                       where data.ActiveFlag != false
                                       orderby data.idSolutionType
                                       select new ClsSolutionType
                                       {
                                           idSolutionType = data.idSolutionType,
                                           SolutionType = data.SolutionType,
                                           CreatedBy = data.CreatedBy,
                                           CreatedOn = data.CreatedOn,
                                           UpdatedBy = data.UpdatedBy,
                                           UpdatedOn = data.UpdatedOn,
                                           ActiveFlag = data.ActiveFlag

                                       }).ToList<ClsSolutionType>();
        return otypes;
    }

    public List<ClsContactType> GetContactTypes()
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        List<ClsContactType> otypes = (from data in puroTouchContext.GetTable<tblContactType>()
                                        where data.ActiveFlag != false
                                        orderby data.idContactType
                                        select new ClsContactType
                                        {
                                            idContactType = data.idContactType,
                                            ContactType = data.ContactType,
                                            CreatedBy = data.CreatedBy,
                                            CreatedOn = data.CreatedOn,
                                            UpdatedBy = data.UpdatedBy,
                                            UpdatedOn = data.UpdatedOn,
                                            ActiveFlag = data.ActiveFlag

                                        }).ToList<ClsContactType>();
        return otypes;
    }
    public List<clsContact> GetContacts(int idRequest)
    {
        List<clsContact> oEquip = SrvContact.GetContactsByRequestID(idRequest);
        return oEquip;
    }
    public List<ClsRequestType> GetRequestTypes()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<ClsRequestType> otypes = (from data in puroTouchContext.GetTable<tblRequestType>()
                                                  where data.ActiveFlag != false
                                                  orderby data.RequestType
                                                 select new ClsRequestType
                                                  {
                                                      idRequestType = data.idRequestType,
                                                      RequestType = data.RequestType,
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      ActiveFlag = data.ActiveFlag

                                                  }).ToList<ClsRequestType>();
            return otypes;
        }


        //GET LIST OF CUSTOMERS FOR AN ITBA
        public List<string> getCustomerList(Int16 idITBA)
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<string> customerList = new List<string>();
            try
            {
                var query =
                       from data in puroTouchContext.GetTable<tblDiscoveryRequest>()
                       where data.idITBA == idITBA
                       where data.ActiveFlag != false
                       orderby data.CustomerName
                       select data;
                foreach (tblDiscoveryRequest row in query)
                {
                    customerList.Add(row.CustomerName);
                }
            }
            catch (Exception ex)
            {

            }

            return customerList;
        }

        //GET LIST OF CUSTOMERS FOR AN ITBA
        public List<string> getCustomerListAll()
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
            List<string> customerList = new List<string>();
            try
            {
                var query =
                       from data in puroTouchContext.GetTable<tblDiscoveryRequest>()
                       where data.ActiveFlag != false
                       orderby data.CustomerName
                       select data;
                foreach (tblDiscoveryRequest row in query)
                {
                    customerList.Add(row.CustomerName);
                }
            }
            catch (Exception ex)
            {

            }

            return customerList;
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    puroTouchContext.Dispose();
                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
