<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadTTGT.aspx.cs" Inherits="UploadTTGT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>THU PHÍ PHẠT CÔNG AN</title>
    <link type="text/css"  href="../Stylesheet.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../Inc/D_calendar.css" media="screen" />
    <script language="javascript" type="text/javascript" src="../Inc/cos_lib.js"></script>    
    <script language="javascript" type="text/javascript" src="../Inc/D_calendar.js"></script>
    
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
        <td width="265" background="../images/logovnpt.gif" height="73"> 
		    &nbsp;</td>
        <td colspan=2 valign="top" bgcolor="#FFFFFF"><table width="100%"  border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="center">
                <%--<img border="0" src="../images/BANNER_CD.jpg" width="400" height="36">--%>
                <H1>THU PHÍ PHẠT CÔNG AN</H1>
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
                        <a class='menuLink2' href='viewTTGT.aspx'>Tra cứu thông tin xử phạt</a> </td>
                    <td align="right" class="text">
                        Copyright @2016 Bưu Điện TP Hồ Chí Minh</td>
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
            <table background="../Images/sidebg1.gif" cellpadding="0" cellspacing="0" 
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
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                &nbsp;<asp:LinkButton ID="cmdLogFile" runat="server" onclick="cmdLogFile_Click">logfile</asp:LinkButton></td>
	        </tr>
	        <tr>
		        <td colspan="2" 
                    style="border-bottom-style: solid; border-bottom-width: 1px; padding-left: 4px; padding-right: 4px; padding-top: 1px; padding-bottom: 1px" 
                    bordercolor="#FF9900">
		        <p align="left"><b><font color="#FF9900">Upload dữ liệu quyết định</font></b></td>
	        </tr>
	        <tr>
		            <td align="left" class="style10">Chọn file </td>
		            <td align="left" class="style11">
                        <asp:FileUpload ID="FileUpload" runat="server" />
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl="~/filemau.xls">File import mẫu</asp:HyperLink></td>
	            </tr>
	            <tr>
		            <td align="left" class="style10">Password</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="tPassword" runat="server" Width="135px"></asp:TextBox>
                                        </td>
	            </tr>
                
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="tOk" runat="server" onclick="tOk_Click" Text="Upload" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <br />
                    </td>
                </tr>
	        </table>
            <br />
            <table border="0" width="800" cellpadding="6" id="tblStep1" style="font-family: Arial; font-size: 10pt; border-collapse: collapse">   
	            
                <tr>
                    <td align="center" colspan="2">
                        <asp:GridView ID="DANHMUC" runat="server" AlternatingRowStyle-CssClass="alternate" 
                            AutoGenerateColumns="False" CellPadding="4" CssClass="grid-view" 
                            DataKeyNames="ID" HeaderStyle-CssClass="header" 
                            meta:resourcekey="DANHMUCResource" 
                            PagerSettings-PageButtonCount="10" PagerStyle-HorizontalAlign="Right" 
                            RowStyle-CssClass="normal" Width="98%">
                            <RowStyle CssClass="normal" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>'> </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="true" HorizontalAlign="left" Width="0%" />
                                </asp:TemplateField>
                                
                                <%--<asp:TemplateField HeaderText="Số quyết định">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="VIEW_1" runat="server" 
                                            NavigateUrl='<%# "viewBHXHct.aspx?ID=" + Eval("ID")%>' 
                                            Text='<%# Eval("SO_QD")%>' ToolTip="Nhắp chuột vào đây để xem">
					                </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="true" HorizontalAlign="left" Width="50" />
                                </asp:TemplateField>--%>
                                <asp:BoundField AccessibleHeaderText="Số quyết định" DataField="SO_QD" 
                                    HeaderText="Số quyết định">
                                    <ItemStyle HorizontalAlign="left"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="Ngày lập BB" DataField="NGAY_BB" 
                                    HeaderText="Ngày lập BB">
                                    <ItemStyle HorizontalAlign="left"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="Tên vi phạm" DataField="NGUOI_VP" 
                                    HeaderText="Tên vi phạm">
                                    <ItemStyle HorizontalAlign="left"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="Số xe" DataField="SO_XE" 
                                    HeaderText="Số xe">
                                    <ItemStyle HorizontalAlign="left"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="Tiền phạt" DataField="SO_TIEN" 
                                    HeaderText="Tiền phạt">
                                    <ItemStyle HorizontalAlign="right"  />
                                </asp:BoundField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" />
                            <HeaderStyle CssClass="header" />
                            <AlternatingRowStyle CssClass="alternate" />
                        </asp:GridView>
                    </td>
                </tr>
</table>
                                
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


    <asp:HiddenField ID="hidField1" runat="server" />


</form>

    </body>
</html>
