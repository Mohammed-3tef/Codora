using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Provides helper methods for managing forms and controls inside a Windows Forms application.
/// </summary>
public static class FormSettings
{
    /// <summary>
    /// Cleans all input fields inside the given parent control.
    /// Supports <see cref="TextBox"/>, <see cref="ComboBox"/>, <see cref="CheckBox"/>, <see cref="NumericUpDown"/>, 
    /// <see cref="RadioButton"/>, <see cref="PictureBox"/>, and <see cref="DateTimePicker"/>.
    /// Works recursively for nested containers such as <see cref="Panel"/> or <see cref="GroupBox"/>.
    /// </summary>
    /// <param name="parent">The parent control (usually the form or a container) whose child controls will be cleared.</param>
    public static void CleanFields(Control parent)
    {
        foreach (Control ctrl in parent.Controls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Clear();

            else if (ctrl is ComboBox)
                ((ComboBox)ctrl).SelectedIndex = -1;

            else if (ctrl is ListBox)
                ((ListBox)ctrl).ClearSelected();

            else if (ctrl is NumericUpDown)
                ((NumericUpDown)ctrl).Value = ((NumericUpDown)ctrl).Minimum;

            else if (ctrl is CheckBox)
                ((CheckBox)ctrl).Checked = false;

            else if (ctrl is RadioButton)
                ((RadioButton)ctrl).Checked = false;

            else if (ctrl is DateTimePicker)
                ((DateTimePicker)ctrl).Value = DateTime.Now;

            else if (ctrl is PictureBox)
            {
                ((PictureBox)ctrl).Image = null;
                ((PictureBox)ctrl).Tag = null;
            }

            // Recursive call for nested containers
            if (ctrl.HasChildren) CleanFields(ctrl);
        }
    }

    /// <summary>
    /// Opens a form inside a given panel.
    /// Closes any currently opened form in the panel before displaying the new one.
    /// </summary>
    /// <param name="form">The form instance to be displayed.</param>
    /// <param name="panel">The panel that will host the form.</param>
    public static void OpenForm(Form form, Panel panel)
    {
        // close any form that is currently open in the panel
        if (panel.Controls.Count > 0)
        {
            if (panel.Controls[0] is Form currentForm && currentForm.GetType() == form.GetType()) return;
            else if (panel.Controls[0] is Form currentForm2) currentForm2.Close();
        }

        form.TopLevel = false;
        form.FormBorderStyle = FormBorderStyle.None;
        form.Dock = DockStyle.Fill;
        panel.Controls.Clear();
        panel.Controls.Add(form);
        form.Show();
    }
}
