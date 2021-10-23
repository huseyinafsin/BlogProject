using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class BlogManager : IBlogService
    {
        IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }


        public List<Blog> GetBlogListByWriter(int id)
        {
            return _blogDal.GetListWithCategoryByWriter( id);
        }

        public List<Blog> GetBlogListWithCategory()
        {
            return _blogDal.GetListWithCategories();
        }

        public Blog GetById(int id)
        {
            throw new NotImplementedException();
        }

      

        public List<Blog> GetLas3Blog()
        {
            return _blogDal.GetListAll().Take(3).ToList();
        }

        public List<Blog> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(Blog t)
        {
            _blogDal.Insert(t);
        }

        public void TDelete(Blog t)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Blog t)
        {
            throw new NotImplementedException();
        }
    }
}
