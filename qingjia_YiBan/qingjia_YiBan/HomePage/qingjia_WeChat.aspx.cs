using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using qingjia_YiBan.HomePage.Class;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using qingjia_YiBan.HomePage.Model;

namespace qingjia_YiBan.HomePage
{
    public partial class qingjia_WeChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取学生基本信息
            LoadDB();
            //获取点名时间、请假截止时间
            LoadTimeEnd();
        }

        private void LoadDB()
        {
            //易班授权后跳转
            YB_AccessToken YB = new YB_AccessToken();
            if (Request.QueryString["access_token"] != null && Request.QueryString["userid"] != null && Request.QueryString["expires"] != null)
            {
                string userID = Request.QueryString["userid"].ToString();
                string YB_AccessToken = Request.QueryString["access_token"].ToString();

                //获取到用户ID  AccessToken
                Client<qingjia_AccessToken> client = new Client<qingjia_AccessToken>();
                ApiResult<qingjia_AccessToken> result = client.GetRequest("YiBanID=" + userID, "/api/oauth/access_token");
            }
            else
            {
                //测试使用
                Client<qingjia_AccessToken> client = new Client<qingjia_AccessToken>();
                ApiResult<qingjia_AccessToken> result = client.GetRequest("YiBanID=" + "123", "/api/oauth/access_token");
                qingjia_AccessToken qingjia_at = result.data;

                client = new Client<qingjia_AccessToken>();
            }

            //string ST_NUM = "0121403490106";
            string ST_NUM = Session["ST_Num"].ToString();

            DB db = new DB();
            DataSet ds_st = db.GetList("ST_Num ='" + ST_NUM + "'");
            if (ds_st.Tables[0].Rows.Count > 0)
            {
                //登录信息正确，将相关信息写入cookies
                HttpCookie cookie = new HttpCookie("UserInfo");
                cookie.Values.Add("UserID", ds_st.Tables[0].Rows[0]["ST_Num"].ToString());
                cookie.Values.Add("UserName", HttpUtility.UrlEncode(ds_st.Tables[0].Rows[0]["ST_Name"].ToString()));
                cookie.Values.Add("UserClass", HttpUtility.UrlEncode(ds_st.Tables[0].Rows[0]["ST_Class"].ToString()));
                cookie.Values.Add("UserYear", ds_st.Tables[0].Rows[0]["ST_Grade"].ToString());
                cookie.Values.Add("UserTeacher", HttpUtility.UrlEncode(ds_st.Tables[0].Rows[0]["ST_Teacher"].ToString()));
                cookie.Values.Add("UserTeacherID", HttpUtility.UrlEncode(ds_st.Tables[0].Rows[0]["ST_TeacherID"].ToString()));
                cookie.Expires = DateTime.Now.AddMinutes(20);
                Response.AppendCookie(cookie);

                UpdateInfo(ds_st.Tables[0].Rows[0]["ST_Num"].ToString());
                label_teacherName.InnerText = ds_st.Tables[0].Rows[0]["ST_Teacher"].ToString();
                label_Year.InnerText = ds_st.Tables[0].Rows[0]["ST_Grade"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('用户信息不存在');", true);
            }
        }

        private void LoadTimeEnd()
        {
            //从Cookie中获取值
            string teacherID = HttpUtility.UrlDecode(Request.Cookies["UserInfo"]["UserTeacherID"].ToString());
            DB db = new DB();

            //获取晚点名请假截止时间
            DataSet ds_te = db.GetTimeEnd(" TypeID= 2 AND TeacherID='" + teacherID + "'");
            if (ds_te.Tables[0].Rows.Count > 0)
            {
                DateTime end_time_dt = (DateTime)ds_te.Tables[0].Rows[0]["Time"];
                if (end_time_dt < DateTime.Now)//小于当前是见表示尚可请假
                {
                    label_EndTime.InnerText = "已过请假时间！";
                }
                else
                {
                    label_EndTime.InnerText = ((DateTime)ds_te.Tables[0].Rows[0]["Time"]).ToString("yyyy/MM/dd HH:mm");
                }
            }
            else
            {
                label_EndTime.InnerText = "未设置";
            }

            //获取晚点名时间
            DataSet ds_CallTime = db.GetCallTime(" ClassName='" + HttpUtility.UrlDecode(Request.Cookies["UserInfo"]["UserClass"].ToString()) + "'");
            if (ds_CallTime.Tables[0].Rows.Count > 0)
            {
                label_CallTime.InnerText = ((DateTime)ds_CallTime.Tables[0].Rows[0][0]).ToString("yyyy/MM/dd HH:mm");
            }
            else
            {
                label_CallTime.InnerText = "未设置";
            }

            //获取节假日请假时间
            DataSet ds_v_end_time = db.GetTimeEnd(" TypeID= 1 AND TeacherID='" + teacherID + "'");
            if (ds_v_end_time.Tables[0].Rows.Count > 0)
            {
                DateTime end_time_dt = (DateTime)ds_v_end_time.Tables[0].Rows[0]["Time"];
                if (end_time_dt < DateTime.Now)//小于当前时间表示不可请假
                {
                    vacation_end_time.Value = "已过请假时间！";
                }
                else
                {
                    Default_Vacation.Visible = true;
                    vacation_end_time.Value = ((DateTime)ds_v_end_time.Tables[0].Rows[0]["Time"]).ToString("yyyy/MM/dd HH:mm");
                }
            }
            else
            {
                vacation_end_time.Value = "未设置";
            }
        }

        //完善个人信息
        private void UpdateInfo(string ST_NUM)
        {
            if (DB.InfoCheck(ST_NUM) == false)
            {
                Response.Redirect("./SubPage/info_detail.aspx");
            }
        }
    }
}