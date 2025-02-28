using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            //Doing all Database related task in this repo,
            //so that controller doesn't has the definition of the 
            //database implementation
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }
        public async Task<Category> GetById(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id ==  category.Id);
            if (existingCategory != null) 
            {
                dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                dbContext.SaveChanges();
                return category;
                /*
                 * ---- Manually assigning the values to the fields ----
                 
                 existingCategory.Name = category.Name;
                 existingCategory.UrlHandle = category.UrlHandle;

                dbContext.SaveChangesAsync();
                return category;
                 */
            }
            return null;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCategory == null)
            {
                return null;
            }
            dbContext.Categories.Remove(existingCategory);
            dbContext.SaveChanges();
            return existingCategory;
        }

    }
}
