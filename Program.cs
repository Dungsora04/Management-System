using Management_System.PAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Management_System
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new FormStart()); //ai muon xem thi bo comment cai nay
            
            //neu khoi dong app bang form start thi comment dong nay lai
            //Application.Run(new FormLogIn()); 
        }
    }
}
