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

            TxtInput.IsReadOnly = true;
            //numers
            if (sender == Btn0)
            {
                LblInput.Content = LblInput.Content + "0";
                TxtInput.Text = TxtInput.Text + "0";
            }
            else if (sender == Btn000)
            {
                LblInput.Content = LblInput.Content + "000";
                TxtInput.Text = TxtInput.Text + "000";
            }
            else if (sender == Btn1)
            {
                LblInput.Content = LblInput.Content + "1";
                TxtInput.Text = TxtInput.Text + "1";
            }
            else if (sender == Btn2)
            {
                LblInput.Content = LblInput.Content + "2";
                TxtInput.Text = TxtInput.Text + "2";
            }
            else if (sender == Btn3)
            {
                LblInput.Content = LblInput.Content + "3";
                TxtInput.Text = TxtInput.Text + "3";
            }
            else if (sender == Btn4)
            {
                LblInput.Content = LblInput.Content + "4";
                TxtInput.Text = TxtInput.Text + "4";
            }
            else if (sender == Btn5)
            {
                LblInput.Content = LblInput.Content + "5";
                TxtInput.Text = TxtInput.Text + "5";
            }
            else if (sender == Btn6)
            {
                LblInput.Content = LblInput.Content + "6";
                TxtInput.Text = TxtInput.Text + "6";
            }
            else if (sender == Btn7)
            {
                LblInput.Content = LblInput.Content + "7";
                TxtInput.Text = TxtInput.Text + "7";
            }
            else if (sender == Btn8)
            {
                LblInput.Content = LblInput.Content + "8";
                TxtInput.Text = TxtInput.Text + "8";
            }
            else if (sender == Btn9)
            {
                LblInput.Content = LblInput.Content + "9";
                TxtInput.Text = TxtInput.Text + "9";
            }
            // acrs
            else if (sender == BtnArcOpen)
            {
                LblInput.Content = LblInput.Content + "(";
                TxtInput.Text = TxtInput.Text + "(";
            }
            else if (sender == BtnArcClose)
            {
                LblInput.Content = LblInput.Content + ")";
                TxtInput.Text = TxtInput.Text + ")";
            }
            // comma
            else if (sender == BtnComma)
            {
                LblInput.Content = LblInput.Content + ".";
                TxtInput.Text = TxtInput.Text + ".";
            }

            // ------- operators -----------------------

            else if (sender == BtnMinus)
            {
                Calculate(false);
                LblInput.Content = LblInput.Content + "-";
                TxtInput.Text = TxtInput.Text + "-";
            }
            else if (sender == BtnMultiply)
            {
                Calculate(false);
                LblInput.Content = LblInput.Content + "*";
                TxtInput.Text = TxtInput.Text + "*";
            }
            else if (sender == BtnPros)
            {
                Calculate(false);
                LblInput.Content = LblInput.Content + "%";
                TxtInput.Text = TxtInput.Text + "%";
            }
            else if (sender == BtnPlus)
            {
                Calculate(false);
                LblInput.Content = LblInput.Content + "+";
                TxtInput.Text = TxtInput.Text + "+";
            }
            else
            {
                Calculate(false);
                LblInput.Content = LblInput.Content + "/";
                TxtInput.Text = TxtInput.Text + "/";
            }

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
