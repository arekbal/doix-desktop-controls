using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.Wpf.Controls
{
    static class SpriteBatchExtensions
    {
        class SpriteBatchScope : IDisposable
        {
            SpriteBatch sb;

            public SpriteBatchScope(SpriteBatch sb)
            {
                this.sb = sb;
            }

            public void Dispose() 
                => sb.End();
        }

        public static IDisposable BeginScope(this SpriteBatch that)
        {
            that.Begin();
            return new SpriteBatchScope(that);
        }
    }
}
