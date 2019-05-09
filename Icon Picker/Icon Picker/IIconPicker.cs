using System.Drawing;
using System.Windows.Media.Imaging;

namespace IconPicker
{
    /// <summary>
    /// Interface for the IconPickerDialog class
    /// </summary>
    public interface IIconPicker
    {
        //  Methods
        //  =======

        /// <summary>
        /// Shows the Windows native icon picker to the user and returns a reference to their selection.
        /// </summary>
        /// <returns>A reference to the user-selected icon or null if they cancel.</returns>
        IIconReference SelectIconReference();

        /// <summary>
        /// Returns a given IconReference as an icon.
        /// If null is passed in then the user will be prompted to pick one with the native Windows icon-picker dialog.
        /// </summary>
        /// <returns>An icon of the given IconReference or null if null is passed in and the user cancels the dialog prompt.</returns>
        Icon SelectIcon(IIconReference iconReference = null);

        /// <summary>
        /// Returns a given IconReference as a bitmap.
        /// If null is passed in then the user will be prompted to pick one with the native Windows icon-picker dialog.
        /// </summary>
        /// <returns>A bitmap of the given IconReference or null if null is passed in and the user cancels the dialog prompt.</returns>
        BitmapSource SelectIconAsBitmap(IIconReference iconReference = null);
    }
}
