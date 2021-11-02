using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            return _blogDal.GetById(id);
        }

      

        public List<Blog> GetLasBlogs(int discount)
        {
            return _blogDal.GetListAll().Take(discount).ToList();
        }    
        
        public List<Blog> GetLasBlogsWithCategory(int discount)
        {
            return _blogDal.GetListWithCategories().Take(discount).ToList();
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
            _blogDal.Delete(t);
        }

        public void TUpdate(Blog t)
        {
            _blogDal.Update(t);
        }

 
    }
}
