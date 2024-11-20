using System;
using System.Threading.Tasks;

class Program
{
    private static readonly string apiKey = "05377b6c7e86a52487e531de"; // Substitua pela sua chave de API real

    static async Task Main(string[] args)
    {
        ConversorMoeda conversor = new ConversorMoeda(apiKey);

        while (true)
        {
            Console.Write("Moeda origem (3 caracteres, vazio para sair): ");
            string moedaOrigem = Console.ReadLine().ToUpper();
            if (string.IsNullOrEmpty(moedaOrigem)) break;
            
            if (!Validador.ValidarMoeda(moedaOrigem))
            {
                Console.WriteLine("A moeda de origem deve ter exatamente 3 caracteres.");
                continue;
            }

            Console.Write("Moeda destino (3 caracteres): ");
            string moedaDestino = Console.ReadLine().ToUpper();
            
            if (!Validador.ValidarMoeda(moedaDestino))
            {
                Console.WriteLine("A moeda de destino deve ter exatamente 3 caracteres.");
                continue;
            }
            
            if (!Validador.ValidarConversao(moedaOrigem, moedaDestino))
            {
                Console.WriteLine("A moeda de origem não pode ser igual à moeda de destino.");
                continue;
            }

            Console.Write("Valor a ser convertido: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal valor) || !Validador.ValidarValor(valor))
            {
                Console.WriteLine("O valor deve ser um número positivo.");
                continue;
            }

            try
            {
                var resultado = await conversor.ConverterMoedaAsync(moedaOrigem, moedaDestino, valor);
                if (resultado != null)
                {
                    Console.WriteLine($"Valor convertido: {resultado.Value.ValorConvertido:F2}");
                    Console.WriteLine($"Taxa de conversão: {resultado.Value.Taxa:F6}");
                }
                else
                {
                    Console.WriteLine("Erro ao converter a moeda. Verifique se os códigos das moedas estão corretos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na comunicação com a API: {ex.Message}");
            }
        }
    }
}
