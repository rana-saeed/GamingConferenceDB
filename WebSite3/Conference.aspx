<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Conference.aspx.cs" Inherits="Conference" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <div>
         <h1 id ="name" runat ="server">Conference Name</h1>
         <h2 id ="venue" runat ="server">Venue</h2>
         <h2 id ="dateStart" runat ="server">Starts</h2>
         <h2 id ="dateEnd" runat ="server">Ends</h2>
    </div>
        <p>
            <asp:Button ID="Button1" runat="server"  Text="Attend" style="margin-left: 42px" Width="128px" OnClick="attendConference" />
            <asp:Label ID="AttendResponse" runat="server" ></asp:Label>
         </p>
             
        <p>
            <asp:Button ID="Button2" runat="server" style="margin-left: 42px" Text="Debut Game" OnClick="debuteGame" Width="128px" />
             <asp:TextBox ID="TextBox1" runat="server" Height="16px" OnTextChanged="AddGameDebuted" Width="145px"></asp:TextBox>
            <asp:Label ID ="Result" runat = "server"></asp:Label>      
        </p>
        <p>         
        <asp:Button ID="Button3" runat="server" style="margin-left: 44px" Text="Review" OnClick="reviewConference" Width="128px" />
            <asp:Label ID="ReviewResponse" runat="server"></asp:Label>
        </p>
         
        <p>
            <asp:TextBox ID="ReviewText" runat="server" Height="123px" style="margin-left: 44px" Width="573px" OnTextChanged="ReviewText_TextChanged"></asp:TextBox>
        </p>
         
    </form>
</body>

</html>

