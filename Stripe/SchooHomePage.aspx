﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchooHomePage.aspx.cs" Inherits="Stripe.schooHomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>School Home Page</title>
    <link href="./bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="./bootstrap/customBootstrapJS/jquery-latest.js"></script>
    <script src="./bootstrap/js/bootstrap.js"></script>

    <link href="./bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="./bootstrap/css/bootstrap.css" rel="stylesheet" />

    <link href="./bootstrap/customBootstrapCSS/prettify.css" rel="stylesheet" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>

    <link href="Content/Custom_StyleSheet/SchoolHomePage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container container-fluid">
            <div class="row" id="headerImage">
                <%--image div starts--%>
                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"></div>
                <div class="col-xs-4 col-sm-4 col-md-6 col-lg-6">
                    <img src="Images/logo.png" />
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

            <div class="row">
                <div class="col-xs-12  col-sm-12 col-md-12 col-lg-12">
                    <center><h2 class="googleFont"><asp:Label ID="schoolNameId" runat="server" Text="School Name"></asp:Label></h2>
                   </center>
                </div>
            </div>
            <div class="row">
                <%--school director information div starts--%>
                <div class="col-xs-12  col-sm-12 col-md-12 col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title  directorcontactinformationmainpanel">School Information</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-xs-4  col-sm-4 col-md-4 col-lg-4">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <h3 class="panel-title schoolInformationpicturepanel">
                                            <asp:Label ID="schoolNameLabelID" runat="server" Text="School Name"></asp:Label>

                                        </h3>
                                    </div>
                                    <div class="panel-body ">
                                        <center><img src="Images/2_Normal_Comm_Logo.jpg" /></center>
                                    </div>

                                </div>
                            </div>
                            <div class="col-xs-8  col-sm-8 col-md-8 col-lg-8">
                                <div class="panel panel-info schoolContactInformation">
                                    <div class="panel-heading">
                                        <h3 class="panel-title directorcontactinformationpicturepanel">Contact Information</h3>
                                    </div>
                                    <div class="panel-body ">
                                        <table class="table table-hover class_selector">
                                            <tbody>
                                                <tr>
                                                    <td class="text-center">Street Address</td>
                                                    <td class="text-center">
                                                        <asp:Label ID="streetAddressLabelID" runat="server" Text="Street Address"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="text-center">City</td>
                                                    <td class="text-center">
                                                        <asp:Label ID="cityLabelID" runat="server" Text="City"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">State</td>
                                                    <td class="text-center">
                                                        <asp:Label ID="stateLabelID" runat="server" Text="State"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">Zip</td>
                                                    <td class="text-center">
                                                        <asp:Label ID="zipLabelID" runat="server" Text="Zip"></asp:Label></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
            <%--school director information div ends--%>

            <div class="row">
                <div class="col-xs-12  col-sm-12 col-md-12 col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title  directorcontactinformationmainpanel">School Director Information</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-xs-4  col-sm-4 col-md-4 col-lg-4"></div>
                                <div class="col-xs-4  col-sm-4 col-md-4 col-lg-4">
                                    <center><div class="panel panel-info">
                                        <div class="panel-heading">
                                            
                                            <h3 class="panel-title directorcontactinformationpicturepanel"><i>
                                            <asp:Label ID="directorFullNamePicturePanelID" runat="server" Text="Full Name"></asp:Label></i></h3>
                                        </div>
                                        
                                        <div class="panel-body ">
                                        <center> <img class="userProfilePicture" src="Images/11_frankwylie.jpg" /></center>
                                    </div>
                                    </div></center>
                                </div>
                                <div class="col-xs-4  col-sm-4 col-md-4 col-lg-4"></div>
                            </div>

                        </div>


                        <div class="row">
                            <div class="col-xs-1  col-sm-1 col-md-1 col-lg-1"></div>
                            <div class="col-xs-10  col-sm-10 col-md-10 col-lg-10">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <h3 class="panel-title schoolInformationpicturepanel">Contact Information</h3>
                                    </div>
                                    <div class="panel-body ">
                                        <table class="table table-hover class_selector">
                                            <tbody>
                                                <tr>
                                                    <td class="text-center">Email</td>
                                                    <td class="text-center">
                                                        <asp:Label ID="emailLabelID" runat="server" Text="Email"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="text-center">Phone Number</td>
                                                    <td class="text-center">
                                                        <asp:Label ID="phoneNumberLabelID" runat="server" Text="Phone Number"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">Street Address</td>
                                                    <td class="text-center">
                                                        <asp:Label ID="directorStreetAddressLabelID" runat="server" Text="Street Address"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="text-center">City</td>
                                                    <td class="text-center">
                                                        <asp:Label ID="directorCityLabel" runat="server" Text="City"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">State</td>
                                                    <td class="text-center">
                                                        <asp:Label ID="directoryStateLabel" runat="server" Text="State"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">Zip</td>
                                                    <td class="text-center">
                                                        <asp:Label ID="directorZipLabel" runat="server" Text="Zip"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">Background Description</td>
                                                    <td>
                                                        <div class="well">
                                                            <div class="tab-content">
                                                                <div class="tab-pane active">

                                                                    <asp:Label ID="directorBackgroundDescriptionID" runat="server" Text="Background Description"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>


                                </div>
                                
                                <div class="col-xs-1  col-sm-1 col-md-1 col-lg-1"></div>
                            </div>
                            
                        </div>
                        
                    </div>
                    </div></div>
            <center>
                             
                                <asp:LinkButton ID="updateUserProfileID" runat="server"
                                                                class="btn btn-success btn-lg" OnClick="updateInformationButton_Click">
                                                        <i aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></i>Update Profile

                                </asp:LinkButton>

                                </center>


    </form>
</body>
</html>