# Template de variaveis de ambiente para tools/provision.
# Copie como tools/supabase.env.ps1 (gitignored), preencha a senha
# real e carregue com:  . .\tools\supabase.env.ps1

$env:SUPABASE_DB_HOST     = "aws-1-us-west-1.pooler.supabase.com"
$env:SUPABASE_DB_PORT     = "5432"
$env:SUPABASE_DB_NAME     = "postgres"
$env:SUPABASE_DB_USER     = "postgres.bsmntmnhyunrheftcvvs"
$env:SUPABASE_DB_PASSWORD = "SUA_SENHA_AQUI"
