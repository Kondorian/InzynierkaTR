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
    /// Interaction logic for pageDetails.xaml
    /// </summary>
    public partial class pageDetails
    {
        string root = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        string recipeTitle { get; set; }
        public pageDetails(string title)
        {
            InitializeComponent();
            textTitle.Text = title;
            recipeTitle = title;
            AddIngridients();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            pageRecipe pageRecipe = new pageRecipe();
            this.NavigationService.Navigate(pageRecipe);
        }

        void AddIngridients()
        {
            string path = root + "\\Images\\Rozne\\Imprezy\\" + recipeTitle + ".txt";
            using (StreamReader textFile = new StreamReader(path))
            {
                string ln;
                string text = "";

                while ((ln = textFile.ReadLine()) != null)
                {
                    if (ln != "")
                    {
                        text = text + ln + "\n";
                    }
                    else
                    {
                        IngridientList.Text = text;
                        text = "";
                    }
                }
                Instruction.Text = text;
                textFile.Close();
            }
        }

        string fileName(string file)
        {
            string fileName = System.IO.Path.GetFileName(file);
            fileName = fileName.Substring(0, fileName.IndexOf("."));
            return fileName;
        }

        private void buttonBack_MouseEnter(object sender, MouseEventArgs e) { buttonBackText.FontStyle = FontStyles.Italic; }
        private void buttonBack_MouseLeave(object sender, MouseEventArgs e) { buttonBackText.FontStyle = FontStyles.Normal; }
    }
}
