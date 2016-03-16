<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Conference.aspx.cs" Inherits="Conference" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <div>
         <h1 id ="name" runat ="server">
             Conference Name</h1>
         <h2 id ="venue" runat ="server">Venue</h2>
         <h2 id ="dateStart" runat ="server">Starts</h2>
         <h2 id ="dateEnd" runat ="server">Ends</h2>
    </div>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="attendConference" Text="Attend" style="margin-left: 42px" Width="128px" />
         </p>
                <h5 id ="OUTPUT" runat ="server"></h5>
        <p>
            <asp:Button ID="Button2" runat="server" style="margin-left: 42px" Text="Debut Game" OnClick="debuteGame" Width="128px" />
             <asp:TextBox ID="TextBox1" runat="server" Height="16px" OnTextChanged="AddGameDebuted" Width="145px"></asp:TextBox>
                </p>
    </form>
</body>
</html>

