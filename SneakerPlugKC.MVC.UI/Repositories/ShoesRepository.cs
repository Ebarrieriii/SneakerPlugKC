using SneakerPlugKC.DATA.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace SneakerPlugKC.MVC.UI.Repositories
{
    public interface IShoesRepository
    {
        IEnumerable<Shoe> GetShoes(int id);
        Shoe Details(int? id);
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
    }
}