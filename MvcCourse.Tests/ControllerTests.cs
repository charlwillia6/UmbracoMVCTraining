using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Moq;
using MvcCourse.Controllers;
using NUnit.Framework;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;
using Umbraco.Core.Profiling;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;

namespace MvcCourse.Tests
{
    [TestFixture]
    public class ControllerTests
    {
        [Test]
        public void FormController_Returns_Model()
        {
            var appCtx = MockApplicationContext();
            var umbCtx = MockUmbracoContext(appCtx);

          //  var ctrl = new DocumentationFormController();
            
        }


        //HELPERS

        private static ApplicationContext MockApplicationContext()
        {
            return new ApplicationContext(CacheHelper.CreateDisabledCacheHelper(),
                new ProfilingLogger(new Mock<ILogger>().Object, new Mock<IProfiler>().Object));
        }

        private static UmbracoContext MockUmbracoContext(ApplicationContext appCtx)
        {
            return UmbracoContext.EnsureContext(
                new Mock<HttpContextBase>().Object,
                appCtx,
                new Mock<WebSecurity>(null, null).Object,
                Mock.Of<IUmbracoSettingsSection>(section => section.WebRouting == Mock.Of<IWebRoutingSection>(routingSection => routingSection.UrlProviderMode == "AutoLegacy")),
                Enumerable.Empty<IUrlProvider>(),
                true);
        }
    }
}
