using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace herokuappTest
{
	public class Dropdown : IDisposable
	{
		private ChromeDriver Driver { get; }


		private readonly By dropdown = By.XPath("//select[@id='dropdown']");
		private readonly By dropdownList = By.XPath("//select[@id='dropdown']/option");

		public Dropdown()
		{
			Driver = new ChromeDriver();
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/dropdown");
		}

		[Theory
			,InlineData("Option 1")
			,InlineData("Option 2")]
		public void ChangeOptionTest(string option)
		{
			var dropdownElement = Driver.FindElement(dropdown);

			Actions builder = new(Driver);
			builder.Click(dropdownElement);

			var optionList = Driver.FindElements(dropdownList);
			var chooseOption = optionList.Where(x => x.Text == option).Single();
			int index = optionList.IndexOf(chooseOption);

			while (index-- > 0)
				builder.SendKeys(Keys.ArrowDown);

			builder.SendKeys(Keys.Enter).Release().Perform();

			chooseOption = optionList.Where(x => x.Text == option).Single();

			Assert.NotNull(chooseOption.GetAttribute("selected"));
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
