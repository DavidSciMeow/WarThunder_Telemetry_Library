using Newtonsoft.Json.Linq;
using SkiaSharp;
using System;
using System.Collections.Generic;
using WarthunderTelemetry.Model;

namespace WarthunderTelemetry.Data
{
    public static class Map
    {
        public static List<MapObjInfo> MapObjInfos { get; } = new List<MapObjInfo>();
        public static float[] GridSize { get; private set; } = new float[2];
        public static float[] GridSteps { get; private set; } = new float[2];
        public static float[] GridZero { get; private set; } = new float[2];

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

            switch (obj.Icon.ToLower())
            {
                case "player":
                    fillPaint.Color = SKColor.Parse("#FFD700");
                    DrawTriangle(canvas, x, y, obj.Dx, obj.Dy, fillPaint);
                    break;
                case "fighter":
                    DrawTriangle(canvas, x, y, obj.Dx, obj.Dy, fillPaint);
                    break;
                case "bombing_point":
                case "defending_point":
                    DrawBombingPoint(canvas, x, y, fillPaint, strokePaint);
                    break;
                case "respawn_base_tank":
                case "respawn_base_fighter":
                case "respawn_base_bomber":
                    break;
                case "capture_zone":
                    var wid = 30;
                    canvas.DrawRoundRect(x - wid, y - wid, wid, wid, wid/3, wid/3, fillPaint);
                    break;
                case "ground_model":
                case "airfield":
                default:
                    canvas.DrawCircle(x, y, 10, fillPaint);
                    break;
            }
        }

        private static void DrawTriangle(SKCanvas canvas, float x, float y, float dx, float dy, SKPaint paint)
        {
            float angle = (float)Math.Atan2(dy, dx);
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

            float triangleSize = 30; // 调整三角形大小
            float lineLength = 50; // 竖线长度

            using var path = new SKPath();
            path.MoveTo(x + triangleSize * cos, y + triangleSize * sin); // 顶点
            path.LineTo(x - 7.5f * sin, y + 7.5f * cos); // 左下角
            path.LineTo(x + 7.5f * sin, y - 7.5f * cos); // 右下角
            path.Close();

            canvas.DrawPath(path, paint);

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
    }
}