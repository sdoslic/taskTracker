using System.Windows.Forms;
using TaskTracker.Model;

namespace TaskTracker.View
{
    public partial class FrmPerson : Form
    {
        private Type.Action currentAction;
        private Person currentPerson;
        public FrmPerson(string name, Type.Action action)
        {
            InitializeComponent();
            Name = name;
            currentAction = action;
        }

        public FrmPerson(string name, Type.Action action, Person p)
        {
            InitializeComponent();
            Text = name;
            currentAction = action;
            currentPerson = p;
            UpdatedFields();
        }

        private void UpdatedFields()
        {
            tbName.Text = currentPerson.Name;
            dateTimePicker1.Value = currentPerson.Birthday;
            tbEmail.Text = currentPerson.Email;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string oldName = currentPerson != null ? currentPerson.Name : "";
            currentPerson = new Person(tbName.Text.Trim(),
                                       dateTimePicker1.Value,
                                       tbEmail.Text.Trim());
            if (currentAction == Type.Action.Add)
            {
                PersonDAO.Add(currentPerson);
            }
            else
            {
                PersonDAO.Update(oldName, currentPerson);
            }

            Close();
        }
    }
}
