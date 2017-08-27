using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Finance.Common.Helpers;

namespace Finance.Common.Tests
{
    [TestClass]
    public class DateTimeHelperTest
    {
        [TestMethod]
        public void TestStartOfTheMonth()
        {
            // Arrange
            var dateTime = new DateTime(2017, 8, 21);

            // Act
            var startOfMonth = DateTimeHelper.StartOfMonth(2017, 8);

            // Assert
            Assert.IsTrue(startOfMonth.DayOfWeek == DayOfWeek.Tuesday);
        }
    }
}
