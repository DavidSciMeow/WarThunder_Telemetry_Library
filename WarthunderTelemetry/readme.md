## 战争雷霆遥测数据

### 这个库是用于分析战争雷霆游戏的遥测数据的。
### 现在它可以解析地图数据, 并返回地图的byte[]列

## Basic Usage
```csharp	
    //... WPF 窗体设计 代码
    <Image x:Name="mapImage" Stretch="Uniform" RenderTransformOrigin="0.5, 0.5"
               MouseLeftButtonDown="MapImage_MouseLeftButtonDown"
               MouseMove="MapImage_MouseMove"
               MouseLeftButtonUp="MapImage_MouseLeftButtonUp">
            <Image.RenderTransform>
                <TransformGroup>
                    <ScaleTransform x:Name="scaleTransform" />
                    <TranslateTransform x:Name="translateTransform" />
                </TransformGroup>
            </Image.RenderTransform>
    </Image>
```

```csharp	
    //WPF 后端设计 代码
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
```

## 详细示例可以查看仓库项目上级文件的`UIHosts`项目