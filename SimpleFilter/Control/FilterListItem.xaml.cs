using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SimpleFilter.Control
{
    public partial class FilterListItem : UserControl
    {

        //public event EventHandler OnChooseFilter;

        public FilterListItem()
        {
            InitializeComponent();
        }

        private bool _isActive = false;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                if (_isActive) bgBorder.BorderBrush = new SolidColorBrush(Colors.Orange);
                else bgBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            }
        }

        private string _imageSource = "";
        public string ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                image.Source = new BitmapImage(new Uri(_imageSource, UriKind.RelativeOrAbsolute));
            }
        }

        private Filter _filterType = Filter.AMARO;
        public Filter FilterType
        {
            get
            {
                return _filterType;
            }
            set
            {
                _filterType = value;
            }
        }

        private string _filterName = "Normal";
        public string FilterName
        {
            get
            {
                return _filterName;
            }
            set
            {
                _filterName = value;
                tbName.Text = _filterName;
            }
        }

        
    }
}
