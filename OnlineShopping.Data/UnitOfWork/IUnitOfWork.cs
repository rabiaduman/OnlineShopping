using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(); //Kaç kayda etki ettiğini geriye döner, o yüzden int.

        Task BeginTransaction(); //Birden fazla kaydı içeren süreci başlatan metod.

        Task CommitTransaction(); //İşlem tamamlandıysa işlemleri kaydet metodu.

        Task RollBackTransaction(); //İşlem sırasında problem olduysa işlemleri geriye al metodu.
    }
}
