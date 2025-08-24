using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Operations.Setting
{
    public interface ISettingService
    {
        Task ToggleMaintenance();

        bool GetMaintenanceState();
    }
}
