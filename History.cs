using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class History
    {
        int enk;
        int level;
        string hard;
        DateTime timeAccess;

        public History(int enk, int level, string hard, DateTime timeAccess)
        {
            this.enk = enk;
            this.level = level;
            this.hard = hard;
            this.timeAccess = timeAccess;
        }

        public int Enk { get => enk; set => enk = value; }
        public int Level { get => level; set => level = value; }
        public string Hard { get => hard; set => hard = value; }
        public DateTime TimeAccess { get => timeAccess; set => timeAccess = value; }
    }
}
