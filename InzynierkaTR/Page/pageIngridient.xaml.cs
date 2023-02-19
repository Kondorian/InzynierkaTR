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
            ingridients++;
            string text = "Skladnik" + ingridients.ToString();
            Style style = this.FindResource("ComboBox") as Style;
            ComboBox myComboBox = new ComboBox() { Text = text};
            myComboBox.Style = style;
            
            foreach (string name in ingridientNames)
            {
                myComboBox.Items.Add(name);
            }

            stackPanelRecipe.Children.Add(myComboBox);
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            string[] igs = new string[ingridients];
            List<bool> bl = new List<bool>();
            List<string> recipeList = new List<string>();
            int i = 0;

            foreach (var ingridient in stackPanelRecipe.Children.OfType<ComboBox>())
            {
                if (ingridient.Text != "")
                {
                    igs[i] = ingridient.Text;
                }
                i++;
            }

            var files = Directory.GetFiles(System.IO.Path.Combine(root, "Images"), "*.*")
                .Where(s => s.EndsWith(".txt"));
            i=0;

            foreach (var file in files) // Checking if has ingridient
            {
                bl.Add(false);
                using (StreamReader textFile = new StreamReader(file))
                {
                    string ln;

                    while ((ln = textFile.ReadLine()) != null)
                    {
                        if (ln == "") { break; }
                        int length = ln.IndexOf("|");
                        ln = ln.Substring(0, length);
                        for (int j = 0; j < ingridients; j++)
                        {
                            if (ln == igs[j])
                            {
                                bl[i] = true;
                            }
                        }
                        if (bl[i]) { break; }

                    }
                    if (bl[i])
                    {
                        recipeTitle = fileName(file);
                        string path = root + "\\Images\\" + recipeTitle + ".png";
                        if (!File.Exists(path))
                        {
                            path = root + "\\Images\\" + recipeTitle + ".jpg";
                            if (!File.Exists(path))
                            {
                                break;
                            }
                            else
                            {
                                addFile(path);
                            }
                        }
                        else
                        {
                            addFile(path);
                        }
                        
                    }
                    i++;
                    textFile.Close();
                }
            }
            /*
            var files = Directory.GetFiles(System.IO.Path.Combine(root, "Images"), "*.*")
                .Where(s => s.EndsWith(".jpg") || s.EndsWith(".png"));

            foreach (var file in files)
            {
                ImagePath id = new ImagePath()
                {
                    Path = file,
                    FileName = fileName(file),
                };
                images.Add(id);
            }*/
            ImageList.ItemsSource = images;
        }

        string fileName(string file)
        {
            string fileName = System.IO.Path.GetFileName(file);
            fileName = fileName.Substring(0, fileName.IndexOf("."));
            return fileName;
        }

        void addFile(string file)
        {
            ImagePath id = new ImagePath()
            {
                Path = file,
                FileName = fileName(file),
            };
            images.Add(id);
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
