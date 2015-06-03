using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFilter
{
    public class Config
    {

        /// <summary>
        /// Path to mask mode image
        /// </summary>
        public const string OVERLAY_MAP = @"/Assets/Filters/shared/overlay_map.png";
        public const string VIGNETTE_MAP = @"/Assets/Filters/shared/vignette_map.png";
        public const string MAP_2D_PATH = @"/Assets/Filters/earlybird/map_2d.png";
        public const string HARDLIGHT_MAP = @"/Assets/Filters/shared/hardlight_map.png";
        public const string BLACKOVERLAY_MAP = @"/Assets/Filters/shared/black_overlay_map.png";
        public const string SOFTLIGHT_MAP = @"/Assets/Filters/shared/soft_light_map.png";
        public const string GRADIENT_MAP = @"/Assets/Filters/valencia/gradient_map.png";

        /// <summary>
        /// default size of image
        /// </summary>
        public const int SIZE = 640;
        public const int MAP_SIZE = 256;

    }

    public enum BlendMode
    {
        OVERLAY,
        BLACK_OVERLAY,
        VIGNETTE,
        MAP_2D,
        HARDLIGHT,
        SOFTLIGHT,
        GRADIENT
    }

    public enum Filter
    {
        INKVELL,
        NASHVILLE,
        RISE,
        KELVIN,
        XPRO,
        AMARO,
        LOFI,
        MAYFAIR,
        HUDSON,
        EARLYBIRD,
        SUTRO,
        TOASTER,
        BRANNAN,
        WALDEN,
        HEFE,
        F1977,
        VALENCIA
    }
}
