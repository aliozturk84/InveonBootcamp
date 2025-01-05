import { Link } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { siteInfo, themeInfo } from "../config";

export default function Navbar() {

    const { user, logout } = useAuth();

    return (
        <nav className="navbar navbar-expand-lg sticky-top" style={{ backgroundColor: themeInfo.colors.primary }}>
            <div className="container">
                <Link className="navbar-brand fw-bold" to="/">{siteInfo.title}</Link>
                <button
                    className="navbar-toggler"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target="#navbarNav"
                >
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav ms-auto">
                        <li className="nav-item">
                            <Link className="nav-link" to="/">Anasayfa</Link>
                        </li>
                        {user ? (
                            <>
                                <li className="nav-item">
                                    <Link className="nav-link" to="/profile">Profil</Link>
                                </li>
                                <li className="nav-item">
                                    <button className="btn btn-danger" onClick={logout}>Çıkış</button>
                                </li>
                            </>
                        ) : (
                            <li className="nav-item">
                                <Link className="nav-link" to="/login">Giriş yap</Link>
                            </li>
                        )}
                    </ul>
                </div>
            </div>
        </nav>
    );
}