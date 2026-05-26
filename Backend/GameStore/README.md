## 🚀 Tecnologias

### Backend
- [.NET](https://dotnet.microsoft.com/) — Web API
- [Entity Framework Core](https://docs.microsoft.com/ef/core/) — ORM
- [SQLite](https://www.sqlite.org/) — Banco de dados

---

## 📁 Estrutura do Projeto 
Clean architecture

```
GameStoreProject/
├── Backend/
│   └── GameStore/
│       ├── GameStore.API/            # Controllers, configuração do projeto e da classe de iniciação (Program.cs)
│       ├── GameStore.Application/    # DTO, Services (Interface e Classe), Validators, Paginação, Dependency Injection (IService e Service)
│       ├── GameStore.Domain/         # Modelos de domínio, Interfaces de repositórios
│       └── GameStore.Infrastructure/ # Criação do banco de dados, Repositórios (Interface e Classe), Migrations, Dependency Injection (IRepository e Repository)
```
---

## ⚙️ Pré-requisitos
- [.NET SDK 8+](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)

---

## 🌍 Variáveis de Ambiente
- Crie a variável de ambiente de string de conexão com o banco de dados SQLite dentro do sistema e configure na classe Dependeny Injection dentro da camada Infrastructure

```
var connectionString =
                Environment.GetEnvironmentVariable("SUA_STRING_DE_CONEXAO", EnvironmentVariableTarget.Machine)
                    ?? throw new ArgumentException("Invalid Connection String!!!");

           services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

```


## 🔧 Como rodar localmente
- Realizar a instalação das dependências e aplicar as migrations via CLI
  
```bash
# Entrar na pasta do projeto e baixar as dependências
cd .\Backend\GameStore\
dotnet restore

# Entrar na camada GameStore (raiz) e aplicar
dotnet ef database update --project GameStore.Infrastructure --startup-project GameStore.API

# Iniciar projeto na camada GameStore.API
cd GameStore.API
dotnet run
```

A API estará disponível em: `http://localhost:5046`

Documentação Swagger: `http://localhost:5046/swagger`
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

## 👤 Autor

Feito por **[Seu Nome](https://github.com/seu-usuario)**
