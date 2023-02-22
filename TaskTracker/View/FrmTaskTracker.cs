using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TaskTracker.Model;

namespace TaskTracker.View
{
    public partial class FrmTaskTracker : Form
    {
        private Type.Mode mode;
        public FrmTaskTracker()
        {
            InitializeComponent();
            mode = Type.Mode.Task;
            RefreshTasks();
            RefreshPersons();
        }

        private void RefreshTasks()
        {
            dgTask.Rows.Clear();
            try
            {
                List<Task> tasks = TaskDAO.GetAll();
                foreach (Task t in tasks)
                {
                    dgTask.Rows.Add(new string[] { t.Name, t.Description, t.StartDate.ToString("dd/MM/yyyy"), t.DueDate.ToString("dd/MM/yyyy"), t.ResposiblePerson.Name, Type.ConvertStatusToString(t.Status) });
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshPersons()
        {
            dgPerson.Rows.Clear();
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            if (mode == Type.Mode.Task)
            {
                FrmTask frm = new FrmTask("Adding task", Type.Action.Add);
                frm.ShowDialog();
                RefreshTasks();
            }
            
        }

        private void btnUpdate_Click(object sender, System.EventArgs e)
        {
            if (mode == Type.Mode.Task)
            {
                DataGridViewRow selectedRow = dgTask.SelectedRows[0];
                Task task = new Task(selectedRow.Cells["colName"].Value.ToString(),
                                     selectedRow.Cells["colDescription"].Value.ToString(),
                                     System.DateTime.ParseExact(selectedRow.Cells["colStartDate"].Value.ToString(), "dd/MM/yyyy", null),
                                     System.DateTime.ParseExact(selectedRow.Cells["colDueDate"].Value.ToString(), "dd/MM/yyyy", null),
                                     PersonDAO.GetByName(selectedRow.Cells["colPerson"].Value.ToString()),
                                     Type.ConvertStringToStatus(selectedRow.Cells["colStatus"].Value.ToString()));
                FrmTask frm = new FrmTask("Updating task", Type.Action.Update, task);
                frm.ShowDialog();
                RefreshTasks();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                mode = Type.Mode.Task;
            } else if (tabControl1.SelectedIndex == 1)
            {
                mode = Type.Mode.Person;
            }
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (mode == Type.Mode.Task)
            {
                DialogResult res = MessageBox.Show("Are your sure?", "Deleting task", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = dgTask.SelectedRows[0];
                    TaskDAO.DeleteByName(selectedRow.Cells["colName"].Value.ToString());
                    RefreshTasks();
                }
                
            }
        }
    }
}
