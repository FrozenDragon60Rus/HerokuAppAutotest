using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace herokuappTest
{
	public class EntryAd : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		private readonly By closeButton = By.Id("ouibounce-modal");
		private readonly By modalPanel = By.Id("modal");

		public EntryAd()
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/entry_ad");
		}

		[Fact]
		public void OpenModalWindowTest() =>
			wait.Until(x => x.FindElement(modalPanel));
		[Fact]
		public void ModalWindowAfterCloseAndUpdate()
		{
			wait.Until(x => x.FindElement(modalPanel));

			var close = Driver.FindElement(closeButton);
			new Actions(Driver).Click(close);

			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/entry_ad");

			Assert.Throws<WebDriverTimeoutException>(() => wait.Until(x => x.FindElement(modalPanel)));
		}
		[Fact]
		public void ModalWindowAfterUpdate()
		{
			wait.Until(x => x.FindElement(modalPanel));

			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/entry_ad");

			wait.Until(x => x.FindElement(modalPanel));
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
