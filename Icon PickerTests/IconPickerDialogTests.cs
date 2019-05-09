using NUnit.Framework;
using IconPicker;
using IconPicker.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Drawing;
using System.Reflection;

namespace IconPicker.Tests
{
    [TestFixture]
    internal class IconPickerDialogTests
    {
        Mock<IIconActions> iconActions;
        Mock<IconPickerDialog> subject;

        [SetUp]
        public void SetUp()
        {
            iconActions = new Mock<IIconActions>();

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
            GivenSubjectIconActionsIsReplacedWithMock();

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
            GivenSubjectIconActionsIsReplacedWithMock();

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
            GivenSubjectIconActionsIsReplacedWithMock();

            subject.Object.SelectIcon(null);

            int value = 0;
            iconActions.Verify(s => s.PickIconDialog(It.IsAny<IntPtr>(), It.IsAny<StringBuilder>(), It.IsAny<int>(), ref value), Times.Once);
        }

        /// <exception cref="MockException">Ignore.</exception>
        [Test]
        public void SelectIcon_DoesNotPromptUserWhenNonNullPassedIn()
        {
            GivenPickIconDlgReturns(0);
            GivenExtractIconExReturns(0);
            GivenDestroyIconExReturns(false);
            GivenSubjectIconActionsIsReplacedWithMock();

            subject.Object.SelectIcon(new IconReference("", 0));

            int value = 0;
            iconActions.Verify(s => s.PickIconDialog(It.IsAny<IntPtr>(), It.IsAny<StringBuilder>(), It.IsAny<int>(), ref value), Times.Never);
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
            GivenSubjectIconActionsIsReplacedWithMock();

            subject.Object.SelectIconAsBitmap(null);

            int value = 0;
            iconActions.Verify(s => s.PickIconDialog(It.IsAny<IntPtr>(), It.IsAny<StringBuilder>(), It.IsAny<int>(), ref value), Times.Once);
        }

        /// <exception cref="MockException">Ignore.</exception>
        [Test]
        public void SelectIconAsBitmap_DoesNotPromptUserWhenNonNullPassedIn()
        {
            GivenPickIconDlgReturns(0);
            GivenExtractIconExReturns(0);
            GivenDestroyIconExReturns(false);
            GivenSubjectIconActionsIsReplacedWithMock();

            subject.Object.SelectIconAsBitmap(new IconReference("", 0));

            int value = 0;
            iconActions.Verify(s => s.PickIconDialog(It.IsAny<IntPtr>(), It.IsAny<StringBuilder>(), It.IsAny<int>(), ref value), Times.Never);
        }

        #endregion

        /// <exception cref="FieldAccessException">Ignore.</exception>
        /// <exception cref="TargetException">Ignore.</exception>
        public void GivenSubjectIconActionsIsReplacedWithMock()
        {
            Type type = subject.Object.GetType();
            FieldInfo fieldInfo = type.BaseType.GetField("iconActions", BindingFlags.Static | BindingFlags.NonPublic);
            fieldInfo.SetValue(subject.Object, iconActions.Object);
        }

        public void GivenPickIconDlgReturns(int returnValue)
        {
            int value = 0;
            iconActions.Setup(s => s.PickIconDialog(It.IsAny<IntPtr>(), It.IsAny<StringBuilder>(), It.IsAny<int>(), ref value)).Returns(returnValue);
        }

        public void GivenExtractIconExReturns(uint returnValue)
        {
            iconActions.Setup(s => s.ExtractIcon(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<IntPtr[]>(), It.IsAny<IntPtr[]>(), It.IsAny<uint>())).Returns(returnValue);
        }

        public void GivenDestroyIconExReturns(bool returnValue)
        {
            iconActions.Setup(s => s.DestroyIconAtHandle(It.IsAny<IntPtr>())).Returns(returnValue);
        }
    }
}