using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using MessageBoxOptions = System.Windows.Forms.MessageBoxOptions;

/// <summary>
/// Provides static methods to display standard MessageBox dialogs
/// in English, including Error, Success, Warning, and Information messages.
/// </summary>
public class MessageDisplay
{
    /// <summary>
    /// Shows an error message box with the specified message.
    /// </summary>
    /// <param name="message">The text to display in the message box.</param>
    public static void ShowError(string message)
    {
        System.Windows.Forms.MessageBox.Show(message, "Error", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Error);
    }

    /// <summary>
    /// Shows a success/information message box with the specified message.
    /// </summary>
    /// <param name="message">The text to display in the message box.</param>
    public static void ShowSuccess(string message)
    {
        System.Windows.Forms.MessageBox.Show(message, "Success", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Information);
    }

    /// <summary>
    /// Shows a warning message box with the specified message.
    /// </summary>
    /// <param name="message">The text to display in the message box.</param>
    public static void ShowWarning(string message)
    {
        System.Windows.Forms.MessageBox.Show(message, "Warning", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Warning);
    }

    /// <summary>
    /// Shows an informational message box with the specified message.
    /// </summary>
    /// <param name="message">The text to display in the message box.</param>
    public static void ShowInfo(string message)
    {
        System.Windows.Forms.MessageBox.Show(message, "Information", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Information);
    }
}

/// <summary>
/// Provides static methods to display MessageBox dialogs in Arabic,
/// with right-to-left alignment and localized captions.
/// Supports Error, Success, Warning, and Information messages.
/// </summary>
public class ArabicMessageDisplay
{
    /// <summary>
    /// Shows an error message box with Arabic caption and right-to-left layout.
    /// </summary>
    /// <param name="message">The text to display in the message box.</param>
    public static void ShowError(string message)
    {
        System.Windows.Forms.MessageBox.Show(
            message,
            "خطأ", // أو "Error" بالعربية
            MessageBoxButtons.OK,
            MessageBoxIcon.Error,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading
        );

    }

    /// <summary>
    /// Shows a success/information message box with Arabic caption and right-to-left layout.
    /// </summary>
    /// <param name="message">The text to display in the message box.</param>
    public static void ShowSuccess(string message)
    {
        System.Windows.Forms.MessageBox.Show(
            message,
            "نجاح", // أو "Success" بالعربية
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading
        );
    }

    /// <summary>
    /// Shows a warning message box with Arabic caption and right-to-left layout.
    /// </summary>
    /// <param name="message">The text to display in the message box.</param>
    public static void ShowWarning(string message)
    {
        System.Windows.Forms.MessageBox.Show(
            message,
            "تحذير", // أو "Warning" بالعربية
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading
        );
    }

    /// <summary>
    /// Shows an informational message box with Arabic caption and right-to-left layout.
    /// </summary>
    /// <param name="message">The text to display in the message box.</param>
    public static void ShowInfo(string message)
    {
        System.Windows.Forms.MessageBox.Show(
            message,
            "معلومات", // أو "Information" بالعربية
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading
        );
    }
}
