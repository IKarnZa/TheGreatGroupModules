using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TheGreatGroupModules.Models;

namespace TheGreatGroupModules.Modules
{
    public class StaffData
    {
        private string errMsg = "";

        public DataTable GetZone()
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @"  SELECT zoneid ,zonecode,CONCAT(zonecode,'-',zonename) AS zonename 
                                    FROM zone
                                    WHERE Activated=1 AND Deleted=0 ";

                
                DataTable dt = DBHelper.List(StrSql, ObjConn);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ObjConn.Close();
            }
        }

        public DataTable GetStaffRole(int staffid, int staffroleid)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @" SELECT s.*,sr.StaffRoleName FROM  Staff  s
            LEFT JOIN  staffrole sr ON s.StaffRoleID = sr.StaffRoleID
            WHERE 0=0 AND s.deleted=0 AND sr.Deleted=0 ";

                if (staffid > 0)
                    StrSql += " and s.StaffID=" + staffid;

                if (staffroleid > 0)
                    StrSql += " and s.StaffRoleID=" + staffroleid;
                DataTable dt = DBHelper.List(StrSql, ObjConn);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ObjConn.Close();
            }
        }

        public DataTable GetStaff(int staffroleId, int zoneId)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @"  SELECT s.*,z.ZoneName FROM  staff_zone sz 
            LEFT JOIN  Staff s ON sz.StaffID = s.StaffID
            LEFT JOIN  zone z ON sz.ZoneID = z.ZoneID
            WHERE 0=0 ";

                if (staffroleId != 0)
                    StrSql += " AND s.StaffRoleID=" + staffroleId;
                if (zoneId != 0)
                    StrSql += " AND sz.ZoneID=" + zoneId;


                DataTable dt = DBHelper.List(StrSql, ObjConn);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ObjConn.Close();
            }
        }

        public void AddStaffRole(StaffRole role) {


               MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {
                string strSql = @"insert into";

                DBHelper.Execute(strSql, ObjConn);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        
        }
    }
}