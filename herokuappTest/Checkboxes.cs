using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace herokuappTest
{
	public class Checkboxes : IDisposable
	{
		private ChromeDriver Driver { get; }

		public Checkboxes()
		{
			Driver = new ChromeDriver();
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/checkboxes");
		}

		[Fact]
		public void Authorization()
		{
			var checkbox = By.XPath("//input[@type='checkbox']");

			var check = Driver.FindElements(checkbox);
			check.First().Click();

			Assert.NotNull(check.First().GetAttribute("checked"));
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
