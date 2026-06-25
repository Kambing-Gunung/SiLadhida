using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiLadhida.App.Services.Observer
{
    public interface IObserver
    {
        void UpdateNotification(string message);
    }
}
