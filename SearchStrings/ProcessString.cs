using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SearchStrings
{
    class ProcessString
    {

        private string _Searchstring;

        public List<string> NOTlist { get; set; }
        public List<string> ANDlist { get; set; }
        

        public ProcessString(string Searchstring)
        {
            _Searchstring = Searchstring;
            ANDlist = new List<string>();
            NOTlist = new List<string>();

        }

        private string Cleanstring(string Uncleanstring)
        {
            Uncleanstring = Regex.Replace(Uncleanstring, "[()]", ""); //remove brackets
            return Uncleanstring.Trim();

        }

        private List<string> Splitter(string Searchstring, string Pattern)
        {
            var Searches = new List<string>();
            var Matches = Regex.Matches(Searchstring, Pattern);

            if (Matches.Count > 0)
            {
                var startindex = 0;

                //first substring - upto the first occurence of the pattern
                Searches.Add(Cleanstring(Searchstring.Substring(startindex, Matches[0].Index)));
                startindex = Matches[0].Index + Matches[0].Length;

                //Add middle AND statements if they exsist
                for (int i = 1; i < Matches.Count; i++)
                {
                    Searches.Add(Cleanstring(Searchstring.Substring(startindex, Matches[i].Index - startindex)));
                    startindex = Matches[i].Index + Matches[i].Length;
                }

                //last substring - after the last occurence of the pattern
                Searches.Add(Cleanstring(Searchstring.Substring(startindex)));
            }
            else
            {
                Searches.Add(Cleanstring(Searchstring));
            }

            return Searches;

        }

        public void Validate()
        {
            var NOTpattern = @" NOT ";
            var ANDpattern = @" AND ";

            var NOTmatches = Regex.Matches(_Searchstring, NOTpattern);

            if (NOTmatches.Count > 0)
            {
                 NOTlist = Splitter(_Searchstring.Substring(NOTmatches[0].Index + NOTmatches[0].Length), NOTpattern);
                _Searchstring = _Searchstring.Substring(0, NOTmatches[0].Index); 
            }

            ANDlist = Splitter(_Searchstring, ANDpattern);
        }





    }
}
