using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TaskTracker.Controller;
using TaskTracker.Model;
using TaskTracker.Util;

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
            RefreshPeople();
        }

        private void RefreshTasks()
        {
           
            try
            {
                List<Task> tasks = TaskDAO.GetAll();
                RefreshTasks(tasks);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tbFilter.Clear();
        }

        private void RefreshTasks(List<Task> tasks)
        {
            dgTask.Rows.Clear();
            foreach (Task t in tasks)
            {
                dgTask.Rows.Add(new string[] { t.Name, t.Description, t.StartDate.ToString("dd/MM/yyyy"), t.DueDate.ToString("dd/MM/yyyy"), t.ResposiblePerson.Name, Type.ConvertStatusToString(t.Status) });
            }
        }

        private void RefreshPeople()
        {
            try
            {
                List<Person> list = PersonDAO.GetAll();
                RefreshPeople(list);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tbFilter.Clear();
        }

        private void RefreshPeople(List<Person> list)
        {
            dgPerson.Rows.Clear();
            foreach (Person p in list)
            {
                dgPerson.Rows.Add(new string[] { p.Name, p.Birthday.ToString("dd/MM/yyyy"), p.Email });
            }
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            FormFactory.Callback cb;
            Entry entry;
            if (mode == Type.Mode.Task)
            {
                entry = new Task();
                cb = new FormFactory.Callback(RefreshTasks);
            }
            else
            {
                entry = new Person();
                cb = new FormFactory.Callback(RefreshPeople);
            }
            FormFactory.Instance.OpenWindow(mode, Type.Action.Add, cb, entry);
        }

        private void btnUpdate_Click(object sender, System.EventArgs e)
        {
            if (mode == Type.Mode.Task)
            {
                if (dgTask.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Task is not selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataGridViewRow selectedRow = dgTask.SelectedRows[0];
                Task task = new Task(selectedRow.Cells["colName"].Value.ToString(),
                                     selectedRow.Cells["colDescription"].Value.ToString(),
                                     System.DateTime.ParseExact(selectedRow.Cells["colStartDate"].Value.ToString(), "dd/MM/yyyy", null),
                                     System.DateTime.ParseExact(selectedRow.Cells["colDueDate"].Value.ToString(), "dd/MM/yyyy", null),
                                     PersonDAO.GetByName(selectedRow.Cells["colPerson"].Value.ToString()),
                                     Type.ConvertStringToStatus(selectedRow.Cells["colStatus"].Value.ToString()));
                FormFactory.Callback cb = new FormFactory.Callback(RefreshTasks);
                FormFactory.Instance.OpenWindow(mode, Type.Action.Update, cb, task);
            } else
            {
                if (dgPerson.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Person is not selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataGridViewRow selectedRow = dgPerson.SelectedRows[0];
                Person person = new Person(selectedRow.Cells["colPName"].Value.ToString(),
                                     System.DateTime.ParseExact(selectedRow.Cells["colBirthday"].Value.ToString(), "dd/MM/yyyy", null),
                                     selectedRow.Cells["colEmail"].Value.ToString());
                FormFactory.Callback cb = new FormFactory.Callback(RefreshPeople);
                FormFactory.Instance.OpenWindow(mode, Type.Action.Update, cb, person);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                mode = Type.Mode.Task;
                RefreshTasks();
            } else if (tabControl1.SelectedIndex == 1)
            {
                mode = Type.Mode.Person;
                RefreshPeople();
            }
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (mode == Type.Mode.Task)
            {
                if (dgTask.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Task is not selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DialogResult res = MessageBox.Show("Are your sure?", "Deleting task", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = dgTask.SelectedRows[0];
                    TaskDAO.DeleteByName(selectedRow.Cells["colName"].Value.ToString());
                    RefreshTasks();
                }
                
            }
            else
            {
                if (dgPerson.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Person is not selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DialogResult res = MessageBox.Show("Are your sure?", "Deleting person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = dgPerson.SelectedRows[0];
                    string name = selectedRow.Cells["colPName"].Value.ToString();
                    if (TaskDAO.PersonHasTasks(name))
                    {
                        MessageBox.Show("Could not delete person. There are still assigned task(s).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } else
                    {
                        PersonDAO.DeleteByName(name);
                        RefreshPeople();
                    }
                }

            }
        }

        private void btnFilter_Click(object sender, System.EventArgs e)
        {
            if (mode == Type.Mode.Task)
            {
                List<Task> tasks = TaskDAO.FilterTasks(tbFilter.Text.Trim());
                RefreshTasks(tasks);
            } else
            {
                List<Person> people = PersonDAO.FilterPerson(tbFilter.Text.Trim());
                RefreshPeople(people);
            }
        }

        private void tbFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void dgPerson_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (mode == Type.Mode.Person)
            {
                if (dgPerson.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Person is not selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataGridViewRow selectedRow = dgPerson.SelectedRows[0];
                string personName = selectedRow.Cells["colPName"].Value.ToString();
                List<Task> tasks = TaskDAO.GetByResponsiblePerson(personName);
                if (tasks.Count == 0)
                {
                    MessageBox.Show(personName + " does not have assigned tasks.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                } else
                {
                    FrmPersonTasks frm = new FrmPersonTasks("Tasks from " + personName, tasks);
                    frm.ShowDialog();
                }
            }
        }
    }
}
