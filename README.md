## Descrição

A API foi desenvolvida utilizando as seguintes tecnologias:

- .Net 8 e C#
- Clean Architecture
- CQRS
- ORM Entity Framework
- FluentValidation
- Autenticação JWT
- Testes com xUnit
- Docker

Banco de dados SQL:

- MySQL

Interface Web do Banco de Dados:

- phpMyAdmin

Documentação da API:

- Swagger

Qualidade de Testes criados com a biblioteca xUnit:

- Testes unitários
- Testes de integração (serviços)
- Testes de integração (repositórios)
- Testes de integração (endpoints)

A UI foi desenvolvida utilizando as seguintes tecnologias:

- Angular 19


## Comandos necessários para inicializar os projetos utilizando Docker

```bash
# Entra no diretório do projeto
$ cd teste-pratico-fullstack

# Restaura os pacotes NuGet
$ dotnet restore TestePratico.sln

# Executa a aplicação e os contêineres em segundo plano
$ docker-compose up --build -d

# Para visualizar a documentação da API
https://localhost:5001/swagger/index.html

# Para visualizar a UI
http://localhost:4200/

# Para visualizar o banco de dados
http://localhost:8080/
```

## Comandos necessários para executar os testes do backend

```bash
# Entra no diretório dos testes unitários
$ cd Tests/Tests.Unit
$ dotnet test

# Entra no diretório dos testes de integração (serviços)
$ cd ../Tests.Integration.Service
$ dotnet test

# Entra no diretório dos testes de integração (repositórios)
$ cd ../Tests.Integration.Repository
$ dotnet test

# Entra no diretório dos testes de integração (endpoints)
$ cd ../Tests.Integration.WebAPI
$ dotnet test

```