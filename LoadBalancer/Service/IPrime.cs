using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IPrime
    {
        Boolean isPrime(int prime);
        List<int> countPrime(int from, int to);
    }
}
