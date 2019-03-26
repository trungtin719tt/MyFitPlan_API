using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using System.Threading;
using NutriationCrawler.Crawler;
using NutriationCrawler.Model;
using Data;

namespace NutriationCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    var rs =  BaoDinhDuongCrawler.runCrawl().Result;
                    rs.AddRange(MyFitnesspalCrawler.runCrawl().Result);
                    InsertToFood(rs);
                    InsertToFoodNutri(rs);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    
                }
                Thread.Sleep(1000 * 60 * 60 * 24);
            }
        }

        
        static void InsertToFood(List<FoodViewModel> foodResult)
        {
            foreach (var food in foodResult)
            {
                using (var db = new MyFitPlanDBContext())
                {
                    var foodTmp = db.Foods.FirstOrDefault(f => f.FoodCode == food.FoodCode);
                    if (foodTmp == null)
                    {
                        var foodEn = new Food()
                        {
                            FoodCode = food.FoodCode,
                            NameENG = food.fromVN ? "" : food.FoodName,
                            NameVN = food.fromVN ? food.FoodName : "",
                            Unit = food.Unit
                        };
                        db.Foods.Add(foodEn);
                        db.SaveChanges();
                        Console.WriteLine("Success insert Food to Db - Code : " + foodEn.FoodCode);
                    }
                }
            }
        }
        static void InsertToFoodNutri(List<FoodViewModel> foodResult)
        {
            foreach (var food in foodResult)
            {
                using (var db = new MyFitPlanDBContext())
                {
                    var foodTmp = db.Foods.FirstOrDefault(f => f.FoodCode == food.FoodCode);
                    if (foodTmp != null)
                    {
                        foreach (var nutri in db.Nutritions)
                        {
                            var foodNutriTmp = db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodTmp.ID & f.NutritionID == nutri.ID);
                            if (foodNutriTmp == null)
                            {
                                var quantity = "";
                                if (nutri.Name == "Protein")
                                {
                                    quantity = food.Protein;
                                }
                                else if (nutri.Name == "Fat")
                                {
                                    quantity = food.Fat;
                                }
                                else if (nutri.Name == "Carbs")
                                {
                                    quantity = food.Carbs;
                                }
                                else if (nutri.Name == "Calories")
                                {
                                    quantity = food.Calories;
                                }
                                var foodNutriEn = new FoodNutrition()
                                {
                                    FoodID = foodTmp.ID,
                                    NutritionID = nutri.ID,
                                    IsActive = true,
                                    Quantity = quantity
                                };
                                db.FoodNutritions.Add(foodNutriEn);
                                Console.WriteLine("Success insert FoodNutriation to Db - Code : " + food.FoodCode);
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
