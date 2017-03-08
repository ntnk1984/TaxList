using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IT4U.Data;

public partial class tracuu_DangNhap : System.Web.UI.UserControl
{
    string P = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["USER_BC"] = "";
        Session["USER_TENBC"] = "";
        Session["USERNAME"] = "";
        Session["USERID"] = "";
        Session["QUYEN"] = "";              // LA CHUOI QUYEN, CACH NHAU BANG DAU ;
        Session["STTTINH"] = "";
        Session["PHONGBAN"] = "";
        Session["TEN_PHONGBAN"] = "";
        Session["CHUCVU"] = "";
        Session["TenDangNhap"] = "";
        Session["HoTen"] = "";

        txt_username.Focus();
    }

    private void DangNhap(string p1, string p2)
    {
        DataTable dt = new DataTable();
        string sql;
        try
        {

            if (p1.ToUpper() != "" && p2 != "")
            {
                Session.Timeout = 20;

                iSqlData con_cl = new iSqlData("ConStr");
                string query = "SELECT a.USERID ,a.USERNAME,a.PASS,a.MA_BUU_CUC,TENNV,MA_TRUNG_TAM FROM SYS_USER a WHERE (USERNAME='" + p1.Trim() + "'  AND PASS='" + p2.Trim() + "' AND ISNULL(XOA,'0')='0') ";

                dt = con_cl.ExecDT(query);

                if (dt.Rows.Count == 0)
                {
                    Label1.Text = "Thông tin đăng nhập không đúng";
                    return;
                }
                else
                {
                    Session["MaNhanVien"] = dt.Rows[0]["USERID"].ToString();
                    Session["TenDangNhap"] = dt.Rows[0]["USERNAME"].ToString();
                    Session["HoTen"] = dt.Rows[0]["TENNV"].ToString();
                    Session["MaBuuCuc"] = dt.Rows[0]["MA_BUU_CUC"].ToString();
                    Session["MaTrungTam"] = dt.Rows[0]["MA_TRUNG_TAM"].ToString();

                    query = " SELECT menu AS MSQUYEN FROM SYS_RIGHTUSER a INNER JOIN SYS_RIGHTS b ON a.MSQUYEN=b.MSQUYEN ";
                    query += " WHERE USERNAME='" + p1.Trim() + "'";
                    query += " GROUP BY menu HAVING menu <> ''";


                    DataTable tab_quyen = con_cl.ExecDT(query);
                    if (tab_quyen.Rows.Count > 0)
                    {
                        string quyen = "";
                        for (int i = 0; i < tab_quyen.Rows.Count; i++)
                            quyen += tab_quyen.Rows[i]["MSQUYEN"].ToString() + ";";

                        quyen = quyen.Substring(0, quyen.Length - 1);
                        Session["QUYEN"] = quyen;
                        tab_quyen.Dispose();
                    }

                    dt.Dispose();
                    con_cl.Close();
                    con_cl.Dispose();

                    Response.Redirect("Main.aspx?P=Homepage");

                    //P = (Request.QueryString["P"] == null ? "" : Request.QueryString["P"].ToString());
                    //if (Session["CHUCVU"].ToString() == "1" || Session["QUYEN"].ToString().IndexOf(P) >= 0)
                    //{ Response.Redirect("Main/Mainform.aspx?P=" + P); }
                    //else { Response.Redirect("Main/Mainform.aspx"); }


                    //if (Session["QUYEN"].ToString().IndexOf("R06") >= 0 || Session["QUYEN"].ToString().IndexOf("R03") >= 0)
                    //    Response.Redirect("Main/Mainform.aspx?P=7");
                    //else Response.Redirect("Main/Mainform.aspx?P=100");
                }




            }
            else
            {
                this.Label1.Text = "Tên đăng nhập hoặc mật khẩu không đúng. Xin vui lòng đăng nhập lại.";
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
        }
    }
    protected void tOk_Click(object sender, EventArgs e)
    {
        DangNhap(IT4U.iString.Format_Input(this.txt_username.Text.ToString().Trim()), IT4U.iString.Format_Input(this.txt_pass.Text.ToString().Trim()));
    }
}