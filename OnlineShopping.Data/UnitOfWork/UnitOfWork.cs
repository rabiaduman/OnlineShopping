using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineShopping.Data.Context;

namespace OnlineShopping.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineShoppingDbContext _db;
        private IDbContextTransaction _transaction; //Transaction işlemlerini gerçekleştirecek olan nesne

        public UnitOfWork(OnlineShoppingDbContext db)
        {
            _db = db;
        }

        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _db.Dispose(); //Garbage Collector'a temizleme iznini verdiğimiz yer, Silinebilir yapıyoruz
        }

        public async Task RollBackTransaction()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
