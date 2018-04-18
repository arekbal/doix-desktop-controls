using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace doix.desktop.wpf.controls.FntFonts
{
    public struct FntPages
    {
        [XmlElement("page")]
        public FntPage[] Items { get; set; }      
    }
}
