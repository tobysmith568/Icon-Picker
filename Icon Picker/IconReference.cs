﻿using System;
using System.Text.RegularExpressions;

namespace IconPicker
{
    /// <summary>
    /// Data model containing a path to a given icon as well as the index
    /// </summary>
    public class IconReference : IIconReference
    {
        //  Variables
        //  =========

        private static readonly Regex regex = new Regex(@".+\,[0-9]+$");

        //  Properties
        //  ==========

        /// <summary>
        /// File path to the icon.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Index of the icon within the file.
        /// </summary>
        public int IconIndex { get; set; }

        //  Constructors
        //  ============

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="reference">A reference for an icon within either an .ico, .exe, or .dll. Must be a valid file location followed by a comma and then an int.</param>
        /// <exception cref="RegexMatchTimeoutException">Ignore.</exception>
        public IconReference(string reference)
        {
            if (!regex.IsMatch(reference))
            {
                throw new ArgumentException("[reference] must be a valid file location followed by a comma and then an int");
            }

            string[] split = reference.Split(',');
            string index = split[split.Length - 1];
            string filePath = reference.Substring(0, reference.Length - index.Length);

            Setup(filePath, index);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath">A valid file location for an .ico, .exe, or .dll.</param>
        /// <param name="index">The index of the icon wanted within the file.</param>
        public IconReference(string filePath, string index)
        {
            Setup(filePath, index);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath">A valid file location for an .ico, .exe, or .dll.</param>
        /// <param name="index">The index of the icon wanted within the file.</param>
        public IconReference(string filePath, int index)
        {
            Setup(filePath, index);
        }

        private void Setup(string filepath, string index)
        {
            if (!int.TryParse(index, out int iconIndex))
            {
                throw new ArgumentException("Prameter [index] needs to be castable to an integer");
            }
            Setup(filepath, index);
        }

        private void Setup(string filepath, int index)
        {
            FilePath = filepath;
            IconIndex = index;
        }
    }
}
