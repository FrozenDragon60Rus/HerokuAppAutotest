using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtension.Status;

namespace herokuappTest
{
	public class MultipleWindows : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		private readonly By newTabRefetence = By.LinkText("Click Here");

		public MultipleWindows()
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/windows");
		}

		[Fact]
		public void OpenNewTabTest()
		{
			Driver.Click(newTabRefetence);
			
			Driver.SwitchTo().Window(Driver.WindowHandles.Last());
			Assert.Equal("https://the-internet.herokuapp.com/windows/new", Driver.Url);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
