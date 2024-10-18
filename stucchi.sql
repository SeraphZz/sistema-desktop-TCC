-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 07/10/2024 às 05:58
-- Versão do servidor: 10.4.32-MariaDB
-- Versão do PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `stucchi`
--

-- --------------------------------------------------------

--
-- Estrutura para tabela `empresa_internacional`
--

CREATE TABLE `empresa_internacional` (
  `nome_empresa` varchar(50) NOT NULL,
  `nome_representante` varchar(50) NOT NULL,
  `setor_atuacao` varchar(50) NOT NULL,
  `email_empresa` varchar(50) NOT NULL,
  `email_representante` varchar(50) NOT NULL,
  `telefone_empresa` varchar(20) NOT NULL,
  `ruc` varchar(38) NOT NULL,
  `rut` varchar(18) NOT NULL,
  `pais` varchar(30) NOT NULL,
  `codigo_postal` varchar(15) NOT NULL,
  `cidade` varchar(50) NOT NULL,
  `bairro` varchar(50) NOT NULL,
  `rua` varchar(50) NOT NULL,
  `numero` varchar(10) NOT NULL,
  `complemento` varchar(10) NOT NULL,
  `classificacao` varchar(10) NOT NULL,
  `data_de_contato` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `empresa_internacional`
--

INSERT INTO `empresa_internacional` (`nome_empresa`, `nome_representante`, `setor_atuacao`, `email_empresa`, `email_representante`, `telefone_empresa`, `ruc`, `rut`, `pais`, `codigo_postal`, `cidade`, `bairro`, `rua`, `numero`, `complemento`, `classificacao`, `data_de_contato`) VALUES
('OVO Sound', 'Drake', 'Música e Entretenimento', 'contact@ovosound.au', 'drake@ovosound.au', '+61 2 9876 5432', '4234567890123', '6876543210987', 'Austrália', '1233214', 'Sydney', 'Darling Harbor', 'King Street', '123', 'B2', 'OEM', '2024-12-06'),
('G.O.O.D Music', 'Kanye West', 'Moda', 'good@music.com', 'kanye@music.com', '+44 20 7890 1234', '8234567890123', '2876543210987', 'Reino Unido', '2313', 'Londres', 'Camden', 'Camden High Street', '35', '10b', 'C. Finais', '2025-01-02');

-- --------------------------------------------------------

--
-- Estrutura para tabela `empresa_nacional`
--

CREATE TABLE `empresa_nacional` (
  `nome_empresa` varchar(60) NOT NULL,
  `nome_representante` varchar(60) NOT NULL,
  `setor_atuacao` varchar(40) NOT NULL,
  `email_empresa` varchar(60) NOT NULL,
  `email_representante` varchar(60) NOT NULL,
  `telefone_empresa` varchar(15) NOT NULL,
  `cnpj` varchar(18) NOT NULL,
  `cep` varchar(9) NOT NULL,
  `cidade` varchar(50) NOT NULL,
  `bairro` varchar(50) NOT NULL,
  `rua` varchar(50) NOT NULL,
  `numero` varchar(10) NOT NULL,
  `classificacao` varchar(30) NOT NULL,
  `data_contrato` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `empresa_nacional`
--

INSERT INTO `empresa_nacional` (`nome_empresa`, `nome_representante`, `setor_atuacao`, `email_empresa`, `email_representante`, `telefone_empresa`, `cnpj`, `cep`, `cidade`, `bairro`, `rua`, `numero`, `classificacao`, `data_contrato`) VALUES
('XO', 'Abel Tesfaye', 'Música', 'theweeknd@gmail.com', 'theweeknd@gmail.com', '(12) 22222-2222', ' 1.234.141/2422-41', '21810-080', 'Rio de Janeiro', 'Bangu', 'Rua Santa Cecília', '2111', 'Clientes Finais', '2025-03-14'),
('Odd Future', 'Tyler Okonma', 'Música', 'teste@gmail.com', 'teste@gmail.com', '(12) 31231-2313', '07.848.816/0001-19', '21820-070', 'Rio de Janeiro', 'Bangu', 'Rua dos Açudes', '111', 'Clientes Finais', '2025-03-21'),
('FE!N', 'FE!N', 'FE!N', 'fe1n@gmail.com.br', 'fe1n@gmail.com.br', '(12) 21121-3213', '21.312.321/2313-21', '89281-160', 'São Bento do Sul', 'Progresso', 'Servidão de Passagem Andre Grosskopf', '224', 'OES', '2025-04-04'),
('PGlang', 'Kendrick Lamar Duckworth', 'Arte', 'duckworth@email.com', 'duckworth@email.com', '(21) 34421-4212', '54.008.738/0001-24', '21810-200', 'Rio de Janeiro', 'Padre Miguel', 'Rua Tupiaçu', '22', 'OEM', '2025-03-18'),
('OPIUM', 'Playboi Carti', 'Música', 'OPIUM@TESTE.COM', 'OPIUM@TESTE.COM', '(83) 82093-9128', '89.231.807/9327-61', '21864-530', 'Rio de Janeiro', 'Bangu', 'Rua Osvaldo Evangelista dos Santos', '433', 'Distribuidores', '2025-08-15'),
('Cactus Jack', 'Travis Scott', 'Música', 'fe1n@gmail.com', 'fe1n@gmail.com', '(20) 13890-2318', '90.238.091/2389-01', '77062-078', 'Palmas', 'Jardim Aureny III', 'Avenida D', '222', 'Distribuidores', '2025-03-29');

-- --------------------------------------------------------

--
-- Estrutura para tabela `funcionarios`
--

CREATE TABLE `funcionarios` (
  `nome` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `cpf` varchar(14) NOT NULL,
  `telefone` varchar(15) NOT NULL,
  `estado` varchar(2) NOT NULL,
  `cep` varchar(9) NOT NULL,
  `cidade` varchar(50) NOT NULL,
  `bairro` varchar(50) NOT NULL,
  `rua` varchar(50) NOT NULL,
  `numero` varchar(10) NOT NULL,
  `senha` varchar(50) NOT NULL,
  `nivel` varchar(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `funcionarios`
--

INSERT INTO `funcionarios` (`nome`, `email`, `cpf`, `telefone`, `estado`, `cep`, `cidade`, `bairro`, `rua`, `numero`, `senha`, `nivel`) VALUES
('acesso funcionario', 'funcionario', '097.969.840-59', '(21) 99448-2398', 'RJ', '21864-430', 'Rio de Janeiro', 'Bangu', 'Rua Carmem Pazzini', '80', 'login', 'FUN'),
('Augusto', 'ferrazpresent@gmail.com', '166.519.717-00', '(21) 96583-5184', 'RJ', '21864-530', 'Rio de Janeiro', 'Bangu', 'Rua Osvaldo Evangelista dos Santos', '191', 'testesistema123', 'ADM'),
('conta acesso', 'admin', '290.138.902-73', '(29) 19193-9299', 'AL', '82813-762', 'aaaa', 'aaaa', 'aaaa', '111', 'login', 'ADM'),
('Jordan Terell Carter', 'playboicarti@gmail.com', '701.246.620-05', '(12) 31248-9721', 'SP', '04012-180', 'São Paulo', 'Vila Mariana', 'Avenida Doutor Dante Pazzanese', '123', 'opium*', 'ADM');

--
-- Índices para tabelas despejadas
--

--
-- Índices de tabela `empresa_internacional`
--
ALTER TABLE `empresa_internacional`
  ADD PRIMARY KEY (`ruc`,`rut`);

--
-- Índices de tabela `empresa_nacional`
--
ALTER TABLE `empresa_nacional`
  ADD PRIMARY KEY (`cnpj`);

--
-- Índices de tabela `funcionarios`
--
ALTER TABLE `funcionarios`
  ADD PRIMARY KEY (`cpf`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
