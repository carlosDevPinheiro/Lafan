using LF.SysAdm.Data.Context.Map;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Shared;
using System.Configuration;
using System.Data.Entity;

namespace LF.SysAdm.Data.Context
{
    public class DbContextEF: DbContext, IDbConnectionContext
    {
        private  string ConnectionString => ConfigurationManager.ConnectionStrings["Lafan"].ConnectionString;
        public DbContextEF()
             :base(ConfigurationManager.ConnectionStrings["Lafan"].ConnectionString) 
        {
           // Database.SetInitializer(new ContextInitialize());

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Users> _dbSetUser { get; set; }
        public DbSet<Customer> _dbsetcustomer { get; set; }
        public DbSet<Address> _dbsetaddress { get; set; }
        public DbSet<Category> _dbSetCategory { get; set; }
        public DbSet<Supply> _dbSetSupply { get; set; }
        public DbSet<Product> _dbSetProduct { get; set; }
        public DbSet<Employee> _dbSetEmployee { get; set; }
        public DbSet<ServiceProvide> _dbSetServiceProvide { get; set; }
        public DbSet<OrderItem> _dbSetOrderItem { get; set; }
        public DbSet<Order> _dbSetOrder { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new SupplyMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new ServiceProvideMap());
            modelBuilder.Configurations.Add(new OrderItemMap());
            modelBuilder.Configurations.Add(new OrderMap());
        }



        public class ContextInitialize : DropCreateDatabaseIfModelChanges<DbContextEF>
        {
            private readonly DbContextEF _context;
            public ContextInitialize()
            {
                _context = new DbContextEF();
                CreateViews(_context);
            }

            public void CreateViews(DbContextEF context)
            {
                _context.Database.ExecuteSqlCommand(LF_SupplyWithAddress);
            }

            private const string LF_SupplyWithAddress = "CREATE VIEW LF_SupplyWithAddress AS " +
                "SELECT SUP.ID ," +
                "SUP.[CompanyName] ," +
                "SUP.[CNPJ] ," +
                "SUP.[Phone] ," +
                "SUP.[Agent] ," +
                "SUP.[Email] ," +
                "SUP.[DateRegister] ," +
                "SUP.[DateOfChange] ," +
                "SUP.[AddressId]," +
                "ADR.[Street]," +
                "ADR.[Number]," +
                "ADR.[Complement]," +
                "ADR.[District]," +
                "ADR.[City],ADR.[State],ADR.[CEP]" +
                " FROM [dbo].[Supply] AS SUP" +
                " INNER JOIN [dbo].[Address] AS ADR ON SUP.[AddressId] = ADR.[ID]";
        }

        public static class LafanStoreProcedureCustomer
        {

            #region [Customer Save]

