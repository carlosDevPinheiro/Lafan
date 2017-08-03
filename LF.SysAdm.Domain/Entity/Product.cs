using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Shared.Validations;
using System;

namespace LF.SysAdm.Domain.Entity
{
    public class Product : BaseEntity
    {
        protected Product() { }
        public Product(
            string name, string desc, DateTime? dtExp, int quant, decimal pric, string img, string batch, int invoi, Category cat, Supply supplyId)
        {
            Name = name;
            Description = desc;
            DateExpiration = dtExp;
            Quantity = quant;
            Price = pric;
            DateRegister = DateTime.Now;
            DateOfChange = null;
            Active = true;
            Image = img;
            Batch = batch;
            Invoice = invoi;
            Rel_Category = cat;
            Rel_Supply = supplyId;
            Register();
        }


        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime? DateExpiration { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public DateTime DateRegister { get; private set; }
        public DateTime? DateOfChange { get; private set; }
        public bool Active { get; private set; }
        public string Image { get; private set; }
        public string Batch { get; private set; }
        public int Invoice { get; private set; }

        
        public Category Rel_Category { get; private set; }       
        public Supply Rel_Supply { get; private set; }



        public void UpDate(
            string name, string desc, DateTime? dtExp, int quant, string img, string batch, int invoice, decimal price)
        {

            Name = name;
            Description = desc;
            DateExpiration = dtExp;
            Quantity = quant;
            Image = img;
            Batch = batch;
            Invoice = invoice;
            Price = price;
            DateOfChange = DateTime.Now;

            new ValidationContract<Product>(this)
                .IsRequired(x => x.Name, "Nome do Produto é Obrigatorio")
                .HasMaxLenght(x => x.Name, 60, "Tamnho maximo do Nome produto 60 char")
                .HasMinLenght(x => x.Name, 2, "Tamanho minimo do nome produto 2 char")
                .IsRequired(x => x.Description, "Dercricao do Produto é Obrigatorio")
                .HasMaxLenght(x => x.Description, 100, "Tamnho maximo do Dercricao produto 60 char")
                .HasMinLenght(x => x.Description, 5, "Tamanho minimo do Dercricao produto 2 char")
                .IsGreaterThan(x => x.Quantity, 0, " Quantidade não pode ser iferior a 1 'UM' ")
                .HasMaxLenght(x => x.Batch, 50, "Tamanho maximo para LOTE é de 50 char")
                .IsRequired(x => x.Batch, "Lote é obrigatorio")
                .IsGreaterOrEqualsThan(x => x.Invoice, 1, "numero de Nota Fiscal Invalida !!")
                .IsGreaterOrEqualsThan(x => x.Price, 1.00M, "Preco deve ser maior que R$ 1,00")
                .IsRequired(x => x.Image, "Imagem do Produto é obrigatorio");


        }

        public void UpDate(int quant, decimal price)
        {

            Quantity = quant;
            Price = price;
            DateOfChange = DateTime.Now;

            new ValidationContract<Product>(this)
             .IsGreaterThan(x => x.Quantity, 0, " Quantidade não pode ser iferior a 1 'UM' ")
             .IsGreaterOrEqualsThan(x => x.Price, 1.00M, "Preco deve ser maior que R$ 1,00");
        }

        public override void Register()
        {
            new ValidationContract<Product>(this)
                .IsRequired(x => x.Name, "Nome do Produto é Obrigatorio")
                .HasMaxLenght(x => x.Name, 60, "Tamnho maximo do Nome produto 60 char")
                .HasMinLenght(x => x.Name, 2, "Tamanho minimo do nome produto 2 char")
                .IsRequired(x => x.Description, "Dercricao do Produto é Obrigatorio")
                .HasMaxLenght(x => x.Description, 100, "Tamnho maximo do Dercricao produto 60 char")
                .HasMinLenght(x => x.Description, 5, "Tamanho minimo do Dercricao produto 2 char")
                .IsGreaterThan(x => x.Quantity, 0, " Quantidade não pode ser iferior a 1 'UM' ")
                .IsRequired(x => x.Image, "Imagem do Produto é obrigatorio")
                .IsGreaterOrEqualsThan(x => x.Invoice, 1, "numero de Nota Fiscal Invalida !!")
                .HasMaxLenght(x => x.Batch, 60, "Tamanho maximo para LOTE é de 60 char")
                .IsRequired(x => x.Batch, "Lote é obrigatorio")
                .IsGreaterOrEqualsThan(x => x.Price, 1.00M, "Preco deve ser maior que R$ 1,00");
        }
    }
}
