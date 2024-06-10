using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace herokuappTest
{
	public class HorizontalSlider : IDisposable
	{
		private ChromeDriver Driver { get; }

		private readonly By sliderContainer = By.XPath("//input[@type='range']");
		private readonly By rangeLabel = By.Id("range");

		public HorizontalSlider()
		{
			Driver = new ChromeDriver();
			Driver.Manage().Window.Maximize();
			Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/horizontal_slider");
		}

		[Theory
			, InlineData(-1, 0f)
			, InlineData(1, 0.5f)
			, InlineData(4, 2f)
			, InlineData(11, 5f)]
		public void SliderChangeTest(int count, float value)
		{
			var slider = Driver.FindElement(sliderContainer);
			var builder = new Actions(Driver);

			/*float step = Convert.ToSingle(slider.GetAttribute("step"));
			float min = Convert.ToSingle(slider.GetAttribute("min"));
			float max = Convert.ToSingle(slider.GetAttribute("max"));*/

			builder
				.MoveToLocation(slider.Location.X, slider.Location.Y)
				.Click();

			if(count > 0)
				for (int i = 0; i < count; i++)
					builder.SendKeys(Keys.ArrowRight);
			else
				for (int i = 0; i > count; i--)
					builder.SendKeys(Keys.ArrowLeft);

			builder.Build().Perform();

			float range = Convert.ToSingle(Driver.FindElement(rangeLabel).Text);

			Assert.Equal(value, range);
		}

		public void Dispose()
		{
			Driver.Quit();
			GC.SuppressFinalize(this);
		}
	}
}
