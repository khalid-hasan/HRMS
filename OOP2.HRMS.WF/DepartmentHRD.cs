using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using OOP2.HRMS.DATA;
using OOP2.HRMS.REPO;

namespace OOP2.HRMS.WF
{
    public partial class DepartmentHRD : MetroForm
    {
        DepartmentRepo deptrepo = new DepartmentRepo();

        private Department SelectedData { get; set; }
        private List<Department> CurrentData = new List<Department>();
        private bool EmptyTextBox = false;
        public DepartmentHRD()
        {
            InitializeComponent();
        }

        private void DepartmentHRD_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitialMode();
            }
            catch (Exception exception)
            {
                MetroMessageBox.Show(this, exception.Message);
            }
        }

        private void InitialMode()
        {
            this.LoadInfo();
        }

        private void LoadInfo()
        {
            var result = deptrepo.GetAll("");

            if (result.HasError)
            {
                MetroMessageBox.Show(this, result.Message);
                return;
            }

            CurrentData = result.Data.ToList();

            if (CurrentData.Count > 0)
            {
                SelectedData = CurrentData[0];

            }
            else
            {
                SelectedData = new Department();
            }

            this.PopulateData();
            this.DisableEdit();
            this.RefreshDGV();
        }

        private void RefreshDGV()
        {
            dgvDeptHRD.AutoGenerateColumns = false;
            dgvDeptHRD.DataSource = CurrentData.ToList();
            dgvDeptHRD.Refresh();
            dgvDeptHRD.ClearSelection();

            for (int i = 0; i < dgvDeptHRD.Rows.Count; i++)
            {
                if (dgvDeptHRD.Rows[i].Cells[0].Value.ToString() == SelectedData.DeptID.ToString())
                {
                    dgvDeptHRD.Rows[i].Selected = true;
                    break;
                }
            }

            this.DisableEdit();
        }

        private void PopulateData()
        {
            txtBoxIDDeptHRD.Text = SelectedData.DeptID == 0 ? "Auto Generated" : SelectedData.DeptID.ToString();
            txtBoxNameDeptHRD.Text = SelectedData.DeptName;
        }

        private void DisableEdit()
        {
            txtBoxIDDeptHRD.ReadOnly = true;
            txtBoxNameDeptHRD.ReadOnly = true;
        }

        private void EnableEdit()
        {
            txtBoxNameDeptHRD.ReadOnly = false;
        }
        private void NewMode()
        {
            dgvDeptHRD.ClearSelection();
            SelectedData = new Department();
            this.PopulateData();
        }

        private void btnNewDeptHRD_Click(object sender, EventArgs e)
        {
            this.EnableEdit();
            this.NewMode();
        }

        private void dgvDeptHRD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Int32.Parse(dgvDeptHRD.Rows[e.RowIndex].Cells[0].Value.ToString());
                SelectedData = CurrentData.FirstOrDefault(q => q.DeptID == id) ?? new Department();

                this.PopulateData();
            }
        }

        private void btnEditDeptHRD_Click(object sender, EventArgs e)
        {
            this.EnableEdit();
        }

        private void btnDeleteDeptHRD_Click(object sender, EventArgs e)
        {
            if (SelectedData.DeptID == 0)
            {
                MetroMessageBox.Show(this, "No Information Selected");
                return;
            }

            if (MetroMessageBox.Show(this, "Are you sure", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }


            var result = deptrepo.Delete(SelectedData.DeptID);

            if (result.HasError)
            {
                MetroMessageBox.Show(this, result.Message);
                return;
            }

            CurrentData.Remove(SelectedData);
            this.NewMode();
            this.RefreshDGV();
        }

        private void FillDate()
        {

            SelectedData.DeptName = txtBoxNameDeptHRD.Text;


        }

        private void btnAddDeptHRD_Click(object sender, EventArgs e)
        {
            this.FillDate();
            /* if (EmptyTextBox)
             {
                 MetroMessageBox.Show(this, "Please Fill up the Text Box First");

                 return;
             }*/


            bool NewData = SelectedData.DeptID == 0;
            var result = deptrepo.Save(SelectedData);

            if (result.HasError)
            {
                MetroMessageBox.Show(this, result.Message);
                return;
            }

            if (NewData)
            {
                CurrentData.Add(result.Data);

            }
            else
            {

                var objChanged = CurrentData.FirstOrDefault(q => q.DeptID == result.Data.DeptID);

                if (objChanged != null)
                {
                    objChanged.DeptID = result.Data.DeptID;
                    objChanged.DeptName = result.Data.DeptName;

                }
            }

            SelectedData = result.Data;

            MetroMessageBox.Show(this, "Department Added");

            this.PopulateData();
            this.RefreshDGV();
        }

        private void DepartmentHRD_FormClosing(object sender, FormClosingEventArgs e)
        {
            HomeHRD home = new HomeHRD();
            home.Show();
            this.Hide();
        }
    }
}
