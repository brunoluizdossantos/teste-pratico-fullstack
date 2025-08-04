## Descrição

A API foi desenvolvida utilizando as seguintes tecnologias:

- .Net 8 e C#
- Clean Architecture
- CQRS
- ORM Entity Framework
- FluentValidation
- Autenticação JWT
- Docker

Banco de dados SQL:

- MySQL

Interface Web do Banco de Dados:

- phpMyAdmin

Documentação da API:

- Swagger

Qualidade de Testes:

- Testes unitários
- Testes de integração (serviços)
- Testes de integração (repositórios)

A UI foi desenvolvida utilizando as seguintes tecnologias:

- Angular 19


## Comandos necessários para inicializar os projetos utilizando Docker

```bash
# Entra no diretório do projeto
$ cd teste-pratico

# Restaura os pacotes NuGet
$ dotnet restore TestePratico.sln

# Executa a aplicação e os contêineres em segundo plano
$ docker-compose up --build -d

# Para visualizar a documentação da API
https://localhost:5001/swagger/index.html

# Para visualizar a UI
http://localhost:4200/
```