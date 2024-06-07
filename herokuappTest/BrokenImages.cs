using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace herokuappTest
{
	public class BrokenImages : IDisposable
	{
		private ChromeDriver Driver { get; }


		private readonly By img = By.XPath("//div[@class=\"example\"]/img");	

		public BrokenImages()
		{
			Driver = new ChromeDriver(Options);
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/broken_images");
		}

#pragma warning disable CA1822 // Пометьте члены как статические
		private ChromeOptions Options
		{
			get
			{
				ChromeOptions options = new();
				options.SetLoggingPreference(LogType.Browser, LogLevel.All);
				return options;
			}
		}

		[Fact]
		public void ImageLoadTest()
		{
			var imageElements = Driver.FindElements(img);
			var images = imageElements.Select(x => x.GetAttribute("src"));
			var logs = Driver.Manage().Logs.GetLog(LogType.Browser);

			bool IsNotLoad = false;

			foreach (var image in images)
				foreach (var log in logs)
					IsNotLoad |= log.Message.Contains(image);

			Assert.False(IsNotLoad);
		}

		public void Dispose()
		{
			Driver.Close();
			GC.SuppressFinalize(this);
		}
	}
}
