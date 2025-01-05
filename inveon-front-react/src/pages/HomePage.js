import { useEffect, useState } from 'react';
import { Form } from 'react-bootstrap';
import { useCourses } from '../contexts/CourseContext';
import CourseList from '../components/courses/CourseList';

const HomePage = () => {
  const { courses, loading, getAllCourses, searchCourses } = useCourses();
  const [searchTerm, setSearchTerm] = useState('');

  useEffect(() => {
    getAllCourses();
  }, []);

  const handleSearch = (e) => {
    const value = e.target.value;
    setSearchTerm(value);
    
    if (value.length >= 3) {
      searchCourses(value);
    } else if (value.length === 0) {
      getAllCourses();
    }
  };

  return (
    <div>
      <h2 className="mb-4">Popüler Kurslar</h2>
      <Form.Group className="mb-4">
        <Form.Control
          type="text"
          placeholder="Kurs Ara..."
          value={searchTerm}
          onChange={handleSearch}
        />
      </Form.Group>
      <CourseList courses={courses} loading={loading} />
    </div>
  );
};

export default HomePage; 