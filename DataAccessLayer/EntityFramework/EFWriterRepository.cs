using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EFWriterRepository : GenericRepository<Writer>, IWriterDal
    {
        public EFWriterRepository(Context context) : base(context)
        {
        }

        public Writer GetWriterByMail(string mail)
        {
            return _context.Writers.FirstOrDefault(x => x.WriterMail == mail);
        }
    }
}