            public const string lafan_uspCustomerSave = "CREATE PROCEDURE lafan_uspCustomerSave" +
                    " @ID UNIQUEIDENTIFIER," +
                    " @Document  VARCHAR(16)," +
                    " @DateBirthday DATETIME," +
                    " @Phone  VARCHAR(14)," +
                    " @Gender VARCHAR(9)," +
                    " @UserId UNIQUEIDENTIFIER," +
                    " @DateRegisterCustomer DATETIME," +
                    " @DateOfChangeCustomer DATETIME, " +
                    " @AddressId UNIQUEIDENTIFIER = null," +
                    " @Street VARCHAR(100) = null," +
                    " @NUmber INT = null," +
                    " @Complement VARCHAR(100) = null," +
                    " @District VARCHAR(50) = null," +
                    " @City VARCHAR(50)= null," +
                    " @State VARCHAR(50)= null," +
                    " @CEP VARCHAR(9) = null," +
                    " @DateRegisterAddress DATETIME = null," +
                    " @DateOfChangeAddress DATETIME = null " +
                    " AS" +
                    " BEGIN" +
                    "   BEGIN TRY" +
                    "     BEGIN TRAN" +
                    "        IF(EXISTS(SELECT C.[ID] FROM[Customer] AS C WHERE C.[ID] = @ID))" +
                    "            BEGIN" +
                    "              UPDATE[Customer]" +
                    "                SET" +
                    "                 [DateBirthday] = @DateBirthday," +
                    "                 [Phone] = @Phone," +
                    "				  [Gender] = @Gender," +
                    "				  [DateOfChange] = @DateOfChangeCustomer" +
                    "                 WHERE[ID] = @ID" +
                    "                    SELECT @ID as retorno_update;" +
                    "			END" +
                    "        ELSE" +
                    "          BEGIN" +
                    "              INSERT INTO[Customer] (" +
                    "              [ID]," +
                    "              [Document]," +
                    "              [Phone]," +
                    "              [Gender]," +
                    "              [DateOfChange]," +
                    "              [DateRegister]," +
                    "              [UserId]," +
                    "              [DateBirthday]," +
                    "[User_ID])" +
                    "              VALUES" +
                    "(@ID, @Document, @Phone, @Gender, @DateOfChangeCustomer, @DateRegisterCustomer, @UserId, @DateBirthday,@UserId)" +
                    "              EXEC lafan_uspAddressCreate" +
                    "              @AddressId," +
                    "			  @Street," +
                    "			  @Number," +
                    "			  @Complement," +
                    "			  @District," +
                    "			  @City," +
                    "			  @State," +
                    "			  @CEP," +
                    "			  @DateRegisterAddress," +
                    "			  @DateOfChangeAddress," +
                    "			  @ID" +
                    "" +
                    "              SELECT @ID AS Retorno_INSERT;" +
                    "			END" +
                    "        COMMIT TRAN" +
                    "    END TRY" +
                    "    BEGIN CATCH" +
                    "        ROLLBACK TRAN" +
                    "        SELECT ERROR_MESSAGE() AS RETORNO_ERRO_UPDATE" +
                    "    END CATCH" +
                    " END";


            #endregion

            #region [Address Create]

            public const string lafan_uspAddressCreate = "CREATE PROCEDURE lafan_uspAddressCreate" +
                " @AddressId UNIQUEIDENTIFIER," +
                " @Street VARCHAR(100)," +
                " @Number INT," +
                " @Complement VARCHAR(100)," +
                " @District VARCHAR(50)," +
                " @City VARCHAR(50)," +
                " @State VARCHAR(50)," +
                " @CEP VARCHAR(9)," +
                " @DateRegisterAddress DATETIME," +
                " @DateChange DATETIME," +
                " @CustomerId UNIQUEIDENTIFIER" +
                " AS " +
                " BEGIN" +
                "    BEGIN TRY" +
                "        BEGIN TRAN" +
                "        IF(@CustomerId = null)" +
                "        RAISERROR('Cliente Obrigatorio !!', 14, 1)" +
                "        ELSE" +
                "         INSERT INTO[Address] ([ID],[Street],[Number],[Complement],[District],[City],[State],[CEP],[DateRegister],[DateOfChange],[CustomerId]) " +
                " VALUES (@AddressId,@Street,@Number,@Complement,@District,@City,@State,@CEP,@DateRegisterAddress,@DateChange,@CustomerId) " +

                "  SELECT @AddressId AS Retorno" +
                "        COMMIT TRAN" +
                "  END TRY" +
                "    BEGIN CATCH" +
                "        ROLLBACK TRAN" +
                "        SELECT ERROR_MESSAGE() AS Retorno" +
                "    END CATCH" +
                " END";

            #endregion

            #region [List Customer Whith User Whith Address]

