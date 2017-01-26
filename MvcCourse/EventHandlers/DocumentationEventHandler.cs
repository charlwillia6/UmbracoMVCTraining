using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.PublishedContentModels;

namespace MvcCourse.EventHandlers
{
    public class DocumentationEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarting(umbracoApplication, applicationContext);

            //subscribe to all documents being published.
            ContentService.Published += ContentService_Published;
        }

        void ContentService_Published(Umbraco.Core.Publishing.IPublishingStrategy sender, Umbraco.Core.Events.PublishEventArgs<Umbraco.Core.Models.IContent> e)
        {
            //the Umbraco services context
            var services = UmbracoContext.Current.Application.Services;

            //get the landing page document type

            //get all landing pages
            
            //iterate through all published items (there can be more then one)
           
        }
    }
}