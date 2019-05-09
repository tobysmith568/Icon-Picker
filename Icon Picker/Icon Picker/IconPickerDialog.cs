using System;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using IconPicker.External;

namespace IconPicker
{
    /// <summary>
    /// Shows the Windows native icon picker to the user and either returns a reference to it or the selected icon itself as an icon or bitmap.
    /// </summary>
    public class IconPickerDialog : IIconPicker
    {
        //  Constants
        //  =========

        private const string Shell32 = "shell32.dll";

        //  Variables
        //  =========

        private static readonly IIconActions iconActions;
        private static readonly string iconFile;

        //  Constructors
        //  ============

        static IconPickerDialog()
        {
            iconActions = new IconActions();
            iconFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), Shell32);
        }

        //  Methods
        //  =======

        /// <summary>
        /// Shows the Windows native icon picker to the user and returns a reference to their selection.
        /// </summary>
        /// <returns>A reference to the user-selected icon or null if they cancel.</returns>
        public IIconReference SelectIconReference()
        {
            int index = 0;
            var sb = new StringBuilder(iconFile, 500);
            int retval = iconActions.PickIconDialog((IntPtr)null, sb, sb.MaxCapacity, ref index);

            if (retval == 1)
            {
                return new IconReference(sb.ToString(), index);
            }
            return null;
        }

        /// <summary>
        /// Returns a given IconReference as an icon.
        /// If null is passed in then the user will be prompted to pick one with the native Windows icon-picker dialog.
        /// </summary>
        /// <returns>An icon of the given IconReference or null if null is passed in and the user cancels the dialog prompt.</returns>
        public Icon SelectIcon(IIconReference iconReference = null)
        {
            iconReference = iconReference ?? SelectIconReference();

            if (iconReference == null)
            {
                return null;
            }

            var largeIcons = new IntPtr[1];
            var smallIcons = new IntPtr[1];
            iconActions.ExtractIcon(iconReference.FilePath, iconReference.IconIndex, largeIcons, smallIcons, 1);

            Icon icon;
            try
            {
                icon = Icon.FromHandle(largeIcons[0]);
            }
            catch
            {
                return null;
            }

            iconActions.DestroyIconAtHandle(largeIcons[0]);
            iconActions.DestroyIconAtHandle(smallIcons[0]);

            return icon;
        }

        /// <summary>
        /// Returns a given IconReference as a bitmap.
        /// If null is passed in then the user will be prompted to pick one with the native Windows icon-picker dialog.
        /// </summary>
        /// <returns>A bitmap of the given IconReference or null if null is passed in and the user cancels the dialog prompt.</returns>
        public BitmapSource SelectIconAsBitmap(IIconReference iconReference = null)
        {
            iconReference = iconReference ?? SelectIconReference();

            if (iconReference == null)
            {
                return null;
            }

            var largeIcons = new IntPtr[1];
            var smallIcons = new IntPtr[1];
            iconActions.ExtractIcon(iconReference.FilePath, iconReference.IconIndex, largeIcons, smallIcons, 1);

            BitmapSource bitmapSource;
            try
            {
                bitmapSource = Imaging.CreateBitmapSourceFromHIcon(largeIcons[0], Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                return null;
            }

            iconActions.DestroyIconAtHandle(largeIcons[0]);
            iconActions.DestroyIconAtHandle(smallIcons[0]);

            return bitmapSource;
        }
    }
}