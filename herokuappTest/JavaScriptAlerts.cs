using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace herokuappTest
{
	public class JavaScriptAlerts : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly By alertButton = By.XPath("//button[text()='Click for JS Alert']");
		private readonly By confirmButton = By.XPath("//button[text()='Click for JS Confirm']");
		private readonly By promptButton = By.XPath("//button[text()='Click for JS Prompt']");
		private readonly By resultLable = By.Id("result");

		public JavaScriptAlerts()
		{
			Driver = new ChromeDriver();
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/javascript_alerts");
		}

		[Fact]
		public void AlertTest()
		{
			Driver.FindElement(alertButton).Click();
			Driver.SwitchTo().Alert().Accept();

			var result = Driver.FindElement(resultLable).Text;
			Assert.Equal("You successfully clicked an alert", result);
		}
		[Fact]
		public void ConfirmAcceptTest()
		{
			Driver.FindElement(confirmButton).Click();
			Driver.SwitchTo().Alert().Accept();

			var result = Driver.FindElement(resultLable).Text;
			Assert.Equal("You clicked: Ok", result);
		}
		[Fact]
		public void ConfirmDismissTest()
		{
			Driver.FindElement(confirmButton).Click();
			Driver.SwitchTo().Alert().Dismiss();

			var result = Driver.FindElement(resultLable).Text;
			Assert.Equal("You clicked: Cancel", result);
		}
		[Theory
			, InlineData("asd", "You entered: asd")
			, InlineData("", "You entered:")]
		public void PromptAcceptTest(string text, string output)
		{
			Driver.FindElement(promptButton).Click();
			var alert = Driver.SwitchTo().Alert();
			alert.SendKeys(text);
			alert.Accept();

			var result = Driver.FindElement(resultLable).Text;
			Assert.Equal(output, result);
		}
		[Fact]
		public void PromptDismissTest()
		{
			Driver.FindElement(promptButton).Click();
			Driver.SwitchTo().Alert().Dismiss();

			var result = Driver.FindElement(resultLable).Text;
			Assert.Equal("You entered: null", result);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
