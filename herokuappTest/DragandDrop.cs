using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.ComponentModel;

namespace herokuappTest
{
	public class DragandDrop : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		private readonly By elementA = By.Id("column-a");
		private readonly By elementB = By.Id("column-b");
		private readonly By elements = By.ClassName("column");

		public DragandDrop()
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/drag_and_drop");
		}

		[Fact]
		public void DragAToB()
		{
			var A = Driver.FindElement(elementA);
			var B = Driver.FindElement(elementB);
			var element = Driver.FindElements(elements);
			var textA = A.FindElement(By.TagName("header")).Text;

			int indexB = element.IndexOf(B);

			Actions builder = new(Driver);
			builder.DragAndDrop(A, B).Release().Perform();

			wait.Until((
				x => x.FindElements(elements)[indexB]
					.FindElement(By.TagName("header"))
					.Text == textA)
			);

			var textB = B.FindElement(By.TagName("header")).Text;

			Assert.Equal(textA, textB);
		}
		[Fact]
		public void DragBToA()
		{
			var A = Driver.FindElement(elementA);
			var B = Driver.FindElement(elementB);
			var element = Driver.FindElements(elements);
			var textA = A.FindElement(By.TagName("header")).Text;

			int indexB = element.IndexOf(B);

			Actions builder = new(Driver);
			builder.DragAndDrop(B, A).Release().Perform();

			wait.Until((
				x => x.FindElements(elements)[indexB]
					.FindElement(By.TagName("header"))
					.Text == textA)
			);

			var textB = B.FindElement(By.TagName("header")).Text;

			Assert.Equal(textA, textB);
		}


		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
