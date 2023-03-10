using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TaskTracker.Model;

namespace TaskTracker.View
{
    public partial class FrmChoosePerson : Form
    {
        public Person SelectedPerson { get; private set; }
        public FrmChoosePerson()
        {
            InitializeComponent();
            SelectedPerson = null;
            try
            {
                List<Person> persons = PersonDAO.GetAll();
                foreach (Person p in persons)
                {
                    dgPerson.Rows.Add(new string[] { p.Name, p.Birthday.ToString("dd/MM/yyyy"), p.Email });
                }
            }
            catch (FileNotFoundException ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgPerson.SelectedRows.Count == 0)
            {
                MessageBox.Show("Person is not selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataGridViewRow selectedRow = dgPerson.SelectedRows[0];
            SelectedPerson = new Person(selectedRow.Cells["colName"].Value.ToString(),
                                        System.DateTime.ParseExact(selectedRow.Cells["colBirthday"].Value.ToString(), "dd/MM/yyyy", null),
                                        selectedRow.Cells["colEmail"].Value.ToString());
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
