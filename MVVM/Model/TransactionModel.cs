using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storeManagement.MVVM.Model
{
    internal class TransactionModel
    {
        public int ProductNo { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public int TotalPrice { get; set; }

        public TransactionModel(int productNo, string name, int quantity, int price, int totalPrice)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            ProductNo = productNo;
            TotalPrice = totalPrice;
        }
    }
}
