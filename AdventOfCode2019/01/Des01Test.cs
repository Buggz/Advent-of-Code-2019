using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019._01
{
    public class Des01Test
    {
        public static int Calculate(List<int> modules)
        {
            return modules.Sum(Calculate);
        }

        public static int Calculate(int module)
        {
            return module / 3 - 2;
        }
        
        public static int CalculateWithFuel(List<int> modules)
        {
            return modules.Sum(CalculateWithFuel);
        }

        public static int CalculateWithFuel(int module)
        {
            var total = module / 3 - 2;
            var fuel = total;
            
            while (true)
            {
                fuel = Calculate(fuel);
                if (fuel <= 0)
                    break;
                total += fuel;
            }

            return total;
        } 
    }
}