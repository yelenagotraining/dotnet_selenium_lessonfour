# dotnet_selenium_lessonfour
## Advanced HTML element selection and manipulation

### Partial Link Text
* Make changes to CreditCardApplicationShould.cs file
* Add following test script:
  ```C# 
  driver.FindElement(By.PartialLinkText("- Apply Now!"));
  ```
Search by partial link
  ```C#
    [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink =
                    driver.FindElement(By.PartialLinkText("- Apply Now!"));
                randomGreetingApplyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
```
### Search by absolute xpath
```C#
driver.FindElement(By.XPath("/html/body/div/div[4]/div/p/a"));
```

```C#
 [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting_Using_XPATH()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink =
                    driver.FindElement(By.XPath("/html/body/div/div[4]/div/p/a"));
                randomGreetingApplyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
        ```
        ### Search by relative xpath
        ```C#
    [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting_Using_XPATH()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink =
                    driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]"));
                randomGreetingApplyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
```
### Explicit Waits
* Define how long you want the script to wait for
```C#
  WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
```
* Use the wait until 
```C#
IWebElement applyLink =   wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));
```
* Change BeInitiatedFromHOmePage_EasyApplication.cs to use explicit wait
```C#
 [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement carouselNext =
                    driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselNext.Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                IWebElement applyLink =
                    wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));
                applyLink.Click();

                //IWebElement applyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                //applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
```
### Using pre-built waits
* In order to use ExpectedConditions package we must first install
   ```xml <package id="DotNetSeleniumExtras.WaitHelpers" version="3.11.0" targetFramework="net472" /> ```
  
```C#
  [Fact]
        public void BeInitiatedFromHomePage_EasyApplication_Prebuilt_Conditions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                WebDriverWait wait =
                    new WebDriverWait(driver, TimeSpan.FromSeconds(11));
                IWebElement applyLink =
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
```
### Implicit Waits
```C#
driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(35);
```

```C#
 [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Setting implicit wait");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(35);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to '{HomeUrl}'");
                driver.Navigate().GoToUrl(HomeUrl);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element");
                IWebElement applyLink = 
                    driver.FindElement(By.ClassName("customer-service-apply-now"));

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element Displayed={applyLink.Displayed} Enabled={applyLink.Enabled}");
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
        ```

* Update to explicit wait
  
  ```C#
    [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to '{HomeUrl}'");
                driver.Navigate().GoToUrl(HomeUrl);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element using explicit wait");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));

                IWebElement applyLink =
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("customer-service-apply-now")));

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element Displayed={applyLink.Displayed} Enabled={applyLink.Enabled}");
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
```
### Selecting Multiple Elements

```C#
 [Fact]
        public void DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                ReadOnlyCollection<IWebElement> tableCells = 
                    driver.FindElements(By.TagName("td"));

                Assert.Equal("Easy Credit Card", tableCells[0].Text);
                Assert.Equal("20% APR", tableCells[1].Text);

                Assert.Equal("Silver Credit Card", tableCells[2].Text);
                Assert.Equal("18% APR", tableCells[3].Text);

                Assert.Equal("Gold Credit Card", tableCells[4].Text);
                Assert.Equal("17% APR", tableCells[5].Text);
            }
        }
```