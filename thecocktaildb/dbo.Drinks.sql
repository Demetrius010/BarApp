﻿CREATE TABLE [dbo].[Drinks] (
    [idDrink]         INT            NOT NULL,
    [strDrink]        VARCHAR (37)   NOT NULL,
    [strTags]         VARCHAR (47)   NULL,
    [strCategory]     VARCHAR (20)   NOT NULL,
    [strIBA]          VARCHAR (21)   NULL,
    [strAlcoholic]    VARCHAR (16)   NULL,
    [strGlass]        VARCHAR (24)   NOT NULL,
    [strInstructions] VARCHAR (2524) NULL,
    [strDrinkThumb]   VARCHAR (69)   NOT NULL,
    [strIngredient1]  VARCHAR (26)   NOT NULL,
    [strIngredient2]  VARCHAR (26)   NOT NULL,
    [strIngredient3]  VARCHAR (26)   NULL,
    [strIngredient4]  VARCHAR (21)   NULL,
    [strIngredient5]  VARCHAR (24)   NULL,
    [strIngredient6]  VARCHAR (26)   NULL,
    [strIngredient7]  VARCHAR (20)   NULL,
    [strIngredient8]  VARCHAR (17)   NULL,
    [strIngredient9]  VARCHAR (13)   NULL,
    [strIngredient10] VARCHAR (13)   NULL,
    [strIngredient11] VARCHAR (13)   NULL,
    [strIngredient12] VARCHAR (13)   NULL,
    [strIngredient13] VARCHAR (30)   NULL,
    [strIngredient14] VARCHAR (30)   NULL,
    [strIngredient15] VARCHAR (30)   NULL,
    [strMeasure1]     VARCHAR (19)   NULL,
    [strMeasure2]     VARCHAR (39)   NULL,
    [strMeasure3]     VARCHAR (24)   NULL,
    [strMeasure4]     VARCHAR (21)   NULL,
    [strMeasure5]     VARCHAR (25)   NULL,
    [strMeasure6]     VARCHAR (17)   NULL,
    [strMeasure7]     VARCHAR (22)   NULL,
    [strMeasure8]     VARCHAR (15)   NULL,
    [strMeasure9]     VARCHAR (19)   NULL,
    [strMeasure10]    VARCHAR (13)   NULL,
    [strMeasure11]    VARCHAR (14)   NULL,
    [strMeasure12]    VARCHAR (13)   NULL,
    [strMeasure13]    VARCHAR (1)    NULL,
    [strMeasure14]    VARCHAR (1)    NULL,
    [strMeasure15]    VARCHAR (1)    NULL,
    [dateModified]    VARCHAR (19)   NULL,
    PRIMARY KEY CLUSTERED ([idDrink] ASC)
);
