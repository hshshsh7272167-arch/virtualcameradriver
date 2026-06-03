using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace VirtualCameraDriver
{
    public partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Title = "Virtual Camera Driver - Static Image Stream";
            this.Width = 600;
            this.Height = 500;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(240, 240, 240));
        }
    }
}
