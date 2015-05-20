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
            var filename = "CSharpResult.txt";
            var go = new RepositoryScraper(Directory.GetFiles(@"C:\Users\dav\Documents\GitHub\Teoretiskt arbete\CSharp", "*.cs",
                                         SearchOption.AllDirectories),filename);
            go.AnalyzeCSFiles();

            Process.Start(String.Format("C:/Users/dav/Documents/GitHub/MethodImplementation/oktogit.Tests/bin/Debug/{0}", filename));

        }
        [TestMethod]
        public void ScrapeRepositoriesJavascript()
        {
            var filename = "JavascriptResult.txt";
            var go = new RepositoryScraper(Directory.GetFiles(@"C:\Users\dav\Documents\GitHub\Teoretiskt arbete\Javascript", "*.js",
                                         SearchOption.AllDirectories), filename);
            go.AnalyzeCSFiles();
            Process.Start(String.Format("C:/Users/dav/Documents/GitHub/MethodImplementation/oktogit.Tests/bin/Debug/{0}",filename));

        }

        [TestMethod]
        public void checkScrape()
        {
            var filename = "TestResult.txt";
            var go = new RepositoryScraper(Directory.GetFiles(@"C:\Users\dav\Documents\GitHub\MethodImplementation\oktogit.Tests\TestData", "*.cs",
                                         SearchOption.AllDirectories), filename);
            go.AnalyzeCSFiles();

            Process.Start(String.Format("C:/Users/dav/Documents/GitHub/MethodImplementation/oktogit.Tests/bin/Debug/{0}", filename));
        }
    }
}