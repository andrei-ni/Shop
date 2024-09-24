

namespace DrPrint.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().HasKey(ci => new { ci.UserId, ci.ProductId });
            modelBuilder.Entity<OrderItem>().HasKey(oi => new { oi.OrderId, oi.ProductId });

            //modelBuilder.Entity<Category>().HasData(
            //    new Category
            //    {
            //        Id = 1,
            //        Name = "Articole Personalizate",
            //        Description = "Personalizam o gama larga de produse pe care le puteti folosi pentru cadouri, decor sau promotionale pentru afacerea Dvs: Tricouri, Cani, Halbe de Bere, Suporturi Pahare, Fotografii Digitale, Tablouri pe Panza (Canvas), Briechete, Brelocuri, Pixuri, Ceasuri de Perete, CD-uri, DVD-uri si Carcase Printate, Jucarii, Magneti de frigider, Medalioane, Mouse-paduri, Placute Ceramice pentru Ocazii sau Comemorative, Puzzle, Perne si multe alte produse la cerere. Gasiti fiecare produs postat in interiorul siteului cu toate informatiile necesare.",
            //        Url = "articole-personalizate"
            //    },
            //    new Category
            //    {
            //        Id = 2,
            //        Name = "Print digital",
            //        Description = "Imprimam documente, carti de vizita, pliante, flyere, postere, foto, canvas si multe altele la cerere. Imprimam formate de la A4 la formate mari A0. Printarea se realizeaza pe utilaje Konica Minolta si Canon. Realizam fonte exceptionale. Comanda on-line sau in centrul de printare.",
            //        Url = "print-digital"
            //    },
            //    new Category
            //    {
            //        Id = 3,
            //        Name = "Fotografii Si Rame Foto",
            //        Description = "",
            //        Url = "fotografii-si-rame-foto"
            //    },
            //    new Category
            //    {
            //        Id = 4,
            //        Name = "Productie Publicitara",
            //        Description = "Drprint.ro va ofera o gama larga de solutii de publicitate. Va oferim: Bannere, Autocolante printate sau taiate la cutter-ploter pentru decorarea vitrinelor sau insctiptionari auto, Sisteme de afisaj: People stopper, Rame Aluminiu Click, sisteme Roll-up, Casete luminoase. Totul pentru afacerea Dvs. Pentru orice informatii suplimentare nu ezitati sa ne contactati!",
            //        Url = "productie-publicitara"
            //    }
            //    );

            //modelBuilder.Entity<SubCategory>().HasData(
            //    new SubCategory
            //    {
            //        Id = 1,
            //        Name = "Cadouri Personalizate - REDUCERI % -",
            //        Description = "Intra in aceasta categorie si alege din zeci de produse personalizate la pret redus. Trimiteti comanda, pozele pt personalizare si datele de livrare pe: contact@drprint.ro sau WhatsApp: 0723896370. In Bucuresti livrarea este gratuita iar in tara prin posta Romana 13 lei sau curier prioripost, 20 lei.",
            //        Url = "reduceri-cadouri-personalizate",
            //        ImageUrl = "/images/subcategory/articole-personalizate/reduceri.jpg",
            //        CategoryId = 1
            //    },
            //    new SubCategory
            //    {
            //        Id = 2,
            //        Name = "Ziua Copilului - Idei Cadouri Personalizate",
            //        Description = "Cadouri unice, personalizate pentru copiii Dvs. pe care le vor pretui toată viata. Alegeti dintr-o gama larga de produse: jucarii de plus, cani, tricouri, puzzle-uri, perne, globuri foto, ghiozdane, penare si multe alte cadouri. Bucuria copilului Dvs. va fi nepretuita!",
            //        Url = "ziua-copilului-idei-cadouri",
            //        ImageUrl = "/images/subcategory/articole-personalizate/ziua-copilului-idei-cadouri.jpg",
            //        CategoryId = 1
            //    },
            //    new SubCategory
            //    {
            //        Id = 3,
            //        Name = "Cani Cu Insertie Foto",
            //        Description = "Canile cu insertie foto personalizate prin introducerea unei fotografii in suportul de plastic transparent care inconjoara cana sunt indeale pentru a va transporta cafeaua in drum inspre serviciu sau in masina, la picnic etc fiind izolate cu capac evitand riscul de a varsa continutul. Acestea au si proprietati de termos pastrand calda bautura Dvs. un timp indelungat. Puteti personaliza si bidoane de plastic pentru copilul Dvs. Alegeti din modelele prezentate in continuare.  ",
            //        Url = "cani-cu-insertie-foto",
            //        ImageUrl = "/images/subcategory/articole-personalizate/cani-cu-insertie-foto.jpg",
            //        CategoryId = 1
            //    },
            //    );

            //modelBuilder.Entity<Product>().HasData(
            //new Product
            //{
            //    Id = 1,
            //    Name = "Carti de vizita fata-verso",
            //    Description = "Carti de vizita color fata-verso print carton special super-glossy. Realizam fonte excelente. Pentru a veni in sprijinul Dvs. in Bucuresti livrarea este gratuita! Tel/WhatsApp 0723896370. E-mail: contact@drprint.ro",
            //    ImageUrl = "/images/products/27255-scaled-800x800-min.jpg",
            //    OriginalPrice = 0.80,
            //    Price = 0.80,
            //    Stock = 100,
            //    CategoryId = 2,
            //    SubCategoryId = 28,
            //    Featured = true
            //},
            //new Product
            //{
            //    Id = 2,
            //    Name = "Carti de vizita o fata",
            //    Description = "Carti de vizita color o fata print, carton special ultra glossy. Realizam fonte excelente. Putem adauga gratuit cod QR. Pentru a veni in sprijinul Dvs. in Bucuresti livrarea este gratuita! Tel/WhatsApp 0723896370. E-mail: contact@drprint.ro",
            //    ImageUrl = "/images/products/buisness-card-800x800-min.jpg",
            //    OriginalPrice = 0.50,
            //    Price = 0.50,
            //    Stock = 100,
            //    CategoryId = 2,
            //    SubCategoryId = 28
            //},
            //);
        }

        //public DbSet<Product> Products => Set<Product>(); to remove DataContext warnings
        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<SubCategory>? SubCategories { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<CartItem>? CartItems { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
        public DbSet<Address>? Addresses { get; set; }
        public DbSet<Image>? Images { get; set; }

    }
}

