using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace herokuappTest
{
	public class DynamicContent : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly By rowList = By.ClassName("row");
		private readonly By img = By.XPath("//div/img");
		private readonly By feed = By.XPath("//div[contains(@class,'large-10')]");

		public DynamicContent()
		{
			Driver = new ChromeDriver();
			Driver.Manage().Window.Maximize();
		}

		public bool IsSame(string url)
		{
			Driver.Navigate().GoToUrl(url);

			var row = Driver.FindElements(rowList);

			int count = row.Count;
			string[]
				src = new string[count]
				, text = new string[count];

			int index = 0;
			foreach (var item in row)
			{
				src[index] = row[index].FindElement(img).GetAttribute("src");
				text[index] = row[index].FindElement(feed).Text;
				index++;
			}

			Driver.Navigate().GoToUrl(url);

			row = Driver.FindElements(rowList);

			bool isSame = true;
			index = 0;
			foreach (var item in row)
			{
				isSame &= row[index].FindElement(img).GetAttribute("src") == src[index];
				isSame &= row[index].FindElement(feed).Text == text[index];
				index++;
			}

			return isSame;
		}
		[Fact]
		public void DynamicContentTest() =>
			Assert.False(IsSame(@"https://the-internet.herokuapp.com/dynamic_content"));
		[Fact]
		public void StaticContentTest() =>
			Assert.True(IsSame(@"https://the-internet.herokuapp.com/dynamic_content?with_content=static"));

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
