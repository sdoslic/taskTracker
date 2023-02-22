using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TaskTracker.Model;

namespace TaskTracker.View
{
    public partial class FrmTaskTracker : Form
    {
        public FrmTaskTracker()
        {
            InitializeComponent();
            try
            {
                List<Task> tasks = TaskDAO.GetAll();
                foreach(Task t in tasks)
                {
                    dgTask.Rows.Add(new string[] { t.Name, t.Description, t.StartDate.ToString("dd/MM/yyyy"), t.DueDate.ToString("dd/MM/yyyy"),t.ResposiblePerson.Name, Type.ConvertStatusToString(t.Status) });
                }
            } catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
