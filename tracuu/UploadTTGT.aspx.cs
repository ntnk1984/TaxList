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
using System.IO;
using System.Text;


public partial class UploadTTGT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cmdLogFile.Text = "";
            hidField1.Value = "";
        }
    }
   
    protected void tOk_Click(object sender, EventArgs e)
    {
        DateTime dtnow = DateTime.Now;
        
        cmdLogFile.Text = "";
        hidField1.Value = "";
        Label1.Text = "";

        string strPass = dtnow.Year.ToString() + dtnow.Month.ToString() + dtnow.Day.ToString() + "ttsgtvt";

        if (tPassword.Text.ToString() != strPass)
        {
            Label1.Text = "Password không đúng";
            return;
        }
        //////UPFILE LEN SERVER		
        
        string fileName = System.IO.Path.GetFileName(FileUpload.PostedFile.FileName);
        string strFileLength = FileUpload.PostedFile.ContentLength.ToString();
        int iFileLengthKB = (int)FileUpload.PostedFile.ContentLength / 1024;
        string strFileExtension = "";

        //get file extension

        int iPos = fileName.IndexOf(".");
        if (iPos != -1)
        {
            strFileExtension = fileName.Substring(iPos + 1, fileName.Length - iPos - 1);
        }

        strFileExtension = strFileExtension.ToLower();
        if (!strFileExtension.Equals("xls"))
        {
            Label1.Text = " Upload KHÔNG thành công! File không đúng định dạng.";
            return;
        }

        string strSaveLocation;

        
        string strFileMoi = dtnow.Year.ToString() + dtnow.Month.ToString() + dtnow.Day.ToString() + dtnow.Hour.ToString() + dtnow.Minute.ToString() + dtnow.Second.ToString() + "." + strFileExtension;
        string strFileResult = dtnow.Year.ToString() + dtnow.Month.ToString() + dtnow.Day.ToString() + dtnow.Hour.ToString() + dtnow.Minute.ToString() + dtnow.Second.ToString() + ".txt";

        //string strThangTinhLuong = (string)Session["LCS_UPLOAD_BCC_THANG_NAM"];
        //strThangTinhLuong = strThangTinhLuong.Substring(0,2)+strThangTinhLuong.Substring(3,4);
        //strSaveLocation = (string)ConfigurationManager.AppSettings["m_downloadpath"].ToString() + Session["USERNAME"].ToString().Trim();
        strSaveLocation = Server.MapPath("upload\\");
        
        try
        {
            if (!System.IO.Directory.Exists(strSaveLocation))
            {
                System.IO.Directory.CreateDirectory(strSaveLocation);
            }
            else
            {
                //System.IO.Directory.Delete(strSaveLocation,true);
                if (System.IO.File.Exists(strSaveLocation + "\\" + strFileMoi))
                {
                    System.IO.File.Delete(strSaveLocation + "\\" + strFileMoi);

                }
                //System.IO.Directory.CreateDirectory(strSaveLocation);
            }

        }
        catch (Exception ex)
        {
            Label1.Text = " Lỗi upload file " + ex.Message;
            return;
        }

        try
        {
            FileUpload.PostedFile.SaveAs(strSaveLocation + "\\" + strFileMoi);
            Label1.Text = " Upload file thành công";
        }
        catch (Exception ex)
        {
            Label1.Text = " Lỗi upload file FileUpload.PostedFile" + ex.Message;
            return;
        }

        /// tạo file result import
        String filepath = strSaveLocation + "\\" + strFileResult;// đường dẫn của file muốn tạo
        cmdLogFile.Text = strFileResult;
        hidField1.Value = filepath;
        if (System.IO.File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);

        }
        FileStream fs = new FileStream(filepath, FileMode.Create);//Tạo file mới tên là test.txt

        StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);//fs là 1 FileStream 
        sWriter.WriteLine(strFileResult + " " + tPassword.Text.ToString()); 
        sWriter.WriteLine("Bắt đầu Import ... ");
        sWriter.WriteLine("Thời gian bắt đầu: ngày " + dtnow.Day.ToString() + " tháng " + dtnow.Month.ToString() + " năm " + dtnow.Year.ToString() + " giờ " + dtnow.Hour.ToString() + " phút " + dtnow.Minute.ToString() + " giây " + dtnow.Second.ToString());
        //////////////////

        /// IMPORT VAO DATABASE
        /// 
        DataSet ds = new DataSet();
        ds = TTGTVT.ImportExport.ImportToDataset(strSaveLocation + "\\" + strFileMoi);

        if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
        {
            DataTable dt = ds.Tables[0];

            string strConn = ConfigurationSettings.AppSettings["ConStr"];
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            //SqlTransaction sqlTran = conn.BeginTransaction();
            //cmd.Transaction = sqlTran;

            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = conn;

            string strCmd = "";

            try
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {

                    strCmd = "INSERT INTO QUYET_DINH ([SO_QD],[NGAY_BB],[NGUOI_VP],[SO_XE],[SO_TIEN], [TUOC_GPLX_DEN_NGAY],[NGAY_QD], [SYSDATE],[USERNAME], MA_DOI)";
                    strCmd += " values(";
                    strCmd += " N'" + dt.Rows[k][1].ToString() + "',";
                    strCmd += " '" + dt.Rows[k][2].ToString() + "',";
                    strCmd += " N'" + dt.Rows[k][3].ToString() + "',";
                    strCmd += " N'" + dt.Rows[k][4].ToString() + "',";
                    strCmd += " " + dt.Rows[k][5].ToString() + ",";
                    strCmd += " '" + dt.Rows[k][6].ToString() + "',";
                    strCmd += " '" + dt.Rows[k][7].ToString() + "',";
                    strCmd += " Getdate(),";
                    strCmd += " '" + this.tPassword.Text.ToString() + "' ";
                    strCmd += ", '" + dt.Rows[k][8].ToString() + "') ";

                    cmd.CommandText = strCmd;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    try
                    {
                       cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        sWriter.WriteLine("Dòng " + (k + 1).ToString() + " import không thành công. " + ex.Message);
                    }
                }
                Label1.Text = " Import hoàn thành. Vui lòng xem logfile để biết thông tin chi tiết.";

            }
            catch (Exception ex)
            {
                sWriter.WriteLine("Lỗi " + ex.Message);
                //return;
            }

            ///////////
            sWriter.WriteLine("Thời gian kết thúc: ngày " + dtnow.Day.ToString() + " tháng " + dtnow.Month.ToString() + " năm " + dtnow.Year.ToString() + " giờ " + dtnow.Hour.ToString() + " phút " + dtnow.Minute.ToString() + " giây " + dtnow.Second.ToString());
            sWriter.WriteLine("Kết thúc Import ... ");
            sWriter.WriteLine("Số records: " + ds.Tables[0].Rows.Count); 
            sWriter.Close();
            sWriter.Dispose();
            
            ///////////
            
            ds.Dispose();

            conn.Close();
            conn.Dispose();
            
        }
        ds.Dispose();
    }
    protected void cmdLogFile_Click(object sender, EventArgs e)
    {
        string filepath = hidField1.Value.ToString();
        if (File.Exists(filepath))
        {
            Response.Buffer = true;
            this.EnableViewState = false;

            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment;filename=Result.txt");

            Response.WriteFile(filepath);
            Response.End();
        }
        else
        {
            Response.Write("<script language='javascript'>alert('Lỗi khi download file');</script>");
        }
    }
}
