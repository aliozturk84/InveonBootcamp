import { createContext, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "./AuthContext";
import alertify from "alertifyjs";

const HomeContext = createContext();

export const useHome = () => useContext(HomeContext);

export const HomeProvider = ({ children }) => {
    const [courses, setCourses] = useState([]);
    const [purchasedItems, setPurchasedItems] = useState([]);
    const [pendingItem, setPendingItem] = useState(null);

    const { user } = useAuth();
    const navigate = useNavigate();

    const buyCourse = (product) => {
        if (!user) {
            alertify.error("Giriş yapmadığınız için satın alamazsınız. Lütfen önce giriş yapın!");
            setPendingItem(product);
            navigate("/login");
            return;
        }
        setCourses((prev) => {
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
            setCourses((prev) => {
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
        setCourses((prev) =>
            prev.map((item) =>
                item.id === id ? { ...item, quantity: item.quantity + 1 } : item
            )
        );
    };

    const decreaseQuantity = (id) => {
        setCourses((prev) =>
            prev.map((item) =>
                item.id === id && item.quantity > 1
                    ? { ...item, quantity: item.quantity - 1 }
                    : item
            )
        );
    };

    const removeFromCart = (id) => {
        setCourses((prev) => prev.filter((item) => item.id !== id));
        alertify.error("Ürün sepetten çıkarıldı!");
    };

    const clearCart = () => {
        setCourses([]);
        alertify.error("Sepetinde ürün kalmadı!");
    };

    const completePurchase = () => {
        if (courses.length > 0) {
            setPurchasedItems((prev) => [...prev, ...courses]);
            setCourses([]);
            alertify.success("Satın alma başarılı!");
        } else {
            alertify.error("Sepetin boş!");
        }
    };

    return (
        <HomeContext.Provider
            value={{
                courses,
                buyCourse,
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
        </HomeContext.Provider>
    );
};
