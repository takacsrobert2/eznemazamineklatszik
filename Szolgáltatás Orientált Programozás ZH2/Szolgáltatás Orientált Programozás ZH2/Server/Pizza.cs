using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Pizza
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int price;
        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        private PizzaType type;

        public PizzaType Type
        {
            get { return type; }
            set { type = value; }
        }

        private int size;
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        private bool vegetarian;
        public bool Vegetarian
        {
            get { return vegetarian; }
            set { vegetarian = value; }
        }

        private string company;
        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        private bool delivery;
        public bool Delivery
        {
            get { return delivery; }
            set { delivery = value; }
        }

        private string orderer;
        public string Orderer
        {
            get { return orderer; }
            set { orderer = value; }
        }

        private int GetId()
        {
            int i = 0;
            foreach (Pizza k in Communication.pizzas)
            {
                i++;
            }
            return ++i;
        }

        public Pizza(int price, PizzaType type, int size, bool vegetarian, string company, bool delivery, string orderer)
        {
            this.id = GetId();
            this.Type = type;
            this.Size = size;
            this.Vegetarian = vegetarian;
            this.Company = company;
            this.Delivery = delivery;
            this.Orderer = orderer;
        }


        public override string ToString()
        {
            return string.Format("id:{0} : Pizzatype: {1}; PizzaPrice :{2}, Vegetarian:{3}, Company:{4}, On Delivery:{5}, Orderer:{6}",
                                this.id, this.type, this.price, this.vegetarian, this.company, this.delivery, this.orderer);
        }

    }
}
