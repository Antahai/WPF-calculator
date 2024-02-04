using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            else if (sender == Btn2) { 
                LblInput.Content = LblInput.Content + "2";
                TxtInput.Text = TxtInput.Text + "2";
            }
            else if (sender == Btn3) { 
                LblInput.Content = LblInput.Content + "3";
                TxtInput.Text = TxtInput.Text + "3";
            }
            else if (sender == Btn4) { 
                LblInput.Content = LblInput.Content + "4";
                TxtInput.Text = TxtInput.Text + "4";
            }
            else if (sender == Btn5) { 
                LblInput.Content = LblInput.Content + "5";
                TxtInput.Text = TxtInput.Text + "5";
            }
            else if (sender == Btn6) { 
                LblInput.Content = LblInput.Content + "6";
                TxtInput.Text = TxtInput.Text + "6";
            }
            else if (sender == Btn7) { 
                LblInput.Content = LblInput.Content + "7";
                TxtInput.Text = TxtInput.Text + "7";
            }
            else if (sender == Btn8) { 
                LblInput.Content = LblInput.Content + "8";
                TxtInput.Text = TxtInput.Text + "8";
            }
            else if (sender == Btn9) { 
                LblInput.Content = LblInput.Content + "9";
                TxtInput.Text = TxtInput.Text + "9";
            }
            // acrs
            else if (sender == BtnArcOpen) { 
                LblInput.Content = LblInput.Content + "(";
                TxtInput.Text = TxtInput.Text + "(";
            }
            else if (sender == BtnArcClose) { 
                LblInput.Content = LblInput.Content + ")";
                TxtInput.Text = TxtInput.Text + ")";
            }
            // comma
            else if (sender == BtnComma) 
            { 
                LblInput.Content = LblInput.Content + ".";
                TxtInput.Text = TxtInput.Text + ".";
            }

            // operators
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
        private void Button_ArcStartAndEnd(object sender, RoutedEventArgs e)
        {
            LblInput.Content = "(" + LblInput.Content + ")";
            TxtInput.Text = "(" + TxtInput.Text + ")";
        }
        private void BtnArcRnd_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            float floatRnd = rnd.Next(0, 1000000);
            string strRnd = (floatRnd/1000000).ToString();
            string newRnd = "";
            var chars = new[] {','};
            foreach (var ch in strRnd)
            {
                if (ch == chars[0]) newRnd = newRnd + ".";
                
                else newRnd = newRnd + ch.ToString();
            }
            LblInput.Content = LblInput.Content + newRnd;
            TxtInput.Text = TxtInput.Text + newRnd;
        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtInput.Text = "";
        }
        private void BtnEquals_Click(object sender, RoutedEventArgs e)
                {
                    Calculate(true);
                    // LblInput.Content = "";
                    TxtInput.Text = TxtInput.Text + "\n| ";
        }
                private void Calculate(bool acButton)
                {
                    bool formulaOK = true;
                    DataTable dt = new DataTable();
                    if (LblInput.Content.ToString().Length > 0)
                    {
                        string theLastChar = LblInput.Content.ToString().Substring(LblInput.Content.ToString().Length - 1);
                        if (theLastChar == "/" || theLastChar == "+" || theLastChar == "-" || theLastChar == "%" || theLastChar == "*" || theLastChar == "(")
                        {
                            formulaOK = false;
                        }
                    }
                    // Laskutoimitus voidaan toteutta, jos viimeinen merkki ei ole operaattori tai (
                    // Ja kaarisulkuja on parillinen määrä
                    if (formulaOK && CountArcs(LblInput.Content.ToString()))
                    {
                        try
                        {
                            var v = dt.Compute(LblInput.Content.ToString(), "");
                            TxtResult.Text = v.ToString();
                            LblError.Content = "";

                            if (acButton) TxtInput.Text = TxtInput.Text + "=" + v.ToString();



                }
                        catch (Exception)
                        {
                            LblError.Content = "Error!";
                        }
                    }
                }

                private void BtnAc_Click(object sender, RoutedEventArgs e)
                {
                    LblInput.Content = "";
                    TxtResult.Text = "0";
                    TxtInput.Text = TxtInput.Text + "|CLR|\n";
                }

                private void BtnDelLast_Click(object sender, RoutedEventArgs e)
                {
                    if (LblInput.Content.ToString().Length > 0)
                    {
                        LblInput.Content = LblInput.Content.ToString().Remove(LblInput.Content.ToString().Length - 1);
                        TxtInput.Text = TxtInput.Text.Remove(TxtInput.Text.Length - 1);
                    }
                }

                private void BtnNegPos_Click(object sender, RoutedEventArgs e)
                {
                    LblInput.Content = "- (" + LblInput.Content + ")";
                    TxtInput.Text = "- (" + TxtInput.Text + ")";
                }

                // tämä selvittää, onko kaarisulkuja parillinen määrä
                public bool CountArcs(string source)
                {
                    bool isItEven = false;
                    int count = 0;
                    var chars = new[]
                    {
                '(',
                ')'
            };

                    foreach (var ch in source)
                    {
                        if (ch == chars[0] || ch == chars[1])
                        {
                            count++;
                        }
                    }
                    // tutkitaan jääkö jakojäännöstä, jos, niin luku on pariton. Palautetaan false, koska siiloin
                    // kaarisulkuja on pariton määrä
                    if (count % 2 == 0) isItEven = true;
                    return isItEven;
                }


    }
            }
