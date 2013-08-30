using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SalesTaxLulz
{
    /// <summary>
    /// Represents an indivdual item type and its associated attributes including
    /// cost, quantity, taxable rate and name.
    /// </summary>
    class Item
    {
        private int quantity; //how many of this item 
        private double cost; //cost of the item
        private double tax_rate; //item's taxable rate
        private string name; //item's parsed name
        private double item_tax; //item's tax amount
        private double final_total; //final cost of item w/ tax

        public Item(int q, double c, string n)
        {
            this.quantity = q;
            this.cost = c;
            this.name = n;
            discernTaxRate();
            this.final_total = (this.quantity * this.cost) + this.item_tax; //calculate the final total
        }

        /// <summary>
        /// Calculates the tax rate of the item (e.g. whether sales tax and/or 
        /// import fee apply) and the actual amount.  
        /// </summary>
        private void discernTaxRate()
        {
            this.tax_rate = 0;
            if (!this.isBookMedicineorFood())
                this.tax_rate += .10;
            if (this.isImport())
                this.tax_rate += .05;

            this.item_tax = (this.quantity * this.cost) * this.tax_rate; //calculate this item's tax rate
            this.item_tax = Math.Ceiling(this.item_tax / .05) * .05; //do the rounding

        }

        /// <summary>
        /// Method to determine whether item has applicable sales tax.  Tries to discern
        /// whether item is a book, medical item, or food.  Unsure of the scope of items,
        /// so this logic would need to be improved to work for other shopping baskets.  
        /// </summary>
        /// <returns>Bool which states whether item is tax exempt (true) or not (false).</returns>
        private bool isBookMedicineorFood()
        {
            if (Regex.Match(this.name, "book", RegexOptions.IgnoreCase).Success)
                return true;
            else if (Regex.Match(this.name, "chocolate", RegexOptions.IgnoreCase).Success)
                return true;
            else if (Regex.Match(this.name, "headache|pills", RegexOptions.IgnoreCase).Success)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method to determine whether item has a 5% import fee applicable to it.
        /// Looks for the word "imported" as per the examples.
        /// </summary>
        /// <returns>Bool which states whether 5% import fee applicable.</returns>
        private bool isImport()
        {
            if (Regex.Match(this.name, "imported", RegexOptions.IgnoreCase).Success)
                return true;
            else
                return false;
        }

        public void Print()
        {
            Console.WriteLine(this.quantity + " " + this.name + ": {0:C2}", this.final_total);
        }

        public double itemsTax()
        {
            return this.item_tax;
        }

        public double itemsFinalCost()
        {
            return this.final_total;
        }
    }
}
