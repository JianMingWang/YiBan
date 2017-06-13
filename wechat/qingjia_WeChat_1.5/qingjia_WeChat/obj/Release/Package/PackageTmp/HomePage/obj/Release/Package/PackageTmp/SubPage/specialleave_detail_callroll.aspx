<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="specialleave_detail_callroll.aspx.cs" Inherits="qingjia_WeChat.SubPage.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0"/>
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
        <a href="specialleave.aspx"><img src="images/icon_back_.png" style="height:40%; margin-left:2%; margin-top:4%;" /></a>
        <a href="../HomePage/qingjia_WeChat.aspx"><img src="images/icon_home.png" style="height:40%; margin-left:2%; margin-top:4%;"/></a>        
    </div>
    <div class="weui-cells weui-cells_form">
        <div class="change">
            <a href="specialleave_detail_callroll.aspx"><img src="images/specialleave_detail_callroll1.png" style="width:25%; margin-left:25%; margin-top:5%;" /></a>
            <a href="specialleave_detail_selfstudy.aspx"><img src="images/specialleave_detail_selfstudy2.png" style="width:25%; margin-left:50%;" /></a>
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
                    <input class="weui-input" type="number" placeholder="晚点名请假" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd"><label class="weui-label2">请假子类型</label></div>
                <div class="weui-cells weui-cells_radio">
                    <label class="weui-cell weui-check__label" for="x11">
                        <div class="weui-cell__bd">
                            <p>公假</p>
                        </div>
                        <div class="weui-cell__ft">
                            <input type="radio" class="weui-check" name="radio1" id="x11" checked="checked">
                            <span class="weui-icon-checked"></span>
                        </div>
                    </label>
                    <label class="weui-cell weui-check__label" for="x12">

                        <div class="weui-cell__bd">
                            <p>事假</p>
                        </div>
                        <div class="weui-cell__ft">
                            <input type="radio" name="radio1" class="weui-check" id="x12">
                            <span class="weui-icon-checked"></span>
                        </div>
                    </label>
                    <label class="weui-cell weui-check__label" for="x13">

                        <div class="weui-cell__bd">
                            <p>病假</p>
                        </div>
                        <div class="weui-cell__ft">
                            <input type="radio" name="radio1" class="weui-check" id="x13">
                            <span class="weui-icon-checked"></span>
                        </div>
                    </label>

                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd"><label for="" class="weui-label2">请假时间</label></div>
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
                <a href="specialleave_succeed.aspx" class="weui-btn weui-btn_primary">立即提交</a>
                <br />
            </div>
            <div class="page__ft">
                <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
            </div>
        </div> 
    </div>     
</body>
</html>
