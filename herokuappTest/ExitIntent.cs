using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtension.Status;
using OpenQA.Selenium.Interactions;

namespace herokuappTest
{
	public class ExitIntent : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		public ExitIntent()
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/exit_intent");
		}

		[Fact]
		public void OutOfViewportAlertTest()
		{
			var builder = new Actions(Driver);

			builder.MoveToLocation(0, 0).Perform();
			//Driver.SwitchTo().Alert().Accept();
		}

		public void Dispose()
		{
			//Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
