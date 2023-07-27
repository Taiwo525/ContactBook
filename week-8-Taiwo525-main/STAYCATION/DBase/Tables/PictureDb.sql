CREATE TABLE [dbo].[PictureDb]
(
	[HotelImageUrl] VARCHAR(200) NOT NULL PRIMARY KEY, 
    [HotelName] VARCHAR(100) NOT NULL, 
    [HotelLocation] VARCHAR(200) NOT NULL, 
    [HotelPrice] NVARCHAR(50) NULL, 
    [HotelDescription] VARCHAR(500) NULL, 
    [HotelGroup] VARCHAR(100) NULL, 
    [HotelPopularity] VARCHAR(100) NULL, 
    [IsPopular] VARCHAR(100) NULL
)
