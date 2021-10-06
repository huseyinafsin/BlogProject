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
    /// <inheritdoc />
    public class NewsletterManager : INewsletterService
    {
      
        private INewsletterDal _newsletterDal;

        public NewsletterManager(INewsletterDal newsletterDal)
        {
            _newsletterDal = newsletterDal;
        }


        public void NewsletterAdd(NewsLetter newsLetter)
        {
           _newsletterDal.Insert(newsLetter);
        }
    }
}