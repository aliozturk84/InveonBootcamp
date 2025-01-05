import { BrowserRouter } from 'react-router-dom';
import { AuthProvider } from './contexts/AuthContext';
import { CourseProvider } from './contexts/CourseContext';
import { OrderProvider } from './contexts/OrderContext';
import Layout from './components/layout/Layout';
import AppRoutes from './routes';
import { ToastContainer } from 'react-toastify';
import ErrorBoundary from './components/common/ErrorBoundary';

// Stil dosyaları
import 'bootstrap/dist/css/bootstrap.min.css';
import 'react-toastify/dist/ReactToastify.css';

function App() {
  return (
    <ErrorBoundary>
      <AuthProvider>
        <CourseProvider>
          <OrderProvider>
            <BrowserRouter>
              <Layout>
                <AppRoutes />
              </Layout>
              <ToastContainer 
                position="top-right"
                autoClose={3000}
                hideProgressBar={false}
                newestOnTop
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
              />
            </BrowserRouter>
          </OrderProvider>
        </CourseProvider>
      </AuthProvider>
    </ErrorBoundary>
  );
}

export default App;