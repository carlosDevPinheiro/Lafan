
CREATE PROCEDURE LF_GetOrders
@SKIP INT,
@TAKE INT

AS
	BEGIN
		BEGIN TRY
			BEGIN TRAN
				SELECT [ID] AS [OrderId] ,
				   [OrderDate] ,
				   [ChangeDate] ,
				   [Status] ,
				   [Total] ,
				   [Comments] ,
				   [PaymentMethod] ,
				   [Discount]
				   FROM [dbo].[Order]
				   ORDER BY [OrderDate] DESC
				   OFFSET @SKIP ROWS FETCH NEXT @TAKE ROWS ONLY
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ReturnError
		END CATCH
	END
