using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace herokuappTest
{
	public class InfiniteScroll : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly By scrollPage = By.ClassName("jscroll-added");

		public InfiniteScroll()
		{
			Driver = new ChromeDriver();
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/infinite_scroll");
		}

		[Theory
			, InlineData(5, 1000)]
		public void ScrollTest(int count, int scrollHeight)
		{
			var builder = new Actions(Driver);
			bool increaseQuantity = true;
			int previewScrollCount, currentScrollCount;

			for (int i = 0; i < count; i++)
			{
				previewScrollCount = Driver.FindElements(scrollPage).Count();
				builder.ScrollByAmount(0, scrollHeight).Perform();
				currentScrollCount = Driver.FindElements(scrollPage).Count();
				increaseQuantity &= previewScrollCount < currentScrollCount;
			}
			
			Assert.True(increaseQuantity);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
