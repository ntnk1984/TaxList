using System;
using System.Collections;
using System.Configuration;
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

public partial class admin_CN_SoHieu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblNote.Text = "Chức năng này sẽ được thực hiện định kỳ tự động vào 12h trưa và 0h tối.</br> Nếu anh/ chị muốn thực hiện ngay thời điểm này vui lòng nhấn nút bên dưới và chờ đợi trong một thời gian nhất định.";
    }
    protected void CN_SoHieu_BG()
    {
        string strConn = ConfigurationSettings.AppSettings["ConStr"];
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "Auto_CN_So_Hieu_Buu_Gui";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = conn;

        cmd.CommandTimeout = 0;
        cmd.ExecuteReader();

        try
        {
            cmd.ExecuteReader();
        }
        catch (Exception e)
        {
            lblNote.Text = "Lỗi khi thực hiện. " + e.Message;
        }
        finally
        {
            lblNote.Text = "Đã thực hiện xong ...";
        }
        conn.Close();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        CN_SoHieu_BG();
    }
}
