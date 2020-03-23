<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Upendo.SkinObjects.ClearCache.View" %>
<%@ Import namespace="DotNetNuke.Services.Localization" %>
<div class="dnnClear uvAdminControls" runat="server" id="divControls">
    <asp:LinkButton runat="server" ID="btnClearCache" CssClass="dnnPrimaryAction uvPrimaryAction" resourcekey="btnClearCache" CausesValidation="False" OnClick="btnClearCache_OnClick" /> 
    <asp:LinkButton runat="server" ID="btnRestartApp" CssClass="dnnSecondaryAction uvSecondaryAction" resourcekey="btnRestartApp" CausesValidation="False" OnClick="btnRestartApp_OnClick" /> 
    <script language="javascript" type="text/javascript">/*<![CDATA[*/
    (function ($, Sys) {
        function setupUvAdminControls() {
            $('#<%= btnRestartApp.ClientID %>').dnnConfirm({
                text: '<%= GetLocalizedString("btnRestartApp.Confirm.Text") %>',
                yesText: '<%= Localization.GetString("Yes.Text", Localization.SharedResourceFile) %>',
                noText: '<%= Localization.GetString("No.Text", Localization.SharedResourceFile) %>',
                title: '<%= Localization.GetString("Confirm.Text", Localization.SharedResourceFile) %>'
            });
        }

        $(document).ready(function () {
            setupUvAdminControls();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                setupUvAdminControls();
            });
        });

    }(jQuery, window.Sys));
    /*]]>*/</script>
</div>