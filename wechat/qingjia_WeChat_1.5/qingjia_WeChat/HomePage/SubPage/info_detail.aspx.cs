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
    public partial class info_detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDB();
            }
        }

        //加载个人信息
        private void LoadDB()
        {
            string ST_NUM = Request.Cookies["UserInfo"]["UserID"].ToString();

            DB dbInfo = new DB();
            DataSet dsInfo = dbInfo.GetList("ST_Num='" + ST_NUM + "'");
            DataTable dtSource = dsInfo.Tables[0];

            if (dtSource.Rows.Count > 0)
            {
                Label_Num.InnerText = dtSource.Rows[0]["ST_Num"].ToString();
                Label_Name.InnerText = dtSource.Rows[0]["ST_Name"].ToString();
                Label_Sex.InnerText = dtSource.Rows[0]["ST_Sex"].ToString();
                Label_Class.InnerText = dtSource.Rows[0]["ST_Class"].ToString();
                txtTel.Value = dtSource.Rows[0]["ST_Tel"].ToString();
                txtQQ.Value = dtSource.Rows[0]["ST_QQ"].ToString();
                if (dtSource.Rows[0]["ContactOne"].ToString() != "" && dtSource.Rows[0]["OneTel"].ToString() != "" && dtSource.Rows[0]["ST_Tel"].ToString() != "" && dtSource.Rows[0]["ST_QQ"].ToString() != "")
                {
                    txtGuardianName.Value = dtSource.Rows[0]["ContactOne"].ToString();
                    txtGuardianNum.Value = dtSource.Rows[0]["OneTel"].ToString();
                }
                else
                {
                    txtError.Value = "首次登陆需请填写个人信息！";
                    txtGuardianName.Value = "";
                    txtGuardianNum.Value = "";
                }
            }
            else
            {
                txtError.Value = "未查找到相关信息！";
            }
        }

        //更新个人信息
        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            if (Check())
            {
                string ST_NUM = Request.Cookies["UserInfo"]["UserID"].ToString();
                DB.UpdateStuInfo(ST_NUM, txtTel.Value.ToString().Trim(), txtQQ.Value.ToString().Trim(), txtGuardianName.Value.ToString().Trim(), txtGuardianNum.Value.ToString().Trim());
                Response.Redirect("info_succeed.aspx");
            }
        }

        //检查输入信息是否合法
        private bool Check()
        {
            if (CheckText(txtTel))
            {
                if (CheckText(txtQQ))
                {
                    if (CheckText(txtGuardianNum))
                    {
                        if (CheckText(txtGuardianName))
                        {
                            string st_guardian_name = txtGuardianName.Value.ToString().Trim();
                            if (!((st_guardian_name.Substring(0, 2) == "父亲" || st_guardian_name.Substring(0, 2) == "母亲" || st_guardian_name.Substring(0, 2) == "其他") && st_guardian_name.Substring(2, 1) == "-"))
                            {
                                txtError.Value = "填写方式错误！关系为父亲、母亲、其他";
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            txtError.Value = "未填写与家长关系及姓名！";
                            return false;
                        }
                    }
                    else
                    {
                        txtError.Value = "未填写家长联系方式！";
                        return false;
                    }
                }
                else
                {
                    txtError.Value = "未填写QQ号信息！";
                    return false;
                }
            }
            else
            {
                txtError.Value = "未填写个人联系方式！";
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


    }
}