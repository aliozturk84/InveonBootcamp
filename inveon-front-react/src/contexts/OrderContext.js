import { createContext, useContext, useState, useEffect } from 'react';
import { orderService } from '../services/orderService';
import { toast } from 'react-toastify';

const OrderContext = createContext(null);

export const OrderProvider = ({ children }) => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(false);

  const purchaseCourse = async (courseId) => {
    setLoading(true);
    try {
      const response = await orderService.purchaseCourse(courseId);
      toast.success('Kurs başarıyla satın alındı');
      return response.data;
    } catch (error) {
      toast.error(error.response?.data?.message || 'Satın alma işlemi başarısız');
      throw error;
    } finally {
      setLoading(false);
    }
  };

  const getUserOrders = async () => {
    setLoading(true);
    try {
      const token = localStorage.getItem("token");
      if (token) {
        const jwt = token;
        const [header, payload, signature] = jwt.split(".");
        const decodedPayload = JSON.parse(atob(payload));
  
  
        const response = await orderService.getUserOrders(decodedPayload.userId);
  
        setOrders(response.data);
  
      }
    } catch (error) {
      console.error("Hata:", error);
      toast.error("Sipariş geçmişi yüklenemedi");
    } finally {
      setLoading(false);
    }
  };
  
  

  const value = {
    orders,
    loading,
    purchaseCourse,
    getUserOrders
  };

  return <OrderContext.Provider value={value}>{children}</OrderContext.Provider>;
};

export const useOrders = () => {
  const context = useContext(OrderContext);
  if (!context) {
    throw new Error('useOrders must be used within an OrderProvider');
  }
  return context;
}; 