<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolApprovalAndRatingPage.aspx.cs" Inherits="Stripe.SchoolApprovalAndRatingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approval & Ratings Page</title>
    <link href="./bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="./bootstrap/customBootstrapJS/jquery-latest.js"></script>
    <script src="./bootstrap/js/bootstrap.js"></script>

    <link href="./bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="./bootstrap/css/bootstrap.css" rel="stylesheet" />

    <link href="./bootstrap/customBootstrapCSS/prettify.css" rel="stylesheet" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>
    <link href="Content/Custom_StyleSheet/SchoolApprovalRatingPage.css" rel="stylesheet" />
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.1.4.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" rel="stylesheet" />
    <script src="Content/Custom_Javascript/SchoolApporvalAndRatingPage.js"></script>
    <script>
        $(function () {
            $('#myTab a:last').tab('show');

        })
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div>
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
                                        <li>
                                            <asp:HyperLink NavigateUrl="SchooHomePage.aspx" runat="server"><span class="glyphicon glyphicon-home" aria-hidden="true"></span><b> Home</b><span class="sr-only"></span></asp:HyperLink>

                                        </li>
                                        <li>
                                            <asp:HyperLink NavigateUrl="SchoolDirectorEventsPage.apsx" runat="server"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span><b> Events</b><span class="sr-only"></span></asp:HyperLink>


                                        </li>
                                        <li>
                                            <asp:HyperLink NavigateUrl=" " runat="server"><span class="glyphicon glyphicon-search" aria-hidden="true"></span><b> Search</b><span class="sr-only"></span></asp:HyperLink>


                                        </li>
                                        <li class="active">
                                            <asp:HyperLink NavigateUrl="SchoolApprovalAndRatingPage.aspx" runat="server"><span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span><b> Approval & Rating</b><span class="sr-only"></span></asp:HyperLink>


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
                    <%--body div content starts--%>

                    <div class="col-xs-12 col-sm12 col-md-12 col-lg-12">
                        <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2">
                            <%--approval and review navigation panel starts--%>
                            <div class="panel panel-primary transparent">
                                <div class="panel-heading approvalReviewNavigationPanelStyling"><i>Stripe</i></div>
                                <div class="panel-body">
                                    <ul class="nav nav-pills nav-stacked navPanelStyling" role="tablist">
                                        <li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">Approval</a></li>
                                        <li role="presentation"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">Review</a></li>

                                    </ul>

                                </div>
                                <div class="panel-footer pendingApprovalCountStyling">
                                    Pending Approvals: 
                                    <div class="panel panel-danger">
                                        <asp:Label ID="pendingApprovalRefereeCountID" runat="server" Text="1"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--approval and review navigation panel ends--%>


                        <div class="col-xs-10 col-sm10 col-md-10 col-lg-10">
                            <%--approval and review navigation body starts--%>

                            <div class="tab-content">
                                <div role="tabpanel" class="tab-pane fade in active" id="home">
                                    <%--approval navigation body panel starts--%>
                                    <div class="panel panel-primary navigationTabsBodyPanelStyling">
                                        <div class="panel-heading  refereeApprovalMainPanelHeadingStyling">Pending Referee Approvals</div>

                                        <div class="panel-body">
                                            <div class="row" style="margin-top: 20px">
                                                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                <div class="col-xs-6 col-sm6 col-md-6 col-lg-6">

                                                    <div class="panel  panel-success">
                                                        <%--home and away panel starts--%>

                                                        <table class="table table-hover text-center">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="homeTeamNameID" runat="server" Text="HOME"></asp:Label>
                                                                    </td>

                                                                    <td></td>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="awayTeamNameID" runat="server" Text="AWAY"></asp:Label>
                                                                    </td>
                                                                </tr>

                                                                <tr class=" addcolor3">
                                                                    <td class="text-center">
                                                                        <center><div class="schoolImagesStyling"><img src="Images/2_Normal_Comm_Logo.jpg" /></div></center>
                                                                    </td>

                                                                    <td>
                                                                        <h2 class="googleFont" style="color: black">VS</h2>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <center><div class="schoolImagesStyling"><img src="Images/2_Normal_Comm_Logo.jpg" /></div></center>
                                                                    </td>
                                                                </tr>
                                                                <tr class=" addcolor2">
                                                                    <td class="text-center">
                                                                        <asp:Label ID="homeSchoolNameID" runat="server" Text="Home School Name"></asp:Label>
                                                                    </td>

                                                                    <td></td>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="awaySchoolNameID" runat="server" Text="Away School Name"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>

                                                    </div>
                                                    <%--home and away panel ends--%>
                                                </div>

                                                <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                                    <%--game information panel starts--%>
                                                    <div class="panel panel-success">
                                                        <table class="table table-hover">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="eventDateLabelID" runat="server" Text="Date"></asp:Label>
                                                                    </td>


                                                                    <td class="text-center">
                                                                        <asp:Label ID="eventTimeLabelID" runat="server" Text="Time"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="sportEventTypeID" runat="server" Text="Sport Type"></asp:Label>
                                                                    </td>

                                                                    <td class="text-center">
                                                                        <div class="gameinformationImagesStyling">
                                                                            <img src="Images/stormTrooper.jpg" id="sportTypeImageID" />
                                                                        </div>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="gameLocationID" runat="server" Text="Location"></asp:Label>
                                                                    </td>


                                                                    <td class="text-center">
                                                                        <asp:Label ID="gameLocationFieldNameID" runat="server" Text="Field Name"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <%--game information panel ends--%>
                                            </div>



                                            <div class="row">
                                                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10">
                                                    <div class="panel panel-success">
                                                        <%--pending referee approval list panel starts--%>
                                                        <div class="panel-heading mainPanelStyling">Pending Approvals</div>

                                                        <div class="panel-body">
                                                            <table class="table table-hover">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="text-center"></td>

                                                                        <td class="text-center">
                                                                            <asp:Label ID="refereeType2ID" runat="server" Text="Referee Type 2"></asp:Label>
                                                                        </td>
                                                                        <td class="text-center"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="text-center"></td>

                                                                        <td class="text-center">
                                                                            <center>
                                                                                <div class="pendingRefereePictureStyling"><img src="Images/stormTrooper.jpg" id="refereeType2ImageID" /></div>

                                                                            </center>
                                                                        </td>
                                                                        <td class="text-center"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="text-center"></td>

                                                                        <td class="text-center">
                                                                            <asp:Label ID="referee2NameID" runat="server" Text="Referee Name 2"></asp:Label>
                                                                        </td>
                                                                        <td class="text-center"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td>
                                                                            <center>
                                                                                <asp:LinkButton ID="approveReferee2Button" runat="server"
                                                                                class="btn btn-success btn-lg" OnClick="approveReferee_Click">
                                                        <i aria-hidden="true" class="glyphicon glyphicon-thumbs-up"></i>Approve

                                                                            </asp:LinkButton>

                                                                            </center>
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td>
                                                                            <center>
                                                                                              <asp:LinkButton ID="RejectReferee2Button" runat="server"
                                                                                class="btn btn-danger btn-lg" OnClick="rejectReferee_Click">
                                                        <i aria-hidden="true" class="glyphicon glyphicon-thumbs-down"></i>Reject

                                                                            </asp:LinkButton>

                                                                            </center>
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <%--pending referee approval list panel ends--%>
                                                </div>
                                                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>
                                            </div>

                                        </div>




                                        <div class="row">
                                            <!-- View Referee in modal Information starts -->
                                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"></div>
                                            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                                <center><a data-toggle="modal" href="#myModal1" class="btn btn-primary btn-lg">Detailed Referee Information</a></center>
                                            </div>
                                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"></div>
                                        </div>
                                        <br />
                                        <br />


                                        <!-- Modal body starts-->
                                        <div class="modal fade modalBodyStyle" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header modal-header-info">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                            <p style="color: white; ba">X</p>
                                                        </button>
                                                        <h4 class="modal-title modalTitleStyling">Detailed Referee Information</h4>
                                                    </div>
                                                    <div class="modal-body">

                                                        <!-- Referee-1 information starts -->
                                                        <div class="panel panel-primary">
                                                            <div class="row">

                                                                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>

                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">

                                                                    <div class="panel panel-default" style="margin-top: 20px">
                                                                        <div class="panel-heading mainPanelStyling">
                                                                            <asp:Label class="modalcareerandcontactbodystyling" ID="referee1FullNameID" runat="server" Text="Referee Name"></asp:Label>
                                                                        </div>
                                                                        <div class="panel-body" style="margin-bottom: 9px">
                                                                            <center>
                                                                            <div>
                                                                                <img src="Images/11_frankwylie.jpg" />
                                                                           </div>
                                                                         </center>
                                                                        </div>
                                                                        <div class="panel-footer mainPanelStyling">
                                                                            <asp:Label class="modalcareerandcontactbodystyling" ID="referee1GameTypeID" runat="server" Text="Game Type"></asp:Label>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7">
                                                                    <div class="panel panel-default" style="margin-top: 20px">
                                                                        <div class="panel-heading modalcareerandcontactheadingstyling">Career & Contact Information</div>
                                                                        <div class="panel-body">
                                                                            <table class="table table-hover">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td class="modalcareerandcontactbodystyling">User Ratings:
                                                                                        </td>

                                                                                        <td class="modalcareerandcontactbodystyling">
                                                                                            <span class="rating-star">
                                                                                                <span class="rating-item">5</span>
                                                                                                <span class="rating-item">4</span>
                                                                                                <span class="rating-item">3</span>
                                                                                                <span class="rating-item">2</span>
                                                                                                <span class="rating-item">1</span>

                                                                                                <span class="rating-item result" id="referee1StarRatingID" style="width: 20%" runat="server"></span>
                                                                                            </span>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="modalcareerandcontactbodystyling">Game Specialization: 
                                                                                        </td>
                                                                                        <td class="modalcareerandcontactbodystyling">
                                                                                            <asp:Label ID="referee1gameSpecializationID" runat="server" Text="Ratings"></asp:Label>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="modalcareerandcontactbodystyling">Total Games Officiated:
                                                                                        </td>

                                                                                        <td class="modalcareerandcontactbodystyling">
                                                                                            <asp:Label ID="referee1TotalGamesOfficiatedID" runat="server" Text="Total Games"></asp:Label>
                                                                                        </td>

                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td class="modalcareerandcontactbodystyling">Email: 
                                                                                        </td>

                                                                                        <td class="modalcareerandcontactbodystyling">
                                                                                            <asp:Label ID="referee1ContactEmailID" runat="server" Text="Referee Email"></asp:Label>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="modalcareerandcontactbodystyling">Phone Number : 
                                                                                        </td>

                                                                                        <td class="modalcareerandcontactbodystyling">
                                                                                            <asp:Label ID="referee1PhoneNumberID" runat="server" Text="Referee Phone Number"></asp:Label>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="modalcareerandcontactbodystyling">Street Address : 
                                                                                        </td>

                                                                                        <td class="modalcareerandcontactbodystyling">
                                                                                            <asp:Label ID="refereeStreetAddressLabelID" runat="server" Text="Referee Street Address"></asp:Label>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="modalcareerandcontactbodystyling">City : 
                                                                                        </td>

                                                                                        <td class="modalcareerandcontactbodystyling">
                                                                                            <asp:Label ID="refereeCityLabelID" runat="server" Text="Referee City"></asp:Label>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="modalcareerandcontactbodystyling">State : 
                                                                                        </td>

                                                                                        <td class="modalcareerandcontactbodystyling">
                                                                                            <asp:Label ID="refereeStateLabelID" runat="server" Text="Referee State"></asp:Label>
                                                                                        </td>

                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>



                                                            </div>

                                                            <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>

                                                        </div>

                                                        <!-- Referee-1 information ends -->

                                                    </div>


                                                </div>
                                                <div class="modal-footer">
                                                </div>
                                            </div>
                                            <!-- modal-content ends-->
                                        </div>
                                        <!-- modal-dialog ends-->
                                    </div>
                                    <!-- modal body ends -->
                                    <!-- view referee information ends -->
                                    <br />
                                    <br />
                                </div>
                                <%--Approval navigation body ends--%>



                                <div role="tabpanel" class="tab-pane fade" id="profile">
                                    <%--Review navigation body starts--%>
                                    <div class="panel panel-success navigationTabsBodyPanelStyling">
                                        <div class="panel-heading  refereeApprovalMainPanelHeadingStyling">Pending Referee Ratings</div>
                                        <div class="panel-body">
                                            <div class="row" style="margin-top: 20px">
                                                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                <div class="col-xs-6 col-sm6 col-md-6 col-lg-6">
                                                    <div class="panel  panel-success">
                                                        <%--home and away panel starts--%>

                                                        <table class="table table-hover text-center">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="refereeRatingHomeTeamNameLabelID" runat="server" Text="HOME"></asp:Label>
                                                                    </td>

                                                                    <td></td>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="refereeRatingAwayTeamNameLabelID" runat="server" Text="AWAY"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class=" addcolor3">
                                                                    <td class="text-center">
                                                                        <center><div class="schoolImagesStyling"><img src="Images/2_Normal_Comm_Logo.jpg" /></div></center>
                                                                    </td>

                                                                    <td>
                                                                        <h2 class="googleFont" style="color: black">VS</h2>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <center><div class="schoolImagesStyling"><img src="Images/2_Normal_Comm_Logo.jpg" /></div></center>
                                                                    </td>
                                                                </tr>

                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>

                                                <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                                    <%--game information panel starts--%>
                                                    <div class="panel panel-success">
                                                        <table class="table table-hover">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="refereeRatingEventDateLabelID" runat="server" Text="Date"></asp:Label>
                                                                    </td>


                                                                    <td class="text-center">
                                                                        <asp:Label ID="refereeRatingEventTimeLabelID" runat="server" Text="Time"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="dummyLabelID" runat="server" Text="Sport Type"></asp:Label>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="refereeRatingSportTypeLabelID" runat="server" Text="Sport Type"></asp:Label>
                                                                    </td>


                                                                </tr>
                                                                <tr>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="refereeRatingEventLocationLabelID" runat="server" Text="Location"></asp:Label>
                                                                    </td>


                                                                    <td class="text-center">
                                                                        <asp:Label ID="refereeRatingEventFieldNameID" runat="server" Text="Field Name"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1"></div>
                                                <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10">
                                                    <div class="panel panel-success">
                                                        <%--pending referee approval list panel starts--%>
                                                        <div class="panel-heading mainPanelStyling">Rating Panel</div>
                                                        <div class="panel-body">
                                                            <div class="row">

                                                                <div class="col-xs-1 col-sm-1 col-md-3 col-lg-3">
                                                                    <table class="table table-hover">
                                                                        <tbody>
                                                                            <tr>


                                                                                <td class="text-center">
                                                                                    <asp:Label ID="refereeRatingRefereeTypeLabelID" runat="server" Text="Referee Type 2"></asp:Label>
                                                                                </td>

                                                                            </tr>
                                                                            <tr>


                                                                                <td class="text-center">
                                                                                    <center>
                                                                                <div class="pendingRefereePictureStyling"><img src="Images/stormTrooper.jpg" id="refereeType2ImageID" /></div>

                                                                            </center>
                                                                                </td>

                                                                            </tr>
                                                                            <tr>


                                                                                <td class="text-center">
                                                                                    <asp:Label ID="refereeRatingRefereeNameLabelID" runat="server" Text="Referee Name 2"></asp:Label>
                                                                                </td>

                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                                <div class="col-xs-1 col-sm-1 col-md-9 col-lg-9">
                                                                    <center>
                                                                    
                                                                
                                                                <asp:Label class="pleaseProvideFinalScoreID" runat="server" Text="Please Provide Rating"></asp:Label></center>
                                                                    <asp:DropDownList class="form-control" ID="RefereeRatingValue" runat="server">
                                                                        <asp:ListItem Selected="True">Please Select</asp:ListItem>
                                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    <center><asp:Label class="pleaseProvideFinalScoreID" runat="server" Text="Please Provide Final Scores Below"></asp:Label></center>
                                                                    <br/>
                                                                    <table class="table table-hover">
                                                                        <tbody>
                                                                            <tr>


                                                                                <td class="text-center">Away Team Score
                                                                                </td>
                                                                                <td class="text-center">
                                                                                    <asp:TextBox ID="awayTeamScoreTextFieldID" placeholder="SCORE" class="form-control" runat="server"></asp:TextBox>
                                                                                </td>

                                                                            </tr>
                                                                            <tr>


                                                                                <td class="text-center">Home Team Score
                                                                                </td>
                                                                                <td class="text-center">
                                                                                    <asp:TextBox ID="homeTeamScoreTextFieldID" placeholder="SCORE" class="form-control" runat="server"></asp:TextBox>
                                                                                </td>

                                                                            </tr>

                                                                        </tbody>
                                                                    </table>
                                                                    <div>
                                                                        <center>
                                                                        <asp:LinkButton ID="saveRefereeRatingInformation" runat="server" CssClass="btn btn-success btn-lg" OnClick="refereeRatingButton_OnClick">
                                                        <i aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></i> Rate Referee</asp:LinkButton></center>
                                                                    </div>
                                                                </div>


                                                                


                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <%--Review navigation body ends--%>
                            </div>

                        </div>
                        <%--approval and review navigation body ends--%>
                    </div>



                </div>

            </div>
        </div>
        <%--body div content ends--%>
    </form>
    <script>
        $('#myTab a').click(function (e) {
            e.preventDefault();
            $(this).tab('show');
        })
    </script>
</body>
</html>
