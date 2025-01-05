import api from '../utils/axios';

// Auth Service
export const authService = {
    login: (credentials) => api.post('/Users/login', credentials).then(res => res.data),
    register: (userData) => api.post('/Users/register', userData).then(res => res.data),
    getCurrentUser: () => api.get('/Users').then(res => res.data)
};

// Course Service
export const courseService = {
    getAllCourses: () => api.get('/Courses').then(res => res.data),
    searchCourses: (query) => api.get(`/Courses/GetCoursesByName/${query}`).then(res => res.data),
    getCourseById: (id) => api.get(`/Courses/${id}`),
    purchaseCourse: (courseId) => api.post('/courses/purchase', { courseId })
};

// User Service
export const userService = {
    updateProfile: (data) => api.put('/Users', data).then(res => res.data),
    changePassword: (data) => api.put('/users/change-password', data),
    getPurchaseHistory: () => api.get('/users/purchases')
};