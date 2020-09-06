using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.Owin;

namespace EmailSendingWithHangfireBackgroundService.AuthorizationFilter
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var owinContext = context.GetHttpContext();

            return owinContext.User.IsInRole("Admin");
        }
    }
}
