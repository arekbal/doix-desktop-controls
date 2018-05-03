using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using doix.forms.controls.demo;
using doix.forms.controls.demo.Scenarios;
using doix.forms.controls.demo.Scenarios.Base;

namespace doix.desktop.forms.controls.demo
{
  public partial class TestForm : Form
  {
    IServiceProvider serviceProvider;

    public TestForm()
    {
      InitializeComponent();

      serviceProvider = new ActivatorServiceProvider();

      cbScenarios.DisplayMember = nameof(ScenarioBase.ScenarioName);

      var scenarios = ScenarioResolver.GetScenariosFromAssembly(this.GetType().Assembly, serviceProvider);

      cbScenarios.Items.AddRange(scenarios);

      int prevIndex = 0;
      cbScenarios.SelectedIndexChanged += (s, e) =>
      {
        if (prevIndex != cbScenarios.SelectedIndex)
          scenarios[prevIndex].Clear();

        viewport.Controls.Clear();
        var control = scenarios[cbScenarios.SelectedIndex].Initialize();
        control.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
        control.Size = viewport.Size;

        viewport.Controls.Add(control);
        prevIndex = cbScenarios.SelectedIndex;
      };

      cbScenarios.SelectedIndex = 0;
    }
  }
}
