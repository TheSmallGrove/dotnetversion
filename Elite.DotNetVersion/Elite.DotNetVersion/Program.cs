﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Elite.DotNetVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments(args, VerbManager.GetVerbs().ToArray())
                    .WithParsed<IProgramOptions>(o =>
                    {
                        if (o.Output != OutputType.Json)
                            Console.WriteLine($"dotnetversion v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");

                        o.Create().RunAsync().Wait();
                    })
                    .WithNotParsed(errs => DisplayErrors(errs));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void DisplayErrors(IEnumerable<Error> errors)
        {
            foreach(var error in errors)
                Console.WriteLine(error);
        }
    }
}
