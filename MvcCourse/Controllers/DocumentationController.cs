using MvcCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedContentModels;

namespace MvcCourse.Controllers
{
    public class DocumentationController : RenderMvcController
    {
        // GET: Documentation
        public override ActionResult Index(RenderModel model)
        {
            // get the related pages
            var ids = Services.RelationService.GetByParentId(model.Content.Id).Select(x => x.ChildId);
            // convert them to LandingPage classes
            var products = Umbraco.TypedContent(ids).OfType<LandingPage>();
            // Create a new view model and pass to the page
            var viewModel = new DocumentationViewModel(model.Content)
            {
                LandingPages = products
            };

            // Return the new model to the view
            return CurrentTemplate(viewModel);
        }
    }
}