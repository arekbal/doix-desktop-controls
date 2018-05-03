using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace doix.forms.controls.demo.Scenarios.Base
{
  static class ScenarioResolver
  {
    public static ScenarioBase[] GetScenariosFromAssembly(Assembly asm, IServiceProvider serviceProvider)
    {
      var scenarioTypes = asm
              .GetTypes()
              .Where(t => t.GetBaseTypes().Contains<ScenarioBase>())
              .Where(t => !t.IsAbstract);

      var scenarios = scenarioTypes
        .Select(t => serviceProvider.GetService(t))
        .Cast<ScenarioBase>()
        .ToArray();

      return scenarios;
    }
  }
}
