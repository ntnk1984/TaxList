<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SuaDmThuPhat.ascx.cs" Inherits="TTGTTP.SuaDmThuPhat" %>

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
		        <p align="left"><b><font color="#FF9900">Nhập thông tin thu phí phạt qua ngân hàng</font>&nbsp;</b></td>
	        </tr>
	        <tr>
		            <td align="left" class="style10">TK ghi thu</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="textboxTK_NS" runat="server" Width="270px"></asp:TextBox>
                                        </td>
	            </tr>
	            <tr>
		            <td align="left" class="style10">Mã LH</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="textboxMA_LH" runat="server" Width="270px"></asp:TextBox>
                                        </td>
	            </tr>
	            <tr>
		            <td align="left" class="style10">Mã CQQĐ</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="textboxMA_CQQD" runat="server" Width="270px"></asp:TextBox>
                                        </td>
	            </tr>
                 <tr>
		            <td align="left" class="style10">Mã PayPost</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="textboxMa_PAYPOST" runat="server" Width="270px"></asp:TextBox>
                                        </td>
	            </tr>
                 <tr>
		            <td align="left" class="style10">Mã KBNN</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="textboxMA_KBNN" runat="server" Width="270px"></asp:TextBox>
                                        </td>
	            </tr>
                    <tr>
		            <td align="left" class="style10">BĐH</td>
		            <td align="left" class="style11">
                        
                        <asp:TextBox ID="textboxBDH" runat="server" Width="270px"></asp:TextBox>
                        
                                        </td>
	            </tr>

                 

                 

                

                 

               
                
                 
                 

                 

                  
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="tOk" runat="server" onclick="tOk_Click" Text="Lưu" Width="111px" />
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
                            DataKeyNames="ma_paypost" HeaderStyle-CssClass="header" 
                            meta:resourcekey="DANHMUCResource" 
                            PagerSettings-PageButtonCount="10" PagerStyle-HorizontalAlign="Right" 
                            RowStyle-CssClass="normal" Width="98%">
                            <RowStyle CssClass="normal" />
                            <Columns>
                                <asp:TemplateField HeaderText="Mã Paypost" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ma_paypost")%>'> </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="true" HorizontalAlign="left" Width="0%" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Mã PayPost">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="VIEW_1" runat="server" 
                                            NavigateUrl='<%# "rQuyetDinh.aspx?ma_paypost=" + Eval("ma_paypost")%>' 
                                            Text='<%# Eval("ma_paypost")%>' ToolTip="Nhắp chuột vào đây để xem">
					                </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="true" HorizontalAlign="center" Width="50" />
                                </asp:TemplateField>
                                <%--<asp:BoundField AccessibleHeaderText="Số quyết định" DataField="SO_QD" 
                                    HeaderText="Số quyết định">
                                    <ItemStyle HorizontalAlign="left"  />
                                </asp:BoundField>--%>
                                <asp:BoundField AccessibleHeaderText="Mã CQQĐ" DataField="ma_cqqd" 
                                    HeaderText="Mã CQQĐ">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="TK ghi thu" DataField="tk_ns" 
                                    HeaderText="TK ghi thu">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="Mã KBNN" DataField="ma_kbnn" 
                                    HeaderText="Mã KBNN">
                                    <ItemStyle HorizontalAlign="left"  />
                                </asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="BĐH" DataField="bdh" 
                                    HeaderText="BĐH">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>
                                <%--<asp:BoundField AccessibleHeaderText="Tiền phạt" DataField="SO_TIEN" 
                                    HeaderText="Tiền phạt">
                                    <ItemStyle HorizontalAlign="right"  />
                                </asp:BoundField>--%>
                               <%-- <asp:TemplateField HeaderText="Tiền phạt">
                                    <ItemTemplate>
                                        <asp:Label ID="SO_TIEN" runat="server" Text='<%# Eval("SO_TIEN", "{0:0,00}")%>'  ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="true" HorizontalAlign="right" Width="50" />
                                </asp:TemplateField>--%>
                              <%--  <asp:BoundField AccessibleHeaderText="Tước GPLX đến" DataField="TUOC_GPLX_DEN_NGAY" 
                                    HeaderText="Tước GPLX đến">
                                    <ItemStyle HorizontalAlign="center"  />
                                </asp:BoundField>--%>
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
