import React from 'react'
import { useAuth } from '../context/AuthContext'
import { useCart } from '../context/CartContext';
import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';
import alertify from 'alertifyjs';

export default function Login() {
  const { login } = useAuth();
  const { addPendingItem } = useCart();

  const { register, handleSubmit, formState: { errors, isSubmitted } } = useForm();

  const navigate = useNavigate();

  const onSubmit = (data) => {
    if (data.username === "admin" && data.password === "1234") {
      login(data.username, data.password);
      alertify.success("Giriş başarılı!");

      addPendingItem();
      navigate("/cart");
    } else {
      alertify.error("Kullanıcı adı veya parola hatalı!");
    }
  }

  return (
    <div className='container d-flex justify-content-center align-items-center' style={{ height: "100vh" }}>
      <div className='card shadow p-4' style={{ width: "400px" }}>
        <h2 className='text-center mt-4'>Giriş yap</h2>
        <form onSubmit={handleSubmit(onSubmit)}>
          <div className='mb-3'>
            <label className='form-label'>Kullanıcı adı</label>
            <input
              {...register("username", { required: true })}
              className='form-control'
              placeholder='kullanıcı adı giriniz'
            ></input>
            {errors.username && <small className='text-danger'>Kullanıcı adı zorunlu</small>}
          </div>
          <div className='mb-3'>
            <label className='form-label'>Şifre</label>
            <input
              {...register("password", { required: true })}
              className='form-control'
              placeholder='şifre giriniz'
            ></input>
            {errors.password && <small className='text-danger'>Şifre zorunlu</small>}
          </div>
          <button type='submit' className='btn btn-success w-100'>Giriş</button>
        </form>
      </div>
    </div>
  );
}

