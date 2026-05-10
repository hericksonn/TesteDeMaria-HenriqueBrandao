-- DeMariaTeste - schema do banco (PostgreSQL 12+)

BEGIN;

-- clientes ----------------------------------------------------------
CREATE TABLE IF NOT EXISTS clientes (
    id              SERIAL          PRIMARY KEY,
    nome            VARCHAR(150)    NOT NULL,
    documento       VARCHAR(20)     NOT NULL,
    tipo            CHAR(1)         NOT NULL,
    email           VARCHAR(150)    NULL,
    telefone        VARCHAR(20)     NULL,
    data_cadastro   TIMESTAMP       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ativo           BOOLEAN         NOT NULL DEFAULT TRUE,
    CONSTRAINT uk_clientes_documento UNIQUE (documento),
    CONSTRAINT ck_clientes_tipo CHECK (tipo IN ('F', 'J'))
);

CREATE INDEX IF NOT EXISTS ix_clientes_documento ON clientes (documento);
CREATE INDEX IF NOT EXISTS ix_clientes_ativo     ON clientes (ativo) WHERE ativo = TRUE;

-- servicos ----------------------------------------------------------
CREATE TABLE IF NOT EXISTS servicos (
    id                  SERIAL          PRIMARY KEY,
    nome                VARCHAR(150)    NOT NULL,
    valor_base          NUMERIC(14,2)   NOT NULL,
    percentual_imposto  NUMERIC(5,2)    NOT NULL DEFAULT 0,
    ativo               BOOLEAN         NOT NULL DEFAULT TRUE,
    CONSTRAINT ck_servicos_valor_base CHECK (valor_base > 0),
    CONSTRAINT ck_servicos_imposto    CHECK (percentual_imposto >= 0 AND percentual_imposto <= 100)
);

CREATE INDEX IF NOT EXISTS ix_servicos_ativo ON servicos (ativo) WHERE ativo = TRUE;

-- ordens de servico -------------------------------------------------
-- "versao" e usada na concorrencia otimista. UPDATE faz +1 e checa
-- a versao anterior no WHERE.
CREATE TABLE IF NOT EXISTS ordens_servico (
    id              SERIAL          PRIMARY KEY,
    cliente_id      INTEGER         NOT NULL,
    data_abertura   TIMESTAMP       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    data_conclusao  TIMESTAMP       NULL,
    status          VARCHAR(20)     NOT NULL DEFAULT 'Aberta',
    observacao      TEXT            NULL,
    valor_total     NUMERIC(14,2)   NOT NULL DEFAULT 0,
    versao          INTEGER         NOT NULL DEFAULT 1,
    CONSTRAINT fk_os_cliente FOREIGN KEY (cliente_id) REFERENCES clientes (id) ON DELETE RESTRICT,
    CONSTRAINT ck_os_status CHECK (status IN ('Aberta','EmAndamento','Concluida','Cancelada')),
    CONSTRAINT ck_os_valor  CHECK (valor_total >= 0)
);

CREATE INDEX IF NOT EXISTS ix_os_cliente_id    ON ordens_servico (cliente_id);
CREATE INDEX IF NOT EXISTS ix_os_data_abertura ON ordens_servico (data_abertura);
CREATE INDEX IF NOT EXISTS ix_os_status        ON ordens_servico (status);

-- itens da OS -------------------------------------------------------
-- valor_unitario e percentual_imposto_aplicado sao "fotografados" no
-- momento da inclusao do item, para que alterar o servico depois nao
-- afete OS antigas.
CREATE TABLE IF NOT EXISTS itens_ordem_servico (
    id                              SERIAL          PRIMARY KEY,
    ordem_servico_id                INTEGER         NOT NULL,
    servico_id                      INTEGER         NOT NULL,
    quantidade                      NUMERIC(14,3)   NOT NULL,
    valor_unitario                  NUMERIC(14,2)   NOT NULL,
    percentual_imposto_aplicado     NUMERIC(5,2)    NOT NULL,
    valor_total_item                NUMERIC(14,2)   NOT NULL,
    CONSTRAINT fk_item_os      FOREIGN KEY (ordem_servico_id) REFERENCES ordens_servico (id) ON DELETE CASCADE,
    CONSTRAINT fk_item_servico FOREIGN KEY (servico_id)       REFERENCES servicos (id)        ON DELETE RESTRICT,
    CONSTRAINT ck_item_qtd     CHECK (quantidade > 0),
    CONSTRAINT ck_item_unit    CHECK (valor_unitario > 0),
    CONSTRAINT ck_item_total   CHECK (valor_total_item >= 0)
);

CREATE INDEX IF NOT EXISTS ix_itens_os_id ON itens_ordem_servico (ordem_servico_id);

-- historico de status ----------------------------------------------
CREATE TABLE IF NOT EXISTS historico_status_os (
    id                  SERIAL          PRIMARY KEY,
    ordem_servico_id    INTEGER         NOT NULL,
    status_anterior     VARCHAR(20)     NULL,
    status_novo         VARCHAR(20)     NOT NULL,
    data_hora           TIMESTAMP       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario             VARCHAR(80)     NOT NULL,
    CONSTRAINT fk_hist_os FOREIGN KEY (ordem_servico_id) REFERENCES ordens_servico (id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS ix_hist_os ON historico_status_os (ordem_servico_id);

-- auditoria ---------------------------------------------------------
CREATE TABLE IF NOT EXISTS auditoria (
    id              BIGSERIAL       PRIMARY KEY,
    entidade        VARCHAR(80)     NOT NULL,
    id_registro     VARCHAR(40)     NOT NULL,
    operacao        VARCHAR(10)     NOT NULL,
    data_hora       TIMESTAMP       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario         VARCHAR(80)     NOT NULL,
    snapshot_antes  JSONB           NULL,
    snapshot_depois JSONB           NULL,
    CONSTRAINT ck_audit_operacao CHECK (operacao IN ('INSERT','UPDATE','DELETE'))
);

CREATE INDEX IF NOT EXISTS ix_audit_entidade  ON auditoria (entidade, id_registro);
CREATE INDEX IF NOT EXISTS ix_audit_data_hora ON auditoria (data_hora);

COMMIT;
