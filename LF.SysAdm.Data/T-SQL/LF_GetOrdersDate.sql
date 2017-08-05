
-- EXECUTE dbo.LF_GetOrdersDate @START = '2017-08-05' , -- datetime
                           --  @END = '2017-08-05'     -- datetime

CREATE PROCEDURE LF_GetOrdersDate
@START DATETIME,
@END DATETIME
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
                       [Discount] ,
                       [CustomerId] ,
                       [EmployeeId]
					   FROM [dbo].[Order] WHERE [OrderDate] = @START AND [OrderDate] = @END
				ORDER BY [OrderDate] ASC				
			COMMIT TRAN
		END TRY
		BEGIN CATCH
		END CATCH
	END