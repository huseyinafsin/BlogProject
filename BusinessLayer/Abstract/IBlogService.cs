using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IBlogService : IGenericService<Blog>
    {
       
        List<Blog> GetBlogListWithCategory();
        List<Blog> GetLasBlogs(int discount);
        List<Blog> GetLasBlogsWithCategory(int discount);
        List<Blog> GetBlogListByWriter(int id);
        
    }
}
