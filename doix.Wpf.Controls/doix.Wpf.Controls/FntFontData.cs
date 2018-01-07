using doix.Wpf.Controls.FntFonts;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.Wpf.Controls
{
    class FntFontData : IDisposable
    {
        public readonly FntFonts.FntFont FntFont;
        public readonly Texture Texture;
        public readonly IReadOnlyDictionary<char, FntFonts.FntChar> FntCharacters;
        OpenGL gl;

        public FntFontData(OpenGL gl, string fntFilepath)
        {
            this.gl = gl;
            FntFont = FntFonts.FntFont.Read(fntFilepath);
            Texture = new Texture();
            FntCharacters = FntFont.Chars.Items.ToDictionary(x => (Char)x.Id);
            if (!Texture.Create(gl, GetFontTexturePath(fntFilepath, FntFont)))
                throw new Exception("texture loading failed");
        }

        string GetFontTexturePath(string fntPath, FntFont font) => Path.Combine(Path.GetDirectoryName(fntPath), font.Pages.Items[0].FilePath);

        public void Dispose() => Texture.Destroy(gl);
    }
}
