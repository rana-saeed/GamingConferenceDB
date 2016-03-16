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
    int r;
    string email = "elbob@yahoo.com";
    protected void Page_Load(object sender, EventArgs e)
    {
          String rev = Request.QueryString["id"];
          r = Convert.ToInt32(rev);

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
        SqlConnection con = new SqlConnection(conname);
        SqlCommand cmd = new SqlCommand("ViewReviewComments", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@review_id", r));
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        String commentor = "";
        String comment = "";
        int comment_id = 0;

        while (dr.Read())
        {
            commentor = dr.GetString(dr.GetOrdinal("Member"));
            comment = dr.GetString(dr.GetOrdinal("Comment"));
            comment_id = dr.GetInt32(dr.GetOrdinal("Comment_ID"));

            Button deleteButton = new Button();
            deleteButton.Text = "Delete";
            deleteButton.ID = comment_id.ToString();
            deleteButton.Click += new EventHandler(deleteComment);

            Label y = new Label();
            y.Text = commentor + ": " + comment + "   ";

            Label x = new Label();
            x.Text = comment_id.ToString() + "<br></br>";
            x.Visible = false;

            form1.Controls.Add(y);
            form1.Controls.Add(deleteButton);
            form1.Controls.Add(x);
        }
        con.Close();
    }

    protected void comment(Object sender, EventArgs e)
    {
        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con = new SqlConnection(conname);
        SqlCommand cmd = new SqlCommand("ConfReviewComment", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@review_id", r));
        cmd.Parameters.Add(new SqlParameter("@email", email));
        cmd.Parameters.Add(new SqlParameter("@content", CommentBox.Text));

        con.Open();
        cmd.ExecuteNonQuery();
        CommentBox.Text = string.Empty;
        CommentResponse.Text = "Comment added successfully.";
        CommentResponse.ForeColor = System.Drawing.Color.Red;
        con.Close();
    }

    protected void deleteComment(Object sender, EventArgs e)
    {
        Button b = (Button)sender;
        int comment_id = Convert.ToInt32(b.ID);
        string conname = ConfigurationManager.ConnectionStrings["MyDBConn"].ToString();
        SqlConnection con = new SqlConnection(conname);
        SqlCommand cmd = new SqlCommand("DeleteCommentOnConfReview", con);
        cmd.CommandType = CommandType.StoredProcedure;


    }
}