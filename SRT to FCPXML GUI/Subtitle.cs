using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRT_to_FCPXML_Core
{
    internal class Subtitle
    {
        private string text;
        private string beginningTime;
        private string endingTime;

        public Subtitle(string text, string beginningTime, string endingTime)
        {
            this.text = text;
            this.beginningTime = beginningTime;
            this.endingTime = endingTime;
        }

        public string toString()
        {
            return "Starting Time: " + beginningTime + " Ending Time: " + endingTime + " Text: " + text;
        }

        public string create_FCPXML_title()
        {
            string res = "";

            int opening = TimeConverter.convert(beginningTime);
            int end = TimeConverter.convert(endingTime);
            int duration = end - opening;

            String line1 = "                        <title name= \"Rich\" start=\"" + opening + "/60s\" duration=\"" + duration + "/60s\" offset=\"" + opening + "/60s\" enabled=\"1\" ref=\"r1\">\n";
            String line2 = "                            <text roll-up-height=\"0\">\n";
            String line3 = "                                <text-style ref=\"ts0\">" + text + "</text-style>\n";
            String line4 = "                            </text>\n" +
                    "                            <text-style-def id=\"ts0\">\n" +
                    "                                <text-style strokeColor=\"0 0 0 1\" lineSpacing=\"0\" font=\"Open Sans\" strokeWidth=\"0\" fontColor=\"1 1 1 1\" bold=\"1\" italic=\"0\" alignment=\"center\" fontSize=\"96\"/>\n" +
                    "                            </text-style-def>\n" +
                    "                            <adjust-transform scale=\"1 1\" anchor=\"0 0\" position=\"0 0\"/>\n" +
                    "                        </title>\n";



            res = line1 + line2 + line3 + line4;
            return res;
        }

        public int getOpening()
        {
            return TimeConverter.convert(beginningTime);
        }
    }
}
