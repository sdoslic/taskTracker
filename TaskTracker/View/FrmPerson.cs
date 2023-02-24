using System.Windows.Forms;
using TaskTracker.Model;
using TaskTracker.Util;

namespace TaskTracker.View
{
    public partial class FrmPerson : Form
    {
        private Type.Action currentAction;
        private Person currentPerson;

        public FrmPerson(Type.Action action, Person p)
        {
            InitializeComponent();
            Text = action == Type.Action.Add ? "Adding person" : "Updating person";
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
            if (!Util.Validator.CheckEmpty(tbName.Text.Trim(), tbName, errorProvider1) ||
                !Util.Validator.CheckEmpty(dateTimePicker1.Text.Trim(), dateTimePicker1, errorProvider1) ||
                !Util.Validator.CheckEmpty(tbEmail.Text.Trim(), tbEmail, errorProvider1) ||
                !Util.Validator.CheckEmail(tbEmail.Text.Trim(), tbEmail, errorProvider1))
            {
                return;
            }
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

        private void tbName_TextChanged(object sender, System.EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void dateTimePicker1_ValueChanged(object sender, System.EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void tbEmail_TextChanged(object sender, System.EventArgs e)
        {
            errorProvider1.Clear();
        }
    }
}
