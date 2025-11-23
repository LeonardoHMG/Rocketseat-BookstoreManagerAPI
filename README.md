# 📚 Gerenciador de Livraria – API REST em .NET

![.NET](https://img.shields.io/badge/.NET-8-purple)  
![C#](https://img.shields.io/badge/C%23-API-green)  
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-blue)  

Este projeto é uma **API REST completa em .NET**, criada como parte dos estudos da formação C# da **Rocketseat**.  
A API gerencia livros de uma livraria, implementando **CRUD completo**, **validações**, **herança no domínio**, tratamento de erros e **documentação via Swagger**.

---

## 🚀 Funcionalidades

A API permite:

1. **📕 Criar um livro**
2. **📚 Listar todos os livros** (com filtros opcionais)
3. **🔍 Buscar livro por ID**
4. **✏️ Atualizar informações de um livro**
5. **🗑️ Excluir um livro**

---

## 📌 Regras e Validações

### 🧾 Campos Obrigatórios

| Campo     | Tipo    | Obrigatório | Validações |
|----------|---------|-------------|------------|
| `id`     | GUID    | Sim         | Gerado automaticamente |
| `title`  | string  | Sim         | 2 a 120 caracteres |
| `author` | string  | Sim         | 2 a 120 caracteres |
| `genre`  | string  | Sim         | Deve estar entre os gêneros válidos |
| `price`  | decimal | Sim         | ≥ 0 |
| `stock`  | int     | Sim         | ≥ 0 |

### 🧠 Regras de Negócio

- `title` e `author` **não podem ser duplicados**  
- `price` **não pode ser negativo**  
- `stock` **não pode ser negativo**  
- `genre` deve estar numa lista de gêneros permitidos  
- Ao criar um livro → preencher `CreatedAt`  
- Ao atualizar → atualizar `UpdatedAt`

---

## 🔗 Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| **POST** | `/api/books` | Criar um novo livro |
| **GET** | `/api/books` | Listar todos os livros |
| **GET** | `/api/books/{id}` | Buscar livro pelo ID |
| **PUT** | `/api/books/{id}` | Atualizar um livro |
| **DELETE** | `/api/books/{id}` | Excluir um livro |

---

## 🔄 Status Codes

| Status | Uso |
|--------|-----|
| **200** | Consultas e atualizações |
| **201** | Recurso criado |
| **204** | Atualização ou exclusão sem retorno |
| **400** | Dados inválidos |
| **404** | Recurso não encontrado |
| **409** | Conflito (duplicidade) |
| **500** | Erro inesperado |

---

## 🛠 Tecnologias Utilizadas

- **.NET 8**
- **C#**
- **ASP.NET Web API**
- **Swagger / OpenAPI**
- **Validações com Data Annotations**
- **Padrões de domínio com herança**

---

## 📂 Estrutura do Projeto 
```
Rocketseat-BookstoreManagerAPI/
│
├── Controllers/
│ └── BooksController.cs
| └── BookstoreManagerAPIBaseController.cs
|
|── Communication/
│ └── Requests/
|     └── RequestRegisterBookJson.cs
|     └── RequestUpdateBookJson.cs
|
| └── Responses/
|     └── ErrorResponse.cs
|     └── ResponseRegisteredBookJson.cs
│
├── Models/
│ ├── BaseModel.cs
│ └── Book.cs
| └── Genre.cs
│
├── Services/
│ └── BookService.cs
│
├── Program.cs
├── appsettings.json
└── README.md
```

