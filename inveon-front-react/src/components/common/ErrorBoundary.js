import React from 'react';
import { Alert, Button } from 'react-bootstrap';

class ErrorBoundary extends React.Component {
  constructor(props) {
    super(props);
    this.state = { hasError: false };
  }

  static getDerivedStateFromError(error) {
    return { hasError: true };
  }

  componentDidCatch(error, errorInfo) {
    console.error('Error:', error);
    console.error('Error Info:', errorInfo);
  }

  render() {
    if (this.state.hasError) {
      return (
        <Alert variant="danger" className="m-3">
          <Alert.Heading>Bir şeyler yanlış gitti</Alert.Heading>
          <p>Lütfen sayfayı yenilemeyi deneyin.</p>
          <Button 
            variant="outline-danger"
            onClick={() => window.location.reload()}
          >
            Sayfayı Yenile
          </Button>
        </Alert>
      );
    }

    return this.props.children;
  }
}

export default ErrorBoundary; 