<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RefereeEventsPage.aspx.cs" Inherits="Stripe.RefereeEventsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Referee Event Page</title>
    <link href="./bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="./bootstrap/customBootstrapJS/jquery-latest.js"></script>
    <script src="./bootstrap/js/bootstrap.js"></script>

    <link href="./bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="./bootstrap/css/bootstrap.css" rel="stylesheet" />

    <link href="./bootstrap/customBootstrapCSS/prettify.css" rel="stylesheet" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>

    <link href="Content/Custom_StyleSheet/RefereeEventPage.css" rel="stylesheet" />
    <script src="Content/Custom_Javascript/jquery-2.1.4.min.js"></script>

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
                                        <asp:HyperLink NavigateUrl="RefereeHomePage.aspx" ID="refereeProfileHomeID" runat="server"><span class="glyphicon glyphicon-home" aria-hidden="true"></span><b> Home</b><span class="sr-only"></span></asp:HyperLink>

                                    </li>
                                    <li>
                                        <asp:HyperLink NavigateUrl=" " ID="eventsID" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span><b> Events</b><span class="sr-only"></span></asp:HyperLink>


                                    </li>
                                    <li>
                                        <asp:HyperLink NavigateUrl=" " ID="searchID" runat="server"><span class="glyphicon glyphicon-search" aria-hidden="true"></span><b> Search</b><span class="sr-only"></span></asp:HyperLink>


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
                    <label for="posts" class='fontawesome-folder-open'>Register Events</label>
                    
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
                    <div class="">
                        <div class="row">
                        <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>
                        <div class="col-xs-1 col-sm-1 col-md-8 col-lg-8">
                            <div class="panel">
                                <div class="panel-heading addcolor1">
                                    <h3 class="panel-title text-center">Event Location and Time Information</h3>
                                </div>
                                <div class="panel-body">
                                    <table class="table table-hover class_selector">
                                        <tbody>
                                            <tr>
                                                    <td class="text-center-2">Home School Name</td>
                                                    <td class="text-center-2">
                                                        
                                                        <asp:Label ID="homeSchoolNameLabelID" runat="server" Text="Home School"></asp:Label></td>
                                                       
                                            </tr>
                                            <tr>
                                                    <td class="text-center-2">Away School Name</td>
                                                    <td class="text-center-2">
                                                        
                                                        <asp:Label ID="awaySchoolNameLabelID" runat="server" Text="Away School"></asp:Label></td>
                                                       
                                            </tr>
                                            <tr>
                                                    <td class="text-center-2">Event Date</td>
                                                    <td class="text-center-2">
                                                        
                                                        <asp:Label ID="EventDateLabelID" runat="server" Text="Event Date"></asp:Label></td>
                                                       
                                            </tr>
                                            <tr>
                                                    <td class="text-center-2">Event Time</td>
                                                    <td class="text-center-2">
                                                        <asp:Label ID="EventTimeLabelID" runat="server" Text="Event Time"></asp:Label></td>
                                                   
                                            </tr>
                                            <tr>
                                                    <td class="text-center-2">Event Location</td>
                                                    <td class="text-center-2">
                                                        <asp:Label ID="EventLocationID" runat="server" Text="Event Location"></asp:Label></td>
                                                </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>
                        <div class="col-xs-1 col-sm-1 col-md-3 col-lg-3">
                             <div class="panel">
                                 <div class="panel-heading addcolor1">
                                    <h3 class="panel-title text-center">Game Position</h3>
                                </div>
                                <div class="panel-body gamePositionRefereeType">
                                    <asp:DropDownList class="form-control" ID="GamePositionTypeSelectionValue" runat="server"></asp:DropDownList>    
                                </div>
                             </div>
                        </div>
                        <div class="col-xs-1 col-sm-1 col-md-5 col-lg-5">
                            <div class="panel">
                                 <div class="panel-heading addcolor1">
                                    <h3 class="panel-title text-center">Apply/ Reject</h3>
                                </div>
                                <div class="panel-body">
                                   <table  class="table table-hover class_selector">
                                       <tbody>
                                           <tr>
                                               <td class="text-center">
                                                   <asp:LinkButton ID="updateUserProfileID" runat="server" CssClass="btn btn-success btn-lg" OnClick="registerEvent_Click">
                                                        <i aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></i> Register</asp:LinkButton>
                    
                                               </td>
                                           </tr>
                                           <tr>
                                               <td class="text-center">
                                                   <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger btn-lg">
                                                        <i aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></i> Decline</asp:LinkButton>
                                               </td>
                                           </tr>
                                       </tbody>
                                   </table>
                                </div>
                             </div>
                        </div>
                         <div class="col-xs-1 col-sm-1 col-md-3 col-lg-3"></div>
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
