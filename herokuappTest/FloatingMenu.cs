using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace herokuappTest
{
	public class FloatingMenu : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		private readonly By menuBoard = By.Id("menu");
		private readonly By footerPanel = By.Id("page-footer");

		public FloatingMenu()
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/floating_menu");
		}

		[Fact]
		public void MenuIsFloatTest()
		{
			var top = Driver.FindElement(menuBoard).GetCssValue("top");
			Actions builder = new(Driver);

			var footer = Driver.FindElement(footerPanel);
			builder.ScrollToElement(footer).Perform();

			var newTop = Driver.FindElement(menuBoard).GetCssValue("top");
			Assert.NotEqual(top, newTop);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
