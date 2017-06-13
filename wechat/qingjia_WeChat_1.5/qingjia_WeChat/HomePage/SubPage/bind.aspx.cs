using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using qingjia_WeChat.HomePage.Class;
using System.Data;

namespace qingjia_WeChat.SubPage
{
    public partial class bind : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["OpenID"] = Request.QueryString["openId"].ToString();
            if (Request.QueryString["openId"].ToString() != "" && Request.QueryString["openId"] != null)
            {
                Session["OpenID"] = Request.QueryString["openId"].ToString();
                Login();
            }
            else
            {
                //Response.Redirect("UserAuth.ashx");
                Response.Redirect("UserProcess.ashx");
            }
        }

        protected void Login()
        {
            string OpenID = Session["OpenID"].ToString();

            DB db = new DB();
            DataSet ds_OpenID = DB.RegisterCheck(" Wechat = '" + OpenID + "'");
            if (ds_OpenID.Tables[0].Rows.Count > 0)
            {
                Session["ST_Num"] = ds_OpenID.Tables[0].Rows[0]["ID"].ToString();
                Response.Redirect("../qingjia_WeChat.aspx");
            }
        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            DB db = new DB();
            if (Check())
            {
                DataSet ds = db.GetList("ST_Num ='" + User_Num.Value.ToString().Trim() + "'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (DB.CheckPassword(User_Num.Value.ToString().Trim(), User_Pw.Value.ToString().Trim()))
                    {
                        int i = DB.RegisterCheck(" ST_Num ='" + User_Num.Value.ToString().Trim() + "'").Tables[0].Rows.Count;
                        if (i == 0)
                        {
                            string OpenID = Request.QueryString["OpenID"].ToString();
                            string ST_Num = User_Num.Value.ToString().Trim();
                            if (DB.Register(ST_Num, OpenID))
                            {
                                Session["ST_Num"] = User_Num.Value;
                                Response.Redirect("../qingjia_WeChat.aspx");
                            }
                            else
                            {
                                txtError.Value = "注册失败！";
                            }
                        }
                        else
                        {
                            txtError.Value = "账户已被注册！如有疑问请联系管理员！";
                        }
                    }
                    else
                    {
                        txtError.Value = "密码错误！";
                    }
                }
                else
                {
                    txtError.Value = "账户不存在！";
                }
            }
        }

        private bool Check()
        {
            if (User_Num.Value.ToString().Trim() != "")
            {
                if (User_Pw.Value.ToString().Trim() != "")
                {
                    return true;
                }
                else
                {
                    txtError.Value = "请输入密码！";
                    return false;
                }
            }
            else
            {
                txtError.Value = "请输入账号！";
                return false;
            }
        }
    }
}