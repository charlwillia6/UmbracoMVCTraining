using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web.PublishedContentModels;

namespace MvcCourse.Models
{
    public class DocumentationViewModel : Documentation
    {
        public DocumentationViewModel(IPublishedContent content):base(content)
            {}

        public IEnumerable<LandingPage> LandingPages { get; internal set; }
    }
}