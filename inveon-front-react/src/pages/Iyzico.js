import React, { useState } from 'react';
import axios from 'axios';
import { orderService } from '../services/orderService';
import { useParams } from "react-router-dom";


const Iyzico = () => {
  const [html, setHtml] = useState(null);
  const [success, setSuccess] = useState("Ödeme Başarılı");
  const [isPaymentSuccess, setIsPaymentSuccess] = useState(false);
  const { courseId } = useParams(); // URL'den courseId parametresini al


  const pay = async () => {
    await orderService.purchaseCourse(courseId).then((response) => {
        const  content  = response.paymentResponse.data.iyzicoResult;
        // HTML içeriğini güvenli şekilde ayarla
        const blob = new Blob([content], { type: 'text/html' });
        const obj = URL.createObjectURL(blob);
        setHtml(obj);
      })
      .catch((error) => {
        console.error('Ödeme işlemi başlatılamadı:', error);
      });
  };

  const handlePayCallback = (status) => {
    if (status === "success") {
      setIsPaymentSuccess(true);
    } else {
      console.error("Ödeme başarısız oldu!");
    }
  };

  return (
    <div>
      <h1>Iyzico Pay</h1>
      {!isPaymentSuccess ? (
        <>
          <button onClick={pay}>Pay</button>
          <br />
          <br />
          {html && <iframe width="500" height="500" src={html} title="Iyzico Payment"></iframe>}
        </>
      ) : (
        <div>
          <h1>{success}</h1>
        </div>
      )}
    </div>
  );
};

export default Iyzico;
