import React from 'react'
import { useHome } from '../context/HomeContext'
import { Link } from 'react-router-dom';

export default function HomeCard({ item }) {

    const { buyCourse } = useHome();

    return (
        <div className='col-md-4 mb-4'>
            <div className='card h-100 shadow-sm'>
                <Link to={`/product/${item.id}`} style={{ textDecoration: "none" }}>
                    <img
                        src={item.image}
                        alt={item.title}
                        className='card-img-top'
                        style={{ objectFit: "contain", height: "200px" }}
                    ></img>
                    <div className='card-body'>
                        <p className='card-title'>{item.title}</p>
                        <p className='card-text'>{item.summary}</p>
                        <p className='card-text'>{item.price}₺</p>
                    </div>
                </Link>
                <button
                    className='btn btn-info w-100 h-100'
                    onClick={() => buyCourse(item)}
                >Satın Al</button>
            </div>
        </div>
    );
}
