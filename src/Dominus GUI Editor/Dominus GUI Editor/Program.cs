using System;
using System.Windows.Forms;

namespace Dominus_GUI_Editor
{
    static class Program
    {
        public static frmMain MainForm { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm = new frmMain();

            Application.Run(MainForm);
        }
    }
}