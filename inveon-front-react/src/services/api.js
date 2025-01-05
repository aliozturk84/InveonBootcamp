
import axios from './helper'

const API_URL = "https://fakestoreapi.com";

export const fetchProduct = () => axios.get(`${API_URL}/products`);
export const fetchProductById = (id) => axios.get(`${API_URL}/products/${id}`);

export const fetchHomeItems = () => axios.get(`${API_URL}/products`).then(res => res.data);