            public const string lafan_uspCustomerWhithUserWhithAddressesList = "CREATE PROCEDURE lafan_uspCustomerWhithUserWhithAddressesList" +
                   "    @SKIP int = 0," +
                   "    @TAKE int = 10" +
                   " AS" +
                   " BEGIN" +
                   "    BEGIN TRY" +
                   "       SELECT" +
                   "       C.[ID] AS ID," +
                   "       C.[Document] AS [Document]," +
                   " 		C.[Phone] AS [Phone]," +
                   "       C.[DateBirthday] AS [DateBirthday]," +
                   "		C.[DateRegister] AS [DateRegister]," +
                   "       C.[DateOfChange] AS [DateOfChange]," +
                   "		C.[UserId] AS [UserId]," +
                   "       U.[ID] AS [UserReg_ID]," +
                   "		U.[Name] AS [UserReg_Name]," +
                   "       U.[Email] AS [UserReg_Email]," +
                   "		U.[Profile] AS [UserReg_Profile]," +
                   "       U.[RegistrationDate] AS [UserReg_RegistrationDate]," +
                   "		U.[Active] AS [UserReg_Active]," +
                   "       A.[ID] AS [Addresses_ID]," +
                   "		A.[Street] AS [Addresses_Street]," +
                   "       A.[Number] AS [Addresses_Number]," +
                   "		A.[Complement] AS [Addresses_Complement]," +
                   "       A.[District] AS [Addresses_District]," +
                   "		A.[State] AS [Addresses_State]," +
                    "		A.[City] AS [Addresses_City]," +
                   "       A.[CEP] AS [Addresses_CEP]," +
                   "		A.[DateRegister] AS [Addresses_DateRegister]," +
                   "       A.[DateOfChange] AS [Addresses_DateOfChange]," +
                   "		A.[CustomerId] AS [Addresses_CustomerId]" +
                   "        FROM[Customer] AS C" +
                   "      INNER JOIN[User] AS U ON C.[UserId] = U.[ID]" +
                   "      INNER JOIN[Address] AS A ON A.[CustomerId] = C.[ID]" +
                   "      ORDER BY C.[DateRegister] ASC" +
                   "     OFFSET @SKIP ROWS FETCH NEXT @TAKE ROWS ONLY" +
                   " END TRY" +
                   " BEGIN CATCH" +
                   "  SELECT ERROR_MESSAGE() AS Retorno" +
                   " END CATCH" +
                   " END";


            #endregion

            #region [Customer Whith Address]

            public const string lafan_uspCustomerWhithAddress = "CREATE PROCEDURE lafan_uspCustomerWhithAddress" +
                " @CustomerID UNIQUEIDENTIFIER" +
                " AS" +
                " BEGIN" +
                "    BEGIN TRY" +
                "        SELECT" +
                "            C.[ID], " +
                " 			 C.[DateBirthday] AS DateBirthday," +
                "            C.[DateOfChange] AS DateOfChange," +
                "            C.[DateRegister] AS DateRegister," +
                "            C.[Document]     AS Document," +
                "            C.[Gender]       AS Gender," +
                "            C.[Phone]        AS Phone," +
                "            C.[UserId]       AS UserId," +
                "            A.[ID]           AS Addresses_ID," +
                "            A.[Street]       AS Addresses_Street," +
                "            A.[Number]       As Addresses_Number," +
                "            A.[Complement]   AS Addresses_Complement," +
                "            A.[District]     AS Addresses_District," +
                "            A.[City]         AS Addresses_City," +
                "            A.[State]        AS Addresses_State," +
                "            A.[CEP]          AS Addresses_CEP," +
                "            A.[DateRegister] AS Addresses_DateRegister," +
                "            A.[DateOfChange] AS Addresses_DateOfChange," +
                "            A.[CustomerId]   AS Addresses_CustomerId" +
                "           FROM[Customer] AS C" +
                "          INNER JOIN[Address] AS A ON A.[CustomerId] = C.[ID]" +
                "          WHERE C.[ID] = @CustomerID" +
                "  END TRY" +
                "  BEGIN CATCH" +
                "    SELECT ERROR_MESSAGE() AS Retorno_ListCustomer" +
                "    END CATCH" +
                " END";



            #endregion
        }

        public static class LafanViewCustomer
        {
            #region [View Customer Whith User]

            public const string lafan_viewSelectCustomerUser = "CREATE VIEW lafan_viewSelectCustomerUser AS" +
               " SELECT  C.[ID], C.[Document], C.[Phone], C.[DateBirthday], C.[Gender], C.[DateOfChange] AS [PersonalInfoChanged]," +
               " U.[Name], U.[Email], U.[RegistrationDate], U.[DateofChange] AS [UserInfoChanged], U.[Profile], U.[Active] " +
               " FROM [dbo].[Customer] AS [C] " +
               " INNER JOIN [User] AS [U] ON [C].UserId = [U].ID ";

