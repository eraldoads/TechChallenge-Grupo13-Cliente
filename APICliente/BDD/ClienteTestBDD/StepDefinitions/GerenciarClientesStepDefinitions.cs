using Newtonsoft.Json;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using Domain.Entities;

namespace ClienteTestBDD.StepDefinitions
{
    [Binding]
    public class GerenciarClientesStepDefinitions
    {

        private String ENDPOINT_CLIENTE = "http://localhost:5008/cliente";

        /// <summary>
        /// Método que realiza o cadastro de um Cliente para que seja utilizado na busca posterior
        /// </summary>        
        [Given(@"que um Cliente já foi cadastrado")]
        public async Task GivenQueUmClienteJaFoiCadastradoAsync()
        {
            var client = new HttpClient();

            // prepara o request com o endpoint base
            var request = new HttpRequestMessage(HttpMethod.Post, ENDPOINT_CLIENTE);

            // prepara o conteúdo a ser enviado no POST
            var content = new StringContent("{\r\n  \"nome\": \"Eduardo\",\r\n  \"sobrenome\": \"Coruja\",\r\n  \"cpf\": \"07258083065\",\r\n  \"email\": \"eduardocoruja@gmail.com\"\r\n}", null, "application/json");
            request.Content = content;

            // realiza o cadastro do Cliente
            var response = await client.SendAsync(request);

            // armazena na variável o retorno do POST
            var rtn = JsonConvert.DeserializeObject<Cliente>(await response.Content.ReadAsStringAsync());

            // valida o status retornado no POST com o esperado (201)
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);

            // armazena no contexto o id do Cliente que foi cadastrado
            ScenarioContext.Current["idCliente"] = rtn.IdCliente;

        }
        /// <summary>
        /// Método que realiza a busca do Cliente cadastrado por seu ID
        /// </summary>        
        [When(@"requisitar a busca do Cliente por ID")]
        public async Task WhenRequisitarABuscaDoClientePorIDAsync()
        {
            var client = new HttpClient();

            // inicializa a variável com o idCliente retornado no cadastro realizado anteriormente
            var id = ScenarioContext.Current["idCliente"];

            // monta o endpoint que será utilizado no GET, incluindo o parâmetro a ser utilizado na busca
            var request = new HttpRequestMessage(HttpMethod.Get, ENDPOINT_CLIENTE + "/" + id);

            // realiza o GET
            var response = await client.SendAsync(request);

            // converte o retorno para Cliente
            var rtn = JsonConvert.DeserializeObject<Cliente>(await response.Content.ReadAsStringAsync());

            // armazena no contexto o conteúdo retornado para utilizar posteriormente na validação
            ScenarioContext.Current["Cliente"] = rtn;

            // verifica se o Status do retorno da requisição é o esperado (200)
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);

        }

        /// <summary>
        /// Método que valida se o Cliente retornado é realmente o que foi cadastrado anteriormente
        /// </summary>
        [Then(@"o Cliente é exibido com sucesso")]
        public void ThenOClienteEExibidoComSucesso()
        {
            // inicializa a variável com o Cliente retornado na consulta anterior
            var clienteRetornado = (Cliente)ScenarioContext.Current["Cliente"];

            // inicializa a variável com o id do Cliente cadastrado no primeiro step
            var idClienteInicial = (int)ScenarioContext.Current["idCliente"];

            // valida se o id do Cliente cadastrado inicialmente é igual ao id do Cliente retornado na consulta
            Assert.True(clienteRetornado.IdCliente == idClienteInicial);
        }
        /// <summary>
        /// Método executado após o Cenário @tag1 para eliminar o registro criado no primeiro step
        /// </summary>
        [AfterScenario("@tag1")]
        public async Task AfterScenarioAsync()
        {
            var client = new HttpClient();

            // inicializa a variável com o idCliente retornado no cadastro realizado anteriormente
            var id = ScenarioContext.Current["idCliente"];

            // monta o endpoint que será utilizado no DELETE, incluindo o parâmetro a ser utilizado
            var request = new HttpRequestMessage(HttpMethod.Delete, ENDPOINT_CLIENTE + "/" + id);

            // realiza o DELETE
            var response = await client.SendAsync(request);
                        
            // verifica se o Status do retorno da requisição é o esperado (200)
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);

        }

    }
}
