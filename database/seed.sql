-- Dados de exemplo. Rodar em base limpa.

BEGIN;

INSERT INTO clientes (nome, documento, tipo, email, telefone, ativo) VALUES
    ('Joao da Silva',     '12345678901',    'F', 'joao@exemplo.com',  '11988887777', TRUE),
    ('Maria Souza ME',    '98765432000110', 'J', 'maria@exemplo.com', '1133224455',  TRUE),
    ('Carlos Pereira',    '45612378900',    'F', 'carlos@exemplo.com', NULL,         TRUE);

INSERT INTO servicos (nome, valor_base, percentual_imposto, ativo) VALUES
    ('Manutencao preventiva',       250.00, 5.00, TRUE),
    ('Instalacao de equipamento',   480.00, 7.50, TRUE),
    ('Suporte remoto - hora tecnica', 120.00, 0.00, TRUE);

COMMIT;
