using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsTaskType
/// </summary>
/// 


    public class ClsTaskType
    {
        public int idTaskType { get; set; }
        public string TaskType { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? ActiveFlag { get; set; }
        public int idOnboardingPhase { get; set; }
        public string OnboardingPhase { get; set; }

        public string InsertTaskType(ClsTaskType data)
        {
            string errMsg = "";
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
           
            try
            {

                tblTaskType oNewRow = new tblTaskType()
                {
                    //idTaskType = (Int32)data.idTaskType,
                    TaskType = data.TaskType,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = (DateTime?)data.CreatedOn,
                    //UpdatedBy = data.UpdatedBy,
                    //UpdatedOn = (DateTime?)data.UpdatedOn,
                    idOnboardingPhase = data.idOnboardingPhase,
                    ActiveFlag = data.ActiveFlag
                };



                puroTouchContext.GetTable<tblTaskType>().InsertOnSubmit(oNewRow);
                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();
                //newID = oNewRow.idTaskType;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public string UpdateTaskType(ClsTaskType data)
        {
            string errMsg = "";
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

            try
            {

                if (data.idTaskType > 0)
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in puroTouchContext.GetTable<tblTaskType>()
                        where qdata.idTaskType == data.idTaskType
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblTaskType updRow in query)
                    {

                        updRow.TaskType = data.TaskType;
                        updRow.ActiveFlag = data.ActiveFlag;
                        updRow.idTaskType = data.idTaskType;
                        updRow.UpdatedBy = data.UpdatedBy;
                        updRow.UpdatedOn = data.UpdatedOn;
                        updRow.idOnboardingPhase = data.idOnboardingPhase;

                    }

                    // Submit the changes to the database. 
                    puroTouchContext.SubmitChanges();


                }
                else
                {
                    errMsg = "There is No Task Type with ID = " + "'" + data.idTaskType + "'";
                }


            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }



    }


