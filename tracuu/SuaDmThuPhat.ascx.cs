using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IT4U.Data;

namespace TTGTTP
{
    public partial class SuaDmThuPhat : System.Web.UI.UserControl
    {
        iSqlData con = new iSqlData("ConStr");
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.imgTu_Ngay0.Attributes.Add("onclick", "displayCalendar(document.getElementById('" + this.textboxNgay_QD_Xu_Phat.ClientID.ToString() + "'),'dd/mm/yyyy',this)");
            //this.imgTu_Ngay0.Attributes.Add("onkeyup", "Cos_Input_Date(this)");
            //this.imgTu_Ngay0.Attributes.Add("onblur", "Check_Date_VN(this)");
            string ma_pp = Request.QueryString["ma_paypost"];

            DataTable dt = con.ExecDT("EXEC get1DM_Thu_Phat '" + ma_pp + "'");

            textboxTK_NS.Text = dt.Rows[0]["tk_ns"].ToString();
            textboxMA_LH.Text = dt.Rows[0]["ma_lh"].ToString();
            textboxMA_CQQD.Text = dt.Rows[0]["ma_cqqd"].ToString();
            textboxMa_PAYPOST.Text = dt.Rows[0]["ma_paypost"].ToString();
            textboxMA_KBNN.Text = dt.Rows[0]["ma_kbnn"].ToString();
            textboxBDH.Text = dt.Rows[0]["bdh"].ToString();

            if (!IsPostBack)
            {

            }
        }

        void loadData(string ma_pp)
        {
           
        }

        protected void tOk_Click(object sender, EventArgs e)
        {
            string strConn = ConfigurationSettings.AppSettings["ConStr"];
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            string strCmd = "";
            string sNgay_BB = "", sNgay_QD = "";

            if (string.IsNullOrEmpty(Session["TenDangNhap"].ToString()))
            {
                Response.Redirect("~/Main.aspx?P=DangNhap");
                return;
            }

           


            try
            {
                //for (int k = 0; k < dt.Rows.Count; k++)
                {

                    //strCmd = "INSERT INTO DM_QUYET_DINH (BKS, HO_TEN, DIA_CHI, SO_DIEN_THOAI, LOAI_GIAY_TO, SO_GIAY_TO, NGAY_CAP, NOI_CAP, TIEN_PHAT_VP, SO_QD_XU_PHAT, NGAY_QD_XU_PHAT, GTTG, GHI_CHU, NGAY_HE_THONG,USERNAME, MA_DOI)";
                    //strCmd += " values(";
                    //strCmd += " N'" + ClsTools.Tools.FormatInput(textboxBKS.Text) + "',";
                    //strCmd += " '" + ClsTools.Tools.FormatInput(textboxHo_Ten.Text) + "',";
                    //strCmd += " N'" + ClsTools.Tools.FormatInput(textboxDia_Chi.Text) + "',";
                    //strCmd += " N'" + ClsTools.Tools.FormatInput(textboxSo_Dien_Thoai.Text) + "',";
                    //strCmd += " " + ClsTools.Tools.FormatInput(textboxLoai_Giay_To.Text) + ",";
                    //strCmd += " N'" + ClsTools.Tools.FormatInput(textboxSo_Giay_To.Text) + "',";
                    //strCmd += " N'" + ClsTools.Tools.FormatInput(textboxNgay_Cap.Text) + "',";
                    //strCmd += " N'" + ClsTools.Tools.FormatInput(textboxNoi_Cap.Text) + "',";
                    //strCmd += " N'" + ClsTools.Tools.FormatInput(textboxTien_Phat_VP.Text) + "',";
                    //strCmd += " N'" + ClsTools.Tools.FormatInput(textboxSo_QD_Xu_Phat.Text) + "',";
                    //strCmd += " N'" + sNgay_QD + "',";
                    //strCmd += " N'" + ClsTools.Tools.FormatInput(textboxGTTG.Text) + "',";
                    //strCmd += " N'" + ClsTools.Tools.FormatInput(textboxGhi_Chu.Text) + "',";
                    //strCmd += " Getdate(),";
                    //strCmd += " '" + Session["TenDangNhap"] + "' ";
                    //strCmd += ", '" + ClsTools.Tools.FormatInput(textboxMa_Doi.Text) + "') ";

                    //cmd.CommandText = strCmd;
                    //cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.Connection = conn;

                    try
                    {
                        cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Label1.Text = "Lỗi " + ex.Message;
                    }
                }
                Label1.Text = "Giao dịch hoàn thành.";

                foreach(TextBox o in this.Controls)
                {
                    o.Text = "";
                }

            }
            catch (Exception ex)
            {
                Label1.Text = "Lỗi " + ex.Message;
                //return;
            }
        }
    }

}