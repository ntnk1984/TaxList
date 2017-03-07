<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CN_KQPhat.aspx.cs" Inherits="admin_CN_KQPhat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link type="text/css"  href="../Stylesheet.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center" class="menuLink">
    
        CẬP NHẬT KẾT QUẢ PHÁT TỰ ĐỘNG<br />
        <br />
        </div>
        <div style="text-align: center">
        <asp:Label ID="lblNote" runat="server" CssClass="lblNote" Font-Bold="True" ></asp:Label>
    
        <br />
        <br />
        <asp:Button ID="btnOK" runat="server" onclick="btnOK_Click" Text="Thực hiện" />
    

    </div>
    

    </form>
</body>
</html>
