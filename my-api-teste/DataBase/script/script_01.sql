USE master
GO

IF EXISTS( SELECT name
             FROM sys.databases
			WHERE name = 'TesteWebCesar' )
   BEGIN
     
	 DROP DATABASE TesteWebCesar

   END
GO

CREATE DATABASE TesteWebCesar
GO

USE TesteWebCesar
GO

IF NOT EXISTS( SELECT name
                 FROM SysObjects
				WHERE name = 'Estado' )
   BEGIN
     
	 CREATE TABLE [dbo].[Estado]
	 (
	    IdEstado         [Int]          IDENTITY(1,1) PRIMARY KEY NOT NULL,
		Descricao        [VarChar](200)                           NOT NULL,
		Sigla            [Char](2)                                NOT NULL,

		-->> unique
		CONSTRAINT [UnEstado_Descricao]
		    UNIQUE ([Descricao]),

		CONSTRAINT [UnEstado_Sigla]
		    UNIQUE ([Sigla])
	 )

	 -->> indexes
	 CREATE INDEX [Idx_Estado_Descricao]
	           ON [dbo].[Estado]([Descricao])
			 WITH FILLFACTOR = 90
			   ON [PRIMARY]

	 CREATE INDEX [Idx_Estado_Sigla]
	           ON [dbo].[Estado]([Sigla])
			 WITH FILLFACTOR = 90
			   ON [PRIMARY]

  END
GO


