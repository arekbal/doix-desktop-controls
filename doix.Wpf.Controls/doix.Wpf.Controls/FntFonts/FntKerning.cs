using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace doix.Wpf.Controls.FntFonts
{
    public struct FntKerning
    {
        [XmlAttribute("first")]
        public int First;

        [XmlAttribute("second")]
        public int Second;

        [XmlAttribute("amount")]
        public int Amount;
    }
}
