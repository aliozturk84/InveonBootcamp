import { useEffect, useState } from "react";
import { fetchHomeItems } from "../services/api";
import HomeCard from "../components/HomeCard";
import { LoadingSpinner } from "../components/LoadingSpinner";

export default function Home() {

  const [loading, setLoading] = useState(false);
  const [items, setItems] = useState([]);
  const [searhTerm, setSearchTerm] = useState("");

  const [currentPage, setCurrentPage] = useState(1);

  const perPage = 6;

  useEffect(() => {
    setLoading(true)
    fetchHomeItems().then((data) => {
      setItems(data);


    }).finally(() => {
      setLoading(false)
    })
  }, []);

  const handleSearch = (e) => {
    const term = e.target.value;
    setSearchTerm(term);
    setCurrentPage(1);
  }

  const totalPages = Math.ceil(items.length / perPage);

  const pageNumbers = [...Array(totalPages).keys()].map((n) => n + 1);


  return (
    <div className="container mt-4">
      {
        loading && <LoadingSpinner size={100} />
      }

      <h1 className="text-center mb-4">Popüler Kurslar</h1>

      <div className="row mb-3">
        <div className="col-md-8"></div>
        <div className="col-md-4">
          <input
            type="text"
            className="form-control"
            placeholder="Kurs ara.."
            value={searhTerm}
            onChange={handleSearch}
          ></input>
        </div>
      </div>

      <div className="row">
        {items.map((item) => (
          <HomeCard key={item.id} item={item}></HomeCard>
        ))}
      </div>

      {totalPages > 1 && (
        <nav>
          <ul className="pagination justifycontent-center mt-4">
            {pageNumbers.map((number) => (
              <li key={number} className={`page-item ${currentPage === number ? "active" : ""}`}>
                <button onClick={() => setCurrentPage(number)} className="page-link">
                  {number}
                </button>
              </li>
            ))}
          </ul>
        </nav>
      )}
    </div>
  );
}