using System;
using Xamarin.Forms;

namespace BacktrackrXA.Controls
{
    public class FALabel : Label
    {
        public enum FAType
        {
            Brands,
            Duotone,
            Regular,
            Solid
        }

        public FALabel()
        {
            FontFamily = Constants.FontAwesomeSolid;
            FontSize = 16;
        }

        public FALabel(FAType type = FAType.Solid) : this()
        {
            switch (type)
            {
                case FAType.Brands:
                    FontFamily = Constants.FontAwesomeBrands;
                    break;
                case FAType.Duotone:
                    FontFamily = Constants.FontAwesomeDuotone;
                    break;
                case FAType.Regular:
                    FontFamily = Constants.FontAwesomeRegular;
                    break;
                case FAType.Solid:
                    FontFamily = Constants.FontAwesomeSolid;
                    break;
            }
        }
    }
}
