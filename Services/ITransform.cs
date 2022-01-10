using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImportCSV.Models;

namespace ImportCSV.Services
{
    public interface ITransform<T, U> where T : class where U : class
    {
         void Parse(T source, U target);
    }
}
