using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRT_to_FCPXML_Core
{
    internal class TimeConverter
    {
        public static int convert(string time)
        {
            int frame = 3600 * 60;
            string result = "";
            
            string[] pieces = time.Split(":");
            for (int i = 0; i < pieces.Length; i++)
            {
                Console.WriteLine(pieces[i]);
            }
            string[] seconds_millisecond = pieces[2].Split(",");
            frame += Convert.ToInt32(pieces[0]) * 3600 * 60;
            frame += Convert.ToInt32(pieces[1]) * 60 * 60;
            frame += Convert.ToInt32(seconds_millisecond[0]) * 60;
            frame += (int)(Convert.ToDouble(seconds_millisecond[1]) / (1000.0 / 60));
            return frame;
        }
    }
}
