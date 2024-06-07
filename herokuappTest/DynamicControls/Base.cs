using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace herokuappTest.DynamicControls
{
    public partial class DynamicControls : IDisposable
    {
        private ChromeDriver Driver { get; }

        private readonly WebDriverWait wait;

        public DynamicControls()
        {
            Driver = new ChromeDriver();
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl(@"https://the-internet.herokuapp.com/dynamic_controls");
        }

        public void Dispose()
        {
            Driver.Quit();
            GC.SuppressFinalize(this);
        }
    }
}
