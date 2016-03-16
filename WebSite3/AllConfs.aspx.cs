using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AllConfs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("ViewAllConfs", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        conn.Open();
        SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (rdr.Read())
        {
            int id = rdr.GetInt32(rdr.GetOrdinal("ID"));
            String name = rdr.GetString(rdr.GetOrdinal("Name"));
            Label id_label = new Label();
            Label name_label = new Label();
            Button view_button = new Button();
            view_button.ID = id + "";
            view_button.Text = "View  ";
            view_button.Click += new EventHandler(View_Conf);
            id_label.Text = "" + id;
            id_label.Visible = false;
            name_label.Text = name + "  <br /> <br />";
            form1.Controls.Add(id_label);
            form1.Controls.Add(view_button);
            form1.Controls.Add(name_label);
        }
    }
    protected void View_Conf(Object sender, EventArgs e)
    {
        Button b = (Button)sender;
        Response.Redirect("Conference.aspx?id1=" + conf_label.Text + "&ID=" + b.ID);
    }
}