using qingjia_YiBan.HomePage.Class;
using System;

namespace qingjia_YiBan.HomePage.SubPage
{
    public partial class LeavePrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string access_token = Session["access_token"].ToString();
            string LV_NUM = Request.QueryString["LV_NUM"].ToString();
            //string access_token = "0121403490106_ed18d81f-c197-4de4-b3b6-a68002ebe3e2";
            //string LV_NUM = "1710300002";
            string url = GetLeavePicUrl(access_token, LV_NUM);
            imgLeave.Src = url;
            picUrl.InnerText = "如图片不能保存，可复制此链接到浏览器中" + url;
        }

        private string GetLeavePicUrl(string access_token, string LV_NUM)
        {
            Client<string> client = new Client<string>();
            ApiResult<string> result = client.GetRequest("access_token=" + access_token + "&LV_NUM=" + LV_NUM, "/api/leavelist/print");
            if (result.result == "success")
            {
                return result.data.ToString();
            }
            return "http://oz02m20ga.bkt.clouddn.com/avatar.JPG";
        }
    }
}