using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doix.forms.controls.demo.Scenarios
{
  class SomeSquaresScenario : Base.ScenarioBase
  {
    public override void Clear() {}

    public override Control Initialize()
    {
      var label = new Label();
      label.Text = "Hello WOrld";
      return label;
    }
  }
}
