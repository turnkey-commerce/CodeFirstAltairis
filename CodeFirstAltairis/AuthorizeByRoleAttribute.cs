using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeFirstAltairis {
    public class AuthorizeByRoleAttribute : AuthorizeAttribute {
 

        protected void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus) {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public override void OnAuthorization(AuthorizationContext filterContext) {
            if (filterContext == null) {
                throw new ArgumentNullException("filterContext");
            }

            if (AuthorizeCore(filterContext.HttpContext)) {
                SetCachePolicy(filterContext);
            } else if (!filterContext.HttpContext.User.Identity.IsAuthenticated) {
                // auth failed, redirect to login page
                filterContext.Result = new HttpUnauthorizedResult();
            } else {
                filterContext.Result = new ViewResult { ViewName = "AccessDenied"};
            }

        }

        protected void SetCachePolicy(AuthorizationContext filterContext) {
            // ** IMPORTANT **
            // Since we're performing authorization at the action level, the authorization code runs
            // after the output caching module. In the worst case this could allow an authorized user
            // to cause the page to be cached, then an unauthorized user would later be served the
            // cached page. We work around this by telling proxies not to cache the sensitive page,
            // then we hook our custom authorization code into the caching mechanism so that we have
            // the final say on whether a page should be served from the cache.
            HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
            cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
        }
    }
}