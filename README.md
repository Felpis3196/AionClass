
#  AionClass - Plataforma de Cursos de Programação (Full-Stack)

Este projeto é uma **plataforma de cursos online robusta e escalável**, desenvolvida com foco em **C#/.NET e tecnologias web modernas**. Ele demonstra proficiência sólida em:

* **Desenvolvimento Backend com ASP.NET Core:** Construção de **APIs RESTful** seguras e de alta performance, utilizando as melhores práticas do ASP.NET Core.
* **Gestão de Autenticação e Autorização:** Implementação de um sistema de segurança robusto com **ASP.NET Core Identity** e **JSON Web Tokens (JWT Bearer)** para controle de acesso granular e proteção de rotas.
* **Persistência de Dados com Entity Framework Core:** Modelagem e interação eficiente com banco de dados **PostgreSQL**, utilizando convenções e migrações do EF Core.
* **Arquitetura Orientada a Serviços:** Aplicação de princípios de Injeção de Dependência (`AddScoped<Interface, Implementation>`) para promover a modularidade, testabilidade e separação de responsabilidades (ex: `AuthService`,`CursoService`, `MatriculaService`, `UserService`).
* **Boas Práticas de Segurança:** Configuração de **HTTPS**, validação rigorosa de tokens JWT e manipulação segura de dados sensíveis.
* **Desenvolvimento Frontend Web (Estrutura):** Habilidades em **HTML, CSS e JavaScript** para construção da interface de usuário, garantindo uma experiência responsiva e intuitiva.

##  Sobre a AionClass

A **AionClass** é uma plataforma inovadora de cursos online projetada para democratizar o acesso ao aprendizado de programação, com ênfase inicial em C# e o ecossistema .NET.
Nosso objetivo é fornecer uma experiência de aprendizado completa e de alta qualidade, abrangendo desde conceitos fundamentais até tópicos avançados.

Funcionalidades chave incluem:

* **Gerenciamento Abrangente de Cursos:** Criação, edição e listagem de cursos com módulos e aulas.
* **Sistema de Matrícula de Alunos:** Facilita a inscrição de usuários em cursos e o acompanhamento de seu progresso.
* **Perfis de Usuário:** Gestão de informações de perfil, incluindo edição e visualização.
* **Autenticação Segura:** Registro e login de usuários com tokens de segurança para acesso autorizado.
* **Interface de Usuário Intuitiva:** Design responsivo e amigável para uma excelente experiência de navegação e aprendizado.

##  Stack Tecnológico Principal

Este projeto foi meticulosamente construído utilizando as seguintes tecnologias e ferramentas modernas:

* **Backend:**
    * **Linguagem:** C#
    * **Framework:** ASP.NET Core  - Utilizado para construir uma API RESTful de alta performance.
    * **ORM:** Entity Framework Core - Para mapeamento objeto-relacional e interação com o banco de dados.
    * **Autenticação:** ASP.NET Core Identity e JWT Bearer.
* **Banco de Dados:**
    * **PostgreSQL:** Banco de dados relacional robusto, de código aberto e escalável.
* **Frontend:**
    * **Estrutura:** HTML5, CSS3, JavaScript.
* **Controle de Versão:**
    * Git / GitHub

##  Como Configurar e Rodar o Projeto Localmente

Siga estes passos para ter uma cópia local do projeto rodando em sua máquina. Este guia é ideal para avaliação técnica.

### Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas:

* [.NET SDK](https://dotnet.microsoft.com/download) 
* [PostgreSQL](https://www.postgresql.org/download/) (Servidor de banco de dados rodando localmente ou em Docker)
* [Git](https://git-scm.com/downloads)

### Instruções de Instalação e Execução

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/Felpis3196/AionClass.git](https://github.com/Felpis3196/AionClass.git)
    cd AionClass
    ```

2.  **Configurar e Iniciar o Backend (ASP.NET Core API):**
    * Navegue até o diretório do projeto Backend:
        ```bash
        cd AionClass.Backend
        ```
    * Restaure as dependências NuGet:
        ```bash
        dotnet restore
        ```
    * **Configuração do Banco de Dados:**
        * Abra o arquivo `appsettings.json` (ou `appsettings.Development.json`).
        * Atualize a string de conexão `"DefaultConnection"` com os detalhes do seu servidor PostgreSQL local (ex: usuário, senha, nome do banco de dados).
        ```json
        "ConnectionStrings": {
          "DefaultConnection": "Host=localhost;Port=5432;Database=AionClassDb;Username=your_user;Password=your_password"
        },
        "Jwt": {
          "Key": "SuaChaveSecretaMuitoLongaESeguraAqui", // **ATENÇÃO: Use uma chave complexa e real em produção, mas para teste local pode ser provisória.**
          "Issuer": "AionClassIssuer",
          "Audience": "AionClassAudience"
        }
        ```
    * **Executar Migrações do Banco de Dados:**
        * Aplique as migrações do Entity Framework Core para criar o banco de dados e as tabelas necessárias:
            ```bash
            dotnet ef database update
            ```
    * **Iniciar a Aplicação Backend:**
        * Execute a aplicação:
            ```bash
            dotnet run
            ```

Contribuição e Contato

Sua colaboração é muito valorizada! Se você tem interesse em aprimorar este projeto, relatar um bug, sugerir uma funcionalidade ou tem qualquer dúvida, sinta-se à vontade para:

* Abrir uma **Issue** detalhando o problema ou a sugestão.
* Abrir um **Pull Request** com suas implementações, garantindo que as modificações estejam alinhadas com as boas práticas.

**Desenvolvido por:**

* **Felpis3196** - [Perfil no GitHub](https://github.com/Felpis3196)


