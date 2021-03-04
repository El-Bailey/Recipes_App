-- Create Recipes DB
CREATE DATABASE Recipes;

----------------------------------------------------------------
-- Create Recipes Table
CREATE TABLE [Recipes].[dbo].[Recipes] (
    pkid INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(50),
    Ingredients VARCHAR(4000),
    Instructions VARCHAR(4000)
)
----------------------------------------------------------------


----------------------------------------------------------------
-- Create RecipeUsers Table
CREATE TABLE [Recipes].[dbo].[RecipesUsers] (
    pkid INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50),
    User_Email VARCHAR(254),
    User_Password VARCHAR(1000)
)
----------------------------------------------------------------


----------------------------------------------------------------
-- Create UserCreate
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RecipesUserCreate]
	--@pkid INT,
	@Username VARCHAR(50),
	@User_Email VARCHAR(254),
	@User_Password VARCHAR(1000)
AS
BEGIN
	INSERT INTO RecipesUsers(Username,User_Email,User_Password)
	--VALUES(@Title,@Ingredients,@Instructions)
	VALUES(@Username,@User_Email,@User_Password)
END
----------------------------------------------------------------


----------------------------------------------------------------
-- CREATE FetchRecipesUserByUsername
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FetchRecipesUserByUsername]
	--@pkid INT,
	@Username VARCHAR(50)
AS
BEGIN
	Select * 
	FROM RecipesUsers
	WHERE Username = @Username
END
----------------------------------------------------------------


----------------------------------------------------------------
-- Create RecipeCreate
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


----------------------------------------------------------------
-- Create RecipeDeleteByPkid 
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


----------------------------------------------------------------
-- CREATE RecipeEdit
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


----------------------------------------------------------------
-- Create RecipeViewAll
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RecipeViewAll]

AS
BEGIN
	SELECT * FROM Recipes
END
----------------------------------------------------------------


----------------------------------------------------------------
-- Create RecipeViewByPkid
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RecipeViewByPkid]
	@pkid INT
AS
BEGIN
	SELECT * 
	FROM Recipes
	WHERE pkid=@pkid
END
----------------------------------------------------------------
