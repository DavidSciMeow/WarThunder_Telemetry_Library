using Newtonsoft.Json.Linq;
using SkiaSharp;
using System;
using System.Collections.Generic;
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


    }
}