<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="classleave_detail.aspx.cs" Inherits="qingjia_WeChat.SubPage.classleave_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0" />
    <title>请假系统-上课请假备案-IMAW</title>
    <link rel="stylesheet" href="css/weui.css" />
    <link rel="stylesheet" href="css/example.css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/example.js"></script>
    <script src="js/router.min.js"></script>
    <script src="js/zepto.min.js"></script>
</head>
<body>
     <div id="top" class="index_top" style="background:white;">
        <a href="classleave.aspx"><img src="images/icon_back_.png" style="height:40%; margin-left:2%; margin-top:4%;" /></a>
        <a href="../HomePage/qingjia_WeChat.aspx"><img src="images/icon_home.png" style="height:40%; margin-left:2%; margin-top:4%;"/></a>        
    </div>
    <div class="classleave_detail" >
            <div class="weui-cells weui-cells_form">
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
                    <div class="weui-cell__hd"><label for="" class="weui-label2">请假时间</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="datetime-local" value="" placeholder="" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">请假类型</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="上课请假备案" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">请假子类型</label></div>
                       <div class="weui-cells weui-cells_radio">
                            <label class="weui-cell weui-check__label" for="x31">
                                <div class="weui-cell__bd">
                                    <p>公假</p>
                                </div>
                                <div class="weui-cell__ft">
                                    <input type="radio" class="weui-check" name="radio3" id="x31" checked="checked">
                                    <span class="weui-icon-checked"></span>
                                </div>
                            </label>
                            <label class="weui-cell weui-check__label" for="x32">

                                <div class="weui-cell__bd">
                                    <p>事假</p>
                                </div>
                                <div class="weui-cell__ft">
                                    <input type="radio" name="radio3" class="weui-check" id="x32" >
                                    <span class="weui-icon-checked"></span>
                                </div>
                            </label>
                            <label class="weui-cell weui-check__labe3" for="x33">

                                <div class="weui-cell__bd">
                                    <p>病假</p>
                                </div>
                                <div class="weui-cell__ft">
                                    <input type="radio" name="radio3" class="weui-check" id="x33" >
                                    <span class="weui-icon-checked"></span>
                                </div>
                            </label>

                        </div>
                    </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">任课老师姓名</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" placeholder="请输入任课老师姓名" />
                    </div>
                </div> 
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label2">请假时段</label></div>
                    <div class="weui-cells weui-cells_radio">
                        <label class="weui-cell weui-check__label" for="x41">
                            <div class="weui-cell__bd">
                                <p>第一大节(8:00-9:40)</p>
                            </div>
                            <div class="weui-cell__ft">
                                <input type="radio" class="weui-check" name="radio4" id="x41" checked="checked">
                                <span class="weui-icon-checked"></span>
                            </div>
                        </label>
                        <label class="weui-cell weui-check__label" for="x42">

                            <div class="weui-cell__bd">
                                <p>第二大节(10:10-11:50)</p>
                            </div>
                            <div class="weui-cell__ft">
                                <input type="radio" name="radio4" class="weui-check" id="x42">
                                <span class="weui-icon-checked"></span>
                            </div>
                        </label>
                        <label class="weui-cell weui-check__label" for="x43">

                            <div class="weui-cell__bd">
                                <p>第三大节(14:00-15:40)</p>
                            </div>
                            <div class="weui-cell__ft">
                                <input type="radio" name="radio4" class="weui-check" id="x43">
                                <span class="weui-icon-checked"></span>
                            </div>
                        </label>
                        <label class="weui-cell weui-check__label" for="x44">

                            <div class="weui-cell__bd">
                                <p>第四大节(16:00-17:40)</p>
                            </div>
                            <div class="weui-cell__ft">
                                <input type="radio" name="radio4" class="weui-check" id="x44">
                                <span class="weui-icon-checked"></span>
                            </div>
                        </label>
                        <label class="weui-cell weui-check__label" for="x45">

                            <div class="weui-cell__bd">
                                <p>第五大节(18:300-20:30)</p>
                            </div>
                            <div class="weui-cell__ft">
                                <input type="radio" name="radio4" class="weui-check" id="x45">
                                <span class="weui-icon-checked"></span>
                            </div>
                        </label>

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
                    <a href="classleave_succeed.aspx" class="weui-btn weui-btn_primary">立即提交</a>
                    <br />
                </div>
                <div class="page__ft">
                    <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
                </div>
                </div>       
    </div>
</body>
</html>
