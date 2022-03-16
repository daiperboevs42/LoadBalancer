using System;
using System.Collections.Generic;

namespace API.Service
{
    public interface IPrime
    {
        Boolean isPrime(int prime);
        List<int> countPrime(int from, int to);
    }
}
