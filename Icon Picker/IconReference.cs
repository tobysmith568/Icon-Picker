﻿namespace IconPicker
{
    /// <summary>
    /// Data model containing a path to a given icon as well as the index
    /// </summary>
    public class IconReference : IIconReference
    {
        //  Properties
        //  ==========

        /// <summary>
        /// File path to the icon
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Index of the icon within the file
        /// </summary>
        public int IconIndex { get; set; }
    }
}
