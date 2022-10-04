using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EFMessageRepository : GenericRepository<Message>, IMessageDal
    {
        public EFMessageRepository(Context context) : base(context)
        {
        }
    }
}
