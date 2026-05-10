# tools

Utilitarios de apoio. Nao fazem parte da aplicacao em runtime.

## provision/

Console em .NET 8 que aplica `database/schema.sql` (e opcionalmente
`database/seed.sql`) num PostgreSQL alvo, lendo as credenciais de
variaveis de ambiente. Util quando nao se tem `psql` na maquina.

### Uso

```powershell
# 1. Copiar template e editar com a senha real
Copy-Item .\tools\supabase.env.example.ps1 .\tools\supabase.env.ps1

# 2. Carregar variaveis (dot source)
. .\tools\supabase.env.ps1

# 3. Rodar (pode usar --seed e --validate)
dotnet run --project .\tools\provision -c Release -- --seed --validate

# 4. Apagar o arquivo de credencial local
Remove-Item .\tools\supabase.env.ps1
```

### Variaveis

| Nome | Exemplo |
|------|---------|
| SUPABASE_DB_HOST | `aws-1-us-west-1.pooler.supabase.com` |
| SUPABASE_DB_PORT | `5432` |
| SUPABASE_DB_NAME | `postgres` |
| SUPABASE_DB_USER | `postgres.<ref>` |
| SUPABASE_DB_PASSWORD | (senha real) |

Os nomes tem `SUPABASE_` por motivo historico. Funciona tambem
com Postgres local; basta apontar host/user/senha apropriados.
