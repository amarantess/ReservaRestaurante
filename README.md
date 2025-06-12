# Reserva Restaurante
<p align="center">
    <a href="https://sonarcloud.io/summary/new_code?id=gabrielamarantes13_ReservaRestaurante">
        <img src="https://sonarcloud.io/api/project_badges/measure?project=gabrielamarantes13_ReservaRestaurante&metric=alert_status" alt="Quality Gate"/>
        <img src="https://sonarcloud.io/api/project_badges/measure?project=gabrielamarantes13_ReservaRestaurante&metric=bugs" alt="Bugs"/>
        <img src="https://sonarcloud.io/api/project_badges/measure?project=gabrielamarantes13_ReservaRestaurante&metric=code_smells" alt="Code Smells"/>
        <img src="https://sonarcloud.io/api/project_badges/measure?project=gabrielamarantes13_ReservaRestaurante&metric=coverage" alt="Coverage"/>
        <img src="https://sonarcloud.io/api/project_badges/measure?project=gabrielamarantes13_ReservaRestaurante&metric=duplicated_lines_density" alt="Duplicated Lines">
        <img src="https://sonarcloud.io/api/project_badges/measure?project=gabrielamarantes13_ReservaRestaurante&metric=vulnerabilities" alt="Vulnerabilities"/>
    </a>
    <br>
    <img src="https://img.shields.io/badge/Status-Concluído-blue?style=for-the-badge" alt="Status: Concluído"/>
</p>

