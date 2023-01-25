using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;


namespace InzynierkaTR.Page
{
    /// <summary>
    /// Interaction logic for pageIngridient.xaml
    /// </summary>
    public partial class pageIngridient
    {
        int ingridients = 0;
        List<string> ingridientNames = new List<string>();
        List<ImagePath> images = new List<ImagePath>();
        string root = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public pageIngridient()
        {
            InitializeComponent();
            string path = root + "\\Ingridients.txt";
            using (StreamReader textFile = new StreamReader(path))
            {
                string ln;
                while ((ln = textFile.ReadLine()) != null)
                {
                    {
                        ingridientNames.Add(ln);
                    }
                }
            }
        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            ingridients++;
            string text = "Skladnik" + ingridients.ToString();
            ComboBox myComboBox = new ComboBox() { Text = text };
            
            foreach (string name in ingridientNames)
            {
                myComboBox.Items.Add(name);
            }

            stackPanelRecipe.Children.Add(myComboBox);
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ImageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            ListBoxItem lbi = (ListBoxItem)lb.ItemContainerGenerator.ContainerFromItem(lb.SelectedItem);
            TextBox textBox = GetVisualChild<TextBox>(lbi);
            if (textBox != null)
                textBox.Focus();
            //recipeTitle = textBox.Text;
        }

        private T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            startPage startPage = new startPage();
            this.NavigationService.Navigate(startPage);
        }
        private void buttonBack_MouseEnter(object sender, MouseEventArgs e) { buttonBackText.FontStyle = FontStyles.Italic; }
        private void buttonBack_MouseLeave(object sender, MouseEventArgs e) { buttonBackText.FontStyle = FontStyles.Normal; }


    }
}
