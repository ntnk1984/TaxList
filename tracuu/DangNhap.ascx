<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DangNhap.ascx.cs" Inherits="tracuu_DangNhap" %>

<table width="100%" height="112"  border="0" cellpadding="0" cellspacing="0">
  
  
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
		        <p align="left"><b><font color="#FF9900">Thông tin đăng nhập</font>&nbsp;</b></td>
	        </tr>
	        <tr>
		            <td align="left" class="style10">Tên đăng nhập</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="txt_username" runat="server" Width="135px"></asp:TextBox>
                        &nbsp;</td>
	            </tr>
	            <tr>
		            <td align="left" class="style10">Mật khẩu</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="txt_pass" runat="server" Width="135px" TextMode="Password">></asp:TextBox>
                                        </td>
	            </tr>
	            
                
                
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="tOk" runat="server" onclick="tOk_Click" Text="Đăng nhập" />
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
                                
                                <asp:TemplateField HeaderText="Số quyết định">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="VIEW_1" runat="server" 
                                            NavigateUrl='<%# "rQuyetDinh.aspx?ID=" + Eval("ID")%>' 
                                            Text='<%# Eval("SO_QD")%>' ToolTip="Nhắp chuột vào đây để xem">
					                </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="true" HorizontalAlign="center" Width="50" />
                                </asp:TemplateField>
                                <%--<asp:BoundField AccessibleHeaderText="Số quyết định" DataField="SO_QD" 
                                    HeaderText="Số quyết định">
                                    <ItemStyle HorizontalAlign="left"  />
                                </asp:BoundField>--%>
                                <asp:BoundField AccessibleHeaderText="Ngày ra QĐ" DataField="NGAY_QD" 
                                    HeaderText="Ngày ra QĐ">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="Ngày lập BB" DataField="NGAY_BB" 
                                    HeaderText="Ngày lập BB">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="Tên vi phạm" DataField="NGUOI_VP" 
                                    HeaderText="Tên vi phạm">
                                    <ItemStyle HorizontalAlign="left"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="Số xe" DataField="SO_XE" 
                                    HeaderText="Số xe">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>
                                <%--<asp:BoundField AccessibleHeaderText="Tiền phạt" DataField="SO_TIEN" 
                                    HeaderText="Tiền phạt">
                                    <ItemStyle HorizontalAlign="right"  />
                                </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Tiền phạt">
                                    <ItemTemplate>
                                        <asp:Label ID="SO_TIEN" runat="server" Text='<%# Eval("SO_TIEN", "{0:0,00}")%>'  ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="true" HorizontalAlign="right" Width="50" />
                                </asp:TemplateField>
                                <asp:BoundField AccessibleHeaderText="Tước GPLX đến" DataField="TUOC_GPLX_DEN_NGAY" 
                                    HeaderText="Tước GPLX đến">
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
