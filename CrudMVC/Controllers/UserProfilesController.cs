using CrudMVC.Models;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrudMVC.Models;
using System.Data.Entity;

public class UserProfilesController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Index()
    {
        return View(db.UserProfiles.ToList());
    }

    public ActionResult List()
    {
        var userProfiles = db.UserProfiles.ToList();
        return PartialView("_UserProfileList", userProfiles);
    }

    [HttpPost]
    public ActionResult Delete(int id)
    {
        UserProfile userProfile = db.UserProfiles.Find(id);
        db.UserProfiles.Remove(userProfile);
        db.SaveChanges();
        return new HttpStatusCodeResult(HttpStatusCode.OK);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(UserProfile userProfile, HttpPostedFileBase profilePhoto)
    {
        if (ModelState.IsValid)
        {
            if (profilePhoto != null && profilePhoto.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(profilePhoto.FileName));
                profilePhoto.SaveAs(path);
                userProfile.ProfilePhotoPath = Path.GetFileName(profilePhoto.FileName);
            }

            db.UserProfiles.Add(userProfile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(userProfile);
    }
    // GET: UserProfiles/Edit/5
    public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        UserProfile userProfile = db.UserProfiles.Find(id);
        if (userProfile == null)
        {
            return HttpNotFound();
        }
        return View(userProfile);
    }

    // POST: UserProfiles/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Name,Email,Country,State,ProfilePhotoPath")] UserProfile userProfile)
    {
        if (ModelState.IsValid)
        {
            db.Entry(userProfile).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(userProfile);
    }


    // Implement Edit actions similarly with AJAX
}
