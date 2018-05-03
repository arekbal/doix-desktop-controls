using System;
using System.Collections.Generic;
using System.Text;

namespace doix.utils
{
  public interface IMove<TOut>
    where TOut : struct
  {
    TOut Move();
  }
}
