using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.HRMS.DATA;
using OOP2.HRMS.FRAMEWORK;

namespace OOP2.HRMS.REPO
{
    public class PayslipRepo
    {
        HRMSContext context = new HRMSContext();

        public Result<List<Payslip>> GetAll(string key)
        {
            var result = new Result<List<Payslip>>();
            try
            {
                IQueryable<Payslip> query = context.Payslips;

                if (ValidationHelper.IsIntValid(key))
                {
                    int id = Int32.Parse(key);
                    query = query.Where(q => q.ID == id);
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

        public Result<List<Payslip>> GetById(int id, string key)
        {
            var result = new Result<List<Payslip>>();
            try
            {
                IQueryable<Payslip> query = context.Payslips.Where(c => c.EmpID == id && c.StatusID== "Post");

                result.Data = query.ToList();
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<Payslip> Save(Payslip payslip)
        {
            var result = new Result<Payslip>();
            var dbContext = new HRMSContext();
            bool isNew = false;
            try
            {
                var objToSave = dbContext.Payslips.FirstOrDefault(q => q.ID == payslip.ID);

                if (objToSave == null)
                {
                    objToSave = new Payslip();
                    dbContext.Payslips.Add(objToSave);
                    isNew = true;
                }

                objToSave.EmpID = payslip.EmpID;
                objToSave.PayrollID = payslip.PayrollID;
                //objToSave.EmpSalaryID = payslip.EmpSalaryID;
                objToSave.BasicSalary = payslip.BasicSalary;
                objToSave.HouseAllowance = payslip.HouseAllowance;
                objToSave.Conveyance = payslip.Conveyance;
                objToSave.Medical = payslip.Medical;
                objToSave.Addition = payslip.Addition;
                objToSave.Deduction = payslip.Deduction;
                objToSave.Date = payslip.Date;
                objToSave.StatusID= payslip.StatusID;

                if (!IsValidToSave(objToSave, result))
                {
                    return result;
                }
                dbContext.SaveChanges();
                result.Data = context.Payslips.FirstOrDefault(c => c.ID == objToSave.ID);
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
                var objToDelete = context.Payslips.FirstOrDefault(q => q.ID == id);

                if (objToDelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid ID";

                    return result;
                }

                context.Payslips.Remove(objToDelete);

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

        private bool IsValidToSave(Payslip obj, Result<Payslip> result)
        {
            if (context.Payslips.Any(p => p.EmpID == obj.EmpID && p.PayrollID == obj.PayrollID && p.ID != obj.ID))
            {
                result.HasError = true;
                result.Message = "Payslip Already Exists";
                return false;
            }
            return true;
        }
    }
}
