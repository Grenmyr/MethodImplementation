using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace oktogit
{
    public class RepositoryScraper
    {
        // Add Path to folder here, and set extension to *.cs or *.js
        public string[] _filePaths;

        public RepositoryScraper(string [] filePaths)
        {
            _filePaths = filePaths;
        }

        /// <summary>
        /// Main Method to analyze all files in filePaths array
        /// </summary>
        public void AnalyzeCSFiles()
        {

            // Total lines scanned
            int totalLines = 0;
            // Total amount of lines with comments // or /**/
            int totalCommentLines = 0;
            // Total length by char of all comments.
            int totalCommentsLength = 0;
            // Total mount of todo comments
            int todoComment = 0;

            using (TextWriter tw = new StreamWriter("CommentSummary.txt"))
            {
                // loop through all files
                foreach (var item in _filePaths)
                {
                    
                    // exclude all files that start with AssemblyInfo and Jasmine
                    if (!(item.Contains("AssemblyInfo.cs") || item.Contains("Jasmine")))
                    {
                        tw.WriteLine("--------------------------------------------------------------------");
                        tw.WriteLine(String.Format("Filepath Analyzed was {0}", item));
                        tw.WriteLine();
                        string line;
                        using (StreamReader reader = new StreamReader(item))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                // Add one line for eatch line in file.
                                totalLines++;
                                // match for line comment and not start with documentation block. (don't want documentation blocks)
                                if (line.Contains("//")  && !line.Trim().StartsWith("///"))
                                {
                                    var lines = line.Split(new string[] { "//" },StringSplitOptions.RemoveEmptyEntries);

                                    // Check it is not an url will remove most invalid comments.
                                    if (lines.Length > 1 && !(lines[0].ToLower().Contains("http:") || lines[0].ToLower().Contains("https:")))
                                    {
                                        tw.WriteLine(line);
                                        totalCommentLines++;
                                        // Have to remove emty space at start and end of line.
                                        totalCommentsLength += line.Trim().Length;

                                        if (line.Contains("TODO") || line.Contains("TODO:"))
                                        {
                                            todoComment++;
                                        }
                                    }                                  
                                }                    
                            }
                        }
                    }
                    else
                    {
                        tw.WriteLine(String.Format("Filepath IGNORED was {0}", item));
                    }
                }
                tw.WriteLine();
                tw.WriteLine("TOTAL LINES SCANNED");
                tw.WriteLine(totalLines);
                tw.WriteLine("TOTAL COMMENTS LINES");
                tw.WriteLine(totalCommentLines);
                tw.WriteLine("AVERAGE COMMENTS CHARACTER LENGHT");
                tw.WriteLine(totalCommentsLength / totalCommentLines);
                tw.WriteLine("TOTAL TODO COMMENTS");
                tw.WriteLine(todoComment);
            }
        }
    }
}