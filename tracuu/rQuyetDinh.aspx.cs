using System;
using System.Collections;
using System.Configuration;
using System.Threading;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class rQuyetDinh : System.Web.UI.Page
{
    public string ChkHTML = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string sID = ClsTools.Tools.FormatInput((Request.QueryString["ID"] != null ? Request.QueryString["ID"].ToString().Trim() : ""));
            lb_baocaoview.Text = ViewBC(sID, "vi-VN");
        }
    }
    private string ViewBC(string sID, string sFormat)
    {
        string str = "";

        //int Rpan;
        //int R1pan, R2pan;
        //int count = 7; //So cot

        DateTime dtnow = DateTime.Now;
        Thread.CurrentThread.CurrentCulture = new CultureInfo(sFormat);

        //Rpan = Convert.ToInt32(count / 2);
        //R1pan = count - Rpan;
        //R2pan = Convert.ToInt32((count - Rpan) / 3);

        string strConn;
        SqlConnection conn; // = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand();


        strConn = ConfigurationSettings.AppSettings["ConStr"];
        conn = new SqlConnection(strConn);
        conn.Open();

        cmd.CommandText = "rQUYET_DINH";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = conn;

        SqlParameter pID = new SqlParameter("ID", SqlDbType.VarChar, 50);
        pID.Direction = ParameterDirection.Input;
        pID.Value = sID;

        cmd.CommandTimeout = 0;
        cmd.Parameters.Add(pID);

        SqlDataReader rdr = null;
        try
        {
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                
                #region View báo cáo
                #region Table khung
                
                str += "<table border='1' width='780px' id='tbtTieude' bordercolorlight='#808080' cellspacing='0' cellpadding='0' bordercolordark='#808080' style='border-collapse: collapse'>";


                //  khung 1
                str += "<tr>";
                str += "<td align='center'>";
                #region table 1
                str += "<table border='0' width='98%' id='tbtT1' cellspacing='0' cellpadding='10'>";
                str += "<tr >";
                str += "<td width='10%' align='left' > <img border='0' src='../images/vnpost.gif'></td>";
                str += "<td width='30%' align='center'><font face='Times New Roman' size='2'><b>BƯU ĐIỆN </BR>TP. HỒ CHÍ MINH</b></br>Hot line 08-39247247</font> </td>";
                str += "<td width='60%' align='center'><p><b><i><font face='Times New Roman' size='4'>Phiếu xác nhận thông tin dịch vụ</BR>nộp hộ tiền phạt vi phạm giao thông qua Bưu điện</font></i></b><p></td>";
                str += "</tr>";
                str += "</table>";
                #endregion
                str += "</td>";
                str += "</tr>";

                // khung 2
                str += "<tr>";
                str += "<td align='center'>";
                #region table 2
                str += "<font face='Times New Roman' size='3'>";
                str += "<table border='0' width='98%' id='tbtT2' cellspacing='0' cellpadding='10'>";
                str += "<tr>";
                str += "<td align='left'><font face='Times New Roman' size='3'><b>Số Biên bản vi phạm: </b> " + rdr["SO_QD"].ToString() + "</font> </td>";
                str += "</tr>";
                str += "<tr>";
                str += "<td align='left'><font face='Times New Roman' size='3'><b>Số Quyết định xử phạt: </b> " + rdr["SO_QD"].ToString() + "</font> </td>";
                str += "</tr>";
                str += "<tr>";
                str += "<td align='left'><font face='Times New Roman' size='3'><b>Họ tên người vi phạm: </b> " + rdr["NGUOI_VP"].ToString() + "</font> </td>";
                str += "</tr>";
                str += "<tr>";
                str += "<td align='left'><font face='Times New Roman' size='3'><b>Số tiền phạt trên QĐ xử phạt: </b> " + (rdr["SO_TIEN"].ToString() != "" ? Convert.ToInt64(rdr["SO_TIEN"].ToString()).ToString("#,#") : "") + "</font> </td>";
                str += "</tr>";
                str += "<tr>";
                str += "<td align='left'><font face='Times New Roman' size='3'><b>Biển số xe: </b> " + rdr["SO_XE"].ToString() + "</font> </td>";
                str += "</tr>";
                str += "</table>";
                str += "</font>"; 
                #endregion
                str += "</td>";
                str += "</tr>";

                str += "</table>";             
                #endregion
                str += "<p>";    
                // NGAY THANG NAM
                #region table 7
                str += "<table border='0' width='780px' id='tbThangNam' cellspacing='0' cellpadding='0'>";
                str += "<tr>";
                str += "<td width='40%' valign='top' align='left'></td>";
                str += "<td width='60%' valign='top' align='center'><font face='Times New Roman' size='3'>TP. Hồ Chí Minh, ngày ........../........../201.....";
                str += "</font></td>";
                str += "</tr>";
                str += "<tr>";
                str += "<td width='40%' valign='top' align='left'></td>";
                str += "<td width='60%' valign='top' align='center'><font face='Times New Roman' size='3'><b>Người yêu cầu</b>";
                str += "</font></td>";
                str += "</tr>";
                str += "<tr>";
                str += "<td width='40%' valign='top' align='left'></td>";
                str += "<td width='60%' valign='top' align='center'><font face='Times New Roman' size='3'><i>(Ký tên, ghi rõ họ tên)</i>";
                str += "</font></td>";
                str += "</tr>";
                str += "</table>";
                #endregion
                #endregion

                

            }
            rdr.Close();
        }
        finally
        {
        }

        return str;
    }
}
