using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAppNetCore.Models;

namespace WebAppNetCore.Controllers
{
    public class SeedController : Controller
    {
        private readonly DataContext context;

        public SeedController(DataContext _context) => context = _context;

        public IActionResult Index()
        {
            ViewBag.Count = context.Products.Count();

            //OrderBy().Take(x) - wezmie x pierwszych rekordow
            //Take(x).OrderBy() - wezmie x randomowych rekordow
            return View(context.Products.Include(p => p.Category).OrderBy(p => p.Id).Take(20));
        }

        [HttpPost]
        public IActionResult CreateSeedData(int count)
        {
            ClearData();
            if (count > 0)
            {
                context.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));

                //W programie LINQ to SQL zarządza tożsamością obiektu DataContext.
                //Za każdym razem, gdy pobierasz nowy wiersz z bazy danych, wiersz jest rejestrowany w tabeli
                //tożsamości według jego klucza podstawowego i tworzony jest nowy obiekt. Po każdym pobraniu
                //tego samego wiersza wystąpienie oryginalnego obiektu zostanie przekazane do aplikacji.
                //W ten sposób DataContext tłumaczy koncepcję tożsamości widzianą przez bazę danych (klucze podstawowe)
                //do koncepcji tożsamości widocznej dla języka (wystąpień).

                //SET NOCOUNT ON - zapobiega wyświetlaniu komunikatu pokazującego, na ilu wierszach działa instrukcja
                //CONCAT - laczy ze soba dwa ciagi znakow
                //SCOPE_IDENTITY - zwraca ostatnia wartosc tozsamosci w biezacym zakresie wykonywania

                context.Database.ExecuteSqlRaw($@"
                        CREATE PROCEDURE CreateSeedData2
                                @RowCount decimal
                        AS
                                BEGIN
                                SET NOCOUNT ON
                            DECLARE @i INT = 1;
                                DECLARE @catId BIGINT;
                                DECLARE @CatCount INT = @RowCount / 10;
                                DECLARE @pprice DECIMAL(5,2);
                                DECLARE @rprice DECIMAL(5,2);
                                BEGIN TRANSACTION
                                    WHILE @i <= @CatCount
                                        BEGIN
                                            INSERT INTO Categories (Name, Description)
                                            VALUES (CONCAT('Category-', @i), 'Test Data Category');
                                            SET @catId = SCOPE_IDENTITY();
                                            DECLARE @j INT = 1;
                                            WHILE @j <= 10
                                                BEGIN
                                                    SET @pprice = RAND()*(500-5+1);
                                                    SET @rprice = (RAND() * @pprice) + @pprice;
                                                    INSERT INTO Products (Name, CategoryId, PurchasePrice, RetailPrice)
                                                    VALUES (CONCAT('Product', @i, '-', @j), @catId, @pprice, @rprice)
                                                    SET @j = @j + 1
                                                END
                                        SET @i = @i + 1
                                        END
                                COMMIT
                        END");
                context.Database.BeginTransaction();
                context.Database.ExecuteSqlRaw($"EXEC CreateSeedData @RowCount = {count}");
                context.Database.CommitTransaction();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ClearData()
        {
            context.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));
            context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw("DELETE FROM Orders");
            context.Database.ExecuteSqlRaw("DELETE FROM Categories");
            context.Database.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }

        //seedowanie copy & paste
        [HttpPost]
        public IActionResult CreateProductionData()
        {
            ClearData();
            context.Categories.AddRange(new Category[] {
                 new Category {
                     Name = "Watersports",
                     Description = "Make a splash",
                     Products = new Product[] {
                        new Product {
                            Name = "Kayak", Description = "A boat for one person",
                            PurchasePrice = 200, RetailPrice = 275
                        },
                        new Product {
                            Name = "Lifejacket",
                            Description = "Protective and fashionable",
                            PurchasePrice = 40, RetailPrice = 48.95m
                         },
                     }
                 },
                 new Category {
                     Name = "Soccer",
                     Description = "The World's Favorite Game",
                     Products = new Product[] {
                        new Product {
                            Name = "Soccer Ball",
                            Description = "FIFA-approved size and weight",
                            PurchasePrice = 18, RetailPrice = 19.50m
                        },
                        new Product {
                            Name = "Corner Flags", 
                            Description = "Give your playing field a professional touch",
                            PurchasePrice = 32.50m, RetailPrice = 34.95m
                         },
                        new Product {
                            Name = "Stadium",
                            Description = "Flat-packed 35,000-seat stadium",
                            PurchasePrice = 75000, RetailPrice = 79500
                        }
                    }
                 },
                 new Category {
                     Name = "Chess",
                     Description = "The Thinky Game",
                     Products = new Product[] {
                        new Product {
                             Name = "Thinking Cap",
                             Description = "Improve brain efficiency by 75%",
                             PurchasePrice = 10, RetailPrice = 16
                        },
                         new Product {
                             Name = "Unsteady Chair", 
                             Description = "Secretly give your opponent a disadvantage",
                             PurchasePrice = 28, RetailPrice = 29.95m
                         },
                        new Product {
                             Name = "Human Chess Board",
                             Description = "A fun game for the family",
                             PurchasePrice = 68.50m, RetailPrice = 75
                         },
                        new Product {
                            Name = "Bling-Bling King",
                            Description = "Gold-plated, diamond-studded King",
                            PurchasePrice = 800, RetailPrice = 1200
                        }
                    }
                 }
             });
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}