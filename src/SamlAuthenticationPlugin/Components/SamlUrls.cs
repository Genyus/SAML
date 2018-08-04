using System.Web;
using Telligent.Caching;
using Telligent.Common;
using Telligent.Evolution.Components;
using Telligent.Evolution.Configuration;

namespace Telligent.Services.SamlAuthenticationPlugin.Components
{
    public class SamlUrls : ApplicationUrls
    {
        /// <summary>
        /// Optional Nexus provider name
        /// </summary>
        public static readonly string SamlUrlsProviderName = "SamlUrlsProvider";

        #region Instance

        private static SamlUrls _defaultInstance;

        static SamlUrls()
        {
            CreateDefaultCommonProvider();
        }

        /// <summary>
        /// Returns an instance of the user-specified data provider class.
        /// </summary>
        /// <returns>An instance of the user-specified data provider class.  This class must inherit the
        /// CommonDataProvider interface.</returns>
        public static SamlUrls Instance()
        {
            return _defaultInstance;
        }

        /// <summary>
        /// Creates an instance of the specified data provider.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        /// <returns></returns>
        public static SamlUrls Instance(Provider dataProvider)
        {
            ICacheService cacheService = Common.Services.Get<ICacheService>();
            SamlUrls urlProvider = cacheService.Get(dataProvider.Name, CacheScope.All) as SamlUrls;

            if (urlProvider == null)
            {
                urlProvider = DataProviders.Invoke(dataProvider) as SamlUrls;
                cacheService.Put(dataProvider.Name, urlProvider, CacheScope.All);
            }
            return urlProvider;
        }

        /// <summary>
        /// Creates the Default CommonDataProvider
        /// </summary>
        private static void CreateDefaultCommonProvider()
        {
            // Read the configuration specific information
            // for this provider
            //
            Provider urlProvider = (Provider)CSConfiguration.GetConfig().Providers[SamlUrlsProviderName];

            // Create the instance for the provider, or fall back on the builtin one
            //
            if (urlProvider != null)
                _defaultInstance = CreateInstance(urlProvider) as SamlUrls;
            else
                _defaultInstance = new SamlUrls();
        }

        #endregion

        /// <summary>
        /// Modal window that connects an account for the specified provider.
        /// </summary>
        /// <returns></returns>
        public virtual string SamlResponseHandler()
        {
            return FormatUrl("Saml_Response");
        }

        /// <summary>
        /// Redirects share requests through handler for logging purposes
        /// </summary>
        /// <returns></returns>
        public virtual string SamlAuthnHandler()
        {
            return FormatUrl("Saml_Authn");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string SamlLogout()
        {
            return FormatUrl("Saml_Logout");
        }
    }
}
