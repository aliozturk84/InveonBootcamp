import api from '../utils/axios';

export const authService = {
  login: async (credentials) => {
    const response = await api.post('/Users/login', credentials).then(res => res.data);
    return response;
  },

  register: async (userData) => {
    const response = await api.post('/Users/register', userData).then(res => res.data);
    return response;
  },

  getCurrentUser: async (id) => {
    const response = await api.get(`/Users/${id}`).then(res => res.data);
    return response;
  },

  updateProfile: async (userData) => {
    const response = await api.put('/Users/UpdateProfile', userData);
    return response;
  },

  forgotPassword: async (passwordData) => {
    const response = await api.post('/Users/forgot-password', passwordData);
    return response;
  }
}; 