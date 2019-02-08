using Xunit;

namespace EPFL
{
  public class TestCirKit
  {
    [Fact]
    public void CanParseExpression()
    {
      var cli = new CirKit();
      cli.Run("tt --expression [ab]");
      var res = cli.Run("ps --tt");
      Assert.Equal("0110", res["binary"]);
    }
  }
}