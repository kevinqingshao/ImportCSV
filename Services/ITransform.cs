using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImportCSV.Models;

namespace ImportCSV.Services
{
    public interface ITransform
    {
         void Parse(string[] source, StandardAccount target);
    }
}
