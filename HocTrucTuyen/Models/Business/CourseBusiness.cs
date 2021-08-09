using HocTrucTuyen.Models.DTO;
using HocTrucTuyen.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HocTrucTuyen.Models.Business
{
    public class CourseBusiness
    {
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();

        public IEnumerable<CourseDTO> getCourse(int page, int pagesize)
        {
            var query = from co in db.Courses
                        join te in db.Teachers on co.Teacher_ID equals te.ID
                        select new CourseDTO()
                        {
                            ID = co.ID,
                            Title = co.Title,
                            Description = co.Description,
                            Images = co.Images,
                            CreatedDate = co.CreatedDate,
                            Teacher_Name = te.Name
                        };
            var model = query.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
            return model;
        }

        public List<CourseDTO> getAll()
        {
            var query = from co in db.Courses
                        join te in db.Teachers on co.Teacher_ID equals te.ID
                        select new CourseDTO()
                        {
                            ID = co.ID,
                            Title = co.Title,
                            Description = co.Description,
                            Images = co.Images,
                            CreatedDate = co.CreatedDate,
                            Teacher_Name = te.Name
                        };
            var model = query.OrderByDescending(x => x.CreatedDate).ToList();
            return model;
        }

        public bool addReview(Review entity)
        {
            try
            {
                db.Reviews.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //lấy review khóa học
        public List<ReviewDTO> getReview(long course_id)
        {
            var query = from rev in db.Reviews
                        join cou in db.Courses on rev.Course_ID equals cou.ID
                        join user in db.Users on rev.User_ID equals user.ID
                        where rev.Course_ID == course_id
                        select new ReviewDTO()
                        {
                            ID = rev.ID,
                            Content = rev.Content,
                            Rating = rev.Rating,
                            Fullname = user.FullName,
                            CreatedDate = rev.CreatedDate
                        };
            return query.OrderByDescending(x => x.CreatedDate).ToList();
        }

        //chi tiết bài giảng
        public CourseDTO detailCourse(long id)
        {
            var query = from co in db.Courses
                        join te in db.Teachers on co.Teacher_ID equals te.ID
                        select new CourseDTO()
                        {
                            ID = co.ID,
                            Title = co.Title,
                            Description = co.Description,
                            Images = co.Images,
                            CreatedDate = co.CreatedDate,
                            Teacher_Name = te.Name
                        };
            var model = query.SingleOrDefault(x => x.ID == id);
            return model;
        }
        public Course findID(long id)
        {
            return db.Courses.Find(id);
        }

        public bool addCourse(Course entity)
        {
            try
            {
                db.Courses.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool editCourse(Course entity)
        {
            try
            {
                var course = db.Courses.Find(entity.ID);
                course.Title = entity.Title;
                course.Description = entity.Description;
                course.Teacher_ID = entity.Teacher_ID;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteCourse(long id)
        {
            try
            {
                var course = db.Courses.Find(id);
                db.Courses.Remove(course);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //lấy giáo viên
        public List<Teacher> GetTeacher()
        {
            return db.Teachers.ToList();
        }

        //Get link 
        public string GetLink(string str)
        {
            string link = "";
            int dem = 1;
            for(var i = str.Length - 1; i >= 0; i--)
            {
                if (dem <= 11)
                {
                    link += str[i];
                    dem++;
                }
                else
                {
                    break;
                }
            }
            char[] arr = link.ToCharArray(); // chuỗi thành mảng ký tự
            Array.Reverse(arr); // đảo ngược mảng
            return new string(arr); // trả về chuỗi mới sau khi đảo mảng
        }
    }
}