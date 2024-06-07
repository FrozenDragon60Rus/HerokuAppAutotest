using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtension.Status;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace herokuappTest
{
	public class FormAuthentication : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly WebDriverWait wait;

		private readonly By logoutButton = By.LinkText("Logout");
		private readonly By infoPanel = By.Id("flash");
		private readonly By usernameField = By.Id("username");
		private readonly By passwordField = By.Id("password");
		private readonly By loginButton = By.ClassName("radius");

		public FormAuthentication()
		{
			Driver = new ChromeDriver();
			wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/login");
		}
		
		private void Login(string user, string pass)
		{
			var username = Driver.FindElement(usernameField);
			var password = Driver.FindElement(passwordField);

			username.SendKeys(user);
			password.SendKeys(pass);

			Driver.Click(loginButton);
		}
		private void Logout()
		{
			Driver.Click(logoutButton);

			wait.Until(x => x.Exist(loginButton));
		}

		[Fact]
		public void LoginSuccessTest()
		{
			Login("tomsmith", "SuperSecretPassword!");

			wait.Until(x => x.Exist(logoutButton));

			var flash = Driver.FindElement(infoPanel);
			string info = flash.Text;

			Logout();

			string result = "You logged into a secure area!";
			Assert.Contains(result, info);
		}

		[Theory
			, InlineData("tomsmit", "SuperSecretPassword!", "Your username is invalid!")
			, InlineData(@"<script>alert('test');</script>", "SuperSecretPassword!", "Your username is invalid!")
			, InlineData("tomsmith", "SuperSecretPassword!^", "Your password is invalid!")]
		public void LoginErrorTest(string user, string pass, string result)
		{
			Login(user, pass);

			var flash = Driver.FindElement(infoPanel);
			string info = flash.Text;

			Assert.Contains(result, info);
		}
		[Fact]
		public void LooutTest()
		{
			Login("tomsmith", "SuperSecretPassword!");

			wait.Until(x => x.Exist(logoutButton));

			Logout();

			var flash = Driver.FindElement(infoPanel);
			string info = flash.Text;

			string result = "You logged out of the secure area!";

			Assert.Contains(result, info);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
