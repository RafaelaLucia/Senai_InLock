CREATE DATABASE inlock_games_tarde;
GO

USE inlock_games_tarde;
GO

--DDL

CREATE TABLE estudios(
idEstudio SMALLINT PRIMARY KEY IDENTITY (1,1),
nomeEstudio VARCHAR (100) NOT NULL
);
GO

CREATE TABLE jogos(
idJogo INT PRIMARY KEY IDENTITY (1,1),
nomeJogo VARCHAR (100) NOT NULL,
descricao VARCHAR (256),
dataLancamento DATE,
valor DECIMAL,
idEstudio SMALLINT FOREIGN KEY REFERENCES estudios (idEstudio)
);
GO

CREATE TABLE tipoUsuarios(
idTipoUsuario INT PRIMARY KEY IDENTITY (1,1),
titulo VARCHAR (15)
);
GO

CREATE TABLE usuarios (
idUsuario INT PRIMARY KEY IDENTITY (1,1),
email VARCHAR (256),
senha VARCHAR (25),
idTipoUsuario INT FOREIGN KEY REFERENCES tipoUsuarios (idTipoUsuario)
);
GO