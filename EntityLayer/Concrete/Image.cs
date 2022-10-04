using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Image 
    {
        [Key]
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }

        public Blog Blog { get; set; }
    }
}
