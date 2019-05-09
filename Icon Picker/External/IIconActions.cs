using System;
using System.Text;

namespace IconPicker.External
{
    public interface IIconActions
    {
        //  Methods
        //  =======

        int PickIconDialog(IntPtr hwndOwner, StringBuilder lpstrFile, int nMaxFile, ref int lpdwIconIndex);

        uint ExtractIcon(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

        bool DestroyIconAtHandle(IntPtr handle);
    }
}
