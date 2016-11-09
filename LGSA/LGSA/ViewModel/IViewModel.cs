using LGSA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LGSA.ViewModel
{
    public interface IViewModel
    {
        Task Load();
    }
}
