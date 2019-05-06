using System.Drawing;
using System.Windows.Media.Imaging;

namespace IconPicker
{
    /// <summary>
    /// Interface for the IconPickerDialog class
    /// </summary>
    public interface IIconPicker
    {
        IconReference SelectIconReference();
        Icon SelectIcon();
        BitmapSource SelectIconAsBitmap();
    }
}
