import { Navbar, Container, Nav, NavDropdown } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../contexts/AuthContext';
import { siteInfo } from '../../config';

const Navigation = () => {
  const navigate = useNavigate();
  const { user, isAuthenticated, logout } = useAuth();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <Navbar bg="dark" variant="dark" expand="lg" className="mb-4">
      <Container>
        <Navbar.Brand onClick={() => navigate('/')} style={{ cursor: 'pointer' }}>
          {siteInfo.title}
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link onClick={() => navigate('/')}>Ana Sayfa</Nav.Link>
            <Nav.Link onClick={() => navigate('/courses')}>Kurslar</Nav.Link>
          </Nav>
          <Nav>
            {!isAuthenticated ? (
              <>
                <Nav.Link onClick={() => navigate('/login')}>Giriş Yap</Nav.Link>
                <Nav.Link onClick={() => navigate('/register')}>Kayıt Ol</Nav.Link>
              </>
            ) : (
              <NavDropdown title={user?.fullName || 'Profil'} id="basic-nav-dropdown">
                <NavDropdown.Item onClick={() => navigate('/profile')}>
                  Profilim
                </NavDropdown.Item>
                <NavDropdown.Item onClick={() => navigate('/my-courses')}>
                  Kurslarım
                </NavDropdown.Item>
                <NavDropdown.Divider />
                <NavDropdown.Item onClick={handleLogout}>
                  Çıkış Yap
                </NavDropdown.Item>
              </NavDropdown>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default Navigation; 