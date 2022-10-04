using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EFMessage2Repository : GenericRepository<Message2>, IMessage2Dal
    {
        public EFMessage2Repository(Context context) : base(context)
        {
        }

        public List<Message2> GetListWithMessageByWriter(int id)
        {
           
                return _context.Message2s.Include(x => x.SenderUser).Where(x => x.ReceiverID == id).ToList();
            
        }
    }
}
 