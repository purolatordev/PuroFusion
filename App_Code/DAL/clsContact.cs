﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for clsContact
/// </summary>
public class clsContact
{
    public int idContact { get; set; }

    public int idContactType { get; set; }
    public string ContactTypeName { get; set; }

    public int idRequest { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }

    public System.Nullable<System.DateTime> UpdatedOn { get; set; }

    public System.Nullable<System.DateTime> CreatedOn { get; set; }

}
public static class SrvContact
{
    public static List<clsContact> GetContactsByRequestID(int idRequest)
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        List<clsContact> oEquip = (from data in puroTouchContext.GetTable<tblContact>()
                                   where data.idRequest == idRequest
                                   //orderby data.EquipmentDesc
                                   select new clsContact
                                   {
                                       idContact = data.idContact,
                                       idContactType = data.idContactType,
                                       ContactTypeName = data.tblContactType.ContactType,
                                       idRequest = data.idRequest,
                                       Name = data.Name,
                                       Title = data.Title,
                                       Phone = data.Phone,
                                       Email = data.Email,
                                       CreatedBy = data.CreatedBy,
                                       CreatedOn = data.CreatedOn
                                   }).ToList<clsContact>();
        return oEquip;
    }
    public static string Insert(clsContact data, out Int32 newID) 
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        newID = -1;
        try
        {
            tblContact oNewRow = new tblContact()
            {
                idContactType = data.idContactType,
                idRequest = data.idRequest,
                Name = data.Name,
                Title = data.Title,
                Phone = data.Phone,
                Email = data.Email,
                CreatedBy = data.CreatedBy,
                CreatedOn = data.CreatedOn
            };
            puroTouchContext.GetTable<tblContact>().InsertOnSubmit(oNewRow);
            puroTouchContext.SubmitChanges();
            newID = oNewRow.idContact;
            data.idRequest = newID;
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg; 
    }
    public static string Update(clsContact data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        try
        {
            var contact = puroTouchContext.GetTable<tblContact>().Where(f=>f.idContact == data.idContact).FirstOrDefault();
            if(contact != null)
            {
                contact.Name = data.Name;
                contact.Title = data.Title;
                contact.Phone = data.Phone;
                contact.Email = data.Email;
                contact.UpdatedBy = data.UpdatedBy;
                contact.UpdatedOn = data.UpdatedOn;
            }
            puroTouchContext.SubmitChanges();
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
    public static string Remove(int idContact)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        try
        {
            var q = puroTouchContext.GetTable<tblContact>().Where(f => f.idContact == idContact).FirstOrDefault();
            puroTouchContext.GetTable<tblContact>().DeleteOnSubmit(q);
            //var qContact = (from data in puroTouchContext.GetTable<tblContact>()
            //                           where data.idContact == idContact
            //select new clsContact
            //{
            //    idContact = data.idContact,
            //    idContactType = data.idContactType,
            //    ContactTypeName = data.tblContactType.ContactType,
            //    idRequest = data.idRequest,
            //    Name = data.Name,
            //    Title = data.Title,
            //    Phone = data.Phone,
            //    Email = data.Email,
            //    CreatedBy = data.CreatedBy,
            //    CreatedOn = data.CreatedOn
            //}).FirstOrDefault();
            //puroTouchContext.GetTable<tblContact>().InsertOnSubmit(oNewRow);
            puroTouchContext.SubmitChanges();
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

}