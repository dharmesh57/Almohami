using Almohami.Data.AlmohamiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
  public class BaseViewModel
    {
      
      public BaseViewModel() {
          ModuleList = new List<ApplicationModule>();
      }
        public List<ApplicationModule> ModuleList { get; set; }
    }
}
