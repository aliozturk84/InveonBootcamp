import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Row, Col, Card, Button, Badge } from 'react-bootstrap';
import { useCourses } from '../contexts/CourseContext';
import { useAuth } from '../contexts/AuthContext';
import { useOrders } from '../contexts/OrderContext';
import LoadingSpinner from '../components/common/LoadingSpinner';
import PaymentModal from '../components/common/PaymentModal';

const CourseDetailPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { getCourseById, loading } = useCourses();
  const { isAuthenticated } = useAuth();
  const { purchaseCourse } = useOrders();
  
  const [course, setCourse] = useState(null);
  const [showPaymentModal, setShowPaymentModal] = useState(false);

  useEffect(() => {
    loadCourseDetails();
  }, [id]);

  const loadCourseDetails = async () => {
    try {
      const data = await getCourseById(id);
      setCourse(data);
    } catch (error) {
      console.error('Error loading course:', error);
    }
  };

  const handlePurchase = () => {
    if (!isAuthenticated) {
      navigate('/login', { state: { from: `/course/${id}` } });
      return;
    }
    setShowPaymentModal(true);
  };

  const handlePaymentComplete = async (paymentDetails) => {
    try {
      // await purchaseCourse(id);
      navigate(`/payment/${id}`);
    } catch (error) {
      console.error('Purchase failed:', error);
    }
    setShowPaymentModal(false);
  };

  if (loading) return <LoadingSpinner />;
  if (!course) return <div>Kurs bulunamadı.</div>;
  
  return (
    <Row>
      <Col lg={8}>
        <Card>
          <Card.Img 
            variant="top" 
            src={course.imageUrl || 'https://via.placeholder.com/800x400'} 
            alt={course.name}
          />
          <Card.Body>
            <Card.Title className="h3">{course.name}</Card.Title>
            <div className="mb-3">
              <Badge bg="primary" className="me-2">{course.category}</Badge>
            </div>
            <Card.Text>{course.description}</Card.Text>
          </Card.Body>
        </Card>
      </Col>
      <Col lg={4}>
        <Card className="sticky-top" style={{ top: '20px' }}>
          <Card.Body>
            <h3 className="mb-3">{course.price} TL</h3>
            <Button 
              variant="primary" 
              size="lg" 
              className="w-100 mb-3"
              onClick={handlePurchase}
            >
              Satın Al
            </Button>
          </Card.Body>
        </Card>
      </Col>

      <PaymentModal 
        show={showPaymentModal}
        onHide={() => setShowPaymentModal(false)}
        onComplete={handlePaymentComplete}
        course={course}
      />
    </Row>
  );
};

export default CourseDetailPage; 