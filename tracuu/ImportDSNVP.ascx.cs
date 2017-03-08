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
        if (Session.Count == 0)
        {
            Response.Redirect("~/tracuu/DangNhap.aspx");
        }

        if (!Page.IsPostBack)
        {
            cmdLogFile.Text = "";
            hidField1.Value = "";

            //danh_Muc();
        }
        //DropDownListDoi_CSGT.DataTextField = "ten_doi";
        //DropDownListDoi_CSGT.DataValueField = "ma_doi";
        //DropDownListDoi_CSGT.DataSource = dmdoi;
        //DropDownListDoi_CSGT.DataBind();
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



            int start = 0;
            int dong_ngay = 0;
            try
            {
                for (int i = start; i < dt.Rows.Count; i++)
                {
                    //if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()) && char.IsDigit(dt.Rows[i][2].ToString(), 0) && dt.Rows[i][2].ToString().Length < 5)
                    if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                    {
                        strCmd = " DELETE DanhSachNNT WHERE MaNNT = '" + ClsTools.Tools.FormatInput(dt.Rows[i][5].ToString()) + "' AND TieuMuc = '" + ClsTools.Tools.FormatInput(dt.Rows[i][9].ToString()) + "' AND NgayBangKeGiaoUNT = '" + ClsTools.Tools.FormatInput(dt.Rows[i][4].ToString()) + "';";
                        cmd.CommandText = strCmd;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        cmd.ExecuteScalar();


                        strCmd = "INSERT INTO DanhSachNNT (";
                        strCmd += "ID, MaCoQuanThue, MaUNT, TenUNT, SoBangKeGiaoUNT, NgayBangKeGiaoUNT, MaNNT, TenNNT, SacThue, Chuong, TieuMuc, DiaBan, KBNN, KyThue";
                        strCmd += ", LoaiTaiSanNSNN, HanNop, MaGiaoUNT, LoaiTien, SoTienGiaoUNT, SoTienConPhaiThu, SoTienThuDuoc, SoTienKhongThuDuocQuyetToan, SoBienLai ";
                        strCmd += ", NgayBienLai, SoBangKeBL, NgayBangKeBL, NgayNhanBangKe, DCCT_SoNha, DCCT_MaTinh, DCCT_TenTinh, DCCT_MaQuan, DCCT_TenQuan, DCCT_MaPhuong";
                        strCmd += ", DCCT_TenPhuong, DCCT_DienThoai, DCCT_Email, DCNTBT_SoNha, DCNTBT_MaTinh, DCNTBT_TenTinh, DCNTBT_MaQuan, DCNTBT_TenQuan, DCNTBT_MaPhuong";
                        strCmd += ", DCNTBT_TenPhuong, NgayImport, NgayKhoiTao, TrangThai, GhiChu, MaNhanVien, MaBuuCuc, MaTrungTam";

                        strCmd += ") values(";

                        //string sNgay_QD = ClsTools.Tools.FormatInput(dt.Rows[start + i][15].ToString());
                        //sNgay_QD = ClsTools.Tools.FormatDateEN(sNgay_QD).Substring(0, 10) + " 00:00:00";
                        strCmd += "'" + Guid.NewGuid() + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][0].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][1].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][2].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][3].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][4].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][5].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][6].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][7].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][8].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][9].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][10].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][11].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][12].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][13].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][14].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][15].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][16].ToString()) + "',";
                        strCmd += " " + (dt.Rows[start + i][17].ToString()) + ",";//
                        strCmd += " " + (dt.Rows[start + i][18].ToString()) + ","; //
                        strCmd += " " + (dt.Rows[start + i][19].ToString()) + ","; //
                        strCmd += " " + (dt.Rows[start + i][20].ToString()) + ","; //
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][21].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][22].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][23].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][24].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][25].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][26].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][27].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][28].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][29].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][30].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][31].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][32].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][33].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][34].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][35].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][36].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][37].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][38].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][39].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][40].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][41].ToString()) + "',";

                        strCmd += " getdate(),";
                        strCmd += " getdate(),";
                        strCmd += " 1,";
                        strCmd += " '',";
                        strCmd += " '" + Session["MaNhanVien"] + "',";
                        strCmd += " '" + Session["MaBuuCuc"] + "',";
                        strCmd += " '" + Session["MaTrungTam"] + "'";


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
        import_5001_1();

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