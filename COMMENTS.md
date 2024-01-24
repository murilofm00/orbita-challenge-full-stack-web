# Sobre o projeto

## Arquitetura utilizada

Para a execução do desafio foram criados 3 projetos, sendo eles, o front end, o back end e um projeto de testes automatizados. neles foram cumpridos todos os requisitos propostos. A seguir está o detalhamento dos projetos.

### orbita-frontend

Consiste em um serviço que disponibiliza uma aplicação web do tipo Single Page Application para interação do usuário com a entidade Student.

O serviço é escrito na linguagem **TypeScript** com o framework **Vue.js 3** rodando no motor **Vite**.

O serviço utiliza a biblioteca **Vue Router**, responsável por administrar o roteamento das páginas da aplicação.

O serviço utiliza a biblioteca de componentes **Vuetify** que provê os componentes e estilos baseados no Design System [Material Design](https://m3.material.io).

O serviço utiliza a biblioteca **Axios** para realizar as requisições ao serviço do _Orbita.Backend_.

Para validação dos formulários, o serviço utiliza a biblioteca **Vuelidate 2.0**. Que provê as validações necessárias para que os dados sejam preenchidos corretamente, alertando ao usuário quando é passado alguma informação fora do padrão esperado.

O serviço utiliza a biblioteca **Maska** para criar máscaras para os campos necessários, garantindo o preenchimento e exibição corretos de campos como CPF.

Tecnologias utilizadas:

- [Vue.js 3](https://vuejs.org/): utilizado para criação dos componentes da aplicação.
- [Vite](https://vitejs.dev/): utilizado para compilar o código em Vue para o navegador.
- [Vue Router](https://router.vuejs.org/): utilizado para administrar o roteamento das páginas da aplicação.
- [Vuetify](https://vuetifyjs.com/en/): utilizado para componentizar e estilizar a aplicação.
- [Axios](https://axios-http.com/docs/intro): utilizado para realizar as requisições HTTP externas.
- [Vuelidate 2.0](https://vuelidate-next.netlify.app/): utilizado para validar os formulários.
- [Maska](https://beholdr.github.io/maska/): utilizado para criar máscaras.

### Orbita.Backend

Consiste em um serviço em **.NET 7.0** em conjunto com um banco de dados **PostgreSQL**. tanto o serviço quanto o banco estão configurados para serem executados em containers **Docker**. A orquestração e comunicação entre os containers é realizada pelo **Docker Compose**.

O serviço é escrito na linguagem C# e disponibiliza endpoints no protocolo HTTP Rest para operações de CRUD(Create, Read, Update, Delete) para as entidades.
Para conexão e mapeamento do banco de dados foi utilizado o **EntityFramework**.

O banco de dados **PostgreSQL** foi utilizado para persistência dos dados das entidades.

Tecnologias utilizadas:

- [Docker](https://docs.docker.com/engine/install/): utilizado para criação dos containers para execução do projeto.
- [Docker compose](https://docs.docker.com/compose/install/): utilizado para orquestração dos containers, garantindo a criação e comunicação entre os containers.
- [Asp.NET Core](https://dotnet.microsoft.com/pt-br/apps/aspnet): utilizado para mapeamento dos endpoints.
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/): utilizado para mapeamento e consultas ao banco de dados.
- [Npgsql](https://www.npgsql.org/efcore/): provedor de conxão do PostgreSQl para o Entity Framework.

### Orbita.BackEnd.Tests

Consiste em um serviço para testar as funções do _Orbita.Backend_. O serviço utiliza o **xUnit** para criar funções que validam se as regras de negócio estão sendo cumpridas pelo backend.

São realizados testes unitários e testes de integração, o ultimo utilizando um banco de dados simulado no **Docker** e que é criado pela ferramenta **Testcontainers**.

Tecnologias utilizadas:

- [xUnit](https://xunit.net/): utilizado para criar o ambiente e as funções de teste.
- [Docker](https://docs.docker.com/engine/install/): utilizado para hospedar um container simulando a base original.
- [Testcontainers](https://testcontainers.com/): utilizado para criar o container do banco.

## Instruções para execução do projeto

### Executar o backend

Ferramentas necessárias:

- [Docker compose](https://docs.docker.com/compose/install/).
- [.NET 7.0](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0).
- [Visual Studio 2022 _(opcional)_](https://visualstudio.microsoft.com/pt-br/vs/).

O projeto _Orbita.Backend_ está configurado para ser executado automaticamente no **Visual Studio 2022** apertando a tecla `F5`.

Caso não possua acesso à IDE em sua máquina, você pode rodar o projeto via linha de comando com o **Docker compose** seguindo os passos a seguir.

- A partir da raiz do projeto, navegue até o diretório do projeto (`./Orbita.BackEnd`).

```bash
cd ./Orbita.Backend
```

- Execute o seguinte comando do Docker compose para inicializar os containers (Obs: Certifique que o docker compose esteja sendo executado).

```bash
docker compose up
```

Após os containers serem iniciados, os serviços estarão sendo executados nas portas:

- `Backend.Api`: `5401` para rquisiçõe `https` e `5400` para requisições `http`.
- `Postgres`: `5432` para o banco de dados.

Caso realize alguma alteração no código, rode novamente o comando de inicialização do docker `docker compose up` para que as alterações tenham efeito.

### Executar o frontend

Ferramentas necessárias:

- [Nodejs e npm](https://nodejs.org/en/download/).

O projeto _orbita-frontend_ utiliza o Nodejs para ser executado, execute os seguintes passos para executar o projeto.

- A partir da raiz do projeto, navegue até o diretório do projeto (`./orbita-frontend`).

```bash
cd ./orbita-frontend
```

- Instale as bibliotecas necessárias para execução do projeto.

```bash
npm install
```

- Inicie o serviço de desenvolvimento.

```
npm run dev
```

O projeto será inicializado por padrão na porta `5173`, caso ela não esteja disponível em sua máquina, ele será inicializado em uma porta aleatória que será exibida na sua linha de comandos. Procure pelo texto `  ➜  Local:   http://localhost:5173/` para descobrir a porta em que o serviço inicializou. Abra o navegador na porta inicializada para visualizar a aplicação em execução.

### Testes do backend

Ferramentas necessárias:

- [Docker](https://docs.docker.com/engine/install/)
- [.NET 7.0](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0).
- [Visual Studio 2022 _(opcional)_](https://visualstudio.microsoft.com/pt-br/vs/).

O projeto _Orbita.BackEnd.Tests_ realiza testes unitários e de integração no projeto _Orbita.BackEnd_. O projeto está configurado para o **Visual Studio 2022**, onde é possível executar os testes pelo [Gerenciador de Testes](https://learn.microsoft.com/pt-br/visualstudio/test/run-unit-tests-with-test-explorer?view=vs-2022).

Caso não possua acesso à IDE em sua máquina, você pode rodar o projeto via linha de comando com a **CLI do .Net** seguindo os passos a seguir.

- A partir da raiz do projeto, navegue até o diretório do projeto (`./Orbita.BackEnd.Tests`).

```bash
cd ./orbita-frontend
```

- Execute o comando para que os testes sejam executados.

```bash
dotnet test --logger "console;verbosity=normal"
```

Após a execução do comando, será apresentado no console o resultado dos testes. Indicando quais obtiveram sucesso e quais falharam.

## Possíveis melhorias

- Implementar autenticação e autorização do usuário. Garantindo que apenas usuários autorizados tenham acesso as informações. Possíveis tecnologias para implementação:
    - [Identity](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio).
    - [JSON Web Tokens (JWT)](https://jwt.io).
- Implementar paginação do lado do servidor para a listagem das entidades. Possíveis tecnologias para implementação:
    - [OData](https://learn.microsoft.com/en-us/odata/).
- Implementar melhor validação do campo de CPF, garantindo que a sequência fornecida é um CPF válido.