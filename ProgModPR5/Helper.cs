using ProgModPR5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgModPR5
{
    internal class Helper
    {
        private static ProgramModPR5Entities1 _context;

        public static ProgramModPR5Entities1 GetContext()
        {
            if (_context == null)
            {
                _context = new ProgramModPR5Entities1();
            }
            return _context;
        }
    }
}
