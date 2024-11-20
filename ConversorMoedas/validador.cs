using System;

public static class Validador
{
    public static bool ValidarMoeda(string moeda)
    {
        return moeda.Length == 3;
    }

    public static bool ValidarValor(decimal valor)
    {
        return valor > 0;
    }

    public static bool ValidarConversao(string moedaOrigem, string moedaDestino)
    {
        return moedaOrigem != moedaDestino;
    }
}
