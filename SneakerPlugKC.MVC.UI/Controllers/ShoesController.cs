using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SneakerPlugKC.DATA.EF;
using SneakerPlugKC.MVC.UI.Services;
using SneakerPlugKC.UI.MVC.Utilities;

namespace SneakerPlugKC.MVC.UI.Controllers
{
    //TODO: Move all DB interactions to the repository layer.
    public class ShoesController : Controller
    {
        private SneakerPlugKCEntities db = new SneakerPlugKCEntities();


        private readonly IShoesService _shoesService;

        // GET: Shoes
        public ActionResult Index()
        {
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "Sizes");
            return View();
        }

        public ShoesController(IShoesService shoesService)
        {
            this._shoesService = shoesService;
        }

  
        public ActionResult ShopSize(int id)
        {
            var shoesList = _shoesService.GetShoes(id);
            return View(shoesList);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var shoe = _shoesService.Details(id);

            if (shoe == null)
            {
                return HttpNotFound();
            }

            return View(shoe);
        }
        public ActionResult Create()
        {
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "Sizes");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShoeID,ShoeName,SizeId,Price,Description,Quantity,Condition,IsInStock,ShoePhoto")] Shoe shoe, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {
                FileUpload(shoe, logo);
                _shoesService.Create(shoe, logo);
                return RedirectToAction("Index");
            }

            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "Size", shoe.SizeId);
            return View(shoe);
        }


        // GET: Shoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shoe shoe = db.Shoes.Find(id);
            if (shoe == null)
            {
                return HttpNotFound();
            }
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "Sizes");
            return View(shoe);
        }

        // POST: Shoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShoeID,ShoeName,SizeId,Price,Description,Quantity,Condition,IsInStock,ShoePhoto")] Shoe shoe, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {

                #region File Upload
                string file = shoe.ShoePhoto;

                if (logo != null)
                {
                    file = logo.FileName;

                    string ext = file.Substring(file.LastIndexOf('.'));

                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower()) && logo.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;
                        #region Resize Image
                        string savePath = Server.MapPath("~/Content/img/shoes/");

                        Image convertedImage = Image.FromStream(logo.InputStream);

                        int maxImageSize = 600;

                        int maxThumbSize = 250;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion

                        if (shoe.ShoePhoto != null && shoe.ShoePhoto != "noimage.png")
                        {
                            string path = Server.MapPath("~/Content/img/shoes/");
                            ImageUtility.Delete(path, shoe.ShoePhoto);
                        }
                        shoe.ShoePhoto = file;
                    }
                }
                #endregion

                db.Entry(shoe).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Product Updated Successfully";
                return RedirectToAction("Index");
            }
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeId", shoe.SizeId);
            return View(shoe);
        }

        // GET: Shoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shoe shoe = db.Shoes.Find(id);
            if (shoe == null)
            {
                return HttpNotFound();
            }
            return View(shoe);
        }

        // POST: Shoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shoe shoe = db.Shoes.Find(id);


            string path = Server.MapPath("~/Content/img/shoes/");
            ImageUtility.Delete(path, shoe.ShoePhoto);

            db.Shoes.Remove(shoe);
            db.SaveChanges();
            TempData["success"] = "Product Deleted Successfully";
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

        private void FileUpload(Shoe shoe, HttpPostedFileBase logo)
        {
            //use default image if there is no image uploaded.
            string file = "noimage.png";

            if (logo != null)
            {
                //reassign the variable that held the default image name to have the name of the uploaded file.
                file = logo.FileName;
                //use Substring() to get just the ext part of the file name.
                string ext = file.Substring(file.LastIndexOf("."));
                //create a list of approved file extensions
                string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                //Check that the uploaded file extension is in the list of approved extensions & make sure the file size is under 4mb.
                if (goodExts.Contains(ext.ToLower()) && logo.ContentLength <= 4194304)
                {
                    //create a new file name (using a guid) and then add the file ext
                    file = Guid.NewGuid() + ext;

            #region Resize Image
                    //define the file path where images will be saved
                    string savePath = Server.MapPath("~/Content/img/shoes/");

                    //convert the HttpPostFileBase to an object of type Image so that the Image Utility can manipulate the dimensions
                    Image convertedImage = Image.FromStream(logo.InputStream);

                    //define the max size for the full size pic in px
                    int maxImageSize = 600;

                    //define the max size for the thumbnail
                    int maxThumbSize = 250;

                    //Pass data to the ResizeImage() on the image utility
                    ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                }
            }
            #endregion
            shoe.ShoePhoto = file;
        }
    }
}
