﻿using CMS.Dto;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CMS.Util
{
    public class DataAccess:BaseRepository
    {
        public async Task<List<QualificationDto>> GetAllQualification()
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<QualificationDto>("select * from eligibility where DeleteFlag = 0", commandType: CommandType.Text);
                return dataList.ToList();
            }
        }

        public async Task<List<MainStreamDto>> GetAllMainStream()
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<MainStreamDto>("select * from MainStream where DeleteFlag = 0", commandType: CommandType.Text);
                return dataList.ToList();
            }
        }

        public async Task<MainStreamDto> GetMainStreamByKey(int id)
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<MainStreamDto>("select * from MainStream where DeleteFlag = 0 and mid=" + id, commandType: CommandType.Text);
                return dataList.FirstOrDefault();
            }
        }

        public async Task<SubStreamDto> GetSubStreamByKey(int id)
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<SubStreamDto>("select * from SubStream where DeleteFlag = 0 and sid = " + id , commandType: CommandType.Text);
                return dataList.FirstOrDefault();
            }
        }

        public async Task <List<SubStreamDto>> GetSubStreamListByKey(int id)
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<SubStreamDto>("select * from SubStream where DeleteFlag = 0 and MainStreamId = " + id, commandType: CommandType.Text);
                return dataList.ToList();
            }
        }

        public async Task<List<CourseDto>> GetAllCourse()
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<CourseDto>("select * from course where DeleteFlag = 0", commandType: CommandType.Text);
                return dataList.ToList();
            }
        }

        public async Task<List<ContactUsDto>> GetAllContactUsDetails()
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<ContactUsDto>("select * from contactus where isDelete = 0", commandType: CommandType.Text);
                return dataList.ToList();
            }
        }


        public async Task<QualificationDto> GetEligibleCourseByKye(int? id)
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<QualificationDto>("select * from eligibility where eid = "+ id , commandType: CommandType.Text);
                return dataList.FirstOrDefault();
            }
        }

        public async Task<AdminUserDto> GetAdminUseByKey(AdminUserDto adminUser)
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<AdminUserDto>("select * from AdminUser where UserName = '"+adminUser.UserName +"' and Password = '" + adminUser.Password +"'", commandType: CommandType.Text);
                return dataList.FirstOrDefault();
            }
        }

        public async Task<CourseDto> GetCourseByKey(int? id)
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<CourseDto>("select * from course where DeleteFlag = 0 and cid = " + id, commandType: CommandType.Text);
                return dataList.FirstOrDefault();
            }
        }

        public async Task<List<CourseDto>> GetCoursForUser(int mainStreamId,int subStreamId)
        {
            using (connection = Get_Connection())
            {
                if(subStreamId != 0)
                {
                    var dataList = await connection.QueryAsync<CourseDto>("select * from course where DeleteFlag = 0 and MainStreamId = "+ mainStreamId + " and SubStreamId = "+ subStreamId, commandType: CommandType.Text);
                    return dataList.ToList();
                }
                else
                {
                    var dataList = await connection.QueryAsync<CourseDto>("select * from course where DeleteFlag = 0 and MainStreamId = " + mainStreamId, commandType: CommandType.Text);
                    return dataList.ToList();
                }
                
            }
        }

        public async Task<List<UsersDto>> GetAllUser()
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<UsersDto>("select * from users where IsDelete = 0", commandType: CommandType.Text);
                return dataList.ToList();
            }
        }

        public async Task<List<CountByStream>> GetUserCountByStream()
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<CountByStream>("select count(*) as Count,MainStreamId from users u join MainStream m on u.MainStreamId = m.mid where m.DeleteFlag = 0 group by u.MainStreamId", commandType: CommandType.Text);
                return dataList.ToList();
            }
        }

        public async Task<UsersDto> GetUserByKey(int id)
        {
            using (connection = Get_Connection())
            {
                var dataList = await connection.QueryAsync<UsersDto>("select * from users where IsDelete = 0 and id = " + id, commandType: CommandType.Text);
                return dataList.FirstOrDefault();
            }
        }

        public async Task<int> InsertUpdateCourse(CourseDto model)
        {
                try
                {
                    using (connection = Get_Connection())
                    {
                        var param = new DynamicParameters();
                        param.Add("cid", model.cid, DbType.Int32, ParameterDirection.Input);
                        param.Add("MainStreamId", model.MainStreamId, DbType.Int32, ParameterDirection.Input);
                        param.Add("SubStreamId", model.SubStreamId, DbType.Int32, ParameterDirection.Input);
                        param.Add("CourseName", model.CourseName, DbType.String, ParameterDirection.Input);
                        param.Add("EligibleCourseId", model.EligibleCourseId, DbType.String, ParameterDirection.Input);
                        param.Add("DeleteFlag", model.DeleteFlag, DbType.Boolean, ParameterDirection.Input);
                        param.Add("College", model.College, DbType.String, ParameterDirection.Input);
                        param.Add("University", model.University, DbType.String, ParameterDirection.Input);
                        param.Add("duration", model.duration, DbType.String, ParameterDirection.Input);

                    var lastInsertedId = await connection.ExecuteScalarAsync<int>("sp_InsertUpdateCourse", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId;
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
               
        }

        public async Task<int> InsertUpdateEligibleCourse(QualificationDto model)
        {

            try
            {
                using (connection = Get_Connection())
                {
                    var param = new DynamicParameters();
                    param.Add("eid", model.Eid, DbType.Int32, ParameterDirection.Input);
                    param.Add("EligibleCourseName", model.EligibleCourseName, DbType.String, ParameterDirection.Input);
                    param.Add("College", model.College, DbType.String, ParameterDirection.Input);
                    param.Add("University", model.University, DbType.String, ParameterDirection.Input);
                    param.Add("duration", model.duration, DbType.String, ParameterDirection.Input);

                    var lastInsertedId = await connection.ExecuteScalarAsync<int>("sp_InsertUpdateEligibleCourse", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<int> InsertUpdateUser(UsersDto model)
        {

            try
            {
                using (connection = Get_Connection())
                {
                    var param = new DynamicParameters();
                    param.Add("id", model.id, DbType.Int32, ParameterDirection.Input);
                    param.Add("UserName", model.UserName, DbType.String, ParameterDirection.Input);
                    param.Add("Email", model.Email, DbType.String, ParameterDirection.Input);
                    param.Add("Contact", model.Contact, DbType.String, ParameterDirection.Input);
                    param.Add("Gender", model.Gender, DbType.String, ParameterDirection.Input);
                    param.Add("QualificationId", model.QualificationId, DbType.Int32, ParameterDirection.Input);
                    param.Add("MainStreamId",model.MainStreamId, DbType.Int32, ParameterDirection.Input);
                    param.Add("SubStreamId", model.SubStreamId, DbType.Int32, ParameterDirection.Input);
                    param.Add("IsDelete", model.IsDelete, DbType.Boolean, ParameterDirection.Input);

                    var userid = await connection.ExecuteScalarAsync<int>("sp_InsertUpdateUsers", param, commandType: CommandType.StoredProcedure);
                    return userid;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<int> InsertContactus(ContactUsDto model)
        {

            try
            {
                using (connection = Get_Connection())
                {
                    var param = new DynamicParameters();
                    param.Add("id", model.id, DbType.Int32, ParameterDirection.Input);
                    param.Add("firstname", model.firstname, DbType.String, ParameterDirection.Input);
                    param.Add("lastname", model.lastname, DbType.String, ParameterDirection.Input);
                    param.Add("Email", model.Email, DbType.String, ParameterDirection.Input);
                    param.Add("phonenumber", model.phonenumber, DbType.String, ParameterDirection.Input);
                    param.Add("message", model.message, DbType.String, ParameterDirection.Input);

                    var userid = await connection.ExecuteScalarAsync<int>("sp_InsertContactUs", param, commandType: CommandType.StoredProcedure);
                    return userid;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<int> GetDeleteCourseByKye(int id)
        {
          
            try
            {
                using (connection = Get_Connection())
                {
                    var param = new DynamicParameters();
                    param.Add("cid",id, DbType.Int32, ParameterDirection.Input);

                    var deletedCourseId = await connection.QueryAsync<int>("sp_DeleteCourseByKey", param, commandType: CommandType.StoredProcedure);
                    return deletedCourseId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> GetDeletecontactUsByKye(int id)
        {

            try
            {
                using (connection = Get_Connection())
                {
                    var param = new DynamicParameters();
                    param.Add("id", id, DbType.Int32, ParameterDirection.Input);

                    var deletedCourseId = await connection.QueryAsync<int>("sp_DeleteCntactUsByKey", param, commandType: CommandType.StoredProcedure);
                    return deletedCourseId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> GetDeleteUserByKye(int id)
        {

            try
            {
                using (connection = Get_Connection())
                {
                    var param = new DynamicParameters();
                    param.Add("id", id, DbType.Int32, ParameterDirection.Input);

                    var deletedUserId = await connection.QueryAsync<int>("sp_DeleteUserByKey", param, commandType: CommandType.StoredProcedure);
                    return deletedUserId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}