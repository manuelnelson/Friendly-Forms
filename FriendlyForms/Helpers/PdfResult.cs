using System.Net.Mime;
using System.Web.Mvc;

namespace FriendlyForms.Helpers
{
    public class PdfResult : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/pdf";
            var cd = new ContentDisposition
            {
                Inline = true,
                FileName = "form.pdf",
            };
            response.AddHeader("Content-Disposition", cd.ToString());
        }
    }
}