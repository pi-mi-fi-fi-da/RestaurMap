﻿using Microsoft.Extensions.Options;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using HtmlAgilityPack;
using RestaurMap.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace RestaurMap.Services;

public class Scrapper
{
    private readonly IRestaurantsService _restaurantsService;
    public Scrapper(IRestaurantsService restaurantsService)
    {
        _restaurantsService = restaurantsService;
        
    }
    public async Task Scrapp()
    {          
        var guard = await _restaurantsService.GetAllAsync(new CancellationTokenSource().Token);
        if (guard.Count<1)

        {
            List<Restaurant> restaurants = new List<Restaurant>();
            var options = new ChromeOptions();
            options.AddArguments(new List<string>() { "headless", "window-size=1200x600" });
            IWebDriver webDriver = new ChromeDriver(options);

            webDriver.Navigate().GoToUrl("https://www.google.pl/maps/search/restauracja+bielsko/@49.9107239,18.9370692,12z/data=!3m1!4b1");
            Thread.Sleep(100);
            IWebElement buttonToClick = webDriver.FindElement(By.XPath("//*[@id=\"yDmH0d\"]/c-wiz/div/div/div/div[2]/div[1]/div[3]/div[1]/div[1]/form[2]/div/div/button"));
            Thread.Sleep(100);
            buttonToClick.Click();
            Thread.Sleep(100);
            string content = webDriver.PageSource;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(content);
            var restaurantList = htmlDocument.DocumentNode.SelectNodes("//a[@class='hfpxzc']");

            foreach (var r in restaurantList)
            {
                Restaurant restaurant = new Restaurant();
                var nestedHtmlDocument = new HtmlDocument();

                string URL = r.GetAttributeValue("href", "No information");
                Regex pattern = new Regex(@"\d\d[.]\d\d\d\d\d\d\d");

                bool janusz = false;
                foreach (Match match in pattern.Matches(URL))
                {
                    if (janusz)
                    {
                        restaurant.CordY = match.Value;
                    }
                    else
                    {
                        restaurant.CordX = match.Value;
                    };
                    janusz = true;
                }

                webDriver.Navigate().GoToUrl(URL);

                string nestedContent = webDriver.PageSource;
                nestedHtmlDocument.LoadHtml(nestedContent);

                var RestaurantInfoNode = nestedHtmlDocument.DocumentNode.SelectNodes("//div[@class='Io6YTe fontBodyMedium kR99db ']");

                var CategoryInfoNode = nestedHtmlDocument.DocumentNode.SelectSingleNode("//button[@class='DkEaL ']");

                //Creating Restaurant 
                restaurant.Name = RestaurantInfoNode[0].InnerHtml;
                restaurant.Adress = RestaurantInfoNode[0].InnerHtml;
                for (int i = 1; i < RestaurantInfoNode.Count; i++)
                {
                    string nodeInnerHtml = RestaurantInfoNode[i].InnerText;

                    if (nodeInnerHtml.Contains('+') && nodeInnerHtml.Contains("Bielsko-Biała"))
                    {
                        restaurant.PlusCode = nodeInnerHtml;
                    }
                    if (nodeInnerHtml.Contains(".pl") || nodeInnerHtml.Contains(".com"))
                    {
                        restaurant.Website = nodeInnerHtml;
                    }
                }
                restaurant.Category = CategoryInfoNode.InnerText;
                await _restaurantsService.CreateAsync(restaurant);
            }
        }
    }
}




