using Npgsql;
using System.Text;

namespace DeMariaTeste.Tools.Provision;

internal static class Program
{
    private static int Main(string[] args)
    {
        try
        {
            var host = RequiredEnv("SUPABASE_DB_HOST");
            var port = RequiredEnv("SUPABASE_DB_PORT");
            var db   = RequiredEnv("SUPABASE_DB_NAME");
            var user = RequiredEnv("SUPABASE_DB_USER");
            var pwd  = RequiredEnv("SUPABASE_DB_PASSWORD");

            var connectionString =
                $"Host={host};Port={port};Database={db};Username={user};Password={pwd};" +
                "SSL Mode=Require;Trust Server Certificate=true;Pooling=false;CommandTimeout=120";

            Log("Conectando em " + host + ":" + port + " (db=" + db + ", user=" + user + ")");

            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            Log("Conexao OK. Versao do servidor: " + conn.PostgreSqlVersion);

            var rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));
            var schemaPath = Path.Combine(rootPath, "database", "schema.sql");
            var seedPath   = Path.Combine(rootPath, "database", "seed.sql");

            if (args.Length >= 1 && File.Exists(args[0])) schemaPath = args[0];
            if (args.Length >= 2 && File.Exists(args[1])) seedPath   = args[1];

            if (!File.Exists(schemaPath))
                throw new FileNotFoundException("schema.sql nao encontrado em " + schemaPath);

            ExecutarArquivo(conn, schemaPath, "schema.sql");

            bool rodarSeed = args.Any(a => string.Equals(a, "--seed", StringComparison.OrdinalIgnoreCase));
            if (rodarSeed)
            {
                if (File.Exists(seedPath))
                    ExecutarArquivo(conn, seedPath, "seed.sql");
                else
                    Log("seed.sql nao encontrado em " + seedPath + ", ignorando.");
            }

            Log("Tabelas do schema public:");
            ImprimirQuery(conn,
                "SELECT table_name FROM information_schema.tables " +
                "WHERE table_schema = 'public' ORDER BY table_name");

            bool validar = args.Any(a => string.Equals(a, "--validate", StringComparison.OrdinalIgnoreCase));
            if (validar)
            {
                Log("Clientes:");
                ImprimirQuery(conn, "SELECT id, nome, documento, tipo, ativo FROM clientes ORDER BY id");

                Log("Servicos:");
                ImprimirQuery(conn, "SELECT id, nome, valor_base, percentual_imposto, ativo FROM servicos ORDER BY id");

                Log("Indices em ordens_servico:");
                ImprimirQuery(conn, "SELECT indexname FROM pg_indexes WHERE tablename = 'ordens_servico' ORDER BY indexname");

                Log("Constraints em ordens_servico:");
                ImprimirQuery(conn,
                    "SELECT conname, contype FROM pg_constraint " +
                    "WHERE conrelid = 'ordens_servico'::regclass ORDER BY conname");
            }

            Log("Concluido.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine();
            Console.Error.WriteLine("[ERRO] " + ex.GetType().Name + ": " + ex.Message);
            if (ex.InnerException != null)
                Console.Error.WriteLine("       inner -> " + ex.InnerException.Message);
            return 1;
        }
    }

    private static void ImprimirQuery(NpgsqlConnection conn, string sql)
    {
        using var cmd = new NpgsqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        var headers = new List<string>();
        for (int i = 0; i < reader.FieldCount; i++) headers.Add(reader.GetName(i));
        Console.WriteLine("    " + string.Join(" | ", headers));
        Console.WriteLine("    " + new string('-', headers.Sum(h => h.Length) + (headers.Count - 1) * 3));
        int n = 0;
        while (reader.Read())
        {
            var valores = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                valores.Add(reader.IsDBNull(i) ? "" : reader.GetValue(i)?.ToString() ?? "");
            }
            Console.WriteLine("    " + string.Join(" | ", valores));
            n++;
        }
        Console.WriteLine("    (" + n + " linha" + (n == 1 ? "" : "s") + ")");
        Console.WriteLine();
    }

    private static void ExecutarArquivo(NpgsqlConnection conn, string path, string nome)
    {
        Log("Executando " + nome + " (" + path + ")");
        var sql = File.ReadAllText(path, Encoding.UTF8);

        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.CommandTimeout = 120;
        cmd.ExecuteNonQuery();

        Log(nome + " executado com sucesso.");
    }

    private static string RequiredEnv(string nome)
    {
        var v = Environment.GetEnvironmentVariable(nome);
        if (string.IsNullOrWhiteSpace(v))
            throw new InvalidOperationException(
                "Variavel de ambiente '" + nome + "' nao definida. " +
                "Carregue o arquivo tools/supabase.env.ps1 antes de rodar.");
        return v!;
    }

    private static void Log(string msg)
    {
        Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + msg);
    }
}
