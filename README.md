# Authentication API

## 1. Descrição do Projeto

A **Authentication API** é uma API RESTful desenvolvida em .NET 10 que fornece funcionalidades de autenticação e gerenciamento de usuários. O projeto foi construído seguindo os princípios de Clean Architecture, separando responsabilidades em camadas distintas e utilizando padrões modernos de desenvolvimento.

### Tecnologias Utilizadas

#### Framework e Linguagem
- **.NET 10** - Framework principal
- **C# 14.0** - Linguagem de programação

#### Arquitetura e Padrões
- **Clean Architecture** - Separação em camadas (Domain, Application, Infrastructure, API)
- **CQRS Pattern** - Utilizando MediatR para separação de comandos e queries
- **Repository Pattern** - Abstração de acesso a dados
- **Pipeline Behavior** - Para validação automática de requests

#### Bibliotecas e Pacotes

**API Layer:**
- `Microsoft.AspNetCore.OpenApi` (10.0.1) - Geração de especificação OpenAPI
- `Scalar.AspNetCore` (1.2.60) - Documentação interativa da API
- `MediatR` (12.4.1) - Implementação do padrão Mediator

**Application Layer:**
- `MediatR` (12.4.1) - Orquestração de comandos e queries
- `FluentValidation` (11.9.2) - Validação de dados
- `FluentValidation.DependencyInjectionExtensions` (11.9.2) - Integração com DI
- `BCrypt.Net-Next` (4.0.3) - Hashing de senhas

**Infrastructure Layer:**
- `MongoDB.Driver` (2.28.0) - Cliente MongoDB para persistência
- `BCrypt.Net-Next` (4.0.3) - Criptografia de senhas
- `Microsoft.Extensions.Options.ConfigurationExtensions` (8.0.0) - Configurações

#### Banco de Dados
- **MongoDB** - Banco de dados NoSQL para persistência

### Estrutura do Projeto

```
src/
├── Auth.Api/                    # Camada de apresentação (Controllers, Middleware)
│   ├── Controllers/
│   │   └── v1/
│   │       └── AuthenticationController.cs
│   ├── Configuration/
│   │   └── Bootstrapper.cs     # Configuração de DI
│   ├── Middleware/
│   │   └── ExceptionHandlingMiddleware.cs
│   └── Program.cs
│
├── Auth.Application/            # Camada de aplicação (Casos de uso, DTOs)
│   ├── Commands/
│   │   └── Register/
│   │       ├── RegisterUserCommandHandler.cs
│   │       └── RegisterUserCommandValidator.cs
│   ├── Behaviors/
│   │   └── ValidationBehavior.cs
│   ├── DTOs/
│   ├── Interfaces/
│   └── Constants/
│
├── Auth.Domain/                 # Camada de domínio (Entidades, Interfaces)
│   ├── Entities/
│   │   ├── User.cs
│   │   └── Interface/
│   ├── Commands/
│   │   └── Register/
│   ├── Interfaces/
│   ├── Enums/
│   └── Exceptions/
│
├── Auth.Infrastructure/         # Camada de infraestrutura (Persistência, Serviços)
│   ├── Persistence/
│   │   └── MongoDbContext.cs
│   ├── Repositories/
│   │   ├── BaseRepository.cs
│   │   └── UserRepository.cs
│   ├── Services/
│   │   └── EncryptionService.cs
│   └── Configuration/
│       └── MongoDbSettings.cs
│
└── Auth.Test/                   # Testes unitários e de integração
```

### Funcionalidades Atuais (v1)

- ✅ Registro de usuários com validação completa
- ✅ Criptografia de senhas com BCrypt
- ✅ Validação de dados com FluentValidation
- ✅ Tratamento global de exceções
- ✅ Documentação automática com Scalar/OpenAPI
- ✅ Persistência em MongoDB

---

## 2. Como Utilizar o Projeto

### Pré-requisitos

1. **.NET 10 SDK** instalado
   ```bash
   # Verificar instalação
   dotnet --version
   ```

2. **MongoDB** instalado e em execução
   - Download: https://www.mongodb.com/try/download/community
   - Ou via Docker:
   ```bash
   docker run -d -p 27017:27017 --name mongodb mongo:latest
   ```

3. **IDE** (opcional, mas recomendado)
   - Visual Studio 2022 (17.12 ou superior)
   - Visual Studio Code com extensão C#
   - JetBrains Rider

### Passos para Configuração

#### 1. Clone o Repositório
```bash
git clone https://github.com/CLJmellem/AuthenticationAPI.git
cd AuthenticationAPI
```

#### 2. Configure o MongoDB

Edite o arquivo `src/Auth.Api/appsettings.json` com suas configurações:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "FinanceProject"
  }
}
```

**Nota:** Para ambientes de produção, utilize variáveis de ambiente ou Azure Key Vault para armazenar credenciais.

#### 3. Restaure as Dependências
```bash
cd src
dotnet restore
```

#### 4. Execute a Aplicação
```bash
cd Auth.Api
dotnet run
```

A API estará disponível em:
- HTTP: `http://localhost:5145`
- HTTPS: `https://localhost:7035`

#### 5. Acesse a Documentação Interativa

**Scalar (Recomendado):**
```
https://localhost:7035/scalar/v1
```

**OpenAPI Spec:**
```
https://localhost:7035/openapi/v1.json
```

