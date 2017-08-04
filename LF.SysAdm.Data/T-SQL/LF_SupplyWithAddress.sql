
CREATE VIEW LF_SupplyWithAddress AS
        SELECT SUP.ID ,
        SUP.[CompanyName] ,
		SUP.[CNPJ] ,
        SUP.[Phone] ,
        SUP.[Agent] ,
        SUP.[Email] ,
        SUP.[DateRegister] ,
        SUP.[DateOfChange] ,
        SUP.[AddressId],
        ADR.[Street],
        ADR.[Number],
        ADR.[Complement],
        ADR.[District],
        ADR.[City],ADR.[State],ADR.[CEP]
        FROM [dbo].[Supply] AS SUP
        INNER JOIN [dbo].[Address] AS ADR ON SUP.[AddressId] = ADR.[ID]




