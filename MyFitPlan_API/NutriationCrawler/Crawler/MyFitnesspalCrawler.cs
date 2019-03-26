using HtmlAgilityPack;
using NutriationCrawler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NutriationCrawler.Crawler
{
    class MyFitnesspalCrawler
    {
        public static async Task<List<FoodViewModel>> runCrawl()
        {
            Console.WriteLine("Start crawl from MyFitnesspal");
            var foodResult = new List<FoodViewModel>();
            var url = "https://www.myfitnesspal.com/nutrition-facts-calories/vietnamese";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var resultCount = htmlDocument.DocumentNode.
                Descendants("div").
                FirstOrDefault(n => n.GetAttributeValue("class", "").Equals("result_count")).InnerText.Trim().
                Replace("Showing results 1 through", "").Replace("of", "-").Split('-');
            var total = Int32.Parse(resultCount[1].Trim());
            var perPage = Int32.Parse(resultCount[0].Trim());
            var pageNum = total / perPage + 1;
            for (int i = 1; i <= pageNum; i++)
            {
                await startCrawlAsync(url + "/" + i, httpClient, foodResult);
                Thread.Sleep(2000);
            }
            Console.WriteLine("Finish crawl from MyFitnesspal");
            return foodResult;
        }

        public static async Task startCrawlAsync(string url, HttpClient httpClient, List<FoodViewModel> foodResult)
        {
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var listLi = htmlDocument.DocumentNode.
                Descendants("ul").
                FirstOrDefault(n => n.GetAttributeValue("class", "").Equals("food_search_results")).
                Descendants("li").
                Where(n => n.GetAttributeValue("class", "").Equals("odd") || n.GetAttributeValue("class", "").Equals("even")).
                ToList();
            foreach (var li in listLi)
            {
                var foodInfo = li.
                    Descendants("div").
                    FirstOrDefault(n => n.GetAttributeValue("class", "").Equals("food_info")).
                    Descendants("a").
                    FirstOrDefault(n => n.GetAttributeValue("class", "").Equals(""));

                var foodCode = "myfitnesspal-" + foodInfo.ChildAttributes("href").FirstOrDefault().Value.Replace("/food/calories/", "");
                var foodName = foodInfo.InnerText.Trim();
                var nutri = li.
                    Descendants("div").
                    FirstOrDefault(n => n.GetAttributeValue("class", "").Equals("nutritional_info"))
                    .InnerText.Trim().Split(',');
                var food = new FoodViewModel()
                {
                    FoodName = foodName,
                    FoodCode = foodCode,
                    Unit = nutri[0].Replace("Serving Size:", "").Trim(),
                    Calories = nutri[1].Replace("Calories:", "").Trim().Replace("g",""),
                    Carbs = nutri[3].Replace("Carbs:", "").Trim().Replace("g", ""),
                    Fat = nutri[2].Replace("Fat:", "").Trim().Replace("g", ""),
                    Protein = nutri[4].Replace("Protein:", "").Trim().Replace("g", ""),
                    fromVN = false
                };
                Console.WriteLine("Success crawl Food " + foodCode);
                foodResult.Add(food);
            }
        }
    }
}
