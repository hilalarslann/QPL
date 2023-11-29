using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPL.BL.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccessToken(int minute); //Üretilen JWT tokenın diğer adıdır.
    }
}
