using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Business.AccessHelper;
using Business.Models;
using Util;

namespace Business.Dal
{
    public class StudentDal
    {


        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public static string Insert(Student student)
        {
            var strError = Validator(student);
            if (strError != "") return strError;
            if (!InsertStudent(student))
                strError = "保存数据失败，请稍后重试！";
                return strError;
        }

        /// <summary>
        /// 删除学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteById(int id)
        {
            string sql = "delete from Student where id=?";
            OleDbParameter[] parameters = new OleDbParameter[1];
            parameters[0] = new OleDbParameter("@id", OleDbType.Integer) {Value = id};
            return AccessHelper.AccessDbUtil.ExecuteNonQuery(sql, parameters)==1; 
        }

        /// <summary>
        /// 软删除学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool SoftDeleteById(int id)
        {
            string sql = "update Student set IsDeleted=1 where id=?";
            OleDbParameter[] parameters = new OleDbParameter[1];
            parameters[0] = new OleDbParameter("@id", OleDbType.Integer) { Value = id };
            return AccessHelper.AccessDbUtil.ExecuteNonQuery(sql, parameters) == 1;
        }


        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public static string Update(Student student)
        {
            var strError = Validator(student);
            if (strError != "") return strError;
            if (!UpdateStudent(student))
                strError = "更新数据失败，请稍后重试！";
            return strError; 
        }

        /// <summary>
        /// 获取学生信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static Student Get(string where)
        {
            var sql = @"SELECT   Id
		                ,RealName
		                ,Age
		                ,Sex
		                ,Class
		                ,Grade
		                ,ParentsName
		                ,ContantNumber
		                ,DanceGradeId
		                ,LastFeeDate
		                ,LastFeeAoumnt
		                ,ExpireDate
		                ,TotalFeeAmount
		                ,CreationTime
		                ,IsDeleted
	                FROM Student
	                WHERE 1=1";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sql += where;
            }
            var ds = AccessHelper.AccessDbUtil.ExecuteQuery(sql);
            return RowToEntity(ds.Tables["ds"].Rows[0]); 
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="curPageIndex"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static Page GetStudents(int curPageIndex, string where)
        {
            var sqlQuery = "select * from Student";
            var sqlCount = "select count(*) from Student";
            var sqlOrder = " order by name";
            if (where != null)
            {
                string sqlCondition = where;
                sqlQuery += sqlCondition + sqlOrder;
                sqlCount += sqlCondition;
            }
            int totalRecord = AccessDbUtil.ExecuteScalar(sqlCount);
            var page = new Page(totalRecord, AccessPageUtil.PageSize);
            if (curPageIndex >= page.TotalPage) curPageIndex = page.TotalPage - 1;
            if (curPageIndex < 0) curPageIndex = 0;
            page.CurPageIndex = curPageIndex;

            var data = AccessPageUtil.Query(sqlQuery, curPageIndex, totalRecord);
            var ls = new List<Student>();
            foreach (DataRow row in data.Tables["ds"].Rows)
            {
                ls.Add(RowToEntity(row));
            }
            page.ValueList = ls;
            return page;
        }

