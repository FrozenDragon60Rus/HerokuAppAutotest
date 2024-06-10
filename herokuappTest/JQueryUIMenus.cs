using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace herokuappTest
{
	public class JQueryUIMenus : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		private readonly By disabledUI = By.Id("ui-id-1");
		private readonly By enabledUI = By.Id("ui-id-3");
		private readonly By downloadUI = By.Id("ui-id-4");
		private readonly By enabledList = By.XPath("//li[@id='ui-id-3']/ul");
		private readonly By hidenList = By.XPath("//li[@id='ui-id-1']/ul");
		private readonly By formatList = By.XPath("//li[@id='ui-id-4']/ul");

		public JQueryUIMenus()
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/jqueryui/menu");
		}

		[Fact]
		public void DisabledUITest()
		{
			var disabled = Driver.FindElement(disabledUI);
			var builder = new Actions(Driver);

			builder.MoveToElement(disabled).Perform();

			var list = Driver.FindElement(hidenList);
			Assert.False(list.Displayed);
		}
		[Fact]
		public void EnabledUITest()
		{
			var enabled = Driver.FindElement(enabledUI);
			var builder = new Actions(Driver);

			builder.MoveToElement(enabled).Perform();
			wait.Until(x => x.FindElement(enabledList).Displayed == true);

			var list = Driver.FindElement(enabledList);
			Assert.True(list.Displayed);
		}
		[Fact]
		public void FormatUITest()
		{
			var enabled = Driver.FindElement(enabledUI);
			var builder = new Actions(Driver);

			builder.MoveToElement(enabled).Perform();
			wait.Until(x => x.FindElement(enabledList).Displayed == true);

			var download = Driver.FindElement(downloadUI);

			builder.MoveToElement(download).Perform();
			wait.Until(x => x.FindElement(formatList).Displayed == true);

			var list = Driver.FindElement(formatList);
			Assert.True(list.Displayed);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
