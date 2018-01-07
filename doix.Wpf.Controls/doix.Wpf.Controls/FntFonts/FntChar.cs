using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace doix.Wpf.Controls.FntFonts
{
    public struct FntChar
    {
        [XmlAttribute("id")]
        public int Id;

        public char Char => (char)Id;

        [XmlAttribute("x")]
        public int X;

        [XmlAttribute("y")]
        public int Y;

        [XmlAttribute("width")]
        public int Width;

        [XmlAttribute("height")]
        public int Height;

        [XmlAttribute("xoffset")]
        public int XOffset;

        [XmlAttribute("yoffset")]
        public int YOffset;

        [XmlAttribute("xadvance")]
        public int XAdvance;

        [XmlAttribute("page")]
        public int Page;

        [XmlAttribute("chnl")]
        public int Chnl;
    }
}
