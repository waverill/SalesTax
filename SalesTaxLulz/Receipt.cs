using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SalesTaxLulz
{
    /// <summary>
    /// Class to represent a single "receipt" a.k.a output from the transaction.
    /// Contains a list of all the items (which in turn contain their own attributes).
    /// </summary>
    public class Receipt
    {
        private List<Item> item_list; //list of every Item in shopping basket

        public Receipt()
        {
            this.item_list = new List<Item>();
        }

        /// <summary>
        /// Used to parse each line of input and make a new Item with all necessary
        /// information (cost, name, quantity).  
        /// </summary>
        /// <param name="line"></param>
        public void AddParseLine(string line)
        {
            Regex quantity = new Regex("^([0-9]+)"); //regex to grab quantity from input
            Regex cost = new Regex(@"([0-9]+\.[0-9]{2}$)"); //regex to grab the cost
            Regex name = new Regex(@"^[0-9]+\s+([A-Za-z\s]+)\s+at\s+[0-9.]+$"); //regex to grab the name

            //cleaning up and formatting the data from the regex match captures
            double c = Convert.ToDouble(cost.Match(line).Groups[1].Captures[0].Value.ToString()); 
            int q = Convert.ToInt16(quantity.Match(line).Groups[1].Captures[0].Value.ToString()); 
            string n = name.Match(line).Groups[1].Captures[0].Value.ToString();

            this.item_list.Add(new Item(q, c, n)); //make a new Item with above data and add it to list
        }

        /// <summary>
        /// Print the receipt.
        /// </summary>
        public void Print()
        {
            double total = 0;
            double taxes = 0;

            Console.WriteLine("\r\nOUTPUT:");
            foreach (Item i in item_list)
            {
                i.Print();
                total += i.itemsFinalCost();
                taxes += i.itemsTax();
            }
            Console.WriteLine("Sales Tax: {0:C2}", taxes);
            Console.WriteLine("Total: {0:C2}\r\n", total);
        }
    }
}
