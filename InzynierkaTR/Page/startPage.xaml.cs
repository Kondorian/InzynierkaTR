using System;
using System.Collections.Generic;
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

namespace InzynierkaTR.Page
{
    /// <summary>
    /// Interaction logic for startPage.xaml
    /// </summary>
    public partial class startPage
    {
        public startPage()
        {
            InitializeComponent();
        }

        private void listIngridient_Click(object sender, RoutedEventArgs e)
        {
            pageIngridient pageIngridient = new pageIngridient();
            this.NavigationService.Navigate(pageIngridient);
        }

        private void listRecipe_Click(object sender, RoutedEventArgs e)
        {
            pageRecipe pageRecipe = new pageRecipe();
            this.NavigationService.Navigate(pageRecipe);
        }

        private void listFavourite_Click(object sender, RoutedEventArgs e)
        {
            pageHistory pageHistory = new pageHistory();    
            this.NavigationService.Navigate(pageHistory);
        }



        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void buttonBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) { }
        private void buttonBack_MouseEnter(object sender, MouseEventArgs e) { buttonBackText.FontStyle = FontStyles.Italic; }
        private void buttonBack_MouseLeave(object sender, MouseEventArgs e) { buttonBackText.FontStyle = FontStyles.Normal; }
        private void listFavourite_MouseEnter(object sender, MouseEventArgs e) { }
    }
}
