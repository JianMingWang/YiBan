using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace qingjia_WeChat.HomePage.Class
{
    static public class LeaveList
    {
        static public DataSet GetList(string strWhere)
        {
            string connString = ConfigurationManager.AppSettings["ConnectionString"];

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM vw_LeaveList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(strSql.ToString(), conn))
                {
                    da.Fill(ds);
                }
            }
            return ds;
        }

        static public DataSet GetList2(string strWhere)
        {
            string connString = ConfigurationManager.AppSettings["ConnectionString"];

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct Top 1 LV_NUM ");
            strSql.Append(" FROM vw_LeaveList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(strSql.ToString(), conn))
                {
                    da.Fill(ds);
                }
            }
            return ds;
        }

        //未修改
        static public bool Add(LL_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_LeaveList(");
            strSql.Append("ID,StudentID,Reason,SubmitTime,StateLeave,StateBack,Notes,TypeID,TimeLeave,TimeBack,LeaveWay,BackWay,Address,TypeChildID,Lesson,Teacher,AuditTeacherID,PtintTimes)");
            strSql.Append(" values (");
            strSql.Append("@ID,@StudentID,@Reason,@SubmitTime,@StateLeave,@StateBack,@Notes,@TypeID,@TimeLeave,@TimeBack,@LeaveWay,@BackWay,@Address,@TypeChildID,@Lesson,@Teacher,@AuditTeacherID,@PtintTimes)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,12),
					new SqlParameter("@StudentID", SqlDbType.Char,13),
					new SqlParameter("@Reason", SqlDbType.NVarChar,255),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
					new SqlParameter("@StateLeave", SqlDbType.DateTime),
					new SqlParameter("@StateBack", SqlDbType.Char,1),
					new SqlParameter("@Notes", SqlDbType.Char,1),
					new SqlParameter("@TypeID", SqlDbType.Char,1),
					new SqlParameter("@TimeLeave", SqlDbType.NVarChar,10),
					new SqlParameter("@TimeBack", SqlDbType.NChar,2),
					new SqlParameter("@LeaveWay", SqlDbType.NVarChar,10),
					new SqlParameter("@BackWay", SqlDbType.NVarChar,10),
					new SqlParameter("@Address", SqlDbType.Int,4),
					new SqlParameter("@TypeChildID", SqlDbType.NVarChar,10),
					new SqlParameter("@Lesson", SqlDbType.NVarChar,10),
					new SqlParameter("@Teacher", SqlDbType.NVarChar,255),
					new SqlParameter("@AuditTeacherID", SqlDbType.NVarChar,255),
					new SqlParameter("@PtintTimes", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.StudentID;
            parameters[2].Value = model.Reason;
            parameters[3].Value = model.SubmitTime;
            parameters[4].Value = model.StateLeave;
            parameters[5].Value = model.StateBack;
            parameters[6].Value = model.Notes;
            parameters[7].Value = model.TypeID;
            parameters[8].Value = model.TimeLeave;
            parameters[9].Value = model.TimeBack;
            parameters[10].Value = model.LeaveWay;
            parameters[11].Value = model.BackWay;
            parameters[12].Value = model.Address;
            parameters[13].Value = model.TypeChildID;
            parameters[14].Value = model.Lesson;
            parameters[15].Value = model.Teacher;
            parameters[16].Value = model.AuditTeacherID;
            parameters[17].Value = model.PrintTimes;

            string connString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(strSql.ToString(), conn))
                {
                    foreach (SqlParameter parm in parameters)
                        cmd.Parameters.Add(parm);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    if (rows > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 获取待请假记录
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        static public DataSet GetList3(string strWhere)
        {
            string connString = ConfigurationManager.AppSettings["ConnectionString"];

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TimeLeave,TimeBack,TypeChildID ");
            strSql.Append(" FROM vw_LeaveList ");
            strSql.Append(" where " + strWhere + " ORDER BY ID DESC");
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(strSql.ToString(), conn))
                {
                    da.Fill(ds);
                }
            }
            return ds;
        }

        static public bool Delete_leaveList(string strWhere)
        {
            string connString = ConfigurationManager.AppSettings["ConnectionString"];

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE ");
            strSql.Append(" FROM T_LeaveList ");
            strSql.Append(" where " + strWhere);
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(strSql.ToString(), conn))
                {
                    int i = (int)cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}