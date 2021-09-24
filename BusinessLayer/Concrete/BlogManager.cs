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

        public void BlogAdd(Blog blog)
        {
            _blogDal.Insert(blog);
        }

        public void BlogDelete(Blog blog)
        {
            _blogDal.Delete(blog);
        }

        public void BlogUpdate(Blog blog)
        {
            _blogDal.Update(blog);
        }

        public Blog GetBlog(int id)
        {
            return _blogDal.GetById(id);
        }

        public List<Blog> GetBlogListWithCatgory()
        {
            return _blogDal.GetListWithCtegories();
        }

        public List<Blog> GetBlogs()
        {
            return _blogDal.GetListAll();
        }
    }
}
