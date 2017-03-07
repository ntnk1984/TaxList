using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IT4U.Data;

public partial class tracuu_ImportDSNVP : System.Web.UI.UserControl
{
    iSqlData conn = new iSqlData("ConStr");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cmdLogFile.Text = "";
            hidField1.Value = "";

            danh_Muc();
        }
        DropDownListDoi_CSGT.DataTextField = "ten_doi";
        DropDownListDoi_CSGT.DataValueField = "ma_doi";
        DropDownListDoi_CSGT.DataSource = dmdoi;
        DropDownListDoi_CSGT.DataBind();
    }

    //private void releaseObject(object obj)
    //{
    //    try
    //    {
    //        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
    //        obj = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        obj = null;
    //        MessageBox.Show("Unable to release the Object " + ex.ToString());
    //    }
    //    finally
    //    {
    //        GC.Collect();
    //    }
    //}
    void import_5001()
    {

        DateTime dtnow = DateTime.Now;

        cmdLogFile.Text = "";
        hidField1.Value = "";
        Label1.Text = "";

        string strPass = dtnow.Year.ToString() + dtnow.Month.ToString() + dtnow.Day.ToString() + "ttsgtvt";

        //if (tPassword.Text.ToString() != strPass)
        //{
        //    Label1.Text = "Password không đúng";
        //    return;
        //}
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
        //sWriter.WriteLine(strFileResult + " " + tPassword.Text.ToString()); 
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
            string bdh = "";
            string po = "";
            string nguoi_lap = "";
            string ngay_thong_ke = "";
            string co_quan_chu_quan = "";
            string ma_nghiep_vu = "";
            Label1.Text = "";

            try
            {
                bdh = dt.Rows[6][2].ToString();
                po = dt.Rows[8][2].ToString();
                nguoi_lap = dt.Rows[8][8].ToString();
                ngay_thong_ke = dt.Rows[2][5].ToString();
                co_quan_chu_quan = dt.Rows[6][5].ToString();
                ma_nghiep_vu = "5001";//dt.Rows[8][11].ToString();

                //kiem tra doi
                co_quan_chu_quan = co_quan_chu_quan.Split('-')[0].Split(':')[1].ToString().Trim();
                //kiem tra ma nghiep vu
                bdh = bdh.Split(':')[1].ToString().Trim().Split('-')[1].ToString().Trim();
                po = po.Split(':')[1].ToString().Trim().Split('-')[0].ToString().Trim();
                nguoi_lap = nguoi_lap.Split(':')[1].ToString().Trim();
            }
            catch
            {
                Label1.Text = "File dữ liệu không đúng. Vui lòng chọn lại";
                return;
            }





            if (!string.IsNullOrEmpty(co_quan_chu_quan))
            {
                if (co_quan_chu_quan != DropDownListDoi_CSGT.SelectedValue)
                {
                    Label1.Text = "Chọn đội CSGT không đúng. Vui lòng chọn lại.";
                    return;
                }

            }
            else
            {
                Label1.Text = " Import không hoàn thành. Lỗi file dữ liệu: Cơ quan chủ quản.";
                return;
            }

            if (!string.IsNullOrEmpty(ma_nghiep_vu))
            {
                if (ma_nghiep_vu != DropDownListMa_Nghiep_Vu.SelectedValue)
                {
                    Label1.Text = "Chọn mã nghiệp vụ PP không đúng. Vui lòng chọn lại.";
                    return;
                }

            }
            else
            {
                Label1.Text = " Import không hoàn thành. Lỗi file dữ liệu: Mã nghiệp vụ.";
                return;
            }



            int start = 21 - 2;
            int dong_ngay = 0;
            try
            {
                for (int i = start; i < dt.Rows.Count; i++)
                {
                    string ngay_giao_dich = "";
                    if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()) && (dt.Rows[i][1].ToString().Substring(0, 4) == "Ngày"))
                    {
                        dong_ngay = i;

                    }

                    if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()) && char.IsDigit(dt.Rows[i][2].ToString(), 0) && dt.Rows[i][2].ToString().Length < 5)
                    {
                        strCmd = "INSERT INTO DM_QUYET_DINH (BKS, HO_TEN, DIA_CHI, SO_DIEN_THOAI, LOAI_GIAY_TO, SO_GIAY_TO, NGAY_CAP, NOI_CAP, TIEN_PHAT_VP, SO_QD_XU_PHAT, NGAY_QD_XU_PHAT, GTTG, GHI_CHU, NGAY_HE_THONG,USERNAME, MA_DOI, bdh,po,nguoi_lap,ngay_thong_ke,co_quan_chu_quan,ma_nghiep_vu,ngay_giao_dich)";
                        strCmd += " values(";

                        string sNgay_QD = ClsTools.Tools.FormatInput(dt.Rows[i][10].ToString());
                        sNgay_QD = ClsTools.Tools.FormatDateEN(sNgay_QD).Substring(0, 10);
                        ngay_giao_dich = dt.Rows[dong_ngay][1].ToString().Split(' ')[1];
                        ngay_giao_dich = ClsTools.Tools.FormatDateEN(ngay_giao_dich).Substring(0, 10);

                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][3].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][5].ToString()) + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " '" + "" + "',";
                        strCmd += " '" + "" + "',";
                        strCmd += " N'" + "" + "',"; // ngay cap
                        strCmd += " N'" + "" + "',";
                        strCmd += " " + ClsTools.Tools.FormatInput(dt.Rows[i][13].ToString().Replace(",", "")) + ",";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][8].ToString()) + "',";
                        strCmd += " N'" + sNgay_QD + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " Getdate(),''";
                        //strCmd += " '" + Session["TenDangNhap"] + "' ";
                        strCmd += ", '" + ClsTools.Tools.FormatInput(DropDownListDoi_CSGT.SelectedValue) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(bdh) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(po) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(nguoi_lap) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ngay_thong_ke) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(co_quan_chu_quan) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ma_nghiep_vu) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ngay_giao_dich) + "' ";

                        strCmd += ");";

                        cmd.CommandText = strCmd;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;

                        try
                        {
                            cmd.ExecuteScalar();
                        }
                        catch (Exception ex)
                        {
                            sWriter.WriteLine("Dòng " + (i + 1).ToString() + " import không thành công. " + ex.Message);
                        }
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
            sWriter.WriteLine("Số records: " + (ds.Tables[0].Rows.Count - start));
            sWriter.Close();
            sWriter.Dispose();

            ///////////

            ds.Dispose();

            conn.Close();
            conn.Dispose();

        }
        ds.Dispose();

    }
    void import_5001_1()
    {

        DateTime dtnow = DateTime.Now;

        cmdLogFile.Text = "";
        hidField1.Value = "";
        Label1.Text = "";

        string strPass = dtnow.Year.ToString() + dtnow.Month.ToString() + dtnow.Day.ToString() + "ttsgtvt";

        //if (tPassword.Text.ToString() != strPass)
        //{
        //    Label1.Text = "Password không đúng";
        //    return;
        //}
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
        //sWriter.WriteLine(strFileResult + " " + tPassword.Text.ToString()); 
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
            string bdh = "";
            string po = "";
            string nguoi_lap = "";
            string ngay_thong_ke = "";
            string co_quan_chu_quan = "";
            string ma_nghiep_vu = "";
            Label1.Text = "";

            try
            {
                bdh = dt.Rows[6][2].ToString();
                po = dt.Rows[8][2].ToString();
                nguoi_lap = dt.Rows[8][8].ToString();
                ngay_thong_ke = dt.Rows[2][6].ToString();
                co_quan_chu_quan = dt.Rows[6][6].ToString();
                ma_nghiep_vu = "5001";//dt.Rows[8][11].ToString();

                //kiem tra doi
                co_quan_chu_quan = co_quan_chu_quan.Split('-')[0].Split(':')[1].ToString().Trim();
                //kiem tra ma nghiep vu
                bdh = bdh.Split(':')[1].ToString().Trim().Split('-')[1].ToString().Trim();
                po = po.Split(':')[1].ToString().Trim().Split('-')[0].ToString().Trim();
                nguoi_lap = nguoi_lap.Split(':')[1].ToString().Trim();
            }
            catch
            {
                Label1.Text = "File dữ liệu không đúng. Vui lòng chọn lại";
                return;
            }





            if (!string.IsNullOrEmpty(co_quan_chu_quan))
            {
                if (co_quan_chu_quan != DropDownListDoi_CSGT.SelectedValue)
                {
                    Label1.Text = "Chọn đội CSGT không đúng. Vui lòng chọn lại.";
                    return;
                }

            }
            else
            {
                Label1.Text = " Import không hoàn thành. Lỗi file dữ liệu: Cơ quan chủ quản.";
                return;
            }

            if (!string.IsNullOrEmpty(ma_nghiep_vu))
            {
                if (ma_nghiep_vu != DropDownListMa_Nghiep_Vu.SelectedValue)
                {
                    Label1.Text = "Chọn mã nghiệp vụ PP không đúng. Vui lòng chọn lại.";
                    return;
                }

            }
            else
            {
                Label1.Text = " Import không hoàn thành. Lỗi file dữ liệu: Mã nghiệp vụ.";
                return;
            }



            int start = 21 - 2;
            int dong_ngay = 0;
            try
            {
                for (int i = start; i < dt.Rows.Count; i++)
                {
                    string ngay_giao_dich = "";
                    if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()) && (dt.Rows[i][2].ToString().Length >= 4) && (dt.Rows[i][2].ToString().Substring(0, 4) == "Ngày"))
                    {
                        dong_ngay = i;

                    }

                    if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()) && char.IsDigit(dt.Rows[i][2].ToString(), 0) && dt.Rows[i][2].ToString().Length < 5)
                    {
                        strCmd = " DELETE DM_QUYET_DINH WHERE SO_QD_XU_PHAT = '" + ClsTools.Tools.FormatInput(dt.Rows[i][11].ToString()) + "';";
                        cmd.CommandText = strCmd;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        cmd.ExecuteScalar();


                        strCmd = " INSERT INTO DM_QUYET_DINH (BKS, HO_TEN, DIA_CHI, SO_DIEN_THOAI, LOAI_GIAY_TO, SO_GIAY_TO, NGAY_CAP, NOI_CAP, TIEN_PHAT_VP, SO_QD_XU_PHAT, NGAY_QD_XU_PHAT, GTTG, GHI_CHU, NGAY_HE_THONG,USERNAME, MA_DOI, bdh,po,nguoi_lap,ngay_thong_ke,co_quan_chu_quan,ma_nghiep_vu,ngay_giao_dich)";
                        strCmd += " values(";

                        string sNgay_QD = ClsTools.Tools.FormatInput(dt.Rows[i][10].ToString());
                        sNgay_QD = ClsTools.Tools.FormatDateEN(sNgay_QD).Substring(0, 10);
                        ngay_giao_dich = dt.Rows[dong_ngay][2].ToString().Split(' ')[1];
                        ngay_giao_dich = ClsTools.Tools.FormatDateEN(ngay_giao_dich).Substring(0, 10);

                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][3].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][5].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][7].ToString()) + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " '" + "" + "',";
                        strCmd += " '" + "" + "',";
                        strCmd += " N'" + "" + "',"; // ngay cap
                        strCmd += " N'" + "" + "',";
                        strCmd += " " + ClsTools.Tools.FormatInput(dt.Rows[i][12].ToString().Replace(",", "")) + ",";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][8].ToString()) + "',";
                        strCmd += " N'" + sNgay_QD + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " Getdate(),''";
                        //strCmd += " '" + Session["TenDangNhap"] + "' ";
                        strCmd += ", '" + ClsTools.Tools.FormatInput(DropDownListDoi_CSGT.SelectedValue) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(bdh) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(po) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(nguoi_lap) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ngay_thong_ke) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(co_quan_chu_quan) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ma_nghiep_vu) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ngay_giao_dich) + "' ";

                        strCmd += ");";

                        cmd.CommandText = strCmd;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;

                        try
                        {
                            cmd.ExecuteScalar();
                        }
                        catch (Exception ex)
                        {
                            sWriter.WriteLine("Dòng " + (i + 1).ToString() + " import không thành công. " + ex.Message);
                        }
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
            sWriter.WriteLine("Số records: " + (ds.Tables[0].Rows.Count - start));
            sWriter.Close();
            sWriter.Dispose();

            ///////////

            ds.Dispose();

            conn.Close();
            conn.Dispose();

        }
        ds.Dispose();

    }
    void import_5001_2()
    {

        DateTime dtnow = DateTime.Now;

        cmdLogFile.Text = "";
        hidField1.Value = "";
        Label1.Text = "";

        string strPass = dtnow.Year.ToString() + dtnow.Month.ToString() + dtnow.Day.ToString() + "ttsgtvt";

        //if (tPassword.Text.ToString() != strPass)
        //{
        //    Label1.Text = "Password không đúng";
        //    return;
        //}
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
        //sWriter.WriteLine(strFileResult + " " + tPassword.Text.ToString()); 
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
            string bdh = "";
            string po = "";
            string nguoi_lap = "";
            string ngay_thong_ke = "";
            string co_quan_chu_quan = "";
            string ma_nghiep_vu = "";
            Label1.Text = "";

            try
            {
                bdh = dt.Rows[6][2].ToString();
                po = dt.Rows[8][2].ToString();
                nguoi_lap = dt.Rows[8][8].ToString();
                ngay_thong_ke = dt.Rows[2][6].ToString();
                co_quan_chu_quan = dt.Rows[6][6].ToString();
                ma_nghiep_vu = "5001";//dt.Rows[8][11].ToString();

                //kiem tra doi
                co_quan_chu_quan = co_quan_chu_quan.Split('-')[0].Split(':')[1].ToString().Trim();
                //kiem tra ma nghiep vu
                bdh = bdh.Split(':')[1].ToString().Trim().Split('-')[1].ToString().Trim();
                po = po.Split(':')[1].ToString().Trim().Split('-')[0].ToString().Trim();
                nguoi_lap = nguoi_lap.Split(':')[1].ToString().Trim();
            }
            catch
            {
                Label1.Text = "File dữ liệu không đúng. Vui lòng chọn lại";
                return;
            }





            if (!string.IsNullOrEmpty(co_quan_chu_quan))
            {
                if (co_quan_chu_quan != DropDownListDoi_CSGT.SelectedValue)
                {
                    Label1.Text = "Chọn đội CSGT không đúng. Vui lòng chọn lại.";
                    return;
                }

            }
            else
            {
                Label1.Text = " Import không hoàn thành. Lỗi file dữ liệu: Cơ quan chủ quản.";
                return;
            }

            if (!string.IsNullOrEmpty(ma_nghiep_vu))
            {
                if (ma_nghiep_vu != DropDownListMa_Nghiep_Vu.SelectedValue)
                {
                    Label1.Text = "Chọn mã nghiệp vụ PP không đúng. Vui lòng chọn lại.";
                    return;
                }

            }
            else
            {
                Label1.Text = " Import không hoàn thành. Lỗi file dữ liệu: Mã nghiệp vụ.";
                return;
            }



            int start = 21 - 2;
            int dong_ngay = 0;
            try
            {
                for (int i = start; i < dt.Rows.Count; i++)
                {
                    string ngay_giao_dich = "";
                    if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()) && (dt.Rows[i][2].ToString().Length >= 4) && (dt.Rows[i][2].ToString().Substring(0, 4) == "Ngày"))
                    {
                        dong_ngay = i;

                    }

                    if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()) && char.IsDigit(dt.Rows[i][2].ToString(), 0) && dt.Rows[i][2].ToString().Length < 5)
                    {
                        strCmd = " DELETE DM_QUYET_DINH WHERE SO_QD_XU_PHAT = '" + ClsTools.Tools.FormatInput(dt.Rows[i][11].ToString()) + "' AND ma_doi = '"+ DropDownListDoi_CSGT.SelectedValue.ToString().Trim() + "'; ";
                        cmd.CommandText = strCmd;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        cmd.ExecuteScalar();


                        strCmd = " INSERT INTO DM_QUYET_DINH (BKS, HO_TEN, DIA_CHI, SO_DIEN_THOAI, LOAI_GIAY_TO, SO_GIAY_TO, NGAY_CAP, NOI_CAP, TIEN_PHAT_VP, SO_QD_XU_PHAT, NGAY_QD_XU_PHAT, GTTG, GHI_CHU, NGAY_HE_THONG,USERNAME, MA_DOI, bdh,po,nguoi_lap,ngay_thong_ke,co_quan_chu_quan,ma_nghiep_vu,ngay_giao_dich)";
                        strCmd += " values(";

                        string Cols = "";
                        if (string.IsNullOrEmpty(dt.Rows[i][7].ToString()))
                        {
                            Cols = "3,5,7,8,10,12";
                        }
                        else
                        {
                            Cols = "3,5,7,11,12,13";
                        }

                        string[] arrCol = Cols.Split(',');

                        string sNgay_QD = ClsTools.Tools.FormatInput(dt.Rows[i][int.Parse(arrCol[4])].ToString());
                        sNgay_QD = ClsTools.Tools.FormatDateEN(sNgay_QD).Substring(0, 10);
                        ngay_giao_dich = dt.Rows[dong_ngay][2].ToString().Split(' ')[1];
                        ngay_giao_dich = ClsTools.Tools.FormatDateEN(ngay_giao_dich).Substring(0, 10);

                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][int.Parse(arrCol[0])].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][int.Parse(arrCol[1])].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][int.Parse(arrCol[2])].ToString()) + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " '" + "" + "',";
                        strCmd += " '" + "" + "',";
                        strCmd += " N'" + "" + "',"; // ngay cap
                        strCmd += " N'" + "" + "',";
                        strCmd += " " + ClsTools.Tools.FormatInput(dt.Rows[i][int.Parse(arrCol[5])].ToString().Replace(",", "")) + ",";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][int.Parse(arrCol[3])].ToString()) + "',";
                        strCmd += " N'" + sNgay_QD + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " Getdate(),''";
                        //strCmd += " '" + Session["TenDangNhap"] + "' ";
                        strCmd += ", '" + ClsTools.Tools.FormatInput(DropDownListDoi_CSGT.SelectedValue) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(bdh) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(po) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(nguoi_lap) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ngay_thong_ke) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(co_quan_chu_quan) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ma_nghiep_vu) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ngay_giao_dich) + "' ";

                        strCmd += ");";

                        cmd.CommandText = strCmd;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;

                        try
                        {
                            cmd.ExecuteScalar();
                        }
                        catch (Exception ex)
                        {
                            sWriter.WriteLine("Dòng " + (i + 1).ToString() + " import không thành công. " + ex.Message);
                        }
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
            sWriter.WriteLine("Số records: " + (ds.Tables[0].Rows.Count - start));
            sWriter.Close();
            sWriter.Dispose();

            ///////////

            ds.Dispose();

            conn.Close();
            conn.Dispose();

        }
        ds.Dispose();

    }
    void import_5002()
    {

        DateTime dtnow = DateTime.Now;

        cmdLogFile.Text = "";
        hidField1.Value = "";
        Label1.Text = "";

        string strPass = dtnow.Year.ToString() + dtnow.Month.ToString() + dtnow.Day.ToString() + "ttsgtvt";

        //if (tPassword.Text.ToString() != strPass)
        //{
        //    Label1.Text = "Password không đúng";
        //    return;
        //}
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
        //sWriter.WriteLine(strFileResult + " " + tPassword.Text.ToString()); 
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
            string bdh = "";
            string po = "";
            string nguoi_lap = "";
            string ngay_thong_ke = "";
            string co_quan_chu_quan = "";
            string ma_nghiep_vu = "";
            Label1.Text = "";

            try
            {
                bdh = dt.Rows[6][2].ToString();
                po = dt.Rows[8][2].ToString();
                nguoi_lap = dt.Rows[10][2].ToString();
                ngay_thong_ke = dt.Rows[2][7].ToString();
                co_quan_chu_quan = dt.Rows[6][7].ToString();
                ma_nghiep_vu = dt.Rows[8][11].ToString();

                //kiem tra doi
                co_quan_chu_quan = co_quan_chu_quan.Split('-')[0].Split(':')[1].ToString().Trim();
                //kiem tra ma nghiep vu
                ma_nghiep_vu = ma_nghiep_vu.Split('-')[0].Split(':')[1].ToString().Trim();
                bdh = bdh.Split(':')[1].ToString().Trim().Split('-')[1].ToString().Trim();
                po = po.Split(':')[1].ToString().Trim().Split('-')[0].ToString().Trim();
                nguoi_lap = nguoi_lap.Split(':')[1].ToString().Trim();
            }
            catch
            {
                Label1.Text = "File dữ liệu không đúng. Vui lòng chọn lại";
                return;
            }





            if (!string.IsNullOrEmpty(co_quan_chu_quan))
            {
                if (co_quan_chu_quan != DropDownListDoi_CSGT.SelectedValue)
                {
                    Label1.Text = "Chọn đội CSGT không đúng. Vui lòng chọn lại.";
                    return;
                }

            }
            else
            {
                Label1.Text = " Import không hoàn thành. Lỗi file dữ liệu: Cơ quan chủ quản.";
                return;
            }

            if (!string.IsNullOrEmpty(ma_nghiep_vu))
            {
                if (ma_nghiep_vu != DropDownListMa_Nghiep_Vu.SelectedValue)
                {
                    Label1.Text = "Chọn mã nghiệp vụ PP không đúng. Vui lòng chọn lại.";
                    return;
                }

            }
            else
            {
                Label1.Text = " Import không hoàn thành. Lỗi file dữ liệu: Mã nghiệp vụ.";
                return;
            }



            int start = 31 - 2;
            int dong_ngay = 0;
            try
            {
                for (int i = start; i < dt.Rows.Count; i++)
                {
                    string ngay_giao_dich = "";
                    if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()) && (dt.Rows[i][1].ToString().Substring(0, 4) == "Ngày"))
                    {
                        dong_ngay = i;

                    }

                    if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()) && char.IsDigit(dt.Rows[i][2].ToString(), 0) && dt.Rows[i][2].ToString().Length < 5)
                    {
                        strCmd = "INSERT INTO DM_QUYET_DINH (BKS, HO_TEN, DIA_CHI, SO_DIEN_THOAI, LOAI_GIAY_TO, SO_GIAY_TO, NGAY_CAP, NOI_CAP, TIEN_PHAT_VP, SO_QD_XU_PHAT, NGAY_QD_XU_PHAT, GTTG, GHI_CHU, NGAY_HE_THONG,USERNAME, MA_DOI, bdh,po,nguoi_lap,ngay_thong_ke,co_quan_chu_quan,ma_nghiep_vu,ngay_giao_dich)";
                        strCmd += " values(";

                        string sNgay_QD = ClsTools.Tools.FormatInput(dt.Rows[i][16].ToString());
                        sNgay_QD = ClsTools.Tools.FormatDateEN(sNgay_QD).Substring(0, 10);
                        ngay_giao_dich = dt.Rows[dong_ngay][1].ToString().Split(' ')[7] + dt.Rows[dong_ngay][1].ToString().Split(' ')[5] + dt.Rows[dong_ngay][1].ToString().Split(' ')[2];

                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][8].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][10].ToString()) + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " '" + "" + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][12].ToString()) + "',";
                        strCmd += " N'" + "" + "',"; // ngay cap
                        strCmd += " N'" + "" + "',";
                        strCmd += " " + ClsTools.Tools.FormatInput(dt.Rows[i][18].ToString().Replace(",", "")) + ",";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][14].ToString()) + "',";
                        strCmd += " N'" + sNgay_QD + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " Getdate(),''";
                        //strCmd += " '" + Session["TenDangNhap"] + "' ";
                        strCmd += ", '" + ClsTools.Tools.FormatInput(DropDownListDoi_CSGT.SelectedValue) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(bdh) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(po) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(nguoi_lap) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ngay_thong_ke) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(co_quan_chu_quan) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ma_nghiep_vu) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ngay_giao_dich) + "' ";

                        strCmd += ");";

                        cmd.CommandText = strCmd;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;

                        try
                        {
                            cmd.ExecuteScalar();
                        }
                        catch (Exception ex)
                        {
                            sWriter.WriteLine("Dòng " + (i + 1).ToString() + " import không thành công. " + ex.Message);
                        }
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
            sWriter.WriteLine("Số records: " + (ds.Tables[0].Rows.Count - start));
            sWriter.Close();
            sWriter.Dispose();

            ///////////

            ds.Dispose();

            conn.Close();
            conn.Dispose();

        }
        ds.Dispose();

    }

    void import_5002_1()
    {

        DateTime dtnow = DateTime.Now;

        cmdLogFile.Text = "";
        hidField1.Value = "";
        Label1.Text = "";

        string strPass = dtnow.Year.ToString() + dtnow.Month.ToString() + dtnow.Day.ToString() + "ttsgtvt";

        //if (tPassword.Text.ToString() != strPass)
        //{
        //    Label1.Text = "Password không đúng";
        //    return;
        //}
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
        //sWriter.WriteLine(strFileResult + " " + tPassword.Text.ToString()); 
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
            string bdh = "";
            string po = "";
            string nguoi_lap = "";
            string ngay_thong_ke = "";
            string co_quan_chu_quan = "";
            string ma_nghiep_vu = "";
            Label1.Text = "";

            try
            {
                bdh = dt.Rows[6][2].ToString();
                po = dt.Rows[8][2].ToString();
                nguoi_lap = dt.Rows[8][11].ToString();
                ngay_thong_ke = dt.Rows[2][6].ToString();
                co_quan_chu_quan = dt.Rows[6][6].ToString();
                ma_nghiep_vu = DropDownListMa_Nghiep_Vu.SelectedValue;

                //kiem tra doi
                co_quan_chu_quan = co_quan_chu_quan.Split('-')[0].Split(':')[1].ToString().Trim();
                //kiem tra ma nghiep vu
                //ma_nghiep_vu = ma_nghiep_vu.Split('-')[0].Split(':')[1].ToString().Trim();
                bdh = bdh.Split(':')[1].ToString().Trim().Split('-')[1].ToString().Trim();
                po = po.Split(':')[1].ToString().Trim().Split('-')[0].ToString().Trim();
                nguoi_lap = nguoi_lap.Split(':')[1].ToString().Trim();
            }
            catch
            {
                Label1.Text = "File dữ liệu không đúng. Vui lòng chọn lại";
                return;
            }





            if (!string.IsNullOrEmpty(co_quan_chu_quan))
            {
                if (co_quan_chu_quan != DropDownListDoi_CSGT.SelectedValue)
                {
                    Label1.Text = "Chọn đội CSGT không đúng. Vui lòng chọn lại.";
                    return;
                }

            }
            else
            {
                Label1.Text = " Import không hoàn thành. Lỗi file dữ liệu: Cơ quan chủ quản.";
                return;
            }

            if (!string.IsNullOrEmpty(ma_nghiep_vu))
            {
                if (ma_nghiep_vu != DropDownListMa_Nghiep_Vu.SelectedValue)
                {
                    Label1.Text = "Chọn mã nghiệp vụ PP không đúng. Vui lòng chọn lại.";
                    return;
                }

            }
            else
            {
                Label1.Text = " Import không hoàn thành. Lỗi file dữ liệu: Mã nghiệp vụ.";
                return;
            }



            int start = 22 - 2;
            int dong_ngay = 0;
            try
            {
                for (int i = start; i < dt.Rows.Count; i++)
                {
                    string ngay_giao_dich = "";
                    if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()) && dt.Rows[i][2].ToString().Length >= 4 && (dt.Rows[i][2].ToString().Substring(0, 4) == "Ngày"))
                    {
                        dong_ngay = i;

                    }

                    if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()) && char.IsDigit(dt.Rows[i][2].ToString(), 0) && dt.Rows[i][2].ToString().Length < 5)
                    {
                        strCmd += " DELETE DM_QUYET_DINH WHERE SO_QD_XU_PHAT = '"+ ClsTools.Tools.FormatInput(dt.Rows[i][9].ToString()) + "' AND ma_doi = '"+ DropDownListDoi_CSGT.SelectedValue.ToString().Trim() +"'; ";
                        strCmd += " INSERT INTO DM_QUYET_DINH (BKS, HO_TEN, DIA_CHI, SO_DIEN_THOAI, LOAI_GIAY_TO, SO_GIAY_TO, NGAY_CAP, NOI_CAP, TIEN_PHAT_VP, SO_QD_XU_PHAT, NGAY_QD_XU_PHAT, GTTG, GHI_CHU, NGAY_HE_THONG,USERNAME, MA_DOI, bdh,po,nguoi_lap,ngay_thong_ke,co_quan_chu_quan,ma_nghiep_vu,ngay_giao_dich)";
                        strCmd += " VALUES(";

                        string sNgay_QD = ClsTools.Tools.FormatInput(dt.Rows[i][12].ToString());
                        sNgay_QD = ClsTools.Tools.FormatDateEN(sNgay_QD).Substring(0, 10);
                        ngay_giao_dich = dt.Rows[dong_ngay][2].ToString().Substring(5,10);
                        ngay_giao_dich = ClsTools.Tools.FormatDateEN(ngay_giao_dich).Substring(0, 10);

                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][3].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][5].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][7].ToString()) + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " '" + "" + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " N'" + "" + "',"; // ngay cap
                        strCmd += " N'" + "" + "',";
                        strCmd += " " + ClsTools.Tools.FormatInput(dt.Rows[i][14].ToString().Replace(",", "")) + ",";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[i][9].ToString()) + "',";
                        strCmd += " N'" + sNgay_QD + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " N'" + "" + "',";
                        strCmd += " Getdate(),''";
                        //strCmd += " '" + Session["TenDangNhap"] + "' ";
                        strCmd += ", '" + ClsTools.Tools.FormatInput(DropDownListDoi_CSGT.SelectedValue) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(bdh) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(po) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(nguoi_lap) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ngay_thong_ke) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(co_quan_chu_quan) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ma_nghiep_vu) + "' ";
                        strCmd += ", N'" + ClsTools.Tools.FormatInput(ngay_giao_dich) + "' ";

                        strCmd += ");";

                        cmd.CommandText = strCmd;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;

                        try
                        {
                            
                            cmd.ExecuteScalar();
                        }
                        catch (Exception ex)
                        {
                            sWriter.WriteLine("Dòng " + (i + 1).ToString() + " import không thành công. " + ex.Message);
                        }
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
            sWriter.WriteLine("Số records: " + (ds.Tables[0].Rows.Count - start));
            sWriter.Close();
            sWriter.Dispose();

            ///////////

            ds.Dispose();

            conn.Close();
            conn.Dispose();

        }
        ds.Dispose();

    }

    protected void tOk_Click(object sender, EventArgs e)
    {
        if (DropDownListMa_Nghiep_Vu.SelectedValue == "5001")
            //switch (DropDownListMau_File.SelectedIndex.ToString())
            //{
            //    case "0":
            //        import_5001_1();
            //        break;
            //    case "1":
            //        import_5001_2();
            //        break;
            //}
            import_5001_2();

        else
            //switch (DropDownListMau_File.SelectedIndex.ToString())
            //{
            //    case "0":
            //        import_5002_1();
            //        break;
            //    case "1":
            //        import_5002_1();
            //        break;
            //}
            import_5002_1();

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

    static DataTable dmdoi;
    void danh_Muc()
    {
        dmdoi = conn.ExecDT("SELECT  ma_doi, ten_doi FROM dmdoi ORDER BY ten_doi");
      

    }




   
}