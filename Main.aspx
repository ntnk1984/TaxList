<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="TTGTTP.Main" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>THU PHÍ PHẠT CÔNG AN</title>
    <link type="text/css"  href="../Stylesheet.css" rel="stylesheet" />
    <link href="bootstrap.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../Inc/D_calendar.css" media="screen" />
    <script language="javascript" type="text/javascript" src="../Inc/cos_lib.js"></script>    
    <script language="javascript" type="text/javascript" src="../Inc/D_calendar.js"></script>
    <script src="jquery-2.1.4.js"></script>
    <script src="moment.js"></script>
    <script src="bootstrap.js"></script>
    <style type="text/css">
        .style5
        {
            width: 100%;
            height: 163px;
        }
        .style6
        {
            width: 100%;
            height: 163px;
        }
        .text {
	        font-family: Verdana, Arial, Helvetica, sans-serif;
	        font-size: 11px;
	        font-weight: bold;
	        color: #FFFFFF;
	        text-decoration: none;
        }
        .style10
        {
        	width: 150px;
        }
        .style11
        {
            
        }
        .style12
        {
            color: #FF0000;
            font-weight: bold;
        }
        .auto-style1 {
            width: 165px;
        }
        .auto-style2 {
            width: 482px;
        }
        </style>
</head>
<body bottommargin=0 topmargin="0" leftmargin="0" rightmargin="0">
   <form id="form1" runat="server">
       
<center>
<table width="100%" height="112"  border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td height="73" background="../images/topbg.gif"><table width=100% border=0 align="center" cellpadding=0 cellspacing=0>
      <tr>
        <td width="12"> <img src="../images/topleft.gif" width="12" height="73"></td>
        <td background="../images/logovnpt.gif" height="73" class="auto-style1"> 
		    &nbsp;</td>
        <td colspan=2 valign="top" bgcolor="#FFFFFF"><table width="100%"  border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="center">
                <%--<img border="0" src="../images/BANNER_CD.jpg" width="400" height="36">--%>
                <H1 class="auto-style2">THU PHÍ PHẠT CÔNG AN</H1>
            </td>
          </tr>
        </table> </td>
        <td width="14"> <img src="../images/topright.gif" width=14 height=73></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="36" background="../images/subnavbg.gif"><table width=100% border=0 align="center" cellpadding=0 cellspacing=0>
      <tr>
        <td width="12"> <img src="../images/subnavleft.gif" width=12 height=36></td>
        <td align="center" class="text">
            <table width="98%" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left" class="text">
                        <%--<a class='menuLink2' href="TTSOGTVT.aspx">Tra cứu thông tin xử phạt</a> --%>
                        <asp:Literal ID="Literal2" runat="server"   ></asp:Literal> 
                    </td>
                    <td align="right" class="text">
                        <asp:Label ID="labelName" runat="server" ></asp:Label></td>
                </tr>
            </table>
             
            </td>
        <td width="14"> <img src="../images/subnavright.gif" width=14 height=36></td>
      </tr>
    </table></td>
  </tr>
   <tr>
    <td height="36"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td width="12" valign="top"> 
            <table background="~/Images/sidebg1.gif" cellpadding="0" cellspacing="0" 
                class="style6">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
          </td>
        <td align="center">
            <br />
            <table border="0" width="600" cellpadding="6" id="tblCation" style="font-family: Arial; font-size: 10pt; border-collapse: collapse">
	        <tr>
		        <td colspan="2" align="center">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"  ></asp:PlaceHolder>
                </td>
	        </tr>

	        </table>
            <br />
                                
            </td>
        <td width="14" valign="top"> 
            <table background="../Images/sidebg1.gif" cellpadding="0" cellspacing="0" 
                class="style5">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
          </td>
      </tr>
    </table></td>
  </tr>
</table>


    </center>

    <center>Copyright @2016 Bưu Điện TP Hồ Chí Minh</center>   
</form>

    </body>
</html>
