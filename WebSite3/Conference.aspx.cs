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
    int nr;
    string game = "";
    string email = "elbob@yahoo.com";
    protected void Page_Load(object sender, EventArgs e)
    {
            String comnr = Request.QueryString["ID"].ToString();
            nr = Convert.ToInt32(comnr);

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
                dateEnd.InnerHtml = "Till: " + date2.ToString();
            }
            con.Close();
            displayTeams();
            displayReviewTitles();
            TextBox1.TextChanged += new EventHandler(this.AddGameDebuted);
        
    }

    protected void displayTeams()
    {
        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con = new SqlConnection(conname);

        con.Open();
        SqlCommand cmd = new SqlCommand("ViewDevTeamGamesOFConf", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@conf_id", nr));
        SqlDataReader dr = cmd.ExecuteReader();
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
            HyperLink x = new HyperLink();
            x.Text = title.ToString()+ "<br></br>";
            x.NavigateUrl ="ConfReview.aspx?id="+title;
            Controls.Add(x);
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
        //this.email = "elbob@yahoo.com";
        string msg = "";
        cmd.Parameters.Add(new SqlParameter("@email", email));
        cmd.Parameters.Add(new SqlParameter("@conf_id", nr));
        cmd.Parameters["@email"].Value = email;
        cmd.Parameters["@conf_id"].Value = nr;
        SqlParameter param = new SqlParameter("@msg", SqlDbType.VarChar, 30);
        param.Direction = ParameterDirection.Output;
        cmd.Parameters.Add(param);

        cmd.ExecuteNonQuery();
        msg = (string)param.Value;
        if (msg.Equals("false"))
        {
            AttendResponse.Text = "You've already attended this conference!";
            AttendResponse.ForeColor = System.Drawing.Color.Red;
        }
        con.Close();


    }


    protected void debuteGame(object sender, EventArgs e)
    {
        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con = new SqlConnection(conname);

        SqlCommand cmd = new SqlCommand("DevelopmentTeam_AddConferencePresentedIn", con);
        cmd.CommandType = CommandType.StoredProcedure;

        
        //String email = Session["email"].ToString();
        //this.email = "Maha_Ehab@hotmail.com";
        cmd.Parameters.Add(new SqlParameter("@email", email));
        cmd.Parameters.Add(new SqlParameter("@game_name",TextBox1.Text));
        cmd.Parameters.Add(new SqlParameter("@conf_id", nr));
        SqlParameter param = new SqlParameter("@response", SqlDbType.VarChar, 50);
        param.Direction = ParameterDirection.Output;
        cmd.Parameters.Add(param);

        con.Open();
        cmd.ExecuteNonQuery();
        Result.Text = (string)param.Value;
        Result.ForeColor = System.Drawing.Color.Red;
        TextBox1.Text = string.Empty;
        cmd.ExecuteNonQuery();
        Response.Redirect("Conference.aspx?id1=" + "&ID=" + nr);
        con.Close();
    }
    protected void AddGameDebuted(object sender, EventArgs e)
    {
        game = TextBox1.Text;
    }

    protected void reviewConference(object sender, EventArgs e)
    {
        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con = new SqlConnection(conname);

        SqlCommand cmd = new SqlCommand("ReviewConference", con);
        cmd.CommandType = CommandType.StoredProcedure;
        //String email = Session["email"].ToString();
        string res = "";
        cmd.Parameters.Add(new SqlParameter("@conf_id", nr));
        cmd.Parameters.Add(new SqlParameter("@email", email));
        cmd.Parameters.Add(new SqlParameter("@content", ReviewText.Text));
        SqlParameter param = new SqlParameter("@res", SqlDbType.VarChar,10);
        param.Direction = ParameterDirection.Output;
        cmd.Parameters.Add(param);

        con.Open();
        cmd.ExecuteNonQuery();
        res = (string) param.Value;
        if (res.Equals("false"))
        {
            ReviewResponse.Text = "You have not attended this conference.";
        }
        else
        {
            ReviewResponse.Text = "Review added successfully.";
        }
        ReviewResponse.ForeColor = System.Drawing.Color.Red;
        ReviewText.Text = string.Empty;
        con.Close();
    }



    protected void ReviewText_TextChanged(object sender, EventArgs e)
    {

    }
}