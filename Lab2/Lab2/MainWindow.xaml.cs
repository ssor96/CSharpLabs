using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Lab2
{

    public partial class MainWindow : Window
    {
        private DataTable _dt = new DataTable();
        private bool _changed;

        public MainWindow()
        {
            InitializeComponent();
            _changed = false;
        }

        public void Backspace(object sender, RoutedEventArgs e)
        {
            if (display.Text.StartsWith("W"))
            {
                display.Text = "";
                return;
            }
            if (display.Text.Length == 0) return;
            display.Text = display.Text.Remove(display.Text.Length - 1);
            _changed = true;
        }

        public void Clrsc(object sender, RoutedEventArgs e)
        {
            display.Text = "";
            _changed = false;
        }

        public void GetRes(object sender, RoutedEventArgs e)
        {
            if (!_changed)
            {
                return;
            }
            string res = "";
            string op = "";
            try
            {
                double val = Convert.ToDouble(_dt.Compute(display.Text, ""));
                if (Double.IsNaN(val) || Double.IsInfinity(val))
                {
                    throw new Exception();
                }
                op = " = ";
                res = Math.Round(val, 4).ToString().Replace(',', '.');
            }
            catch (Exception)
            {
                op = " -> ";
                res = "Wrong Expression";
            }
            history.Text = display.Text + op + res + Environment.NewLine
                            + Environment.NewLine + history.Text;
            display.Text = res;
            _changed = false;
        }

        public void AddSymb(object sender, RoutedEventArgs e)
        {
            char c = ((Button)e.OriginalSource).Content.ToString()[0];
            if (display.Text.StartsWith("W") || (!_changed && (Char.IsDigit(c) || c == '.')))
            {
                display.Text = "";
            }
            display.Text += c;
            _changed = true;
        }
    }
}
