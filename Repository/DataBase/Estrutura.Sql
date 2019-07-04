DROP TABLE IF EXISTS compras;
DROP TABLE IF EXISTS cartoes_credito;
DROP TABLE IF EXISTS contas_receber;
DROP TABLE IF EXISTS contas_pagar;
DROP TABLE IF EXISTS categorias;
DROP TABLE IF EXISTS clientes;
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS contabilidades;

CREATE TABLE contabilidades (
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(45)
);

CREATE TABLE usuarios (
	id INT PRIMARY KEY IDENTITY(1,1),
	login VARCHAR(45),
	senha VARCHAR(45),
	data_nascimento DATETIME2,
	id_contabilidade INT NOT NULL,
	FOREIGN KEY (id_contabilidade) REFERENCES contabilidades(id)
);

CREATE TABLE clientes (
	id INT PRIMARY KEY IDENTITY(1,1),
	id_contabilidade INT NOT NULL,
	FOREIGN KEY (id_contabilidade) REFERENCES contabilidades(id),
	nome VARCHAR(45),
	cpf VARCHAR(14)
);

CREATE TABLE categorias (
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(45)
);

CREATE TABLE conta_pagar (
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT NOT NULL,
	FOREIGN KEY (id_cliente) REFERENCES clientes(id),
	id_categoria INT NOT NULL,
	FOREIGN KEY (id_categoria) REFERENCES categorias(id),
	nome VARCHAR(45),
	data_vencimento DATETIME2,
	data_pagamento DATETIME2,
	valor DECIMAL(8,2)
);

CREATE TABLE contas_receber (
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT NOT NULL,
	FOREIGN KEY (id_cliente) REFERENCES clientes(id),
	id_categoria INT NOT NULL,
	FOREIGN KEY (id_categoria) REFERENCES categorias(id),
	nome VARCHAR(45),
	data_pagamento DATETIME2,
	valor DECIMAL(8,2)
);

CREATE TABLE cartoes_credito (
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT NOT NULL,
	FOREIGN KEY (id_cliente) REFERENCES clientes(id),
	numero VARCHAR(45),
	data_vencimento DATETIME2,
	cvv VARCHAR(45)
);

CREATE TABLE compras (
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cartao_credito INT NOT NULL,
	FOREIGN KEY (id_cartao_credito) REFERENCES cartoes_credito(id),
	valor DECIMAL(8,2),
	data_compra DATETIME2
);



SELECT 
clientes.id AS 'ClienteId',
clientes.nome AS 'ClienteNome',
clientes.cpf AS 'ClienteCpf',
categorias.id AS 'CategoriasId',
categorias.nome AS 'CategoriasNome',
contas_receber.id AS 'ContaReceberId',
contas_receber.nome AS 'ContaReceberNome',
contas_receber.data_pagamento AS 'ContaReceberDataPagamento',
contas_receber.valor AS 'ContaReceberValor'
FROM contas_receber
INNER JOIN clientes ON (contas_receber.id_clientes = clientes.id)
INNER JOIN categorias ON (contas_receber.id_categorias = categorias.id)