<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dmThuPhat.ascx.cs" Inherits="TTGTTP.dmThuPhat" %>

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

<script type="text/javascript">
    $(document).ready(function () {
        $('#exampleModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget) // Button that triggered the modal
            var recipient = button.data('whatever') // Extract info from data-* attributes
            // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
            // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
            var modal = $(this)
            modal.find('.action').val(recipient)
        });
    });
    
</script>
<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" data-whatever="New">Thêm mới</button>

<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="exampleModalLabel">Nhập thông tin thu phí phạt qua ngân hàng</h4>
      </div>
      <div class="modal-body">
        <form>
            <table border="0" width="600" cellpadding="6" id="tblCation" style="font-family: Arial; font-size: 10pt; border-collapse: collapse">
	        <tr>
		        <td colspan="2" align="center">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                </td>
	        </tr>
	      
	         <tr>
		            <td align="left" class="style10">TK ghi thu</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="textboxTK_NS" class="tk_ns" runat="server" Width="270px"></asp:TextBox>
                                        </td>
	            </tr>
	            <tr>
		            <td align="left" class="style10">Mã LH</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="textboxMA_LH" class="ma_lh" runat="server" Width="270px"></asp:TextBox>
                                        </td>
	            </tr>
	            <tr>
		            <td align="left" class="style10">Mã CQQĐ</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="textboxMA_CQQD" class="ma_cqqd" runat="server" Width="270px"></asp:TextBox>
                                        </td>
	            </tr>
                 <tr>
		            <td align="left" class="style10">Mã PayPost</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="textboxMa_PAYPOST" class="ma_paypost" runat="server" Width="270px"></asp:TextBox>
                                        </td>
	            </tr>
                 <tr>
		            <td align="left" class="style10">Mã KBNN</td>
		            <td align="left" class="style11">
                        <asp:TextBox ID="textboxMA_KBNN" class="ma_kbnn" runat="server" Width="270px"></asp:TextBox>
                                        </td>
	            </tr>
               
               <tr>
		            <td align="left" class="style10">Thao tác</td>
		            <td align="left" class="style11">
                        
                        
                        
                        <asp:TextBox ID="textboxAction" class="action" runat="server" Width="270px"></asp:TextBox>
                        
                        
                        
                                        </td>
	            </tr>
	        </table>

            <div class="form-group">
            &nbsp;</div>
        </form>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Quay lại</button>
        <asp:Button runat="server" type="button" onclick="tOk_Click" class="btn btn-primary" style="width:150px;" Text="Lưu"/>
      </div>
    </div>
  </div>
</div>

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
                                            NavigateUrl='<%# "../Main.aspx?P=SuaDmThuPhat.ascx&ma_paypost=" + Eval("ma_paypost")%>' 
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