            #endregion
        }

        public static class LafanStoreProcedureSupply
        {
            #region [Create Supply]
            public const string lafan_uspSupplyCreate = "CREATE PROCEDURE lafan_uspSupplyCreate" +
                     " @ID UNIQUEIDENTIFIER," +
                     " @CompanyName VARCHAR(50)," +
                     " @CNPJ VARCHAR(18)," +
                     " @Phone VARCHAR(11)," +
                     " @Agent VARCHAR(11)," +
                     " @Email VARCHAR(100)," +
                     " @DateRegister DATETIME," +
                     " @DateOfChange DATETIME = NULL," +
                     " @AddressId UNIQUEIDENTIFIER" +
                     " AS" +
                     "    BEGIN" +
                     "     BEGIN TRY" +
                     "      BEGIN TRAN" +
                     "        BEGIN" +
                     "            INSERT INTO[Supply] ([ID], [CompanyName],[CNPJ],[Email],[Phone],[Agent],[DateRegister],[DateOfChange],[AddressId])" +
                     "           VALUES" +
                     "           (@ID, @CompanyName, @CNPJ, @Email, @Phone, @Agent, @DateRegister, @DateOfChange, @AddressId)" +
                     "        END" +
                     "      COMMIT TRAN" +
                     "     END TRY" +
                     "     BEGIN CATCH" +
                     "        ROLLBACK TRAN" +
                     "        SELECT ERROR_MESSAGE() AS RETORNO_INSERT" +
                     "     END CATCH" +
                     "    END";
            #endregion

            #region [Update Supply]
            public const string lafan_uspSupplyUpDate = "CREATE PROCEDURE lafan_uspSupplyUpDate" +
                     " @ID UNIQUEIDENTIFIER," +
                     " @CompanyName VARCHAR(50)," +
                     " @Phone VARCHAR(11)," +
                     " @Agent VARCHAR(11)," +
                     " @Email VARCHAR(100)," +
                     " @DateOfChange DATETIME" +
                     " AS" +
                     " BEGIN" +
                     "    BEGIN TRY" +
                     "        BEGIN TRAN" +
                     "    UPDATE[Supply]" +
                     "    SET" +
                     "    [CompanyName] = @CompanyName," +
                     "    [Phone] = @Phone," +
                     "    [Agent] = @Agent," +
                     "    [Email] = @Email," +
                     "    [DateOfChange] = @DateOfChange" +
                     "    WHERE[ID] =@ID;" +
                     "		COMMIT TRAN" +
                     "    END TRY" +
                     "    BEGIN CATCH" +
                     "       ROLLBACK" +
                     "       SELECT ERROR_MESSAGE() AS Retorno_Update_Supply" +
                     "    END CATCH" +
                     " END";
            #endregion
        }

        public static class LafanViewSupply
        {
            #region [VIEW Supply Whith Address]

            public const string lafan_viewSelectSupplyWhithAddress = "CREATE VIEW lafan_viewSelectSupplyWhithAddress" +
                   " AS" +
                   " SELECT S.[ID] AS SupplyId, S.[CompanyName], S.[CNPJ], S.[Phone], S.[Agent], S.[Email], S.[DateRegister], S.[DateOfChange], S.[AddressId]," +
                   " A.[Street], A.[Number], A.[Complement], A.[District], A.[City], A.[State], A.[CEP]" +
                   " FROM [Supply] AS S" +
                   " INNER JOIN [Address] AS A ON S.[AddressId] = A.[ID]";
            #endregion
        }

        public static class LafanStoreProcedureProduct
        {
            #region [Create Product]

