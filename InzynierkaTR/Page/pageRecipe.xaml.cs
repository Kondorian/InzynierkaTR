﻿using System;
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
    /// Interaction logic for pageRecipe.xaml
    /// </summary>
    public partial class pageRecipe
    {
        public pageRecipe()
        {
            InitializeComponent();
        }

        /* TODO: Implement functions to save Images + add favorite bool
        void AddImages()
        {
            var files = Directory.GetFiles(System.IO.Path.Combine(root, "Images\\Rozne\\Imprezy"), "*.*");

            foreach (var file in files)
            {
                ImagePath id = new ImagePath()
                {
                    Path = file,
                    FileName = System.IO.Path.GetFileName(file),

                };
                images.Add(id);
            }


        }

        void CopyImages()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Image jpeg(*.jpg)|*.jpg|Image png(*.png)|*.png";
            ofd.DefaultExt = ".jpeg";

            // Process open file dialog box results 
            if (ofd.ShowDialog() == true)
            {
                var filename = ofd.FileName; // Get the filename from absolute path
                var file = System.IO.Path.GetFullPath(ofd.FileName);

                var filename2 = file.Substring(file.LastIndexOf("\\") + 1);
                string _finalPath = System.IO.Path.Combine(root, "Images\\Rozne\\Imprezy").ToString(); //it is the destination folder path e.g,C:\Users\Neha\Pictures\11-03-2014
                if (Directory.Exists(_finalPath))
                {

                    _finalPath = System.IO.Path.Combine(_finalPath, filename2);

                    System.IO.File.Copy(file, _finalPath, true);
                }

            }
        }*/
    }
}
