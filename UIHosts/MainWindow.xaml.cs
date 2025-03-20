using SkiaSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WarthunderTelemetry.Base;
using WarthunderTelemetry.Data;

namespace WarthunderTelemetry
{
    public partial class MainWindow : Window
    {
        private Point origin;
        private Point start;

        public MainWindow()
        {
            InitializeComponent();
            mapImage.MouseWheel += MapImage_MouseWheel;
            // mapImage.MouseLeftButtonDown += MapImage_MouseLeftButtonDown;
            // mapImage.MouseMove += MapImage_MouseMove;
            // mapImage.MouseLeftButtonUp += MapImage_MouseLeftButtonUp;

            SetWindowSize();

            Task.Run(UpdateMapImage);
            Task.Run(UpdateInfoText);
        }

        private void SetWindowSize()
        {
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;
            Width = screenWidth / 2;
            Height = screenHeight / 2;
        }

        private void MapImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scale = e.Delta > 0 ? 1.1 : 0.9;
            var position = e.GetPosition(mapImage);
            scaleTransform.CenterX = position.X;
            scaleTransform.CenterY = position.Y;
            scaleTransform.ScaleX *= scale;
            scaleTransform.ScaleY *= scale;
        }

        // private void MapImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        // {
        //     mapImage.CaptureMouse();
        //     start = e.GetPosition(this);
        //     origin = new Point(translateTransform.X, translateTransform.Y);
        // }

        // private void MapImage_MouseMove(object sender, MouseEventArgs e)
        // {
        //     if (mapImage.IsMouseCaptured)
        //     {
        //         var position = e.GetPosition(this);
        //         translateTransform.X = origin.X + (position.X - start.X);
        //         translateTransform.Y = origin.Y + (position.Y - start.Y);
        //     }
        // }

        // private void MapImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        // {
        //     mapImage.ReleaseMouseCapture();
        // }

        private async Task UpdateMapImage()
        {
            try
            {
                byte[]? newMapImageBytes = await Get.GetMapImgAsync();
                if (newMapImageBytes == null)
                {
                    // 显示纯白图片
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        using var bitmap = new SKBitmap(800, 600);
                        using var canvas = new SKCanvas(bitmap);
                        canvas.Clear(SKColors.White);

                        using var image = SKImage.FromBitmap(bitmap);
                        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
                        using var stream = new MemoryStream(data.ToArray());

                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = stream;
                        bitmapImage.EndInit();
                        mapImage.Source = bitmapImage;
                    });

                }
                else
                {
                    // 更新UI
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        using var stream = new MemoryStream(newMapImageBytes);
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();
                        mapImage.Source = bitmap;
                    });
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            await Task.Delay(100);
            await UpdateMapImage();
        }


        private async Task UpdateInfoText()
        {
            try
            {
                string info = await Army.GetInfoAsync();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    infoTextBlock.Text = info;
                });
            }
            catch
            {

            }

            await Task.Delay(100);
            await UpdateInfoText();
        }
    }
}