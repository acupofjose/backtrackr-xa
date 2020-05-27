using System;
using Xamarin.Forms;

namespace BacktrackrXA
{
    public static class Constants
    {
        public static OnPlatform<string> FontAwesomeBrands => (OnPlatform<string>)App.Instance.Resources[nameof(FontAwesomeBrands)];
        public static OnPlatform<string> FontAwesomeSolid => (OnPlatform<string>)App.Instance.Resources[nameof(FontAwesomeSolid)];
        public static OnPlatform<string> FontAwesomeRegular => (OnPlatform<string>)App.Instance.Resources[nameof(FontAwesomeRegular)];
        public static OnPlatform<string> FontAwesomeDuotone => (OnPlatform<string>)App.Instance.Resources[nameof(FontAwesomeDuotone)];
    }
}
