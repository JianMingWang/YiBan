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
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDB();
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
        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            if (Check())
            {
                Insertdata_out();
            }
        }

        //检查输入信息是否合法
        private bool Check()
        {
            if (CheckText(test_default1) && CheckText(test_default2))
            {
                if (Convert.ToDateTime(ChangeTime(test_default1.Value.ToString())) < Convert.ToDateTime(ChangeTime(test_default2.Value.ToString())))
                {
                    if (Leave_Reason.Value.ToString() != "")
                    {
                        if (Leave_Reason.Value.ToString().Length <= 60)
                        {
                            DateTime time_go = Convert.ToDateTime(ChangeTime(test_default1.Value.ToString()));
                            DateTime time_back = Convert.ToDateTime(ChangeTime(test_default2.Value.ToString()));
                            TimeSpan time_days = time_back - time_go;
                            int days = time_days.Days;
                            if (days < 3)
                            {
                                return true;
                            }
                            else
                            {
                                txtError.Value = "短期请假不能超过3天！";
                                return false;
                            }
                        }
                        else
                        {
                            txtError.Value = "请假原因不能超过60个字！";
                            return false;
                        }
                    }
                    else
                    {
                        txtError.Value = "请假原因不能为空！";
                        return false;
                    }
                }
                else
                {
                    txtError.Value = "返校时间不能小于离校时间！";
                    return false;
                }
            }
            else
            {
                txtError.Value = "时间不能为空！";
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

        protected void Insertdata_out()//离校请假用的
        {
            string LV_NUM = DateTime.Now.ToString("yyMMdd");//流水号的生成
            DateTime gotime_out = Convert.ToDateTime(ChangeTime(test_default1.Value.ToString()));
            DateTime backtime_out = Convert.ToDateTime(ChangeTime(test_default2.Value.ToString()));
            //检查是否存在相同申请   
            DataSet ds_ll = LeaveList.GetList("StudentID='" + Label_Num.InnerText.ToString().Trim() + "' and (( TimeLeave>='" + gotime_out + "' and TimeLeave<= '" + backtime_out + "' )"
                    + "or (  TimeBack>='" + gotime_out + "' and  TimeBack<= '" + backtime_out + "') or (  TimeLeave<='" + gotime_out + "' and  TimeBack>= '" + backtime_out + "')) ");

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
                        Insert_out(LV_NUM, gotime_out, backtime_out);
                        break;
                    }
                }
            }
            else
            {
                Insert_out(LV_NUM, gotime_out, backtime_out);
            }
        }

        public void Insert_out(string LV_NUM, DateTime gotime_out, DateTime backtime_out)
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

            DateTime nowtime = DateTime.Now;
            LL_Model model_ll = new LL_Model();

            //model_ll.StudentID = Label_Num.InnerText.ToString().Trim();
            //model_ll.TimeLeave = gotime_out;
            //model_ll.TimeBack = backtime_out;
            ////6代表节假日请假
            //model_ll.TypeID = 1;
            //model_ll.ID = LV_NUM;
            //model_ll.SubmitTime = nowtime;
            //model_ll.LeaveWay = txt_leave_way.Value.ToString().Trim();
            //model_ll.BackWay = txt_back_way.Value.ToString().Trim();
            //model_ll.Address = Leave_Reason.Value.ToString().Trim();
            //model_ll.StateLeave = "0";
            //model_ll.StateBack = "0";
            //model_ll.Reason = LV_REASON;
            //model_ll.TypeChildID = 6;
            //model_ll.Teacher = "";
            //model_ll.Lesson = "";
            //model_ll.Notes = "";

            model_ll.StudentID = Label_Num.InnerText.ToString().Trim();
            model_ll.TimeLeave = gotime_out;
            model_ll.TimeBack = backtime_out;
            //4代表短期请假
            model_ll.TypeID = 1;
            model_ll.ID = LV_NUM;
            model_ll.SubmitTime = nowtime;
            model_ll.LeaveWay = "";
            model_ll.BackWay = "";
            model_ll.Address = "";
            model_ll.StateLeave = "0";
            model_ll.StateBack = "0";
            model_ll.Reason = Leave_Reason.Value.ToString().Trim();
            model_ll.TypeChildID = 4;
            model_ll.Teacher = "";
            model_ll.Lesson = "";
            model_ll.Notes = "";

            LeaveList.Add(model_ll);
            Response.Redirect("schoolleave_succeed.aspx");
        }

        private string ChangeTime(string time)
        {
            string txt_time = test_default1.Value.ToString();
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
                else//AM 且等于12
                {
                    time_changed += "00" + time.Substring(13, 3) + ":00.000";
                }
            }
            return time_changed;
        }
    }
}