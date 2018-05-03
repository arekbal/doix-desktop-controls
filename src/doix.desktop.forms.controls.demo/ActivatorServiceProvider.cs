using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.forms.controls.demo
{
  public class ActivatorServiceProvider : IServiceProvider
  {
    public object GetService(Type serviceType) => Activator.CreateInstance(serviceType);
  }
}
