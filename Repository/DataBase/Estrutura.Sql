DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS contabilidade;
DROP TABLE IF EXISTS clientes;
DROP TABLE IF EXISTS categorias;
DROP TABLE IF EXISTS cartoes_credito;
DROP TABLE IF EXISTS contas_pagar;
DROP TABLE IF EXISTS contas_receber;
DROP TABLE IF EXISTS compras;

CREATE TABLE usuarios (
	id INT PRIMARY KEY IDENTITY(1,1),
	login VARCHAR(45),
	senha VARCHAR(45),
	data_nascimento DATETIME2,
	id_contabilidade INT NOT NULL,
	FOREIGN KEY (id_contabilidade) REFERENCES contabilidades(id)
);

CREATE TABLE contabilidades (
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(45)
);

CREATE TABLE clientes (
	id INT PRIMARY KEY IDENTITY(1,1),
	id_contabilidade INT NOT NULL,
	FOREIGN KEY (id_contabilidade) REFERENCES contabilidades(id),
	nome VARCHAR(45),
	cpf VARCHAR(14)
);

CREATE TABLE 