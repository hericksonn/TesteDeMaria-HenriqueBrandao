# Arquitetura

## Camadas

```
UI  ->  Application  ->  Infrastructure  ->  PostgreSQL
                                          \-> FileLogger
```

- **UI**: WinForms, MDI, formularios de cadastro e listagem.
- **Application**: services com regras de negocio, transacao,
  auditoria e projecao para o relatorio.
- **Infrastructure**: repositorios com Npgsql, `ConnectionFactory`,
  `UnitOfWork`, `FileLogger`.
- **Domain**: entidades, enums e excecoes. Sem dependencia de
  banco.

## Salvar uma Ordem de Servico

1. UI monta a entidade `OrdemServico` com seus `ItemOrdemServico`.
2. Chama `OrdemServicoService.Salvar`.
3. A service:
   - recalcula totais e valida regras;
   - abre `UnitOfWork`;
   - se for update, le a OS para snapshot e checagem de status;
   - faz `Inserir` ou `Atualizar` (este ultimo dispara
     `ConcorrenciaException` se a versao mudou);
   - apaga e reinsere os itens;
   - registra historico de status quando o status mudou;
   - registra a auditoria com snapshot antes/depois;
   - faz commit.
4. Em caso de erro, rollback e a excecao sobe pro `TratadorErro`.

## Auditoria

Cada operacao relevante grava em `auditoria`:

- entidade (`Cliente`, `Servico`, `OrdemServico`);
- id do registro;
- operacao (`INSERT`, `UPDATE`, `DELETE`);
- usuario logado;
- snapshot antes (jsonb);
- snapshot depois (jsonb).

A serializacao usa `JavaScriptSerializer` para evitar dependencia
de `Newtonsoft.Json`.

## Concorrencia

`ordens_servico.versao` comeca em 1. O UPDATE inclui a versao no
`WHERE` e usa `RETURNING versao`. Se o retorno vier nulo
(zero linhas afetadas), o repositorio lanca
`ConcorrenciaException`, que herda de `DominioException` e e
tratada na UI.

## Notas de performance

- Listagem de OS nao traz itens. Eles sao carregados ao abrir uma
  OS especifica.
- Paginacao com `LIMIT/OFFSET` em todas as listagens.
- Indices em `documento`, `data_abertura`, `status`, `cliente_id`.
- Pooling habilitado na connection string.
