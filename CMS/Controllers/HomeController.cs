﻿using CMS.Dto;
using CMS.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private DataAccess dataAccess = new DataAccess();
        
        public async Task<ActionResult> Index()
        {
            //var mainStream_list = await dataAccess.GetAllMainStream();
            ViewBag.eligigbleCourseList = await dataAccess.GetAllQualification();
            ViewBag.mainStreamList = await dataAccess.GetAllMainStream();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public async Task<ActionResult> AddUserDetails(UsersDto usr)
        {
            UserDetailsViewModel userModel = new UserDetailsViewModel();

            


            var User = await dataAccess.InsertUpdateUser(usr);

           

            //var test =  GetCourseDetailsOfUser(User);
            var userDetails = await dataAccess.GetUserByKey(User);

            var lastQualification = await dataAccess.GetEligibleCourseByKye(userDetails.QualificationId);

            userModel.id = userDetails.id;
            userModel.UserName = userDetails.UserName;
            userModel.LastQualification = lastQualification.EligibleCourseName;
            userModel.Email = userDetails.Email;
            userModel.Contact = userDetails.Contact;
            


            List<CourseDto> courseDetails = new List<CourseDto>();
            List<QualificationDto> ObjQualification = new List<QualificationDto>();

            var mainStream = await dataAccess.GetMainStreamByKey(userDetails.MainStreamId);
            var subStream = await dataAccess.GetSubStreamByKey(userDetails.SubStreamId);

            userModel.MainStream = mainStream.MainStreamName;
            userModel.SubStream = subStream != null ?  subStream.SubStreamName : string.Empty;

            courseDetails = await dataAccess.GetCoursForUser(userDetails.MainStreamId, userDetails.SubStreamId);

            //if (userDetails.SubStreamId != 0)
            //{
            //    courseDetails = await dataAccess.GetCoursForUser(userDetails.MainStreamId, userDetails.SubStreamId);
            //}



            foreach (var item in courseDetails)
            {
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
                        if (usr.QualificationId == eligibleCourseName.Eid)
                        {
                            QualificationDto objQualificatio = new QualificationDto();
                            objQualificatio.EligibleCourseName = item.CourseName;
                            objQualificatio.Eid = item.cid;
                            objQualificatio.DeleteFlag = item.DeleteFlag;
                            objQualificatio.College = item.College;
                            objQualificatio.University = item.University;
                            objQualificatio.duration = item.duration;

                            ObjQualification.Add(objQualificatio);
                        }

                    }
                }
            }

            if(ObjQualification != null)
            {
                var result = ObjQualification.GroupBy(q => q.Eid)
                   .Select(grp => grp.First())
                   .ToList();

                foreach(var item in result)
                {
                    userModel.Course.Add(item);
                }
                
            }

           

            if (userModel != null)
            {
                HttpContext.Session["UserDetails"] = userModel;
            }
           

            return Json(User);
        }

        public ActionResult CourseDetails()
        {
            if(HttpContext.Session["UserDetails"] != null)
            {
                var UserDetails = (UserDetailsViewModel)HttpContext.Session["UserDetails"];
                return View(UserDetails);
            }
            else
            {
                return View();
            }
            
        }

        public async Task<ActionResult> GetSubStreamData(int Mid)
        {

            //var dataAccess = new DataAccess();

            var subStreamList = await dataAccess.GetSubStreamListByKey(Mid);
            string res = JsonConvert.SerializeObject(subStreamList);
            return Json(res);
        }

        public async Task<UserDetailsViewModel> GetCourseDetailsOfUser(int userId)
        {
            //var dataAccess = new DataAccess();

            var userDetails = await dataAccess.GetUserByKey(userId);

            UserDetailsViewModel userModel = new UserDetailsViewModel();

            List<CourseDto> courseDetails = new List<CourseDto>();

            var lastQualification = await dataAccess.GetEligibleCourseByKye(userDetails.QualificationId);
            var mainStream = await dataAccess.GetMainStreamByKey(userDetails.MainStreamId);
            var subStream = await dataAccess.GetSubStreamByKey(userDetails.SubStreamId);

            userModel.id = userDetails.id;
            userModel.UserName = userDetails.UserName;
            userModel.Email = userDetails.Email;
            userModel.LastQualification = lastQualification.EligibleCourseName;
            userModel.Contact = userDetails.Contact;
            userModel.MainStream = mainStream.MainStreamName;
            userModel.SubStream = subStream.SubStreamName;


            if (userDetails.SubStreamId != 0)
            {
               courseDetails = await dataAccess.GetCoursForUser(userDetails.MainStreamId, userDetails.SubStreamId);
            }

            foreach(var item in courseDetails)
            {
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
                        userModel.Course.Add(eligibleCourseName);
                    }
                }
            }

            if(userModel != null)
            {
                HttpContext.Session["UserDetails"] = userModel;
            }
            

            return userModel;
        }

        public async Task<ActionResult> ContactUsDetails(ContactUsDto contactus)
        {
            var insetedContact = await dataAccess.InsertContactus(contactus);
            
            return Json("Ok");
        }

        public async Task<ActionResult> Statastics()
        {
            StatasticsDto objstatastic = new StatasticsDto();

            

            var StreameData = await dataAccess.GetUserCountByStream();

            List<chart> objChartList = new List<chart>();

            foreach (var item in StreameData)
            {
                chart objCart = new chart();

                var MainStream = await dataAccess.GetMainStreamByKey(item.MainStreamId);

                objCart.y = item.Count;
                objCart.indexLabel = MainStream.MainStreamName;

                objChartList.Add(objCart);
            }


            string ChartDetails = JsonConvert.SerializeObject(objChartList);

            objstatastic.ChartJson = ChartDetails;

            return View(objstatastic);
        }
    }
}