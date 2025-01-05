import api from '../utils/axios';

export const orderService = {
  purchaseCourse: async (courseId) => {
    const response = await api.post('/Orders', { courseId });
    const res = (await api.post('/Payments',{orderId:response.data.processId} ))
    const finalres= {
      orderResponse:response,
      paymentResponse:res
    }
    return finalres;
  },

  getUserOrders: async (id) => {
    const response = await api.get(`/Orders/GetOrdersByUserId/${id}`).then(res => res.data);
    return response;
  },

  getOrderDetails: async (orderId) => {
    const response = await api.get(`/Orders/${orderId}`).then(res => res.data);
    return response;
  },

  cancelOrder: async (orderId) => {
    const response = await api.post(`/orders/${orderId}/cancel`);
    return response;
  }
}; 