# 🎮 GameStore

Aplicação fullstack para gerenciamento e exibição de jogos digitais simulando a Epic Games.

---

## 🚀 Tecnologias

### Frontend

- [Next.js](https://nextjs.org/)
- [TypeScript](https://www.typescriptlang.org/)
- [Tailwind CSS](https://tailwindcss.com/)

### Backend

- [C# .NET](https://dotnet.microsoft.com/) — Web API
- [Entity Framework Core](https://docs.microsoft.com/ef/core/) — ORM

### Banco de Dados

- [SQLite](https://www.sqlite.org/)
- [DBeaver](https://dbeaver.io/download/)

---

## 📁 Estrutura do Projeto

```
GameStoreProject/
├── Frontend/
│   └── GameStore/                    # Aplicação Next.js (páginas, componentes e estilos)
│       ├── public/                   # Imagens dos jogos
│       └── src/
│           ├── app/                  # Rotas e páginas da aplicação
│           ├── Components/           # Componentes reutilizáveis
│           └── lib/                  # Utilitários
└── Backend/
    └── GameStore/                    # Clean architecture
        ├── GameStore.API/            # Controllers, configuração do projeto e da classe de iniciação (Program.cs)
        ├── GameStore.Application/    # DTO, Services (Interface e Classe), Validators, Paginação, Dependency Injection (IService e Service)
        ├── GameStore.Domain/         # Modelos de domínio, Interfaces de repositórios
        └── GameStore.Infrastructure/ # Criação do banco de dados, Repositórios (Interface e Classe), Migrations, Dependency Injection (IRepository e Repository)
```

---

## ⚙️ Pré-requisitos

- [NodeJS](https://nodejs.org/) ( 24.15.0 )
- [Npm](https://www.npmjs.com/) ( 11.13.0 + )
- [.NET SDK 8+](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)
- [DBeaver](https://dbeaver.io/download/) ( opcional, para visualizar o banco )

---

## 🌍 Variáveis de Ambiente

A API lê a string de conexão do banco SQLite da variável de ambiente `SQLITE_CONNECTION`. Ela precisa estar definida **no mesmo terminal** em que você executar as migrations e rodar a API, caso contrário a aplicação não inicia com o erro `Invalid Connection String!!!`.

- No PowerShell (válida somente na sessão atual do terminal):

```powershell
$env:SQLITE_CONNECTION = "Data Source=GameStore.db"
```

- No PowerShell (permanente no sistema, exige abrir um novo terminal após definir):

```powershell
setx SQLITE_CONNECTION "Data Source=GameStore.db"
```

> **Dica:** o valor `Data Source=GameStore.db` cria o arquivo do banco **na pasta onde os comandos são executados**. Por isso, no passo a passo abaixo tudo é executado a partir de `GameStore.API` — assim a migration e a API sempre usam o mesmo arquivo.

---

## 🔧 Como rodar localmente

### 1. Backend

> **Pré-requisito único:** ter o .NET SDK 8+ instalado. O `dotnet-ef` (ferramenta do Entity Framework) só precisa ser instalado uma vez, caso ainda não esteja:

```bash
# Instalar o EF Core versão 8 (apenas se ainda não instalado)
dotnet tool install --global dotnet-ef --version 8.0.8

# Caso já esteja instalado, apenas atualize para a versão 8.0.8
# dotnet tool update --global dotnet-ef --version 8.0.8

# Conferir se está instalado
dotnet ef --version
```

Passo a passo para subir a API (tudo a partir da pasta `GameStore.API` — é lá que o arquivo `GameStore.db` será criado e lido):

```bash
# 1) Entrar na pasta da API
cd Backend\GameStore\GameStore.API

# 2) Definir a variável de ambiente da string de conexão (no MESMO terminal; a cada novo terminal)
$env:SQLITE_CONNECTION = "Data Source=GameStore.db"

# 3) Baixar as dependências (restaura a API e todas as camadas do projeto)
dotnet restore

# 4) Aplicar as migrations — cria o banco GameStore.db já populado com os jogos
dotnet ef database update --project ..\GameStore.Infrastructure --startup-project .

# 5) Iniciar a API
dotnet run
```

A API estará disponível em: `http://localhost:5046`

Documentação Swagger: `http://localhost:5046/swagger`

> **Dica:** para verificar rapidamente se o banco subiu com os jogos, acesse `http://localhost:5046/games` no navegador — deve retornar a lista paginada com os jogos do seed.

### 2. Frontend

```bash
# Em outro terminal, entrar na pasta do frontend
cd Frontend\GameStore

# Instalar as dependências
npm install

# Iniciar a aplicação
npm run dev
```

O frontend estará disponível em: `http://localhost:3000`

> **Dica:** deixe a API rodando no primeiro terminal e o frontend no segundo.

---

## 🗑️ Reset do banco de dados

Para recriar o banco do zero (por exemplo, após alterar o modelo ou apagar dados por engano):

```bash
cd Backend\GameStore\GameStore.API

# Encerrar a API (Ctrl+C) e apagar o arquivo do banco
Remove-Item GameStore.db

# Recriar o banco aplicando todas as migrations novamente
dotnet ef database update --project ..\GameStore.Infrastructure --startup-project .
```

---

## 🧰 Solução de problemas

| Erro / Sintoma | Causa | Solução |
|---|---|---|
| `Invalid Connection String!!!` | Variável `SQLITE_CONNECTION` não definida no terminal atual | Rodar `$env:SQLITE_CONNECTION = "Data Source=GameStore.db"` no mesmo terminal antes de migrar/rodar |
| `dotnet ef` não é um comando reconhecido | Ferramenta `dotnet-ef` não instalada | `dotnet tool install --global dotnet-ef --version 8.0.8` e abrir um novo terminal |
| Erro de build (`NETSDK`, `TargetFramework net8.0`) | Versão do .NET SDK incompatível | Instalar o [.NET SDK 8+](https://dotnet.microsoft.com/download) |
| API inicia, mas `GET /Games` retorna lista vazia | Banco foi criado em outra pasta (migration rodada fora de `GameStore.API`) | Apagar `GameStore.db` e refazer a migration **a partir da pasta `GameStore.API`** |
| Porta `5046` já em uso | Outro processo ocupando a porta | Encerrar o processo anterior (`Ctrl+C`) ou trocar de porta |
| Swagger não abre | API rodando em ambiente `Production` | Rodar via `dotnet run` (usa o perfil `http`, que ativa o Swagger em Development) |

---

## 📌 Endpoints da API

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/Games` | Lista todos os jogos paginados |
| GET | `/Games/{id}` | Busca jogo por ID |
| GET | `/Games/{gameName}` | Busca jogo por Nome |
| POST | `/Games` | Cria um novo jogo |
| PUT | `/Games/{id}` | Atualiza um jogo |
| DELETE | `/Games/{id}` | Remove jogo |
| PATCH | `/Games/{id}` | Atualiza somente os campos desejados |

---

## Observações

- As imagens dos jogos estarão salvas na pasta public/ do front end, logo o caminho a ser usado para a criação dos jogos
deverá ser: "./Nome.extensão". Ex: "./Undertale.jpg"

- Ao utilizar a rota PATCH, devemos passar somente os campos que desejamos modificar no body (senão irá alterar todos).
  Ex: Quero alterar o status do jogo para instalado
  ```
  Estrutura do body:

  {
    "isInstalled": true
  }
  ```

---

## 👤 Autor

Feito por **[Antônio Pedro](https://github.com/Tun1n)**
