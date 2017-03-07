<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImportDSNVP.ascx.cs" Inherits="tracuu_ImportDSNVP" %>
<center>
    

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
	        <%--<tr>
		            <td align="left" class="style10">Mã nghiệp vụ PP </td>
		            <td align="left" class="style11">
                                                
                                        
                                        <asp:DropDownList ID="DropDownListMa_Nghiep_Vu" onchange="setBeginRow(this);" runat="server" Width="135px" >
                                            <asp:ListItem Selected="True" >5001</asp:ListItem>
                                            <asp:ListItem>5002</asp:ListItem>
                        </asp:DropDownList>
                                        &nbsp;</td>
	            </tr>--%>
    <%--<tr>
		            <td align="left" class="style10"></td>
		            <td align="left" class="style11">
                                                
                                        
                                        <asp:DropDownList ID="DropDownListMau_File" Visible="false" onchange="setBeginRow(this);" runat="server" Width="135px" >
                                            <asp:ListItem Selected="True" >01</asp:ListItem>
                                            <asp:ListItem  >02</asp:ListItem>
                        </asp:DropDownList>
                                        </td>
	            </tr>--%>
                <tr>
		            <td align="left" class="style10">Chọn file</td>
		            <td align="left" class="style11">
                                                
                                        
                        <asp:FileUpload ID="FileUpload" runat="server" />
                                       <%-- <asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl="~/fileMau_5002.xls">File import mẫu 5002</asp:HyperLink><asp:HyperLink ID="HyperLink2" runat="server" 
                            NavigateUrl="~/fileMau_5001.xls">File import mẫu 5001</asp:HyperLink>--%>
                                        </td>
	            </tr>
                <%--<tr>
		            <td align="left" class="style10">Đội CSGT</td>
		            <td align="left" class="style11">
                                        <asp:DropDownList ID="DropDownListDoi_CSGT" runat="server" Width="321px" Height="16px">
                        </asp:DropDownList>
                                        </td>
	            </tr>--%>
                
	            <tr>
		            <td colspan="2" align="left">Lưu ý: tên file không sử dụng dấu "."</td>
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
    <asp:HiddenField ID="hidField1" runat="server" />

    </center>