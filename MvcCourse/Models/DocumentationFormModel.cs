using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCourse.Models
{
    public class DocumentationFormModel
    {
        [Required]
        [AllowHtml]
        public string BodyText { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public IEnumerable<HttpPostedFileBase> Images { get; set; }
    }
}