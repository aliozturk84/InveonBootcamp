import { Row, Col } from 'react-bootstrap';
import CourseCard from './CourseCard';
import LoadingSpinner from '../common/LoadingSpinner';

const CourseList = ({ courses, loading }) => {
  if (loading) return <LoadingSpinner />;

  return (
    <Row xs={1} md={2} lg={3} className="g-4">
      {courses.map(course => (
        <Col key={course.id}>
          <CourseCard course={course} />
        </Col>
      ))}
      {courses.length === 0 && (
        <Col xs={12}>
          <p className="text-center">Kurs bulunamadı.</p>
        </Col>
      )}
    </Row>
  );
};

export default CourseList; 