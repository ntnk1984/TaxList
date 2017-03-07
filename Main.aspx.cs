using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IT4U.Data;

namespace TTGTTP
{ 
    public partial class Main : System.Web.UI.Page
    {
        protected Control objPage;
        string P = "";
        string quyen = "";
        string Pprev = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Session["quyen"] == null)
            {
                P = (Request.QueryString["P"] == null ? "" : Request.QueryString["P"].ToString());
                //Response.Redirect(@"~\Default.aspx?P=" + Pprev);
                PageAdd("");
            }
            else
            {
                

                P = (Request.QueryString["P"] == null ? "" : Request.QueryString["P"].ToString());

                if (P == "" || P.ToUpper() == "DANGNHAP")
                {
                    labelName.Text = "";
                    Literal2.Text = "";
                }
                else
                {
                    Literal2.Text = Load_menu();
                    labelName.Text = "Xin chào, " + Session["HoTen"].ToString();
                }
                
                PageAdd(P);
            }

            
            //quyen = Session["QUYEN"].ToString();



        }

        string Load_menu()
        {
            iSqlData con_cl = new iSqlData("ConStr");
            //string r = Session["QUYEN"].ToString().Trim();

            string sSql = "select * from sys_command where menu_id in (select menu_id0 from sys_command where  dbo.ff_Inlist(menu_id, '" + Session["quyen"].ToString().Trim() + "')=1) order by menu_id ";
            //string sSql = "select * from sys_command where menu_id in (select menu_id0 from sys_command) order by menu_id ";

            DataTable MainMenu = con_cl.ExecDT(sSql);

            string htmlMenu = "<div class=\"collapse navbar-collapse\" id=\"bs-example-navbar-collapse-1\" > <ul class=\"nav navbar-nav\">";


            for (int i = 0; i <= MainMenu.Rows.Count - 1; i++)
            {
                sSql = "select * from sys_command where menu_id0 = '" + MainMenu.Rows[i]["menu_id"] + "' AND dbo.ff_Inlist(menu_id, '" + Session["quyen"].ToString().Trim() + "') = 1 order by menu_id0, menu_id";
                //sSql = "select * from sys_command where menu_id0 = '" + MainMenu.Rows[i]["menu_id"] + "' order by menu_id0, menu_id";

                DataTable Menu = con_cl.ExecDT(sSql);

                htmlMenu += "<li class=\"dropdown\"><a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" role=\"button\" aria-expanded=\"false\" ><b>" + MainMenu.Rows[i]["bar"] + "</b></a>";
                htmlMenu += "<ul class=\"dropdown-menu\" role=\"menu\">";

                for (int j = 0; j <= Menu.Rows.Count - 1; j++)
                {

                    htmlMenu += "<li> <a href=\"main.aspx?P=" + Menu.Rows[j]["page"].ToString() + "\"><b>" + Menu.Rows[j]["bar"].ToString() + "</b></a></li>";
                }

                htmlMenu += "</ul> </li>";

            }

            htmlMenu += "</ul>";


            htmlMenu += "</div>";

            return htmlMenu;
        }

        protected void PageAdd(string P)
        {

            if (P != "")
            {
                UserControl uc = (UserControl)Page.LoadControl("~/tracuu/" + P + ".ascx");
                uc.EnableViewState = false;
                PlaceHolder1.Controls.Add(uc);
            }
            else
            {
                UserControl uc = (UserControl)Page.LoadControl("tracuu/dangnhap.ascx");
                uc.EnableViewState = false;
                PlaceHolder1.Controls.Add(uc);

            }

            //if (P == "21")
            //{
            //    if (Session["QUYEN"].ToString().IndexOf(P) >= 0)
            //    {
            //        //LB_TEN_BC.Text = "THỐNG KÊ CHI TIẾT SẢN LƯỢNG LIÊN TỈNH, QT Ở BC PHÁT HCM";
            //        //objPage = (TTGTTP.addBienBan)LoadControl("addBienBan.ascx");
            //        //PlaceHolder1.Controls.Add(objPage);
            //        //return;
            //    }
            //    else
            //    {
            //        //LB_TEN_BC.Text = "ANH /CHỊ KHÔNG CÓ QUYỀN TRUY CẬP ";
            //        //objPage = (TTGTVT.Main.KO_TRUY_CAP)LoadControl("KO_TRUY_CAP.ascx");
            //        //PlaceHolder1.Controls.Add(objPage);
            //        //return;
            //    }
            //}


        }
    }

  
}