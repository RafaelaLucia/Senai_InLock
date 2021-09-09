USE inlock_games_tarde;
GO
--DQL

--Listar todos os estúdios;
SELECT * FROM estudios;
GO
--Listar todos os tipos de usuários
SELECT * FROM tipoUsuarios;
GO
--Listar todos os usuários;
SELECT * FROM usuarios;
GO
--Listar todos os jogos;
SELECT * FROM jogos;
GO
--Listar todos os jogos e seus respectivos estúdios;
SELECT nomeJogo AS 'JOGO',nomeEstudio AS 'ESTUDIO' FROM jogos
INNER JOIN estudios
ON jogos.idEstudio = estudios.IdEstudio

--Buscar e trazer na lista todos os estúdios com os respectivos jogos.
SELECT nomeEstudio AS 'ESTUDIO', nomeJogo AS 'JOGO' FROM estudios
LEFT JOIN jogos
ON  estudios.idEstudio = jogos.idEstudio

--Buscar um usuário por e-mail e senha (login);
SELECT email, senha FROM usuarios
WHERE email = 'admin@admin.com'
AND senha = 'admin'

--Buscar um jogo por idJogo;
SELECT nomeJogo FROM jogos
WHERE idJogo = 2

--Buscar um estúdio por idEstudio;
SELECT nomeEstudio FROM estudios
WHERE idEstudio = 2