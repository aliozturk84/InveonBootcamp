import { Modal, Form, Button } from 'react-bootstrap';
import { useState } from 'react';

const PaymentModal = ({ show, onHide, onComplete, course }) => {
  const [formData, setFormData] = useState({
    cardNumber: '',
    expiryDate: '',
    cvv: '',
    nameOnCard: ''
  });

  const handleSubmit = (e) => {
    e.preventDefault();
    onComplete(formData);
  };

  return (
    <Modal show={show} onHide={onHide} centered>
      <Modal.Header closeButton>
        <Modal.Title>Ödeme Bilgileri</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form onSubmit={handleSubmit}>
          <Form.Group className="mb-3">
            <Form.Label>Kart Üzerindeki İsim</Form.Label>
            <Form.Control
              type="text"
              value={formData.nameOnCard}
              onChange={(e) => setFormData({...formData, nameOnCard: e.target.value})}
              required
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Kart Numarası</Form.Label>
            <Form.Control
              type="text"
              value={formData.cardNumber}
              onChange={(e) => setFormData({...formData, cardNumber: e.target.value})}
              required
              maxLength="16"
            />
          </Form.Group>

          <div className="row">
            <div className="col-md-6">
              <Form.Group className="mb-3">
                <Form.Label>Son Kullanma Tarihi</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="MM/YY"
                  value={formData.expiryDate}
                  onChange={(e) => setFormData({...formData, expiryDate: e.target.value})}
                  required
                />
              </Form.Group>
            </div>
            <div className="col-md-6">
              <Form.Group className="mb-3">
                <Form.Label>CVV</Form.Label>
                <Form.Control
                  type="text"
                  value={formData.cvv}
                  onChange={(e) => setFormData({...formData, cvv: e.target.value})}
                  required
                  maxLength="3"
                />
              </Form.Group>
            </div>
          </div>

          <div className="d-flex justify-content-between align-items-center">
            <h5 className="mb-0">Toplam: {course?.price} TL</h5>
            <Button variant="primary" type="submit">
              Ödemeyi Tamamla
            </Button>
          </div>
        </Form>
      </Modal.Body>
    </Modal>
  );
};

export default PaymentModal; 