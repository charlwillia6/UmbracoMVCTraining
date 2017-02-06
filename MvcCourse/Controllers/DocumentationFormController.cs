using MvcCourse.Helper;
using MvcCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedContentModels;

namespace MvcCourse.Controllers
{
    public class DocumentationFormController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult Render(Documentation model)
        {
            // create the forms view model
            var documentationFormModel = new DocumentationFormModel
            {
                BodyText = model.BodyText.ToString(),
                Name = model.Name
            };
            return PartialView("~/Views/Partials/DocumentationForm.cshtml", documentationFormModel);
        }

        [HttpPost]
        public ActionResult Submit(DocumentationFormModel model)
        {
            // Name and BodyText section
            if (ModelState.IsValid == false)
            {
                return CurrentUmbracoPage();
            }

            var currentPageId = CurrentPage.Id;
            var content = Services.ContentService.GetById(currentPageId);
            content.Name = model.Name;
            var bodyTextProperty = Documentation.GetModelPropertyType(d => d.BodyText);

            content.SetValue(bodyTextProperty.PropertyTypeAlias, model.BodyText);

            // Media Section
            var mediaService = Services.MediaService;
            
            if (model.Images.HasFiles() && model.Images.ContainsImages())
            {
                var imagesProperty = Documentation.GetModelPropertyType(d => d.Images);
                var folderId = content.GetValue<int>(imagesProperty.PropertyTypeAlias);

                if (folderId <= 0)
                {
                    var folder = mediaService.CreateMedia(model.Name, -1, Folder.ModelTypeAlias);

                    mediaService.Save(folder);
                    folderId = folder.Id;
                    content.SetValue(imagesProperty.PropertyTypeAlias, folderId);
                }

                foreach (var file in model.Images)
                {
                    if (file.IsImage())
                    {
                        // Process Files
                        var mediaImage = mediaService.CreateMedia(file.FileName, folderId, Image.ModelTypeAlias);
                        var umbracoFileProperty = Image.GetModelPropertyType(i => i.UmbracoFile);

                        mediaImage.SetValue(umbracoFileProperty.PropertyTypeAlias, file);
                        mediaService.Save(mediaImage);
                    }
                }
            }

            Services.ContentService.SaveAndPublishWithStatus(content);

            return RedirectToCurrentUmbracoPage();
        }
    }
}