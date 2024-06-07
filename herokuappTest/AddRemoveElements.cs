using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtension.Status;

namespace herokuappTest
{
	public class AddRemoveElements : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		private readonly By addButton = By.XPath("//button[text()='Add Element']");
		private readonly By removeButton = By.ClassName("added-manually");

		public AddRemoveElements()
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/add_remove_elements/");
		}

		[Fact]
		public void AddElementTest()
		{
			Driver.Click(addButton);

			wait.Until(x => x.FindElement(removeButton));

			var remove = Driver.FindElement(removeButton);

			Assert.NotNull(remove);
		}

		[Fact]
		public void AddThreeElementTest()
		{
			var add = Driver.FindElement(addButton);

			int index = 0;
			while(index < 3)
			{
				add.Click();
				index++;
			}

			wait.Until(x => x.FindElement(removeButton));

			var element = Driver.FindElements(removeButton);

			Assert.Equal(index, element.Count);
		}

		[Fact]
		public void RemoveElementTest() 
		{
			var add = Driver.FindElement(addButton);
			add.Click();

			wait.Until(x => x.FindElement(removeButton));

			var element = Driver.FindElement(removeButton);
			element.Click();

			Assert.Throws<NoSuchElementException>(() => Driver.FindElement(removeButton));
		}

		public void Dispose() 
		{
			Driver.Close();
			GC.SuppressFinalize(this);
		}
	}
}
