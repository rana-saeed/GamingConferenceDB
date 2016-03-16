using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConfReview : System.Web.UI.Page
{
    int r = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        String rev = Request.QueryString["id"];
        //r = Convert.ToInt32(Convert.ToDouble(rev));

        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con = new SqlConnection(conname);

        SqlCommand cmd = new SqlCommand("ViewReview", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@review_id", r));
        con.Open();

        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        int id = 0;
        String conf = "";
        String member = "";
        String content = "";

        while (dr.Read())
        {
            id = dr.GetInt32(dr.GetOrdinal("Title"));
            member = dr.GetString(dr.GetOrdinal("Member"));
            content = dr.GetString(dr.GetOrdinal("Review"));
            conf = dr.GetString(dr.GetOrdinal("Conf"));

            title.InnerHtml = id.ToString() + ": conference " + conf;
            reviewer.InnerHtml = member;
            review.InnerHtml = content;
        }
        con.Close();
        getComments();
    }

    protected void getComments()
    {
        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con2 = new SqlConnection(conname);
        SqlCommand cmd2 = new SqlCommand("ViewReviewComments", con2);
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.Add(new SqlParameter("@review_id", r));
        con2.Open();
        SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
        String commentor = "";
        String comment = "";

        while (dr2.Read())
        {
            commentor = dr2.GetString(dr2.GetOrdinal("Member"));
            comment = dr2.GetString(dr2.GetOrdinal("Comment"));

            reviewComment.InnerHtml = commentor + ": " + comment;
        }
        con2.Close();
    }
}