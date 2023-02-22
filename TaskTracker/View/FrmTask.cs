using System.Windows.Forms;
using TaskTracker.Model;

namespace TaskTracker.View
{
    public partial class FrmTask : Form
    {
        private Type.Action currentAction;
        private Task currentTask;

        public FrmTask(string name, Type.Action action)
        {
            InitializeComponent();
            Text = name;
            currentAction = action;
        }

        public FrmTask(string name, Type.Action action, Task t)
        {
            InitializeComponent();
            Text = name;
            currentAction = action;
            currentTask = t;
            UpdatedFields();
        }

        private void UpdatedFields()
        {
            tbName.Text = currentTask.Name;
            tbDescription.Text = currentTask.Description;
            dtpStartDate.Value = currentTask.StartDate;
            dtpDueDate.Value = currentTask.DueDate;
            if (currentTask.ResposiblePerson != null)
            {
                tbPersonName.Text = currentTask.ResposiblePerson.Name;
            }
            cbStatus.SelectedItem = Type.ConvertStatusToString(currentTask.Status);
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string oldName = currentTask != null ? currentTask.Name : "";
            currentTask = new Task(tbName.Text.Trim(),
                              tbDescription.Text.Trim(),
                              dtpStartDate.Value,
                              dtpDueDate.Value,
                              PersonDAO.GetByName(tbPersonName.Text),
                              Type.ConvertStringToStatus((string)cbStatus.SelectedItem)) ;
            if (currentAction == Type.Action.Add)
            {
                TaskDAO.Add(currentTask);
            } else
            {
                TaskDAO.Update(oldName, currentTask);
            }
            
            Close();
        }

        private void btnAddPerson_Click(object sender, System.EventArgs e)
        {
            FrmChoosePerson frm = new FrmChoosePerson();
            frm.ShowDialog();
            tbPersonName.Text = frm.SelectedPerson == null ? "" : frm.SelectedPerson.Name;
        }
    }
}
