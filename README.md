# SKRebirth2

Sistema desktop para cadastro, consulta e gerenciamento de empresas nacionais, internacionais e funcionários, com funcionalidades de backup e restauração de dados. Desenvolvido em C# (.NET Framework 4.7.2) utilizando MaterialSkin para interface moderna e integração com banco de dados MySQL.

## Funcionalidades

- **Cadastro e consulta de empresas nacionais**
- **Cadastro e consulta de empresas internacionais**
- **Cadastro e consulta de funcionários**
- **Validação de CNPJ, CPF e e-mails**
- **Busca automática de endereço via CEP (API ViaCEP)**
- **Envio de e-mail automático com dados de acesso para funcionários**
- **Backup e restauração do banco de dados MySQL**
- **Interface moderna com MaterialSkin**

## Tecnologias Utilizadas

- C# (.NET Framework 4.7.2)
- Windows Forms
- [MaterialSkin](https://github.com/IgnaceMaes/MaterialSkin)
- MySQL (MySql.Data)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)
- API ViaCEP

## Pré-requisitos

- Visual Studio 2022
- MySQL Server
- .NET Framework 4.7.2

## Configuração do Banco de Dados

1. Crie um banco de dados chamado `stucchi` no MySQL.
2. Importe as tabelas necessárias (`empresa_nacional`, `empresa_internacional`, `funcionarios`).
3. Ajuste a string de conexão se necessário (por padrão: `server=localhost; user id=root; password=; database=stucchi`).

## Como Executar

1. Clone o repositório:
