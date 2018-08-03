using mezzanine.Utility;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Primitives;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace mezzanine.MVCBases
{
    /// <summary>
    /// Base page for Razor views.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class RazorBase<TModel> : RazorPage<TModel>
    {
        public AssemblyInfo AppInfo { get; private set; } = null;
        public CultureInfo ServerCulture { get; private set; } = CultureInfo.CurrentCulture;
        public CultureInfo UserCulture { get; private set; } = CultureInfo.CurrentUICulture; // default

        public RazorBase() : base()
        {
            this.AppInfo = new AssemblyInfo(Assembly.GetEntryAssembly());
            this.DetectRequestUserLanguage();
        }

        /// <summary>
        /// Get the users accept language from the request header.
        /// </summary>
        private void DetectRequestUserLanguage()
        {
            string requestUILang = this.UserCulture.Name;

            StringValues headerDataStringValues = StringValues.Empty;

            string firstAcceptLanguage = string.Empty;

            char[] MainSplit = new char[] { ';' }; // The header accept language needs splitting twice
            char[] SecondSplit = new char[] { ',' };

            try
            {
                if (base.Context?.Request?.Headers?.TryGetValue("Accept-Language", out headerDataStringValues) == true)
                {
                    firstAcceptLanguage = headerDataStringValues.FirstOrDefault();
                    firstAcceptLanguage = firstAcceptLanguage.Split(MainSplit)[0];
                    firstAcceptLanguage = firstAcceptLanguage.Split(SecondSplit)[0];
                    if (firstAcceptLanguage != string.Empty || firstAcceptLanguage != null)
                    {
                        requestUILang = firstAcceptLanguage;
                    }
                }                
            }
            catch
            {
                requestUILang = this.UserCulture.Name;
            }
            finally
            {
                if (this.UserCulture.Name != requestUILang)
                {
                    this.UserCulture = new CultureInfo(requestUILang);
                }
            }
        }

        /// <summary>
        /// Returns the text direction according to the user culture. Use the the <html> tag.
        /// </summary>
        /// <returns></returns>
        public string TextDirection
        {
            get
            {
                this.DetectRequestUserLanguage();
                string strReturn = "ltr";
                if (this.UserCulture.TextInfo.IsRightToLeft == true)
                {
                    strReturn = "rtl";
                }
                return strReturn;
            }
        }

        /// <summary>
        /// Get the ISO user language.
        /// </summary>
        /// <returns></returns>
        public string UserLang
        {
            get
            {
                this.DetectRequestUserLanguage();
                return this.UserCulture.Name;
            }
        }

        public string RequestUrl
        {
            get
            {
                return base.Context?.Request?.Path;
            }
        }
    }
}