### Executando com Perfis Específicos

**Perfil HTTP:**
```bash
dotnet run --launch-profile http
```

**Perfil HTTPS:**
```bash
dotnet run --launch-profile https
```

**Perfil Scalar (com navegador):**
```bash
dotnet run --launch-profile scalar
```

### Executando os Testes
```bash
cd src/Auth.Test
dotnet test
```

### Configuração para Desenvolvimento

#### Variáveis de Ambiente (Opcional)
Crie um arquivo `appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information"
    }
  },
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "FinanceProject_Dev"
  }
}
```

---

## 3. Detalhes da Implementação

### Arquitetura

O projeto segue os princípios de **Clean Architecture**, dividido em 4 camadas:

#### 1. **Auth.Domain** (Camada de Domínio)
- Contém as entidades de negócio, interfaces de repositório e exceções customizadas
- Não possui dependências de outras camadas
- Define os contratos (interfaces) que serão implementados nas camadas externas

#### 2. **Auth.Application** (Camada de Aplicação)
- Contém os casos de uso (Commands/Queries)
- Implementa validações com FluentValidation
- Define DTOs para comunicação com a camada de apresentação
- Usa MediatR para implementar o padrão CQRS

#### 3. **Auth.Infrastructure** (Camada de Infraestrutura)
- Implementa os repositórios e acesso ao MongoDB
- Contém serviços de criptografia
- Gerencia a configuração de banco de dados

#### 4. **Auth.Api** (Camada de Apresentação)
- Expõe os endpoints REST
- Contém middleware de tratamento de exceções
- Configura injeção de dependências
- Gera documentação OpenAPI/Scalar

### Pipeline de Validação

O projeto utiliza um **ValidationBehavior** que intercepta todas as requisições do MediatR e aplica validações automaticamente:

```
Request → ValidationBehavior → Validators → Handler → Response
```

Se a validação falhar, uma `ValidationException` é lançada e capturada pelo middleware de exceções.

### Tratamento de Exceções

O `ExceptionHandlingMiddleware` captura e trata exceções globalmente:

| Exceção | Status Code | Descrição |
|---------|-------------|-----------|
| `ValidationException` | 400 Bad Request | Erros de validação do FluentValidation |
| `InvalidUserDataException` | 400 Bad Request | Dados de usuário inválidos |
| `UserAlreadyExistsException` | 409 Conflict | Usuário já existe (email ou username duplicado) |
| Outras exceções | 500 Internal Server Error | Erros não tratados |

### Endpoint: Registro de Usuário (v1)

#### POST `/api/v1/authentication/register`

Registra um novo usuário no sistema.

**Request Body:**
```json
{
  "username": "joaosilva",
  "email": "joao.silva@example.com",
  "password": "Senha@123",
  "confirmPassword": "Senha@123"
}
```

**Regras de Validação:**

**Username:**
- Obrigatório
- Mínimo de 3 caracteres
- Máximo de 20 caracteres
- Apenas letras, números e underscore (_)

**Email:**
- Obrigatório
- Formato de email válido
- Máximo de 100 caracteres

**Password:**
- Obrigatório
- Mínimo de 8 caracteres
- Deve conter pelo menos 1 letra maiúscula
- Deve conter pelo menos 1 letra minúscula
- Deve conter pelo menos 1 número
- Deve conter pelo menos 1 caractere especial

**ConfirmPassword:**
- Obrigatório
- Deve ser igual ao campo Password

**Respostas:**

**201 Created** - Usuário criado com sucesso
```json
{
  "message": "User registered successfully"
}
```

**400 Bad Request** - Erros de validação
```json
{
  "statusCode": 400,
  "message": "Validation errors",
  "errors": [
    "Username must have at least 3 characters.",
    "Password must contain at least one uppercase letter."
  ]
}
```

**409 Conflict** - Usuário já existe
```json
{
  "statusCode": 409,
  "message": "Email 'joao.silva@example.com' already in use by another user."
}
```

**500 Internal Server Error** - Erro interno
```json
{
  "statusCode": 500,
  "message": "Internal server error"
}
```

### Exemplos de Requisições cURL

#### Registro com Sucesso

```bash
curl -X POST https://localhost:7035/api/v1/authentication/register \
  -H "Content-Type: application/json" \
  -d '{
    "username": "joaosilva",
    "email": "joao.silva@example.com",
    "password": "Senha@123",
    "confirmPassword": "Senha@123"
  }'
```

### Segurança

1. **Hashing de Senhas:** BCrypt com Enhanced Hashing
2. **Validação Robusta:** FluentValidation com múltiplas regras
3. **Middleware de Exceções:** Não expõe detalhes internos em produção
4. **HTTPS:** Configurado por padrão para ambientes de produção

### Próximas Funcionalidades (Roadmap)

- [ ] Endpoint de Login com JWT
- [ ] Refresh Token
- [ ] Confirmação de email
- [ ] Recuperação de senha
- [ ] Autenticação de dois fatores (2FA)
- [ ] Rate limiting
- [ ] Logging estruturado (Serilog)

---

## Contribuindo

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

---

## Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

---

## Contato

- GitHub: [@CLJmellem](https://github.com/CLJmellem)
- Repositório: [AuthenticationAPI](https://github.com/CLJmellem/AuthenticationAPI)

---

**Última atualização:** Janeiro 2024  
**Versão da API:** v1
