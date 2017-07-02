<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bind.aspx.cs" Inherits="qingjia_WeChat.SubPage.bind" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0" />
    <title>请假系统-绑定账号-IMAW</title>
    <link rel="stylesheet" href="css/weui.css" />
    <link rel="stylesheet" href="css/example.css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/example.js"></script>
    <script src="js/router.min.js"></script>
    <script src="js/zepto.min.js"></script>
</head>
<body>
    <div id="top" class="index_top" style="background:white;">
        <img src="./images/logo.png" style="height:90%; margin-left:2%; margin-top:1%;" />
    </div>
    <div class="modify_detail">
        <div class="weui-cells weui-cells_form">
            <br /><br /><br />
            <div class="weui-cell">
                <div class="weui-cell__hd"><label class="weui-label">账号</label></div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="text" placeholder="请输入您的账号" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd"><label class="weui-label">密码</label></div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="password" placeholder="请输入您的密码" />
                </div>
            </div>

            <div class="page__bd page__bd_spacing">
                <br /><br />
                <a href="../HomePage/qingjia_WeChat.aspx" class="weui-btn weui-btn_primary">绑定账号</a>
                <br />
            </div>

            <div class="page__ft">
                <br /><br /><br /><br /><br /><br /><br /><br /><br />
                <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
                <br /><br /><br />
            </div>
        </div>
    </div>
</body>
</html>
