
CREATE PROCEDURE LF_GetProduct
@ID UNIQUEIDENTIFIER
AS 
BEGIN
	BEGIN TRY
	SELECT PD.[ID] AS [ProductId],
           PD.[Name] ,
           PD.[Description] ,
           PD.[DateExpiration] ,
           PD.[Quantity] ,
		   PD.[Image],
           PD.[Price],
		   CT.[ID] AS [CategoryId],
		   CT.[NameCategory],
		   SP.[ID] AS [SupplyId],
		   SP.[CNPJ],
		   SP.[CompanyName]
           FROM [dbo].[Product] AS PD
		   INNER JOIN [dbo].[Category] AS CT ON PD.[CategoryId] = CT.[ID]
		   INNER JOIN [dbo].[Supply] AS SP ON PD.[SupplyId] = SP.[ID]
		   WHERE PD.[ID] = @ID
		  
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Result
	END CATCH
END



						  
		   
 