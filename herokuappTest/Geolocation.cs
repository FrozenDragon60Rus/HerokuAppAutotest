using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtension.Status;

namespace herokuappTest
{
	public class Geolocation : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		private readonly By geolocationButton = By.XPath("//div[@class='example']/button");
		private readonly By latValue = By.Id("lat-value");
		private readonly By longValue = By.Id("long-value");

		public Geolocation()
		{
			Dictionary<string, object> profile = [];

			// 0 - Default, 1 - Allow, 2 - Block
			profile.Add("profile.default_content_setting_values.geolocation", 1);

			Dictionary<string, object> chromeOptions = [];

			chromeOptions.Add("prefs", profile);

			ChromeOptions capability = new();
			capability.AddAdditionalOption("chromeOptions", chromeOptions);

			Driver = new ChromeDriver(capability);
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/geolocation");
		}

		[Fact]
		public void GeolocationRequestTest()
		{
			Driver.Click(geolocationButton);

			wait.Until(x => x.Exist(latValue));

			double
				Latitude = Convert.ToDouble(Driver.FindElement(latValue).Text)
				, Longitude = Convert.ToDouble(Driver.FindElement(longValue).Text);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
