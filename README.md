# Authentication API

Uma API RESTful de autenticação construída com .NET 10, seguindo princípios de Clean Architecture e utilizando MongoDB como banco de dados.

[![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14.0-239120?logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![MongoDB](https://img.shields.io/badge/MongoDB-6.0-47A248?logo=mongodb)](https://www.mongodb.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

---

## 🚀 Início Rápido

### Pré-requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [MongoDB](https://www.mongodb.com/try/download/community) (ou Docker)

### Instalação em 3 Passos

```bash
# 1. Clone o repositório
git clone https://github.com/CLJmellem/AuthenticationAPI.git
cd AuthenticationAPI

# 2. Configure o MongoDB (opcional se estiver usando padrões locais)
# Edite src/Auth.Api/appsettings.json se necessário

# 3. Execute a aplicação
cd src/Auth.Api
dotnet run
```

### MongoDB via Docker (Recomendado)

```bash
docker run -d -p 27017:27017 --name mongodb mongo:latest
```

### Acesse a Documentação Interativa

Após iniciar a aplicação, acesse:
```
https://localhost:7035/scalar/v1
```

---

## 📋 Funcionalidades

### ✅ Implementado (v1)
- Registro de usuários com validação robusta
- Criptografia de senhas (BCrypt)
- Validação automática com FluentValidation
- Tratamento global de exceções
- Documentação interativa (Scalar/OpenAPI)
- Persistência em MongoDB

### 🔄 Em Desenvolvimento
- Login com JWT
- Refresh Token
- Confirmação de email
- Recuperação de senha

---

## 🏗️ Arquitetura

O projeto segue **Clean Architecture** com 4 camadas:

```
┌─────────────────────────────────────┐
│   Auth.Api (Apresentação)           │
│   Controllers, Middleware           │
└───────────────┬─────────────────────┘
                │
┌───────────────▼─────────────────────┐
│   Auth.Application (Aplicação)      │
│   Commands, Validators, DTOs        │
└───────────────┬─────────────────────┘
                │
┌───────────────▼─────────────────────┐
│   Auth.Domain (Domínio)             │
│   Entities, Interfaces              │
└───────────────┬─────────────────────┘
                ▲
┌───────────────┴─────────────────────┐
│   Auth.Infrastructure               │
│   Repositories, Services            │
└─────────────────────────────────────┘
```

**Padrões Utilizados:**
- ✅ Clean Architecture
- ✅ CQRS (MediatR)
- ✅ Repository Pattern
- ✅ Pipeline Behavior

---

## 🛠️ Stack Tecnológico

| Categoria | Tecnologia |
|-----------|------------|
| **Framework** | .NET 10, C# 14.0 |
| **Banco de Dados** | MongoDB 6.0+ |
| **Padrões** | CQRS, Repository, Clean Architecture |
| **Validação** | FluentValidation 11.9 |
| **Mediator** | MediatR 12.4 |
| **Documentação** | Scalar, OpenAPI 3.0 |
| **Segurança** | BCrypt.Net-Next 4.0 |

---

## 📚 Documentação Completa

Acesse a [**Wiki**](docs/wiki/HOME.md) para documentação detalhada:

### Primeiros Passos
- 📖 [**Guia de Instalação**](docs/wiki/INSTALLATION.md) - Setup completo passo a passo
- 🎯 [**Início Rápido**](docs/wiki/QUICK_START.md) - Primeiros comandos (em breve)

### Arquitetura e Design
- 🏛️ [**Visão Geral da Arquitetura**](docs/wiki/ARCHITECTURE.md) - Clean Architecture detalhada
- 💻 [**Tecnologias Utilizadas**](docs/wiki/TECHNOLOGIES.md) - Stack técnico completo
- 🎨 [**Padrões de Design**](docs/wiki/DESIGN_PATTERNS.md) - CQRS, Repository, etc. (em breve)

### Referência da API
- 🔌 [**Endpoints**](docs/wiki/API_ENDPOINTS.md) - Documentação completa com exemplos cURL
- 📝 [**Exemplos de Requisições**](docs/wiki/API_EXAMPLES.md) - Postman, JavaScript, Python (em breve)
- 📊 [**Modelos de Dados**](docs/wiki/DATA_MODELS.md) - Entidades e DTOs (em breve)

---

## 🔥 Exemplo de Uso

### Registrar um Novo Usuário

**Request:**
```bash
curl -X POST https://localhost:7035/api/v1/authentication/register \
  -H "Content-Type: application/json" \
  -d '{
    "username": "john_doe",
    "email": "john@example.com",
    "password": "Senha@123",
    "confirmPassword": "Senha@123"
  }'
```

**Response (201 Created):**
```json
{
  "message": "User registered successfully"
}
```

**Mais exemplos:** [API Endpoints](docs/wiki/API_ENDPOINTS.md)

---

## 🧪 Testes

```bash
cd src/Auth.Test
dotnet test
```

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Por favor:

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

Veja [CONTRIBUTING.md](docs/wiki/CONTRIBUTING.md) para mais detalhes (em breve).

---

## 📝 Roadmap

### v1.1 (Em Breve)
- [ ] Login com JWT
- [ ] Refresh Token
- [ ] Logout

### v1.2 (Planejado)
- [ ] Confirmação de email
- [ ] Recuperação de senha
- [ ] Perfil de usuário

### v2.0 (Futuro)
- [ ] Autenticação de dois fatores (2FA)
- [ ] OAuth2 / OpenID Connect
- [ ] Rate limiting
- [ ] Logging estruturado (Serilog)

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 📞 Contato

**Autor:** [@CLJmellem](https://github.com/CLJmellem)

**Issues:** [Reportar problema](https://github.com/CLJmellem/AuthenticationAPI/issues)

**Documentação:** [Wiki Completa](docs/wiki/HOME.md)

---

## ⭐ Mostre seu Apoio

Se este projeto foi útil, considere dar uma ⭐ no repositório!

---

**Última atualização:** Janeiro 2024 | **Versão:** v1.0
