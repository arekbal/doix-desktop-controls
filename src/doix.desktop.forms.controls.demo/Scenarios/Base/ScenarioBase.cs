using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doix.forms.controls.demo.Scenarios.Base
{
  abstract class ScenarioBase : IDisposable
  {
    protected readonly Type ScenarioType;
    public ScenarioBase()
    {
      ScenarioType = this.GetType();
    }

    protected virtual string GetScenarioName()
    {
      var scenarioName = ScenarioType.Name;
      if (scenarioName.EndsWith("Scenario"))
        scenarioName = scenarioName.Substring(0, scenarioName.Length - "Scenario".Length);

      return scenarioName;
    }

    public abstract Control Initialize();

    public abstract void Clear();

    public string ScenarioName => GetScenarioName();
   
    public bool IsDisposed { get; private set; } 

    protected virtual void Dispose(bool disposing)
    {
      if (IsDisposed)
        return;

      if (disposing)
      {
        // TODO: dispose managed state (managed objects).
      }

      // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
      // TODO: set large fields to null.

      IsDisposed = true;
    }

    //TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
     ~ScenarioBase()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(false);
    }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
     
      GC.SuppressFinalize(this);
    }
  }
}
