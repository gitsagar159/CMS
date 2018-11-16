using CMS.Dto;
using CMS.Util;
using Newtonsoft.Json;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class AdminController : Controller
    {
        private DataAccess dataAccess = new DataAccess();

        public ActionResult Index()
        {
            if (HttpContext.Session["AdminUser"] == null)
            {
                return View();
            }
            else
            {
                return View("Dashboard");
            }
        }

        public ActionResult Dashboard()
        {
            
            if (HttpContext.Session["AdminUser"] != null)
            {
                return View();
            }
            else
            {
                ViewData["LoginError"] = "Please Login First";
                return View("Index");
            }

            

        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        public async Task<ActionResult> Login(AdminUserDto adminUser)
        {
           
            if (string.IsNullOrEmpty(adminUser.UserName) || string.IsNullOrEmpty(adminUser.Password))
            {
                return View("index");
            }
            else
            {
                var userDetails = await dataAccess.GetAdminUseByKey(adminUser);

                if(userDetails != null)
                {
                    HttpContext.Session["AdminUser"] = userDetails;
                    return View("Dashboard");
                }
                else
                {
                    ViewData["LoginError"] = "Creadential are Wrong";
                    return View("index");
                }

                
            }


        }


        public async Task<ActionResult> Course(int? Cid)
        {
            if (HttpContext.Session["AdminUser"] != null)
            {
                CourseDto course = new CourseDto();
                if (Cid != null)
                {
                    var courseDetails = await dataAccess.GetCourseByKey(Cid);
                    ViewBag.eligigbleCourseList = await dataAccess.GetAllQualification();
                    ViewBag.mainStreamList = await dataAccess.GetAllMainStream();
                    return View(courseDetails);
                }
                else
                {
                    ViewBag.eligigbleCourseList = await dataAccess.GetAllQualification();
                    ViewBag.mainStreamList = await dataAccess.GetAllMainStream();
                    return View();
                }
            }
            else
            {
                ViewData["LoginError"] = "Please Login First";
                return View("Index");
                
            }
            
        }


        public async Task<ActionResult> ViewCourse()
        {
            if (HttpContext.Session["AdminUser"] != null)
            {
                List<CourseDetailsViewModel> objCouseDetailList = new List<CourseDetailsViewModel>();

                var courseList = await dataAccess.GetAllCourse();

                foreach (var item in courseList)
                {
                    CourseDetailsViewModel courseDetails = new CourseDetailsViewModel();

                    string[] EligibilityIdArray = new string[] { };
                    if (!string.IsNullOrEmpty(item.EligibleCourseId))
                    {
                        EligibilityIdArray = item.EligibleCourseId.Split(',');
                    }

                    if (EligibilityIdArray.Count() > 0)
                    {
                        foreach (var eligibleCourse in EligibilityIdArray)
                        {
                            var eligibleCourseName = await dataAccess.GetEligibleCourseByKye(Convert.ToInt32(eligibleCourse));
                            courseDetails.Eligibilitie.Add(eligibleCourseName);
                        }
                    }

                    courseDetails.Cid = item.cid;
                    courseDetails.CourseName = item.CourseName;

                    var mainStream = await dataAccess.GetMainStreamByKey(item.MainStreamId);
                    var subStream = await dataAccess.GetSubStreamByKey(item.SubStreamId);
                    courseDetails.MainStreamName = mainStream.MainStreamName;
                    courseDetails.SubStreamName = subStream == null ? string.Empty : subStream.SubStreamName;

                    objCouseDetailList.Add(courseDetails);
                }
                return View(objCouseDetailList);

            }
            else
            {
                ViewData["LoginError"] = "Please Login First";
                return View("Index");
            }
            
            
        }

        public async Task<ActionResult> CourseInsertUpdate(CourseDto course)
        {
            int courseAdd = await dataAccess.InsertUpdateCourse(course);

            if(courseAdd != 0 && course.cid != 0)
            {
                return Json("Update");
            }
            else
            {
                return Json("Add");
            }

        }

        public async Task<ActionResult> GetSubStreamData(int Mid)
        {
            var subStreamList = await dataAccess.GetSubStreamListByKey(Mid);
            string res = JsonConvert.SerializeObject(subStreamList);
            return Json(res);
        }

        public async Task<ActionResult> DeleteCourse(int id)
        {
            var subStreamList = await dataAccess.GetDeleteCourseByKye(id);
            string res = JsonConvert.SerializeObject(subStreamList);
            return Json(res);
        }
    }
}