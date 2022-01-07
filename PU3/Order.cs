using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PU3
{
    public class Order
    {

        public int id = 0;
        public Product[] orderProducts;
        public bool isPayed = false;
        public double orderPrice = 0;
        public string o_date = "";

        public string SerializeOrder()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }

        public Order(Product[] orderList, bool isOrderPayed, int id = 0, string date = "")
        {
            orderProducts = orderList;
            isPayed = isOrderPayed;
            if (orderList != null) {
                foreach (Product p in orderList)
                {
                    orderPrice += p.getPrice();
                }
            }

            if(id != 0)
            {
                this.id = id;
            }
            if(!string.IsNullOrEmpty(date))
            {
                o_date = date; 

            }
        
        }

        public void ChangePayedStatus(bool status)
        {
            this.isPayed = status;
        }

        public void ApplyBdDiscount()
        {
            orderPrice *= 0.9;
        }

        public double getOrderPrice()
        {
            return orderPrice;
        }

        public string getOrderPriceString()
        {
            return orderPrice.ToString() + " €";
        }




    }
}
