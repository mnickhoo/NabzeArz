using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NabzeArz.Models
{
    public interface ISchedule
    {
       Task Run();
    }
}