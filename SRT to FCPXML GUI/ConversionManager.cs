using SRT_to_FCPXML_Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRT_to_FCPXML_GUI
{
    internal class ConversionManager
    {
        SubtitleManager sbm;
        string opening_boilerplate = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
            "<!DOCTYPE fcpxml>\n" +
            "<fcpxml version=\"1.9\">\n" +
            "    <resources>\n" +
            "        <format frameDuration=\"1/60s\" height=\"1080\" name=\"FFVideoFormat1080p60\" width=\"1920\" id=\"r0\"/>\n" +
            "        <effect name=\"Basic Title\" uid=\".../Titles.localized/Bumper:Opener.localized/Basic Title.localized/Basic Title.moti\" id=\"r1\"/>\n" +
            "    </resources>\n" +
            "    <library>\n" +
            "        <event name=\"Subtitles\">\n" +
            "            <project name=\"Subtitles\">\n" +
            "                <sequence format=\"r0\" tcStart=\"3600/1s\" duration=\"1258/60s\" tcFormat=\"NDF\">\n" +
            "                    <spine>\n" +
            "<title lane=\"2\" ref=\"r1\" name=\"Rich\" duration=\"5/1s\" start=\"3600/1s\" offset=\"3600/1s\" enabled=\"1\">\n" +
            "<text roll-up-height=\"0\">\n" +
            "<text-style ref=\"ts0\">Delete Me 1</text-style>\n" +
            "</text>\n" +
            "<text-style-def id=\"ts0\">\n" +
            " <text-style font=\"Open Sans\" strokeWidth=\"0\" fontSize=\"96\" bold=\"1\" alignment=\"center\" italic=\"0\" fontColor=\"1 1 1 1\" strokeColor=\"0 0 0 1\" lineSpacing=\"0\"/>\n" +
            "</text-style-def>\n" +
            "<adjust-transform anchor=\"0 0\" position=\"0 0\" scale=\"1 1\"/>\n" +
            "</title>"+
            "<title lane=\"1\" ref=\"r1\" name=\"Rich\" duration=\"5/1s\" start=\"3600/1s\" offset=\"3600/1s\" enabled=\"1\">\n" +
            "<text roll-up-height=\"0\">\n" +
            "<text-style ref=\"ts0\">Delete Me 2</text-style>\n" +
            "</text>\n" +
            "<text-style-def id=\"ts0\">\n" +
            " <text-style font=\"Open Sans\" strokeWidth=\"0\" fontSize=\"96\" bold=\"1\" alignment=\"center\" italic=\"0\" fontColor=\"1 1 1 1\" strokeColor=\"0 0 0 1\" lineSpacing=\"0\"/>\n" +
            "</text-style-def>\n" +
            "<adjust-transform anchor=\"0 0\" position=\"0 0\" scale=\"1 1\"/>\n" +
            "</title>";
        string closing_boilerplate = " </spine>\n" +
            "                </sequence>\n" +
            "            </project>\n" +
            "        </event>\n" +
            "    </library>\n" +
            "</fcpxml>\n";
        string outputFolder = "";
        string outputName = "";
        public ConversionManager(string input_url,string output_folder)
        {
            sbm = new SubtitleManager(input_url);
            string[] input_url_brokenup = input_url.Split("\\");

            outputName = input_url_brokenup[input_url_brokenup.Length - 1].Replace(".srt",".fcpxml");
            outputFolder = output_folder;
        }

        public void printFile()
        {
            Debug.WriteLine("We are about to print the file: " + outputName);
            List<Subtitle> list = sbm.getSubtitles(); //Get the subtitles

            try
            {
                using (StreamWriter writer = new StreamWriter(outputFolder+"\\"+outputName))
                {
                    writer.Write(opening_boilerplate+"\n");
                    writer.Write("                            <gap name=\"Gap\" start=\"3600/1s\" duration=\"" + (list[0].getOpening() - 3600 * 60) + "/60s\" offset=\"3600/1s\"/>");
                    foreach(Subtitle subtitle in list)
                    {
                        writer.Write(subtitle.create_FCPXML_title()+"\n");
                    }
                    writer.Write(closing_boilerplate);
                    writer.Close();
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }

        }
    }
}
