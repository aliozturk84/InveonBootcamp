using InveonBootcamp.Business.DTOs.Requests.Course;
using InveonBootcamp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Abstract
{
    public interface ICourseService : IGenericService<Course, CreateCourseRequest, UpdateCourseRequest>
    {
        Task<ServiceResult<List<Course>>> GetCoursesByCategoryAsync(string category);
        Task<ServiceResult<List<Course>>> GetCoursesByNameAsync(string courseName);
    }
}
