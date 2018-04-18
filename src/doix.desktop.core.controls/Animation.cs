using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.desktop.core.controls
{
  public struct Animation
  {
    public const double CubicPowerDefault = 0.77d;

    public struct BetweenValues
    {
      public Animation Time;
      public double OldValue;
      public double NewValue;

      public double GetLinear(double currentTime)
        => OldValue + Time.GetLinear(currentTime) * (NewValue - OldValue);

      public double GetCubic(double currentTime, double power= CubicPowerDefault)
        => OldValue + Time.GetCubic(currentTime, power) * (NewValue - OldValue);

      double GetWith(double currentTime, Func<double, double> method)
        => OldValue + method(currentTime) * (NewValue - OldValue);
    }

    public double StartTime;
    public double EndTime;

    public double GetLinear(double currentTime)
    {
      if (StartTime >= currentTime)
        return 0d;

      if (EndTime <= currentTime)
        return 1d;

      var length = EndTime - StartTime;
      return (currentTime - StartTime) / length;
    }

    public double GetCubic(double currentTime, double power= CubicPowerDefault)
    {
      if (StartTime >= currentTime)
        return 0d;

      if (EndTime <= currentTime)
        return 1d;

      var length = EndTime - StartTime;
      var pos = (currentTime - StartTime);

      return Math.Pow(pos / length, power);
    }

    public bool HasFinished(double currentTime)
      => EndTime < currentTime;
  }
}
