using InveonBootcamp.Business.Abstract;
using InveonBootcamp.DataAccess.Abstract;
using InveonBootcamp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Concrete
{
    public class CourseManager(ICourseDal courseDal) : ICourseService
    {
        public async Task DeleteAsync(Course entity)
        {
            await courseDal.DeleteAsync(entity);
        }

        public async Task<List<Course>> GetAllAsync(Expression<Func<Course, bool>> filter = null)
        {
            return await courseDal.GetAllAsync(filter);
        }

        public async Task<Course> GetEntityAsync(Expression<Func<Course, bool>> filter)
        {
            return await courseDal.GetEntityAsync(filter);
        }

        public async Task<Course> GetEntityByIdAsync(int id)
        {
            return await courseDal.GetEntityByIdAsync(id);
        }

        public async Task InsertAsync(Course entity)
        {
            await courseDal.InsertAsync(entity);
        }

        public async Task UpdateAsync(Course entity)
        {
            await courseDal.UpdateAsync(entity);
        }
    }
}
