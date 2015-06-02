using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SimpleFilter.Models
{
    public static class BasicUtils
    {
        public static WriteableBitmap BlendBaseWithMap(WriteableBitmap baseImage, WriteableBitmap blendImage)
        {
            //int mWidthMap = MAP_Config.SIZE;
            int mHeightMap = blendImage.PixelHeight;

            WriteableBitmap baseBitmap = baseImage;
            WriteableBitmap blendBitmap = blendImage;

            if (mHeightMap == 1)
            {
                baseBitmap.ForEach(
                (x, y, color) => Color.FromArgb(color.A,
                    (byte)blendBitmap.GetPixel(color.R, 0).R,
                    (byte)blendBitmap.GetPixel(color.G, 0).G,
                    (byte)blendBitmap.GetPixel(color.B, 0).B)
                );
            }
            else
            {
                baseBitmap.ForEach(
                (x, y, color) => Color.FromArgb(color.A,
                    (byte)blendBitmap.GetPixel(color.R, 0).R,
                    (byte)blendBitmap.GetPixel(color.G, 1).G,
                    (byte)blendBitmap.GetPixel(color.B, 2).B)
                );
            }
            blendBitmap = null;
            return baseBitmap;
        }

        public static WriteableBitmap BlendBaseWithMap(WriteableBitmap baseImage, WriteableBitmap[] blendImages)
        {
            WriteableBitmap resultBitmap = baseImage;

            foreach (var blendItem in blendImages)
            {
                resultBitmap = BlendBaseWithMap(resultBitmap, blendItem);
            }

            GC.Collect();
            return resultBitmap;
        }

        public static async Task<WriteableBitmap> BlendBaseWithMode(WriteableBitmap baseImage, WriteableBitmap maskImage, double maskOpacity, BlendMode mode)
        {
            WriteableBitmap blendModeMap = await GetBlendModeMap(mode);
            WriteableBitmap maskBitmap = maskImage;

            WriteableBitmap resultBitmap = new WriteableBitmap(Config.SIZE, Config.SIZE);

            for (int x = 0; x < Config.SIZE; x++)
                for (int y = 0; y < Config.SIZE; y++)
                {
                    var colorA = baseImage.GetPixel(x, y);
                    var colorB = maskBitmap.GetPixel(x, y);

                    int R = blendModeMap.GetPixel(colorB.R, colorA.R).R;
                    R = colorA.R + (int)((R - colorA.R) * maskOpacity);

                    int G = blendModeMap.GetPixel(colorB.G, colorA.G).G;
                    G = colorA.G + (int)((G - colorA.G) * maskOpacity);

                    int B = blendModeMap.GetPixel(colorB.B, colorA.B).B;
                    B = colorA.B + (int)((B - colorA.B) * maskOpacity);

                    int A = blendModeMap.GetPixel(colorB.A, colorA.A).A;
                    A = colorA.A + (int)((A - colorA.A) * maskOpacity);

                    R = (R > 255) ? 255 : R;
                    G = (G > 255) ? 255 : G;
                    B = (B > 255) ? 255 : B;
                    A = (A > 255) ? 255 : A;

                    resultBitmap.SetPixel(x, y, Color.FromArgb((byte)A, (byte)R, (byte)G, (byte)B));
                }
            blendModeMap = null;
            maskBitmap = null;

            return resultBitmap;
        }

        private static Task<WriteableBitmap> GetBlendModeMap(BlendMode mode)
        {
            var tcs = new TaskCompletionSource<WriteableBitmap>();
            string blendMapPath = "";
            switch (mode)
            {
                case BlendMode.OVERLAY:
                    blendMapPath = Config.OVERLAY_MAP;
                    break;
                case BlendMode.BLACK_OVERLAY:
                    blendMapPath = Config.BLACKOVERLAY_MAP;
                    break;
                case BlendMode.GRADIENT:
                    blendMapPath = Config.GRADIENT_MAP;
                    break;
                case BlendMode.HARDLIGHT:
                    blendMapPath = Config.HARDLIGHT_MAP;
                    break;
                case BlendMode.MAP_2D:
                    blendMapPath = Config.MAP_2D_PATH;
                    break;
                case BlendMode.SOFTLIGHT:
                    blendMapPath = Config.SOFTLIGHT_MAP;
                    break;
                case BlendMode.VIGNETTE:
                    blendMapPath = Config.VIGNETTE_MAP;
                    break;
                default:
                    blendMapPath = Config.OVERLAY_MAP;
                    break;
            }
            BitmapImage modeMap = new BitmapImage(new Uri(blendMapPath, UriKind.RelativeOrAbsolute));
            modeMap.CreateOptions = BitmapCreateOptions.None;
            modeMap.ImageOpened += (s, e) =>
            {
                tcs.SetResult(new WriteableBitmap(s as BitmapImage));
            };
            return tcs.Task;
        }

        public static Task<WriteableBitmap> GetBitmap(string path)
        {
            var tcs = new TaskCompletionSource<WriteableBitmap>();
            BitmapImage bmp = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            bmp.CreateOptions = BitmapCreateOptions.None;

            bmp.ImageOpened += (s, e) =>
            {
                tcs.SetResult(new WriteableBitmap(bmp));
            };
            bmp.ImageFailed += (s, e) =>
            {
                tcs.SetException(new Exception("Cannot open Image"));
            };

            return tcs.Task;
        }

        public static WriteableBitmap GrayBitmap(WriteableBitmap bmp)
        {
            bmp.ForEach((x, y, color) => Color.FromArgb(color.A, GrayLevel(color), GrayLevel(color), GrayLevel(color)));
            return bmp;
        }

        private static byte GrayLevel(Color c)
        {
            return (byte)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
        }

    }
}
