using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using qingjia_WeChat.HomePage.Class;
using System.Data;
using System.Data.SqlClient;

namespace qingjia_WeChat.SubPage
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CheckDate())
            {
                LoadDB();
            }
            else
            {
                end.Style.Add("display", "block");
                index.Style.Add("display", "none");
            }
        }

        private void LoadDB()
        {
            string ST_NUM = Request.Cookies["UserInfo"]["UserID"].ToString();

            DB dbInfo = new DB();
            DataSet dsInfo = dbInfo.GetList("ST_Num='" + ST_NUM + "'");
            DataTable dtSource = dsInfo.Tables[0];

            Label_Num.InnerText = dtSource.Rows[0]["ST_Num"].ToString();
            Label_Name.InnerText = dtSource.Rows[0]["ST_Name"].ToString();
            Label_Class.InnerText = dtSource.Rows[0]["ST_Class"].ToString();
            Label_Tel.InnerText = dtSource.Rows[0]["ST_Tel"].ToString();
            Label_ParentTel.InnerText = dtSource.Rows[0]["OneTel"].ToString();

            //获取晚点名时间
            DB db = new DB();
            DataSet ds_CallTime = db.GetCallTime(" ClassName='" + HttpUtility.UrlDecode(Request.Cookies["UserInfo"]["UserClass"].ToString()) + "'");
            if (ds_CallTime.Tables[0].Rows.Count > 0)
            {
                txtLeaveTime.InnerText = ((DateTime)ds_CallTime.Tables[0].Rows[0][0]).ToString("yyyy/MM/dd HH:mm");
            }
            else
            {
                txtLeaveTime.InnerText = "未设置";
            }


        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            if (Check())
            {
                Insertdata_out();
            }
        }

        private bool CheckDate()
        {
            string teacher = HttpUtility.UrlDecode(Request.Cookies["UserInfo"]["UserTeacher"].ToString());

            DB db = new DB();
            DataSet ds_endTime = db.GetTimeEnd(" TypeID='2' AND TeacherID='" + teacher + "'");
            if (ds_endTime.Tables[0].Rows.Count > 0)
            {
                DateTime end_time_dt = (DateTime)ds_endTime.Tables[0].Rows[0]["Time"];
                if (end_time_dt <= DateTime.Now)//小于当前时间表示不可请假
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool Check()
        {
            if ( LeaveReason.Value.ToString().Trim() != "")
            {
                if (ChangeTime(txtLeaveTime.InnerText.ToString()) != "")
                {
                    return true;
                }
                else
                {
                    txtError.Value = "请假时间不能为空！";
                    return false;
                }
            }
            else
            {
                txtError.Value = "请将信息填充完整！";
                return false;
            }
        }

        private bool CheckText(HtmlInputText txt)
        {
            if (txt.Value == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void Insertdata_out()//晚点名请假
        {
            string LV_NUM = DateTime.Now.ToString("yyMMdd");//流水号的生成
            DateTime gotime_out = Convert.ToDateTime(txtLeaveTime.InnerText.ToString());
            //检查是否存在相同申请   
            DataSet ds_ll = LeaveList.GetList("StudentID='" + Label_Num.InnerText.ToString().Trim() + "' and TimeLeave='" + gotime_out + "' and LEFT(LeaveType,4) = '晚点名请' ");

            if (ds_ll.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds_ll.Tables[0].Rows.Count; i++)
                {
                    if (ds_ll.Tables[0].Rows[i]["StateBack"].ToString().Trim() == "0")
                    {
                        txtError.Value = "您已提交过此时间段的请假申请！";
                        break;
                    }
                    else
                    {
                        Insert_out(LV_NUM, gotime_out);
                        break;
                    }
                }
            }
            else
            {
                Insert_out(LV_NUM, gotime_out);
            }
        }

        public void Insert_out(string LV_NUM, DateTime gotime_out)
        {
            DataSet ds_ll_2 = LeaveList.GetList2("ID like '%" + LV_NUM + "%' order by ID DESC ");
            string end3str = "0001";
            if (ds_ll_2.Tables[0].Rows.Count > 0)
            {
                string leavenumtop = ds_ll_2.Tables[0].Rows[0][0].ToString().Trim();
                int end3 = Convert.ToInt32(leavenumtop.Substring(6, 4));
                end3++;
                end3str = end3.ToString("0000");//按照此格式Tostring
            }
            LV_NUM += end3str;

            int LV_Class = 0;
            if (x11.Checked == true)
            {
                LV_Class = 7;//公假
            }
            if (x12.Checked == true)
            {
                LV_Class = 8;//事假
            }
            if (x13.Checked == true)
            {
                LV_Class = 9;//病假
            }

            DateTime nowtime = DateTime.Now;
            LL_Model model_ll = new LL_Model();

            //model_ll.StudentID = Label_Num.InnerText.ToString().Trim();
            //model_ll.TimeLeave = gotime_out;
            //model_ll.TimeBack = gotime_out;
            ////5代表早晚自习请假
            //model_ll.TypeID = 2;
            //model_ll.ID = LV_NUM;
            //model_ll.SubmitTime = nowtime;
            //model_ll.LeaveWay = "";
            //model_ll.BackWay = "";
            //model_ll.Address = "";
            //model_ll.StateLeave = "4";
            //model_ll.StateBack = "4";
            //model_ll.Reason = LeaveReason.Value.ToString().Trim();
            //model_ll.TypeChildID = LV_Class;
            //model_ll.Teacher = "";
            //model_ll.Lesson = "";
            //model_ll.Notes = "";

            model_ll.StudentID = Label_Num.InnerText.ToString().Trim();
            model_ll.TimeLeave = gotime_out;
            model_ll.TimeBack = gotime_out;
            //2代表节假日请假
            model_ll.TypeID = 2;
            model_ll.ID = LV_NUM;
            model_ll.SubmitTime = nowtime;
            model_ll.LeaveWay = "";
            model_ll.BackWay = "";
            model_ll.Address = "";
            model_ll.StateLeave = "4";
            model_ll.StateBack = "4";
            model_ll.Reason = LeaveReason.Value.ToString().Trim();
            model_ll.TypeChildID = LV_Class;
            model_ll.Teacher = "";
            model_ll.Lesson = "";
            model_ll.Notes = "";
            LeaveList.Add(model_ll);
            Response.Redirect("specialleave_succeed.aspx");
        }

        private string ChangeTime(string time)
        {
            string time_changed = time.Substring(6, 4) + "-" + time.Substring(0, 2) + "-" + time.Substring(3, 2) + " ";
            if (time.IndexOf("PM") != -1)//说明含有PM
            {
                if (time.Substring(11, 2) != "12")//PM   且不为12
                {
                    int hour = int.Parse(time.Substring(11, 2)) + 12;
                    time_changed += hour + time.Substring(13, 3) + ":00.000";
                }
                else
                {
                    time_changed += time.Substring(11, 5) + ":00.000";
                }
            }
            else//说明含有AM
            {
                if (time.Substring(11, 2) != "12")//PM   且不为12
                {
                    time_changed += time.Substring(11, 5) + ":00.000";
                }
                else
                {
                    time_changed += "00" + time.Substring(13, 3) + ":00.000";
                }
            }
            return time_changed;
        }

    }
}