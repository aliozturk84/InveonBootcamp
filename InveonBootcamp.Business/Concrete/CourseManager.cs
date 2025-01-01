using AutoMapper;
using InveonBootcamp.Business.Abstract;
using InveonBootcamp.Business.DTOs.Requests.Course;
using InveonBootcamp.DataAccess.Abstract;
using InveonBootcamp.DataAccess.Repositories.EntityFramework;
using InveonBootcamp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Concrete
{
    public class CourseManager(ICourseDal courseDal, IMapper mapper, IUnitOfWork unitOfWork) : ICourseService
    {
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var course = await unitOfWork.CourseDal.GetEntityByIdAsync(id);
            if (course == null)
            {
                return ServiceResult.Fail("Silinecek kurs bulunamadı.", HttpStatusCode.NotFound);
            }

            await unitOfWork.CourseDal.DeleteAsync(course); // Kursu siliyoruz

            await unitOfWork.CompleteAsync(); // Değişiklikleri kaydediyoruz
            return ServiceResult.Success("Kurs başarıyla silindi.", HttpStatusCode.OK); // Mesaj ekliyoruz
        }


        public async Task<ServiceResult<List<Course>>> GetAllAsync(Expression<Func<Course, bool>> filter = null)
        {
            var courses = await courseDal.GetAllAsync(filter);
            if (courses == null || !courses.Any())
            {
                return ServiceResult<List<Course>>.Fail(
                    new List<string> { "Kurslar bulunamadı." },  // ErrorMessage'yi List<string> olarak geçiyoruz
                    "Kurslar veritabanında bulunamadı.",  // String mesajı olarak açıklama ekliyoruz
                    HttpStatusCode.NotFound
                );
            }

            return ServiceResult<List<Course>>.Success(courses, "Kurslar başarıyla getirildi.", HttpStatusCode.OK);
        }


        public async Task<ServiceResult<List<Course>>> GetCoursesByCategoryAsync(string category)
        {
            var filter = (Expression<Func<Course, bool>>)(c => c.Category.Contains(category)); // Category'ye göre filtre oluşturuyoruz
            return await GetAllAsync(filter); // GetAllAsync metodunu çağırıyoruz
        }
        public async Task<ServiceResult<List<Course>>> GetCoursesByNameAsync(string courseName)
        {
            var filter = (Expression<Func<Course, bool>>)(c => c.Name.Contains(courseName)); // Adına göre arama
            return await GetAllAsync(filter); // GetAllAsync metodunu çağırıyoruz
        }


        public async Task<ServiceResult<Course>> GetEntityAsync(Expression<Func<Course, bool>> filter)
        {
            var course = await courseDal.GetEntityAsync(filter);
            if (course == null)
            {
                return ServiceResult<Course>.Fail(
                    new List<string> { "Kurs bulunamadı." },  // ErrorMessage'yi List<string> olarak geçiyoruz
                    "Kurs veritabanında bulunamadı.",  // String mesajı olarak açıklama ekliyoruz
                    HttpStatusCode.NotFound
                );
            }

            return ServiceResult<Course>.Success(course, "Kurs başarıyla getirildi.", HttpStatusCode.OK);
        }


        public async Task<ServiceResult<Course>> GetEntityByIdAsync(int id)
        {
            var course = await courseDal.GetEntityByIdAsync(id);
            if (course == null)
            {
                return ServiceResult<Course>.Fail(
                    new List<string> { $"{id} numaralı kurs bulunamadı." },  // ErrorMessage'yi List<string> olarak geçiyoruz
                    $"{id} numaralı kurs veritabanında bulunamadı.",  // String mesajı olarak açıklama ekliyoruz
                    HttpStatusCode.NotFound
                );
            }

            return ServiceResult<Course>.Success(course, $"{id} numaralı kurs başarıyla getirildi.", HttpStatusCode.OK);
        }


        public async Task<ServiceResult> InsertAsync(CreateCourseRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return ServiceResult.Fail(
                    "Kurs adı boş olamaz.",
                    HttpStatusCode.BadRequest
                );
            }

            var newCourse = mapper.Map<Course>(request);
            await unitOfWork.CourseDal.InsertAsync(newCourse); // UnitOfWork ile veri ekleme işlemi

            await unitOfWork.CompleteAsync(); // Değişiklikleri kaydediyoruz

            return ServiceResult.Success("Kurs başarıyla oluşturuldu.", HttpStatusCode.Created);
        }


        public async Task<ServiceResult> UpdateAsync(UpdateCourseRequest request)
        {
            if (request == null)
            {
                return ServiceResult.Fail(
                    "Güncellenecek kurs verisi geçersiz.",
                    HttpStatusCode.BadRequest
                );
            }

            var existingCourse = await unitOfWork.CourseDal.GetEntityByIdAsync(request.Id);

            if (existingCourse == null)
            {
                return ServiceResult.Fail(
                    $"{request.Id} numaralı kurs bulunamadı.",
                    HttpStatusCode.NotFound
                );
            }

            // Gelen verilerle mevcut kursu güncelle
            existingCourse.Name = request.Name;
            existingCourse.Description = request.Description;
            existingCourse.Price = request.Price;
            existingCourse.Category = request.Category;

            // Güncellenmiş kursu veritabanına kaydet
            await unitOfWork.CourseDal.UpdateAsync(existingCourse); // Kursu güncelliyoruz

            await unitOfWork.CompleteAsync(); // Değişiklikleri kaydediyoruz

            return ServiceResult.Success("Kurs başarıyla güncellendi.", HttpStatusCode.OK); // Başarı mesajı burada
        }
    }
}
