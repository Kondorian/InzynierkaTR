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
    /// Interaction logic for pageHistory.xaml
    /// </summary>
    public partial class pageHistory
    {
        string _finalPath = System.IO.Path.Combine(
            System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
            "Images\\").ToString();
        string root = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        int ingridients = 0;
        string recipeFilename;  //Path
        string recipeName;      //Name
        public pageHistory()
        {
            InitializeComponent();
        }

        private string CopyImages()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Image jpeg(*.jpg)|*.jpg|Image png(*.png)|*.png";
            ofd.DefaultExt = ".jpeg";

            // Process open file dialog box results 
            if (ofd.ShowDialog() == true)
            {
                recipeFilename = ofd.FileName;

                
                var file = System.IO.Path.GetFullPath(ofd.FileName);

                var filename2 = file.Substring(file.LastIndexOf("\\") + 1);
                recipeName = filename2.Substring(0, filename2.IndexOf("."));
                return filename2;
            }
            return null;
        }

        private void buttonRecipe_Click(object sender, RoutedEventArgs e)
        {
            
            buttonTextRecipe.Text = CopyImages();
            //CopyImages, get filename
        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            //Add Textbox to Stackpanel
            ingridients++;
            string text = "Skladnik " + ingridients.ToString();
            TextBox myTextBox = new TextBox() { Text = text};
            stackPanelRecipe.Children.Add(myTextBox);

            text = "Ilosc" + ingridients.ToString();
            myTextBox = new TextBox() { Text = text};
            stackPanelWeight.Children.Add(myTextBox);
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(_finalPath))
            {
                var filename2 = recipeFilename.Substring(recipeFilename.LastIndexOf("\\") + 1);

                System.IO.File.Copy(recipeFilename, System.IO.Path.Combine(_finalPath, filename2), true);
            }

            string[] ingridientText = new string[ingridients+2];
            int i = 0;

            foreach (var ingridient in stackPanelRecipe.Children.OfType<TextBox>())
            {
                ingridientText[i] = ingridient.Text;
                addIngridientToList(ingridient.Text);
                i++;
            }
            i = 0;
            foreach (var ingridient in stackPanelWeight.Children.OfType<TextBox>())
            {
                ingridientText[i] = ingridientText[i] + "|" + ingridient.Text;
                i++;
            }
            ingridientText[i] = "";
            ingridientText[i+1] = instructionBox.Text;

            SaveFile(ingridientText, recipeName, _finalPath);

            startPage startPage = new startPage();
            this.NavigationService.Navigate(startPage);
        }

        public static async Task SaveFile(string[] iText, string recipeName, string _finalPath)
        {
            string name = recipeName + ".txt";
            using StreamWriter file = new(_finalPath + name);
            foreach (string line in iText)
            {
                await file.WriteLineAsync(line);
            }
        }

        void addIngridientToList(string ig)
        {
            bool isNew = true;
            using (StreamReader textFile = new StreamReader(root + "\\Ingridients.txt"))
            {
                string ln;
                while ((ln = textFile.ReadLine()) != null)
                {
                    if (ln == ig)
                    {
                        isNew = false;
                        break;
                    }
                }
            }
            if (isNew)
            {
                using (StreamWriter sw = File.AppendText(root + "\\Ingridients.txt"))
                {
                    sw.Write(ig + "\n");
                }
            }
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
