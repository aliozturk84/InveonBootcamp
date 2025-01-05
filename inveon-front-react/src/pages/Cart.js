import React from 'react'
import { useCart } from '../context/CartContext'
import { Link } from 'react-router-dom';

export default function Cart() {

  const {cart,increaseQuantity,decreaseQuantity,removeFromCart,clearCart} = useCart();
  return (
    <div className='container mt-4'>
      <h2>Your Cart</h2>

      {cart.length === 0 ? (
        <p>Your cart is empty</p>
      ) : (
        <>
        <ul className='list-group'>
          {cart.map((item) => (
            <li key={item.id} className='list-group-item d-flex justify-content-between'>
              <div>
                <h5>{item.title}</h5>
                <p>{item.price} x {item.quantity}</p>
                <button onClick={() => increaseQuantity(item.id)}>+</button>
                <button onClick={()=> decreaseQuantity(item.id)}>-</button>
                <button onClick={()=> removeFromCart(item.id)}>Remove</button>
              </div>
              <p>Total: ${item.price * item.quantity}</p>
            </li>
          ))}
        </ul>
        <div className='mt-3'>
          <button className='btn btn-danger' onClick={clearCart}>Remove Cart</button>
          <Link to="/payment" className='btn btn-success ms-2'>Proceed to Payment</Link>
        </div>
        </>
      )}
    </div>
  );
}
