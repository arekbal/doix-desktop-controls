using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using doix.desktop.core.controls;
using doix.desktop.core.controls.Helpers;
using SharpGL;

namespace doix.forms.controls.demo.Scenarios.Base
{
  abstract class SpriteBatchScenarioBase : GLScenarioBase
  {
    public SpriteBatch SpriteBatch { get; private set; }

    protected static readonly Color[] TestColors = new Color[] { Color.Bisque, Color.Azure, Color.AliceBlue, Color.Aqua, Color.Brown, Color.Chocolate, Color.Yellow, Color.CornflowerBlue };

    protected static readonly Random Random = new Random();

    protected static Color TestColor => TestColors[Random.Next() % TestColors.Length];

    protected override void OnInitialize()
      => SpriteBatch = new SpriteBatch(GL, 100000);

    public override void Clear()
    { 
      SpriteBatch.Dispose();
      SpriteBatch = null;
    }
  }
}