            public const string lafan_uspProductCreate = "CREATE PROCEDURE lafan_uspProductCreate" +
                   " @ProductId uniqueidentifier," +
                   " @Name VARCHAR(60)," +
                   " @Description VARCHAR(100)," +
                   " @DateExpiration DATETIME," +
                   " @Quantity INT," +
                   " @Price DECIMAL(18,2)," +
                   " @DateRegister DATETIME," +
                   " @DateOfChange DATETIME = NULL," +
                   " @Active BIT," +
                   " @Image VARCHAR(8000)," +
                   " @Batch VARCHAR(50)," +
                   " @Invoice INT," +
                   " @CategoryId UNIQUEIDENTIFIER," +
                   " @SupplyId UNIQUEIDENTIFIER" +
                   " As" +
                   " BEGIN" +
                   "  BEGIN TRY" +
                   "   BEGIN TRAN" +
                   "    BEGIN" +
                   "        INSERT INTO[Product] (" +
                   "       [ID],[Name],[Description],[DateExpiration],[Quantity],[Price],[DateRegister],[DateOfChange],[Active]," +
                   "       [Image],[Batch],[Invoice],[CategoryId],[SupplyId])" +
                   "       VALUES" +
                   "       (@ProductId, @Name, @Description, @DateExpiration, @Quantity, @Price, @DateRegister, @DateOfChange," +
                   "        @Active, @Image, @Batch, @Invoice, @CategoryId, @SupplyId);" +
                   "            END" +
                   "           COMMIT TRAN" +
                   "          END TRY" +
                   "            BEGIN CATCH" +
                   "             ROLLBACK" +
                   "            SELECT ERROR_MESSAGE() AS Retorno_ERRO_INSERT_PRODUCT" +
                   "      END CATCH" +
                   " END";
            #endregion

            #region [UpDate Product]

            public const string lafan_uspProductUpDate = "CREATE PROCEDURE lafan_uspProductUpdate" +
                    " @ProductId uniqueidentifier," +
                    " @Name VARCHAR(60), " +
                    " @Description VARCHAR(100), " +
                    " @DateExpiration DATETIME," +
                    " @Quantity INT, " +
                    " @Price DECIMAL(18,2), " +
                    " @DateOfChange DATETIME = NULL," +
                    " @Active BIT, " +
                    " @Image VARCHAR(8000), " +
                    " @Batch VARCHAR(50), " +
                    " @Invoice INT" +
                    " AS" +
                    " BEGIN" +
                    "  BEGIN TRY" +
                    "    BEGIN TRAN" +
                    "        UPDATE[Product] SET" +
                    "        [Name] = @Name," +
                    "        [Description] = @Description," +
                    "        [DateExpiration] = @DateExpiration," +
                    "        [Quantity] = @Quantity," +
                    "        [Price] = @Price," +
                    "        [DateOfChange] = @DateOfChange," +
                    "        [Active] = @Active," +
                    "        [Image] = @Image," +
                    "        [Batch] = @Batch," +
                    "        [Invoice] = @Invoice" +
                    "        WHERE[ID] = @ProductId" +
                    "   COMMIT TRAN" +
                    " END TRY" +
                    " BEGIN CATCH" +
                    "   ROLLBACK TRAN" +
                    "   SELECT ERROR_MESSAGE() AS RetornoErro_ProductUpdate" +
                    "  END CATCH" +
                    " END";
            #endregion
        }

        public static class LafanViewProduct
        {
            public const string lafan_viewProductWhithSupplyWhithCategory = "CREATE VIEW lafan_viewProductWhithSupplyWhithCategory" +
                " AS " +
                " SELECT" +
                " Prod.[ID] AS[ProductId]," +
                " Prod.[Name] AS[ProductName]," +
                " Prod.[Description] AS[Description]," +
                " Prod.[Quantity] AS[Quantity]," +
                " Prod.[Price] AS[Price]," +
                " Prod.[Active] AS[Active]," +
                " Prod.[Image] AS[Image]," +
                " Sup.[CompanyName] AS[CompanyName]," +
                " Sup.[CNPJ] AS[CNPJ]," +
                " Cat.[NameCategory] AS[NameCategory]," +
                " Cat.[ID] AS[CategoryId]" +
                " FROM[Product] AS Prod" +
                " INNER JOIN[Category] AS Cat on Prod.[CategoryId] = Cat.[ID]" +
                " INNER JOIN[Supply] AS Sup ON Prod.[SupplyId] = Sup.[ID]";
        }
    }
}
