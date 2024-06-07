using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Xml.Linq;
using System.Net;

namespace herokuappTest
{
	public class FileDownload : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly By fileReference = By.XPath("//div[@class='example']/a");

		public FileDownload()
		{
			Driver = new ChromeDriver();
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/download");
		}

		private bool FileExist(string url)
		{
			HttpWebResponse response;
			var request = WebRequest.Create(url) as HttpWebRequest;
			request.Method = "HEAD";

			try
			{
				response = (HttpWebResponse)request.GetResponse();
				return true;
			}
			catch
			{
				return false;
			}
		}

		[Fact]
		public void DownloadTest()
		{
			var fileExist = true;

			var reference = Driver.FindElements(fileReference);
			foreach (var element in reference)
				fileExist &= FileExist(element.GetAttribute("href"));

			Assert.True(fileExist);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
