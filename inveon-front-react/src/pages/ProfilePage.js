import { useState, useEffect } from 'react';
import { Card, Button, Form, Tab, Tabs, Alert, Row, Col } from 'react-bootstrap';
import { useAuth } from '../contexts/AuthContext';
import { useOrders } from '../contexts/OrderContext';
import CourseCard from '../components/courses/CourseCard';
import LoadingSpinner from '../components/common/LoadingSpinner';
import { useCourses } from '../contexts/CourseContext';

const ProfilePage = () => {
  const { user, updateProfile } = useAuth();
  const { orders, getUserOrders, loading: ordersLoading } = useOrders();
  const [profileData, setProfileData] = useState({
    userName: user?.userName || '',
    email: user?.email || '',
    password: '',
    newPassword: '',
    confirmPassword: ''
  });
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [loading, setLoading] = useState(false);
  const { userCourses, getUsersCourses, createCourses } = useCourses();
  const [showCreateCourseForm, setShowCreateCourseForm] = useState(false);
  const [createCourseFields, setCreateCourseFields] = useState({
    name: "",
    description: "",
    price: 0,
    category: "",
  });

  useEffect(() => {
    getUserOrders();
    if(user.roles[0] == "Eğitmen"){
      getUsersCourses();
    }
  }, []);

  useEffect(() => {
    setProfileData(prev => ({
      ...prev,
      userName: user?.userName || '',
      email: user?.email || '',
      password: '',
      newPassword: '',
      confirmPassword: ''
    }));
  }, [user]);


  const handleProfileUpdate = async (e) => {
    e.preventDefault();
    setError('');
    setSuccess('');

    if (profileData.newPassword !== profileData.confirmPassword) {
      return setError('Yeni şifreler eşleşmiyor');
    }

    try {
      setLoading(true);
      await updateProfile({
        userName: profileData.userName,
        password: profileData.password,
        email: profileData.email,
        newPassword: profileData.newPassword || undefined
      });
      setSuccess('Profil başarıyla güncellendi');
      setProfileData(prev => ({
        ...prev,
        password: '',
        newPassword: '',
        confirmPassword: ''
      }));
    } catch (err) {
      setError('Profil güncellenirken bir hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  const showCreateCourse = () => setShowCreateCourseForm(!showCreateCourseForm);
  const handleCreateCourse = async () => {
    setError('');
    setSuccess('');

    if (createCourseFields.name == "" || createCourseFields.description == "" || createCourseFields.category == "" || createCourseFields.price == "") {
      return setError('Alanları doldurunuz.');
    }

    try {
      setLoading(true);
      await createCourses({
        ...createCourseFields
      });
      setSuccess('Kurs başarıyla kaydedildi');
      setCreateCourseFields(prev => ({
        ...prev,
        name: '',
        description: '',
        price: 0,
        category: ''
      }));
    } catch (err) {
      setError('Kurs kaydedilirken bir hata oluştu');
    } finally {
      setLoading(false);
    }
  }

  if (loading || ordersLoading) return <LoadingSpinner />;

  return (
    <div>

      <h2 className="mb-4">Profil</h2>
      <Tabs defaultActiveKey="profile" className="mb-4">
        <Tab eventKey="profile" title="Profil Bilgileri">
          <Card>
            <Card.Body>
              {error && <Alert variant="danger">{error}</Alert>}
              {success && <Alert variant="success">{success}</Alert>}
              <Form onSubmit={handleProfileUpdate}>
                <Form.Group className="mb-3">
                  <Form.Label>Kullanıcı Adı</Form.Label>
                  <Form.Control
                    type="text"
                    value={profileData.userName}
                    onChange={(e) => setProfileData(prev => ({
                      ...prev,
                      userName: e.target.value
                    }))}
                  />
                </Form.Group>

                <Form.Group className="mb-3">
                  <Form.Label>Email</Form.Label>
                  <Form.Control
                    type="email"
                    value={profileData.email}
                    disabled
                  />
                </Form.Group>

                <h5 className="mt-4">Şifre Değiştir</h5>
                <Form.Group className="mb-3">
                  <Form.Label>Mevcut Şifre</Form.Label>
                  <Form.Control
                    type="password"
                    value={profileData.password}
                    onChange={(e) => setProfileData(prev => ({
                      ...prev,
                      password: e.target.value
                    }))}
                  />
                </Form.Group>

                <Form.Group className="mb-3">
                  <Form.Label>Yeni Şifre</Form.Label>
                  <Form.Control
                    type="password"
                    value={profileData.newPassword}
                    onChange={(e) => setProfileData(prev => ({
                      ...prev,
                      newPassword: e.target.value
                    }))}
                  />
                </Form.Group>

                <Form.Group className="mb-3">
                  <Form.Label>Yeni Şifre (Tekrar)</Form.Label>
                  <Form.Control
                    type="password"
                    value={profileData.confirmPassword}
                    onChange={(e) => setProfileData(prev => ({
                      ...prev,
                      confirmPassword: e.target.value
                    }))}
                  />
                </Form.Group>

                <Button
                  type="submit"
                  variant="primary"
                  disabled={loading}
                >
                  {loading ? 'Güncelleniyor...' : 'Güncelle'}
                </Button>
              </Form>
            </Card.Body>
          </Card>
        </Tab>

        <Tab eventKey="courses" title="Satın Alınan Kurslar">
          <Row xs={1} md={2} lg={3} className="g-4">
            {orders.map(order => (
              <Col key={order.id}>
                <CourseCard course={order.course} />
              </Col>
            ))}
            {orders.length === 0 && (
              <Col xs={12}>
                <Alert variant="info">
                  Henüz satın aldığınız kurs bulunmuyor.
                </Alert>
              </Col>
            )}
          </Row>
        </Tab>

        {user.roles[0] == "Eğitmen" ?
          <Tab eventKey="my-courses" title="Hazırlanan Kurslar">
            <button className='btn btn-success my-3' onClick={e => {
              e.preventDefault();
              showCreateCourse();
            }}>Yeni Kurs</button>
            {showCreateCourseForm ?
              <Form onSubmit={(e) => {
                e.preventDefault();
                handleCreateCourse();
                showCreateCourse();

              }
              }>
                <Form.Group className="mb-3">
                  <Form.Label>Kurs İsmi</Form.Label>
                  <Form.Control
                    type="text"
                    value={createCourseFields.name}
                    onChange={(e) => setCreateCourseFields(prev => ({
                      ...prev,
                      name: e.target.value
                    }))}
                  />
                </Form.Group>
                <Form.Group className="mb-3">
                  <Form.Label>Kurs Açıklaması</Form.Label>
                  <Form.Control
                    type="text"
                    value={createCourseFields.description}
                    onChange={(e) => setCreateCourseFields(prev => ({
                      ...prev,
                      description: e.target.value
                    }))}
                  />
                </Form.Group>
                <Form.Group className="mb-3">
                  <Form.Label>Kurs Fiyatı</Form.Label>
                  <Form.Control
                    type="text"
                    value={createCourseFields.price}
                    onChange={(e) => setCreateCourseFields(prev => ({
                      ...prev,
                      price: e.target.value
                    }))}
                  />
                </Form.Group>
                <Form.Group className="mb-3">
                  <Form.Label>Kurs Kategorisi</Form.Label>
                  <Form.Control
                    type="text"
                    value={createCourseFields.category}
                    onChange={(e) => setCreateCourseFields(prev => ({
                      ...prev,
                      category: e.target.value
                    }))}
                  />
                </Form.Group>
                <Button className="mb-2" type='submit'>KAYIT</Button>
              </Form>
              : <></>}
            <Row xs={1} md={2} lg={3} className="g-4">
              {userCourses && userCourses.length > 0 ? (
                userCourses.map(order => (
                  <Col key={order.id}>
                    <CourseCard course={order} />
                  </Col>
                ))
              ) : (
                <Col xs={12}>
                  <Alert variant="info">Henüz satın aldığınız kurs bulunmuyor.</Alert>
                </Col>
              )}
            </Row>
          </Tab>
          : <></>}
      </Tabs>
    </div>
  );
};

export default ProfilePage; 