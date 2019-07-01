<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallBack.aspx.cs" Inherits="CHBase_FitBit_TestProject.Pages.CallBack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="litAuthCode" runat="server"></asp:Literal>
    Fetch My Weight Data
        <a id="lnkFetchAllData" runat="server" onserverclick="lnkFetchAllData_ServerClick">Fetch All Weight Data</a>
    </div>
    </form>
</body>
</html>
