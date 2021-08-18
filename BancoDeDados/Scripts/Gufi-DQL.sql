USE Gufi;
GO

--->LISTA TODOS OS TIPO DE USUARIO
	SELECT * FROM TipoUsuario;
	GO

--> LISTA TODOS OS USUARIOS
	SELECT * FROM Usuario;
	GO
--> LISTA TODAS AS INSTUICOES
	SELECT * FROM Instituicao;
	GO
--> LISTA TODOS OS TIPOS DE EVENTOS
	SELECT * FROM TipoEvento;
	GO
--> LISTA DE TODAS AS PRESENCAS
	SELECT * FROM Inscricao;
	GO
-->SELECIONAR OS DADOS DOS EVENTOS , DA INSTITUICAO E DOS TIPOS DE EVENTOS
	SELECT NomeEvento 'Nome do evento', TituloTipoEvento 'Tipo do evento',NomeFantasia 'Nome da Escola', DescricaoEvento 'Descrição do evento',DataEvento 'Data'
	FROM Evento
	INNER JOIN TipoEvento
	ON Evento.IdTipoEvento = TipoEvento.IdTipoEvento
	INNER JOIN Instituicao
	ON Evento.IdInstituicao = Instituicao.IdInstituicao;
	GO
-- Seleciona os dados dos usuários mostrando o tipo de usuário
	SELECT NomeUsuario, TituloTipoUsuario, Email
	FROM Usuario
	INNER JOIN TipoUsuario
	ON Usuario.IdTipoUsuario = TipoUsuario.IdTipoUsuario;
	GO
-- Seleciona os dados dos eventos, da instituição, dos tipos de eventos 
--e dos usuários
-- e a situacao da presenca
	SELECT  NomeUsuario 'Usuário',NomeFantasia 'Nome da Escola', NomeEvento 'Nome do Evento', TituloTipoEvento 'Tipo de evento', DescricaoEvento 'Descrição do evento',DataEvento 'Data'
	FROM Inscricao
	INNER JOIN Usuario
	ON Usuario.IdUsuario = Inscricao.IdUsuario
	INNER JOIN TipoUsuario 
	ON TipoUsuario.IdTipoUsuario = Usuario.IdTipoUsuario
	INNER JOIN Evento
	ON Evento.IdEvento = Inscricao.IdEvento
	INNER JOIN TipoEvento
	ON Evento.IdTipoEvento = TipoEvento.IdTipoEvento
	INNER JOIN Instituicao
	ON Evento.IdInstituicao = Instituicao.IdInstituicao
	INNER JOIN Situacao
	ON Situacao.IdSituacao = Inscricao.IdSituacao;
	GO
-- Busca um usuário através do seu e-mail e senha
	SELECT * FROM Usuario WHERE Email = 'Odirla@gmail.com' AND Senha = '65763';
	GO