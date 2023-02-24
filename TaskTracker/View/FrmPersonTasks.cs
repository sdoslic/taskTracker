using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TaskTracker.Model;

namespace TaskTracker.View
{
    public partial class FrmPersonTasks : Form
    {
        public FrmPersonTasks(string name, List<Task> tasks)
        {
            InitializeComponent();
            Text = name;
            RefreshTasks(tasks);
        }

        private void RefreshTasks(List<Task> tasks)
        {
            dgTasks.Rows.Clear();
            try
            {
                foreach (Task t in tasks)
                {
                    dgTasks.Rows.Add(new string[] { t.Name, t.Description, t.StartDate.ToString("dd/MM/yyyy"), t.DueDate.ToString("dd/MM/yyyy"), t.ResposiblePerson.Name, Util.Type.ConvertStatusToString(t.Status) });
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
