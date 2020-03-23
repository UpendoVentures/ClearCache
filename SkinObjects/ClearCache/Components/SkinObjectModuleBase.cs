using System.IO;
using DotNetNuke.Instrumentation;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Skins; 

namespace Upendo.SkinObjects.ClearCache.Components
{
    public class ClearCacheModuleBase : SkinObjectBase 
	{
        private static readonly ILog Logger = LoggerSource.Instance.GetLogger(typeof(ClearCacheModuleBase));
        private string _localResourceFile;
        private const string DEFAULTCONTROLNAME = "View.ascx";

        protected string ControlPath 
		{
            get 
			{
                return string.Concat(TemplateSourceDirectory, "/"); 
            }
        }

        protected string GetLocalizedString(string Key)
        {
            return Localization.GetString(Key, LocalResourceFile);
        }

        protected string GetLocalizedString(string Key, string LocalizationFilePath)
        {
            return Localization.GetString(Key, LocalizationFilePath);
        }

        protected string LocalResourceFile
        {
            get
            {
                string fileRoot;
                var controlName = (string.IsNullOrEmpty(ID)) ? DEFAULTCONTROLNAME : ID;
                fileRoot = string.IsNullOrEmpty(_localResourceFile) ? Path.Combine(ControlPath, string.Concat(Localization.LocalResourceDirectory, "/", controlName)) : _localResourceFile;
                return fileRoot;
            }
            set
            {
                _localResourceFile = value;
            }
        }
    }
}