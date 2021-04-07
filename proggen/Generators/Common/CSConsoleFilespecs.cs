using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proggen.Generators.Common
{
    static class CSConsoleFileSpecs
    {
        static CSConsoleFileSpecs()
        {
            var binFolder= Environment.GetEnvironmentVariable("PROGGENBINFOLDER");
            if (!string.IsNullOrEmpty(binFolder) && Directory.Exists(binFolder))
            {
                postBuildStep = $@"  <PropertyGroup>
    <PostBuildEvent>copy $(TargetPath) {binFolder}</PostBuildEvent>
   </PropertyGroup>";
            }
        }
        private static string postBuildStep = "";
        private static string sixteen = new string(' ', 16);
        private static string errorStuff =
            sixteen + "var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;\r\n" +
            sixteen + "var progname = Path.GetFileNameWithoutExtension(fullname);\r\n" +
            sixteen + "Console.Error.WriteLine($\"{progname} Error: {ex.Message}\");";

        public static FileSpec[] CSConsoleSpecs => new []
        {
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/Program.cs",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "using System;",
                    "using System.Collections.Generic;",
                    "using System.IO;",
                    "using System.Linq;",
                    "using System.Text;",
                    "using System.Text.RegularExpressions;",
                    "using System.Xml.Linq;",
                    "using System.Threading.Tasks;\r\n",
                    "namespace $$(PROJECTNAMECAMEL)",
                    "{",
                    "    class Program",
                    "    {",
                    "        private static void Main(string[] args)",
                    "        {",
                    $"            try\r\n            {{\r\n                //startstarttypingtypingherehere\r\n            }}\r\n            catch (Exception ex)\r\n            {{\r\n{errorStuff}\r\n            }}\r\n",
                    "        }",
                    "    }",
                    "}"
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
                Contents = new[] { @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net472</TargetFramework>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
  </PropertyGroup>
</Project>" }
            },
            new FileSpec (CommonFileSpecs.SlnFileSpec),
            new FileSpec (CommonFileSpecs.GitIgnore)
        };

        public static FileSpec[] CSConsoleSpecsNet5 => new[]
        {
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/Program.cs",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "using System;",
                    "using System.Collections.Generic;",
                    "using System.IO;",
                    "using System.Linq;",
                    "using System.Text;",
                    "using System.Text.RegularExpressions;",
                    "using System.Xml.Linq;",
                    "using System.Threading.Tasks;\r\n",
                    "namespace $$(PROJECTNAMECAMEL)",
                    "{",
                    "    class Program",
                    "    {",
                    "        private static void Main(string[] args)",
                    "        {",
                    $"            try\r\n            {{\r\n                //startstarttypingtypingherehere\r\n            }}\r\n            catch (Exception ex)\r\n            {{\r\n{errorStuff}\r\n            }}\r\n",
                    "        }",
                    "    }",
                    "}"
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
                Contents = new[] { @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
  </PropertyGroup>
</Project>" }
            },
            new FileSpec (CommonFileSpecs.SlnFileSpec),
            new FileSpec (CommonFileSpecs.GitIgnore)
        };


        public static FileSpec[] DbConsoleSpecsNet19 => new[]
{
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/Program.cs",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    @"using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Wonk1
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var server = @""(localdb)\db1"";
                var dbName = ""db3"";
                var masterConnstring = DbAccess.GetConnectionString(server, ""master"");
                var connstring = DbAccess.GetConnectionString(server, dbName);
                DbAccess.CreateDb(masterConnstring, dbName);
                DbAccess.CreateOrAlterProcedure(connstring, ""Proc1"", ""CREATE PROCEDURE PROC1 AS RETURN"");
                //startstarttypingtypingherehere
            }
            catch (Exception ex)
            {
                var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                var progname = Path.GetFileNameWithoutExtension(fullname);
                Console.Error.WriteLine($""{progname} Error: {ex.Message}"");
            }

        }
    }
}
"
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
                Contents = new[] { @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net472</TargetFramework>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
  </PropertyGroup>
</Project>" }
            },
            new FileSpec
            {
                Pathname = "$$(PROJECTNAMECAMEL)/DbAccess.cs",
                Contents = new [] {
                    "\uFEFF",
@"using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Wonk1
{
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
                    while (reader.Read())
                    {
                        readerAction?.Invoke(reader);
                    }
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

            var sql = $@""
            IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{dbName}')  BEGIN CREATE DATABASE [{dbName}] END
            IF NOT EXISTS(SELECT * FROM sys.database_principals WHERE Name = '{user}') CREATE USER {user} FOR LOGIN [{domain}\{user}]
            "";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public static void CreateOrAlterProcedure(string connectionString, string procName, string procCreateSql)
        {
            var spDel = $@""IF EXISTS (SELECT 1 FROM sys.procedures WHERE NAME = '{procName}' AND type = 'P')  DROP PROCEDURE {procName}"";
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
}
"

                }
            },
            new FileSpec (CommonFileSpecs.SlnFileSpec),
            new FileSpec (CommonFileSpecs.GitIgnore)
        };

    }
}