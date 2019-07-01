<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartupPage.aspx.cs" Inherits="CHBase_FitBit_TestProject.Pages.StartupPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Click Here to <a id="lnkFitbit" runat="server" onserverclick="lnkFitbit_ServerClick"> Integrate </a> with Fitbit
    </div>
         <div>
    Click Here to <a id="lnkOmron" runat="server" onserverclick="lnkOmron_ServerClick"> Integrate </a> with Omron
    </div>
    </form>
</body>
</html>
