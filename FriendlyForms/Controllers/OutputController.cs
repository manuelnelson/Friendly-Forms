using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Web.Mvc;
using BusinessLogic.Contracts;
using Models;
using Pechkin;
using Pechkin.Synchronized;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.Controllers
{
    public class OutputController : ControllerBase
    {
        private readonly SynchronizedPechkin _synchronizedPechkin;
        private readonly IParticipantService _participantService;

        public OutputController(IParticipantService participantService)
        {
            _participantService = participantService;
            var globalConfig = new GlobalConfig();
            globalConfig.SetPaperSize(PaperKind.Letter);
            var margins = new Margins(100, 100, 100, 100);
            globalConfig.SetMargins(margins);
            _synchronizedPechkin = new SynchronizedPechkin(globalConfig);
        }
        //
        // GET: /Output/
        [HttpPost]
        [ValidateInput(false)]
        [Authenticate]
        public ActionResult Pdf(string html, string name = "form")
        {
            var contentPath = Server.MapPath("~/Content/");
            var css = System.IO.File.ReadAllText(Path.Combine(contentPath, "pdf.css"));
            var fullHtml = string.Format(@"<!DOCTYPE html> <html> <head><style type=""text/css"">{0}</style></head><body><div id=""main-content"">{1}</div></body></html>", css, html);
            var config = new ObjectConfig();
            config.SetAllowLocalContent(true);
            config.SetPrintBackground(true);

            var userId = Convert.ToInt32(UserSession.CustomId);
            var participants = _participantService.GetByUserId(userId) as Participant;

            if (Request.Url != null)
            {
                var headerUrl = Request.Url.AbsoluteUri.Replace(Request.Url.LocalPath, "") + "/Headers/Header/" + userId;
                config.Header.SetHtmlContent(headerUrl);
            }
            config.Footer.SetFont(new Font("Times New Roman", 8F, FontStyle.Regular));
            config.Footer.SetTexts(participants.PlaintiffsName + " Initials_______", @"[page] of [topage]", participants.DefendantsName + " Initials________");
            var pdfBuf = _synchronizedPechkin.Convert(config, fullHtml);
            if (!name.Contains(".pdf"))
            {
                name = name + ".pdf";    
            }            
            return File(pdfBuf, "application/pdf", name);
        }
    }
}
