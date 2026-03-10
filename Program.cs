using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioTracker.Models
{
    // Clasa de baza pentru un activ (poate fi actiune, crypto, ETF etc.)
    public class Asset
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal AveragePurchasePrice { get; set; }
        public decimal CurrentPrice { get; set; }

        // Proprietati calculate ca sa le pot lega direct in UI la laborator (Data Binding)
        public decimal TotalInvested => Quantity * AveragePurchasePrice;
        public decimal CurrentValue => Quantity * CurrentPrice;
        public decimal ProfitLoss => CurrentValue - TotalInvested;

        public decimal ProfitLossPercentage
        {
            get
            {
                // Evitam impartirea la zero daca inca n-am bagat bani
                if (TotalInvested == 0) return 0;
                return (ProfitLoss / TotalInvested) * 100;
            }
        }

        public Asset(string symbol, string name, decimal quantity, decimal averagePurchasePrice)
        {
            Symbol = symbol;
            Name = name;
            Quantity = quantity;
            AveragePurchasePrice = averagePurchasePrice;
            CurrentPrice = averagePurchasePrice; // Punem pretul de cumparare ca pret curent la inceput
        }
    }

    // Clasa care tine tot portofoliul la un loc
    public class Portfolio
    {
        public string OwnerName { get; set; }
        public List<Asset> Assets { get; set; }

        // Totale pe intreg portofoliul (folosim LINQ ca e mai rapid de scris)
        public decimal TotalPortfolioValue => Assets.Sum(a => a.CurrentValue);
        public decimal TotalPortfolioInvested => Assets.Sum(a => a.TotalInvested);
        public decimal TotalPortfolioProfitLoss => TotalPortfolioValue - TotalPortfolioInvested;

        public Portfolio(string ownerName)
        {
            OwnerName = ownerName;
            Assets = new List<Asset>();
        }

        // Metoda sa adaugam chestii noi in portofoliu
        // Daca aveam deja actiunea, ii crestem cantitatea si facem media la pret
        public void AddOrUpdateAsset(Asset newAsset)
        {
            var existingAsset = Assets.FirstOrDefault(a => a.Symbol.Equals(newAsset.Symbol, StringComparison.OrdinalIgnoreCase));

            if (existingAsset != null)
            {
                // Calculam noul pret mediu de achizitie (DCA - dollar cost averaging)
                decimal totalCostExisting = existingAsset.Quantity * existingAsset.AveragePurchasePrice;
                decimal totalCostNew = newAsset.Quantity * newAsset.AveragePurchasePrice;

                existingAsset.Quantity += newAsset.Quantity;
                existingAsset.AveragePurchasePrice = (totalCostExisting + totalCostNew) / existingAsset.Quantity;
            }
            else
            {
                // Nu il aveam, il bagam in lista
                Assets.Add(newAsset);
            }
        }

        // Scoatem o actiune din lista dupa simbol (ex: cand vindem tot)
        public void RemoveAsset(string symbol)
        {
            var assetToRemove = Assets.FirstOrDefault(a => a.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));
            if (assetToRemove != null)
            {
                Assets.Remove(assetToRemove);
            }
        }
    }
}


namespace PortfolioTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Testam logica sa vedem daca merg calculele
            var portofoliulMeu = new Models.Portfolio("Test");

            // Bagam niste actiuni de test
            var apple = new Models.Asset("AAPL", "Apple", 10, 150m);
            var bitcoin = new Models.Asset("BTC", "Bitcoin", 0.5m, 60000m);

            portofoliulMeu.AddOrUpdateAsset(apple);
            portofoliulMeu.AddOrUpdateAsset(bitcoin);

            // Printam in consola sa vedem ca totul e ok
            Console.WriteLine($"Portofoliul lui {portofoliulMeu.OwnerName}");
            Console.WriteLine($"Total investit: {portofoliulMeu.TotalPortfolioInvested} USD");

            Console.ReadLine(); // Tine consola deschisa
        }
    }
}