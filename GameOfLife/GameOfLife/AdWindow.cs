using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GameOfLife
{
    class AdWindow : Window
    {
        private readonly DispatcherTimer _adTimer;
        private int _imageNumber;     // the number of the image currently shown
        private string _link;    // the URL where the currently shown ad leads to
        private ImageBrush _myBrush;
        private BitmapImage[] _images;
        
        public AdWindow(Window owner)
        {
            InitWindow(owner);

            _adTimer = new DispatcherTimer();
            InitTimer();

            InitSources();

            SetInitialImageNumber();

            ChangeAds(this, EventArgs.Empty);
        }

        private void InitSources()
        {
            _link = "http://example.com";
            _myBrush = new ImageBrush();
            _images = new BitmapImage[]
            {
                new BitmapImage(new Uri("ad1.jpg", UriKind.Relative)),
                new BitmapImage(new Uri("ad2.jpg", UriKind.Relative)),
                new BitmapImage(new Uri("ad3.jpg", UriKind.Relative))
            };
        }

        private void InitWindow(Window owner)
        {
            Owner = owner;
            Width = 350;
            Height = 100;
            ResizeMode = ResizeMode.NoResize;
            WindowStyle = WindowStyle.ToolWindow;
            Title = "Support us by clicking the ads";
            Cursor = Cursors.Hand;
            ShowActivated = false;
            MouseDown += OnClick;
        }

        private void InitTimer()
        {
            // Run the timer that changes the ad's image 
            _adTimer.Interval = TimeSpan.FromSeconds(3);
            _adTimer.Tick += ChangeAds;
            _adTimer.Start();
        }

        private void SetInitialImageNumber()
        {
            Random rnd = new Random();
            _imageNumber = rnd.Next(1, 3);
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(_link);
            Close();
        }
        
        public void Unsubscribe()
        {
            _adTimer.Tick -= ChangeAds;
        }

        private void ChangeAds(object sender, EventArgs eventArgs)
        {
            switch (_imageNumber)
            {
                case 1:
                case 2:
                    _imageNumber++;
                    break;
                case 3:
                    _imageNumber = 1;
                    break;
            }

            _myBrush.ImageSource = _images[_imageNumber - 1];
            Background = _myBrush;
        }
    }
}