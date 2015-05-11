using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace oktogit.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var go = new RepositorySearch("key",new string [] {"insertGithubYourUserName","insertyourGithubPassword"});
            await go.GetTopRatedReposLanguageCSharp();
            await go.GetTopRatedReposLanguageJavascript();

            go.GetMatch();
            //
        }

        [TestMethod]
        public void TestMethod2()
        {
            var go = new RepositoryScraper("Insert local path of repo here", "*.cs");
            go.AnalyzeCSFiles();

        }
    }
}
