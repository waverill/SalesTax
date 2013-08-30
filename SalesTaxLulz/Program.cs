using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SalesTaxLulz
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex exit_cmd = new Regex("^quit$", RegexOptions.IgnoreCase); //used to match user input to exit command
            Regex txt_file = new Regex(@"^.*\.txt$"); //used to ensure .txt extension of input file name 

            string user_input = ""; //initilization
            Console.WriteLine("Sales Tax Calculator v0.1\r\n_________________________\r\n"); //l33t ascii art
            Console.WriteLine("Note: You may type 'quit' at anytime to exit this program.\r\n\r\n");  //readme goes here
            Receipt rec = new Receipt(); //initialize

            while (!exit_cmd.Match(user_input).Success) //don't stop until they've had enough!
            {
                Console.WriteLine("Please enter the name of input text file: ");
                user_input = Console.ReadLine();
                if (txt_file.Match(user_input).Success) //use our regex to validate input ends with .txt
                {
                    try
                    {
                        rec = new Receipt(); //make a new receipt
                        string line = "";
                        System.IO.StreamReader file_stream = new System.IO.StreamReader(user_input);
                        while ((line = file_stream.ReadLine()) != null) //read each line
                        {
                            rec.AddParseLine(line); //parse it and send it to receipt
                        }
                        file_stream.Close(); //close file stream since we're done
                        rec.Print(); //print out the receipt
                    }
                    catch (Exception e) //catch any I/O exception here
                    { 
                        Console.WriteLine(e.Message); //explain Exception
                    }
                }
                else if (!exit_cmd.Match(user_input).Success) //exit with error code
                {
                    Console.WriteLine("\r\nERROR! Please ensure the file you have specified ends with a .txt extension.\r\n");
                }
            }
        }
    }
}
