using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IT4U.Data;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    string Load_menu()
    {
        iSqlData con_cl = new iSqlData("ConSQL");
        string r = Session["QUYEN"].ToString().Trim();

        //string sSql = "select * from sys_command where menu_id in (select menu_id0 from sys_command where  dbo.ff_Inlist(menu_id, '" + Session["quyen"].ToString().Trim() + "')=1) order by menu_id ";
        string sSql = "select * from sys_command where menu_id in (select menu_id0 from sys_command) order by menu_id ";

        DataTable MainMenu = con_cl.ExecDT(sSql);

        string htmlMenu = "<div class=\"collapse navbar-collapse\" id=\"bs-example-navbar-collapse-1\" > <ul class=\"nav navbar-nav\">";


        for (int i = 0; i <= MainMenu.Rows.Count - 1; i++)
        {
            sSql = "select * from sys_command where menu_id0 = '" + MainMenu.Rows[i]["menu_id"] + "' AND dbo.ff_Inlist(menu_id, '" + Session["quyen"].ToString().Trim() + "') = 1 order by menu_id0, menu_id";


            DataTable Menu = con_cl.ExecDT(sSql);

            htmlMenu += "<li class\"dropdown\"><a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" role=\"button\" aria-expanded=\"false\" ><b>" + MainMenu.Rows[i]["bar"] + "</b></a>";
            htmlMenu += "<ul class=\"dropdown-menu\" role=\"menu\">";

            for (int j = 0; j <= Menu.Rows.Count - 1; j++)
            {

                htmlMenu += "<li> <a href=\"" + Menu.Rows[j]["page"].ToString() + "\"><b>" + Menu.Rows[j]["bar"].ToString() + "</b></a></li>";
            }

            htmlMenu += "</ul> </li>";

        }

        htmlMenu += "</ul>";


        htmlMenu += "</div>";

        return htmlMenu;
    }
}
