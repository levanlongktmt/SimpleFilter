using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SimpleFilter.Models
{
    public static class FilterBitmap
    {
        public static async Task<WriteableBitmap> MaskImage(WriteableBitmap baseImage, Filter filterName)
        {
            WriteableBitmap resultBitmap = null;
            switch(filterName)
            {
                case Filter.INKVELL:
                    resultBitmap = await FilterInkvell(baseImage);
                    break;
                case Filter.NASHVILLE:
                    resultBitmap = await FilterNashVille(baseImage);
                    break;
                case Filter.RISE:
                    resultBitmap = await FilterRise(baseImage);
                    break;
                case Filter.KELVIN:
                    resultBitmap = await FilterKelvin(baseImage);
                    break;
                case Filter.XPRO:
                    resultBitmap = await FilterXpro(baseImage);
                    break;
                case Filter.AMARO:
                    resultBitmap = await FilterAmaro(baseImage);
                    break;
                case Filter.LOFI:
                    resultBitmap = await FilterLofi(baseImage);
                    break;
                case Filter.MAYFAIR:
                    resultBitmap = await FilterMayfair(baseImage);
                    break;
                case Filter.HUDSON:
                    resultBitmap = await FilterHudson(baseImage);
                    break;
                case Filter.EARLYBIRD:
                    resultBitmap = await FilterEarlyBird(baseImage);
                    break;
                case Filter.SUTRO:
                    resultBitmap = await FilterSutro(baseImage);
                    break;
                case Filter.TOASTER:
                    resultBitmap = await FilterToaster(baseImage);
                    break;
                case Filter.BRANNAN:
                    resultBitmap = await FilterBrannan(baseImage);
                    break;
                case Filter.WALDEN:
                    resultBitmap = await FilterWalden(baseImage);
                    break;
                case Filter.HEFE:
                    resultBitmap = await FilterHefe(baseImage);
                    break;
                case Filter.F1997:
                    resultBitmap = await FilterF1997(baseImage);
                    break;
                case Filter.VALENCIA:
                    resultBitmap = await FilterValencia(baseImage);
                    break;
                default:
                    break;
            }
            return resultBitmap;
        }

        private static async Task<WriteableBitmap> FilterInkvell(WriteableBitmap baseImage)
        {
            baseImage = BasicUtils.GrayBitmap(baseImage);
            string path = @"/Assets/Filters/inkwell/map.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = BasicUtils.BlendBaseWithMap(baseImage, mapBlend);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterNashVille(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/nashville/map.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = BasicUtils.BlendBaseWithMap(baseImage, mapBlend);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterRise(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/rise/map.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = BasicUtils.BlendBaseWithMap(baseImage, mapBlend);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterKelvin(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/lord_kelvin/map.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = BasicUtils.BlendBaseWithMap(baseImage, mapBlend);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterXpro(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/x_pro2/map.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = BasicUtils.BlendBaseWithMap(baseImage, mapBlend);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterAmaro(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/shared/blackboard.png";
            var mapBlend = await BasicUtils.GetBitmap(path);

            var mapResult = await BasicUtils.BlendBaseWithMode(baseImage, mapBlend, 0.9, BlendMode.OVERLAY);
            path = @"/Assets/Filters/amaro/map.png";
            mapBlend = await BasicUtils.GetBitmap(path);
            mapResult = BasicUtils.BlendBaseWithMap(mapResult, mapBlend);

            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterLofi(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/lo_fi/map.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = BasicUtils.BlendBaseWithMap(baseImage, mapBlend);
            path = @"/Assets/Filters/shared/blackboard.png";
            mapBlend = await BasicUtils.GetBitmap(path);
            mapResult = await BasicUtils.BlendBaseWithMode(mapResult, mapBlend, 0.4, BlendMode.VIGNETTE);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterMayfair(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/mayfair/glowField.PNG";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = await BasicUtils.BlendBaseWithMode(baseImage, mapBlend, 0.9, BlendMode.OVERLAY);

            path = @"/Assets/Filters/mayfair/colorOverlay.png";
            mapBlend = await BasicUtils.GetBitmap(path);
            path = @"/Assets/Filters/mayfair/colorGradient.png";
            var mapBlend2 = await BasicUtils.GetBitmap(path);
            var maps = new WriteableBitmap[] {mapBlend, mapBlend2};
            mapResult = BasicUtils.BlendBaseWithMap(mapResult, maps);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterHudson(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/hudson/blowout.PNG";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = await BasicUtils.BlendBaseWithMode(baseImage, mapBlend, 0.8, BlendMode.OVERLAY);
            path = @"/Assets/Filters/hudson/map.png";
            mapBlend = await BasicUtils.GetBitmap(path);
            mapResult = BasicUtils.BlendBaseWithMap(baseImage, mapBlend);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterEarlyBird(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/shared/blackboard_2.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = await BasicUtils.BlendBaseWithMode(baseImage, mapBlend, 0.4, BlendMode.SOFTLIGHT);

            path = @"/Assets/Filters/earlybird/blowout_map.png";
            mapBlend = await BasicUtils.GetBitmap(path);
            path = @"/Assets/Filters/earlybird/curves_map.png";
            var mapBlend2 = await BasicUtils.GetBitmap(path);
            path = @"/Assets/Filters/earlybird/overlay_map.png";
            var mapBlend3 = await BasicUtils.GetBitmap(path);
            var maps = new WriteableBitmap[] { mapBlend, mapBlend2, mapBlend3 };
            mapResult = BasicUtils.BlendBaseWithMap(mapResult, maps);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterSutro(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/sutro/edge_burn.PNG";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = await BasicUtils.BlendBaseWithMode(baseImage, mapBlend, 0.4, BlendMode.HARDLIGHT);

            path = @"/Assets/Filters/sutro/map.png";
            mapBlend = await BasicUtils.GetBitmap(path);
            mapResult = BasicUtils.BlendBaseWithMap(mapResult, mapBlend);

            path = @"/Assets/Filters/sutro/metal.PNG";
            mapBlend = await BasicUtils.GetBitmap(path);
            mapResult = await BasicUtils.BlendBaseWithMode(mapResult, mapBlend, 0.5, BlendMode.SOFTLIGHT);

            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterToaster(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/shared/blackboard_2.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = await BasicUtils.BlendBaseWithMode(baseImage, mapBlend, 0.4, BlendMode.SOFTLIGHT);

            path = @"/Assets/Filters/toaster/metal.PNG";
            mapBlend = await BasicUtils.GetBitmap(path);
            mapResult = await BasicUtils.BlendBaseWithMode(mapResult, mapBlend, 0.4, BlendMode.BLACK_OVERLAY);

            path = @"/Assets/Filters/toaster/map.png";
            mapBlend = await BasicUtils.GetBitmap(path);
            path = @"/Assets/Filters/toaster/color_shift_map.png";
            var mapBlend2 = await BasicUtils.GetBitmap(path);
            var maps = new WriteableBitmap[] { mapBlend, mapBlend2 };
            mapResult = BasicUtils.BlendBaseWithMap(mapResult, maps);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterBrannan(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/brannan/map.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            path = @"/Assets/Filters/brannan/blowout_map.png";
            var mapBlend2 = await BasicUtils.GetBitmap(path);
            path = @"/Assets/Filters/brannan/screen_map.png";
            var mapBlend3 = await BasicUtils.GetBitmap(path);
            path = @"/Assets/Filters/brannan/luma_map.png";
            var mapBlend4 = await BasicUtils.GetBitmap(path);
            path = @"/Assets/Filters/brannan/contrast_map.png";
            var mapBlend5 = await BasicUtils.GetBitmap(path);
            var maps = new WriteableBitmap[] { mapBlend, mapBlend2, mapBlend3, mapBlend4, mapBlend5 };
            var mapResult = BasicUtils.BlendBaseWithMap(baseImage, maps);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterWalden(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/walden/map.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = BasicUtils.BlendBaseWithMap(baseImage, mapBlend);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterHefe(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/hefe/map.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = BasicUtils.BlendBaseWithMap(baseImage, mapBlend);
            path = @"/Assets/Filters/hefe/gradient_map.png";
            mapBlend = await BasicUtils.GetBitmap(path);
            mapResult = BasicUtils.BlendBaseWithMap(mapResult, mapBlend);

            path = @"/Assets/Filters/hefe/metal.PNG";
            mapBlend = await BasicUtils.GetBitmap(path);
            mapResult = await BasicUtils.BlendBaseWithMode(mapResult, mapBlend, 0.4, BlendMode.SOFTLIGHT);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterF1997(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/f1977/map.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = BasicUtils.BlendBaseWithMap(baseImage, mapBlend);
            path = @"/Assets/Filters/f1977/screen_map.png";
            mapBlend = await BasicUtils.GetBitmap(path);
            mapResult = BasicUtils.BlendBaseWithMap(baseImage, mapBlend);
            return mapResult;
        }
        private static async Task<WriteableBitmap> FilterValencia(WriteableBitmap baseImage)
        {
            string path = @"/Assets/Filters/shared/blackboard.png";
            var mapBlend = await BasicUtils.GetBitmap(path);
            var mapResult = await BasicUtils.BlendBaseWithMode(baseImage, mapBlend, 0.4, BlendMode.GRADIENT);

            path = @"/Assets/Filters/valencia/map.png";
            mapBlend = await BasicUtils.GetBitmap(path);
            mapResult = BasicUtils.BlendBaseWithMap(baseImage, mapBlend);
            return mapResult;
        }
    }
}
