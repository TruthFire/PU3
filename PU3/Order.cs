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

        public Product[] orderProducts;
        public bool isPayed = false;
        public double orderPrice = 0;

        public string SerializeOrder()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }

        public Order(Product[] orderList, bool isOrderPayed)
        {
            /*orderProducts = new OrderItem[orderList.Length];
            for(int i = 0; i < orderList.Length; i++)
            {
                orderProducts[i].ItemId = orderList[i].getId();
                orderProducts[i].ItemName = orderList[i].getName();
            }*/
            orderProducts = orderList;
            isPayed = isOrderPayed; 
            foreach(Product p in orderList)
            {
                orderPrice += p.getPrice();
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
