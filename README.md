# Clean Architecture .NET Template

## 📘 Descrição

Este template fornece uma estrutura inicial para projetos .NET seguindo os princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**.  
Ele é ideal para o desenvolvimento de APIs Web modernas, oferecendo uma organização clara de camadas e uma base sólida para aplicações empresariais escaláveis.

---

## 🚀 Características Principais

### 🧱 Estrutura em Camadas Limpas

- **Application**: Contém a lógica de casos de uso  
- **Domain**: Modelos de domínio e contratos  
- **Infrastructure**: Implementações de infraestrutura  
- **WebApi**: Camada de apresentação (API REST)


### 🧰 Tecnologias Base

- .NET Core  6+ / 7+  
- ASP.NET Core Web API  
- Suporte a containers **Docker**

---

## 🏁 Como Começar

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/genSouza/DotNet.CleanArch.Template

   cd seu-repositorio
2. **Instalação:**

   ```bash
   dotnet new install DotNet.CleanArch.Template
3. **Criar um Novo Projeto:**
   ```bash
   dotnet new install DotNet.CleanArch.Template
4. **Estrutura Gerada:**
   ```
   src/
   ├── SeuProjeto.Application/     # Casos de uso e serviços
   ├── SeuProjeto.Domain/          # Entidades e contratos
   ├── SeuProjeto.Infrastructure/  # Implementações
   └── SeuProjeto.WebApi/          # API HTTP
   ```

## 🛠️ Configuração

1. **Defina seu nome de projeto:**

   ```bash
   -n ou --name: Especifica o nome do projeto (padrão: 'MyApp')
2. **Personalização:**

   * Edite os arquivos de configuração em src/SeuProjeto.WebApi/appsettings.json
   * Configure os serviços em ApplicationServiceRegistration.cs e PersistenceServiceRegistration.cs


## 📄 Requisitos

   * NET SDK 6.0 ou superior
   * Visual Studio 2022 ou VS Code (recomendado)

## 🗺️ Roadmap

    * Incluir teste unitários básicos
    * Adicionar configuração para CQRS
    * Suporte para GraphQL

## 👥 Contribuição

Contribuições são bem-vindas! Siga os passos:

   1. Faça um fork do repositório

   2. Crie sua feature branch (git checkout -b feature/fooBar)

   3. Commit suas mudanças (git commit -am 'Add some fooBar')

   4. Push para a branch (git push origin feature/fooBar)

   5. Crie um novo Pull Request

## 📜 Licença

MIT License - veja o arquivo LICENSE para detalhes.

Nota: Este template é mantido por [Genilton Souza] e pela comunidade de contribuidores.