using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using qingjia_YiBan.HomePage.Class;
using System.Data;
using System.Data.SqlClient;
using qingjia_YiBan.HomePage.Model.API;

namespace qingjia_YiBan.SubPage
{
    public partial class WebForm5 : System.Web.UI.Page
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
            //从API接口获取数据
            string access_token = Session["access_token"].ToString();
            string ST_NUM = access_token.Substring(0, access_token.IndexOf("_"));
            Client<UserInfo> client = new Client<UserInfo>();
            ApiResult<UserInfo> result = client.GetRequest("access_token=" + access_token, "/api/student/me");

            if (result.result == "error" || result.data == null)
            {
                //出现错误，获取信息失败，跳转到错误界面 尚未完成
                Response.Redirect("Error.aspx");
                return;
            }
            UserInfo userInfo = result.data;

            Label_Num.InnerText = userInfo.UserID;
            Label_Name.InnerText = userInfo.UserName;
            Label_Class.InnerText = userInfo.UserClass;
            Label_Tel.InnerText = userInfo.UserTel;
            Label_ParentTel.InnerText = userInfo.ContactTel;
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
                            if (days >= 3)
                            {
                                return true;
                            }
                            else
                            {
                                txtError.Value = "长期请假不能小于3天！";
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
            //string LV_NUM = DateTime.Now.ToString("yyMMdd");//流水号的生成
            //DateTime gotime_out = Convert.ToDateTime(ChangeTime(test_default1.Value.ToString()));
            //DateTime backtime_out = Convert.ToDateTime(ChangeTime(test_default2.Value.ToString()));
            ////检查是否存在相同申请   
            //DataSet ds_ll = LeaveList.GetList("ID='" + Label_Num.InnerText.ToString().Trim() + "' and (( TimeLeave>='" + gotime_out + "' and TimeLeave<= '" + backtime_out + "' )"
            //        + "or (  TimeBack>='" + gotime_out + "' and  TimeBack<= '" + backtime_out + "') or (  TimeLeave<='" + gotime_out + "' and  TimeBack>= '" + backtime_out + "')) ");

            //if (ds_ll.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds_ll.Tables[0].Rows.Count; i++)
            //    {
            //        if (ds_ll.Tables[0].Rows[i]["StateBack"].ToString().Trim() == "0")
            //        {
            //            txtError.Value = "您已提交过此时间段的请假申请！";
            //            break;
            //        }
            //        else
            //        {
            //            Insert_out(LV_NUM, gotime_out, backtime_out);
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    Insert_out(LV_NUM, gotime_out, backtime_out);
            //}
            DateTime gotime_out = Convert.ToDateTime(ChangeTime(test_default1.Value.ToString()));
            DateTime backtime_out = Convert.ToDateTime(ChangeTime(test_default2.Value.ToString()));

            //请假记录插入操作
            Insert_out(gotime_out, backtime_out);
        }

        public void Insert_out(DateTime gotime_out, DateTime backtime_out)
        {
            #region 拼装数据
            string access_token = Session["access_token"].ToString();
            string leave_type = "长期请假";
            string leave_date = gotime_out.ToString("yyyy-MM-dd");
            string leave_time = gotime_out.ToString("HH:mm:ss");//HH 代表24小时制 hh代表12小时制
            string back_date = backtime_out.ToString("yyyy-MM-dd");
            string back_time = backtime_out.ToString("HH:mm:ss");//HH 代表24小时制 hh代表12小时制
            string leave_reason = Leave_Reason.Value.ToString().Trim();
            string leave_way = "";//长期请假不涉及这三个属性
            string back_way = "";//长期请假不涉及这三个属性
            string address = "";//长期请假不涉及这三个属性
            #endregion

            #region 发送Post请求
            Client<string> client = new Client<string>();
            string _postString = String.Format("access_token={0}&leave_type={1}&leave_date={2}&leave_time={3}&back_date={4}&back_time={5}&leave_way={6}&back_way={7}&address={8}", access_token, leave_type, leave_date, leave_time, back_date, back_time, leave_way, back_way, address);//9个参数
            ApiResult<string> result = client.PostRequest(_postString, "/api/leavelist/leaveschool");
            if (result != null)
            {
                if (result.result == "success")
                {
                    Response.Redirect("schoolleave_succeed.aspx");
                }
                else
                {
                    txtError.Value = result.messages;
                }
            }
            else
            {
                //出现错误   此处报错说明API接口或网络存在问题
                txtError.Value = "出现未知错误，请联系管理员！";
            }
            #endregion

            //DataSet ds_ll_2 = LeaveList.GetList2("LV_NUM like '%" + LV_NUM + "%' order by LV_NUM DESC ");
            //string end3str = "0001";
            //if (ds_ll_2.Tables[0].Rows.Count > 0)
            //{
            //    string leavenumtop = ds_ll_2.Tables[0].Rows[0][0].ToString().Trim();
            //    int end3 = Convert.ToInt32(leavenumtop.Substring(6, 4));
            //    end3++;
            //    end3str = end3.ToString("0000");//按照此格式Tostring
            //}
            //LV_NUM += end3str;

            //DateTime nowtime = DateTime.Now;
            //LL_Model model_ll = new LL_Model();

            ////model_ll.StudentID = Label_Num.InnerText.ToString().Trim();
            ////model_ll.TimeLeave = gotime_out;
            ////model_ll.TimeBack = backtime_out;
            //////6代表节假日请假
            ////model_ll.TypeID = 1;
            ////model_ll.ID = LV_NUM;
            ////model_ll.SubmitTime = nowtime;
            ////model_ll.LeaveWay = txt_leave_way.Value.ToString().Trim();
            ////model_ll.BackWay = txt_back_way.Value.ToString().Trim();
            ////model_ll.Address = Leave_Reason.Value.ToString().Trim();
            ////model_ll.StateLeave = "0";
            ////model_ll.StateBack = "0";
            ////model_ll.Reason = LV_REASON;
            ////model_ll.TypeChildID = 6;
            ////model_ll.Teacher = "";
            ////model_ll.Lesson = "";
            ////model_ll.Notes = "";

            //model_ll.StudentID = Label_Num.InnerText.ToString().Trim();
            //model_ll.TimeLeave = gotime_out;
            //model_ll.TimeBack = backtime_out;
            ////5代表短期请假
            //model_ll.TypeID = 1;
            //model_ll.ID = LV_NUM;
            //model_ll.SubmitTime = nowtime;
            //model_ll.LeaveWay = "";
            //model_ll.BackWay = "";
            //model_ll.Address = "";
            //model_ll.StateLeave = "0";
            //model_ll.StateBack = "0";
            //model_ll.Reason = Leave_Reason.Value.ToString().Trim();
            //model_ll.TypeChildID = 5;
            //model_ll.Teacher = "";
            //model_ll.Lesson = "";
            //model_ll.Notes = "";

            //LeaveList.Add(model_ll);
            //Response.Redirect("schoolleave_succeed.aspx");
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
                else
                {
                    time_changed += "00" + time.Substring(13, 3) + ":00.000";
                }
            }
            return time_changed;
        }
    }
}