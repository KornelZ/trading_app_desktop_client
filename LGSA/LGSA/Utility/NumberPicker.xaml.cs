﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace LGSA.Utility
{
    /// <summary>
    /// Interaction logic for NumberPicker.xaml
    /// </summary>
    public partial class NumberPicker : UserControl
    {
        int minvalue = 1,
        maxvalue = 100,
        startvalue = 1;
        public NumberPicker()
        {
            InitializeComponent();
            NUDTextBox.Text = startvalue.ToString();
        }

        public static DependencyProperty buttTextProperty =
DependencyProperty.Register("buttText", typeof(String),
typeof(NumberPicker), new FrameworkPropertyMetadata(string.Empty));

        public String buttText
        {
            get { return GetValue(buttTextProperty).ToString(); }
            set { SetValue(buttTextProperty, value); }
        }

        private void NUDButtonUP_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox.Text != "") number = Convert.ToInt32(NUDTextBox.Text);
            else number = 0;
            if (number < maxvalue)
                NUDTextBox.Text = Convert.ToString(number + 1);
        }

        private void NUDButtonDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox.Text != "") number = Convert.ToInt32(NUDTextBox.Text);
            else number = 0;
            if (number > minvalue)
                NUDTextBox.Text = Convert.ToString(number - 1);
        }

        private void NUDTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                NUDButtonUP.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                NUDButtonDown.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { true });
            }
        }

        private void NUDTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { false });
        }

        private void NUDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (NUDTextBox.Text != "")
                if (!int.TryParse(NUDTextBox.Text, out number)) NUDTextBox.Text = startvalue.ToString();
            if (number > maxvalue) NUDTextBox.Text = maxvalue.ToString();
            if (number < minvalue) NUDTextBox.Text = minvalue.ToString();
            NUDTextBox.SelectionStart = NUDTextBox.Text.Length;
            buttText = NUDTextBox.Text;
        }
    }
}
