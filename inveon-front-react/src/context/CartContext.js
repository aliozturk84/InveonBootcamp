import { createContext, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "./AuthContext";
import alertify from "alertifyjs";

const CartContext = createContext();

export const useCart = () => useContext(CartContext);

export const CartProvider = ({ children }) => {
  const [cart, setCart] = useState([]);
  const [purchasedItems, setPurchasedItems] = useState([]);
  const [pendingItem, setPendingItem] = useState(null);

  const { user } = useAuth();
  const navigate = useNavigate();

  const addToCart = (product) => {
    if (!user) {
      alertify.error("Giriş yapmadığınız için ürünü sepete ekleyemezsiniz!");
      setPendingItem(product);
      navigate("/login");
      return;
    }
    setCart((prev) => {
      const itemExists = prev.find((item) => item.id === product.id);

      if (itemExists) {
        return prev.map((item) =>
          item.id === product.id
            ? { ...item, quantity: item.quantity + 1 }
            : item
        );
      }
      return [...prev, { ...product, quantity: 1 }];
    });
    alertify.success("Ürün sepete eklendi!");
  };

  const addPendingItem = () => {
    if (pendingItem) {
      setCart((prev) => {
        const itemExists = prev.find((item) => item.id === pendingItem.id);

        if (itemExists) {
          return prev.map((item) =>
            item.id === pendingItem.id
              ? { ...item, quantity: item.quantity + 1 }
              : item
          );
        }
        return [...prev, { ...pendingItem, quantity: 1 }];
      });
      alertify.success("Bekleyen ürün sepete eklendi!");
      setPendingItem(null);
    }
  };

  const increaseQuantity = (id) => {
    setCart((prev) =>
      prev.map((item) =>
        item.id === id ? { ...item, quantity: item.quantity + 1 } : item
      )
    );
  };

  const decreaseQuantity = (id) => {
    setCart((prev) =>
      prev.map((item) =>
        item.id === id && item.quantity > 1
          ? { ...item, quantity: item.quantity - 1 }
          : item
      )
    );
  };

  const removeFromCart = (id) => {
    setCart((prev) => prev.filter((item) => item.id !== id));
    alertify.error("Ürün sepetten çıkarıldı!");
  };

  const clearCart = () => {
    setCart([]);
    alertify.error("Sepetinde ürün kalmadı!");
  };

  const completePurchase = () => {
    if (cart.length > 0) {
      setPurchasedItems((prev) => [...prev, ...cart]);
      setCart([]);
      alertify.success("Satın alma başarılı!");
    } else {
      alertify.error("Sepetin boş!");
    }
  };

  return (
    <CartContext.Provider
      value={{
        cart,
        addToCart,
        addPendingItem,
        increaseQuantity,
        decreaseQuantity,
        removeFromCart,
        clearCart,
        completePurchase,
        purchasedItems,
      }}
    >
      {children}
    </CartContext.Provider>
  );
};
