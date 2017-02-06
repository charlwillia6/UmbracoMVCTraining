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
            var type = services.ContentTypeService.GetContentType(LandingPage.ModelTypeAlias);

            //iterate through all published items (there can be more then one)
            foreach (var page in e.PublishedEntities)
            {
                // only do this if the page is a documentation page
                if (page.ContentType.Alias == Documentation.ModelTypeAlias)
                {
                    // get all landing pages
                    var landingPages = services.ContentService.GetContentOfContentType(type.Id).ToList();

                    // relate the pages
                    var bodyTextProperty = Documentation.GetModelPropertyType(l => l.BodyText);
                    var bodyText = page.GetValue<string>(bodyTextProperty.PropertyTypeAlias);

                    foreach (var landingPage in landingPages)
                    {
                        if (bodyText.ToLower().Contains(landingPage.Name.ToLower()))
                        {
                            var relationService = services.RelationService;

                            if (!relationService.AreRelated(page, landingPage, "product"))
                            {
                                relationService.Relate(page, landingPage, "product");
                            }
                        }
                        else
                        {
                            var relationService = services.RelationService;

                            if (relationService.AreRelated(page, landingPage, "product"))
                            {
                                var deleteRelation = relationService.GetAllRe;


                                //relationService.Delete(deleteRelation);
                            }
                        }
                    }
                    
                }
            }

        }
    }
}