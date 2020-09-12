using System;
using System.Threading.Tasks;
using CommandLine;
using StoreLib.Services;
using StoreLib.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace StoreLib.Cli
{

    enum Commands
    {
        packages,
        query,
        search,
        convert
    }

    class Options
    {
        [Option('a', "authtoken", Required = false, HelpText = "Auth-token header value (e.g. \"XBL3.0=123;xyz\")")]
        public string AuthToken { get; set; }

        [Option('m', "market", Required = false, Default = Market.US, HelpText = "Market (e.g. US)")]
        public Market Market { get; set; }

        [Option('l', "lang", Required = false, Default=Lang.en, HelpText = "Language (e.g. EN)")]
        public Lang Language { get; set; }

        [Option('e', "env", Required = false, Default=DCatEndpoint.Production, HelpText = "Environment (e.g. Production)")]
        public DCatEndpoint Environment { get; set; }

        [Option('t', "idtype", Required = false, Default=IdentiferType.ProductID, HelpText = "IdentifierType")]
        public IdentiferType IdType { get; set; }

        [Option('f', "devicefamily", Required = false, Default=DeviceFamily.Desktop, HelpText = "Device Family (used for search)")]
        public DeviceFamily DeviceFamily { get; set; }

        [Value(0, MetaName = "command", Required = true,
         HelpText = "Command to execute\nAvailable commands: packages, query, search, convert")]
        public Commands Command { get; set; }

        [Value(1, MetaName = "id", Required = true, HelpText = "Id or Search-query")]
        public string IdOrSearchQuery { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {

            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run)
                .WithNotParsed(HandleErrors);
        }

        private static void Run(Options opts)
        {
            DisplayCatalogHandler dcatHandler = new DisplayCatalogHandler(
                opts.Environment,
                new Locale(opts.Market, opts.Language, true));

            if (!String.IsNullOrEmpty(opts.AuthToken) &&
                !opts.AuthToken.StartsWith("Token") &&
                !opts.AuthToken.StartsWith("Bearer") &&
                !opts.AuthToken.StartsWith("XBL3.0="))
            {
                Console.WriteLine("Invalid token format, ignoring...");
            }
            else if (!String.IsNullOrEmpty(opts.AuthToken))
            {
                Console.WriteLine("Setting token...");
                CommandHandler.Token = opts.AuthToken;
            }

            switch (opts.Command)
            {
                case Commands.packages:
                    Console.WriteLine("* PACKAGES");
                    CommandHandler
                        .PackagesAsync(dcatHandler, opts.IdOrSearchQuery, opts.IdType)
                        .GetAwaiter()
                        .GetResult();
                    break;
                case Commands.query:
                    Console.WriteLine("* QUERY");
                    CommandHandler
                        .AdvancedQueryAsync(dcatHandler, opts.IdOrSearchQuery, opts.IdType)
                        .GetAwaiter()
                        .GetResult();
                    break;
                case Commands.search:
                    Console.WriteLine("* SEARCH");
                    CommandHandler
                        .SearchAsync(dcatHandler, opts.IdOrSearchQuery, opts.DeviceFamily)
                        .GetAwaiter()
                        .GetResult();
                    break;
                case Commands.convert:
                    Console.WriteLine("* CONVERT");
                    CommandHandler
                        .ConvertId(dcatHandler, opts.IdOrSearchQuery, opts.IdType)
                        .GetAwaiter()
                        .GetResult();
                    break;
            }
        }

        static void HandleErrors(IEnumerable<Error> errs)
        {
            Console.WriteLine("Failed to parse cmdline arguments");
        }
    }
}
