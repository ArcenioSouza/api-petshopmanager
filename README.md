# 🎯 DesafioPetShopManager

Este projeto é uma WEB API de gerenciamento de PetShop.

Com ela você poderá consultar, cadastrar, editar e excluir dados de clientes, animais e veterinários além de obter descrições de consultas realizadas e um registro de todos os atendimentos do seu empreendimento.

Esse sistema ainda conta com serviços de segurança de acesso, para consumir dados da api é necessário estar registrado e logado, além de estar identificado através de um token gerado pelo sistema JWT.

<br>

# 🧰 Como utilizar

1 - Abra seu terminal e clone o repositório usando esse comando:
- git clone ...

2 - Entre na pasta do projeto clonado:
- cd desafiopetshopmanager

3 - Dentro da pasta do projeto, execute esses dois comandos: 
- dotnet ef database update (Carrega o migration criando e populando o banco de dados) 
- dotnet run --project PetShopManager (Executa a aplicação)

Para rodar os testes, execute esse comando:
- dotnet test

Nosso banco de dados já possui dois usuários cadastrados com diferentes permissões:

Administrador: admin@gft.com

Senha: Gft@1234

Tem acesso a todos os métodos da API

Cliente: user@gft.com

Senha: Gft@1234

Tem permissão para ...

<br>

# 🛠 Ferramentas utilizadas

.Net 5.0;

ASP NET Core MVC;

ASP NET Cora WEB API;

EntityFrameworkCore;

Visual Studio Code;

xUnit.

<br>

# 📖 Aprendizados

- [x] Conceitos de arquitetura MVC;

- [x] CRUD com Pomelo.EntityFrameworkCore em banco MySQL;

- [x] Customização de migrations para popular banco de dados;

- [x] Uso de DataAnotation para validação de dados;

- [x] Criar e manipular banco de dados com relação entre entidades;

- [x] Criar rotas para acesso a dados do sistema;

- [x] Criar sistema de autenticação por tokens JWT;

- [x] Criar testes unitários usando banco de dados mockado;

<br>

# Imagens do processo de organização e criação do projeto

<details>
<summary>🖼️ Backlog do projeto</summary>
<img src="./assets/img/Trello1.JPG" width="900px">
</details>