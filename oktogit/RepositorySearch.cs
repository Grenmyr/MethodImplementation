using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace oktogit
{
    public class RepositorySearch
    {

        // Connection with authentication for github api.
        private Connection _connection;
        private string _searchWord;
        private GitHubClient _github;


        // List with C# repositories
        public List<Repository> CSRepositories = new List<Repository>();
        // List with JS repositories.
        public List<Repository> JSRepositories = new List<Repository>();

        // List of C# repositories that also has user that written repo in JS.
        public List<Repository> CSmatch = new List<Repository>();
        // List of JS repositories that also has user that written repo in C#.
        public List<Repository> JSmatch = new List<Repository>();

        
        /// <summary>
        /// Constructor to initialise and apply settings.
        /// </summary>
        /// <param name="searchWord"></param>
        /// <param name="authentication"></param>
        public RepositorySearch(string searchWord, string[] authentication)
        {
            _searchWord = searchWord;
            Authenticate(authentication);
            _github = new Octokit.GitHubClient(_connection);
        }

        /// <summary>
        /// Create connection with basic authentication to Github. 
        /// </summary>
        /// <param name="authentication"></param>
        public void Authenticate(string[] authentication)
        {
            _connection = new Octokit.Connection(new Octokit.ProductHeaderValue("GitSearch"))
            {
                Credentials = new Octokit.Credentials(authentication[0], authentication[1])
            };
        }


        /// <summary>
        /// Method, set quearylanguage and make request for Repositories in CSharp.
        /// </summary>
        /// <returns></returns>
        public async Task GetTopRatedReposLanguageCSharp()
        {
            var query = GetNewQuery(Language.CSharp);
            CSRepositories = await GetTenPagesOfRepositories(query);
        }

        /// <summary>
        /// Method, set quearylanguage and make request for Repositories in Javascript.
        /// </summary>
        /// <returns></returns>
        public async Task GetTopRatedReposLanguageJavascript()
        {
            var query = GetNewQuery(Language.JavaScript);
            JSRepositories =await GetTenPagesOfRepositories(query);
        }

        /// <summary>
        /// Return 1000 first results. Github has a limit to only allow first 1000 items from eatch repository search.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>List with repositories in either CSharp or Javascript</returns>
        public async Task<List<Repository>> GetTenPagesOfRepositories(SearchRepositoriesRequest query)
        {
            List<Repository> list = new List<Repository>();
            for (int i = 1; i < 11; i++)
            {
                query.Page = i;
                var request = await _github.Search.SearchRepo(query);
                list.AddRange(request.Items.ToList());
            }
            return list;
        }

        /// <summary>
        /// Used to get different quearys depending on language, also set quesry sorted on date.
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public SearchRepositoriesRequest GetNewQuery (Language language)
        {
            var query = new SearchRepositoriesRequest(_searchWord);
            query.Language = language;
            query.SortField = RepoSearchSort.Updated;
            return query;
        }

        /// <summary>
        /// Iterate through the 2 lists, and make 2 new lists, these lists contain
        /// only repositories where same user has written in both lanugages.
        /// It also write to a text file, so user of wrapper manually can check repositories qualify for research criterias.
        /// </summary>
        public void GetMatch ()
        {

            CSmatch = CSRepositories.SelectMany(e => JSRepositories.Where(o => o.Owner.Id == e.Owner.Id)).ToList();
            JSmatch = JSRepositories.SelectMany(e => CSRepositories.Where(o => o.Owner.Id == e.Owner.Id)).ToList();

            TextWriter tw = new StreamWriter("SavedLists.txt");
            tw.WriteLine("JAVASCRIPT REPOS BELOW");

            foreach (var element in CSmatch)
            {
                  tw.WriteLine(element.CloneUrl);
            }
          
            tw.WriteLine("C# REPOS BELOW");

            foreach (var element in JSmatch)
            {
                tw.WriteLine(element.CloneUrl);
            }
            tw.Close();
        }  
    }
}