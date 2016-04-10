using ActiveLearning.DB;
using ActiveLearning.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Repository.Repository
{
    public class CourseRepository : ICourseRepository
    {
        //public CourseRepository(ENET_Project_Active_Learning_Group4Entities context) : base(context)
        //{
        //}

        public void AddCourse(Course course)
        {
            using (ENET_Project_Active_Learning_Group4Entities db = new ENET_Project_Active_Learning_Group4Entities())
            {
                db.Courses.Add(course);
                db.SaveChanges();

            }
        }

        public void EnrolInstructorToCourse(int courseID, int instructorID)
        {
            using (ENET_Project_Active_Learning_Group4Entities db = new ENET_Project_Active_Learning_Group4Entities())
            {
                Instructor_Course_Map map = new Instructor_Course_Map();
                map.InstructorSid = instructorID;
                map.CourseSid = courseID;

                db.Instructor_Course_Map.Add(map);
                db.SaveChanges();

            }
        }

        public void EnrolStudentToCourse(int courseID, int studentID)
        {
            using (ENET_Project_Active_Learning_Group4Entities db = new ENET_Project_Active_Learning_Group4Entities())
            {
                Student_Course_Map map = new Student_Course_Map();
                map.StudentSid = studentID;
                map.CourseSid = courseID;

                db.Student_Course_Map.Add(map);
                db.SaveChanges();

            }
        }

        public List<Course> GetCoursesByInstructor(int instructorID)
        {
            List<Course> _courses = new List<Course>();

            using (ENET_Project_Active_Learning_Group4Entities db = new ENET_Project_Active_Learning_Group4Entities())
            {

                foreach (var course in db.Instructor_Course_Map)
                {
                    if (course.InstructorSid == instructorID)
                    {

                        _courses.Add(course.Course);
                    }
                }
            }

            return _courses;
        }

        public List<Course> GetCoursesByStudent(int studentID)
        {
            List<Course> _courses = new List<Course>();

            using (ENET_Project_Active_Learning_Group4Entities db = new ENET_Project_Active_Learning_Group4Entities())
            {

                foreach (var course in db.Student_Course_Map)
                {
                    if (course.StudentSid == studentID)
                    {

                        _courses.Add(course.Course);
                    }
                }
            }

            return _courses;
        }

        public bool IsCourseExist(int courseID)
        {
            using (ENET_Project_Active_Learning_Group4Entities db = new ENET_Project_Active_Learning_Group4Entities())
            {
                foreach (var _course in db.Courses)
                {
                    if (_course.Sid == courseID)
                    {
                        return true;
                    }
                }


            }

            return false;
        }

        public void RemoveInstructorFromCourse(int courseID, int instructorID)
        {
            using (ENET_Project_Active_Learning_Group4Entities db = new ENET_Project_Active_Learning_Group4Entities())
            {
                Instructor_Course_Map map = null;

                foreach (var course in db.Instructor_Course_Map)
                {
                    if (course.InstructorSid == instructorID && course.CourseSid == courseID)
                    {
                        map = course;

                        break;
                    }
                }

                if (map != null)
                {
                    db.Instructor_Course_Map.Remove(map);
                    db.SaveChanges();
                }

            }
        }

        public void RemoveStudentFromCourse(int courseID, int studentID)
        {
            using (ENET_Project_Active_Learning_Group4Entities db = new ENET_Project_Active_Learning_Group4Entities())
            {
                Student_Course_Map map = null; ;

                foreach (var course in db.Student_Course_Map)
                {
                    if (course.StudentSid == studentID && course.CourseSid == courseID)
                    {

                        map = course;

                        break;
                    }
                }

                if (map != null)
                {
                    db.Student_Course_Map.Remove(map);
                    db.SaveChanges();
                }
            }
        }
    }
}
