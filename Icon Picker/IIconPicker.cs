using System.Drawing;
using System.Windows.Media.Imaging;

namespace IconPicker
{
    /// <summary>
    /// Interface for the IconPickerDialog class
    /// </summary>
    public interface IIconPicker
    {
        /// <summary>
        /// Shows the Windows native icon picker to the user and returns a reference to their selection.
        /// </summary>
        /// <returns>A reference to the user-selected icon or null if they cancel.</returns>
        IIconReference SelectIconReference();

        /// <summary>
        /// Shows the Windows native icon picker to the user and returns the icon as a bitmap.
        /// </summary>
        /// <returns>A bitmap of the user-selected image or null if they cancel.</returns>
        Icon SelectIcon();

        /// <summary>
        /// Shows the Windows native icon picker to the user and returns the icon as a bitmap.
        /// </summary>
        /// <returns>A bitmap of the user-selected image or null if they cancel.</returns>
        BitmapSource SelectIconAsBitmap();
    }
}
