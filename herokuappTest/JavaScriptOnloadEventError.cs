using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Linq;
using Xunit.Abstractions;

namespace herokuappTest
{
	public class JavaScriptOnloadEventError : IDisposable
	{
		private ChromeDriver Driver { get; }
		private ITestOutputHelper output { get; }

		public JavaScriptOnloadEventError(ITestOutputHelper output)
		{
			Driver = new ChromeDriver(Options);
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/javascript_error");
			this.output = output;
		}

		private ChromeOptions Options
		{
			get
			{
				ChromeOptions options = new ChromeOptions();
				options.SetLoggingPreference(LogType.Browser, LogLevel.All);
				return options;
			}
		}

		[Fact]
		public void OnLoadTest()
		{
			var logs = Driver.Manage().Logs.GetLog(LogType.Browser);

			foreach (var log in logs)
				output.WriteLine(log.Message);

			Assert.Empty(logs);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
