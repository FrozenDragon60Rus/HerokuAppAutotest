using OpenQA.Selenium;
using SeleniumExtension.Status;

namespace herokuappTest.DynamicControls
{
	public partial class DynamicControls : IDisposable
	{
		private readonly By textBoxElement = By.XPath("//form[@id='input-example']/input");
		private readonly By enableButton = By.XPath("//form[@id='input-example']/button");
		private readonly By enableLoadingBar = By.XPath("//form[@id='input-example']/div[@id='loading']");

		private void SwitchTextbox()
		{
			var enable = Driver.FindElement(enableButton);
			enable.Click();

			wait.Until(x => x.Exist(enableLoadingBar) == false);
		}

		[Fact]
		public void EnableTextboxText()
		{
			SwitchTextbox();

			var textbox = Driver.FindElement(textBoxElement);
			var enableFlag = textbox.GetAttribute("disabled");
			Assert.Null(enableFlag);
		}
		[Fact]
		public void DisableTextboxText()
		{
			SwitchTextbox();
			SwitchTextbox();

			var textbox = Driver.FindElement(textBoxElement);
			var enableFlag = textbox.GetAttribute("disabled");
			Assert.NotNull(enableFlag);	
		}
		[Fact]
		public void RemoveTextboxLoadingBarViewTest()
		{
			var enable = Driver.FindElement(enableButton);
			enable.Click();

			var loading = Driver.FindElement(enableLoadingBar);
			Assert.NotNull(loading);
		}
		[Fact]
		public void RemoveTextboxLoadingBarDestroyTest()
		{
			var enable = Driver.FindElement(enableButton);
			enable.Click();

			wait.Until(x => x.Exist(enableLoadingBar) == false);
			Assert.False(Driver.Exist(enableLoadingBar));
		}
	}
}
