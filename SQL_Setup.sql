-- Create Recipes DB
CREATE DATABASE Recipes;

----------------------------------------------------------------
-- Create Recipes Table
CREATE TABLE [Recipes].[dbo].[Recipes] (
    pkid INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(50),
    Ingredients VARCHAR(4000),
    Instructions VARCHAR(4000)
);

----------------------------------------------------------------
-- Create RecipeCreate
USE [Recipes]
GO
/****** Object:  StoredProcedure [dbo].[RecipeCreate]    Script Date: 3/1/2021 01:23:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RecipeCreate]
	--@pkid INT,
	@Title VARCHAR(50),
	@Ingredients VARCHAR(4000),
	@Instructions VARCHAR(4000)
AS
BEGIN
	INSERT INTO Recipes(Title,Ingredients,Instructions)
	--VALUES(@Title,@Ingredients,@Instructions)
	VALUES(LTRIM(RTRIM(@Title)),LTRIM(RTRIM(@Ingredients)),LTRIM(RTRIM(@Instructions)))
END

----------------------------------------------------------------
-- Create RecipeDeleteByPkid 
USE [Recipes]
GO
/****** Object:  StoredProcedure [dbo].[RecipeDeleteByPkid]    Script Date: 3/1/2021 01:24:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RecipeDeleteByPkid]
	@pkid INT
AS
BEGIN
	DELETE FROM Recipes -- May need to be delete * from
	WHERE pkid=@pkid
END


----------------------------------------------------------------
-- CREATE RecipeEdit
/****** Object:  StoredProcedure [dbo].[RecipeEdit]    Script Date: 3/1/2021 01:28:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RecipeEdit]
	@pkid INT,
	@Title VARCHAR(50),
	@Ingredients VARCHAR(4000),
	@Instructions VARCHAR(4000)
AS
BEGIN
	UPDATE Recipes
	SET
		Title=@Title,
		Ingredients=@Ingredients,
		Instructions=@Instructions
	WHERE pkid=@pkid
END


----------------------------------------------------------------
-- Create RecipeViewAll
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Recipes].[dbo].[RecipeViewAll]

AS
BEGIN
	SELECT * FROM Recipes
END


----------------------------------------------------------------
-- Create RecipeViewByPkid
/****** Object:  StoredProcedure [dbo].[RecipeViewByPkid]    Script Date: 3/1/2021 01:32:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Recipes].[dbo].[RecipeViewByPkid]
	@pkid INT
AS
BEGIN
	SELECT * 
	FROM Recipes
	WHERE pkid=@pkid
END