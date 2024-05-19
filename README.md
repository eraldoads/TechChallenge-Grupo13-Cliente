# TechChallenge-Grupo13-Cliente
Este repositório é dedicado ao microsserviço de clientes, o qual foi desmembrado do monolito criado para a lanchonete durante a evolução da pós-graduação em Arquitetura de Software da FIAP.

Tanto o build e push para o repositório no ECR da AWS usando Terraform, quanto a análise de código e cobertura de testes utilizando SonarCloud são realizados via Github Actions.

## 🖥️ Grupo 13 - Integrantes
🧑🏻‍💻 *<b>RM352133</b>*: Eduardo de Jesus Coruja </br>
🧑🏻‍💻 *<b>RM352316</b>*: Eraldo Antonio Rodrigues </br>
🧑🏻‍💻 *<b>RM352032</b>*: Luís Felipe Amengual Tatsch </br>

## Arquitetura
Quando disparamos a Github Action, é realizado o build da aplicação e o push para o repositório criado previamente no Elastic Container Registry (ECS).
Ao final da action, é atualizada a Service no Elastic Container Service (ECS), executando assim a service que irá realizar a criação do container.

![image](https://github.com/eraldoads/TechChallenge-Grupo13-Cliente/assets/47857203/cc0b90a4-d8f3-4c77-ad54-2d9038e034ff)

Para este microsserviço, utilizamos .NET 8.0, o que também representa uma evolução de tecnologia em relação ao monolito, o qual foi baseado no .NET 6.0 .

## Testes

Utilizamos a ferramenta SonarCloud para análise de código e cobertura de testes. Para este microsserviço, atingimos 98% de cobertura, conforme abaixo:

https://sonarcloud.io/summary/overall?id=eraldoads_TechChallenge-Grupo13-Cliente

![image](https://github.com/eraldoads/TechChallenge-Grupo13-Cliente/assets/47857203/cf911e32-016a-4429-8122-61bc2085eecb)
![image](https://github.com/eraldoads/TechChallenge-Grupo13-Cliente/assets/47857203/0a5ca248-8be2-449d-99a9-1c28eccd486f)

### BDD – Behavior-Driven Development

Para organizar o código e armazenar os cenários de testes com a técnica BDD (Desenvolvimento Orientado por Comportamento) criou-se um projeto baseado na ferramenta SpecFlow que trata-se de uma implementação oficial do Cucumber para .NET que utiliza o Gherkin como analisador. Neste exemplo foi configurado em conjunto com o framework nUnit. 

#### Organização do Teste

Um novo projeto, chamado <b>ClienteTestBDD</b> foi adicionado à solução na pasta BDD dentro da estrutura de Testes.

Arquivo de configuração do projeto ClienteTestBDD
![image](https://github.com/eraldoads/TechChallenge-Grupo13-Cliente/assets/149120484/9dad3904-b9f7-4f54-8181-26bfc8128ff7)

O arquivo <b>Cliente.features</b> armazena as funcionalidades (features) que serão utilizadas como base para a validação da API Cliente. Para efeito de estudo, foi definido o cenário que realiza a busca de um Cliente a partir de um ID informado.
![image](https://github.com/eraldoads/TechChallenge-Grupo13-Cliente/assets/149120484/56a558fd-0d50-4405-8579-f3c383611e32)

O arquivo <b>GerenciarClientesStepDefinitions.cs</b> contém os passos que serão executados para validar os cenários definidos nas features. No exemplo, há quatro métodos implementados, sendo três executados para validar o cenário e um para que o contexto do sistema permaneça o mesmo que antes da realização dos testes.
![image](https://github.com/eraldoads/TechChallenge-Grupo13-Cliente/assets/149120484/dbd44005-6eec-4809-b80a-03cdaa8b65f8)

##### GivenQueUmClienteJaFoiCadastradoAsync
Implementa os passos que serão realizados para atender o que foi estabelecido no <b>“Dado”</b>. Neste exemplo o método é responsável por inserir um Cliente para garantir que o Cenário de Busca seja concluído com sucesso.
![image](https://github.com/eraldoads/TechChallenge-Grupo13-Cliente/assets/149120484/a1fbc5ca-772e-4dc3-b147-7ca51a99a972)

##### WhenRequisitarABuscaDoClientePorIDAsync
Implementa os passos que serão realizados para atender o que foi estabelecido no <b>“Quando”</b>. Neste exemplo o método realiza a busca pelo Cliente utilizando o ID.

![image](https://github.com/eraldoads/TechChallenge-Grupo13-Cliente/assets/149120484/ee13e61c-a1e8-4dfc-aa48-cdde1b560adb)

##### ThenOClienteEExibidoComSucesso
Implementa os passos que serão realizados para atender o que foi estabelecido no <b>“Então”</b>. Neste exemplo o método confere se o Cliente exibido é realmente o cadastrado anteriormente.
![image](https://github.com/eraldoads/TechChallenge-Grupo13-Cliente/assets/149120484/3da307c3-fac1-498a-be42-fe5e118ff89f)

##### AfterScenarioAsync
Implementa os passos que serão realizados após a execução dos métodos específicos de cada Cenário. Neste exemplo o método exclui o Cliente que foi cadastrado no primeiro passo. Desta forma é possível garantir que o teste possa ser executado com sucesso em uma próxima execução, pois do contrário o teste falharia porque a API não permite que um mesmo CPF seja utilizado duas vezes.
![image](https://github.com/eraldoads/TechChallenge-Grupo13-Cliente/assets/149120484/8782557c-bd88-4723-a35b-603749a92f13)

#### Execução dos Testes
A imagem a seguir apresenta o resultado da execução de todos os testes que a solução possui, destacando o cenário definido em BDD BuscaClientePorID, bem como o detalhe dos passos realizados.
![image](https://github.com/eraldoads/TechChallenge-Grupo13-Cliente/assets/149120484/e90167fc-4c06-4a77-88a2-97170f897386)
