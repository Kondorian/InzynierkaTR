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
        int ingredients = 0;
        List<string> ingridientNames = new List<string>();
        List<ImagePath> images = new List<ImagePath>();
        string root = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private string recipeTitle { get; set; }

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
            ingredients++;  //Number of ingridients
            string text = "Skladnik" + ingredients.ToString();  //Placeholder text
            Style style = this.FindResource("ComboBox") as Style;   //ComboBox style
            ComboBox myComboBox = new ComboBox() { Text = text};    //ComboBox element
            myComboBox.Style = style;   //Set ComboBox style
            
            foreach (string name in ingridientNames)
            {
                myComboBox.Items.Add(name); //List of existing ingredients
            }
            stackPanelRecipe.Children.Add(myComboBox);  //Add ComboBox to Stack panel
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            string[] igs = new string[ingredients]; //Array of inserted ingredients
            List<bool> bl = new List<bool>();       
            List<string> recipeList = new List<string>();
            int i = 0;

            foreach (var ingridient in stackPanelRecipe.Children.OfType<ComboBox>())
            {   //Iterate through combobox values
                if (ingridient.Text != "")  //If not empty
                {
                    igs[i] = ingridient.Text;   // Save value
                }
                i++;    
            }
            i = 0;


            var files = Directory.GetFiles(System.IO.Path.Combine(root, "Images"), "*.*")
                .Where(s => s.EndsWith(".txt"));    //Get files
            
            foreach (var file in files) // Checking if has ingridient
            {
                bl.Add(false);  //Create bool for this file
                using (StreamReader textFile = new StreamReader(file))
                {               //Read file
                    string ln;
                    while ((ln = textFile.ReadLine()) != null)
                    {   //Read line
                        if (ln == "") { break; }    //Break when stopped reading ingredients
                        int length = ln.IndexOf("|");   //Separator
                        ln = ln.Substring(0, length);   //Separate
                        for (int j = 0; j < ingredients; j++)   //Iterate through user input
                        {
                            if (ln == igs[j])   //If ingredient/recipe match found
                            {
                                bl[i] = true;   //Add file to list
                            }
                        }
                        if (bl[i]) { break; }   //Break if found match
                    }
                    if (bl[i])  //Add recipe to list
                    {
                        recipeTitle = fileName(file);   //File name
                        string path = root + "\\Images\\" + recipeTitle + ".png";   //Check if png exists
                        if (!File.Exists(path))
                        {
                            path = root + "\\Images\\" + recipeTitle + ".jpg";  //If not, use jpg
                            if (!File.Exists(path))
                            {
                                break;          //Error
                            }
                            else
                            {
                                ImagePath id = new ImagePath()
                                {
                                    Path = file,
                                    FileName = fileName(file),
                                };
                                images.Add(id);  //Add image.jpg to recipe
                            }
                        }
                        else
                        {
                            ImagePath id = new ImagePath()
                            {
                                Path = file,
                                FileName = fileName(file),
                            };
                            images.Add(id); //Add image.png to recipe
                        }
                    }
                    i++;
                    textFile.Close();   //Close file
                }
            }
            ImageList.ItemsSource = images;
        }

        string fileName(string file)
        {
            string fileName = System.IO.Path.GetFileName(file);
            fileName = fileName.Substring(0, fileName.IndexOf("."));
            return fileName;
        }

        private void ImageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            ListBoxItem lbi = (ListBoxItem)lb.ItemContainerGenerator.ContainerFromItem(lb.SelectedItem);
            TextBox textBox = GetVisualChild<TextBox>(lbi);
            if (textBox != null)
                textBox.Focus();
            recipeTitle = textBox.Text;
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
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pageDetails pageDetails = new pageDetails(recipeTitle);
            this.NavigationService.Navigate(pageDetails);
        }

        private void buttonBack_MouseEnter(object sender, MouseEventArgs e) { buttonBackText.FontStyle = FontStyles.Italic; }
        private void buttonBack_MouseLeave(object sender, MouseEventArgs e) { buttonBackText.FontStyle = FontStyles.Normal; }


    }
}
