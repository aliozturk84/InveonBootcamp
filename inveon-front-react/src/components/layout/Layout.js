import Navbar from './Navbar';
import { Container } from 'react-bootstrap';

const Layout = ({ children }) => {
  return (
    <>
      <Navbar />
      <Container className="py-4">
        {children}
      </Container>
    </>
  );
};

export default Layout; 