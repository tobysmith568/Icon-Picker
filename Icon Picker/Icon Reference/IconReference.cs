using System;
using System.Text.RegularExpressions;

namespace IconPicker
{
    /// <summary>
    /// Data model containing a path to a given icon as well as the index
    /// </summary>
    public class IconReference : IIconReference
    {
        //  Constants
        //  =========

        private const string comma = ",";

        //  Variables
        //  =========

        private static readonly Regex regex = new Regex(@".+\,[0-9]+$");

        //  Properties
        //  ==========

        /// <summary>
        /// File path to the icon.
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Index of the icon within the file.
        /// </summary>
        public int IconIndex { get; private set; }

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
            string filePath = reference.Substring(0, reference.Length - index.Length - 1);

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

        /// <summary>
        /// Returns the FileName and the IconIndex separated by a comma
        /// </summary>
        /// <returns>Returns the FileName and the IconIndex separated by a comma</returns>
        public override string ToString()
        {
            return (FilePath ?? string.Empty) + comma + (IconIndex.ToString() ?? string.Empty);
        }

        private void Setup(string filepath, string index)
        {
            if (!int.TryParse(index, out int iconIndex))
            {
                throw new ArgumentException("Parameter [index] needs to be castable to an integer");
            }

            Setup(filepath, iconIndex);
        }

        private void Setup(string filepath, int index)
        {
            if (index < 0)
            {
                throw new ArgumentException("Parameter [index] needs to be greater than or equal to zero");
            }

            FilePath = filepath;
            IconIndex = index;
        }
    }
}
