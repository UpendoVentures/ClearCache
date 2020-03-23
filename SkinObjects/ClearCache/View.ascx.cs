
using DotNetNuke.Services.Exceptions;
using System;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Instrumentation;
using DotNetNuke.Web.Client.ClientResourceManagement;
using Upendo.SkinObjects.ClearCache.Components;

namespace Upendo.SkinObjects.ClearCache
{
    public partial class View : ClearCacheModuleBase
    {
        private static readonly ILog Logger = LoggerSource.Instance.GetLogger(typeof(ClearCacheModuleBase));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var user = UserController.Instance.GetCurrentUserInfo();
                var adminRoleName = PortalSettings.AdministratorRoleName;

                if (user != null && user.UserID > Null.NullInteger &&
                    (user.IsSuperUser || user.IsInRole(adminRoleName)))
                {
                    // show the ui
                    btnClearCache.Visible = true;
                    btnRestartApp.Visible = user.IsSuperUser;
                    divControls.Visible = true;

                    btnClearCache.Text = GetLocalizedString("btnClearCache.Text");
                    if (btnRestartApp.Visible)
                    {
                        btnRestartApp.Text = GetLocalizedString("btnRestartApp.Text");
                    }

                    JavaScript.RequestRegistration(CommonJs.DnnPlugins);
                }
                else
                {
                    // hide the ui
                    btnClearCache.Visible = false;
                    btnRestartApp.Visible = false;
                    divControls.Visible = false;
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Logger.Error(exc.Message, exc);
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void btnClearCache_OnClick(object sender, EventArgs e)
        {
            try
            {
                Logger.Debug("Attempting to clear the cache.");
                DataCache.ClearCache();
                ClientResourceManager.ClearCache();
                Logger.Debug("Cleared the cache.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Exceptions.LogException(ex);
            }
            
        }

        protected void btnRestartApp_OnClick(object sender, EventArgs e)
        {
            try
            {
                var user = UserController.Instance.GetCurrentUserInfo();
                if (user.IsSuperUser)
                {
                    Logger.Debug("Attempting to restart the application pool.");
                    Config.Touch();
                }
                else
                {
                    Logger.Debug("Non-superuser attempted to restart the application pool.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Exceptions.LogException(ex);
            }
        }
    }
}