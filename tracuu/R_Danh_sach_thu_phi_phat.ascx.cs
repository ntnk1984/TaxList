using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IT4U.Data;

public partial class tracuu_R_Danh_sach_thu_phi_phat : System.Web.UI.UserControl
{
    static string sBC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.imgTu_Ngay.Attributes.Add("onkeyup", "Cos_Input_Date(this)");
        this.imgTu_Ngay.Attributes.Add("onblur", "Check_Date_VN(this)");
        this.imgTu_Ngay.Attributes.Add("onclick", "displayCalendar(document.getElementById('" + this.txt_TuNgay.ClientID.ToString() + "'),'dd/mm/yyyy',this)");

        this.imgDen_Ngay.Attributes.Add("onkeyup", "Cos_Input_Date(this)");
        this.imgDen_Ngay.Attributes.Add("onblur", "Check_Date_VN(this)");
        this.imgDen_Ngay.Attributes.Add("onclick", "displayCalendar(document.getElementById('" + this.txt_DenNgay.ClientID.ToString() + "'),'dd/mm/yyyy',this)");

        this.tOk.Attributes.Add("onclick", "javascript:return KiemTra_TuNgay_DenNgay('" + this.txt_TuNgay.ClientID.ToString() + "','" + this.txt_DenNgay.ClientID.ToString() + "')");

