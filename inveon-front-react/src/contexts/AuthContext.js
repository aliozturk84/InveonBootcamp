import { createContext, useContext, useState, useEffect } from 'react';
import { authService } from '../services/authService';
import { toast } from 'react-toastify';

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    checkAuthStatus();
  }, [isAuthenticated]);

  const checkAuthStatus = async () => {
    const token = localStorage.getItem('token');
    if (token) {
      const jwt = token;

      const [header, payload, signature] = jwt.split('.');

      const decodedHeader = JSON.parse(atob(header));
      const decodedPayload = JSON.parse(atob(payload));

      try {
        const response = await authService.getCurrentUser(decodedPayload.userId);
        setUser(response.data);
        setIsAuthenticated(true);
      } catch (error) {
        localStorage.removeItem('token');
        setIsAuthenticated(false);
      }
    }
    setLoading(false);
  };

  const login = async (credentials) => {
    try {
      const response = await authService.login(credentials);
      localStorage.setItem('token', response.data.accessToken);
      // setUser(response.data.user);
      setIsAuthenticated(true);
      toast.success('Başarıyla giriş yapıldı');
      return response.data;
    } catch (error) {
      toast.error(error.response?.data?.message || 'Giriş başarısız');
      throw error;
    }
  };

  const register = async (userData) => {
    try {
      const response = await authService.register(userData);
      toast.success('Kayıt başarılı! Giriş yapabilirsiniz.');
      return response.data;
    } catch (error) {
      toast.error(error.response?.data?.message || 'Kayıt başarısız');
      throw error;
    }
  };

  const forgotPassword = async (userData) => {
    try {
      const response = await authService.forgotPassword(userData);
      toast.success('Şifre değişimi başarılı! Giriş yapabilirsiniz.');
      return response.data;
    } catch (error) {
      toast.error(error.response?.data?.message || 'Şifre değişimi başarısız');
      throw error;
    }
  };

  const logout = () => {
    localStorage.removeItem('token');
    setUser(null);
    setIsAuthenticated(false);
    toast.info('Çıkış yapıldı');
  };

  const updateProfile = async (userData) => {
    try {
      const response = await authService.updateProfile(userData);
      // setUser(response.data);
      toast.success('Profil güncellendi');
      return response.data;
    } catch (error) {
      toast.error(error.response?.data?.message || 'Güncelleme başarısız');
      throw error;
    }
  };

  const value = {
    user,
    loading,
    isAuthenticated,
    login,
    register,
    logout,
    updateProfile,
    forgotPassword
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
}; 