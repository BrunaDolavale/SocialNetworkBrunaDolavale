using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Data.Context;
using DomainModel.Entities;

namespace WebApp.Controllers
{
    public class MessagesController : Controller
    {
        private SocialNetworkContext db = new SocialNetworkContext();

        // GET: Messages
        public ActionResult Index(Guid userId)
        {
            Guid myUserId = db.Users.ToList().Where(u => u.Email == Session["UserEmail"].ToString()).SingleOrDefault().Id;
            var filteredMessages = new List<Message>();
            var messages = db.Messages.ToList();
            foreach(var msg in messages)
                if ((msg.Recipient.Id == userId && msg.Sender.Id == myUserId)
                    || (msg.Recipient.Id == myUserId && msg.Sender.Id == userId))
                    filteredMessages.Add(msg);

            filteredMessages.Sort((m1, m2) => m2.DateTimeSent.CompareTo(m1.DateTimeSent));

            return View(filteredMessages);
        }

        // GET: Messages/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create(Guid? recipientId)
        {
            ViewBag.RecipientId = recipientId;
            return View();
        }

        // POST: Messages/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content")] Message message, Guid recipientId)
        {
            if (ModelState.IsValid)
            {
                message.Id = Guid.NewGuid();
                message.Sender = db.Users.ToList()
                    .Where(user => user.Email == Session["userEmail"].ToString())
                    .First();
                message.Recipient = db.Users.Find(recipientId);
                message.DateTimeSent = DateTime.Now;
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index", new { userId = recipientId });
            }

            return View("Index", new { userId = recipientId });
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateTimeSent,Content")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { userId = message.Recipient.Id });
            }
            return View("Index", new { userId = message.Recipient.Id });
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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
