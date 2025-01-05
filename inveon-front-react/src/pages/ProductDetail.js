import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { useCart } from '../context/CartContext';
import { fetchProductById } from '../services/api';

export default function ProductDetail() {
  const {id} = useParams();

  const [product, setProduct] = useState(null);

  const{addToCart} = useCart();

  useEffect(() =>{
    fetchProductById(id).then((res)=> setProduct(res.data));
  },[id]);

  if(!product) return <div className='container mt-4'>Loading...</div>

  return(
    <div className='container mt-4'>
      <div className='row'>
        <div className='col-md-6'>
          <img src={product.image} alt={product.title} className='img-fluid'></img>
        </div>
        <div className='col-md-6'>
          <h2>{product.title}</h2>
          <p>{product.description}</p>
          <h4>{product.price}₺</h4>
          <button className='btn btn-success' onClick={()=>addToCart(product)}>Add To Cart</button>
        </div>
      </div>
    </div>
  );
}
