Use Gufi;
GO

INSERT INTO TipoUsuario (TituloTipoUsuario)
VALUES ('Administrador'), ('Comum');
GO

INSERT INTO TipoEvento(TituloTipoEvento)
VALUES ('C#'), ('ReactJS'), ('SQL');
GO

INSERT INTO Instituicao(CNPJ, NomeFantasia, Endereco)
VALUES ('9999999999', 'Escola senai de informática', 'Alameda barão de Limeira 539');
GO

INSERT INTO Situacao(Descricao)
VALUES ('Aguardando'), ('Aprovada'), ('Recusada');
GO

INSERT INTO Usuario(IdTipoUsuario,NomeUsuario,Email, Senha)
VALUES (1, 'Saulo', 'saulo@gmail.com', '1234'), (2, 'Lucas', 'lucas@gmail.com', '31223'),
(2, 'Thiago', 'ThiagoEmail@gmail.com', '1214'), (1, 'Odirlei', 'Odirla@gmail.com', '65763');
GO

INSERT INTO Evento(IdTipoEvento, IdInstituicao, NomeEvento,
DescricaoEvento, DataEvento, AcessoLivre)
VALUES (1, 1, 'Orientação a objetos', 'Conceitos de pilares da programação orientada a objetos',
'18/08/2021 14:00', 1),
(2,1, 'Ciclo de vida', 'Como utilizar os ciclos de vida com a biblioteca REACTJS',
'20/08/2021 10:30', 0);
GO

INSERT INTO Inscricao(IdUsuario, IdEvento, IdSituacao)
VALUES (3, 1, 2), (2, 2, 1);
GO