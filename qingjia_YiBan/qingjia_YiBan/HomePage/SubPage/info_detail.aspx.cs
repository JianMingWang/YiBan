using qingjia_YiBan.HomePage.Class;
using qingjia_YiBan.HomePage.Model.API;
using System;
using System.Web;
using System.Web.UI.HtmlControls;

namespace qingjia_YiBan.SubPage
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
            string access_token = Session["access_token"].ToString();
            string ST_NUM = access_token.Substring(0, access_token.IndexOf("_"));
            Client<UserInfo> client = new Client<UserInfo>();
            ApiResult<UserInfo> result = client.GetRequest("access_token=" + access_token, "/api/student/me");
            if (result.result == "error" || result.data == null)
            {
                Response.Redirect("../Error.aspx?errorMessage=" + result.messages);
                return;
            }

            UserInfo userInfo = result.data;

            Label_Num.InnerText = userInfo.UserID;
            Label_Name.InnerText = userInfo.UserName;
            Label_Sex.InnerText = userInfo.UserSex;
            Label_Class.InnerText = userInfo.UserClass;
            txtTel.Value = userInfo.UserTel;
            txtQQ.Value = userInfo.UserQQ;
            txtEmail.Value = userInfo.UserEmail;
            if (userInfo.ContactName != "" && userInfo.ContactTel != "" && userInfo.UserTel != "" && userInfo.UserQQ != "")
            {
                txtGuardianName.Value = userInfo.ContactName;
                txtGuardianNum.Value = userInfo.ContactTel;
            }
            else
            {
                txtError.Value = "首次登陆需请填写个人信息！";
                txtGuardianName.Value = "";
                txtGuardianNum.Value = "";
            }
        }

        //更新个人信息
        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            string access_token = Session["access_token"].ToString();

            if (Check())
            {
                string ST_Tel = txtTel.Value;
                string ST_QQ = txtQQ.Value;
                string Guardian = txtGuardianName.Value.ToString().Trim();
                string ST_Guardian = Guardian.Substring(0, 2);
                string ST_GuardianName = Guardian.Substring(3, Guardian.Length - 3);
                string ST_GuardianTel = txtGuardianNum.Value;
                string ST_Email = txtEmail.Value.ToString().Trim();

                Client<string> client = new Client<string>();
                string _postString = String.Format("access_token={0}&ST_Tel={1}&ST_QQ={2}&ST_Guardian={3}&ST_GuardianName={4}&ST_GuardianTel={5}&ST_Email={6}", access_token, ST_Tel, ST_QQ, ST_Guardian, ST_GuardianName, ST_GuardianTel, ST_Email);
                ApiResult<string> result = client.PostRequest(_postString, "/api/student/changeinfo");

                if (result.result == "success")
                {
                    Client<UserInfo> user_client = new Client<UserInfo>();
                    ApiResult<UserInfo> user_result = user_client.GetRequest("access_token=" + access_token, "/api/student/me");

                    if (user_result.result == "error" || user_result.data == null)
                    {
                        //出现错误，获取信息失败，跳转到错误界面 尚未完成
                        Response.Redirect("../Error.aspx");
                        return;
                    }

                    UserInfo userInfo = user_result.data;

                    HttpCookie cookie = new HttpCookie("UserInfo");
                    cookie.Values.Add("UserID", userInfo.UserID.ToString().Trim());
                    cookie.Values.Add("UserName", HttpUtility.UrlEncode(userInfo.UserName.ToString().Trim()));
                    cookie.Values.Add("UserClass", HttpUtility.UrlEncode(userInfo.UserClass.ToString().Trim()));
                    cookie.Values.Add("UserYear", userInfo.UserYear.ToString().Trim());
                    cookie.Values.Add("UserTel", userInfo.UserTel.ToString().Trim());
                    cookie.Values.Add("UserQQ", userInfo.UserQQ.ToString().Trim());
                    cookie.Values.Add("UserEmail", userInfo.UserEmail.ToString().Trim());
                    cookie.Values.Add("UserTeacher", HttpUtility.UrlEncode(userInfo.UserTeacherName.ToString().Trim()));
                    cookie.Values.Add("UserTeacherID", userInfo.UserTeacherID.ToString().Trim());
                    cookie.Values.Add("UserContactName", HttpUtility.UrlEncode(userInfo.ContactName.ToString().Trim()));
                    cookie.Values.Add("UserContactTel", userInfo.ContactTel.ToString().Trim());
                    cookie.Values.Add("IsFreshman", userInfo.IsFreshman.ToString().Trim());
                    cookie.Expires = DateTime.Now.AddMinutes(20);
                    HttpContext.Current.Response.Cookies.Add(cookie);

                    Response.Redirect("info_succeed.aspx");
                }
                else
                {
                    txtError.Value = result.messages;
                }
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
                            if (st_guardian_name.Length > 3)
                            {
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
                                txtError.Value = "请注意填写方式！";
                                return false;
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