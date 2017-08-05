


CREATE PROCEDURE LF_OrderInvoice 
@ID UNIQUEIDENTIFIER
AS 
	BEGIN
	 BEGIN TRY
	  BEGIN TRAN
			 SELECT O.[ID] AS [OrderId] ,
			   O.[OrderDate] ,
			   O.[ChangeDate] ,
			   O.[Status] ,
			   O.[Total] ,
			   O.[Comments] ,
			   O.[PaymentMethod] ,
			   O.[Discount] ,
			   O.[CustomerId] ,
			   O.[EmployeeId] ,      
			   C.[Document] ,      
			   C.[Phone] , 
			   A.[Street],
			   A.[Number] ,
			   A.[Complement] ,
			   A.[District] ,
			   A.[City] ,
			   A.[State] ,
			   A.[CEP] ,      
			   U.[Name] ,
			   U.[Email] 
			  FROM [dbo].[Order] AS O
				INNER JOIN [dbo].[Customer] AS C ON O.[CustomerId] = C.[ID]
				INNER JOIN [dbo].[Address] AS A ON A.[CustomerId] = C.[ID]
				INNER JOIN [dbo].[Users] AS U ON C.[UserId] = C.[UserId]
				WHERE O.[ID] = @ID
		COMMIT TRAN
	   END TRY
       BEGIN CATCH
		SELECT ERROR_MESSAGE() AS ReturnError
       END CATCH
   END
