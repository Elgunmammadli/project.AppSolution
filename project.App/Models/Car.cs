using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace project.App.Models
{
    [Serializable]
    internal class Car:IEquatable<Car>
    {
        static int counter = 0;

        public bool Equals(Car other)
        {
            return Id == other.Id;
        }

        public Car()
        {
            counter++;
            this.Id = counter;
        }

        public Car(int id)
        {
            this.Id = id;
        }
        public static void SetCounter(int counter)
        {
            Car.counter = counter;
        }

        public int Id { get;private set; }
        public int ModelId { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        public string BanNovu { get; set; }
        public string OilType { get; set; }

        public override string ToString()
        {
            return $"{Id} {ModelId} {Year} {Price:0.00}{Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol} {BanNovu} {OilType}";
        }

        public string ToString(Model m)
        {
            string data = (m == null) ? null : $"{m.ModelName}";
            return $"{Id} \"{data?? this.ModelId.ToString()}\" {Year} {Price:0.00}{Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol} {BanNovu} {OilType}";
        }
    }
}
