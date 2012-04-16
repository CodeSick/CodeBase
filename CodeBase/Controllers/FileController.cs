using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeBase.Helper;
using CodeBase.Models;
using DropNet;

namespace CodeBase.Controllers
{
    public class FileController : Controller
    {
        CodeBaseContext context = new CodeBaseContext();
        DropNetClient c;

        public FileController()
        {
            c = new DropNetClient("fu6v5k1k9s7g4g7", "ctrgl1yqii2pjuw","9jyurcfypebd4kd","g8m4prvbove7k2a");
        }

        private string StorageRoot
        {
            get { return Path.Combine(Server.MapPath("~/Files")); }
        }


        public ActionResult Status(int articleId)
        {
            if (Request.RequestType == "POST")
            {
                return Upload1(articleId);
            }
            var files = context.Files.Where(x => x.ArticleId == articleId);
            List<ViewDataUploadFilesResult> status = new List<ViewDataUploadFilesResult>();
            foreach(var i in files){
                var f = new ViewDataUploadFilesResult
                {
                    url = "Download?id=" + i.FileId,
                    type = "GET",
                    thumbnail_url = "https://a248.e.akamai.net/assets.github.com/images/modules/about_page/octocat.png?1315867479",
                    size = i.Size,
                    name = i.Filename,
                    delete_type = "DELETE",
                    delete_url = "Delete?id=" + i.FileId
                };
                status.Add(f);

            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Upload(int articleId)
        {
            ViewBag.Id = articleId;
            return View();
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpDelete]
        public void Delete(int id)
        {
            Models.File f = context.Files.Where(x=>x.FileId==id).FirstOrDefault();
            if(f!=null){
                c.Delete(ModelHelpers.createFilePath(f));
                context.Files.Remove(f);
                context.SaveChanges();
            }
        }



        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpGet]
        public void Download(int id)
        {

            var contextR = HttpContext;
            Models.File f = context.Files.Where(x => x.FileId == id).FirstOrDefault();
            if(f!=null){
            var filename = f.Filename;
            var filePath = ModelHelpers.createFilePath(f);
            var file = c.GetFile("/" + filePath);

                contextR.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
                contextR.Response.ContentType = "application/octet-stream";
                contextR.Response.ClearContent();
                contextR.Response.BinaryWrite(file);
            }
            else
                contextR.Response.StatusCode = 404;
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpPost]
        public ActionResult Upload1(int articleId)
        {
            var r = new List<ViewDataUploadFilesResult>();

            foreach (string file in Request.Files)
            {
                var statuses = new List<ViewDataUploadFilesResult>();
                var headers = Request.Headers;

                UploadWholeFile(articleId, Request, statuses);

                JsonResult result = Json(statuses);
                result.ContentType = "text/plain";

                return result;
            }

            return Json(r);
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }


        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadWholeFile(int articleId,HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        {
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];

                byte[] buffer = new byte[file.ContentLength];
                file.InputStream.Read(buffer, 0, file.ContentLength);
                var f = new Models.File { ArticleId = articleId, Filename = file.FileName, Size = file.ContentLength };
                context.Files.Add(f);
                context.SaveChanges();

                var filePath = ModelHelpers.createFilePath(f);

                c.UploadFile("/", filePath, buffer);

                statuses.Add(new ViewDataUploadFilesResult()
                {
                    name = file.FileName,
                    size = file.ContentLength,
                    type = file.ContentType,
                    url = "Download?id=" + f.FileId,
                    delete_url = "Delete?id=" + f.FileId,
                    delete_type = "GET",
                });
            }
        }
    }

    public class ViewDataUploadFilesResult
    {
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string delete_url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_type { get; set; }
    }
}