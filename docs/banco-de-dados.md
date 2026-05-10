# Banco de dados

Como criar o PostgreSQL usado pela aplicacao. Funciona em qualquer
Postgres 12+; uso o Supabase para nao precisar instalar Postgres
local, mas tambem da pra rodar local.

## Trilha A - Supabase

### 1. Criar o projeto

1. Entrar em https://supabase.com e criar/abrir o painel.
2. `+ New project`. Anotar a senha do banco (so aparece uma vez).
   Regiao mais proxima.
3. Esperar o status ficar `Healthy`.

### 2. Pegar os dados de conexao

No painel do projeto, botao `Connect` no topo. Vai aparecer algo
parecido com isso:

```
DATABASE_URL="postgresql://postgres.<ref>:[YOUR-PASSWORD]@aws-X-<regiao>.pooler.supabase.com:6543/postgres?pgbouncer=true"
DIRECT_URL="postgresql://postgres.<ref>:[YOUR-PASSWORD]@aws-X-<regiao>.pooler.supabase.com:5432/postgres"
```

Use a `DIRECT_URL` (porta 5432). E o pooler em modo Session, que
preserva transacao.

Nao use a `DATABASE_URL` (porta 6543). E Transaction pooling, libera
a conexao a cada comando e quebra o `UnitOfWork`.

Os campos que precisamos sao:

| Campo | Onde achar |
|-------|------------|
| Host | algo como `aws-1-us-west-1.pooler.supabase.com` |
| Port | `5432` |
| Database | `postgres` |
| Username | `postgres.<ref>` |
| Password | a senha do passo 1 |

### 3. Criar as tabelas

Tres opcoes, escolha uma.

**SQL Editor do Supabase**: cola o conteudo de `database/schema.sql`,
roda. Opcionalmente faz o mesmo com `database/seed.sql`.

**Ferramenta `tools/provision`** (precisa do .NET 8 SDK):

```powershell
Copy-Item .\tools\supabase.env.example.ps1 .\tools\supabase.env.ps1
# editar tools\supabase.env.ps1 colocando a senha real

. .\tools\supabase.env.ps1
dotnet run --project .\tools\provision -c Release -- --seed --validate

Remove-Item .\tools\supabase.env.ps1
```

**psql** (se ja tiver instalado):

```powershell
$cs = "postgresql://postgres.<ref>:<SENHA>@aws-1-us-west-1.pooler.supabase.com:5432/postgres?sslmode=require"
psql "$cs" -f .\database\schema.sql
psql "$cs" -f .\database\seed.sql
```

### 4. Restringir IP (opcional mas recomendado)

`Project Settings > Database > Network Restrictions`, adiciona o
seu IP. Reduz risco caso a senha vaze.

## Trilha B - PostgreSQL local

Instala via instalador oficial, Chocolatey, WSL ou Docker. Exemplo
com Docker:

```bash
docker run --name demaria-pg \
    -e POSTGRES_USER=postgres \
    -e POSTGRES_PASSWORD=postgres \
    -p 5432:5432 \
    -d postgres:16
```

Cria o database e aplica o schema:

```sql
CREATE DATABASE demaria_os;
```

```powershell
psql -U postgres -h localhost -d demaria_os -f .\database\schema.sql
psql -U postgres -h localhost -d demaria_os -f .\database\seed.sql
```

Ou pela ferramenta de provisionamento, apontando as variaveis para
o local:

```powershell
$env:SUPABASE_DB_HOST     = "localhost"
$env:SUPABASE_DB_PORT     = "5432"
$env:SUPABASE_DB_NAME     = "demaria_os"
$env:SUPABASE_DB_USER     = "postgres"
$env:SUPABASE_DB_PASSWORD = "postgres"
dotnet run --project .\tools\provision -c Release -- --seed --validate
```

(O nome das variaveis tem `SUPABASE_` por motivo historico, mas
funciona pra qualquer Postgres.)

## Configurar a connection string

Em `src/DeMariaTeste.UI/App.config`:

```xml
<add name="DeMariaDb"
     connectionString="Host=...;Port=...;Database=...;Username=...;Password=SUA_SENHA_AQUI;SSL Mode=Require;Trust Server Certificate=true;Pooling=true;Minimum Pool Size=1;Maximum Pool Size=20;CommandTimeout=60"
     providerName="Npgsql" />
```

Para Postgres local, sem SSL:

```xml
<add name="DeMariaDb"
     connectionString="Host=localhost;Port=5432;Database=demaria_os;Username=postgres;Password=postgres;Pooling=true;Maximum Pool Size=20;CommandTimeout=60"
     providerName="Npgsql" />
```

Para evitar comitar a senha sem querer:

```powershell
git update-index --skip-worktree src/DeMariaTeste.UI/App.config
```

A partir dai o git ignora alteracoes locais nesse arquivo. Para
reverter um dia: `git update-index --no-skip-worktree ...`.

## Validar

Apos rodar `schema.sql` (e opcionalmente `seed.sql`), o esperado:

- Tabelas: `clientes`, `servicos`, `ordens_servico`,
  `itens_ordem_servico`, `historico_status_os`, `auditoria`.
- Pos-seed: 3 clientes, 3 servicos, 0 OS.
- `ordens_servico` com indices em `cliente_id`, `data_abertura`,
  `status` e coluna `versao`.

Conferir pela ferramenta:

```powershell
dotnet run --project .\tools\provision -c Release -- --validate
```

Ou no `psql`:

```sql
\dt
SELECT count(*) FROM clientes;
SELECT count(*) FROM servicos;
\d ordens_servico
\di ordens_servico*
```

## Troubleshooting

| Sintoma | Causa | Solucao |
|---------|-------|---------|
| Timeout em `db.<ref>.supabase.co` | Rede sem IPv6 | Usar o pooler em `aws-X-<regiao>.pooler.supabase.com:5432` com user `postgres.<ref>` |
| `28P01 password authentication failed` | Senha incorreta | Conferir, ou resetar em `Project Settings > Database` |
| `SSL connection is required` | Connection string sem SSL | Adicionar `SSL Mode=Require;Trust Server Certificate=true` |
| `prepared statement already exists` ao salvar OS | Conexao na porta 6543 (Transaction pooler) | Trocar para 5432 (Session pooler ou direta) |
| `ConcorrenciaException` ao salvar OS | Outro usuario alterou a OS no meio do caminho | Recarregar e reaplicar a edicao |
| Build da UI nao acha Npgsql ou ReportViewer | Pacotes NuGet nao restaurados | Apagar `packages/`, restaurar e rebuildar |
