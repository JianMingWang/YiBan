<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="schoolleave_detail_short.aspx.cs" Inherits="qingjia_WeChat.SubPage.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>请假系统-离校请假-IMAW</title>
    <link rel="stylesheet" href="css/weui.css" />
    <link rel="stylesheet" href="css/example.css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/example.js"></script>
    <script src="js/router.min.js"></script>
    <script src="js/zepto.min.js"></script>
</head>
<body>
    <div id="top" class="index_top" style="background:white;">
        <a href="schoolleave.html"><img src="images/icon_back_.png" style="height:40%; margin-left:2%; margin-top:4%;" /></a>
        <a href="../HomePage/qingjia_WeChat.aspx"><img src="images/icon_home.png" style="height:40%; margin-left:2%; margin-top:4%;"/></a>        
    </div>
    <div class="weui-cells weui-cells_form">
        <div class="change">
                <a href="schoolleave_detail_short.aspx"><img src="images/schoolleave_short1.png" style="width:25%; margin-left:12.5%; margin-top:5%;" /></a>
                <a href="schoolleave_detail_long.aspx"><img src="images/schoolleave_long2.png" style="width:25%; margin-left:37.5%;" /></a>
                <a href="schoolleave_detail_holiday.aspx"><img src="images/schoolleave_holiday2 .png" style="width:25%; margin-left:62.5%;" /></a>
         </div>
         <div class="schoolleavedetail_main">
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">学号</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="0121403490333" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">姓名</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="张艳菲" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">班级</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="信管1403" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">性别</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="女" />
                    </div>
                </div>
                <div class="weui-cell" id="class">
                    <div class="weui-cell__hd"><label class="weui-label2">本人联系方式</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="15171431742" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">父母联系方式</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="18091368089" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">请假类型</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="短期请假" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label for="" class="weui-label2">离校时间</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="datetime-local" value="" placeholder="" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label for="" class="weui-label2">离校时间</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="datetime-local" value="" placeholder="" />
                    </div>
                </div>
                <div class="weui-cells__title">请假原因</div>
                <div class="weui-cells weui-cells_form">
                    <div class="weui-cell">
                        <div class="weui-cell__bd">
                            <textarea class="weui-textarea" placeholder="请输入文本" rows="3"></textarea>
                            <div class="weui-textarea-counter"><span>0</span>/200</div>
                        </div>
                    </div>
                </div>
                <div class="page__bd page__bd_spacing">
                    <br /><br />
                    <a href="schoolleave_succeed.aspx" class="weui-btn weui-btn_primary">立即提交</a>
                    <br />
                </div>
                <div class="page__ft">
                    <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
                </div>
           </div>
    </div>
</body>
</html>


