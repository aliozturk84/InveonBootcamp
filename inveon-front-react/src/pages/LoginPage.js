import { useState } from 'react';
import { Form, Button, Card, Alert, Row, Col } from 'react-bootstrap';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const LoginPage = () => {
  const navigate = useNavigate();
  const { login, forgotPassword } = useAuth();
  const [formData, setFormData] = useState({
    email: '',
    password: ''
  });
  const [passData, setPassData] = useState({
    email: '',
  });
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [isForgotPassword, setIsForgotPassword] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      setError('');
      setLoading(true);
      await login(formData);
      navigate('/');
    } catch (err) {
      setError('Giriş başarısız oldu. Lütfen bilgilerinizi kontrol edin.');
    } finally {
      setLoading(false);
    }
  };
  const handleForgotPassBtn  = async (e) => {
    e.preventDefault();
    try {
      setError('');
      setLoading(true);
      await forgotPassword(passData);
      setIsForgotPassword(false);
      navigate('/login');
    } catch (err) {
      setError('Giriş başarısız oldu. Lütfen bilgilerinizi kontrol edin.');
    } finally {
      setLoading(false);
    }
  }

  return (
    <Row className="justify-content-center">
      <Col md={6}>
        <Card>
          <Card.Body>
            <h2 className="text-center mb-4">Giriş Yap</h2>
            {error && <Alert variant="danger">{error}</Alert>}
            {!isForgotPassword?
            <Form onSubmit={e=>e.preventDefault()}>
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
              <Button className='btn btn-secondary w-100 text-white' disabled={loading} onClick={e=>setIsForgotPassword(!isForgotPassword)}>{isForgotPassword?"Şifremi Hatırladım":"Şifremi Unuttum"}</Button>

              <Button
                className="w-100 my-2"
                type="submit"
                disabled={loading}
                onClick={handleSubmit}
              >
                {loading ? 'Giriş Yapılıyor...' : 'Giriş Yap'}
              </Button>
            </Form>
            :
            <Form onSubmit={e=>e.preventDefault()}>
              <Form.Group className="mb-3">
                <Form.Label>Email</Form.Label>
                <Form.Control
                  type="email"
                  required
                  value={passData.email}
                  onChange={(e) => setPassData({
                    email: e.target.value
                  })}
                />
              </Form.Group>
              <Button className='btn btn-secondary w-100 text-white' disabled={loading} onClick={e=>setIsForgotPassword(!isForgotPassword)}>{isForgotPassword?"Şifremi Hatırladım":"Şifremi Unuttum"}</Button>
              <Button
                className="w-100 my-2"
                type="submit"
                disabled={loading}
                onClick={handleForgotPassBtn}
              >
                {loading ? 'Şifreniz Geliyooor...' : 'Şifre Gönder'}
              </Button>
            </Form>
            }
            <div className="text-center mt-3">
              <Link to="/register">Hesabınız yok mu? Kayıt olun</Link>
            </div>
          </Card.Body>
        </Card>
      </Col>
    </Row>
  );
};

export default LoginPage; 