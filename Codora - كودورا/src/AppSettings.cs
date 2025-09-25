using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Codora___كودورا
{
    public static class AppSettings
    {
        public const string AppName = "Codora - كودورا";
        public static Icon AppIcon = Properties.Resources.AppIcon;

        /// <summary>
        /// Handles the FormClosing event of a WinForms form.
        /// Prompts the user with a confirmation dialog when they try to close the form.
        /// </summary>
        /// <param name="sender">The source of the event (the form).</param>
        /// <param name="e">FormClosingEventArgs containing event data and a Cancel property.</param>
        public static void FormClosing(object sender, FormClosingEventArgs e)
        {
            // Check if the form is being closed by the user
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Ask for confirmation before closing
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to exit?", "Confirm Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No) e.Cancel = true; // Cancel the close event
                else Application.Exit(); // Kill the entire application
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();
    }
}