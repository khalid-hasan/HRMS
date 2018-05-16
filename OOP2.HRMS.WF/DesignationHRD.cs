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
    public partial class DesignationHRD : MetroForm
    {
        DesignationRepo desigrepo = new DesignationRepo();

        private Designation SelectedData { get; set; }
        private List<Designation> CurrentData = new List<Designation>();
        private bool EmptyTextBox = false;
        public DesignationHRD()
        {
            InitializeComponent();
        }

        private void DesignationHRD_Load(object sender, EventArgs e)
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
            var result = desigrepo.GetAll("");

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
                SelectedData = new Designation();
            }

            this.PopulateData();
            this.DisableEdit();
            this.RefreshDGV();
        }

        private void RefreshDGV()
        {
            dgvDesigHRD.AutoGenerateColumns = false;
            dgvDesigHRD.DataSource = CurrentData.ToList();
            dgvDesigHRD.Refresh();
            dgvDesigHRD.ClearSelection();

            for (int i = 0; i < dgvDesigHRD.Rows.Count; i++)
            {
                if (dgvDesigHRD.Rows[i].Cells[0].Value.ToString() == SelectedData.DesigID.ToString())
                {
                    dgvDesigHRD.Rows[i].Selected = true;
                    break;
                }
            }

            this.DisableEdit();
        }

        private void PopulateData()
        {
            txtBoxIDDesigHRD.Text = SelectedData.DesigID == 0 ? "Auto Generated" : SelectedData.DesigID.ToString();
            txtBoxNameDesigHRD.Text = SelectedData.DesigName;
        }

        private void DisableEdit()
        {
            txtBoxIDDesigHRD.ReadOnly = true;
            txtBoxNameDesigHRD.ReadOnly = true;
        }

        private void EnableEdit()
        {
            txtBoxNameDesigHRD.ReadOnly = false;
        }

        private void NewMode()
        {
            dgvDesigHRD.ClearSelection();
            SelectedData = new Designation();
            this.PopulateData();
        }

        private void btnNewDesigHRD_Click(object sender, EventArgs e)
        {
            this.EnableEdit();
            this.NewMode();
        }

        private void dgvDesigHRD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Int32.Parse(dgvDesigHRD.Rows[e.RowIndex].Cells[0].Value.ToString());
                SelectedData = CurrentData.FirstOrDefault(q => q.DesigID == id) ?? new Designation();

                this.PopulateData();
            }
        }

        private void btnEditDesigHRD_Click(object sender, EventArgs e)
        {
            this.EnableEdit();
        }

        private void btnDeleteDesigHRD_Click(object sender, EventArgs e)
        {
            if (SelectedData.DesigID == 0)
            {
                MetroMessageBox.Show(this, "No Information Selected");
                return;
            }

            if (MetroMessageBox.Show(this, "Are you sure", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }


            var result = desigrepo.Delete(SelectedData.DesigID);

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

            SelectedData.DesigName = txtBoxNameDesigHRD.Text;


        }

        private void btnAddDesigHRD_Click(object sender, EventArgs e)
        {
            this.FillDate();
            /*if (EmptyTextBox)
            {
                MetroMessageBox.Show(this, "Please Fill up the Text Box First");

                return;
            }*/


            bool NewData = SelectedData.DesigID == 0;
            var result = desigrepo.Save(SelectedData);

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

                var objChanged = CurrentData.FirstOrDefault(q => q.DesigID == result.Data.DesigID);

                if (objChanged != null)
                {
                    objChanged.DesigID = result.Data.DesigID;
                    objChanged.DesigName = result.Data.DesigName;

                }
            }

            SelectedData = result.Data;

            MetroMessageBox.Show(this, "Designation Added");

            this.PopulateData();
            this.RefreshDGV();
        }

        private void DesignationHRD_FormClosing(object sender, FormClosingEventArgs e)
        {
            HomeHRD home = new HomeHRD();
            home.Show();
            this.Hide();
        }
    }
}
