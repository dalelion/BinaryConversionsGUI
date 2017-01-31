using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Text.RegularExpressions;

namespace BinaryConversionsGUI {

    public partial class MainWindow : Window {
        public MainWindow () {
            InitializeComponent();
        }

        private void ToDecimalButton_Click (object sender, RoutedEventArgs e) {
            try {
                ConvertedTextBox.Text = TextFromUserBox.Text != "" && ContainsNumsOnly(TextFromUserBox.Text) && IsBinary(TextFromUserBox.Text) ? ToDecimal(TextFromUserBox.Text) : "Error";
            } catch (Exception) {
                ConvertedTextBox.Text = "Error";
            }
        }

        private void ToBinaryButton_Click (object sender, RoutedEventArgs e) {
            try {
                ConvertedTextBox.Text = TextFromUserBox.Text != "" && ContainsNumsOnly(TextFromUserBox.Text) ? ToBinary(TextFromUserBox.Text) : "Error";
            } catch (Exception) {
                ConvertedTextBox.Text = "Error";
            }
        }

        private void FlipButton_Click (object sender, RoutedEventArgs e) {
            try {
                ConvertedTextBox.Text = !TextFromUserBox.Text.Equals("") && ContainsNumsOnly(TextFromUserBox.Text) ? Flip(TextFromUserBox.Text) : "Error";
            } catch (Exception) {
                ConvertedTextBox.Text = "Error";
            }
        }

        private void TransferButton_Click (object sender, RoutedEventArgs e) {
            TextFromUserBox.Text = !ConvertedTextBox.Text.Equals("Error") ? ConvertedTextBox.Text.Replace(" ", "") : TextFromUserBox.Text;
            ConvertedTextBox.Text = "";
        }

        public static Boolean ContainsNumsOnly (String S) {
            return !new Regex("[^0-9]").IsMatch(S);
        }

        public static Boolean IsBinary (String S) {
            return !new Regex("[^01]").IsMatch(S);
        }

        public static String ToBinary (String S) {
            int Count = 0;
            String Result = "";
            int NumToConvert = Convert.ToInt32(S);

            while (NumToConvert > 0) {
                ++Count;
                Result = Count % 4 == 0 ? " " + NumToConvert % 2 + Result : NumToConvert % 2 + Result;
                NumToConvert /= 2;
            }
            return Result;
        }

        public String ToDecimal (String S) {
            int Result = 0, Count = 0;

            foreach (char x in S.ToString().Reverse()) {
                Result += x.Equals('1') ? Int32.Parse(x.ToString()) * (int)Math.Pow(2, Count) : 0;
                if (x != ' ') ++Count;
            }
            return Result.ToString();
        }

        public static String Flip (String Bin) {
            bool copy = true;
            StringBuilder Flipped = new StringBuilder();

            foreach (char c in Bin.Reverse()) {
                if (copy) {
                    Flipped.Insert(0, c);
                    if (c.Equals('1')) copy = false;
                } else {
                    Flipped.Insert(0, c.Equals('1') ? '0' : '1');
                }
            }
            return Flipped.ToString();
        }
    }
}