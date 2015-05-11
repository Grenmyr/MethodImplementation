using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;
using System.Diagnostics;

namespace oktogit.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public async Task GetMatchingRepositoriesWithSearchKey()
        {
            var go = new RepositorySearch("Key", new string[] { "insertGithubYourUserName", "insertyourGithubPassword" });
            await go.GetTopRatedReposLanguageCSharp();
            await go.GetTopRatedReposLanguageJavascript();

            go.GetMatch();
        }

        [TestMethod]
        public void ScrapeRepositoriesCSharp()
        {
            var go = new RepositoryScraper(Directory.GetFiles(@"C:\Users\dav\Documents\GitHub\Teoretiskt arbete\CSharp", "*.cs",
                                         SearchOption.AllDirectories));
            go.AnalyzeCSFiles();
            Process.Start("C:/Users/dav/Documents/GitHub/MethodImplementation/oktogit.Tests/bin/Debug/CommentSummary.txt");

        }
        [TestMethod]
        public void ScrapeRepositoriesJavascript()
        {
            var go = new RepositoryScraper(Directory.GetFiles(@"C:\Users\dav\Documents\GitHub\Teoretiskt arbete\Javascript", "*.js",
                                         SearchOption.AllDirectories));
            go.AnalyzeCSFiles();
            Process.Start("C:/Users/dav/Documents/GitHub/MethodImplementation/oktogit.Tests/bin/Debug/CommentSummary.txt");

        }

        [TestMethod]
        public void checkScrape()
        {
            var go = new RepositoryScraper(Directory.GetFiles(@"C:\Users\dav\Documents\GitHub\MethodImplementation\oktogit.Tests\TestData", "*.cs",
                                         SearchOption.AllDirectories));
            go.AnalyzeCSFiles();

            Process.Start("C:/Users/dav/Documents/GitHub/MethodImplementation/oktogit.Tests/bin/Debug/CommentSummary.txt");
        }
    }
}