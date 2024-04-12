using Microsoft.EntityFrameworkCore;
using Moq;

namespace ASPNETCoreBackend.Tests
{
    internal static class TestHelpers
    {
        internal static DbSet<T> MockDbSet<T>(IEnumerable<T> testData) where T : class
        {
            var data = testData.AsQueryable();
            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockDbSet.Object;
        }
    }
}
