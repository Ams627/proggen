using System;
using System.IO;
using System.Text;

namespace Proggen.Generators.Common;

internal static class CSConsoleFileSpecs
{
    static CSConsoleFileSpecs()
    {
        var binFolder = Environment.GetEnvironmentVariable("PROGGENBINFOLDER");
        if (!string.IsNullOrEmpty(binFolder) && Directory.Exists(binFolder))
        {
            postBuildStep = $@"  <PropertyGroup>
    <PostBuildEvent>copy $(TargetPath) {binFolder}</PostBuildEvent>
   </PropertyGroup>";
        }
    }
    private static readonly string postBuildStep = "";
    private static readonly string sixteen = new string(' ', 16);
    private static readonly string errorStuff =
        sixteen + "var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;\r\n" +
        sixteen + "var progname = Path.GetFileNameWithoutExtension(fullname);\r\n" +
        sixteen + "Console.Error.WriteLine($\"{progname} Error: {ex.Message}\");";

    private static readonly string ClassProgram =
        $$"""
            class Program
            {
                private static void Main(string[] args)
                {
                    try
                    {
                    }
                    catch (Exception ex)
                    {
                        var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                        var progname = Path.GetFileNameWithoutExtension(fullname);
                        Console.Error.WriteLine($"{progname} Error: {ex.Message}");
                    }
                }
            }
            """;

