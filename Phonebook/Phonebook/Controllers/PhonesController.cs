using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    public class PhonesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Phones/
        public ActionResult Index(int id2 = 0)
        {
            var phones = db.Phone.Where(c => c.ContactId == id2);
            // var phone = db.Phone.Include(p => p.Contact);
            // return View(phone.ToList());
            ViewBag.id2 = id2;
            return View(phones.ToList());
        }

        // GET: /Phones/Details/5
        public ActionResult Details(int? id, int id2 = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phone.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.id2 = id2;
            return View(phone);
        }

        // GET: /Phones/Create
        public ActionResult Create(int id2 = 0)
        {
            ViewBag.id2 = id2;
            ViewBag.contactName = db.Contacts.Find(id2).Name;

            ViewBag.ContactId = new SelectList(db.Contacts, "id", "Name");
            return View();
        }

        // POST: /Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Number")] Phone phone, int id2 = 0 ) // public ActionResult Create([Bind(Include="Id,Number,ContactId")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                phone.ContactId = id2;
                phone.Contact = db.Contacts.Find(id2);

                db.Phone.Add(phone);
                db.SaveChanges();
                return RedirectToAction("Index", new { id2 = id2 });
            }

            ViewBag.ContactId = new SelectList(db.Contacts, "id", "Name", phone.ContactId);
            ViewBag.contactName = db.Contacts.Find(id2).Name;
            return View(phone);
        }

        // GET: /Phones/Edit/5
        public ActionResult Edit(int? id, int id2 = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phone.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "id", "Name", phone.ContactId);
            ViewBag.contactName = db.Contacts.Find(id2).Name;
            ViewBag.id2 = id2;
            return View(phone);
        }

        // POST: /Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number")] Phone phone, int id2 = 0) // ,ContactId
        {
            if (ModelState.IsValid)
            {
                phone.ContactId = id2;
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id2 = id2});
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "id", "Name", phone.ContactId);
            return View(phone);
        }

        // GET: /Phones/Delete/5
        public ActionResult Delete(int? id, int id2 = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phone.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.id2 = id2;
            return View(phone);
        }

        // POST: /Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int id2 = 0)
        {
            Phone phone = db.Phone.Find(id);
            db.Phone.Remove(phone);
            db.SaveChanges();
            return RedirectToAction("Index", new { id2  = id2 });
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
