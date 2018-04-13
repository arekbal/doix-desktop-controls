using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace doix.core.controls.FntFonts
{
    public struct FntCommon
    {
        [XmlAttribute("lineHeight")]
        public int LineHeight;

        [XmlAttribute("base")]
        public int Base;

        [XmlAttribute("scaleW")]
        public int ScaleWidth;

        [XmlAttribute("scaleH")]
        public int ScaleHeight;

        [XmlAttribute("pages")]
        public int Pages;

        [XmlAttribute("packed")]
        public int Packed;

        [XmlAttribute("alphaChnl")]
        public int AlphaChannelBits;

        [XmlAttribute("redChnl")]
        public int RedChannelBits;

        [XmlAttribute("greenChnl")]
        public int GreenChannelBits;

        [XmlAttribute("blueChnl")]
        public int BlueChannelBits;
    }
}
