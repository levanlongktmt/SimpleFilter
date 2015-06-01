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
using System.Windows.Media;
using Microsoft.Phone.Tasks;
using SimpleFilter.Models;
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Media.PhoneExtensions;

namespace SimpleFilter.Control
{
    public partial class FilterImage : UserControl
    {
        public event EventHandler OnBeginProcess;
        public event EventHandler OnCompletedProcess;
        public event EventHandler OnPressBack;


        WriteableBitmap sourceImage;
        WriteableBitmap displayImage;
        bool isProcess = false;

        /// <summary>
        /// List Thumbnails path
        /// </summary>
        const string _path = @"/Assets/Thumbnails/filter_{0}.png";

        string amaro_path = string.Format(_path, "amaro");
        string inkvell_path = string.Format(_path, "inkwell");
        string nashville_path = string.Format(_path, "nashville");
        string rise_path = string.Format(_path, "rise");
        string kelvin_path = string.Format(_path, "kelvin");
        string xpro_path = string.Format(_path, "xproii");
        string lofi_path = string.Format(_path, "lofi");
        string mayfair_path = string.Format(_path, "mayfair");
        string hudson_path = string.Format(_path, "hudson");
        string ealybird_path = string.Format(_path, "earlybird");
        string sutro_path = string.Format(_path, "sutro");
        string toaster_path = string.Format(_path, "toaster");
        string brannan_path = string.Format(_path, "brannan");
        string walden_path = string.Format(_path, "walden");
        string hefe_path = string.Format(_path, "hefe");
        string f1997_path = string.Format(_path, "1977");
        string valencia_path = string.Format(_path, "valencia");
        //string amaro_path = string.Format(_path, "amaro");

        public FilterImage()
        {
            InitializeComponent();
            SetVisible(Visibility.Collapsed);
            //this.Loaded += FilterImage_Loaded;
        }


        public void SetVisible(Visibility visible)
        {
            this.Visibility = visible;
            if(visible==Visibility.Collapsed)
            {
                ResetControl();
            }
        }

        private void ResetControl()
        {
            sourceImage = null;
            InitFilterList();
            processImage.Visibility = Visibility.Collapsed;
            tbEmpty.Visibility = Visibility.Visible;
            lsbFilterList.Visibility = Visibility.Collapsed;
        }
        void FilterImage_Loaded(object sender, RoutedEventArgs e)
        {
            ResetControl();
        }

