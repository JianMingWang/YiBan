<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="form_notaudited.aspx.cs" Inherits="qingjia_WeChat.SubPage.form_notaudited" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>请假系统-特殊请假-IMAW</title>
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
    <div class="weui-cells weui-cells_form">
        <div class="change">
            <a href="form_notaudited.aspx"><img src="images/form_notaudited1.png" style="width:25%; margin-left:25%; margin-top:5%;" /></a>
            <a href="form_taudited.aspx"><img src="images/form_taudited2.png" style="width:25%; margin-left:50%;" /></a>
        </div>
        <div class="schoolleavedetail_main">
            <div class="weui-cells weui-cells_form" id="form11">
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">请假单号</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="0121403490333" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">请假类别</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="事假" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">返校时间</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="datetime" placeholder="2016.10.4 12:30" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">提交时间</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="datetime" placeholder="2016.10.4 12:30" />
                    </div>
                </div>
                <div class="page__bd page__bd_spacing">
                    <br /><br />
                    <a href="form_succeed.aspx" class="weui-btn weui-btn_primary">撤销</a>
                    <br />
                </div>

            </div>
            <br />
            <div class="weui-cells weui-cells_form" id="form12">
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">请假单号</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="0121403490333" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">请假类别</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="事假" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">返校时间</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="datetime" placeholder="2016.10.4 12:30" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">提交时间</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="datetime" placeholder="2016.10.4 12:30" />
                    </div>
                </div>
                <div class="page__bd page__bd_spacing">
                    <br /><br />
                    <a href="form_succeed.aspx" class="weui-btn weui-btn_primary">撤销</a>
                    <br />
                </div>

            </div>
            <br />
            <div class="weui-cells weui-cells_form" id="form13">
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">请假单号</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="0121403490333" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">请假类别</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="事假" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">返校时间</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="datetime" placeholder="2016.10.4 12:30" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">提交时间</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="datetime" placeholder="2016.10.4 12:30" />
                    </div>
                </div>
                <div class="page__bd page__bd_spacing">
                    <br /><br />
                    <a href="form_succeed.aspx" class="weui-btn weui-btn_primary">撤销</a>
                    <br />
                </div>

            </div>
            <br />
            <div class="page__ft">
                <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
            </div>
        </div>

    </div>    
</body>
</html>
