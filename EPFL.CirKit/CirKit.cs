using System;
using System.Json;
using System.Runtime.InteropServices;
using System.Text;

namespace EPFL
{
  public class CirKit
  {
    private class Lib
    {
      [DllImport("libcirkit_c", EntryPoint = "cirkit_create")]
      public static extern IntPtr cirkit_create();

      [DllImport("libcirkit_c", EntryPoint = "cirkit_delete")]
      public static extern void cirkit_delete(IntPtr cli);

      [DllImport("libcirkit_c", EntryPoint = "cirkit_command")]
      public static extern int cirkit_command(IntPtr cli, string command, StringBuilder log, int size);
    }

    ~CirKit()
    {
      Lib.cirkit_delete(cli);
    }

    public JsonValue Run(string command)
    {
      var log_length = command.StartsWith("write_") ? 1 << 20 : 1 << 12;

      StringBuilder read = new StringBuilder(log_length);
      if (Lib.cirkit_command(cli, command, read, log_length) > 0)
      {
        return JsonValue.Parse(read.ToString());
      }

      return null;
    }

    private IntPtr cli = Lib.cirkit_create();
  }
}
