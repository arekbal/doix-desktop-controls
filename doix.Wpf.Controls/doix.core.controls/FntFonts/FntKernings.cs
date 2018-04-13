using System.Xml.Serialization;

namespace doix.core.controls.FntFonts
{
    public struct FntKernings
    {
        [XmlElement("kerning")]
        public FntKerning[] Items;

        [XmlAttribute("count")]
        public int Count;
    }
}
