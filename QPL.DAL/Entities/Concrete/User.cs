using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPL.DAL.Entities.Concrete
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string ActiveQPLSearch { get; set; }
        public Role Role { get; set; }
    }
}
