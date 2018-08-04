using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace Telligent.Services.SamlAuthenticationPlugin.Components
{
    public class RequestTypeHandlerMethodConstraint : IRouteConstraint
    {

        private List<string> _verbs = null;

        public string[] Verbs
        {
            get { return _verbs.ToArray(); }
        }
        public RequestTypeHandlerMethodConstraint(params string[] verbs)
        {
            var verbsList = new List<string>();
            foreach (string verb in verbs)
                verbsList.Add(verb.ToUpperInvariant());

            _verbs = verbsList;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var inMethod = httpContext.Request.HttpMethod.ToUpperInvariant();
            return _verbs.Contains(inMethod);
        }
    }
}
