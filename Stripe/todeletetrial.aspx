<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="todeletetrial.aspx.cs" Inherits="Stripe.todeletetrial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="Content/Custom_StyleSheet/trialStyleSheetToDelete.css" rel="stylesheet" />
    <script src="Content/Custom_Javascript/jquery-2.1.4.min.js"></script>
    <link href="./bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="./bootstrap/customBootstrapJS/jquery-latest.js"></script>
    <script src="./bootstrap/js/bootstrap.js"></script>

    <link href="./bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="./bootstrap/css/bootstrap.css" rel="stylesheet" />

    <link href="./bootstrap/customBootstrapCSS/prettify.css" rel="stylesheet" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>
    <link href="Content/Custom_StyleSheet/RefereeHomePage.css" rel="stylesheet" />
    <script>
        $(function () {
            $('#myTab a:last').tab('show');

        })
</script>
</head>
<body>
    <form id="form1" runat="server">
       <ul class="nav nav-pills nav-stacked" id="myTab">
                                <li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab">Referee Approval</a></li>
                                <li role="presentation"><a href="#profile" aria-controls="profile" role="tab">Referee Rating</a></li>
                                
                            </ul>
 
<div class="tab-content">
                                <div role="tabpanel" class="tab-pane fade active" id="home">
                                    <div class="panel panel-primary navigationTabsPanel">
                                        </div>
                               
                                <div role="tabpanel" class="tab-pane fade" id="profile">
                                    <div class="panel panel-success navigationTabsPanel"></div>
                                </div>
                                </div>
                            </div>
 



    </form>
    <script>
        $('#myTab a').click(function (e) {
            e.preventDefault();
            $(this).tab('show');
        })
    </script>
   
</body>
</html>
