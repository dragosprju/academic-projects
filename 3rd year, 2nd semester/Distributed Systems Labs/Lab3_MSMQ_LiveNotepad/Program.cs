using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_MSMQ_LiveNotepad
{   
    static class Program
    {
        public static LiveNotepadForm form;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new LiveNotepadForm();
            Application.Run(form);
        }

        public static void log(string text)
        {
            form.log(text);
        }
    }
}
