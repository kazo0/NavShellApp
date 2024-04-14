using Uno.UI.Runtime.Skia;

namespace NavShellApp;

public class Program
{
	[STAThread]
	public static void Main(string[] args)
	{
		App.InitializeLogging();

		var host = SkiaHostBuilder.Create()
			.App(() => new App())
			.UseX11()
			.UseLinuxFrameBuffer()
			.UseMacOS()
			.UseWindows()
			.Build();

		host.Run();
	}
}
