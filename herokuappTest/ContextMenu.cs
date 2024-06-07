using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using Xunit.Abstractions;

namespace herokuappTest
{
	public class ContextMenu : IDisposable
	{
		private ChromeDriver Driver { get; }
		private ITestOutputHelper Output { get; }

		private readonly WebDriverWait wait;

		private readonly By hotSpot = By.Id("hot-spot");

		public ContextMenu(ITestOutputHelper output)
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/context_menu");
			Output = output;
		}

#pragma warning disable CA1822 // Пометьте члены как статические
		bool IsAlertShown(IWebDriver driver)
		{
			try
			{
				driver.SwitchTo().Alert();
			}
			catch
			{
				return false;
			}
			return true;
		}

		[Fact]
		public void OpenContextMenuTest()
		{
			var spot = Driver.FindElement(hotSpot);

			new Actions(Driver).ContextClick(spot).Build().Perform();

			wait.Until(x => IsAlertShown(x));
			Driver.SwitchTo().Alert().Dismiss();

			var contextMenu = By.XPath("//ul[@class='maximenuck']/li");
			wait.Until(x => x.FindElement(contextMenu));

			var menu = Driver.FindElements(contextMenu);
			foreach (var element in menu)
				Output.WriteLine(element.Text);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
