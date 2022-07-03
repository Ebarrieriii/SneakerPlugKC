using SneakerPlugKC.DATA.EF;
using SneakerPlugKC.MVC.UI.Repositories;
using System.Collections.Generic;

namespace SneakerPlugKC.MVC.UI.Services
{
    public interface IShoesService
    {
        IEnumerable<Shoe> GetShoes(int id);
        Shoe Details(int? id);
    }
    public class ShoesService : IShoesService
    {
        private readonly IShoesRepository _shoesRepository;

        public ShoesService(IShoesRepository shoesRepository)
        {
            this._shoesRepository = shoesRepository;
        }

        public IEnumerable<Shoe> GetShoes(int id) => _shoesRepository.GetShoes(id);

        public Shoe Details(int? id) => _shoesRepository.Details(id);
    }
}