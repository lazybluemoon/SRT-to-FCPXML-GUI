using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This is the file that handles the file reading and parsing

namespace SRT_to_FCPXML_Core
{
    internal class SubtitleManager
    {
        private List<Subtitle> subtitles;
        public SubtitleManager(string srtURL)
        {
            string[] lines = System.IO.File.ReadAllLines(srtURL);
            subtitles = new List<Subtitle>();

            for(int i = 0; i < lines.Length; i++) {
                i++;//Skip the first line
                string times = lines[i];//Get the times
                Console.WriteLine(times);
                i++;//next line
                string text = lines[i];//get text
                Console.WriteLine(text);
                i++;//next line
                if(i < lines.Length && lines[i] != "") {
                    text += " " + lines[i];
                    i++;
                }//if there is a multiline text, concatenate them
                Console.WriteLine(text);

                String[] time_split = times.Split(" --> ");//split the times up

                Subtitle current_subtitle = new Subtitle(text, time_split[0], time_split[1]);
                subtitles.Add(current_subtitle);//instantiate subtitle object and add it to the list
            }
        }

        public List<Subtitle> getSubtitles()
        {
            return subtitles;
        }
    }
}
