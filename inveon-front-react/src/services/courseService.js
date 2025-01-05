import api from '../utils/axios';

export const courseService = {
  getAllCourses: async () => {
    const response = await api.get('/courses').then(res => res.data);
    return response;
  },

  getCourseById: async (id) => {
    const response = await api.get(`/courses/${id}`).then(res => res.data);
    return response;
  },

  searchCourses: async (query) => {
    const response = await api.get(`/Courses/GetCoursesByName/${query}`).then(res => res.data)
    return response;
  },

  getCoursesByCategory: async (categoryId) => {
    const response = await api.get(`/courses/category/${categoryId}`);
    return response;
  },

  getPopularCourses: async () => {
    const response = await api.get('/courses/popular');
    return response;
  },

  getRecommendedCourses: async () => {
    const response = await api.get('/courses/recommended');
    return response;
  },

  getUsersCourses: async () => {
    const response = await api.get('/Courses/GetUsersCourses').then(res=>res.data);
    return response;
  },
  
  createCourses: async (course) => {
    const response = await api.post('/Courses',course).then(res=>res.data);
    return response;
  }
}; 