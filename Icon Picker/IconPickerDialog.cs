﻿using System;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
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
        private const string User32 = "user32.dll";

        //  Variables
        //  =========

        private static readonly string iconFile;

        //  Constructors
        //  ============

        static IconPickerDialog()
        {
            iconFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), Shell32);
        }

        //  External Methods
        //  ================

        [DllImport(Shell32, CharSet = CharSet.Auto)]
        private static extern int Extern_PickIconDlg(IntPtr hwndOwner, StringBuilder lpstrFile, int nMaxFile, ref int lpdwIconIndex);

        [DllImport(Shell32, CharSet = CharSet.Auto)]
        private static extern uint Extern_ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

        [DllImport(User32, CharSet = CharSet.Auto)]
        private static extern bool Extern_DestroyIcon(IntPtr handle);

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
            int retval = PickIconDlg((IntPtr)null, sb, sb.MaxCapacity, ref index);

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
            ExtractIconEx(iconReference.FilePath, iconReference.IconIndex, largeIcons, smallIcons, 1);

            Icon icon;
            try
            {
                icon = Icon.FromHandle(largeIcons[0]);
            }
            catch
            {
                return null;
            }

            DestroyIcon(largeIcons[0]);
            DestroyIcon(smallIcons[0]);

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
            ExtractIconEx(iconReference.FilePath, iconReference.IconIndex, largeIcons, smallIcons, 1);

            BitmapSource bitmapSource;
            try
            {
                bitmapSource = Imaging.CreateBitmapSourceFromHIcon(largeIcons[0], Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                return null;
            }

            DestroyIcon(largeIcons[0]);
            DestroyIcon(smallIcons[0]);

            return bitmapSource;
        }

        internal virtual int PickIconDlg(IntPtr hwndOwner, StringBuilder lpstrFile, int nMaxFile, ref int lpdwIconIndex)
        {
            return Extern_PickIconDlg(hwndOwner, lpstrFile, nMaxFile, ref lpdwIconIndex);
        }

        internal virtual uint ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons)
        {
            return Extern_ExtractIconEx(szFileName, nIconIndex, phiconLarge, phiconSmall, nIcons);
        }

        internal virtual bool DestroyIcon(IntPtr handle)
        {
            return Extern_DestroyIcon(handle);
        }
    }
}