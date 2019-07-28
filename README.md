# LuizaLabs Wishes
Api para Luiza Labs de cadastro de produto/usuário  e lista de desejo.

Desenvolvido em .Net Core 2.2 e Mongodb.

# Autenticação

**JWT Bearer**

Para usar os endpoints, as requisições precisam usar autenticação Bearer passando o token de autenticação no header com nome de "Authorization", como no exemplo a seguir usando Postman:
![image](https://user-images.githubusercontent.com/19582173/62012267-72a37a00-b15a-11e9-91e0-48247709ebdc.png)

*Obtendo Token:*
Para obter o token, precisamos fazer um requisição do tipo Post para o endpoint __/auth__ passando o seguinte Paylod:

```
{
	"login" : "flavio",
	"senha" : "luizaLabs"
}
```

Retorno esperado com token e data e hora de expiração:

![image](https://user-images.githubusercontent.com/19582173/62012342-7aafe980-b15b-11e9-8a07-8592bb757f95.png)

# Injeção de Dependência

Para melhorar o desenvolvimento, reutilização do código e facilidade de teste, a api está utlizando injeção de dependência do .net core. Para incluir novas classes que utiliza dessa facilidade, devemos incluir o código no Startup.cs no projeto da API, conforme imagem abaixo:

![image](https://user-images.githubusercontent.com/19582173/62012466-fcecdd80-b15c-11e9-8c24-0bf422409513.png)

# DDD - Domain Driven Design

A api utiliza conceitos do DDD para implementar as regras de negócio no dominio da aplicação, deixando o sistema mais flexível para uma evolução das funcionalidades, a imagem abaixo mostra no visual studio a arquitetura do projeto:

![image](https://user-images.githubusercontent.com/19582173/62012546-0fb3e200-b15e-11e9-9f72-7ffd0c0e4748.png)

1. Application : Camada de apresentação tem como objetivo de expor toda a interação com o usuário, nesse caso utilizamos API para expor os dados, mas poderíamos ter outras camadas, como exemplo tela de mobile.

2. Domain:
  - Entities : Modelo de classes com toda a estrutura e comportamento do sistema, camada principal onde podemos evoluir as regras de negócio de cada entidade.
  - Services : Camada responsável em manipular uma ou mais entidades, quando temos regras que necessite de mais interações, como por exemplo o acesso ao repositório, utilizamos a camada de serviço para complementar as regras da entidade.
  
3. Infra:
  - Cross : Quando temos a necessidade de ter classes para nos auxiliar na entrega das regras de negócios, como por exemplo o controle das exceções, que pode acontecer em qualquer camada da aplicação.
  - Data : Todo acesso ao banco de dados ou qualquer outro sistema de persistencia de dados, temos a camada do repositório para nos auxiliar a entregar essa comunicação de forma fácil. Nesse projeto utilizamos um pattner de repositório, foi criado um classe para abstrair todos os metodos comuns entre as classes de repositórios, caso necessite de uma repositório mais específico, podemos usar as classes de especialização. Outro benefício nessa camada, é que podemos implementar algum serviço de cache, como exemplo Redis.

4. UnitTest : Testes de unidade do projeto, dando mais integridade no código, protegendo sobre futuras alterações.
