using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace herokuappTest
{
	public class DynamicLoading : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		private readonly By startButton = By.XPath("//div[@id='start']/button");
		private readonly By elementField = By.Id("finish");

		public DynamicLoading()
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
		}

		[Fact]
		public void LoadingHiddenElementTest()
		{
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/dynamic_loading/1");

			var start = Driver.FindElement(startButton);
			start.Click();

			wait.Until(x => x.FindElement(elementField).Displayed);
		}

		[Fact]
		public void LoadingRendererElementTest()
		{
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/dynamic_loading/2");

			var start = Driver.FindElement(startButton);
			start.Click();

			wait.Until(x => x.FindElement(elementField).Displayed);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
