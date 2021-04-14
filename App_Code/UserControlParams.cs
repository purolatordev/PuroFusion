using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserControlParams
/// </summary>
public class UserControlParams
{
    public int idRequest { get; set; }
    public string FirstName { get; set; }
    public string Heading { get; set; }
    public int iRecordID { get; set; }

    public IList<int> xTimes;
    public IList<string> xNames;
    public IList<string> yNames;

    public UserControlParams()
    {
        xTimes = new List<int>();
        xNames = new List<string>();
        yNames = new List<string>();
    }
    public UserControlParams(int iCount)
    {
        xTimes = new List<int>();
        xNames = new List<string>();
        yNames = new List<string>();
        //iRecords = iCount;
        for (int i = 0; i < iCount; i++)
        {
            xTimes.Add(100 + i);
            xNames.Add("xNames: " + (100 + i).ToString());
            yNames.Add("yNames: " + (100 + i).ToString());
        }
    }
}