
CREATE VIEW LF_EmployeeWithAddress AS
SELECT EP.[ID] AS [EmployeeId] ,
       EP.[Name] ,
       EP.[Function] ,
       EP.[Department] ,
       EP.[Document] ,
       EP.[DateBirthday] ,
       EP.[DateRegister] ,
       EP.[DateOfChange] ,
       EP.[RE] ,
       EP.[AddressId] ,       
       AD.[Street] ,
       AD.[Number] ,
       AD.[Complement] ,
       AD.[District] ,
       AD.[City] ,
       AD.[State] ,
       AD.[CEP]
       FROM [dbo].[Employee] AS EP
INNER JOIN [dbo].[Address] AS AD ON EP.[AddressId] = AD.ID