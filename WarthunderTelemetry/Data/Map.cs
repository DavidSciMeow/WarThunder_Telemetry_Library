using Newtonsoft.Json.Linq;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WarthunderTelemetry.Model;

namespace WarthunderTelemetry.Data
{
    /// <summary>
    /// 地图信息类
    /// </summary>
    public static class Map
    {
        /// <summary>
        /// 当前地图对象信息
        /// </summary>
        public static List<MapObjInfo> MapObjInfos { get; } = new List<MapObjInfo>();
        /// <summary>
        /// 地图大小
        /// </summary>
        public static float[] GridSize { get; private set; } = new float[2];
        /// <summary>
        /// 网格步长
        /// </summary>
        public static float[] GridSteps { get; private set; } = new float[2];
        /// <summary>
        /// 零点位置
        /// </summary>
        public static float[] GridZero { get; private set; } = new float[2];
        /// <summary>
        /// 玩家位置
        /// </summary>
        public static MapObjInfo? PlayerPosition => MapObjInfos.FirstOrDefault(obj => obj.Icon == "player");
        /// <summary>
        /// 地图调试信息:是否绘制玩家
        /// </summary>
        public static bool Drawplayer { get; set; } = true;
        /// <summary>
        /// 地图调试信息:是否绘制战斗机
        /// </summary>
        public static bool Drawfighter { get; set; } = true;
        /// <summary>
        /// 地图调试信息:是否绘制轰炸点
        /// </summary>
        public static bool Drawbombing_point { get; set; } = true;
        /// <summary>
        /// 地图调试信息:是否绘制防守点
        /// </summary>
        public static bool Drawdefending_point { get; set; } = false;
        /// <summary>
        /// 地图调试信息:是否绘制重生点(坦克)
        /// </summary>
        public static bool Drawrespawn_base_tank { get; set; } = false;
        /// <summary>
        /// 地图调试信息:是否绘制重生点(战斗机)
        /// </summary>
        public static bool Drawrespawn_base_fighter { get; set; } = false;
        /// <summary>
        /// 地图调试信息:是否绘制重生点(轰炸机)
        /// </summary>
        public static bool Drawrespawn_base_bomber { get; set; } = false;
        /// <summary>
        /// 地图调试信息:是否绘制占领区
        /// </summary>
        public static bool Drawcapture_zone { get; set; } = true;
        /// <summary>
        /// 地图调试信息:是否绘制地面模型
        /// </summary>
        public static bool Drawground_model { get; set; } = true;
        /// <summary>
        /// 地图调试信息:是否绘制机场
        /// </summary>
        public static bool Drawairfield { get; set; } = true;


        private static readonly SKPaint grayPaint = new SKPaint
        {
            Color = SKColors.Gray,
            StrokeWidth = 1
        };

        private static readonly SKPaint fillPaint = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Fill
        };

