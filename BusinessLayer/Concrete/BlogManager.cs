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
        IBlogDal blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            this.blogDal = blogDal;
        }

        public void BlogAdd(Blog blog)
        {
            blogDal.Insert(blog);
        }

        public void BlogDelete(Blog blog)
        {
            blogDal.Delete(blog);
        }

        public void BlogUpdate(Blog blog)
        {
            blogDal.Update(blog);
        }

        public Blog GetBlog(int id)
        {
            return blogDal.GetById(id);
        }

        public List<Blog> GetBlogs()
        {
            return blogDal.GetListAll();
        }
    }
}
