using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Windows;

namespace WPF_paiva2_laskin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Button clicks (numbers and operators)
        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            
            TxtInput.IsReadOnly = true; // inputarea is not allowed to be edited

            string btnPressed = sender.ToString().Substring(sender.ToString().Length - 1);      // find out which button was pressed
            if (sender == BtnMultiply) btnPressed = "*";                                        // if button is "X", replace it with "*" for DataTable

            // if operator use calculate function
            if (sender == BtnMinus || sender == BtnPlus || sender == BtnPros || sender == BtnDivide || sender == BtnMultiply) Calculate(false);

            // update input fields
            LblInput.Content = LblInput.Content + btnPressed;
            TxtInput.Text = TxtInput.Text + btnPressed;
        }

        // Arcs in start and end
        private void Button_ArcStartAndEnd(object sender, RoutedEventArgs e)
        {
            LblInput.Content = "(" + LblInput.Content + ")";
            TxtInput.Text = "(" + TxtInput.Text + ")";
        }

        // Random n:o
        private void BtnArcRnd_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            float floatRnd = rnd.Next(0, 1000000);
            string strRnd = (floatRnd / 1000000).ToString();
            string newRnd = "";
            var chars = new[] { ',' };

            // replace char , to .
            foreach (var ch in strRnd)
            {
                if (ch == chars[0]) newRnd = newRnd + ".";

                else newRnd = newRnd + ch.ToString();
            }

            // update label and txtBoc
            LblInput.Content = LblInput.Content + newRnd;
            TxtInput.Text = TxtInput.Text + newRnd;
        }

        // Clear AC - button
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtInput.Text = "";
        }

        // = Button (Equals Button) 
        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            Calculate(true);
            // LblInput.Content = "";
            TxtInput.Text = TxtInput.Text + "\n| ";
        }
        // AC button
        private void BtnAc_Click(object sender, RoutedEventArgs e)
        {
            LblInput.Content = "";
            TxtResult.Text = "0";
            TxtInput.Text = TxtInput.Text + "|CLR|\n";
        }

        // Calculate 
        private void Calculate(bool acButton)
        {
            bool formulaOK = true;
            // Using DataTable, creating new object
            DataTable dt = new DataTable();
            if (LblInput.Content.ToString().Length > 0)
            {
                // Checking if last character is operator or arc. If so, formula is not ok
                string theLastChar = LblInput.Content.ToString().Substring(LblInput.Content.ToString().Length - 1);
                if (theLastChar == "/" || theLastChar == "+" || theLastChar == "-" || theLastChar == "%" || theLastChar == "*" || theLastChar == "(")
                {
                    formulaOK = false;
                }
            }

            // Acts needs to be even number, to be valid
            if (formulaOK && CountArcs(LblInput.Content.ToString()))
            {
                try
                {
                    // using DataTable to calculate formula in string
                    var v = dt.Compute(LblInput.Content.ToString(), "");
                    TxtResult.Text = v.ToString();

                    // setting error label to empty
                    LblError.Content = "";

                    // Txt box is updated as so...
                    if (acButton) TxtInput.Text = TxtInput.Text + "=" + v.ToString();

                }
                catch (Exception)
                {
                    // if error, update error label
                    LblError.Content = "Error!";
                }
            }
        }

        // Remove last character in formula
        private void BtnDelLast_Click(object sender, RoutedEventArgs e)
        {
            if (LblInput.Content.ToString().Length > 0)
            {
                LblInput.Content = LblInput.Content.ToString().Remove(LblInput.Content.ToString().Length - 1);
                TxtInput.Text = TxtInput.Text.Remove(TxtInput.Text.Length - 1);
            }
        }

        // Change formula to negative value
        private void BtnNegPos_Click(object sender, RoutedEventArgs e)
        {
            LblInput.Content = "- (" + LblInput.Content + ")";
            TxtInput.Text = "- (" + TxtInput.Text + ")";
        }

        // for checking out if amount of ( and ) are even
        public bool CountArcs(string source)
        {
            bool isItEven = false;

            int count = 0;
            // creating chars which look for
            var chars = new[]{'(',')'};

            // loop to calculate amount of arcs
            foreach (var ch in source)
            {
                if (ch == chars[0] || ch == chars[1])
                {
                    count++;
                }
            }
            // if remainder is 0, numbe is even, then return true
            if (count % 2 == 0) isItEven = true;   
            return isItEven; // false or true (boolean)
            
        }


    }
}
