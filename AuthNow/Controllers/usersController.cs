using AuthNow.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthNow.Controllers
{
    public class usersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected UserManager<ApplicationUser> UserManager { get; set; }

        public usersController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        // GET: users/id
        [Route("users/{id}")]
        public ActionResult Index(string id)
        {
            UserViewModels UserView = new UserViewModels();

            if (!String.IsNullOrWhiteSpace(id)) {
                ApplicationUser user = db.Users.Find(id);

                ViewBag.Name = user.UserName;

                IEnumerable<Campaign> campaigns = db.Campaigns.AsEnumerable().Where(x => x.User == user);

                UserView.User = user;
                UserView.Campaigns = campaigns;
                //campaigns.ToList();
                return View(UserView);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
            //if (User.Identity.IsAuthenticated)
            //{
            //    //var user = User.Identity;
            //    //ViewBag.Name = user.GetUserName();
            //}

           
        }

        public FileContentResult UserPhotos()
        {
            string DefaultImgPath = @"~/Content/img/noImg.png";
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(DefaultImgPath);

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");

                }
                // to get the user details to load user Image
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault();
                if (userImage.UserPhoto != null)
                {
                    return new FileContentResult(userImage.UserPhoto, "image/jpeg");
                }
                else
                {
                    string fileName = HttpContext.Server.MapPath(DefaultImgPath);

                    byte[] imageData = null;
                    try
                    {
                        FileInfo fileInfo = new FileInfo(fileName);
                        long imageFileLength = fileInfo.Length;
                        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        imageData = br.ReadBytes((int)imageFileLength);
                        return File(imageData, "image/png");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    return null;
                }

            }
            else
            {
                string fileName = HttpContext.Server.MapPath(DefaultImgPath);

                byte[] imageData = null;
                try
                {
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);
                    return File(imageData, "image/png");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return null;

            }
        }
    }
}