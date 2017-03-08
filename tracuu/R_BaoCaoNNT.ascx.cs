using IT4U.Data;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class tracuu_R_BaoCaoNNT : System.Web.UI.UserControl
{
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

            //ReportViewer1.SizeToReportContent = true;
            ReportViewer1.ZoomMode = ZoomMode.Percent;
            ReportViewer1.ZoomPercent = 100;
            LoadTrungTam();

        }
    }

    private void LoadTrungTam()
    {
        iSqlData con = new iSqlData("ConStr");
        DataTable dt = new DataTable();
        string sql;
        try
        {
            sql = "select MaTrungTam,TenTrungTam from DMTrungTam with(nolock) order by MaTrungTam";
            dt = con.ExecDT(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dtRowSP;
                dtRowSP = dt.NewRow();
                dtRowSP[1] = "Tất cả";
                dtRowSP[0] = 0;
                dt.Rows.InsertAt(dtRowSP, 0);

                this.cboTrungTam.DataSource = dt;
                this.cboTrungTam.DataTextField = "TenTrungTam";
                this.cboTrungTam.DataValueField = "MaTrungTam";
                this.cboTrungTam.DataBind();
                this.cboTrungTam.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Response.Write("Có lỗi: " + ex.Message.ToString());
            Response.End();
        }
        finally
        {
            dt.Dispose();
            dt = null;
            con.Close();
            con.Dispose();
            con = null;
        }
    }
    protected void tOk_Click(object sender, EventArgs e)
    {
        iSqlData con = new iSqlData("ConStr");
        DataTable dt = new DataTable();
        string sql;
        try
        {
            string sTuNgay, sDenNgay, sMaTrungTam, sMaBuuCuc, sTrangThai;
            sMaTrungTam = this.cboTrungTam.SelectedValue;
            sMaBuuCuc = this.cboBuuCuc.SelectedValue;
            sTuNgay = IT4U.iDateTime.Format_Date_EN(this.txt_TuNgay.Text);
            sDenNgay = IT4U.iDateTime.Format_Date_EN(this.txt_DenNgay.Text);
            sTrangThai = this.cboTrangThai.SelectedValue;

            ReportViewer1.Visible = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/tracuu/Report.rdlc");
            sql = "select * from DanhSachNNT with(nolock)";
            sql += " where ";
            sql += " convert(date, NgayImport) >= convert(date, '" + sTuNgay + "') and convert(date, NgayImport) <= convert(date, '" + sDenNgay + "')";
            if (sMaTrungTam != "0" && sMaTrungTam != "") sql += " and MaTrungTam='" + sMaTrungTam + "'";
            if (sMaBuuCuc != "0" && sMaBuuCuc != "") sql += " and MaBuuCuc='" + sMaBuuCuc + "'";
            if (sTrangThai != "0" && sTrangThai != "") sql += " and TrangThai='" + sTrangThai + "'";
            sql += " order by MaNNT,TieuMuc";
            dt = con.ExecDT(sql);
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.Refresh();
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            dt.Dispose();
            dt = null;
            con.Close();
            con.Dispose();
            con = null;
        }
    }


    private void LoadBuuCuc(string p)
    {
        iSqlData con = new iSqlData("ConStr");
        DataTable dt = new DataTable();
        string sql;
        try
        {
            sql = "select MaBuuCuc,TenBuuCuc from DMBuuCuc with(nolock) where MaTrungTam='" + p + "' order by TenBuuCuc";
            dt = con.ExecDT(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dtRowSP;
                dtRowSP = dt.NewRow();
                dtRowSP[1] = "Tất cả";
                dtRowSP[0] = 0;
                dt.Rows.InsertAt(dtRowSP, 0);

                this.cboBuuCuc.DataSource = dt;
                this.cboBuuCuc.DataTextField = "TenBuuCuc";
                this.cboBuuCuc.DataValueField = "MaBuuCuc";
                this.cboBuuCuc.DataBind();
                this.cboBuuCuc.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Response.Write("Có lỗi: " + ex.Message.ToString());
            Response.End();
        }
        finally
        {
            dt.Dispose();
            dt = null;
            con.Close();
            con.Dispose();
            con = null;
        }
    }
    protected void cboTrungTam_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBuuCuc(this.cboTrungTam.SelectedValue);
    }
}