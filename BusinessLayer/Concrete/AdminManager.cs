using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class AdminManager :IAdminService
    {
        private IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public void TAdd(Admin t)
        {
            _adminDal.Insert(t);
        }

        public void TDelete(Admin t)
        {
           _adminDal.Delete(t);
        }

        public void TUpdate(Admin t)
        {
            _adminDal.Update(t);
        }

        public List<Admin> GetList()
        {
            return _adminDal.GetListAll();
        }

        public Admin GetById(int id)
        {
            return _adminDal.GetById(id);
        }

        public Admin Get(Expression<Func<Admin, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
