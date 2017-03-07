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
using IT4U.Data;

public partial class thue_UploadDSNVP : System.Web.UI.Page
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
    protected void tOk_Click(object sender, EventArgs e)
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
            string bdh  = "";
            string po  = "";
            string nguoi_lap  = "";
            string ngay_thong_ke  = "";
            string co_quan_chu_quan  = "";
            string ma_nghiep_vu  = "";


            bdh = dt.Rows[6][1].ToString();
            po = dt.Rows[8][1].ToString();
            nguoi_lap = dt.Rows[10][1].ToString();
            ngay_thong_ke = dt.Rows[2][6].ToString();
            co_quan_chu_quan = dt.Rows[6][6].ToString();
            ma_nghiep_vu = dt.Rows[8][10].ToString();

            //kiem tra doi
            co_quan_chu_quan = co_quan_chu_quan.Split('-')[0].Split(':')[1].ToString().Trim();
            //kiem tra ma nghiep vu
            ma_nghiep_vu = ma_nghiep_vu.Split('-')[0].Split(':')[1].ToString().Trim();
            bdh = bdh.Split(':')[1].ToString().Trim();
            nguoi_lap = nguoi_lap.Split(':')[1].ToString().Trim();

            Label1.Text = "";

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



            int start = int.Parse(textboxDong_Bat_Dau.Text);
            try
            {
                for (int i = 0; i < dt.Rows.Count - start; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[start + i][0].ToString()))
                    {
                        strCmd = "INSERT INTO DanhSachNNT (";
                        strCmd += "ID, MaCoQuanThue, MaUNT, TenUNT, SoBangKeGiaoUNT, NgayBangKeGiaoUNT, MaNNT, TenNNT, SacThue, Chuong, TieuMuc, DiaBan, KBNN, KyThue";
                        strCmd += ", LoaiTaiSanNSNN, HanNop, MaGiaoUNT, LoaiTien, SoTienGiaoUNT, SoTienConPhaiThu, SoTienThuDuoc, SoTienKhongThuDuocQuyetToan, SoBienLai ";
                        strCmd += ", NgayBienLai, SoBangKeBL, NgayBangKeBL, NgayNhanBangKe, DCCT_SoNha, DCCT_MaTinh, DCCT_TenTinh, DCCT_MaQuan, DCCT_TenQuan, DCCT_MaPhuong";
                        strCmd += ", DCCT_TenPhuong, DCCT_DienThoai, DCCT_Email, DCNTBT_SoNha, DCNTBT_MaTinh, DCNTBT_TenTinh, DCNTBT_MaQuan, DCNTBT_TenQuan, DCNTBT_MaPhuong";
                        strCmd += ", DCNTBT_TenPhuong, NgayImport, NgayKhoiTao, TrangThai, GhiChu, MaNhanVien, MaBuuCuc, MaTrungTam";

                        strCmd += ") values(";

                        string sNgay_QD = ClsTools.Tools.FormatInput(dt.Rows[start + i][15].ToString());
                        sNgay_QD = ClsTools.Tools.FormatDateEN(sNgay_QD).Substring(0, 10) + " 00:00:00";

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
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][17].ToString()) + "',";
                        strCmd += " " + ClsTools.Tools.FormatInput(dt.Rows[start + i][18].ToString()) + ","; //
                        strCmd += " " + ClsTools.Tools.FormatInput(dt.Rows[start + i][19].ToString()) + ","; //
                        strCmd += " " + ClsTools.Tools.FormatInput(dt.Rows[start + i][20].ToString()) + ","; //
                        strCmd += " " + ClsTools.Tools.FormatInput(dt.Rows[start + i][21].ToString()) + ","; //
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
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][42].ToString()) + "',";
                        strCmd += " N'" + ClsTools.Tools.FormatInput(dt.Rows[start + i][43].ToString()) + "',";

                        strCmd += " getdate(),";
                        strCmd += " getdate(),";
                        strCmd += " 1,";
                        strCmd += " '',";
                        strCmd += " '" + Session["MaNhanVien"] + ",";
                        strCmd += " '" + Session["MaBuuCuc"] + ",";
                        strCmd += " ''";
                        

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

    void danh_Muc()
    {
        DataTable dmdoi = conn.ExecDT("SELECT ma_doi, ten_doi FROM dmdoi ORDER BY ten_doi");
        DropDownListDoi_CSGT.DataTextField = "ten_doi";
        DropDownListDoi_CSGT.DataValueField = "ma_doi";
        DropDownListDoi_CSGT.DataSource = dmdoi;
        DropDownListDoi_CSGT.DataBind();

    }



}
