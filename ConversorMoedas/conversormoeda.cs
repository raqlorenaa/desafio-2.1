using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class ConversorMoeda
{
    private static readonly HttpClient client = new HttpClient();
    private string apiKey;

    public ConversorMoeda(string apiKey)
    {
        this.apiKey = apiKey;
    }

    public async Task<(decimal ValorConvertido, decimal Taxa)?> ConverterMoedaAsync(string moedaOrigem, string moedaDestino, decimal valor)
    {
        string url = $"https://v6.exchangerate-api.com/v6/{apiKey}/pair/{moedaOrigem}/{moedaDestino}/{valor}";
        
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        JObject data = JObject.Parse(responseBody);

        if (data["result"]?.ToString() == "success")
        {
            decimal taxa = (decimal)data["conversion_rate"];
            decimal valorConvertido = (decimal)data["conversion_result"];
            return (valorConvertido, taxa);
        }
        else
        {
            return null;
        }
    }
}
