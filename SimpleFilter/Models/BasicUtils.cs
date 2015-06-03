using System;
using System.Collections.Generic;
using System.IO;
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

            int mWidth = baseImage.PixelWidth;
            int mHeight = baseImage.PixelHeight;

            WriteableBitmap baseBitmap = baseImage;
            WriteableBitmap blendBitmap = blendImage;
            int index = 0;// mapIndex = 0;
            int rA = 0, gA = 0, bA = 0, aA = 0;
            int R = 0, G = 0, B = 0, A = 0;
            /*if (mHeightMap == 1)
            {
                //baseBitmap.ForEach(
                //(x, y, color) => Color.FromArgb(color.A,
                //    (byte)blendBitmap.GetPixel(color.R, 0).R,
                //    (byte)blendBitmap.GetPixel(color.G, 0).G,
                //    (byte)blendBitmap.GetPixel(color.B, 0).B)
                //);
                for(int x = 0; x < Config.SIZE; x++)
                    for(int y = 0; y < Config.SIZE; y++)
                    {
                        index = y * Config.SIZE + x;
                        int colorA = baseImage.Pixels[index];

                        rA = (colorA >> 16) & 0xff;
                        gA = (colorA >> 8) & 0xff;
                        bA = colorA & 0xff;
                        aA = (colorA >> 24) & 0xff;
                        
                        baseBitmap.Pixels[index] = blendBitmap.Pixels[]
                    }

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
             */

            for (int x = 0; x < mWidth; x++)
            {
                for (int y = 0; y < mHeight; y++)
                {
                    index = y * mWidth + x;

                    rA = (baseBitmap.Pixels[index] >> 16) & 0xff;
                    gA = (baseBitmap.Pixels[index] >> 8) & 0xff;
                    bA = baseBitmap.Pixels[index] & 0xff;
                    aA = (baseBitmap.Pixels[index] >> 24) & 0xff;

                    if (mHeightMap == 1)
                    {
                        R = (blendBitmap.Pixels[rA] >> 16) & 0xff;
                        G = (blendBitmap.Pixels[gA] >> 8) & 0xff;
                        B = blendBitmap.Pixels[bA] & 0xff;
                        A = aA;
                    }
                    else
                    {
                        R = (blendBitmap.Pixels[rA] >> 16) & 0xff;
                        G = (blendBitmap.Pixels[Config.MAP_SIZE + gA] >> 8) & 0xff;
                        B = blendBitmap.Pixels[2 * Config.MAP_SIZE + bA] & 0xff;
                        A = aA;
                    }

                    //R = (R > 255) ? 255 : R;
                    //G = (G > 255) ? 255 : G;
                    //B = (B > 255) ? 255 : B;
                    //A = (A > 255) ? 255 : A;
                    baseImage.Pixels[index] = (R << 16) | (G << 8) | B | (A << 24);
                }
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
            int index = 0; int modeIndex = 0;
            int rA = 0, gA = 0, bA = 0, aA = 0;
            int rB = 0, gB = 0, bB = 0, aB = 0;
            int R = 0, G = 0, B = 0, A = 0;
            for (int x = 0; x < Config.SIZE; x++)
                for (int y = 0; y < Config.SIZE; y++)
                {
                    index = y * Config.SIZE + x;
                    //var colorA = baseImage.GetPixel(x, y);
                    //var colorB = maskBitmap.GetPixel(x, y);

                    //R = blendModeMap.GetPixel(colorB.R, colorA.R).R;
                    //R = colorA.R + (int)((R - colorA.R) * maskOpacity);

                    //G = blendModeMap.GetPixel(colorB.G, colorA.G).G;
                    //G = colorA.G + (int)((G - colorA.G) * maskOpacity);

                    //B = blendModeMap.GetPixel(colorB.B, colorA.B).B;
                    //B = colorA.B + (int)((B - colorA.B) * maskOpacity);

                    //A = blendModeMap.GetPixel(colorB.A, colorA.A).A;
                    //A = colorA.A + (int)((A - colorA.A) * maskOpacity);

                    int colorA = baseImage.Pixels[index];
                    int colorB = baseImage.Pixels[index];

                    rA = (colorA >> 16) & 0xff;
                    gA = (colorA >> 8) & 0xff;
                    bA = colorA & 0xff;
                    aA = (colorA >> 24) & 0xff;

                    rB = (colorB >> 16) & 0xff;
                    gB = (colorB >> 8) & 0xff;
                    bB = colorB & 0xff;
                    aB = (colorB >> 24) & 0xff;

                    modeIndex = rA * Config.MAP_SIZE + rB;
                    R = (blendModeMap.Pixels[modeIndex] >> 16) & 0xff;
                    R = rA + (int)((R - rA) * maskOpacity);
                    
                    modeIndex = gA * Config.MAP_SIZE + gB;
                    G = (blendModeMap.Pixels[modeIndex] >> 8) & 0xff;
                    G = gA + (int)((G - gA) * maskOpacity);
                    
                    modeIndex = bA * Config.MAP_SIZE + bB;
                    B = blendModeMap.Pixels[modeIndex] & 0xff;
                    B = bA + (int)((B - bA) * maskOpacity);
                    
                    modeIndex = aA * Config.MAP_SIZE + aB;
                    A = (blendModeMap.Pixels[modeIndex] >> 24) & 0xff;
                    A = aA + (int)((A - aA) * maskOpacity);
                    
                    R = (R > 255) ? 255 : R;
                    G = (G > 255) ? 255 : G;
                    B = (B > 255) ? 255 : B;
                    A = (A > 255) ? 255 : A;

                    resultBitmap.Pixels[index] = (A << 24) | (R << 16) | (G << 8) | B;
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
                WriteableBitmap wrMap = new WriteableBitmap(modeMap);
                if(wrMap.PixelWidth != Config.MAP_SIZE || wrMap.PixelHeight != Config.MAP_SIZE)
                {
                    wrMap = wrMap.Resize(Config.MAP_SIZE, Config.MAP_SIZE, WriteableBitmapExtensions.Interpolation.NearestNeighbor);
                }
                tcs.SetResult(wrMap);
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
