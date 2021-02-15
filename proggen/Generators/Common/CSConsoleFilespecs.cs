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
            new FileSpec
            {
                Pathname = "$$(PROJECTNAMECAMEL)/DbAccess.cs",
                Contents = new [] {
                    "\uFEFF",
@"using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace $$(PROJECTNAMECAMEL)
    class DbAccess
    {
        public static void RunQuery(string server, string dbName, string sql, Action<IDataReader> readerAction = null, Action<IDbCommand> preExecute = null)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = dbName,
                IntegratedSecurity = true
            };

            var list = new List<List<object>>();
            using (var connection = new SqlConnection(builder.ToString()))
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

        public static void RunNonQuery(string server, string dbName, string sql, Action<IDbCommand> preExecute = null)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = dbName,
                IntegratedSecurity = true
            };

            using (var connection = new SqlConnection(builder.ToString()))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                preExecute?.Invoke(command);
                command.ExecuteNonQuery();
            }
        }

        public static object RunScalar(string server, string dbName, string sql, Action<IDbCommand> preExecute = null)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = dbName,
                IntegratedSecurity = true
            };

            using (var connection = new SqlConnection(builder.ToString()))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                preExecute?.Invoke(command);
                return command.ExecuteScalar();
            }
        }

        public static void CreateDb(string server, string dbName, string masterdbName = ""master"")
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = masterdbName,
                IntegratedSecurity = true
            };

            var user = Environment.UserName;
            var domain = Environment.UserDomainName;

            var sql = $@""
            IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{dbName}')  BEGIN CREATE DATABASE [{dbName}] END
            IF NOT EXISTS(SELECT * FROM sys.database_principals WHERE Name = '{user}') CREATE USER {user} FOR LOGIN [{domain}\{user}]
            "";

            using (var connection = new SqlConnection(builder.ToString()))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void CreateOrAlterProcedure(string server, string dbName, string procName, string procCreateSql)
        {
            var spDel = $@""IF EXISTS (SELECT 1 FROM sys.procedures WHERE NAME = '{procName}' AND type = 'P')  DROP PROCEDURE {procName}"";
            DbAccess.RunNonQuery(server, dbName, spDel);
            DbAccess.RunNonQuery(server, dbName, procCreateSql);
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