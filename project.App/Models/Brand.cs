using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.App.Models
{
    [Serializable]
    internal class Brand:IEquatable<Brand>
    {
        static int counter = 0;

        public bool Equals(Brand other)
        {
            return Id == other.Id;
        }

        public Brand()
        {
            counter++;
            this.Id = counter;
        }

        public Brand(int id)
        {
            this.Id = id;
        }

        public static void SetCounter(int counter)
        {
            Brand.counter = counter;
        }

        public int Id { get; private set;}
        public string BrandName { get; set; }

        public override string ToString()
        {
            return $"{Id} {BrandName}";
        }

    }
}
