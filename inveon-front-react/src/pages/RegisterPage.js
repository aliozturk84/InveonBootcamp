import { useState } from 'react';
import { Form, Button, Card, Alert, Row, Col } from 'react-bootstrap';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const RegisterPage = () => {
  const navigate = useNavigate();
  const { register } = useAuth();
  const [formData, setFormData] = useState({
    userName: '',
    email: '',
    password: '',
    confirmPassword: ''
  });
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (formData.password !== formData.confirmPassword) {
      return setError('Şifreler eşleşmiyor');
    }

    try {
      setError('');
      setLoading(true);
      await register({
        userName: formData.userName,
        email: formData.email,
        password: formData.password
      });
      navigate('/login');
    } catch (err) {
      setError('Kayıt işlemi başarısız oldu.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Row className="justify-content-center">
      <Col md={6}>
        <Card>
          <Card.Body>
            <h2 className="text-center mb-4">Kayıt Ol</h2>
            {error && <Alert variant="danger">{error}</Alert>}
            <Form onSubmit={handleSubmit}>
              <Form.Group className="mb-3">
                <Form.Label>Kullanıcı Adı</Form.Label>
                <Form.Control
                  type="text"
                  required
                  value={formData.userName}
                  onChange={(e) => setFormData({
                    ...formData,
                    userName: e.target.value
                  })}
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Email</Form.Label>
                <Form.Control
                  type="email"
                  required
                  value={formData.email}
                  onChange={(e) => setFormData({
                    ...formData,
                    email: e.target.value
                  })}
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Şifre</Form.Label>
                <Form.Control
                  type="password"
                  required
                  value={formData.password}
                  onChange={(e) => setFormData({
                    ...formData,
                    password: e.target.value
                  })}
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Şifre Tekrar</Form.Label>
                <Form.Control
                  type="password"
                  required
                  value={formData.confirmPassword}
                  onChange={(e) => setFormData({
                    ...formData,
                    confirmPassword: e.target.value
                  })}
                />
              </Form.Group>

              <Button
                className="w-100"
                type="submit"
                disabled={loading}
              >
                {loading ? 'Kayıt Yapılıyor...' : 'Kayıt Ol'}
              </Button>
            </Form>
            <div className="text-center mt-3">
              <Link to="/login">Zaten hesabınız var mı? Giriş yapın</Link>
            </div>
          </Card.Body>
        </Card>
      </Col>
    </Row>
  );
};

export default RegisterPage; 