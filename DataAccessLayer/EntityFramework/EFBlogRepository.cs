using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFBlogRepository : GenericRepository<Blog>, IBlogDal
    {
        public EFBlogRepository(Context context) : base(context)
        {
        }

        public List<Blog> GetListWithCategories()
        {
          
                return _context.Blogs.Include(x => x.Category).ToList();
           
        }

        public List<Blog> GetListWithCategoryByWriter(int id)
        {
                return _context.Blogs.Include(x => x.Category).Where(x=>x.WriterID==id).ToList();
            
        }
    }
}
