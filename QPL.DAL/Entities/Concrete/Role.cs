using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPL.DAL.Entities.Concrete
{
    public class Role : BaseEntity
    {
        public Roles RoleName { get; set; }

    }
    public enum Roles
    {
        QPL_Arama,
        QPL_Veri_Girisi,
        QPL_Uretici_Girisi,
        QPL_Detayli_Arama
    };
}
