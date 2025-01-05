import React from 'react'
import {useCart} from '../context/CartContext'
import { Link } from 'react-router-dom';

export default function ProductCart({product}) {

  const {addToCart} = useCart();

  return(
    <div className='col-md-4 mb-4'>
      <div className='card h-100 shadow-sm'>
        <Link to={`/product/${product.id}`} style={{textDecoration: "none"}}>
          <img
          src={product.image}
          alt={product.title}
          className='card-img-top'
          style={{objectFit: "contain", height: "200px"}}
          ></img>
          <div className='card-body'>
            <p className='card-title'>{product.title}</p>
            <p className='card-text'>{product.price}₺</p>
          </div>
        </Link>
        <button
        className='btn btn-warning w-100 h-100'
        onClick={()=>addToCart(product)}
        >Add to Cart</button>
      </div>
    </div>
  );
}
