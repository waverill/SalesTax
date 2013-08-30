using System;

public class Class1
{
    private double total_cost;
    private double sales_tax;

	public Class1()
	{
        this.total_cost = 0;
        this.sales_tax = 0;
	}

    public void AddParseLine(string line)
    {
        Regex quantity = new Regex("^([0-9]+)");
        Regex import = new Regex("(import)");
        Regex cost = new Regex(@"([0-9]+\.[0-9]{2}$)");

        double c = Convert.ToDouble(cost.Match(l).Groups[1].Captures[0].Value.ToString());
        int q = Convert.ToInt16(quantity.Match(l).Groups[1].Captures[0].Value.ToString());
    }
}
