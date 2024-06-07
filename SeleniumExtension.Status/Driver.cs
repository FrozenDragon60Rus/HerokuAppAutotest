using OpenQA.Selenium;

namespace SeleniumExtension.Status
{
	public static class Driver
	{
		public static void Click(this IWebDriver driver, By element) =>
			driver.FindElement(element).Click();
		public static bool Exist(this IWebDriver driver, By element)
		{
			try
			{
				driver.FindElement(element);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
