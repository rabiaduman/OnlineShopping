using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Business.DataProtection
{
    public interface IDataProtection
    {
        string Protect(string text); //Şifrelenmiş metni dönecek metot.
        string UnProtect(string protectedText); //Gönderilen şifleli metni şifresiz hale getirecek.
    }
}
