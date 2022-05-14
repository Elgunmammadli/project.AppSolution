using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.App.Models
{
    [Serializable]
    internal class Model:IEquatable<Model>
    {
        static int counter = 0;

        public bool Equals(Model other)
        {
            return Id == other.Id;
        }

        public Model()
        {
            counter++;
            this.Id = counter;
        }
        public Model(int id)
        {
            this.Id = id;
        }

        public static void SetCounter(int counter)
        {
            Model.counter = counter;
        }

        public int Id { get;private set; }
        public string ModelName { get; set; }

        public int BrandId { get; set; }

        public override string ToString()
        {
            return $"{Id} {ModelName} {BrandId}";
        }
        public  string ToString(Brand b)
        {
            string data = (b == null) ? null : $"{b.BrandName}";
            return $"{Id} {ModelName} \"{data ?? this.BrandId.ToString()}\"";
        }
    }
}
