

DROP TABLE IF EXISTS contas_receber;
DROP TABLE IF EXISTS contas_pagar;
DROP TABLE IF EXISTS categorias;
DROP TABLE IF EXISTS clientes;
DROP TABLE IF EXISTS contabilidades;

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