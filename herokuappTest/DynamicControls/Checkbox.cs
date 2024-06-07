using OpenQA.Selenium;
using SeleniumExtension.Status;

namespace herokuappTest.DynamicControls
{
	public partial class DynamicControls : IDisposable
	{
		private readonly By checkboxPanel = By.Id("checkbox");
		private readonly By checkboxElement = By.XPath("//div[@id='checkbox']/input");
		private readonly By addRemoveButton = By.XPath("//form[@id='checkbox-example']/button");
		private readonly By checkboxLoadingBar = By.XPath("//form[@id='checkbox-example']/div[@id='loading']");

		private void RemoveCheckBox()
		{
			Driver.Click(addRemoveButton);

			wait.Until(x => x.Exist(checkboxPanel) == false);
		}
		private void AddCheckBox()
		{
			Driver.Click(addRemoveButton);

			wait.Until(x => x.FindElement(checkboxLoadingBar).Displayed == false);
		}

		[Fact]
		public void RemoveUnckeackedCheckboxTest()
		{
			RemoveCheckBox();

			Assert.Throws<NoSuchElementException>(() => Driver.FindElement(checkboxPanel));
		}
		[Fact]
		public void RemoveCkeackedCheckboxTest()
		{
			Driver.Click(checkboxElement);

			RemoveCheckBox();

			Assert.Throws<NoSuchElementException>(() => Driver.FindElement(checkboxPanel));
		}
		[Fact]
		public void AddCheckboxPanelTest()
		{
			var tagnameBefore = Driver.FindElement(checkboxPanel).TagName;

			RemoveCheckBox();
			AddCheckBox();

			var tagnameAfter = Driver.FindElement(checkboxPanel).TagName;
			Assert.Equal(tagnameBefore, tagnameAfter);
		}
		[Fact]
		public void AddedCheckboxPositionTest()
		{
			var chackbox = Driver.FindElement(checkboxPanel);

			int indexBefore = Driver
				.FindElements(By.XPath("//form[@id='checkbox-example']//following-sibling::*"))
				.IndexOf(chackbox);

			RemoveCheckBox();
			AddCheckBox();

			int indexAfter = Driver
				.FindElements(By.XPath("//form[@id='checkbox-example']//following-sibling::*"))
				.IndexOf(chackbox);

			Assert.Equal(indexBefore, indexAfter);
		}
		[Fact]
		public void RemoveCheckboxLoadingBarViewTest()
		{
			Driver.Click(addRemoveButton);

			var loading = Driver.FindElement(checkboxLoadingBar);
			Assert.NotNull(loading);
		}
		[Fact]
		public void RemoveCheckboxLoadingBarDestroyTest()
		{
			Driver.Click(addRemoveButton);

			wait.Until(x => x.Exist(checkboxLoadingBar) == false);
			Assert.False(Driver.Exist(checkboxLoadingBar));
		}
	}
}
