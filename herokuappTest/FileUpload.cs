using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtension.Status;

namespace herokuappTest
{
	public class FileUpload : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		private readonly By fileButton = By.Id("file-upload");
		private readonly By successText = By.Id("uploaded-files");
		private readonly By uploadButton = By.Id("file-submit");
		private readonly By uploadArea = By.Id("drag-drop-upload");

		public FileUpload()
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/upload");
		}

		[Fact]
		public void UploadTest()
		{
			Driver
				.FindElement(fileButton)
				.SendKeys(@"I:\Яндекс диск\Шаблоны\C#\HerokuAppAutotest\herokuappTest\bin\Debug\net8.0\herokuappTest.runtimeconfig.json");

			var builder = new Actions(Driver);

			Driver.Click(uploadButton);

			wait.Until(x => x.Exist(successText));
			var fileConfirmationField = Driver.FindElement(successText);

			Assert.Equal("herokuappTest.runtimeconfig.json", fileConfirmationField.Text);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
