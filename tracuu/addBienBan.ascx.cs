using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TTGTTP
{
    public partial class addBienBan : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.imgTu_Ngay0.Attributes.Add("onclick", "displayCalendar(document.getElementById('" + this.textboxNgay_QD_Xu_Phat.ClientID.ToString() + "'),'dd/mm/yyyy',this)");
            this.imgTu_Ngay0.Attributes.Add("onkeyup", "Cos_Input_Date(this)");
            this.imgTu_Ngay0.Attributes.Add("onblur", "Check_Date_VN(this)");

            if (!IsPostBack)
            {
                //new 

            }
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

            if (textboxNgay_QD_Xu_Phat.Text != "")
            {
                sNgay_QD = ClsTools.Tools.FormatInput(this.textboxNgay_QD_Xu_Phat.Text);
                sNgay_QD = ClsTools.Tools.FormatDateEN(sNgay_QD).Substring(0, 10) + " 00:00:00";
            }


            try
            {
                //for (int k = 0; k < dt.Rows.Count; k++)
                {

                    strCmd = "INSERT INTO DM_QUYET_DINH (BKS, HO_TEN, DIA_CHI, SO_DIEN_THOAI, LOAI_GIAY_TO, SO_GIAY_TO, NGAY_CAP, NOI_CAP, TIEN_PHAT_VP, SO_QD_XU_PHAT, NGAY_QD_XU_PHAT, GTTG, GHI_CHU, NGAY_HE_THONG,USERNAME, MA_DOI)";
                    strCmd += " values(";
                    strCmd += " N'" + ClsTools.Tools.FormatInput(textboxBKS.Text) + "',";
                    strCmd += " '" + ClsTools.Tools.FormatInput(textboxHo_Ten.Text) + "',";
                    strCmd += " N'" + ClsTools.Tools.FormatInput(textboxDia_Chi.Text) + "',";
                    strCmd += " N'" + ClsTools.Tools.FormatInput(textboxSo_Dien_Thoai.Text) + "',";
                    strCmd += " " + ClsTools.Tools.FormatInput(textboxLoai_Giay_To.Text) + ",";
                    strCmd += " N'" + ClsTools.Tools.FormatInput(textboxSo_Giay_To.Text) + "',";
                    strCmd += " N'" + ClsTools.Tools.FormatInput(textboxNgay_Cap.Text) + "',";
                    strCmd += " N'" + ClsTools.Tools.FormatInput(textboxNoi_Cap.Text) + "',";
                    strCmd += " N'" + ClsTools.Tools.FormatInput(textboxTien_Phat_VP.Text) + "',";
                    strCmd += " N'" + ClsTools.Tools.FormatInput(textboxSo_QD_Xu_Phat.Text) + "',";
                    strCmd += " N'" + sNgay_QD + "',";
                    strCmd += " N'" + ClsTools.Tools.FormatInput(textboxGTTG.Text) + "',";
                    strCmd += " N'" + ClsTools.Tools.FormatInput(textboxGhi_Chu.Text) + "',";
                    strCmd += " Getdate(),";
                    strCmd += " '" + Session["TenDangNhap"] + "' ";
                    strCmd += ", '" + ClsTools.Tools.FormatInput(textboxMa_Doi.Text) + "') ";

                    cmd.CommandText = strCmd;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = conn;

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