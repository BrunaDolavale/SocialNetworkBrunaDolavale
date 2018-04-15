using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Application;
using Data.Context;
using DomainModel.Entities;

namespace WebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private SocialNetworkContext db = new SocialNetworkContext();

        // GET: Users
        public ActionResult Index(Guid? id)
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Birth,Gender,Sexuality,Description,CellphoneNumber,Email,SchoolLevel,Office")] User user, HttpPostedFileBase profilePhoto)
        {
            if (ModelState.IsValid)
            {
                user.PhotoProfile = new PhotoProfile();
                user.PhotoProfile.Url = "https://infnetstorage.blob.core.windows.net/bruna/default-user.png";
                if (profilePhoto != null)
                    user.PhotoProfile.Url = ApplicationServices.GetInstance().UploadPhoto(profilePhoto.InputStream, profilePhoto.ContentType);
                ApplicationServices.GetInstance().AddNewUser(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }


        // GET: Users/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Details(Guid id)
        {
            if (id == null && Session["UserEmail"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user;
            if (id != null)
                user = db.Users.Find(id);
            else
                user = db.Users.ToList()
                   .Where(p => p.Email == Session["UserEmail"].ToString())
                   .First();

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Birth,Gender,Sexuality,Description,CellphoneNumber,Email,SchoolLevel,Office")] User user, HttpPostedFileBase profilePhoto)
        {
            if (ModelState.IsValid)
            {
                if (profilePhoto != null)
                    user.PhotoProfile.Url = ApplicationServices.GetInstance().UploadPhoto(profilePhoto.InputStream, profilePhoto.ContentType);
                ApplicationServices.GetInstance().UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            //#### Delete Messages ####
            IEnumerable<Message> messagesToDelete = db.Messages.ToList().Where(m => m.Sender.Id == id 
                || m.Recipient?.Id == id);
            foreach (var msg in messagesToDelete)
                db.Messages.Remove(msg);
            //##########################

            //##### Delete User #####
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            //#######################

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
