<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Callback.aspx.cs" Inherits="CHBase_FitBit_TestProject.Callback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:Literal ID="litResponse1" runat="server"></asp:Literal>
        </div>
        <%--<asp:Button ID="btnSignUP" runat="server" Text="You are logged out. Try logging in again." OnClick="btnSignUP_Click" Visible="false" />--%>

        <div>
            <asp:Literal ID="litResponse2" runat="server"></asp:Literal>
        </div>
    </div>


    </form>
</body>
</html>
