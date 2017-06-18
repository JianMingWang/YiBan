<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="web.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="value" runat="server">
        <asp:TextBox ID="testValue" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="data" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="userInfoButton" runat="server" Text="获取用户个人信息" OnClick="userInfoButton_Click" />
        <asp:TextBox ID="userInfo" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