        /// <summary>
        /// 行转实体
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static Student RowToEntity(DataRow row)
        {
            var student=new Student()
            {
                Id = row["id"].ToInt(),
                RealName = row["RealName"].ToStr(), 
                Age = row["RealName"].ToInt(),
                Sex = Enum.GetInstance<SexType>(row["Sex"]),
                Class = row["Class"].ToStr(),
                Grade = row["Grade"].ToStr(),
                ParentsName = row["ParentsName"].ToStr(),
                ContantNumber = row["ContantNumber"].ToStr(),
                DanceGradeId = row["DanceGradeId"].ToInt(),
                LastFeeDate = row["LastFeeDate"].ToDate(),
                LastFeeAoumnt = row["LastFeeAoumnt"].ToDecimal(),
                ExpireDate = row["ExpireDate"].ToDate(),
                TotalFeeAmount = row["TotalFeeAmount"].ToDecimal(),
                CreationTime = row["RealName"].ToDate(),
                IsDeleted = row["RealName"].ToStr()=="1"
            };
            return student;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        private static string Validator(Student student)
        {
            var strError = "";
            if (string.IsNullOrWhiteSpace(student.RealName))
            {
                strError += "学生姓名不能为空！";
            }
            //            else
            //            {
            //                var studentEntity = Get(" and RealName='"+ student.RealName.Trim() + "'");
            //                if (studentEntity!=null)
            //                {
            //                    strError += "学生姓名！";
            //                }
            //            }
            if (string.IsNullOrWhiteSpace(student.RealName))
            {
                strError += "学生姓名不能为空！";
            }
            if (string.IsNullOrWhiteSpace(student.ParentsName))
            {
                strError += "家长姓名不能为空！";
            }
            if (string.IsNullOrWhiteSpace(student.ContantNumber))
            {
                strError += "家长联系电话不能为空！";
            }
            return strError;
        }

        /// <summary>
        /// 新增学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        private static bool InsertStudent(Student student)
        {
            var sql =
                @"INSERT INTO Student (RealName,Age,Sex,Class,Grade,ParentsName,ContantNumber,DanceGradeId) VALUES(?,?,?,?,?,?,?,?)";
            var parameters = new OleDbParameter[8];
            parameters[0] = new OleDbParameter("@RealName", OleDbType.VarChar, 50) { Value = student.RealName.Trim() };
            parameters[1] = new OleDbParameter("@Age", OleDbType.Integer) { Value = student.Age };
            parameters[2] = new OleDbParameter("@Sex", OleDbType.Integer) { Value = student.Sex.Value() };
            parameters[3] = new OleDbParameter("@Class", OleDbType.VarChar, 25) { Value = student.Class.Trim() };
            parameters[4] = new OleDbParameter("@Grade", OleDbType.VarChar, 50) { Value = student.Grade.Trim() };
            parameters[5] = new OleDbParameter("@ParentsName", OleDbType.VarChar, 50) { Value = student.ParentsName.Trim() };
            parameters[6] = new OleDbParameter("@ContantNumber", OleDbType.VarChar, 150) { Value = student.ContantNumber.Trim() };
            parameters[7] = new OleDbParameter("@DanceGradeId", OleDbType.Integer) { Value = student.DanceGradeId };
            return AccessDbUtil.ExecuteInsert(sql, parameters) == 1;
        }

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        private static bool UpdateStudent(Student student)
        {

            var sql = @"UPDATE Student
	                SET RealName = ?
		                ,Age = ?
		                ,Sex =?
		                ,Class = ?
		                ,Grade = ?
		                ,ParentsName = ?
		                ,ContantNumber = ?
		                ,DanceGradeId = ?
	                WHERE Id=?";
            var parameters = new OleDbParameter[9];
            parameters[0] = new OleDbParameter("@RealName", OleDbType.VarChar, 50) { Value = student.RealName };
            parameters[1] = new OleDbParameter("@Age", OleDbType.Integer) { Value = student.Age };
            parameters[2] = new OleDbParameter("@Sex", OleDbType.Integer) { Value = (int)student.Sex };
            parameters[3] = new OleDbParameter("@Class", OleDbType.VarChar, 25) { Value = student.Class };
            parameters[4] = new OleDbParameter("@Grade", OleDbType.VarChar, 50) { Value = student.Grade };
            parameters[5] = new OleDbParameter("@ParentsName", OleDbType.VarChar, 50) { Value = student.ParentsName };
            parameters[6] = new OleDbParameter("@ContantNumber", OleDbType.VarChar, 150) { Value = student.ContantNumber };
            parameters[7] = new OleDbParameter("@DanceGradeId", OleDbType.Integer) { Value = student.DanceGradeId };
            parameters[8] = new OleDbParameter("@id", OleDbType.Integer) { Value = student.Id };

            return AccessDbUtil.ExecuteNonQuery(sql, parameters) == 1;

        }


    }
}