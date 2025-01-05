import React, { useState } from 'react'
import { useCart } from '../context/CartContext'

export default function Profile() {
  const {purchasedItems} = useCart();

  const [selectedOrder, setSelectedOrder] = useState(null);

  const orders = purchasedItems.reduce((acc, item, index)=>{
    const orderIndex = Math.floor(index/3);
    if(!acc[orderIndex]) acc[orderIndex] = [];
    acc[orderIndex].push(item);
    return acc;
  }, []);

  return(
    <div className='container mt-4'>
      <h2>Your Orders</h2>

      {orders.length === 0 ? (
        <p>Your have no orders yet.</p>
      ): (
        <ul className='list-group'>
          {orders.map((order,index) =>(
            <li className='list-group-item list-group-item-action' 
            onClick={()=>setSelectedOrder(order)}
            >
                <strong>Order {index + 1}</strong>
            </li>
          ))}
        </ul>
      )}

      {selectedOrder && (
        <div
          className='modal show d-block'
          tabIndex="-1"
          role='dialog'
          style={{backgroundColor: "rgba(0,0,0,0.5)"}}
        >
          <div className='modal-dialog'>
            <div className='modal-content'>
              <div className='modal-header'>
                <h5>Order Details</h5>
                <button className='btn-close' onClick={() => setSelectedOrder(null)}></button>
              </div>
              <div className='modal-body'>
                  <ul className='list-group'>
                    {selectedOrder.map((item, idx)=>(
                      <li key={idx} className='list-group-item'>
                        <strong>{item.title}</strong> - ${item.price} x {item.quantity}
                      </li>
                    ))}
                  </ul>
              </div>
              <div className='modal-footer'>
                <button className='btn btn-secondary' onClick={() => setSelectedOrder(null)}>Close</button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
