using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.HRMS.DATA;
using OOP2.HRMS.FRAMEWORK;

namespace OOP2.HRMS.REPO
{
    public class EmployeeInfoRepo
    {
        HRMSContext context = new HRMSContext();

        public Result<List<EmployeeInfo>> GetAll(string key)
        {
            var result = new Result<List<EmployeeInfo>>();
            try
            {
                IQueryable<EmployeeInfo> query = context.EmployeeInfoes;

                if (ValidationHelper.IsIntValid(key))
                {
                    int id = Int32.Parse(key);
                    query = query.Where(q => q.EmpID == id);
                }
                else if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.EmpName.Contains(key));
                }

                result.Data = query.ToList();
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<EmployeeInfo> GetById(int id, string key)
        {
            var result = new Result<EmployeeInfo>();
            try
            {
                var query = context.EmployeeInfoes.FirstOrDefault(c => c.EmpID == id);

                result.Data = query;
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }
        public Result<EmployeeInfo> Save(EmployeeInfo employeeInfo)
        {
            var result = new Result<EmployeeInfo>();
            bool isNew = false;
            try
            {
                var objToSave = context.EmployeeInfoes.FirstOrDefault(q => q.EmpID == employeeInfo.EmpID);

                if (objToSave == null)
                {
                    objToSave = new EmployeeInfo();
                    context.EmployeeInfoes.Add(objToSave);
                    isNew = true;
                }

                objToSave.EmpID = employeeInfo.EmpID;
                objToSave.Address = employeeInfo.Address;
                objToSave.BloodGroup = employeeInfo.BloodGroup;
                objToSave.Contact = employeeInfo.Contact;
                objToSave.DOB = employeeInfo.DOB;
                objToSave.DeptID = employeeInfo.DeptID;
                objToSave.DesigID = employeeInfo.DesigID;
                objToSave.MaritalStatus = employeeInfo.MaritalStatus;
                objToSave.Email = employeeInfo.Email;
                objToSave.NID = employeeInfo.NID;
                objToSave.Nationality = employeeInfo.Nationality;
                objToSave.JoiningDate = employeeInfo.JoiningDate;
                objToSave.HireDate = employeeInfo.HireDate;
                objToSave.EmpName = employeeInfo.EmpName;
                objToSave.Gender = employeeInfo.Gender;
                objToSave.Religion = employeeInfo.Religion;
                objToSave.Status = employeeInfo.Status;

                if (!isNew)
                {
                    objToSave.UserAccount.Password = employeeInfo.UserAccount.Password;
                    objToSave.UserAccount.UserType = employeeInfo.UserAccount.UserType;
                }



                context.SaveChanges();

                if (isNew)
                {
                    var userAccount = new UserAccount();
                    userAccount.UserID = objToSave.EmpID;
                    //userAccount.Password = new Random().Next(1000, 9999).ToString();
                    userAccount.Password = employeeInfo.UserAccount.Password;
                    userAccount.UserType = employeeInfo.UserAccount.UserType;

                    context.UserAccounts.Add(userAccount);

                    context.SaveChanges();

                    result.Message = "Your Password is " + userAccount.Password;
                }

                result.Data = context.EmployeeInfoes.FirstOrDefault(q => q.EmpID == objToSave.EmpID);
                if (result.Data != null && isNew)
                {
                    result.Data.Department = context.Departments.FirstOrDefault(d => d.DeptID == result.Data.DeptID);
                }

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<bool> Delete(int id)
        {
            var result = new Result<bool>();
            try
            {
                var objToDelete = context.EmployeeInfoes.FirstOrDefault(q => q.EmpID == id);
                var objUserToDelete = context.UserAccounts.FirstOrDefault(q => q.UserID == id);

                if (objToDelete == null || objUserToDelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid ID";

                    return result;
                }

                context.UserAccounts.Remove(objUserToDelete);
                context.EmployeeInfoes.Remove(objToDelete);


                context.SaveChanges();
                result.Data = true;

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }


            return result;
        }

        private bool IsValidToSave(EmployeeInfo obj, Result<EmployeeInfo> result)
        {
            if (!ValidationHelper.IsNameValid(obj.EmpName))
            {
                result.HasError = true;
                result.Message = "Invalid Name";
                return false;
            }

            if (!ValidationHelper.IsStringValid(obj.Address))
            {
                result.HasError = true;
                result.Message = "Invalid Address";
                return false;
            }

            if (!ValidationHelper.IsEmailValid(obj.Email))
            {
                result.HasError = true;
                result.Message = "Invalid Email";
                return false;
            }

            if (!ValidationHelper.IsNumberValid(obj.NID))
            {
                result.HasError = true;
                result.Message = "Invalid NID";
                return false;
            }

            if (!ValidationHelper.IsInputStringValid(obj.Nationality))
            {
                result.HasError = true;
                result.Message = "Invalid Nationality";
                return false;
            }

            if (context.EmployeeInfoes.AsNoTracking().Any(q => q.EmpName == obj.EmpName && q.EmpID != obj.EmpID))
            {
                result.HasError = true;
                result.Message = "Contact Exits";
                return false;
            }
            return true;
        }
    }
}