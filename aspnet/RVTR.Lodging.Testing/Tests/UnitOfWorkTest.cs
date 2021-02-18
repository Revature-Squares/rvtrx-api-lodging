using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.Context;
using RVTR.Lodging.Context.Repositories;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class UnitOfWorkTest : DataTest
  {
    [Fact]
    public async void Test_UnitOfWork_CommitAsync()
    {
      using var ctx = new CampgroundContext(Options);
      var unitOfWork = new UnitOfWork(ctx);
      var actual = await unitOfWork.CommitAsync();

      Assert.NotNull(unitOfWork.Campground);
      Assert.NotNull(unitOfWork.Campsite);
      Assert.NotNull(unitOfWork.Review);
      Assert.Equal(0, actual);
    }
  }
}
