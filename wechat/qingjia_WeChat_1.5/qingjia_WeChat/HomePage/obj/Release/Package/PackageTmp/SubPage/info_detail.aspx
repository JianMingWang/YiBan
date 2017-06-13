<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="info_detail.aspx.cs" Inherits="qingjia_WeChat.SubPage.info_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0" />
    <title>请假系统-个人信息-IMAW</title>
    <link rel="stylesheet" href="css/weui.css" />
    <link rel="stylesheet" href="css/example.css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/example.js"></script>
    <script src="js/router.min.js"></script>
    <script src="js/zepto.min.js"></script>
</head>
<body>
    <div id="top" class="index_top" style="background:white;">
        <a href="../HomePage/qingjia_WeChat.aspx"><img src="images/icon_back_.png" style="height:40%; margin-left:2%; margin-top:4%;" /></a>
        <a href="../HomePage/qingjia_WeChat.aspx"><img src="images/icon_home.png" style="height:40%; margin-left:2%; margin-top:4%;"/></a>        
    </div>
    <div class="info_detail" >
            <div class="weui-cells weui-cells_form">
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label3">学号</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="0121403490333" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label3">姓名</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="text"  placeholder="张艳菲" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label3">性别</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="text" placeholder="女" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label3">班级</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="text" placeholder="信管1403" />
                    </div>
                </div>                
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label3">入学年份</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="text" placeholder="2014" />
                    </div>
                </div>
                <div class="weui-cell" id="class">
                    <div class="weui-cell__hd"><label class="weui-label3">手机号</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="15171431742" />
                    </div>
                </div>
                <div class="weui-cell" id="class">
                    <div class="weui-cell__hd"><label class="weui-label3">QQ号</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="1032430923" />
                    </div>
                </div>
                <div class="weui-cell" id="class">
                    <div class="weui-cell__hd"><label class="weui-label3">邮箱</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="email" placeholder="15171431742@qq.com" />
                    </div>
                </div>
                <div class="weui-cell" id="class">
                    <div class="weui-cell__hd"><label class="weui-label3">寝室号</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="text" placeholder="北四-000" />
                    </div>
                </div>
                <div class="weui-cell" id="class">
                    <div class="weui-cell__hd"><label class="weui-label3">与家长关系及姓名</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="text" placeholder="父亲-张爸爸" />
                    </div>
                </div>
                    <div class="weui-cell" id="class">
                        <div class="weui-cell__hd"><label class="weui-label3">家长联系方式</label></div>
                        <div class="weui-cell__bd">
                            <input class="weui-input" type="number" placeholder="15171431742" />
                        </div>
                    </div>               
                <div class="page__bd page__bd_spacing">
                    <br /><br />
                    <a href="info_succeed.aspx" class="weui-btn weui-btn_primary">提交</a>
                    <br />
                </div>
                <div class="page__ft">
                    <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
                </div>
                </div>       
    </div>
</body>
</html>
