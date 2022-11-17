using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serverside.Config.Models
{
    public class LevelsConfig
    {
        public Dictionary<int, int> LevelSteps { get; set; } = new Dictionary<int, int>();
    }
}
