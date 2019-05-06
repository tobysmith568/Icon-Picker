﻿using System;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;

namespace IconPicker
{
    public class IconPickerDialog
    {
        //  Constants
        //  =========

        private const string Shell32 = "shell32.dll";
        private const string User32 = "user32.dll";
        private const string SlashShell32 = @"\shell32.dll";

        //  External Methods
        //  ================

        [DllImport(Shell32, CharSet = CharSet.Auto)]
        private static extern int PickIconDlg(IntPtr hwndOwner, StringBuilder lpstrFile, int nMaxFile, ref int lpdwIconIndex);

        [DllImport(Shell32, CharSet = CharSet.Auto)]
        private static extern uint ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

        [DllImport(User32, CharSet = CharSet.Auto)]
        private static extern bool DestroyIcon(IntPtr handle);

        //  Variables
        //  =========

        private static readonly string iconFile;

        //  Constructors
        //  ============

        static IconPickerDialog()
        {
            iconFile = Environment.GetFolderPath(Environment.SpecialFolder.System) + SlashShell32;
        }

        //  Methods
        //  =======

        public static IconReference SelectIcon()
        {
            int index = 0;
            var sb = new StringBuilder(iconFile, 500);
            int retval = PickIconDlg((IntPtr)null, sb, sb.MaxCapacity, ref index);

            if (retval != 0)
            {
                return new IconReference()
                {
                    FilePath = sb.ToString(),
                    IconIndex = index
                };
            }
            return null;
        }

        public static BitmapSource SelectIconAsBitmap()
        {
            IconReference iconReference = SelectIcon();

            if (iconReference == null)
            {
                return null;
            }

            var largeIcons = new IntPtr[1];
            var smallIcons = new IntPtr[1];
            ExtractIconEx(iconReference.FilePath, iconReference.IconIndex, largeIcons, smallIcons, 1);

            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHIcon(largeIcons[0], Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            DestroyIcon(largeIcons[0]);
            DestroyIcon(smallIcons[0]);

            return bitmapSource;
        }
    }
}