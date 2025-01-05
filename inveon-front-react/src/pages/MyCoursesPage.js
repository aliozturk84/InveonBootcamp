import { useEffect } from 'react';
import { Container, Row, Col, Alert } from 'react-bootstrap';
import { useOrders } from '../contexts/OrderContext';
import CourseCard from '../components/courses/CourseCard';
import LoadingSpinner from '../components/common/LoadingSpinner';

const MyCoursesPage = () => {
  const { orders, getUserOrders, loading } = useOrders();

  useEffect(() => {
    getUserOrders();
  }, []);

  if (loading) return <LoadingSpinner />;

  return (
    <Container>
      <h2 className="mb-4">Kurslarım</h2>
      {orders.length === 0 ? (
        <Alert variant="info">
          Henüz satın aldığınız kurs bulunmuyor.
        </Alert>
      ) : (
        <Row xs={1} md={2} lg={3} className="g-4">
          {orders.map(order => (
            <Col key={order.id}>
              <CourseCard 
                course={order.course}
                showProgress={true}
                progress={order.progress || 0}
              />
            </Col>
          ))}
        </Row>
      )}
    </Container>
  );
};

export default MyCoursesPage; 