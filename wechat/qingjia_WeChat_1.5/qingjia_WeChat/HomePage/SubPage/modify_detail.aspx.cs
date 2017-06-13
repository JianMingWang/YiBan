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
    public partial class modify_detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            if (Check())
            {
                string ST_NUM = Request.Cookies["UserInfo"]["UserID"].ToString();
                string PW_Old = oldPw.Value.ToString().Trim();
                string Pw_New = newPw.Value.ToString().Trim();

                if (DB.CheckPassword(ST_NUM, PW_Old))
                {
                    DB.ChangePw(ST_NUM, Pw_New);
                    Response.Redirect("modify_succeed.aspx");
                }
                else
                {
                    txtError.Value = "原密码错误！";
                    oldPw.Value = "";
                    newPw.Value = "";
                    newPwConfirm.Value = "";
                }
            }
        }

        //检查输入信息是否合法
        private bool Check()
        {
            if (CheckText(oldPw) && CheckText(newPw) && CheckText(newPwConfirm))
            {
                string Pw1 = newPw.Value.ToString().Trim();
                string Pw2 = newPwConfirm.Value.ToString().Trim();
                if (Pw1 == Pw2)
                {
                    return true;
                }
                else
                {
                    txtError.Value = "两次输入新密码不同！";
                    return false;
                }
            }
            else
            {
                txtError.Value = "请输入旧密码！";
                return false;
            }
        }

        private bool CheckText(HtmlInputPassword Input)
        {
            if (Input.Value == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}