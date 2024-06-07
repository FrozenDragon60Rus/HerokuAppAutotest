using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Xunit.Abstractions;

namespace herokuappTest
{
	public class DisappearingElements : IDisposable
	{
		private ChromeDriver Driver { get; }
		private ITestOutputHelper Output { get; }

		private readonly By homeReference = By.XPath("//a[text()='Home']");
		private readonly By aboutReference = By.XPath("//a[text()='About']");
		private readonly By contactReference = By.XPath("//a[text()='Contact Us']");
		private readonly By portfolioReference = By.XPath("//a[text()='Portfolio']");
		private readonly By galleryReference = By.XPath("//a[text()='Gallery']");

		public DisappearingElements(ITestOutputHelper output) 
		{
			Driver = new ChromeDriver();
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/disappearing_elements");
			Output = output;
		}

		[Theory
			,InlineData(10)]
		public void LoadReferenceTest(int index)
		{
			bool IsLoad = true;

			try
			{
				while (index-- > 0)
				{
					Driver.Navigate().Refresh();
					_ = Driver.FindElement(homeReference);
					_ = Driver.FindElement(aboutReference);
					_ = Driver.FindElement(contactReference);
					_ = Driver.FindElement(portfolioReference);
					_ = Driver.FindElement(galleryReference);
				}
			}
			catch
			{
				IsLoad = false;
				Output.WriteLine(index.ToString());
			}

			Assert.True(IsLoad);
		}
		[Fact]
		public void HomeTest()
		{
			var home = Driver.FindElement(homeReference);
			home.Click();

			Assert.Equal(@"https://the-internet.herokuapp.com/", Driver.Url);
		}
		[Fact]
		public void AboutTest()
		{
			var about = Driver.FindElement(aboutReference);
			about.Click();

			Assert.Equal(@"https://the-internet.herokuapp.com/about/", Driver.Url);
		}
		[Fact]
		public void ContactTest()
		{
			var contact = Driver.FindElement(contactReference);
			contact.Click();

			Assert.Equal(@"https://the-internet.herokuapp.com/contact-us/", Driver.Url);
		}
		[Fact]
		public void PortfolioTest()
		{
			var portfolio = Driver.FindElement(portfolioReference);
			portfolio.Click();

			Assert.Equal(@"https://the-internet.herokuapp.com/portfolio/", Driver.Url);
		}
		[Fact]
		public void GalleryTest()
		{
			var gallery = Driver.FindElement(galleryReference);
			gallery.Click();

			Assert.Equal(@"https://the-internet.herokuapp.com/gallery/", Driver.Url);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
