using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Helper
{
    public class CalculateProcess
    {
        public static Tuple<string , int> makeProcess(int UserPoint)
        {
            int total = 1000;
            int blockValue = 50;
            int totalBlocks = 20;

            string processBar = "";
            
            int userBlock = UserPoint / blockValue;

            var percentage = (100 / totalBlocks) * userBlock; 
            int remain = totalBlocks - userBlock;
            while (userBlock > 0)
            {
                processBar += "█";
                userBlock--; 
            }
            while (remain > 0)
            {
                processBar += "_̲̅";
                remain--; 
            }
            processBar += "]";

            return new Tuple<string, int>(processBar, percentage );
        }
    }
}