        private static readonly SKPaint strokePaint = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 4
        };

        // 图钉列表
        private static readonly List<MapObjInfo> Pins = new List<MapObjInfo>();

        /// <summary>
        /// 添加图钉
        /// </summary>
        /// <param name="logicalX">逻辑 X 坐标</param>
        /// <param name="logicalY">逻辑 Y 坐标</param>
        public static void AddPin(float logicalX, float logicalY)
        {
            Pins.Add(new MapObjInfo
            {
                X = logicalX,
                Y = logicalY,
                Icon = "pin", // 自定义图钉类型
                Color = "#FF0000" // 红色图钉
            });
        }

        /// <summary>
        /// 移除最近的图钉
        /// </summary>
        public static void RemoveLastPin()
        {
            if (Pins.Count > 0)
            {
                Pins.RemoveAt(Pins.Count - 1);
            }
        }

        /// <summary>
        /// 绘制图钉
        /// </summary>
        /// <param name="canvas">SkiaSharp 画布</param>
        /// <param name="scale">当前缩放比例</param>
        /// <param name="translateX">当前 X 轴平移量</param>
        /// <param name="translateY">当前 Y 轴平移量</param>
        public static void DrawPins(SKCanvas canvas, double scale, double translateX, double translateY)
        {
            foreach (var pin in Pins)
            {
                // 将逻辑坐标转换为屏幕坐标
                var screenPosition = LogicalToScreen(pin.X, pin.Y, scale, translateX, translateY);

                // 绘制图钉
                using var paint = new SKPaint
                {
                    Color = SKColor.Parse(pin.Color),
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill
                };
                canvas.DrawCircle((float)screenPosition.X, (float)screenPosition.Y, 10, paint);
            }
        }
        /// <summary>
        /// 将逻辑坐标转换为屏幕坐标
        /// </summary>
        /// <param name="logicalX">逻辑 X 坐标</param>
        /// <param name="logicalY">逻辑 Y 坐标</param>
        /// <param name="scale">当前缩放比例</param>
        /// <param name="translateX">当前 X 轴平移量</param>
        /// <param name="translateY">当前 Y 轴平移量</param>
        /// <returns>屏幕坐标</returns>
        public static Point LogicalToScreen(float logicalX, float logicalY, double scale, double translateX, double translateY)
        {
            // 根据网格信息计算屏幕坐标
            double screenX = (logicalX - GridZero[0]) * (GridSteps[0] / GridSize[0]) * scale + translateX;
            double screenY = (logicalY - GridZero[1]) * (GridSteps[1] / GridSize[1]) * scale + translateY;
            return new Point((int)Math.Round(screenX), (int)Math.Round(screenY));
        }

        /// <summary>
        /// 获取地图信息
        /// </summary>
        /// <returns></returns>
        public static string GetMapInfo()
        {
            string ss = $"" +
                $"-----------\nMapInfo\n-----------\n" +
                $"G_Size:{GridSize[0]}:{GridSize[1]}\n" +
                $"G_Step:{GridSteps[0]}:{GridSteps[1]}\n" +
                $"G_Zero:{GridZero[0]}:{GridZero[1]}\n" +
                $"-----------\nMapObject\n-----------\n";
            foreach (var i in MapObjInfos) ss += $"{i}\n";
            ss += "-----------\n";
            return ss;
        }
        /// <summary>
        /// 生成地图图片
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateDefaultMapImage()
        {
            using var bitmap = new SKBitmap(2000, 2000);
            using var canvas = new SKCanvas(bitmap);
            canvas.Clear(SKColors.White);

            var text = "Not In Any Battle";

            // 计算字体大小，使其占据大约40%的宽度
            float targetWidth = bitmap.Width * 0.4f;
            var font = new SKFont();
            var paint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true
            };

            float textSize = 24;
            font.Size = textSize;
            float textWidth = font.MeasureText(text);
            while (textWidth < targetWidth)
            {
                textSize += 1;
                font.Size = textSize;
                textWidth = font.MeasureText(text);
            }

            // 计算文本位置，使其居中
            var x = (bitmap.Width - textWidth) / 2;
            var y = (bitmap.Height + font.Size) / 2;

            // 绘制阴影
            var shadowPaint = new SKPaint
            {
                Color = SKColors.Gray,
                IsAntialias = true
            };
            canvas.DrawText(text, x + 5, y + 5, SKTextAlign.Left, font, shadowPaint);

            // 绘制文本
            canvas.DrawText(text, x, y, SKTextAlign.Left, font, paint);

            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            return data.ToArray();
        }
        /// <summary>
        /// 初始化地图
        /// </summary>
        /// <param name="mapInfo">要传递的MapInfo参量</param>
        /// <param name="mapObjects">要传递的mapObjects参量</param>
        /// <param name="mapData">要传递的mapData参量</param>
        /// <returns></returns>
        public static byte[] Initialize(JObject mapInfo, JArray mapObjects, byte[] mapData)
        {
            // 解析地图信息
            GridSize = mapInfo["grid_size"]?.ToObject<float[]>() ?? new float[2];
            GridSteps = mapInfo["grid_steps"]?.ToObject<float[]>() ?? new float[2];
            GridZero = mapInfo["grid_zero"]?.ToObject<float[]>() ?? new float[2];

            // 解析单位信息
            MapObjInfos.Clear();
            foreach (var obj in mapObjects) MapObjInfos.Add(new MapObjInfo(obj));

            // 下载并解析地图图片
            using SKBitmap MapImage = SKBitmap.Decode(mapData);
            using var surface = SKSurface.Create(new SKImageInfo(MapImage.Width, MapImage.Height));
            var canvas = surface.Canvas;

            // 画背景地图
            canvas.DrawBitmap(MapImage, 0, 0);

            // 画网格
            for (float x = GridZero[0]; x < MapImage.Width; x += GridSteps[0])
                canvas.DrawLine(x, 0, x, MapImage.Height, grayPaint);
            for (float y = GridZero[1]; y < MapImage.Height; y += GridSteps[1])
                canvas.DrawLine(0, y, MapImage.Width, y, grayPaint);

            // 先绘制非绿色单位
            foreach (var obj in MapObjInfos)
            {
                if (obj.Color.ToLower() != "#39d921") // 绿色
                {
                    DrawMapObject(canvas, obj, MapImage.Width, MapImage.Height);
                }
            }

            // 再绘制绿色单位
            foreach (var obj in MapObjInfos)
            {
                if (obj.Color.ToLower() == "#39d921") // 绿色
                {
                    DrawMapObject(canvas, obj, MapImage.Width, MapImage.Height);
                }
            }

            // 保存绘制结果
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            return data.ToArray();
        }

        private static void DrawMapObject(SKCanvas canvas, MapObjInfo obj, int mapWidth, int mapHeight)
        {
            float x = obj.X * mapWidth;
            float y = obj.Y * mapHeight;
            SKColor color = SKColor.Parse(obj.Color);

            fillPaint.Color = color;
            strokePaint.Color = color;
            var wid = 30;

            switch (obj.Type.ToLower())
            {
                case "airfield":
                    if (Drawairfield)
                    {
                        fillPaint.StrokeWidth = 10;
                        canvas.DrawLine(obj.Sx * mapWidth, obj.Sy * mapHeight, obj.Ex * mapWidth, obj.Ey * mapHeight, fillPaint);
                    }
                    break;
                case "ground_model":
                    if (Drawground_model)
                    {
                        canvas.DrawCircle(x, y, 5, fillPaint);
                    }
                    break;
            }

            switch (obj.Icon.ToLower())
            {
                case "player":
                    if (Drawplayer)
                    {
                        fillPaint.Color = SKColor.Parse("#FFD700");
                        DrawAirUnit(canvas, x, y, obj.Dx, obj.Dy, fillPaint);
                    }
                    break;
                case "fighter":
                    if (Drawfighter)
                    {
                        DrawAirUnit(canvas, x, y, obj.Dx, obj.Dy, fillPaint, true);
                    }
                    break;
                case "bombing_point":
                    if (Drawbombing_point)
                    {
                        DrawBombingPoint(canvas, x, y, fillPaint, strokePaint);
                    }
                    break;
                case "defending_point":
                    if (Drawdefending_point)
                    {
                        DrawBombingPoint(canvas, x, y, fillPaint, strokePaint);
                    }
                    break;
                case "respawn_base_tank":
                    if (Drawdefending_point)
                    {
                        canvas.DrawRoundRect(x - wid, y - wid, wid, wid, wid / 3, wid / 3, fillPaint);
                    }
                    break;
                case "respawn_base_fighter":
                    if (Drawrespawn_base_fighter)
                    {
                        canvas.DrawRoundRect(x - wid, y - wid, wid, wid, wid / 3, wid / 3, fillPaint);
                    }
                    break;
                case "respawn_base_bomber":
                    if (Drawrespawn_base_bomber)
                    {
                        canvas.DrawRoundRect(x - wid, y - wid, wid, wid, wid / 3, wid / 3, fillPaint);
                    }
                    break;
                case "capture_zone":
                    if (Drawcapture_zone)
                    {
                        canvas.DrawRoundRect(x - wid, y - wid, wid, wid, wid / 3, wid / 3, fillPaint);
                    }
                    break;
            }
        }
        private static void DrawAirUnit(SKCanvas canvas, float x, float y, float dx, float dy, SKPaint paint, bool round = false)
        {
            float angle = (float)Math.Atan2(dy, dx);
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

            float triangleSize = 30; // 调整三角形大小
            float lineLength = 50; // 竖线长度
            if (!round)
            {
                using var path = new SKPath();
                path.MoveTo(x + triangleSize * cos, y + triangleSize * sin); // 顶点
                path.LineTo(x - 7.5f * sin, y + 7.5f * cos); // 左下角
                path.LineTo(x + 7.5f * sin, y - 7.5f * cos); // 右下角
                path.Close();

                canvas.DrawPath(path, paint);
            }
            else
            {
                canvas.DrawCircle(x, y, 5, fillPaint);
            }
            // 绘制短的竖线
            using var linePaint = new SKPaint
            {
                Color = paint.Color,
                StrokeWidth = 3,
                IsAntialias = paint.IsAntialias
            };
            canvas.DrawLine(x, y, x + lineLength * cos, y + lineLength * sin, linePaint);
        }
        private static void DrawBombingPoint(SKCanvas canvas, float x, float y, SKPaint fillPaint, SKPaint strokePaint) => canvas.DrawCircle(x, y, 14, strokePaint);

        /// <summary>
        /// 将地图坐标转换为游戏距离
        /// </summary>
        /// <param name="mapX">地图上的 X 坐标</param>
        /// <param name="mapY">地图上的 Y 坐标</param>
        /// <returns>游戏中的实际坐标</returns>
        public static (float gameX, float gameY) ConvertToGameDistance(float mapX, float mapY)
        {
            float gameX = (mapX - GridZero[0]) * (GridSize[0] / GridSteps[0]);
            float gameY = (mapY - GridZero[1]) * (GridSize[1] / GridSteps[1]);
            return (gameX, gameY);
        }

        /// <summary>
        /// 计算两点之间的游戏距离
        /// </summary>
        /// <param name="mapX1">第一个点的地图 X 坐标</param>
        /// <param name="mapY1">第一个点的地图 Y 坐标</param>
        /// <param name="mapX2">第二个点的地图 X 坐标</param>
        /// <param name="mapY2">第二个点的地图 Y 坐标</param>
        /// <returns>两点之间的游戏距离</returns>
        public static double CalculateGameDistance(float mapX1, float mapY1, float mapX2, float mapY2)
        {
            var (gameX1, gameY1) = ConvertToGameDistance(mapX1, mapY1);
            var (gameX2, gameY2) = ConvertToGameDistance(mapX2, mapY2);

            return Math.Sqrt(Math.Pow(gameX2 - gameX1, 2) + Math.Pow(gameY2 - gameY1, 2));
        }

    }
}