IF NOT EXISTS( SELECT name
                 FROM SysObjects
				WHERE name = 'Cidade' )
   BEGIN
     
	 CREATE TABLE [dbo].[Cidade]
	 (
	    IdCidade         [Int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
		Descricao        [VarChar](200)                  NOT NULL,
		IdEstado         [Int]                           NOT NULL,

		-->> unique
		CONSTRAINT [UnCidade]
		    UNIQUE ([Descricao],[IdEstado]),
        
	    
		-->> foreign key
	    CONSTRAINT [FkCidade_Estado]
	       FOREIGN KEY([IdEstado])
	    REFERENCES [dbo].[Estado]([IdEstado]),
	 
	 )

	 -->> indexes
	 CREATE INDEX [Idx_Cidade_Descricao]
	           ON [dbo].[Cidade]([Descricao])
			 WITH FILLFACTOR = 90
			   ON [PRIMARY]

	 CREATE INDEX [Idx_Cidade_IdEstado]
	           ON [dbo].[Cidade]([IdEstado])
			 WITH FILLFACTOR = 90
			   ON [PRIMARY]

   END
GO


IF NOT EXISTS( SELECT name
                 FROM SysObjects
				WHERE name = 'PontoTuristico' )
   BEGIN
     
	 CREATE TABLE [dbo].[PontoTuristico]
	 (
	    IdPontoTuristico [Int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
		Nome             [VarChar](100)                  NOT NULL,
        Descricao        [VarChar](100)                  NOT NULL,
		Referencia       [VarChar](100)                      NULL,
		IdCidade         [Int]                           NOT NULL,
		DataInclusao     [DateTime]                      NOT NULL,

		
		-->> foreign key
	    CONSTRAINT [FkPontoTuristico_Cidade]
	       FOREIGN KEY([IdCidade])
	    REFERENCES [dbo].[Cidade]([IdCidade]),
	 
	 )

	 -->> indexes
	 CREATE INDEX [Idx_PontoTuristico_Nome]
	           ON [dbo].[PontoTuristico]([Nome])
			 WITH FILLFACTOR = 90
			   ON [PRIMARY]


	 CREATE INDEX [Idx_PontoTuristico_Descricao]
	           ON [dbo].[PontoTuristico]([Descricao])
			 WITH FILLFACTOR = 90
			   ON [PRIMARY]


	 CREATE INDEX [Idx_PontoTuristico_DataInclusao]
	           ON [dbo].[PontoTuristico]([DataInclusao])
			 WITH FILLFACTOR = 90
			   ON [PRIMARY]


	 CREATE INDEX [Idx_PontoTuristico_Referencia]
	           ON [dbo].[PontoTuristico]([Referencia])
			 WITH FILLFACTOR = 90
			   ON [PRIMARY]

	 
	 CREATE INDEX [Idx_PontoTuristico_IdCidade]
	           ON [dbo].[PontoTuristico]([IdCidade])
			 WITH FILLFACTOR = 90
			   ON [PRIMARY]

   END
GO


IF EXISTS( SELECT name
             FROM SysObjects
			WHERE name = 'stp_PontoTuristicoGet'
			  AND type = 'P' )
  BEGIN
    
	DROP PROCEDURE [dbo].[stp_PontoTuristicoGet]

  END
GO

CREATE PROCEDURE [dbo].[stp_PontoTuristicoGet]
AS
  
  BEGIN
    
	SET NOCOUNT ON
	    

		SELECT IdPontoTuristico    = ptu.IdPontoTuristico,
		       Nome                = ptu.Nome,
			   Descricao           = ptu.Descricao,
			   Referencia          = ptu.Referencia,
			   Cidade              = cid.Descricao,
			   Estado              = est.Descricao
		  FROM dbo.PontoTuristico AS ptu
	INNER JOIN dbo.Cidade         AS cid ON ptu.IdCidade = cid.IdCidade
	INNER JOIN dbo.Estado         AS est ON cid.IdEstado = est.IdEstado
	  ORDER BY ptu.DataInclusao  DESC

  
    SET NOCOUNT OFF

  END
GO


IF EXISTS( SELECT name
             FROM SysObjects
			WHERE name = 'stp_EstadoGet'
			  AND type = 'P' )
  BEGIN
    
	DROP PROCEDURE [dbo].[stp_EstadoGet]
  
  END
GO

CREATE PROCEDURE [dbo].[stp_EstadoGet]
                       @IdEstado Int = 0
AS
  
  BEGIN
    
	SET NOCOUNT ON
	    

		SELECT IdEstado    = est.IdEstado,
		       Descricao   = est.Descricao,
			   Sigla       = est.Sigla
		  FROM dbo.Estado AS est
		 WHERE ( ( @IdEstado = 0 ) OR ( est.IdEstado = @IdEstado ) )
      ORDER BY est.Sigla

    
	SET NOCOUNT OFF
  
  END
GO


IF EXISTS( SELECT name
             FROM SysObjects
			WHERE name = 'stp_CidadeGetByIdEstado'
			  AND type = 'P' )
  BEGIN
    
	DROP PROCEDURE [dbo].[stp_CidadeGetByIdEstado]
  
  END
GO

CREATE PROCEDURE [dbo].[stp_CidadeGetByIdEstado]
                       @IdEstado Int
AS
  
  BEGIN
    
	SET NOCOUNT ON
	    

		SELECT IdCidade     = cid.IdCidade,
		       Descricao    = cid.Descricao,
			   IdEstado     = cid.IdEstado
		  FROM dbo.Cidade  AS cid
		 WHERE cid.IdEstado = @IdEstado
      ORDER BY cid.Descricao

    
	SET NOCOUNT OFF
  
  END
GO

IF EXISTS( SELECT name
             FROM SysObjects
			WHERE name = 'stp_PontoTuristicoInsert'
			  AND type = 'P' )
  BEGIN
    
	DROP PROCEDURE [dbo].[stp_PontoTuristicoInsert]
  
  END
GO

CREATE PROCEDURE [dbo].[stp_PontoTuristicoInsert]
                       @IdPontoTuristico Int Out,
					   @Nome             VarChar(100), 
                       @Descricao        VarChar(100),
                       @Referencia       VarChar(100) = NULL,
                       @IdCidade         Int
AS
  
  BEGIN
    
	SET NOCOUNT ON

	    DECLARE 
		  @DataInclusao DateTime = GETDATE()
	    
		  INSERT INTO dbo.PontoTuristico
		              ( Nome, 
		  		        Descricao, 
		  			    Referencia, 
		  			    IdCidade, 
		  			    DataInclusao
		  		      )
		  	   VALUES 
		  	          ( @Nome, 
		  		        @Descricao, 
		  			    @Referencia, 
		  			    @IdCidade, 
		  			    @DataInclusao
		  			  )

		  SET @IdPontoTuristico = SCOPE_IDENTITY()
		  
    
	SET NOCOUNT OFF
  
  END
GO


IF EXISTS( SELECT name
             FROM SysObjects
			WHERE name = 'stp_PontoTuristicoUpdate'
			  AND type = 'P' )
  BEGIN
    
	DROP PROCEDURE [dbo].[stp_PontoTuristicoUpdate]

  END
GO

CREATE PROCEDURE [dbo].[stp_PontoTuristicoUpdate]
                       @IdPontoTuristico Int Out,
					   @Nome             VarChar(100), 
                       @Descricao        VarChar(100),
                       @Referencia       VarChar(100) = NULL,
                       @IdCidade         Int

AS
  BEGIN
    
	SET NOCOUNT ON
	    

		UPDATE dbo.PontoTuristico
		   SET Nome             = @Nome, 
               Descricao        = @Descricao,
               Referencia       = @Referencia,
               IdCidade         = @IdCidade
		 WHERE IdPontoTuristico = @IdPontoTuristico

	
	SET NOCOUNT OFF

  END
GO


IF EXISTS( SELECT name
             FROM SysObjects
			WHERE name = 'stp_PontoTuristicoDelete'
			  AND type = 'P' )
  BEGIN
    
	DROP PROCEDURE [dbo].[stp_PontoTuristicoDelete]
  
  END
GO

CREATE PROCEDURE [dbo].[stp_PontoTuristicoDelete]
                       @IdPontoTuristico Int
AS
  
  BEGIN
    
	SET NOCOUNT ON

	    DELETE 
		  FROM dbo.PontoTuristico 
		 WHERE IdPontoTuristico = @IdPontoTuristico  
    
	SET NOCOUNT OFF
  
  END
GO


IF EXISTS( SELECT name
             FROM SysObjects
			WHERE name = 'stp_PontoTuristicoCarregarDadosGet'
			  AND type = 'P' )
  BEGIN
    
	DROP PROCEDURE [dbo].[stp_PontoTuristicoCarregarDadosGet]
  
  END
GO

CREATE PROCEDURE [dbo].[stp_PontoTuristicoCarregarDadosGet]
                       @IdPontoTuristico Int
AS
  
  BEGIN
    
	SET NOCOUNT ON
	    

		SELECT IdPontoTuristico     = ptu.IdPontoTuristico,
		       Nome                 = ptu.Nome,
               Descricao            = ptu.Descricao,
			   IdEstado             = cid.IdEstado,
			   IdCidade             = ptu.IdCidade,
               Referencia           = ptu.Referencia,
			   DataInclusao         = ptu.DataInclusao
		  FROM dbo.PontoTuristico  AS ptu
	INNER JOIN dbo.Cidade          AS cid ON ptu.IdCidade = cid.IdCidade
		 WHERE ptu.IdPontoTuristico = @IdPontoTuristico

    
	SET NOCOUNT OFF

  END
GO

