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
using OfficeOpenXml;
using OfficeOpenXml.Style;

public partial class exportTTGT : System.Web.UI.Page
{
    static DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.imgTu_Ngay.Attributes.Add("onkeyup", "Cos_Input_Date(this)");
        this.imgTu_Ngay.Attributes.Add("onblur", "Check_Date_VN(this)");
        this.imgDen_Ngay.Attributes.Add("onkeyup", "Cos_Input_Date(this)");
        this.imgDen_Ngay.Attributes.Add("onblur", "Check_Date_VN(this)");
        this.tOk.Attributes.Add("onclick", "javascript:return KiemTra_TuNgay_DenNgay('" + this.txt_TuNgay.ClientID.ToString() + "','" + this.txt_DenNgay.ClientID.ToString() + "')");

        if (!IsPostBack)
        {
            txt_TuNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_DenNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }
        

    }
   
    protected void tOk_Click(object sender, EventArgs e)
    {
        DateTime dNow = DateTime.Now;
        // kiem tra da nhap du lieu du hay chua
        this.Label1.Text = "";
        //if ((tSO_QD.Text.Trim().Length < 1) && (t_NGUOI_VP.Text.Trim().Length < 1) && (tSO_XE.Text.Trim().Length < 1))
        //{
        //    this.Label1.Text = "<p> Vui lòng nhập điều kiện tìm kiếm.";
        //    return;
        //}

        LoadDataList();
    }

    protected void tExport_Click(object sender, EventArgs e)
    {
        DumpExcel(dt);
    }

    private void DumpExcel(DataTable tbl)
    {
        using (ExcelPackage pck = new ExcelPackage())
        {
            //Create the worksheet
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Baocao");

            //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells["A1"].LoadFromDataTable(tbl, true);

            //Format the header for column 1-3
            using (ExcelRange rng = ws.Cells["A1:M1"])
            {
                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White);
            }

            //Example how to Format Column 1 as numeric 
            using (ExcelRange col = ws.Cells[2, 1, 2 + tbl.Rows.Count, 1])
            {
                col.Style.Numberformat.Format = "#,##0.00";
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }

            //Write it back to the client
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;  filename=phat_hanh.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
        }
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
        //DataTable dt = new DataTable();
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

        strSQL = "SELECT SO_XE BKS, QD.[NGUOI_VP] HO_TEN, ''  DIA_CHI, '' SO_DIEN_THOAI, '' LOAI_GIAY_TO, '' SO_GIAY_TO, '' NGAY_CAP, '' NOI_CAP, QD.[SO_TIEN] TIEN_PHAT_VP, SO_QD SO_QD_XU_PHAT, CONVERT(varchar,QD.[NGAY_QD], 103) AS NGAY_QD_XU_PHAT, '' GTTG, '' GHI_CHU ";
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
                strSQL2 = "(QD.NGAY_QD >='" + sTu_Ngay + "' AND QD.NGAY_QD <='" + sDen_Ngay + "' ) ";
            else
                strSQL2 += "  AND  (QD.NGAY_QD >='" + sTu_Ngay + "' AND QD.NGAY_QD <='" + sDen_Ngay + "' ) ";
        }
        strSQL = strSQL + strSQL2 + " order by QD.NGAY_QD DESC";
        return strSQL;
    } 
}
