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
    public partial class classleave_detail : System.Web.UI.Page
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
            Label_Tel.InnerText = dtSource.Rows[0]["ST_TEL"].ToString();
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
            if (CheckText(test_default1) && CheckText(txtTeacherName) && LeaveReason.Value.ToString().Trim() != "")
            {
                if (Convert.ToDateTime(ChangeTime(test_default1.Value.ToString())) > DateTime.Now)
                {
                    if (LeaveReason.Value.ToString() != "")
                    {
                        if (LeaveReason.Value.ToString().Length <= 60)
                        {
                            return true;
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
                    txtError.Value = "请假课程开始时间不能小于当前时间！";
                    return false;
                }
            }
            else
            {
                txtError.Value = "存在未填写信息！";
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

            string ClassPeriod = "";
            if (x41.Checked == true)
            {
                ClassPeriod = "1";
            }
            if (x42.Checked == true)
            {
                ClassPeriod = "2";
            }
            if (x43.Checked == true)
            {
                ClassPeriod = "3";
            }
            if (x44.Checked == true)
            {
                ClassPeriod = "4";
            }
            if (x45.Checked == true)
            {
                ClassPeriod = "5";
            }

            //检查是否存在相同申请   
            DataSet ds_ll = LeaveList.GetList(" ((StudentID='" + Label_Num.InnerText.ToString() + "') and ( Lesson ='" + ClassPeriod + "') and (TypeID = '" + 3
                + "' ))");

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
                        Insert_out(LV_NUM, gotime_out, ClassPeriod);
                        break;
                    }
                }
            }
            else
            {
                Insert_out(LV_NUM, gotime_out, ClassPeriod);
            }
        }

        public void Insert_out(string LV_NUM, DateTime gotime_out, string Period)
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
            if (x31.Checked == true)
            {
                LV_Class = 13;//公假
            }
            if (x32.Checked == true)
            {
                LV_Class = 14;//事假
            }
            if (x33.Checked == true)
            {
                LV_Class = 15;//病假
            }

            DateTime nowtime = DateTime.Now;
            LL_Model model_ll = new LL_Model();
            model_ll.StudentID = Label_Num.InnerText.ToString().Trim();
            model_ll.TimeLeave = gotime_out;
            model_ll.TimeBack = gotime_out;
            model_ll.TypeID = 3;
            model_ll.ID = LV_NUM;
            model_ll.SubmitTime = nowtime;
            model_ll.LeaveWay = "";
            model_ll.BackWay = "";
            model_ll.Address = "";
            model_ll.StateLeave = "0";
            model_ll.StateBack = "0";
            model_ll.Reason = LeaveReason.Value.ToString().Trim();
            model_ll.TypeChildID = LV_Class;
            model_ll.Teacher = txtTeacherName.Value.ToString().Trim();
            model_ll.Lesson = Period;
            model_ll.Notes = "";

            LeaveList.Add(model_ll);
            Response.Redirect("classleave_succeed.aspx");
        }

        //转换时间格式
        private string ChangeTime(string time)
        {
            string txt_time = test_default1.Value.ToString();
            string time_changed = time.Substring(6, 4) + "-" + time.Substring(0, 2) + "-" + time.Substring(3, 2) + " ";
            if (x41.Checked == true)
            {
                time_changed += "08:00:00.000";
            }
            if (x42.Checked == true)
            {
                time_changed += "10:10:00.000";
            }
            if (x43.Checked == true)
            {
                time_changed += "14:00:00.000";
            }
            if (x44.Checked == true)
            {
                time_changed += "16:00:00.000";
            }
            if (x45.Checked == true)
            {
                time_changed += "18:30:00.000";
            }
            return time_changed;
        }
    }
}