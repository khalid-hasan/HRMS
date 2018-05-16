using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.HRMS.DATA;
using OOP2.HRMS.FRAMEWORK;

namespace OOP2.HRMS.REPO
{
    public class DesignationRepo
    {

        HRMSContext context = new HRMSContext();

        public Result<List<Designation>> GetAll(string key)
        {
            var result = new Result<List<Designation>>();
            try
            {
                IQueryable<Designation> query = context.Designations;

                /*if (ValidationHelper.IsIntValid(key))
                {
                    int id = Int32.Parse(key);
                    query = query.Where(q => q.DesigID == id);
                }
                else if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.DesigName.Contains(key));
                }*/

                result.Data = query.ToList();
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
                var objToDelete = context.Designations.FirstOrDefault(q => q.DesigID == id);


                if (objToDelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid ID";

                    return result;
                }

                //context.UserAccounts.Remove(objUserToDelete);
                context.Designations.Remove(objToDelete);

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

        public Result<Designation> Save(Designation designationInfo)
        {
            var result = new Result<Designation>();

            try
            {
                var objToSave = context.Designations.FirstOrDefault(q => q.DesigID == designationInfo.DesigID);

                if (objToSave == null)
                {
                    objToSave = new Designation();
                    context.Designations.Add(objToSave);

                }

                objToSave.DesigID = designationInfo.DesigID;
                objToSave.DesigName = designationInfo.DesigName;

                context.SaveChanges();

                result.Data = context.Designations.FirstOrDefault(q => q.DesigID == objToSave.DesigID);

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }
    }
}
