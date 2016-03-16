<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfReview.aspx.cs" Inherits="ConfReview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <h1 id ="title" runat ="server">Title</h1>
         <h3 id ="reviewer" runat ="server">Reviewer</h3>
         <h3 id ="review" runat ="server">Review</h3>
         <div id ="reviewComment" runat ="server">
             <asp:Button ID="Button1" runat="server" Text="Comment" OnClick = "comment"  Width="149px" />
             <asp:Label ID="CommentResponse" runat="server"></asp:Label>
         </div>
    </div>
        <asp:TextBox ID="CommentBox" runat="server" Height="108px" Width="549px"></asp:TextBox>
    </form>
</body>
</html>