## Sobre o Projeto
O **Reserva Restaurante** é uma API RESTful desenvolvida em .NET 8 que simplifica o processo de reserva de mesas em restaurantes. O projeto foi criado como um **[desafio de programação](https://racoelho.com.br/listas/desafios/sistema-de-reservas-de-restaurante)** proposto por **[Rafael Coelho](https://www.linkedin.com/in/racoelhoo/)**, servindo como uma oportunidade para aprofundar conhecimentos em C# e explorar tecnologias e padrões modernos de desenvolvimento.

A aplicação oferece um conjunto completo de endpoints para gerenciar usuários, mesas e reservas, com um sistema de autenticação seguro baseado em tokens JWT.

## Features
- **Gerenciamento de Usuários**: CRUD completo de usuários.
- **Gerenciamento de Mesas**: CRUD completo de mesas disponíveis no restaurante.
- **Gerenciamento de Reservas**: Sistema para criar, editar e cancelar reservas.
- **Autenticação Segura**: Login com JWT e Refresh Tokens.
- **Login com Google**: Autenticação integrada com contas Google.
- **Mensageria**: Fila de mensagens com Service Bus para lidar com processos assíncronos, como a exclusão de contas.
- **Tratamento de Exceções**: Exceções personalizadas para fornecer feedback claro e informativo sobre erros.

## Arquitetura e Padrões
Para garantir um código de alta qualidade, manutenível e escalável, o projeto foi construído sobre uma base sólida de padrões e boas práticas:

* **Domain-Driven Design (DDD) & SOLID:** A arquitetura segue os princípios do DDD para modelar o domínio de negócio de forma rica e os princípios SOLID para criar um código coeso e com baixo acoplamento.
* **Testes Abrangentes:** Foram implementados **testes de unidade e integração** para garantir a confiabilidade e o correto funcionamento de cada parte da aplicação.
* **Injeção de Dependência:** Utilizada em toda a aplicação para promover a modularidade e facilitar os testes.
* **CI/CD com Azure DevOps & SonarCloud:** Um pipeline de integração e entrega contínua automatiza os builds, testes e a análise estática de código, garantindo que novas alterações mantenham a qualidade e a segurança.
* **GitFlow:** A estratégia de ramificação GitFlow foi usada para organizar o desenvolvimento, separando features, releases e hotfixes de forma controlada.

## Tecnologias Utilizadas

**Backend**
<br>
![C#](https://img.shields.io/badge/C%23-512BD4?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET 8](https://img.shields.io/badge/.NET%208-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET](https://img.shields.io/badge/ASP.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

**Banco de Dados**
<br>
![MySQL](https://img.shields.io/badge/mysql-4479A1.svg?style=for-the-badge&logo=mysql&logoColor=white)
![Dapper](https://img.shields.io/badge/Dapper-4479A1?style=for-the-badge)
![Fluent Migrator](https://img.shields.io/badge/Fluent%20Migrator-4479A1?style=for-the-badge)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

**Testes**
<br>
![xUnit](https://img.shields.io/badge/xUnit-007ACC?style=for-the-badge&logo=xunit&logoColor=white)
![Moq](https://img.shields.io/badge/Moq-007ACC?style=for-the-badge)
![Bogus](https://img.shields.io/badge/Bogus-007ACC?style=for-the-badge)
![Fluent Assertions](https://img.shields.io/badge/Fluent%20Assertions-007ACC?style=for-the-badge)

**Qualidade & DevOps**
<br>
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Azure DevOps](https://img.shields.io/badge/Azure%20DevOps-0000ff?logo=azuredevops&logoColor=fff&style=for-the-badge)
![SonarCloud](https://img.shields.io/badge/SonarCloud-F3702A?style=for-the-badge&logo=sonarcloud&logoColor=white)

**Segurança e Autenticação**
<br>
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![Google](https://img.shields.io/badge/Google%20OAuth-4285F4?style=for-the-badge&logo=google&logoColor=white)
![BCrypt](https://img.shields.io/badge/BCrypt-624389?style=for-the-badge)

**Outras Ferramentas**
<br>
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![AutoMapper](https://img.shields.io/badge/AutoMapper-ff0000?style=for-the-badge)
![FluentValidation](https://img.shields.io/badge/Fluent%20Validation-592D84?style=for-the-badge)

## Aprendizados e Desafios
Desenvolver esta API foi uma jornada de aprendizado imensa, marcando meu primeiro contato prático com diversas tecnologias e padrões essenciais. Nesta seção, compartilho os principais desafios e as soluções que implementei.

### Domain-Driven Design (DDD)
O primeiro grande desafio foi a mudança de paradigma para o Domain-Driven Design (DDD). Compreendi que o DDD não é uma arquitetura, mas uma abordagem que coloca o domínio do negócio no centro do desenvolvimento. Em vez de focar primeiro na tecnologia, mergulhei nas regras de negócio de um sistema de reservas para criar um modelo rico e coeso. A aplicação do DDD foi fundamental para entender o valor dos princípios SOLID, especialmente a Responsabilidade Única, resultando em um código muito mais organizado, testável e fácil de manter.

### Estratégia de Testes
Inicialmente, eu tinha dificuldade em entender o propósito dos testes automatizados. Após estudar o tema, compreendi seu valor e implementei uma estratégia robusta. Em vez de testar o sistema todo de uma vez, foquei em testar as menores unidades de forma isolada (testes de use cases, validação e integração). A arquitetura baseada em DDD tornou os testes de unidade especialmente eficazes, pois pude validar a lógica de negócio diretamente no modelo de domínio, com mínima dependência de infraestrutura. Isso me deu muito mais confiança para refatorar e adicionar novas features, sabendo que os testes garantiriam a estabilidade do código.

### Autenticação com JWT e Refresh Tokens
Sempre tive curiosidade em entender como aplicações mantêm o usuário logado de forma segura. Aprendi que, após o login, a API retorna um **Access Token (JWT)**, que contém *claims* (reivindicações) sobre a identidade e permissões do usuário. Este token é enviado no cabeçalho `Authorization` de cada requisição e sua assinatura digital é verificada no servidor para garantir sua autenticidade.

Para melhorar a experiência do usuário, implementei também o fluxo de **Refresh Tokens**. Um refresh token é um token de longa duração que é usado especificamente para obter um novo access token quando o original expira, evitando que o usuário precise digitar suas credenciais repetidamente.

### Processamento Assíncrono com Mensageria
A funcionalidade de exclusão de conta precisava de uma solução mais sofisticada do que um simples `DELETE` no banco de dados. O objetivo era implementar um período de "carência": ao solicitar a exclusão, a conta seria desativada e apenas excluída permanentemente após alguns dias, dando ao usuário a chance de reativá-la.

Para resolver isso, utilizei um padrão de mensageria com o **Azure Service Bus**.
1.  Quando o usuário solicita a exclusão, a API publica uma mensagem na fila com um **agendamento para entrega futura** (ex: 7 dias depois).
2.  Um `BackgroundService` monitora essa fila.
3.  Apenas quando a mensagem se torna visível (após o período de carência), o worker a processa, executando a exclusão definitiva dos dados do usuário de forma assíncrona e resiliente.

Este foi meu primeiro contato prático com arquiteturas orientadas a eventos e me ensinou o poder do processamento assíncrono para criar sistemas mais responsivos, resilientes e desacoplados.

### Instalação
1.  Clone o repositório:
    ```sh
    git clone https://github.com/amarantess/ReservaRestaurante.git
    ```
2.  **Navegue até o diretório do projeto:**
    ```sh
    cd ReservaRestaurante
    ```
3.  **Configure a Conexão com o Banco:**
    * No arquivo `appsettings.Development.json`, preencha a sua `ConnectionString` para o banco de dados MySQL.

4.  **Execute as Migrações:**
    * Este projeto usa **FluentMigrator**. Execute o comando abaixo no terminal para criar as tabelas:
    ```sh
    dotnet fm migrate up
    ```
5.  **Execute a API:**
    ```sh
    dotnet run
    ```
    A API estará disponível em `http://localhost:5000` (ou a porta configurada).

## Contato
Desenvolvido por **Gabriel Amarantes**

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/gabriel-amarantes/)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/amarantess)