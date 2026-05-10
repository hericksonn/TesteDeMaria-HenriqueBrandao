# DeMariaTeste

Sistema desktop de Gestao de Ordens de Servico em Windows Forms,
.NET Framework 4.6 e PostgreSQL (via Npgsql), com relatorio em
ReportViewer.

Entrega do teste pratico para vaga de Desenvolvedor C# Pleno.

## Estrutura do projeto

| Projeto | O que faz |
|---------|-----------|
| `DeMariaTeste.Domain` | Entidades, enums e excecoes. Sem dependencia de banco. |
| `DeMariaTeste.Infrastructure` | Conexao com PostgreSQL, repositorios, Unit of Work e logger em arquivo. |
| `DeMariaTeste.Application` | Services, regras de negocio, transacoes, auditoria e projecao para o relatorio. |
| `DeMariaTeste.UI` | WinForms (MDI), telas de cadastro, listagem, login e o RDLC. |

A UI nao acessa repositorios direto. Os formularios usam um
`ServiceLocator` simples para obter as services e tudo que toca o
banco passa pela camada de aplicacao.

## Notas tecnicas

- Acesso a dados manual com `NpgsqlConnection` e `NpgsqlCommand`,
  parametros nomeados e `using` em todos os blocos. Sem ORM (foi
  requisito do teste).
- `UnitOfWork` abre conexao + transacao no construtor; as services
  chamam `Commit` ou `Rollback` explicitamente. O `Dispose` faz
  rollback se a transacao nao foi finalizada.
- Concorrencia otimista no UPDATE da OS: o `WHERE` inclui
  `versao = @versao`, o `SET` faz `versao = versao + 1` e o comando
  usa `RETURNING versao`. Quando o registro foi alterado por outro
  usuario o retorno vem nulo e o repositorio lanca
  `ConcorrenciaException`.
- Cada `ItemOrdemServico` guarda `valor_unitario` e
  `percentual_imposto_aplicado` no momento da inclusao. Isso atende
  a regra de "alterar `ValorBase` do servico nao afeta OS ja
  existentes".
- Auditoria das principais operacoes (insert/update/delete de
  cliente, servico e OS) com snapshot antes/depois em coluna `jsonb`.
- Listagem da OS nao traz os itens; itens sao buscados ao abrir uma
  OS especifica. Listagens usam `LIMIT/OFFSET` e ha indices em
  `documento`, `data_abertura`, `status` e `cliente_id`.
- Tratamento de erro centralizado em `TratadorErro`: distingue
  excecao de dominio, `PostgresException` (mapeia `23505` e `23503`)
  e erro generico.
- Log em arquivo: `FileLogger` grava em `logs/app-yyyyMMdd.log` na
  pasta do executavel.

## Banco de dados

O guia de criacao do banco esta em [`docs/banco-de-dados.md`](docs/banco-de-dados.md).
Cobre Supabase, Postgres local, a ferramenta `tools/provision`,
configuracao do `App.config` e troubleshooting.
As evidencias funcionais consolidadas estao em
[Visualizar evidencias (HTML)](https://htmlpreview.github.io/?https://raw.githubusercontent.com/hericksonn/TesteDeMaria-HenriqueBrandao/main/docs/Evidencias-DeMariaTeste.htm).

Resumo:

1. Cria-se um Postgres (Supabase ou local).
2. Aplica-se `database/schema.sql` (cria as tabelas, indices,
   constraints e a coluna `versao`).
3. Opcionalmente roda-se `database/seed.sql` (3 clientes e 3
   servicos de exemplo).

## Como rodar

1. Abrir `DeMariaTeste.sln` no Visual Studio (2017+ ou MSBuild com
   .NET Framework 4.6) e restaurar pacotes NuGet.
2. Provisionar o banco seguindo `docs/banco-de-dados.md`.
3. Editar `src/DeMariaTeste.UI/App.config` e ajustar a connection
   string (usar `Password=SUA_SENHA_AQUI` como placeholder no repo).
4. Para evitar comitar a senha, rodar uma vez:

   ```powershell
   git update-index --skip-worktree src/DeMariaTeste.UI/App.config
   ```

5. Definir `DeMariaTeste.UI` como projeto de inicializacao e
   executar (F5).

## Checklist rapido pre-entrega

- `src/DeMariaTeste.UI/App.config` sem senha real
  (`Password=SUA_SENHA_AQUI`).
- `src/DeMariaTeste.Infrastructure/App.config` sem senha real
  (`Password=SUA_SENHA_AQUI`).
- `.gitignore` cobrindo `.vs/`, `bin/`, `obj/`, `logs/`.
- `database/schema.sql` criando tabelas, constraints e indices.
- build em `Release` sem erro.

A tela de login nao tem autenticacao real. O nome digitado e usado
apenas para identificar o usuario na auditoria.

## Estrutura de pastas

```
DeMariaTeste/
+-- DeMariaTeste.sln
+-- README.md
+-- database/
|   +-- schema.sql
|   +-- seed.sql
+-- docs/
|   +-- banco-de-dados.md
|   +-- arquitetura.md
+-- src/
|   +-- DeMariaTeste.Domain/
|   +-- DeMariaTeste.Infrastructure/
|   +-- DeMariaTeste.Application/
|   +-- DeMariaTeste.UI/
+-- tools/
    +-- provision/        (utilitario opcional p/ aplicar o schema)
```

## Funcionalidades

- CRUD de Clientes com filtros (nome, documento, ativo) e paginacao.
- CRUD de Servicos.
- Cadastro de OS com itens, recalculo de totais, troca de status e
  bloqueio para OS Concluida ou Cancelada.
- Cancelamento de OS com motivo (vai pro historico de status).
- Relatorio com filtros (periodo, cliente, status), agrupamento por
  cliente, totais por cliente, total geral, total de impostos e
  exportacao para PDF.
- Auditoria das operacoes principais com snapshot JSON.
- Historico de mudanca de status.
- Log de erros em arquivo.

## Pendencias

- Autenticacao real contra base de usuarios.
- Testes automatizados (os repositorios estao por interface e podem
  ser substituidos por mocks).
- Mascaras de input para CPF/CNPJ e telefone.
