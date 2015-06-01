using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using SimpleFilter.Converter;

namespace SimpleFilter.Control
{
    public partial class NinePatchImage : UserControl
    {

        private string _imageSource = "";

        public NinePatchImage()
        {
            InitializeComponent();
        }

        public new double Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                base.Width = value;
                RenderImage();
            }
        }

        public new double Height
        {
            get
            {
                return base.Height;
            }
            set
            {
                base.Height = value;
                RenderImage();
            }
        }

        public string ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                RenderImage();
            }
        }

        private void RenderImage()
        {
            if (!string.IsNullOrEmpty(_imageSource))
            {
                var imgBase = new BitmapImage(new Uri(_imageSource, UriKind.RelativeOrAbsolute));

                imgBase.CreateOptions = BitmapCreateOptions.None;
                imgBase.ImageOpened += (s, e) =>
                {
                    double imgWidth = imgBase.PixelWidth > Width ? imgBase.PixelWidth : Width;
                    double imgHeght = imgBase.PixelHeight > Height ? imgBase.PixelHeight : Height;

                    if (this.Width != imgWidth) this.Width = imgWidth;
                    if (this.Height != imgHeght) this.Height = imgHeght;

                    NinePatch imgConvert = new NinePatch(imgBase, (int)imgWidth, (int)imgHeght);
                    WriteableBitmap img9Patch = imgConvert.getBitmap9Patch();
                    image.Source = img9Patch;
                    image.Width = imgWidth;
                    image.Height = imgHeght;
                };

            }
        }

        public string Text
        {
            get
            {
                return tbContent.Text;
            }
            set
            {
                tbContent.Text = value;
            }
        }
    }
}
