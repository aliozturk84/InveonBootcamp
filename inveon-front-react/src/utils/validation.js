export const validateEmail = (email) => {
  const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return re.test(email);
};

export const validatePassword = (password) => {
  // En az 8 karakter, bir büyük harf, bir küçük harf ve bir rakam
  const re = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/;
  return re.test(password);
};

export const validateName = (name) => {
  return name.length >= 2 && name.length <= 50;
};

export const validateCreditCard = (number) => {
  // Basit bir kredi kartı validasyonu
  const re = /^[0-9]{16}$/;
  return re.test(number);
};

export const validateCVV = (cvv) => {
  const re = /^[0-9]{3,4}$/;
  return re.test(cvv);
};

export const validateExpiryDate = (date) => {
  const re = /^(0[1-9]|1[0-2])\/([0-9]{2})$/;
  if (!re.test(date)) return false;

  const [month, year] = date.split('/');
  const expiry = new Date(2000 + parseInt(year), parseInt(month) - 1);
  const today = new Date();
  
  return expiry > today;
};
