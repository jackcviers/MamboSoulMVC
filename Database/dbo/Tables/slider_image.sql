CREATE TABLE [dbo].[slider_image] (
    [ImageId]          INT           IDENTITY (1, 1) NOT NULL,
    [FileName]         VARCHAR (150) NOT NULL,
    [Extension]        VARCHAR (10)  NULL,
    [ProjectFileDirectory] VARCHAR (200) NOT NULL,
	[Description] VARCHAR(50) NULL,
    [Enabled]          BIT           NULL, 
    CONSTRAINT [PK_SliderImages] PRIMARY KEY CLUSTERED ([ImageId] ASC)
);

