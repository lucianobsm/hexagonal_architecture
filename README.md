# 📐 Arquitetura Hexagonal: Portas e Adaptadores

Abaixo está a representação visual da estrutura do sistema, dividida entre o ecossistema externo e o núcleo de domínio isolado.

```text
┌──────────────────────────────────────────────────────────────────┐
│                 EXTERIOR (Infrastructure / API)                  │
│                                                                  │
│   ┌────────────────────────┐          ┌──────────────────────┐   │
│   │    INBOUND ADAPTERS    │          │  OUTBOUND ADAPTERS   │   │
│   │       (Drivers)        │          │       (Driven)       │   │
│   │                        │          │                      │   │
│   │  ┌──────────────────┐  │          │  ┌────────────────┐  │   │
│   │  │  MySolution.API  │  │          │  │ Infrastructure │  │   │
│   │  │   (Controllers)  │  │          │  │  (Banco/SQL)   │  │   │
│   │  └────────┬─────────┘  │          │  └───────▲────────┘  │   │
│   └───────────┼────────────┘          └──────────┼───────────┘   │
└───────────────┼──────────────────────────────────┼───────────────┘
                │                                  │
                │ Fluxo de                         │ Implementa
                │ Execução                         │ a Porta
                ▼                                  │
┌──────────────────────────────────────────────────┼───────────────┐
│               NÚCLEO CENTRAL (MySolution.Domain) │               │
│                                                  │               │
│      ┌───────────────────────────────────────────┴──────────┐    │
│      │                    PORTS                             │    │
│      │                                                      │    │
│      │  [Inbound Ports]               [Outbound Ports]      │    │
│      │  (Interfaces de Entrada)       (Interfaces de Saída) │    │
│      └────────▲──────────────────────────────────┬──────────┘    │
│               │                                  │               │
│               │ Aciona                           │ Chama         │
│               ▼                                  ▼               │
│      ┌──────────────────────────────────────────────────────┐    │
│      │                  USE CASES                           │    │
│      │            (Casos de Uso / Regras)                   │    │
│      └────────────────────────┬─────────────────────────────┘    │
│                               │                                  │
│                               ▼                                  │
│      ┌──────────────────────────────────────────────────────┐    │
│      │                  ENTITIES                            │    │
│      │             (Modelos de Domínio)                     │    │
│      └──────────────────────────────────────────────────────┘    │
│                                                                  │
└──────────────────────────────────────────────────────────────────┘
```
```text
hexagonal_architecture.sln
│
├── 📁 src/
│   │
│   ├── 🔌 Adapters/
│   │   ├── 🌐 API/                     (Web API - ASP.NET Core)
│   │   │   └── [Controllers e Configurações de Entrada]
│   │   │
│   │   └── 🛠️ Infrastructure/          (Class Library - .NET Core)
│   │       └── Adapters/
│   │           └── Outbound/
│   │               └── UserRepository.cs
│   │
│   └── 🧠 core/
│       ├── ⚙️ Application/             (Class Library - .NET Core)
│       │   ├── Ports/
│       │   │   ├── Inbound/
│       │   │   │   └── ICreateUserUseCase.cs
│       │   │   └── Outbound/
│       │   │       └── Repository/
│       │   │           └── IUserRepository.cs
│       │   └── UseCases/
│       │       └── CreateUserUseCase.cs
│       │
│       └── 💎 Domain/                  (Class Library - .NET Core)
│           └── Entities/
│               └── User.cs
│
└── 🧪 Tests/                           (Projeto de Testes)
```

Configurando as Referências de Projeto
Para que esse hexágono funcione perfeitamente e o isolamento seja real, as referências entre os projetos (as dependências) devem ser configuradas assim:

Domain: Não referencia nenhum outro projeto.

Application: Referencia apenas o Domain.

Infrastructure: Referencia o Application (para poder enxergar e implementar a interface IUserRepository).

API: Referencia o Application (para chamar os casos de uso) e o Infrastructure (apenas para registrar a injeção de dependência no início do app).