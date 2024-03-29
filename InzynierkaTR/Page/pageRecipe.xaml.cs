﻿using Microsoft.Win32;
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
    /// Interaction logic for pageRecipe.xaml
    /// </summary>
    /// 
    public partial class pageRecipe
    {
        List<ImagePath> images = new List<ImagePath>();
        string root = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public pageRecipe()
        {
            InitializeComponent();
            AddImages();
            ImageList.ItemsSource = images;
        }

        private string recipeTitle { get; set; }

        void AddImages()
        {
            var files = Directory.GetFiles(System.IO.Path.Combine(root, "Images"), "*.*")
                .Where(s => s.EndsWith(".jpg") || s.EndsWith(".png"));  //List of jpg/png files

            foreach (var file in files)     //Iterate through
            {
                ImagePath id = new ImagePath()  //New class:
                {
                    Path = file,                //Path to image
                    FileName = fileName(file),  //Image name
                };
                images.Add(id);                 //Add object to UI
            }
        }

        string fileName(string file)
        {
            string fileName = System.IO.Path.GetFileName(file);     //Get Filename with extension
            fileName = fileName.Substring(0, fileName.IndexOf("."));//Remove extension
            return fileName;    //Return name
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pageDetails pageDetails = new pageDetails(recipeTitle);
            this.NavigationService.Navigate(pageDetails);
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            startPage startPage = new startPage();
            this.NavigationService.Navigate(startPage);
        }


        private void buttonBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) { }
        private void buttonBack_MouseEnter(object sender, MouseEventArgs e) { buttonBackText.FontStyle = FontStyles.Italic; }
        private void buttonBack_MouseLeave(object sender, MouseEventArgs e) { buttonBackText.FontStyle = FontStyles.Normal; }
        private void Grid_MouseEnter(object sender, MouseEventArgs e) { }
        private void buttonStackpanel_Click(object sender, RoutedEventArgs e)
        {
            
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
    }
}
