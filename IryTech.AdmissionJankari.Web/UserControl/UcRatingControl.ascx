﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcRatingControl.ascx.cs" Inherits="IryTech.AdmissionJankari.Web.UserControl.UcRatingControl" %>
<%@ Register Assembly="Spaanjaars.Toolkit" Namespace="Spaanjaars.Toolkit" TagPrefix="AJ" %>
<asp:UpdatePanel ID="updateRating" runat="server">
    <ContentTemplate>
        <AJ:ContentRating
            ID="ContentRating1"
            runat="server"
            LegendText="rates: {0} avg: {1}"
            OnRated="ContentRating1_Rated"
            OnRating="ContentRating1_Rating" ToolTip="Rate"></AJ:ContentRating>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function MessageRating(message) {
        $("#divReportMessage").html("");
        $("#divReportMessage").append(message);
        $("#divReportMessage").show();

        $("#divReportMessage").fadeOut(10000);
    }
</script>
