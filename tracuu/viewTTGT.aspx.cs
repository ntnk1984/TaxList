using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;


public partial class viewTTGT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.imgTu_Ngay.Attributes.Add("onkeyup", "Cos_Input_Date(this)");
        this.imgTu_Ngay.Attributes.Add("onblur", "Check_Date_VN(this)");
        this.imgDen_Ngay.Attributes.Add("onkeyup", "Cos_Input_Date(this)");
        this.imgDen_Ngay.Attributes.Add("onblur", "Check_Date_VN(this)");
        this.tOk.Attributes.Add("onclick", "javascript:return KiemTra_TuNgay_DenNgay('" + this.txt_TuNgay.ClientID.ToString() + "','" + this.txt_DenNgay.ClientID.ToString() + "')");
            
    }
   
    protected void tOk_Click(object sender, EventArgs e)
    {
        DateTime dNow = DateTime.Now;
        // kiem tra da nhap du lieu du hay chua
        this.Label1.Text = "";
        if ((tSO_QD.Text.Trim().Length < 1) && (t_NGUOI_VP.Text.Trim().Length < 1) && (tSO_XE.Text.Trim().Length < 1))
        {
            this.Label1.Text = "<p> Vui lòng nhập điều kiện tìm kiếm.";
            return;
        }

        LoadDataList();
    }
    private void LoadDataList()
    {
        string strSQL = sqlStringFind(1);
        
        string strConn;
        strConn = ConfigurationSettings.AppSettings["ConStr"];
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        SqlDataReader rdr = null;
        DataTable dt = new DataTable();
        try
        {
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);

            if (dt.Rows.Count > 0)
            {
                this.Label1.Text = "Tổng số tin tìm được: " + dt.Rows.Count.ToString() + ".";
                this.DANHMUC.DataSource = dt;
                this.DANHMUC.DataBind();
            }
            else
            {
                this.DANHMUC.Controls.Clear();
                this.Label1.Text = "Tổng số tin tìm được: 0.";
            }
            dt.Dispose();
        }
        catch (Exception e)
        {
            Response.Write(e.Message);
            Response.End();
        }
    }
    protected string sqlStringFind(int iLoai)
    {
        string strSQL = "";
        string strSQL2 = "";

        strSQL = "SELECT QD.[ID], QD.[SO_QD], CONVERT(varchar,QD.[NGAY_BB], 103) AS [NGAY_BB], QD.[NGUOI_VP], QD.[SO_XE], QD.[SO_TIEN], CONVERT(varchar,QD.[TUOC_GPLX_DEN_NGAY], 103) AS [TUOC_GPLX_DEN_NGAY], CONVERT(varchar,QD.[NGAY_QD], 103) AS [NGAY_QD] ";
        strSQL += "  FROM QUYET_DINH QD with(nolock) ";
        strSQL += "  where ";

        if (tSO_QD.Text.ToString().Trim().Length > 0)
            strSQL2 = "QD.SO_QD = '" + tSO_QD.Text.ToString().Trim() + "'";

        if (tSO_XE.Text.ToString().Trim().Length > 0)
            if (strSQL2 =="")
                strSQL2 = "QD.SO_XE = '" + tSO_XE.Text.ToString().Trim() + "'";
            else
                strSQL2 += " AND QD.SO_XE = '" + tSO_XE.Text.ToString().Trim() + "'";

        if (t_NGUOI_VP.Text.ToString().Trim().Length > 0)
            if (strSQL2 == "")
                strSQL2 = "QD.NGUOI_VP = N'" + t_NGUOI_VP.Text.ToString().Trim() + "'";
            else
                strSQL2 += " AND QD.NGUOI_VP = N'" + t_NGUOI_VP.Text.ToString().Trim() + "'";

        ////ngay dk// 
        if (txt_TuNgay.Text != "" && txt_DenNgay.Text != "")
        {
            string sTu_Ngay = ClsTools.Tools.FormatInput(this.txt_TuNgay.Text);
            string sDen_Ngay = ClsTools.Tools.FormatInput(this.txt_DenNgay.Text);
            sTu_Ngay = ClsTools.Tools.FormatDateEN(sTu_Ngay).Substring(0, 10) + " 00:00:00";
            sDen_Ngay = ClsTools.Tools.FormatDateEN(sDen_Ngay).Substring(0, 10) + " 23:59:59";

            if (strSQL2 == "")
                strSQL2 = "(QD.NGAY_BB >='" + sTu_Ngay + "' AND QD.NGAY_BB <='" + sDen_Ngay + "' ) ";
            else
                strSQL2 += "  AND  (QD.NGAY_BB >='" + sTu_Ngay + "' AND QD.NGAY_BB <='" + sDen_Ngay + "' ) ";
        }
        strSQL = strSQL + strSQL2 + " order by QD.NGAY_BB DESC";
        return strSQL;
    } 
}
