<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolDirectorEventsPage.aspx.cs" Inherits="Stripe.SchoolDirectorEventsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>School Director Event Page</title>
    <link href="./bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="./bootstrap/customBootstrapJS/jquery-latest.js"></script>
    <script src="./bootstrap/js/bootstrap.js"></script>
    <link href="./bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="./bootstrap/css/bootstrap.css" rel="stylesheet" />
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
        <div class="container container-fluid">
            <div class="row" id="headerImage">
                <%--image div starts--%>
                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"></div>
                <div class="col-xs-4 col-sm-4 col-md-6 col-lg-6">
                    <img src="Images/MainLogo.png" />
                </div>
                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"></div>
            </div>
            <%--image div ends--%>
            <div class="row">
                <%--header div starts--%>
                <div class="col-xs-1  col-sm-0 col-md-1 col-lg-1"></div>

                <div id="header" class="col-xs-7 col-sm-12 col-md-12 col-lg-12">
                    <%--"nav nav-tabs nav-justified"--%>
                    <nav class="navbar navbar-default">
                        <div class="container-fluid">
                            <%--container1 begins--%>
                            <div class="navbar-header">
                                <%--header begins--%>
                            </div>
                            <%--header ends--%>
                            <div class="collapse navbar-collapse">
                                <%--collapse header content begins--%>
                                <ul class="nav nav-tabs nav-justified">
                                    <%--<ul class="nav navbar-nav">--%>
                                    <li class="active">
                                        <asp:HyperLink NavigateUrl="SchooHomePage.aspx" ID="refereeProfileHomeID" runat="server"><span class="glyphicon glyphicon-home" aria-hidden="true"></span><b> Home</b><span class="sr-only"></span></asp:HyperLink>

                                    </li>
                                    <li>
                                        <asp:HyperLink NavigateUrl="SchoolDirectorEventsPage.aspx" ID="eventsID" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span><b> Events</b><span class="sr-only"></span></asp:HyperLink>


                                    </li>
                                    <li>
                                        <asp:HyperLink NavigateUrl=" " ID="searchID" runat="server"><span class="glyphicon glyphicon-search" aria-hidden="true"></span><b> Search</b><span class="sr-only"></span></asp:HyperLink>


                                    </li>
                                    <li>
                                        <asp:HyperLink NavigateUrl="SchoolApprovalAndRatingPage.aspx" ID="approveAndRatingID" runat="server"><span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span><b> Approval & Rating</b><span class="sr-only"></span></asp:HyperLink>


                                    </li>
                                    <li>
                                        <asp:LinkButton ID="logoutout" runat="server"><span class="glyphicon glyphicon-off" aria-hidden="true"></span><b> Logout</b><span class="sr-only"></span></asp:LinkButton>

                                    </li>
                                </ul>

                            </div>
                            <%--collapse header ends--%>
                        </div>
                        <%--container1 ends --%>
                    </nav>
                </div>
                <div class="col-xs-0 col-sm-0 col-md-1 col-lg-1"></div>
            </div>
            <%--header div ends--%>

            <%--body section starts--%>
            <div class="row">
           
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            
            <div id="mainSection">
                
                <input type="radio" id="settings" value="1" name="tractor" checked='checked'/>
                <input type="radio" id="posts" value="2" name="tractor" />
                

                <div class="navSection">
                    <label for="settings" class='fontawesome-cogs'>Upcoming & Previous Events</label>
                    <label for="posts" class='fontawesome-cogs'>Create New Event</label>
                    
                </div>

                <div class='uno fontawesome-umbrella article'>
                     <div class="row">  
                         <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>
                         <div class="col-xs-1 col-sm-1 col-md-9 col-lg-9">
                    <div class="panel panel-default">
                        <div class="panel-heading"><center><b><p style="font-size:25px !important;font-weight:750">PREVIOUS EVENT LIST</p></b></center></div>
                               <div class="panel-body">

                                  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringLocalDB %>" SelectCommand=""></asp:SqlDataSource>
                                   <asp:DataGrid ID="DataGrid1" runat="server" DataSourceID="SqlDataSource1" CssClass="table table-hover table-striped " Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                           
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:DataGrid>
                               </div>         
                    </div>
                             </div>
                         <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                    </div> 
                </div>
                  <br/>
                    <div class="row">  
                         <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>
                         <div class="col-xs-1 col-sm-1 col-md-9 col-lg-9">
                    <div class="panel panel-default">
                        <div class="panel-heading"><center><b><p style="font-size:25px !important;font-weight:750">UPCOMING EVENT LIST</p></b></center></div>
                               <div class="panel-body">
                                   <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringLocalDB %>" SelectCommand=""></asp:SqlDataSource>
                                   <asp:DataGrid ID="DataGrid2" runat="server" DataSourceID="SqlDataSource2" CssClass="table table-hover table-striped " Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                           
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:DataGrid>
                               </div>         
                    </div>
                             </div>
                         <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                    </div> 
                </div>
                </div>
                <div class="dos fontawesome-umbrella article">
                    <div class="row">
                        <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>
                        <div class="col-xs-1 col-sm-1 col-md-8 col-lg-8">
                            <div class="panel">
                                <div class="panel-heading addcolor1">
                                    <h3 class="panel-title eventLocationAndTimeInformation">Event Location and Time Information</h3>
                                </div>
                                <div class="panel-body">
                                    <table class="table table-hover class_selector">
                                        <tbody>
                                            <tr>
                                                    <td class="text-center">Event Date</td>
                                                    <td class="text-center">
                                                        
                                                        <asp:TextBox ID="EventDateTextBoxID" placeholder="EVENT DATE" class="form-control" runat="server"></asp:TextBox>
                                                        <script type="text/javascript">
                                                            var picker = new Pikaday( 
                                                                { 
                                                                    field: document.getElementById('EventDateTextBoxID'), 
                                                                    firstDay: 1, 
                                                                    minDate: new Date('2000-01-01'), 
                                                                    maxDate: new Date('2020-12-31'), 
                                                                    yearRange: [2000, 2020], 
                                                                    numberOfMonths: 3, 
                                                                    theme: 'dark-theme' }); 
                                                        </script>

                                            </tr>
                                            <tr>
                                                    <td class="text-center">Event Time</td>
                                                    <td class="text-center">
                                                        <asp:TextBox ID="EventTimeFieldID" placeholder="HH:MMPM/AM" class="form-control" runat="server"></asp:TextBox></td>
                                                   
                                            </tr>
                                            <tr>
                                                    <td class="text-center">Event Location</td>
                                                    <td class="text-center">
                                                        <asp:TextBox ID="eventLocationFieldID" placeholder="EVENT LOCATION" class="form-control" runat="server"></asp:TextBox></td>
                                                </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-1 col-sm-1 col-md-2 col-lg-2"></div>
                   </div>
                     <div class="row">
                        <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>
                        <div class="col-xs-1 col-sm-1 col-md-3 col-lg-3">
                             <div class="panel">
                                 <div class="panel-heading addcolor1">
                                    <h3 class="panel-title eventLocationAndTimeInformation">Game Type</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:DropDownList class="form-control" ID="GameTypeSelectionValue" runat="server"></asp:DropDownList>    
                                </div>
                             </div>
                        </div>
                        <div class="col-xs-1 col-sm-1 col-md-5 col-lg-5">
                            <div class="panel">
                                 <div class="panel-heading addcolor1">
                                    <h3 class="panel-title eventLocationAndTimeInformation">Away Team</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:DropDownList class="form-control" ID="AwayTeamSelection" runat="server">
                                                                       
                                                                    </asp:DropDownList>    
                                </div>
                             </div>
                        </div>
                         <div class="col-xs-1 col-sm-1 col-md-3 col-lg-3"></div>
                    </div>
                     
                    <div class="row">
                        <div class="col-xs-1 col-sm-1 col-md-3 col-lg-3"></div>
                        <div class="col-xs-1 col-sm-1 col-md-4 col-lg-4">
                             <center>
                         <asp:LinkButton ID="updateUserProfileID" runat="server" CssClass="btn btn-success btn-lg" OnClick="createEventButton_Click">
                                                        <i aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></i> Create Event</asp:LinkButton>
                    </center>
                        </div>
                        <div class="col-xs-1 col-sm-1 col-md-5 col-lg-5></div>
                         
                    </div>

                    

                </div>
                    </div>

                </div>
            </div>
            
            
            
            </div>
            <%--body sections ends--%>
        
    </form>

</body>
</html>
