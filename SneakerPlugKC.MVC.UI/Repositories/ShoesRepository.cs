using SneakerPlugKC.DATA.EF;
using SneakerPlugKC.UI.MVC.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SneakerPlugKC.MVC.UI.Repositories
{
    public interface IShoesRepository
    {
        IEnumerable<Shoe> GetShoes(int id);
        Shoe Details(int? id);
        void Create([Bind(Include = "ShoeID,ShoeName,SizeId,Price,Description,Quantity,Condition,IsInStock,ShoePhoto")] Shoe shoe, HttpPostedFileBase logo);

    }
    public class ShoesRepository : IShoesRepository
    {
        private SneakerPlugKCEntities db = new SneakerPlugKCEntities();

        //GET: Shoes by Size
        public IEnumerable<Shoe> GetShoes(int id) 
        {
            return db.Shoes.Where(x => x.SizeId.Equals(id));
        }

        //GET: Details
        public Shoe Details(int? id)
        {
            Shoe shoe = db.Shoes.Find(id);
            return shoe;
        }

        public void Create([Bind(Include = "ShoeID,ShoeName,SizeId,Price,Description,Quantity,Condition,IsInStock,ShoePhoto")] Shoe shoe, HttpPostedFileBase logo)
        {
                db.Shoes.Add(shoe);
                db.SaveChanges();
        }
    }
}