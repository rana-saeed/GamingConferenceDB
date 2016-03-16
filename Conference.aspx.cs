using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Conference : Page
{
    int nr = 6;
    string game = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        String comnr = Request.QueryString["id"];
        //nr = Convert.ToInt32(Convert.ToDouble(comnr));

        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con = new SqlConnection(conname);

        SqlCommand cmd = new SqlCommand("ViewConference", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@conf_id", nr));

        con.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        string name1 = "";
        string venue1 = "";
        while (dr.Read())
        {
            name1 = dr.GetString(dr.GetOrdinal("Conference Name"));
            venue1 = dr.GetString(dr.GetOrdinal("Venue"));
            DateTime date1 = dr.GetDateTime(dr.GetOrdinal("Start Date"));
            DateTime date2 = dr.GetDateTime(dr.GetOrdinal("End Date"));

            name.InnerHtml = "Conference: " + name1;
            venue.InnerHtml = "Venue: " + venue1;
            dateStart.InnerHtml = "From: " + date1.ToString();
            dateEnd.InnerHtml ="Till: " + date2.ToString();
        }
        con.Close();
        displayTeams();
        displayReviewTitles();
        Button1.Click += new EventHandler(this.attendConference);
        Button2.Click += new EventHandler(this.debuteGame);
        TextBox1.TextChanged += new EventHandler(this.AddGameDebuted);

    }

    /**
    protected void getTopics()
    {
        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con = new SqlConnection(conname);

        SqlCommand cmd = new SqlCommand("viewTopics", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@theme", nr));

        con.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        string name = "";
        int tid = 0;
        while (dr.Read())
        {
            name = dr.GetString(dr.GetOrdinal("title"));
            tid = dr.GetInt32(dr.GetOrdinal("topic_id"));
            //topics.InnerHtml = topics.InnerHtml + "<a href =\"topic.aspx?id=" + tid + "\">" + name + "</a><br />";
            HtmlGenericControl link = new HtmlGenericControl("a");
            HtmlGenericControl newLine = new HtmlGenericControl("br");
            link.Attributes["href"] = "topics.aspx?id=" + tid;
            link.InnerHtml = name;
            // topics.Controls.Add(link);
            // topics.Controls.Add(newLine);

        }
        con.Close();
    }
    **/

    protected void displayTeams()
    {
        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con = new SqlConnection(conname);

        SqlCommand cmd = new SqlCommand("ViewDevTeamGamesOFConf", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@conf_id", nr));

        con.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        String team = "";
        String game = "";

        Label y = new Label();
        y.Text = "Teams presenting: " + "<br></br>";
        Controls.Add(y);

        while (dr.Read())
        {
            team = dr.GetString(dr.GetOrdinal("Team Name"));
            game = dr.GetString(dr.GetOrdinal("Game Name"));

            Label x = new Label();
            x.Text = team + " - " + game + "<br></br>";
           Controls.Add(x);
            // devTeam.InnerHtml ="Team: " + team + " - " + game + "<br></br>";
        }
        con.Close();
    }

    protected void displayReviewTitles()
    {
        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con = new SqlConnection(conname);

        SqlCommand cmd = new SqlCommand("GetReviews", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@conf_id", nr));

        con.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        int title = 0;

        Label y = new Label();
        y.Text = "Reviews: " + "<br></br>";
        Controls.Add(y);

        while (dr.Read())
        {
            title = dr.GetInt32(dr.GetOrdinal("Title"));
            Label x = new Label();
            x.Text = title.ToString() + "<br></br>";
            Controls.Add(x); 
            //reviewTitle.InnerHtml ="Review: " + title.ToString();
        }
        con.Close();
    }

    protected void attendConference(object sender, EventArgs e)
    {
        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con = new SqlConnection(conname);

        SqlCommand cmd = new SqlCommand("Member_AttendConference", con);
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();
        //String email = Session["email"].ToString();
        string email = "elbob@yahoo.com";
        cmd.Parameters.Add("@email", SqlDbType.VarChar);
        cmd.Parameters.Add("@conf_id", SqlDbType.Int);
        cmd.Parameters["@email"].Value = email;
        cmd.Parameters["@conf_id"].Value = nr;
        cmd.ExecuteNonQuery();
    }


    protected void debuteGame(object sender, EventArgs e)
    {
        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con = new SqlConnection(conname);

        SqlCommand cmd = new SqlCommand("DevelopmentTeam_AddConferencePresentedIn", con);
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();
        string email = "Maha_Ehab@hotmail.com";
        string res = "";
        cmd.Parameters.Add("@email", SqlDbType.VarChar);
        cmd.Parameters.Add("@game_name", SqlDbType.VarChar);
        cmd.Parameters.Add("@conf_id", SqlDbType.Int);
        cmd.Parameters.Add("@response", SqlDbType.VarChar);
        cmd.Parameters["@email"].Value = email;
        cmd.Parameters["@game_name"].Value = game;
        cmd.Parameters["@conf_id"].Value = nr;
        cmd.Parameters["@response"].Value = res;
        OUTPUT.InnerHtml = res;
        cmd.ExecuteNonQuery();
    }

    
    protected void AddGameDebuted(object sender, EventArgs e)
    {
       game = TextBox1.Text;
    }
}