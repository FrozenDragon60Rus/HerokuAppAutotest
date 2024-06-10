using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Xunit.Sdk;

namespace herokuappTest
{
	public class Hovers : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly By figureList = By.ClassName("figure");
		private readonly By figcaptionPanel = By.ClassName("figcaption");

		public Hovers()
		{
			Driver = new ChromeDriver();
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/hovers");
		}

		[Fact]
		public void HoverTest()
		{
			var figure = Driver.FindElements(figureList);
			
			var builder = new Actions(Driver);

			bool displayCaption = true;
			foreach(var currentFigure in figure)
			{
				builder.MoveToElement(currentFigure).Perform();
				displayCaption &= currentFigure.Displayed;
			}

			Assert.True(displayCaption);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
