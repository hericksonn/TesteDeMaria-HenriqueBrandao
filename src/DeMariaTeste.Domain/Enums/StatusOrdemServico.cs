namespace DeMariaTeste.Domain.Enums
{
    // Persistido como string em ordens_servico.status para ficar legivel
    // em consultas ad-hoc.
    public enum StatusOrdemServico
    {
        Aberta = 0,
        EmAndamento = 1,
        Concluida = 2,
        Cancelada = 3
    }
}
