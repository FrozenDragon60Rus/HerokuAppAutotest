using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Xunit.Abstractions;

namespace herokuappTest
{
	public class Inputs : IDisposable
	{
		private ChromeDriver Driver { get; }
		private ITestOutputHelper Output { get; }

		private readonly By inputBox = By.XPath("//div[@class='example']/input");

		public Inputs(ITestOutputHelper output)
		{
			Driver = new ChromeDriver();
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/inputs");
			Output = output;
		}

		[Theory
			, InlineData("1234567890", true)
			, InlineData(@"eE", true)
			, InlineData("qwrtyuiopasdfghjklzxcvbnm", false)
			, InlineData("QWRTYUIOPASDFGHJKLZXCVBNM", false)
			, InlineData(@"[];'\,/{}-=_+()*&^%$#@!<>?:№%`~", false)
			, InlineData("йцукенгшщзхъфывапролджэячсмитьбю", false)]
		public void InputNumberTest(string character, bool allow)
		{
			var input = Driver.FindElement(inputBox);
			var builder = new Actions(Driver);
			builder.Click(input).Perform();

			bool equal = allow;
			foreach (char c in character)
			{
				builder.SendKeys(c.ToString()).Perform();

				if ((input.GetAttribute("value") == c.ToString()) != allow)
				{
					equal = !allow;
					Output.WriteLine($"Символ '{input.GetAttribute("value")}' не допустим");
				}

				builder.SendKeys(Keys.Backspace).Perform();
			}

			Assert.Equal(allow, equal);
		}

		[Theory
			, InlineData("e.", false)
			, InlineData("ee", false)
			, InlineData("..", false)]
		public void InputManyNumberSymbolsTest(string character, bool allow)
		{
			var input = Driver.FindElement(inputBox);
			var builder = new Actions(Driver);

			builder
				.Click(input)
				.SendKeys(character)
				.Perform();

			bool equal = input.GetAttribute("value") == character;

			Assert.Equal(allow, equal);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