        if (!IsPostBack)
        {
            txt_TuNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_DenNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }


    }

    private string write_Bao_cao_01()
    {
        iSqlData conn = new iSqlData("ConStr");
        DataTable dt = new DataTable();
        string ma_tt, tu_ngay, den_ngay, loai;
        decimal t_sl = 0, t_tien = 0;
        loai = "";
        tu_ngay = txt_TuNgay.Text;
        den_ngay = txt_DenNgay.Text;

        string query_tu_ngay = txt_TuNgay.Text.Trim().Substring(6, 4) + txt_TuNgay.Text.Trim().Substring(3, 2) + txt_TuNgay.Text.Trim().Substring(0, 2) + " 00:00:00";
        string query_den_ngay = txt_DenNgay.Text.Trim().Substring(6, 4) + txt_DenNgay.Text.Trim().Substring(3, 2) + txt_DenNgay.Text.Trim().Substring(0, 2) + " 23:59:00";

        loai = DropDownListloai.SelectedIndex.ToString();

        dt = conn.ExecDT("EXEC R_Danh_sach_thu_phi_phat '" + query_tu_ngay + "','" + query_den_ngay + "','"+loai+"'");




        string html = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n"
        + " <html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n"
        + " <head>\r\n"
        + " <style type=\"text/css\"> .textmode {mso-number-format:\"\\@\"}</style>"

        + " <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n"
        + " <title>Untitled Document</title>\r\n"

        + " </head>\r\n"
        + " <body>\r\n"

          + " <table width=\"90%\" height=\"152\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"  bordercolor=\"#666666\">\r\n"

            + "   <tr>\r\n"
     + "     <td width=\"10%\" ><div align=\"center\"><strong></strong></div></td>\r\n"
     + "     <td width=\"20%\"><div align=\"center\"><strong></strong></div></td>\r\n"
     + "     <td width=\"10%\" ><div align=\"center\"><strong></strong></div></td>\r\n"
     + "     <td width=\"10%\" ><div align=\"center\"><strong></strong></div></td>\r\n"
     + "     <td width=\"10%\" ><div align=\"center\"><strong></strong></div></td>\r\n"
     + "     <td width=\"10%\" ><div align=\"center\"><strong></strong></div></td>\r\n"
     + "     <td width=\"10%\" ><div align=\"center\"><strong></strong></div></td>\r\n"
     + "     <td width=\"10%\" ><div align=\"center\"><strong></strong></div></td>\r\n"
     + "     <td width=\"10%\" ><div align=\"center\"><strong></strong></div></td>\r\n"
     + "     <td width=\"10%\" ><div align=\"center\"><strong></strong></div></td>\r\n"
     + "     <td width=\"10%\" ><div align=\"center\"><strong></strong></div></td>\r\n"
     + "     <td width=\"10%\" ><div align=\"center\"><strong></strong></div></td>\r\n"
     + "     <td width=\"10%\" ><div align=\"center\"><strong></strong></div></td>\r\n"


     + "   </tr>\r\n"

     + "   <tr >\r\n"
     + "   <td colspan=\"2\" >TỔNG CÔNG TY BƯU ĐIỆN VIỆT NAM<br/> BƯU ĐIỆN TP.HỒ CHÍ MINH <br/> Số : ……………./BĐHCM - ĐSSL"
     + "   </td >\r\n"
     + "   <td colspan=\"2\"></td>"
     + "   <td colspan=\"9\" align=\"center\"><strong>CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM <br/> Độc Lập - Tự Do - Hạnh Phúc</strong>"
     + "   </td >\r\n"
     + "   </tr>"

       + "   <tr >\r\n"
     + "   <td colspan=\"13\" align=\"center\"><strong>Kính gửi: </strong><br/>Từ ngày " + tu_ngay + " đến ngày " + den_ngay
     + "   </td >\r\n"
     + "   </tr>";

        //+ "   <tr >\r\n"
        //+ "   <td colspan=\"7\" align=\"center\">Căn cứ ……………., TT.ĐSSL trình Giám đốc duyệt chuyển nộp tiền thu hộ phí phạt VPHC theo Thoả thuận số ………….. vào Ngân sách Nhà nước thông qua Ngân hàng Vietin bank số tiền " + t_tien.ToString("###,###,###") + ".(bằng chữ: " + clsPrint.ChuyenSo(t_tien.ToString()) + ". ), "
        //+ "   </td >\r\n"
        //+ "   </tr>";



        html += "</table>"

     + " <table width=\"100%\" height=\"152\" border=\"1\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"  bordercolor=\"#666666\">\r\n"
     + "   <tr>\r\n"
     + "     <td width=\"37\"><div align=\"center\"><strong>STT</strong></div></td>\r\n"
     + "     <td width=\"450\" ><div align=\"center\"><strong>Tên người vi phạm</strong></div></td>\r\n"
     + "     <td width=\"550\"height=\"28\"><div align=\"center\"><strong>Địa chỉ người nộp phạt</strong></div></td>\r\n"
     + "     <td width=\"550\"height=\"28\" ><div align=\"center\"><strong>Mã KBNN</strong></div></td>\r\n"
     + "     <td width=\"61\" ><div align=\"center\"><strong>Mã CQQĐ</strong></div></td>\r\n"
     + "     <td width=\"61\" ><div align=\"center\"><strong>Mã LHTP</strong></div></td>\r\n"
     + "     <td width=\"61\" ><div align=\"center\"><strong>TK ghi thu NSNN</strong></div></td>\r\n"
     + "     <td width=\"61\" ><div align=\"center\"><strong>Số QĐ</strong></div></td>\r\n"
     + "     <td width=\"61\" ><div align=\"center\"><strong>Ngày QĐ</strong></div></td>\r\n"
     + "     <td width=\"61\" ><div align=\"center\"><strong>Số tiền phạt</strong></div></td>\r\n"
     + "     <td width=\"61\" ><div align=\"center\"><strong>Số ngày nộp chậm</strong></div></td>\r\n"
     + "     <td width=\"61\" ><div align=\"center\"><strong>Tổng tiền</strong></div></td>\r\n"
     + "     <td width=\"61\" ><div align=\"center\"><strong>Nội dung</strong></div></td>\r\n"



     + "   </tr>\r\n";

     //+ "   <tr>\r\n"
     //+ "     <td  width=\"65\" height=\"29\"><div align=\"center\"><strong>SỐ LƯỢNG</strong></div></td>\r\n"
     //+ "     <td  width=\"20\" height=\"29\"><div align=\"center\"><strong>TIỀN</strong></div></td>\r\n"
     //+ "     <td  width=\"65\" height=\"29\"><div align=\"center\"><strong>SỐ LƯỢNG</strong></div></td>\r\n"
     //+ "     <td  width=\"20\" height=\"29\"><div align=\"center\"><strong>TIỀN</strong></div></td>\r\n"
     //+ "   </tr>\r\n";

        //tu_ngay = txt_TuNgay.Text.Trim().Substring(6, 4) + txt_TuNgay.Text.Trim().Substring(3, 2) + txt_TuNgay.Text.Trim().Substring(0, 2) + " 00:00:00";
        //den_ngay = txt_DenNgay.Text.Trim().Substring(6, 4) + txt_DenNgay.Text.Trim().Substring(3, 2) + txt_DenNgay.Text.Trim().Substring(0, 2) + " 23:59:00";

        //dt = conn.ExecDT("EXEC R_Danh_Sach_Nop_VPHC_Vao_NSNN '" + tu_ngay + "','" + den_ngay + "'");

        if (dt.Rows.Count > 0)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string bstrong, estrong,space;
                bstrong = "";
                estrong = "";
                space = "";
                if (dt.Rows[j]["sys_total"].ToString() == "0")
                {
                    bstrong = "<strong>";
                    estrong = "</strong>";
                }

                switch (dt.Rows[j]["sys_order"].ToString())
                {
                    case "1":
                        space = "";
                        break;
                    case "2":
                        space = "&nbsp;&nbsp;";
                        break;
                    case "3":
                        space = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        break;
                }
                    


                
                html += " <tr>"+bstrong;
                html += "     <td>" + dt.Rows[j]["stt"].ToString() + "</td>";
                html += "     <td><div align=\"left\">"+bstrong+ space + dt.Rows[j]["ho_ten"].ToString() +estrong+ "</div></td>";
                html += "     <td><div align=\"left\">" + dt.Rows[j]["dia_chi"].ToString().Trim() + "</div></td>";
                html += "     <td><div align=\"left\">" + dt.Rows[j]["ma_kbnn"].ToString().Trim() + "</div></td>";
                html += "     <td><div align=\"left\">" + dt.Rows[j]["ma_cqqd"].ToString().Trim() + "</div></td>";
                html += "     <td><div align=\"left\" class=\"textmode\">" + dt.Rows[j]["ma_lh"].ToString().Trim() + "</div></td>";
                html += "     <td><div align=\"left\" class=\"textmode\">" + dt.Rows[j]["tk_thu"].ToString().Trim() + "</div></td>";
                html += "     <td><div align=\"left\" class=\"textmode\">" + dt.Rows[j]["so_qd_xu_phat"].ToString().Trim() + "</div></td>";
                html += "     <td><div align=\"left\">" + dt.Rows[j]["ngay_qd_xu_phat"].ToString().Trim() + "</div></td>";
                html += "     <td>"+bstrong + String.Format("{0:N0}", Math.Round(double.Parse(dt.Rows[j]["tien_phat_vp"].ToString()), 2)) + estrong+"</td>";
                html += "     <td><div align=\"left\">" + dt.Rows[j]["so_ngay_nop_cham"].ToString().Trim() + "</div></td>";
                html += "     <td>"+bstrong + String.Format("{0:N0}", Math.Round(double.Parse(dt.Rows[j]["tong_tien"].ToString()), 2)) + estrong+"</td>";
                html += "     <td> </td>";

                html += estrong+" </tr>";

            }

        }
        html += ""

       + "  </tr>\r\n"
       + " </table>\r\n"

   //    + " <table width=\"100%\" height=\"152\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"  bordercolor=\"#666666\">\r\n"
   //    + "<tr class=\"noborder\">"
   //      + " <td colspan=\"7\"><div align=\"left\"><strong>Đính kèm Danh sách nộp phí phạt số ……………...,  ngày …..…/…...…/…...…. ";
   //     html += ""
   //  + "</strong></div></td>\r\n"
   //  + "</tr>"

   //  + "<tr>"
   //   + " <td colspan=\"1\"><div align=\"left\"><strong></strong></div></td> ";
   //     html += ""
   //   + " <td colspan=\"6\"><div align=\"left\"><strong>Trân trọng trình./. </strong></div></td>";
   //     html += ""
   //  + "</tr>"

   //   + "<tr>"

   //   + " <td colspan=\"2\"><div align=\"center\"><strong>Người lập</strong></div></td>";
   //     html += ""
   //    + " <td colspan=\"2\"><div align=\"center\"><strong>Trưởng TTĐSSL</strong></div></td>";
   //     html += ""
   //+ " <td colspan=\"2\"><div align=\"center\"><strong>Kế toán trưởng</strong></div></td>";
   //     html += ""
   //+ " <td colspan=\"1\"><div align=\"center\"><strong>Giám đốc</strong></div></td>";
   //     html += ""
   //  + "</tr>"

   //    + " </table>\r\n"

       + " <p>&nbsp;</p>"
       + " </body>\r\n"
       + " </html>";

        conn.Close();
        conn.Dispose();

        return html;
    }

    protected void tOk_Click(object sender, EventArgs e)
    {
        LabelR.Text = write_Bao_cao_01();
        sBC = LabelR.Text;
    }

    protected void tExport_Click(object sender, EventArgs e)
    {
        LabelR.Text = sBC;
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=danh_sach_thu_phi_phat.xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        LabelR.RenderControl(htmlWrite);
        Response.Output.Write(stringWrite.ToString());
        Response.Flush();
        Response.End();
    }
}