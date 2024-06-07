using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace herokuappTest
{
	public class DigestAuthentication : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		public DigestAuthentication()
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
		}

		[Theory
			, InlineData("admin", "admin")]
		public void AuthorizationTest(string userName, string password)
		{
			string url = @"the-internet.herokuapp.com/digest_auth";
			string AuthUrl = $"http://{userName}:{password}@{url}";

			Driver.Navigate().GoToUrl(AuthUrl);

			var example = By.ClassName("example");

			wait.Until(x => x.FindElement(example));

			var congratulation = Driver.FindElement(example);

			Assert.NotNull(congratulation);
		}

		public void Dispose()
		{
			Driver.Close();
			GC.SuppressFinalize(this);
		}
	}
}
