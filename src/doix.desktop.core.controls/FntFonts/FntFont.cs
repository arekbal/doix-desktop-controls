using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace doix.desktop.core.controls.FntFonts
{
    [XmlRoot(ElementName = "font")]
    public struct FntFont
    {
        [XmlElement("info")]
        public FntInfo Info;

        [XmlElement("pages")]
        public FntPages Pages;

        [XmlElement("chars")]
        public FntChars Chars;

        [XmlElement("common")]
        public FntCommon Common;

        [XmlElement("kernings")]
        public FntKernings Kernings;

        public static FntFont Read(string fntPath)
        {
            var serializer = new XmlSerializer(typeof(FntFont));

            using (var reader = File.OpenRead(fntPath))
            {
                return (FntFont)serializer.Deserialize(reader);
            }
        }
    }
}
