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
            this.text = this.text.Replace(",", "");
            this.text = this.text.Replace(".", "");

            string[] pieces = this.text.Split(" ");
            string first = ""+pieces[1][0];
            pieces[1] = first.ToUpper() + pieces[1].Substring(1,pieces[1].Length-1);
            this.text = "";
           for(int i = 0; i < pieces.Length;i++)
            {
                this.text += " " + pieces[i];
            }
            this.text = this.text.Trim();
            

            this.beginningTime = beginningTime;
            this.endingTime = endingTime;
        }

        public string toString()
        {
            return "Starting Time: " + beginningTime + " Ending Time: " + endingTime + " Text: " + text;
        }

        public string create_FCPXML_title(string font = "Berlin Sans FB Demi")
        {
            string res = "";

            int opening = TimeConverter.convert(beginningTime);
            int end = TimeConverter.convert(endingTime);
            int duration = end - opening;

            if (findColor() == "")
            {

                String line1 = "                        <title lane= \"3\" name= \"Rich\" start=\"" + opening + "/60s\" duration=\"" + duration + "/60s\" offset=\"" + opening + "/60s\" enabled=\"1\" ref=\"r1\">\n";
                String line2 = "                            <text roll-up-height=\"0\">\n";
                String line3 = "                                <text-style ref=\"ts0\">" + text + "</text-style>\n";
                String line4 = "                            </text>\n" +
                                                    "                            <text-style-def id=\"ts0\">\n" +
                                       "                            <text-style font=\""+font+"\" bold=\"1\" fontColor=\"1 1 1 1\" lineSpacing=\"0\" strokeWidth=\"5\" strokeColor=\"0 0 0 1\" fontSize=\"96\" italic=\"0\" alignment=\"center\"/>\n" +
                                    "                            </text-style-def>\n" +
                        "                            <adjust-transform scale=\"1 1\" anchor=\"0 0\" position=\"0 -37.1296\"/>\n" +
                        "                        </title>\n";



                res = line1 + line2 + line3 + line4;
            } 
            
            else
            {
                string[] new_text = text.Split(" ");
                string output = "";
                for(int i = 1; i < new_text.Length;i++)
                {
                    output += new_text[i] + " ";
                }

                output = output.Trim();

                String line1 = "                        <title lane= \"4\" name= \"Rich\" start=\"" + opening + "/60s\" duration=\"" + duration + "/60s\" offset=\"" + opening + "/60s\" enabled=\"1\" ref=\"r1\">\n";
                String line2 = "                            <text roll-up-height=\"0\">\n";
                String line3 = "                                <text-style ref=\"ts0\">" + output + "</text-style>\n";
                String line4 = "                            </text>\n" +
                                                    "                            <text-style-def id=\"ts0\">\n" +
                                       "                            <text-style font=\"" + font + "\" bold=\"1\" fontColor=\"1 1 1 1\" lineSpacing=\"0\" strokeWidth=\"5\" strokeColor=\"0 0 0 1\" fontSize=\"96\" italic=\"0\" alignment=\"center\"/>\n" +
                                    "                            </text-style-def>\n" +
                        "                            <adjust-transform scale=\"1 1\" anchor=\"0 0\" position=\"0 -37.1296\"/>\n" +
                        "                        </title>\n";

                String line5 = "                        <title lane= \"3\" name= \"Rich\" start=\"" + opening + "/60s\" duration=\"" + duration + "/60s\" offset=\"" + opening + "/60s\" enabled=\"1\" ref=\"r1\">\n";
                String line6 = "                            <text roll-up-height=\"0\">\n";
                String line7 = "                                <text-style ref=\"ts0\">" + output + "</text-style>\n";
                String line8 = "                            </text>\n" +
                                                    "                            <text-style-def id=\"ts0\">\n" +
                                       "                            <text-style font=\"" + font + "\" bold=\"1\" fontColor=\"" + findColor()+ "\" lineSpacing=\"0\" strokeWidth=\"3\" strokeColor=\"" + findColor() + "\" fontSize=\"96\" italic=\"0\" alignment=\"center\"/>\n" +
                                    "                            </text-style-def>\n" +
                        "                            <adjust-transform scale=\"1 1\" anchor=\"0 0\" position=\"0.740741 -36.6667\"/>\n" +
                        "                        </title>\n";
                res = line1 + line2 + line3 + line4 + line5 + line6 + line7 + line8;
            }
            return res;
        }

        public int getOpening()
        {
            return TimeConverter.convert(beginningTime);
        }
        
        public string findColor()
        {
            string[] pieces = text.Split(" ");

            switch (pieces[0].ToLower()) {
                case "red":
                    return "1 0 0 1";
                case "blue":
                    return "0 0.317647 1 1";
                case "green":
                    return "0 1 0.498039 1";
                case "yellow":
                    return "1 1 0 1";
                case "orange":
                    return "1 0.666667 0 1";
                case "purple":
                    return "0.701961 0 1 1";
                case "pink":
                    return "1 0 0.764706 1";
                case "cyan":
                    return "0 1 1 1";
                case "brown":
                    return "0.333333 0 0 1";
                default:
                    return "";
            }
        }
    }
}
