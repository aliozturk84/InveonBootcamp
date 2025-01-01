using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InveonBootcamp.Business.DTOs.Requests.Course;
using InveonBootcamp.Business.DTOs.Requests.Order;
using InveonBootcamp.Business.DTOs.Requests.Payment;
using InveonBootcamp.Business.DTOs.Requests.User;
using InveonBootcamp.Business.DTOs.Responses.User;
using InveonBootcamp.Entities.Concrete;

namespace InveonBootcamp.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CreateCourseRequest>().ReverseMap();
            CreateMap<Order, CreateOrderRequest>().ReverseMap();
            CreateMap<Payment, CreatePaymentRequest>().ReverseMap();
            CreateMap<User, CreateUserRequest>().ReverseMap();
            CreateMap<User, GetUserByIdResponse>().ReverseMap();
        }
    }
}
