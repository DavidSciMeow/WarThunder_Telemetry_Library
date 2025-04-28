using SkiaSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UIHosts;
using SkiaSharp.Views.WPF;
using WarthunderTelemetry.Base;
using WarthunderTelemetry.Data;
using SkiaSharp.Views.Desktop;

namespace WarthunderTelemetry
{
    public partial class MainWindow : Window
    {
        private Point origin;
        private Point start;
        private Point lastMousePosition;

        private void MapImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 获取鼠标点击位置
            Point mousePosition = e.GetPosition(mapImage);

            // 将屏幕坐标转换为逻辑坐标
            float logicalX = (float)((mousePosition.X - translateTransform.X) / scaleTransform.ScaleX * Map.GridSize[0] / Map.GridSteps[0] + Map.GridZero[0]);
            float logicalY = (float)((mousePosition.Y - translateTransform.Y) / scaleTransform.ScaleY * Map.GridSize[1] / Map.GridSteps[1] + Map.GridZero[1]);

            // 添加图钉
            Map.AddPin(logicalX, logicalY);

            // 重新绘制地图
            mapImage.InvalidateVisual();
        }

        private void RemovePin_Click(object sender, RoutedEventArgs e)
        {
            // 移除最近的图钉
            Map.RemoveLastPin();

            // 重新绘制地图
            mapImage.InvalidateVisual();
        }

        private void MapImage_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);

            // 绘制地图背景（省略具体实现）
            mapImage.CaptureMouse();
            start = e.GetPosition(this);
            origin = new Point(translateTransform.X, translateTransform.Y);

            // 绘制图钉
            Map.DrawPins(canvas, scaleTransform.ScaleX, translateTransform.X, translateTransform.Y);
        }

        public MainWindow()
        {
            InitializeComponent();
            mapImage.MouseWheel += MapImage_MouseWheel;
            mapImage.MouseLeftButtonDown += MapImage_MouseLeftButtonDown;
            mapImage.MouseMove += MapImage_MouseMove;
            mapImage.MouseLeftButtonUp += MapImage_MouseLeftButtonUp;
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
            scaleTransform.ScaleX *= scale;
            scaleTransform.ScaleY *= scale;
        }

        private void MapImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (mapImage.IsMouseCaptured)
            {
                var position = e.GetPosition(this);
                translateTransform.X = origin.X + (position.X - start.X);
                translateTransform.Y = origin.Y + (position.Y - start.Y);
            }
        }

        private void MapImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mapImage.ReleaseMouseCapture();
        }

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
            catch (Exception ex)
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