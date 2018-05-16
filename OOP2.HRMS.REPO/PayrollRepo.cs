using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.HRMS.DATA;
using OOP2.HRMS.FRAMEWORK;

namespace OOP2.HRMS.REPO
{
    public class PayrollRepo
    {
        HRMSContext context = new HRMSContext();

        public Result<List<Payroll>> GetAll(string key)
        {
            var result = new Result<List<Payroll>>();
            try
            {
                IQueryable<Payroll> query = context.Payrolls;

                if (ValidationHelper.IsIntValid(key))
                {
                    int id = Int32.Parse(key);
                    query = query.Where(q => q.ID == id);
                }
                else if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Name.Contains(key));
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

        public Result<Payroll> GetById(int id, string key)
        {
            var result = new Result<Payroll>();
            try
            {
                var query = context.Payrolls.FirstOrDefault(c => c.ID == id);

                result.Data = query;
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }
        public Result<Payroll> Save(Payroll payroll)
        {
            var result = new Result<Payroll>();
            bool isNew = false;
            try
            {
                var objToSave = context.Payrolls.FirstOrDefault(q => q.ID == payroll.ID);

                if (objToSave == null)
                {
                    objToSave = new Payroll();
                    context.Payrolls.Add(objToSave);
                    isNew = true;
                }

                objToSave.Name = payroll.Name;
                objToSave.Month = payroll.Month;
                objToSave.Year = payroll.Year;

                if (!IsValidToSave(objToSave, result))
                {
                    return result;
                }
                context.SaveChanges();
                result.Data = context.Payrolls.FirstOrDefault(c => c.ID == objToSave.ID);
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
                var objToDelete = context.Payrolls.FirstOrDefault(q => q.ID == id);

                if (objToDelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid ID";

                    return result;
                }

                context.Payrolls.Remove(objToDelete);

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

        private bool IsValidToSave(Payroll obj, Result<Payroll> result)
        {
            return true;
        }
    }
}
