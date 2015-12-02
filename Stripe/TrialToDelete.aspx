<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrialToDelete.aspx.cs" Inherits="Stripe.TrialToDelete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>School Director Event Page</title>
    <link href="./bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="./bootstrap/customBootstrapJS/jquery-latest.js"></script>
    <script src="./bootstrap/js/bootstrap.js"></script>
    <link href="Content/Custom_StyleSheet/bootstrap-timepicker.css" rel="stylesheet" />
    <link href="./bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="./bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="Content/Custom_Javascript/bootstrap-timepicker.js"></script>
  


    <link href="./bootstrap/customBootstrapCSS/prettify.css" rel="stylesheet" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>

    <link href="Content/Custom_StyleSheet/SchoolDirectorEventsPage.css" rel="stylesheet" />
    <script src="Content/Custom_Javascript/jquery-2.1.4.min.js"></script>
    <link href="Content/Custom_StyleSheet/SchoolDirectorEventsPageTheme.css" rel="stylesheet" />
    <link href="Content/Custom_StyleSheet/SchoolDirectorEventsPageSite.css" rel="stylesheet" />
    <link href="Content/Custom_StyleSheet/SchoolDirectorEventsPagePikaday.css" rel="stylesheet" />
    <script src="Content/Custom_Javascript/SchoolDirectorEventsPage.js"></script>
</head>
<body>
    <form id="form1" runat="server">
   
    <div class="input-group bootstrap-timepicker timepicker">
             <asp:TextBox ID="timepicker2" class="form-control input-small" runat="server"></asp:TextBox>
            <span class="input-group-addon">
                <i class="glyphicon glyphicon-time"></i>
            </span>
        </div>
 
        <script type="text/javascript">
            $('#timepicker2').timepicker({
                minuteStep: 1,
                template: 'modal',
                appendWidgetTo: 'body',
                showSeconds: true,
                showMeridian: false,
                defaultTime: false
            });
        </script>
        
        </form>
</body>
</html>
