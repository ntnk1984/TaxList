<%@ Page Language="C#" AutoEventWireup="true" CodeFile="exportTTGT.aspx.cs" Inherits="exportTTGT" %>
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
        .style12
        {
            color: #FF0000;
            font-weight: bold;
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
                        <a class='menuLink2' href='../TTSOGTVT.aspx'>Đăng ký nộp phạt qua bưu điện</a> </td>
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
                </td>
	        </tr>
	        <tr>
		        <td colspan="2" 
                    style="border-bottom-style: solid; border-bottom-width: 1px; padding-left: 4px; padding-right: 4px; padding-top: 1px; padding-bottom: 1px" 
                    bordercolor="#FF9900">
		        <p align="left"><b><font color="#FF9900">Tra cứu thông tin xử phạt</font>&nbsp;</b></td>
	        </tr>
	        <tr>
		            <td align="left" class="style10">Số QĐ/Biên bản xử phạt </td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="tSO_QD" runat="server" Width="135px"></asp:TextBox>
                        &nbsp;</td>
	            </tr>
	            <tr>
		            <td align="left" class="style10">Số xe</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="tSO_XE" runat="server" Width="135px"></asp:TextBox>
                                        </td>
	            </tr>
	            <tr>
		            <td align="left" class="style10">Tên vi phạm</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="t_NGUOI_VP" runat="server" Width="270px"></asp:TextBox>
                                        </td>
	            </tr>
                <tr>
                    <td align="left" class="style10">
                        Ngày lập quyết định</td>
                    <td align="left" class="style11">
                        <asp:TextBox ID="txt_TuNgay" runat="server" 
                            Font-Bold="True" Font-Italic="True" Width="100px"></asp:TextBox>
                        &nbsp;<img id="imgTu_Ngay" runat="server" alt="Click vào để chọn ngày" 
                            src="~/Images/calendar_24.gif"  onclick="javascript:return displayCalendar(txt_TuNgay, 'dd/mm/yyyy',this)"
                            style="cursor: pointer; vertical-align:middle;" /> đến
                        <asp:TextBox ID="txt_DenNgay" runat="server" 
                            Font-Bold="True" Font-Italic="True" Width="100px"></asp:TextBox>
                        &nbsp;<img id="imgDen_Ngay" runat="server" alt="Click vào để chọn ngày" 
                            src="~/Images/calendar_24.gif"   onclick="javascript:return displayCalendar(txt_DenNgay, 'dd/mm/yyyy',this)"
                            style="cursor: pointer; vertical-align:middle;" /> <span class="style12">
                        (dd/mm/yyyy)</span></td>
                </tr>
                <tr>
                    <td align="left" class="style10">
                        Mã đội</td>

                    <td align="left" class="style11">
                        <asp:TextBox ID="tMA_DOI" runat="server" Width="135px"></asp:TextBox>
                        </td>

                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="tOk" runat="server" onclick="tOk_Click" Text="Tra cứu" />   
                        &nbsp;&nbsp;&nbsp;   
                        <asp:Button ID="tExport" runat="server" onclick="tExport_Click" Text="Xuất danh sách" />
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
                             HeaderStyle-CssClass="header" 
                            meta:resourcekey="DANHMUCResource" 
                            PagerSettings-PageButtonCount="10" PagerStyle-HorizontalAlign="Right" 
                            RowStyle-CssClass="normal" Width="98%">
                            <RowStyle CssClass="normal" />
                            <Columns>
                                                              
                                <asp:BoundField AccessibleHeaderText="BKS" DataField="BKS" 
                                    HeaderText="BKS">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>
                                 <asp:BoundField AccessibleHeaderText="HỌ TÊN" DataField="HO_TEN" 
                                    HeaderText="HỌ TÊN">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>

                                <asp:BoundField AccessibleHeaderText="ĐỊA CHỈ" DataField="DIA_CHI" 
                                    HeaderText="ĐỊA CHỈ">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="SỐ ĐIỆN THOẠI" DataField="SO_DIEN_THOAI" 
                                    HeaderText="SỐ ĐIỆN THOẠI">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="LOẠI GIẤY TỜ" DataField="LOAI_GIAY_TO" 
                                    HeaderText="LOẠI GIẤY TỜ">
                                    <ItemStyle HorizontalAlign="left"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="SỐ GIẤY TỜ" DataField="SO_GIAY_TO" 
                                    HeaderText="SỐ GIẤY TỜ">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>
                                 <asp:BoundField AccessibleHeaderText="NGÀY CẤP" DataField="NGAY_CAP" 
                                    HeaderText="NGÀY CẤP">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>

                                  <asp:BoundField AccessibleHeaderText="NƠI CẤP" DataField="NOI_CAP" 
                                    HeaderText="NƠI CẤP">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="TIỀN PHẠT VP">
                                    <ItemTemplate>
                                        <asp:Label ID="TIEN_PHAT_VP" runat="server" Text='<%# Eval("TIEN_PHAT_VP", "{0:0,00}")%>'  ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="true" HorizontalAlign="right" Width="50" />
                                </asp:TemplateField>

                                   <asp:BoundField AccessibleHeaderText="SỐ QĐ XỬ PHẠT" DataField="SO_QD_XU_PHAT" 
                                    HeaderText="SỐ QĐ XỬ PHẠT">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>

                                    <asp:BoundField AccessibleHeaderText="GTTG" DataField="GTTG" 
                                    HeaderText="GTTG">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>

                                <asp:BoundField AccessibleHeaderText="GHI CHÚ" DataField="GHI_CHU" 
                                    HeaderText="GHI CHÚ">
                                    <ItemStyle HorizontalAlign="center"  />
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


</form>

    </body>
</html>
