using System;
using System.Runtime.InteropServices;
using System.Text;

namespace IconPicker.External
{
    internal class IconActions : IIconActions
    {
        //  Constants
        //  =========

        private const string Shell32 = "shell32.dll";
        private const string User32 = "user32.dll";

        //  External Methods
        //  ================

        [DllImport(Shell32, CharSet = CharSet.Auto)]
        private static extern int PickIconDlg(IntPtr hwndOwner, StringBuilder lpstrFile, int nMaxFile, ref int lpdwIconIndex);

        [DllImport(Shell32, CharSet = CharSet.Auto)]
        private static extern uint ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

        [DllImport(User32, CharSet = CharSet.Auto)]
        private static extern bool DestroyIcon(IntPtr handle);

        //  Methods
        //  =======

        public int PickIconDialog(IntPtr hwndOwner, StringBuilder lpstrFile, int nMaxFile, ref int lpdwIconIndex)
        {
            return PickIconDlg(hwndOwner, lpstrFile, nMaxFile, ref lpdwIconIndex);
        }

        public uint ExtractIcon(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons)
        {
            return ExtractIconEx(szFileName, nIconIndex, phiconLarge, phiconSmall, nIcons);
        }

        public bool DestroyIconAtHandle(IntPtr handle)
        {
            return DestroyIcon(handle);
        }
    }
}
