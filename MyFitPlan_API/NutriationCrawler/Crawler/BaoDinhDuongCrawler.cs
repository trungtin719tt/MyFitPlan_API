using HtmlAgilityPack;
using NutriationCrawler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NutriationCrawler.Crawler
{
    class BaoDinhDuongCrawler
    {
        public static async Task<List<FoodViewModel>> runCrawl()
        {
            Console.WriteLine("Start crawl from BaoDinhDuong");
            var foodResult = new List<FoodViewModel>();
            var url = "https://baodinhduong.com/calo-trong-thuc-pham-hang-ngay/";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var tbody = htmlDocument.DocumentNode.
                Descendants("tbody").FirstOrDefault();
            var listTr = tbody.
                Descendants("tr").Where(n => n.GetAttributeValue("bgcolor", "").Equals("")).ToList();
            foreach (var tr in listTr)
            {
                var listTd = tr.
                    Descendants("td").ToArray();
                if (listTd.Count() > 1)
                {
                    var foodCode = "baodinhduong-";
                    for (int i = 0; i < listTd.Length; i++)
                    {
                        foodCode += listTd[i].InnerText.First();
                    }
                    var food = new FoodViewModel()
                    {
                        FoodName = listTd[0].InnerText,
                        Unit = listTd[1].InnerText,
                        Calories = listTd[2].InnerText,
                        Fat = listTd[3].InnerText,
                        Carbs = listTd[4].InnerText,
                        Protein = listTd[5].InnerText,
                        FoodCode = foodCode,
                        fromVN = true
                    };
                    foodResult.Add(food);
                }
            }
            Console.WriteLine("Finish crawl from BaoDinhDuong");
            return foodResult;
        }
    }
}