    public static FileSpec[] CSConsoleSpecs => new[]
    {
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/Program.cs",
            Contents = new [] {
                GetUsings(),
                "namespace $$(PROJECTNAMECAMEL);",
                "",
                ClassProgram,
            }
        },
        CommonFileSpecs.PolyFill,
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
            Contents = new[] {
                CommonFileUtils.GetCsProj("net48")
            }
        },
        CommonFileSpecs.SlnFileSpec,
        CommonFileSpecs.GitIgnore,
        CommonFileSpecs.PolyFill
    };

    public static FileSpec[] CSConsoleSpecsNet5 => new[]
    {
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/Program.cs",
            Contents = new [] {
                GetUsings(global:true),
                "namespace $$(PROJECTNAMECAMEL);",
                "",
                ClassProgram,
            }
        },
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
            Contents = new[] {
                CommonFileUtils.GetCsProj("net5", selfContained: false, pubSingle:true)
        }
        },
        new FileSpec (CommonFileSpecs.SlnFileSpec),
        new FileSpec (CommonFileSpecs.GitIgnore)
    };

    public static FileSpec[] CSConsoleSpecsNet6 => new[]
    {
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/Program.cs",
            Contents = new [] {
                GetUsings(global:true),
                "namespace $$(PROJECTNAMECAMEL);",
                "",
                ClassProgram,
            }
        },
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
            Contents = new[] { CommonFileUtils.GetCsProj("net6", pubSingle: true, selfContained: false) }
        },
        new FileSpec (CommonFileSpecs.SlnFileSpec),
        new FileSpec (CommonFileSpecs.GitIgnore)
    };

    public static FileSpec[] CSConsoleSpecsNet7 => new[]
    {
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/Program.cs",
            Contents = new [] {
                GetUsings(global:true),
                "namespace $$(PROJECTNAMECAMEL);",
                "",
                ClassProgram,
            }
        },
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
            Contents = new[] { CommonFileUtils.GetCsProj("net7", rtti:"win10-x64", pubSingle: true, implicitUsings:true) },
        },
        new FileSpec (CommonFileSpecs.SlnFileSpec),
        new FileSpec (CommonFileSpecs.GitIgnore)
    };


    public static FileSpec[] CSConsoleSpecsNet8 => new[]
    {
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/Program.cs",
            Contents = new [] {
                GetUsings(global : true),
                "namespace $$(PROJECTNAMECAMEL);",
                "",
                ClassProgram,
            }
        },
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
            Contents = new[] { CommonFileUtils.GetCsProj("net8.0", rtti:"win10-x64", pubSingle: true, implicitUsings:true, langVersion: "preview") },
        },
        new FileSpec (CommonFileSpecs.SlnFileSpec),
        new FileSpec (CommonFileSpecs.GitIgnore)
    };


    public static FileSpec[] DbConsoleSpecsNet19 => new[]
    {
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/Program.cs",
            Contents = new [] {
                GetUsings(global : false),
                """
                    namespace $$(PROJECTNAMECAMEL);
                    
                    class Program
                    {
                        private static void Main(string[] args)
                        {
                            try
                            {
                                var server = @"(localdb)\db1";
                                var dbName = "db3";
                                var masterConnstring = DbAccess.GetConnectionString(server, "master");
                                var connstring = DbAccess.GetConnectionString(server, dbName);
                                DbAccess.CreateDb(masterConnstring, dbName);
                                DbAccess.CreateOrAlterProcedure(connstring, "Proc1", "CREATE PROCEDURE PROC1 AS RETURN");
                                //startstarttypingtypingherehere
                            }
                            catch (Exception ex)
                            {
                                var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                                var progname = Path.GetFileNameWithoutExtension(fullname);
                                Console.Error.WriteLine($"{progname} Error: {ex.Message}");
                            }

                        }
                    }
                    """
            }
        },
        CommonFileSpecs.PolyFill,
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
            Contents = new[] { CommonFileUtils.GetCsProj(netVersion: "net48") }
        },
        new FileSpec
        {
            Pathname = "$$(PROJECTNAMECAMEL)/DbAccess.cs",
            Contents = new [] {
                "\uFEFF"+
                """
                using System;
                using System.Collections.Generic;
                using System.Data;
                using System.Data.SqlClient;

                namespace $$(PROJECTNAMECAMEL);

                class DbAccess
                {
                    public static void RunQuery(string connectionString, string sql, Action<IDataReader> readerAction = null, Action<IDbCommand> preExecute = null)
                    {
                        var list = new List<List<object>>();
                        using (var connection = new SqlConnection(connectionString))
                        using (var command = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            preExecute?.Invoke(command);
                            using (var reader = command.ExecuteReader())
                            {
                                readerAction?.Invoke(reader);
                            }
                        }
                    }

                    public static void RunNonQuery(string connectionString, string sql, Action<IDbCommand> preExecute = null)
                    {
                        using (var connection = new SqlConnection(connectionString))
                        using (var command = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            preExecute?.Invoke(command);
                            command.ExecuteNonQuery();
                        }
                    }

                    public static object RunScalar(string connectionString, string sql, Action<IDbCommand> preExecute = null)
                    {
                        using (var connection = new SqlConnection(connectionString))
                        using (var command = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            preExecute?.Invoke(command);
                            return command.ExecuteScalar();
                        }
                    }

                    public static void CreateDb(string connectionString, string dbName)
                    {
                        var user = Environment.UserName;
                        var domain = Environment.UserDomainName;

                        var sql = $@"
                        IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{dbName}')  BEGIN CREATE DATABASE [{dbName}] END
                        IF NOT EXISTS(SELECT * FROM sys.database_principals WHERE Name = '{user}') CREATE USER {user} FOR LOGIN [{domain}\{user}]
                        ";

                        using (var connection = new SqlConnection(connectionString))
                        using (var command = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }


                    public static void CreateOrAlterProcedure(string connectionString, string procName, string procCreateSql)
                    {
                        var spDel = $@"IF EXISTS (SELECT 1 FROM sys.procedures WHERE NAME = '{procName}' AND type = 'P')  DROP PROCEDURE {procName}";
                        DbAccess.RunNonQuery(connectionString, spDel);
                        DbAccess.RunNonQuery(connectionString, procCreateSql);
                    }

                    public static string GetConnectionString(string server, string dbname, bool integratedSecurity = true)
                    {
                        return new SqlConnectionStringBuilder
                        {
                            DataSource = server,
                            InitialCatalog = dbname,
                            IntegratedSecurity = integratedSecurity
                        }.ToString();
                    }
                }
                """,
            }
        },
        new FileSpec (CommonFileSpecs.SlnFileSpec),
        new FileSpec (CommonFileSpecs.GitIgnore)
    };


    private static string GetUsings(string[] usings = null, bool global = false)
    {
        usings ??= new[]
        {
            "System",
            "System.Collections.Generic",
            "System.IO",
            "System.Linq",
            "System.Text",
            "System.Text.RegularExpressions",
            "System.Xml.Linq",
            "System.Threading.Tasks",
        };

        var sb = new StringBuilder();
        sb.Append("\uFEFF");
        foreach (var us in usings)
        {
            if (global)
            {
                sb.Append("global ");
            }
            sb.Append("using ");
            sb.Append(us);
            sb.AppendLine(";");
        }

        return sb.ToString();
    }
}

