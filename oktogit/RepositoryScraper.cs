using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace oktogit
{
    public class RepositoryScraper
    {
        //TODO: Not done implementing this class yet only tested it works.
        // Get all files in folder and subfolders that contain .cs as filending
        public string[] filePaths = Directory.GetFiles(@"C:\Users\dav\Documents\GitHub\oktogit\oktogit\", "*.cs",
                                         SearchOption.AllDirectories);


        private String _path;
        private String _fileEnd;
        public RepositoryScraper(string localFilepath, string fileEnd)
        {
            _path = localFilepath;
            _fileEnd = fileEnd;
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
                foreach (var item in filePaths)
                {
                    // exclude all files that start with AssemblyInfo
                    if (!item.Contains("AssemblyInfo"))
                    {
                        string line;
                        using (StreamReader reader = new StreamReader(item))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                // Add one line for eatch line in file.
                                totalLines++;

                                if (line.Contains("//"))
                                {
                                    tw.WriteLine(line);
                                    totalCommentLines++;
                                    totalCommentsLength += line.Length;

                                    if (line.Contains("TODO"))
                                    {
                                        todoComment++;
                                    }

                                }

                                if (line.Contains("/*"))
                                {
                                    tw.WriteLine(line);
                                    totalCommentLines++;
                                    totalCommentsLength += line.Length;

                                    while ((line = reader.ReadLine()) != null)
                                    {

                                        tw.WriteLine(line);
                                        totalCommentLines++;
                                        totalCommentsLength += line.Length;
                                        if (line.Contains("*/")) { break; }
                                    }
                                }
                            }
                        }
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