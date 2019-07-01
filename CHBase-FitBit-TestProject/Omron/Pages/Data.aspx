<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Data.aspx.cs" Inherits="CHBase.Omron.Omron.Pages.Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <h3>
                <asp:Literal ID="litHeader" runat="server"></asp:Literal>
            </h3>
            <asp:DropDownList ID="ddlMethods" runat="server"></asp:DropDownList>
            <asp:Button ID="btnSubmit" runat="server" Text="Click to get selected data!" OnClick="btnSubmit_Click" />

            <div>
                <asp:Literal ID="litOutput" runat="server"></asp:Literal>
            </div>

        </div>
    </div>
    </form>
</body>
</html>
