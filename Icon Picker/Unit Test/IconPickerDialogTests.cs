using NUnit.Framework;
using IconPicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Drawing;

namespace IconPicker.Tests
{
    [TestFixture]
    public class IconPickerDialogTests
    {
        Mock<IconPickerDialog> subject;

        [SetUp]
        public void SetUp()
        {
            subject = new Mock<IconPickerDialog>
            {
                CallBase = true
            };
        }

        #region SelectIconReference

        [Test]
        public void SelectIconReference_UserSelectsIcon()
        {
            GivenPickIconDlgReturns(1);

            IIconReference result = subject.Object.SelectIconReference();

            Assert.NotNull(result);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(50)]
        public void SelectIconReference_UserCancelsSelection(int value)
        {
            GivenPickIconDlgReturns(value);

            IIconReference result = subject.Object.SelectIconReference();

            Assert.Null(result);
        }

        #endregion
        #region SelectIcon

        /// <exception cref="MockException">Ignore.</exception>
        [Test]
        public void SelectIcon_PromptsUserWhenNullPassedIn()
        {
            GivenPickIconDlgReturns(0);
            GivenExtractIconExReturns(0);
            GivenDestroyIconExReturns(false);

            subject.Object.SelectIcon(null);

            int value = 0;
            subject.Verify(s => s.PickIconDlg(It.IsAny<IntPtr>(), It.IsAny<StringBuilder>(), It.IsAny<int>(), ref value), Times.Once);
        }

        /// <exception cref="MockException">Ignore.</exception>
        [Test]
        public void SelectIcon_DoesNotPromptUserWhenNonNullPassedIn()
        {
            GivenPickIconDlgReturns(0);
            GivenExtractIconExReturns(0);
            GivenDestroyIconExReturns(false);

            subject.Object.SelectIcon(new IconReference("", 0));

            int value = 0;
            subject.Verify(s => s.PickIconDlg(It.IsAny<IntPtr>(), It.IsAny<StringBuilder>(), It.IsAny<int>(), ref value), Times.Never);
        }

        #endregion
        #region SelectIconAsBitmap

        /// <exception cref="MockException">Ignore.</exception>
        [Test]
        public void SelectIconAsBitmap_PromptsUserWhenNullPassedIn()
        {
            GivenPickIconDlgReturns(0);
            GivenExtractIconExReturns(0);
            GivenDestroyIconExReturns(false);

            subject.Object.SelectIconAsBitmap(null);

            int value = 0;
            subject.Verify(s => s.PickIconDlg(It.IsAny<IntPtr>(), It.IsAny<StringBuilder>(), It.IsAny<int>(), ref value), Times.Once);
        }

        /// <exception cref="MockException">Ignore.</exception>
        [Test]
        public void SelectIconAsBitmap_DoesNotPromptUserWhenNonNullPassedIn()
        {
            GivenPickIconDlgReturns(0);
            GivenExtractIconExReturns(0);
            GivenDestroyIconExReturns(false);

            subject.Object.SelectIconAsBitmap(new IconReference("", 0));

            int value = 0;
            subject.Verify(s => s.PickIconDlg(It.IsAny<IntPtr>(), It.IsAny<StringBuilder>(), It.IsAny<int>(), ref value), Times.Never);
        }

        #endregion

        public void GivenPickIconDlgReturns(int returnValue)
        {
            int value = 0;
            subject.Setup(s => s.PickIconDlg(It.IsAny<IntPtr>(), It.IsAny<StringBuilder>(), It.IsAny<int>(), ref value)).Returns(returnValue);
        }

        public void GivenExtractIconExReturns(uint returnValue)
        {
            subject.Setup(s => s.ExtractIconEx(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<IntPtr[]>(), It.IsAny<IntPtr[]>(), It.IsAny<uint>())).Returns(returnValue);
        }

        public void GivenDestroyIconExReturns(bool returnValue)
        {
            subject.Setup(s => s.DestroyIcon(It.IsAny<IntPtr>())).Returns(returnValue);
        }
    }
}