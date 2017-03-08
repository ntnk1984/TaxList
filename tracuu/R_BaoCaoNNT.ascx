<%@ Control Language="C#" AutoEventWireup="true" CodeFile="R_BaoCaoNNT.ascx.cs" Inherits="tracuu_R_BaoCaoNNT" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<table width="100%" height="112" border="0" cellpadding="0" cellspacing="0">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <tr>
        <td height="36">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="12" valign="top">
                        <table background="../Images/sidebg1.gif" cellpadding="0" cellspacing="0"
                            class="style6">
                            <tr>
                                <td>&nbsp;</td>
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
                                    <p align="left">
                                        <b><font color="#FF9900">Danh sách NNT</font>&nbsp;</b>
                                </td>
                            </tr>



                            <tr>
                                <td align="left" class="style10">Ngày thống kê</td>
                                <td align="left" class="style11">
                                    <asp:TextBox ID="txt_TuNgay" runat="server"
                                        Font-Bold="True" Font-Italic="True" Width="100px"></asp:TextBox>
                                    &nbsp;<img id="imgTu_Ngay" runat="server" alt="Click vào để chọn ngày"
                                        src="~/Images/calendar_24.gif"
                                        style="cursor: pointer; vertical-align: middle;" />
                                    đến
                       
                                    <asp:TextBox ID="txt_DenNgay" runat="server"
                                        Font-Bold="True" Font-Italic="True" Width="100px"></asp:TextBox>
                                    &nbsp;<img id="imgDen_Ngay" runat="server" alt="Click vào để chọn ngày"
                                        src="~/Images/calendar_24.gif"
                                        style="cursor: pointer; vertical-align: middle;" />
                                    <span class="style12">(dd/mm/yyyy)</span></td>
                            </tr>
                            <tr>
                                <td align="left" class="style10">Trung tâm</td>
                                <td align="left" class="style11">
                                    <asp:DropDownList ID="cboTrungTam" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="cboTrungTam_SelectedIndexChanged">
                                    </asp:DropDownList>

                                    &nbsp;
                                
                                Bưu cục&nbsp;&nbsp;
                                    <asp:DropDownList ID="cboBuuCuc" runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style10">Trạng thái</td>
                                <td align="left" class="style10">
                                    <asp:DropDownList ID="cboTrangThai" runat="server" Width="450px">
                                        <asp:ListItem Selected="True" Value="0">Tất cả</asp:ListItem>
                                        <asp:ListItem Value="1">Mới phát sinh dữ liệu</asp:ListItem>
                                        <asp:ListItem Value="2">Mới cập nhật dữ liệu</asp:ListItem>
                                        <asp:ListItem Value="3">Không có phát sinh dữ liệu</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="tOk" runat="server" OnClick="tOk_Click" Width="125px" Text="Xem báo cáo" />
                                    &nbsp;&nbsp;   
                       
                                </td>
                                <td align="center" colspan="2"></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label ID="LabelR" runat="server" Text=""></asp:Label>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table border="0" width="1200" cellpadding="6" id="tblStep1" style="font-family: Arial; font-size: 10pt; border-collapse: collapse">

                            <tr>
                                <td align="center" colspan="2">
                                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="9pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ShowBackButton="False" ShowDocumentMapButton="False" ShowFindControls="False" ShowPrintButton="true" Height="1000px" Width="1200px">
                                    </rsweb:ReportViewer>

                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="14" valign="top">
                        <table background="../Images/sidebg1.gif" cellpadding="0" cellspacing="0"
                            class="style5">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
