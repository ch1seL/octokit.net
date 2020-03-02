﻿using System.IO;
using System.Threading.Tasks;

namespace Octokit.CodeGen
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var filter = new PathFilter();
            filter.Allow("/marketplace_listing/accounts/");

            var apiBuilder = new ApiBuilder();
            apiBuilder.Register(ApiBuilder.AddTypeNamesAndFileName);
            apiBuilder.Register(ApiBuilder.AddMethodForEachVerb);

            var dir = Directory.GetCurrentDirectory();
            var file = File.OpenRead(Path.Combine(dir, "schema", "api.github.com.json"));

            var results = await PathProcessor.Process(file);

            var filteredPaths = filter.Filter(results);

            var apiMetadata = apiBuilder.Build(filteredPaths);
        }
    }
}
