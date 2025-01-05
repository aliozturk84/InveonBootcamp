import { createContext, useContext, useState } from 'react';
import { courseService } from '../services/courseService';
import { toast } from 'react-toastify';

const CourseContext = createContext(null);

export const CourseProvider = ({ children }) => {
  const [courses, setCourses] = useState([]);
  const [userCourses, setUserCourses] = useState([]);
  const [loading, setLoading] = useState(false);
  const [selectedCourse, setSelectedCourse] = useState(null);

  const getAllCourses = async () => {
    setLoading(true);
    try {
      const response = await courseService.getAllCourses();
      setCourses(response.data);
    } catch (error) {
      toast.error('Kurslar yüklenirken bir hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  const getCourseById = async (id) => {
    setLoading(true);
    try {
      const response = await courseService.getCourseById(id);
      setSelectedCourse(response.data);
      return response.data;
    } catch (error) {
      toast.error('Kurs detayları yüklenemedi');
      throw error;
    } finally {
      setLoading(false);
    }
  };

  const searchCourses = async (query) => {
    setLoading(true);
    try {
      const response = await courseService.searchCourses(query);
      setCourses(response.data);
    } catch (error) {
      toast.error('Arama yapılırken bir hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  const getUsersCourses = async ()=>{
    setLoading(true);
    try{
      const response = await courseService.getUsersCourses();
      setUserCourses(response.data);
    }catch (error) {
      toast.error('Giriş yapılırken bir hata oluştu');
    } finally {
      setLoading(false);
    }
  }
  const createCourses = async (course)=>{
    setLoading(true);
    try{
      await courseService.createCourses(course);
      const response = await courseService.getUsersCourses();
      setUserCourses(response.data);
      }catch (error) {
      toast.error('Giriş yapılırken bir hata oluştu');
    } finally {
      setLoading(false);
    }
  }

  const value = {
    courses,
    userCourses,
    loading,
    selectedCourse,
    getAllCourses,
    getCourseById,
    searchCourses,
    getUsersCourses,
    createCourses
  };

  return <CourseContext.Provider value={value}>{children}</CourseContext.Provider>;
};

export const useCourses = () => {
  const context = useContext(CourseContext);
  if (!context) {
    throw new Error('useCourses must be used within a CourseProvider');
  }
  return context;
}; 