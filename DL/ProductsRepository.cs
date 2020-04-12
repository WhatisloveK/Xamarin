using SqliteApp.Services;
using SqliteApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace DL.Standard
{
    public class ProductsRepository : IDataStore<Product>
    {
        private readonly DatabaseContext _databaseContext;

        public ProductsRepository()
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "productsDB.db");

            _databaseContext = new DatabaseContext(dbPath);
        }

        public ProductsRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }

        public async Task<bool> AddItemAsync(Product product)
        {
            try
            {
                product.Id = Guid.NewGuid().ToString();

                var tracking = await _databaseContext.Products.AddAsync(product);

                var isAdded = tracking.State == EntityState.Added;

                return isAdded;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(Product product)
        {
            try
            {
                var tracking = _databaseContext.Update(product);

                await _databaseContext.SaveChangesAsync();

                var isModified = tracking.State == EntityState.Modified;

                return isModified;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            try
            {
                var oldProduct = _databaseContext.Products.FindAsync(id);

                var tracking = _databaseContext.Remove(oldProduct);

                await _databaseContext.SaveChangesAsync();

                var isDeleted = tracking.State == EntityState.Deleted;

                return isDeleted;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<Product> GetItemAsync(string id)
        {
            try
            {
                var product = await _databaseContext.Products.FindAsync(id);

                return product;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                var products = await _databaseContext.Products.ToListAsync();

                return products;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
