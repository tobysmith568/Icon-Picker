﻿namespace IconPicker
{
    public interface IIconReference
    {
        //  Properties
        //  ==========

        /// <summary>
        /// File path to the icon
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// Index of the icon within the file
        /// </summary>
        int IconIndex { get; }
    }
}