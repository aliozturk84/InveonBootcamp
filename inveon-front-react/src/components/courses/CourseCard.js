import { Card, Button, ProgressBar } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

const CourseCard = ({ course, showProgress = false, progress = 0 }) => {
  const navigate = useNavigate();

  return (
    <Card className="h-100">
      <Card.Img 
        variant="top" 
        src={course.imageUrl || 'https://via.placeholder.com/300x200'} 
        alt={course.name}
      />
      <Card.Body className="d-flex flex-column">
        <Card.Title>{course.name}</Card.Title>
        <Card.Text>{course.description}</Card.Text>
        <div className="mt-auto">
          {showProgress && (
            <div className="mb-3">
              <ProgressBar 
                now={progress} 
                label={`${progress}%`}
                variant="success"
              />
            </div>
          )}
          <div className="d-flex justify-content-between align-items-center">
            {!showProgress && <h5 className="mb-0">{course.price} TL</h5>}
            <Button 
              variant={showProgress ? "primary" : "outline-primary"}
              onClick={() => navigate(`/course/${course.id}`)}
            >
              {showProgress ? 'Kursa Devam Et' : 'Detayları Gör'}
            </Button>
          </div>
        </div>
      </Card.Body>
    </Card>
  );
};

export default CourseCard; 