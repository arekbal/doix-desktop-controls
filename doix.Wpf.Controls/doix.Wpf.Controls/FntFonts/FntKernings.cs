using System.Xml.Serialization;

namespace doix.Wpf.Controls.FntFonts
{
    public struct FntKernings
    {
        [XmlElement("kerning")]
        public FntKerning[] Items;

        [XmlAttribute("count")]
        public int Count;
    }
}
