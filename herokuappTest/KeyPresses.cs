using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace herokuappTest
{
	public class KeyPresses : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		private readonly By resultLabel = By.Id("result");

		public KeyPresses()
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/key_presses");
		}

		[Fact]
		public void KeyPressTest()
		{
			var result = Driver.FindElement(resultLabel);
			var builder = new Actions(Driver);

			builder.SendKeys(Keys.Alt).Perform();

			Assert.Equal("You entered: ALT", result.Text);
			//You entered:
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
