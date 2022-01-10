using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImportCSV.Models;

namespace ImportCSV.Services
{
    public class Type1Transform: ITransform
    {
        public void Parse(string[] source, StandardAccount target)
        {
            string[] accountCode = source[0].Split('|');
            target.AccountCode = accountCode[1]; 
            target.Name = source[1];
            target.Type = source[2];
            switch (source[2])
            {
                case "1":
                    target.Type = "Trading";
                    break;
                case "2":
                    target.Type = "RRSP";
                    break;
                case "3":
                    target.Type = "RESP";
                    break;
                case "4":
                    target.Type = "Fund";
                    break;
            }
            target.OpenDate = (source[3].Length == 0) ? null : DateTime.Parse(source[3]);
            switch (source[4])
            {
                case "CD":
                    target.Currency = "CAD";
                    break;
                case "US":
                    target.Currency = "USD";
                    break;
                default:
                    target.Currency = "";
                    break;
            }
        }
    }

    public class Type2Transform : ITransform
    {
        public void Parse(string[] source, StandardAccount target)
        {
            target.Name = source[0];
            target.Type = source[1];
            switch (source[2])
            {
                case "C":
                    target.Currency = "CAD";
                    break;
                case "U":
                    target.Currency = "USD";
                    break;
                default:
                    target.Currency = "";
                    break;
            }
            target.AccountCode = source[3];
        }
    }

    public class StandardTransform : ITransform
    {
        public void Parse(string[] source, StandardAccount target)
        {
            target.AccountCode = source[0];
            target.Name = source[1];
            target.Type = source[2];
            target.OpenDate = (source[3].Length == 0) ? null : DateTime.Parse(source[3]);
            target.Currency = source[4];
        }
    }

    public class Transform
    {
        private ITransform _transform;
        public Transform(ITransform chosenCSVFile)
        {
            this._transform = chosenCSVFile;
        }
        public void Parse(string[] source, StandardAccount target)
        {
            _transform.Parse(source, target);
        }
    }
}
