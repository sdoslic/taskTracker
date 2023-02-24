using System.Windows.Forms;
using TaskTracker.Model;
using TaskTracker.View;

namespace TaskTracker.Controller
{
    public class FormFactory
    {
        private static FormFactory instance = null;
        static readonly object lockObj = new object();

        public delegate void Callback();
        private FormFactory()
        {

        }

        public static FormFactory Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new FormFactory();
                    }
                    return instance;
                }
            }
        }

        public void OpenWindow(Util.Type.Mode mode, Util.Type.Action action, Callback fn, Entry entry)
        {
            Form frm = null;
            if (mode == Util.Type.Mode.Task)
            {
                frm = new FrmTask(action, (Model.Task) entry);
            }
            else
            {
                frm = new FrmPerson(action, (Person) entry);
            }
            frm.ShowDialog();
            fn();
        }

    }
}