        void InitFilterList()
        {
            lsbFilterList.Items.Clear();

            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = amaro_path, FilterName = "Amaro", FilterType = Filter.AMARO });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = inkvell_path, FilterName = "Inkwell", FilterType = Filter.INKVELL });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = nashville_path, FilterName = "Nash Ville", FilterType = Filter.NASHVILLE });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = rise_path, FilterName = "Rise", FilterType = Filter.RISE });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = kelvin_path, FilterName = "Kelvin", FilterType = Filter.KELVIN });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = xpro_path, FilterName = "Xpro", FilterType = Filter.XPRO });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = lofi_path, FilterName = "Lofi", FilterType = Filter.LOFI });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = mayfair_path, FilterName = "Mayfair", FilterType = Filter.MAYFAIR });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = hudson_path, FilterName = "Hudson", FilterType = Filter.HUDSON });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = ealybird_path, FilterName = "Early Bird", FilterType = Filter.EARLYBIRD });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = toaster_path, FilterName = "Toaster", FilterType = Filter.TOASTER });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = brannan_path, FilterName = "Brannan", FilterType = Filter.BRANNAN });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = walden_path, FilterName = "Walden", FilterType = Filter.WALDEN });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = hefe_path, FilterName = "Hefe", FilterType = Filter.HEFE });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = f1997_path, FilterName = "F1977", FilterType = Filter.F1997 });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = valencia_path, FilterName = "Valencia", FilterType = Filter.VALENCIA });
            lsbFilterList.Items.Add(new FilterListItem() { IsActive = false, ImageSource = sutro_path, FilterName = "Sutro", FilterType = Filter.SUTRO });
        }

        private void tbEmpty_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.Green);
        }

        private void tbEmpty_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.White);
        }

        private void tbEmpty_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.White);
        }

        private void tbEmpty_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhotoChooserTask task = new PhotoChooserTask();
            task.PixelHeight = 640;
            task.PixelWidth = 640;

            task.ShowCamera = true;
            task.Show();

            task.Completed += task_Completed;
        }

        void task_Completed(object sender, PhotoResult e)
        {
            if(e.TaskResult==TaskResult.OK)
            {
                BitmapImage bmp = new BitmapImage();
                Debug.WriteLine(e.OriginalFileName);
                bmp.SetSource(e.ChosenPhoto);
                sourceImage = new WriteableBitmap(bmp);
                MemoryStream ms = new MemoryStream();
                sourceImage.SaveJpeg(ms, 640, 640, 0, 100);
                ms.Close();
                Dispatcher.BeginInvoke(()=>
                {
                    processImage.Source = sourceImage;
                    displayImage = null;
                    lsbFilterList.Visibility = Visibility.Visible;
                    processImage.Visibility = Visibility.Visible;
                    tbEmpty.Visibility = Visibility.Collapsed;
                    foreach (var item in lsbFilterList.Items)
                    {
                        (item as FilterListItem).IsActive = false;
                    }
                }
                    );
                
            }
        }

        private void lsbFilterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!isProcess)
            {
                Debug.WriteLine("da chon bo loc");
                var list = sender as ListBox;
                var selectedItem = list.SelectedItem as FilterListItem;
                if (selectedItem != null)
                {
                    foreach (var item in list.Items)
                    {
                        if ((item as FilterListItem).FilterType != selectedItem.FilterType)
                            (item as FilterListItem).IsActive = false;
                    }
                    selectedItem.IsActive = true;
                    Debug.WriteLine("Bat dau xu ly");
                    isProcess = true;
                    lsbFilterList.IsHitTestVisible = false;
                    OnBeginProcess(this, null);
                    ProcessFilter(selectedItem.FilterType);
                }
            }
            
            //selectedItem = null;
        }

        public async void ProcessFilter(Filter filterName)
        {
            WriteableBitmap processMap = new WriteableBitmap(sourceImage);
            WriteableBitmap resultBitmap = await FilterBitmap.MaskImage(processMap, filterName);
            GC.Collect();
            Dispatcher.BeginInvoke(() =>
            {
                displayImage = resultBitmap;
                processImage.Source = displayImage;
                lsbFilterList.IsHitTestVisible = true;
                OnCompletedProcess(this, null);
            });
            isProcess = false;
            Debug.WriteLine("Da xu ly xong");
        }

        private void btnBack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            OnPressBack(this, null);
        }

        private void imgNew_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhotoChooserTask task = new PhotoChooserTask();
            task.PixelHeight = 640;
            task.PixelWidth = 640;

            task.ShowCamera = true;
            task.Show();

            task.Completed += task_Completed;
        }

        private void imgSave_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if(displayImage!=null)
            {
                OnBeginProcess(this, null);
                var filename = string.Format("simplefilter-{0}.jpg", DateTime.Now.ToLocalTime());
                using(var stream = new MemoryStream())
                {
                    displayImage.SaveJpeg(stream, displayImage.PixelWidth, displayImage.PixelHeight, 0, 100);
                    stream.Seek(0, SeekOrigin.Begin);
                    new MediaLibrary().SavePicture(filename, stream);
                }
                OnCompletedProcess(this, null);
            }
        }

        private void imgShare_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (displayImage != null)
            {
                var filename = string.Format("simplefilter-{0}.jpg", DateTime.Now.ToLocalTime());
                using (var stream = new MemoryStream())
                {
                    displayImage.SaveJpeg(stream, displayImage.PixelWidth, displayImage.PixelHeight, 0, 100);
                    stream.Seek(0, SeekOrigin.Begin);
                    var lib = new MediaLibrary();
                    var pic = lib.SavePicture(filename, stream);
                    var task = new ShareMediaTask();
                    task.FilePath = pic.GetPath();
                    task.Show();
                }
                
            }
        }

        private void imgClear_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            displayImage = sourceImage;
            processImage.Source = displayImage;
        }

        private void btnRotation_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if(displayImage==null)
            {
                if(sourceImage!=null)
                {
                    displayImage = sourceImage.Rotate(90);
                    processImage.Source = displayImage;
                }
            }
            else
            {
                displayImage = displayImage.Rotate(90);
                processImage.Source = displayImage;
            }
        }

    }
}
