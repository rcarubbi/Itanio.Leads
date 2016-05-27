using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Itanio.Leads.WebUI.ActionResults
{
    public class ExternalFileResult : ActionResult
    {
        private readonly string _url;
        public ExternalFileResult(string url)
        {
            _url = url;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var cd = new ContentDisposition
            {
                FileName = Path.GetFileName(_url),
                Inline = false
            };
            var response = context.HttpContext.Response;
            response.ContentType = "application/octet-stream";
            response.Headers["Content-Disposition"] = cd.ToString();
            
            using (var client = new WebClient())
            using (var stream = client.OpenRead(_url))
            {
                // in .NET 4.0 implementation this will process in chunks
                // of 4KB
                stream.CopyTo(response.OutputStream);
            }
        }

    }
}
