using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrPrint.Server.Migrations
{
    public partial class ProductsAndCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    OriginalPrice = table.Column<double>(type: "float", nullable: true),
                    View = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Personalizam o gama larga de produse pe care le puteti folosi pentru cadouri, decor sau promotionale pentru afacerea Dvs: Tricouri, Cani, Halbe de Bere, Suporturi Pahare, Fotografii Digitale, Tablouri pe Panza (Canvas), Briechete, Brelocuri, Pixuri, Ceasuri de Perete, CD-uri, DVD-uri si Carcase Printate, Jucarii, Magneti de frigider, Medalioane, Mouse-paduri, Placute Ceramice pentru Ocazii sau Comemorative, Puzzle, Perne si multe alte produse la cerere. Gasiti fiecare produs postat in interiorul siteului cu toate informatiile necesare.", "Articole Personalizate", "articole-personalizate" },
                    { 2, "Imprimam documente, carti de vizita, pliante, flyere, postere, foto, canvas si multe altele la cerere. Imprimam formate de la A4 la formate mari A0. Printarea se realizeaza pe utilaje Konica Minolta si Canon. Realizam fonte exceptionale. Comanda on-line sau in centrul de printare.", "Print digital", "print-digital" },
                    { 3, "", "Fotografii Si Rame Foto", "fotografii-si-rame-foto" },
                    { 4, "Drprint.ro va ofera o gama larga de solutii de publicitate. Va oferim: Bannere, Autocolante printate sau taiate la cutter-ploter pentru decorarea vitrinelor sau insctiptionari auto, Sisteme de afisaj: People stopper, Rame Aluminiu Click, sisteme Roll-up, Casete luminoase. Totul pentru afacerea Dvs. Pentru orice informatii suplimentare nu ezitati sa ne contactati!", "Productie Publicitara", "productie-publicitara" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Url" },
                values: new object[,]
                {
                    { 1, 1, "Intra in aceasta categorie si alege din zeci de produse personalizate la pret redus. Trimiteti comanda, pozele pt personalizare si datele de livrare pe: contact@drprint.ro sau WhatsApp: 0723896370. In Bucuresti livrarea este gratuita iar in tara prin posta Romana 13 lei sau curier prioripost, 20 lei.", "/images/subcategory/articole-personalizate/reduceri.jpg", "Cadouri Personalizate - REDUCERI % -", "reduceri-cadouri-personalizate" },
                    { 2, 1, "Cadouri unice, personalizate pentru copiii Dvs. pe care le vor pretui toată viata. Alegeti dintr-o gama larga de produse: jucarii de plus, cani, tricouri, puzzle-uri, perne, globuri foto, ghiozdane, penare si multe alte cadouri. Bucuria copilului Dvs. va fi nepretuita!", "/images/subcategory/articole-personalizate/ziua-copilului-idei-cadouri.jpg", "Ziua Copilului - Idei Cadouri Personalizate", "ziua-copilului-idei-cadouri" },
                    { 3, 1, "Canile cu insertie foto personalizate prin introducerea unei fotografii in suportul de plastic transparent care inconjoara cana sunt indeale pentru a va transporta cafeaua in drum inspre serviciu sau in masina, la picnic etc fiind izolate cu capac evitand riscul de a varsa continutul. Acestea au si proprietati de termos pastrand calda bautura Dvs. un timp indelungat. Puteti personaliza si bidoane de plastic pentru copilul Dvs. Alegeti din modelele prezentate in continuare.  ", "/images/subcategory/articole-personalizate/cani-cu-insertie-foto.jpg", "Cani Cu Insertie Foto", "cani-cu-insertie-foto" },
                    { 4, 1, "Personalizam cani din ceramica superioara cu poza sau textul Dvs.. Puteti face cadouri sau va puteti promova compania prin cani personalizate cu logoul si mesajul Dvs.. Puteti alege dintr-o gama variata de cani: albe, cu tortita si interiorul color, cani magice termosensibile, cani cu tortita in forma de inima, cani aurii sau argintii, sidefate, cani expresso cu farfurioare, cani termos cu insertie foto.", "/images/subcategory/articole-personalizate/cani-personalizate.jpg", "Cani Personalizate", "cani-personalizate" },
                    { 5, 1, "Personalizam cani pentru sarbatori din ceramica superioara cu poza sau textul Dvs..", "/images/subcategory/articole-personalizate/cani-pentru-sarbatori.jpg", "Cani Personalizate Pentru Sarbatori", "cani-personalizate-pentru-sarbatori" },
                    { 6, 1, "Creati perne personalizate pentru cadouri deosebite, restaurant, banquette, zona de relaxare in aer liber sau sufrageria Dvs. Suntem foarte mandri de detaliu si calitatea tuturor pernelor personalizate pe care le facem. Puteti alege perne satinate, diferite culori sau albe/argintii, perne patrate, inima, perne cuplu. De asemenea va oferim perne personalizate cu diferite mesaje si haioase pentru jumatatea ta. Alegeti din multutudine de modele prezentate in aceasta categorie.", "/images/subcategory/articole-personalizate/perne-personalizate.jpg", "Perne Personalizate", "perne-personalizate" },
                    { 7, 1, "Personalizam tricouri pentru cadouri, pentru angajatii firmei Dvs. sau ca materiale promotionale si campanii fiind un mod eficient de a face publicitate. Personalizam tricouri pentru barbati, femei si copii. Tricourile pot fii din bumbac 100% sau polyester ideale pentru activitati sportive. Personalizarile se realizeaza prin termotransfer sau serigrafie pentru cantitati mari.", "/images/subcategory/articole-personalizate/tricouri-personalizate.jpg", "Tricouri Personalizate", "tricouri-personalizate" },
                    { 8, 1, "Personalizate cu pozele preferate, globurile foto reprezinta un decor minunat pentru biroul Dvs sau pentru pomul de Craciun. Puteti faceun cadou deosebit pentru Craciun, Ziua Indragostitilor zile onomastice, sau orice alta ocazie deosebita.", "/images/subcategory/articole-personalizate/globuri-foto.jpg", "Globuri Foto Personalizate", "globuri-foto-personalizate" },
                    { 9, 1, "Magnetii de frigider personalizati pot fii folositi ca invitatii de nunta si botez, marturii, cadouri deosebite, carti de vizita sau o modalitate excelenta de reclama pentru afacerea Dvs. Puteti opta pentru: magneti printati pe hartie foto pentru o claritate si vivacitate ridicata a culorilor si suport folie magnetica flexibila, magneti flexibili cu insertie foto in diferite forme, magneti rigizi din platic cu insertie foto.", "/images/subcategory/articole-personalizate/magneti-personalizati.jpg", "Magneti De Frigider Personalizati", "magenti-de-frigider-personalizati" },
                    { 10, 1, "Personalizam cesuri din plastic,  sticla, MDF (lemn) sau Mahon - un cadou deosebit  sau un accesoriu util pentru biroul sau sufrageria Dvs.", "/images/subcategory/articole-personalizate/ceasuri-personalizate.jpg", "Ceasuri Personalizate", "ceasuri-personalizate" },
                    { 11, 1, "Puzzle-ul personalizat este ideal pentru un cadou unic de pentru aniversari, Craciun sau chiar pentru tine. O fotografie Puzzle este u mod popular si surprinzator de a trimite un mesaj frumos persoanei dragi. Puteti opta pentru puzzle cartonat drept-unghi sau in forma de inima. Trimiteti poza preferata pe adresa de e-mail: contact@drprint.ro iar noi vom realiza puzzle-ul Dvs. personalizat de cea mai inalta calitate!", "/images/subcategory/articole-personalizate/puzzle-personalizat.jpg", "Puzzle Personalizat", "puzzle-personalizat" },
                    { 12, 1, "Profesionale si practice, personalizate Mouse pad-uri adăuga o nota personala la birou. Adaugati fotografii, logo-ul sau un mesaj pentru a crea un cadou distractiv sau daruiti-l clientilor ca si obiect promotional. Disponibile in forma rotunda, drept-unghiulara sau in forma de inima.", "/images/subcategory/articole-personalizate/mousepad-personalizat.jpg", "Mousepad-uri Personalizate", "mousepad-uri-personalizate" },
                    { 13, 1, "Personalizam genti de umar de calitate superioara, de satin cu fermoar, diverse dimensiuni, pentru folosirea de zi cu zi sau pentru laptop.", "/images/subcategory/articole-personalizate/genti-personalizate.jpg", "Genti Personalizate", "genti-personalizate" },
                    { 14, 1, "Jucarii de plus pentru copilul tau, personalizate cu poza si textul favorite, un cadou minunat cu ocazia Sarbatorilor de iarna. Puteti alege: ursuleti de plus, panda, ren de plus, ren de plus cu caciulita, iepuras.", "/images/subcategory/articole-personalizate/jucarii-de-plus-personalizate.jpg", "Jucarii De Plus Personalizate", "jucarii-de-plus-personalizate" },
                    { 15, 1, "Personalizam veste pentru activitati de constructii, drumuri etc sau prntru activitati sportive (veste departajare). ", "/images/subcategory/articole-personalizate/veste-reflectorizante-personalizate.jpg", "Veste Reflectorizante Personalizate", "veste-reflextorizante-personalizate" },
                    { 16, 1, "Sort bucatarie personalizat cu poza sau textul favorite - acorda o nota profesinala restaurantului Dvs. sau reprezinta un cadou minunat pt mama, sotia sau bunica Dvs.", "/images/subcategory/articole-personalizate/sort-bucatarie-personalizat.jpg", "Sort Bucatarie Personalizat", "sort-bucatarie-personalizat" },
                    { 17, 1, "Carnetele personalizate ideale pentru scoli, licee sau un cadou unic si practic pentru prieteni. ", "/images/subcategory/articole-personalizate/carnetele-personalizate.jpg", "Carnetele Personalizate Cu Insertie Foto", "carnetele-personalizate-cu-insertie-foto" },
                    { 18, 1, "Placi ardezie ( piatra naturala ) personalizate foto. Fiecare bucata un unicat. Calitate masiva. Va puteti decora sufrageria, biroul, sau face un cadou unic, deosebit.", "/images/subcategory/articole-personalizate/piatra-naturala-personalizata.jpg", "Placi Ardezie / Piatra Naturala Personalizate", "placi-ardezie-piatra-naturala-personalizate" },
                    { 20, 1, "Portofelele sunt unul dintre accesoriile cele mai importante in vita de zi cu zi. Confera-le personalitate! Personalizeaza-le cu poza favorita si fa-le cadou prietenilor sau foloseste-le ca obiecte promotionale pentru a-ti promova afacerea.", "/images/subcategory/articole-personalizate/portofele-personalizate.jpg", "Portofele Personalizate", "portofele-personalizate" },
                    { 21, 1, "Halbe de bere personalizate cu poza, textul sau sigla dorita ideale pentru brandul restaurantului Dvs sau cadou pentru prieteni. ", "/images/subcategory/articole-personalizate/halbe-de-bere-personalizate.jpg", "Halbe De Bere Personalizate", "halbe-de-bere-personalizate" },
                    { 22, 1, "Personalizam o gama variata de suporturi de pahare pe care le puteti darui prietenilor, va puteti brandui barul sau pe puteti darui ca obiecte promotionale pentru a va face cunoscuta afacerea. ", "/images/subcategory/articole-personalizate/suporturi-pahare-personalizate.jpg", "Suporturi Pahare Personalizate", "suporturi-pahare-personalizate" },
                    { 23, 1, "Note-book personalizat, coperta si fiecare pagina in parte cu datele preferate. Ideal pentru afacerea ta sau caiet personalizat pentru copilul tau.", "/images/subcategory/articole-personalizate/note-book-personalizat.jpg", "Note-book Personalizat", "note-book-personalizat" },
                    { 24, 1, "Tricouri haioase pentru ea, pt el sau pt cuplul vostru, un cadou deosebit pentru jumatatea ta sau prieteni pentru orice ocazie.", "/images/subcategory/articole-personalizate/tricouri-haioase.jpg", "Tricouri Haioase", "tricouri-haioase" },
                    { 25, 1, "Bidoane sport cu capac personalizate cu imaginea favorita, ideale pt activitatile sportive, sala, biciclisti, calatorii montane sau ca mijloc de promovare.", "/images/subcategory/articole-personalizate/bidoane-personalizate.jpg", "Bidoane Sport Metalice Personalizate", "bidoane-sport-metalice-personalizate" },
                    { 26, 1, "", "/images/subcategory/articole-personalizate/brelocuri-personalizate.jpg", "Brelocuri Metalice Cu Insertie Foto", "brelocuri-metalice-cu-insertie-foto" },
                    { 27, 1, "Am aflat un secret! Mos Craciun ia cadourile personalizate de la DRPRINT.RO! Peantru Ea, pentru El, Mamici, Tatici si Bunici, toate le gasiti aici! Intra si tu si alege din multitudinea de cadouri: globuri foto, cani, jucarii de plus, penare, ornamente ceramice, fotografii si rame foto, magneti de frigider si multe altele.", "/images/subcategory/articole-personalizate/cadouri-personalizate.jpg", "Cadouri Personalizate", "cadouri-personalizate" },
                    { 28, 2, "Prin cartea de vizita oamenii isi creeaza prima impresie, astfel ca aceasta trebuie sa puna in lumina cat mai pozitiv activitatea si profilul Dvs. Va oferim Carti de Vizita color printate pe carton special super-glossy. Realizam fonte excelente. Nu aveti un design care sa va reprezinte? Apeleti la specialistii nostri! Va punem la dispozitie o serie de modele si le personalizam cu detaliile Dvs. Cea mai mare partea a cartoanelor folosite provin din materie reciclata. Daca aveti o singura sansa de a va face o prima impresie, faceti una buna!", "/images/subcategory/print-digital/carti-de-vizita.jpg", "Carti De Vizita", "carti-de-vizita" },
                    { 29, 2, "Etichete universale din hartie alba de inalta calitate, cu grad de alb mare si adeziv rezistent la temperaturi mari. Hartia din spatele autocolantului este de tip siliconic (80g/mp, 80 microni), iar hartia din fata este alba, mata ( 78g/mp, 74 microni).Adezivul este permanent. Etichetele sunt pretaiate fiind astfel usor de dezlipit de pe suportul din spate si usor de utilizat. ", "/images/subcategory/print-digital/etichete-autoadezive.jpg", "Etichete Autoadezive", "etichete-autoadezive" },
                    { 30, 2, "Flyerele personalizate reprezinta un mod atragator de promovare a unui produs sau eveniment. Atrageti clienti noi sau informati cumparatorii cu privire la produse noi sau reduceri. Print full color fata verso pe hartie lucioasa de calitate superioara cu gramaje cuprinse intre 115-130 g/mp. Design modern si atragator pe care il puteti alege din modelele puse la dispozitie pe site si personalizate cu datele si logoul Dvs. de specialistii nostri pentru un impact vizual exceptioanal asupra viitorilor clienti. Capacitate mare de productie si termene reduse de executie si transport.", "/images/subcategory/print-digital/flyere.jpg", "Flyere", "flyere" },
                    { 31, 2, "Pliantele personalizate reprezinta un mod atragator de promovare a unui produs sau eveniment. Atrageti clienti noi sau informati cumparatorii cu privire la produse noi sau reduceri. Print full color fata verso pe hartie lucioasa de calitate superioara cu gramaje cuprinse intre 115-130 g/mp. Design modern si atragator pe care il puteti alege din modelele puse la dispozitie pe site si personalizate cu datele si logoul Dvs. de specialistii nostri pentru un impact vizual exceptioanal asupra viitorilor clienti. Capacitate mare de productie si termene reduse de executie si transport.", "/images/subcategory/print-digital/pliante.jpg", "Pliante", "pliante" },
                    { 32, 2, "Realizam invitatii pentru nunti, botezuri si alte evenimente speciale din viata Dvs. Personalizate cu textele favorite si printate pe cartoane speciale sau cu aplicatii Glitter, Emboss, stantate sau in relief, invitatiile speciele de la Dr Print va ajuta sa anuntati cele mai placute evenimente din viata Dvs. in cel mai elegant mod.", "/images/subcategory/print-digital/invitatii.jpg", "Invitatii", "invitatii" },
                    { 33, 2, "Asigurati-va ca partenerii de afaceri si clientii recunosc corespondenta de la compania dvs. dintr-o privire prin utilizarea plicurilor personalizate de afaceri. Adaugati logo-ul dvs. si adresa expeditorului. De asemenea puteti folosi plicurile personalizate pentru ocaziile speciale din viata Dvs, nunti, botezuri etc. Selectati plicuri personalizate intr-o varietate de dimensiuni pentru a satisface nevoile exacte ale companiei.", "/images/subcategory/print-digital/plicuri-personalizate.jpg", "Plicuri Personalizate", "plicuri-personalizate" },
                    { 34, 2, "Printarea se face pe utilaje Konica Minolta si Canon noi. Realizam fonte, calitate exceptionala. Media de printare variaza intre 80 si 300 g/mp.", "/images/subcategory/print-digital/print-a4-a3.jpg", "Print A4 / A3", "print-a4-a3" },
                    { 35, 2, "Primul pas catre realizarea unei noi afaceri este reprezentat de prezentarea informatiilor companiei dvs catre clienti. In mapele personalizate de inalta calitate puteti cuprinde intreaga informatie necesara. Acestea contin buzunare adanci pentru documente si CD-uri de prezentare si suport pentru cartile Dvs de vizita.", "/images/subcategory/print-digital/mape-de-prezentare.jpg", "Mape De Prezentare Si Notebook Personalizat", "mape-de-prezentare-si-notebook-personalizat" },
                    { 36, 2, "Indosariati printurile Dvs. intr-un singur document eliminand astfel riscul de pierdere sau amestecarea paginilor. Un document indosaiat va fii mut mai usor de utilizat si de depozitat.Puteti indosaria documente, carti copiate, cursuri dar si portofolii de prezentare, cataloage, meniuri etc., spirala plastic sau metal, coperta plastic transparenta fata pentru protectia documentului si o nota de eleganta, coperta carton imitatie piele spate culoare neagra sau alba", "/images/subcategory/print-digital/servicii-indosariere.jpg", "Servicii Indosariere Documente", "servicii-indosariere-documente" },
                    { 37, 2, "Serviciul nostru de scanare la cerere maximizeaza eficienta, accesibilitatea si securitatea si va salva compania dvs. de costul de recuperare a documentatiei pe suport de hartie. Folosind acest serviciu fantastic inseamna ca biroul dumneavoastra nu va mai fi ingreunata cu fisiere si documente ca tot ceea ce aveti nevoie este returnat in format digital putand fi securizat prin parola. Tot ce trebuie sa faceti este sa ne furnizati documentele si le veti avea pe ecran in cateva minute! Scanam documente si fotografii color la  o rezolutie ridicata (600 dpi)  in format PDF, JPG, TIFF, formate A4 sau A3. Viteza mare de scanare si posibilitatea de scanare paginilor multiple intr-un singur document PDF sau separate.", "/images/subcategory/print-digital/scanare-documente.jpg", "Scanare Documente", "scanare-documente" },
                    { 38, 2, "Copiem documente alb-negru sau color pe utileje Konica Minolta noi avand conferind o calitate ridicata a serviciului si viteza mare de copiere atat fata cat si fata-verso. Realizam fonte. Copierea se realizeaza pe hartie standard 80 g/mp, dimensiuni A4 sau A3.", "/images/subcategory/print-digital/fotocopiere.jpg", "Fotocopiere", "fotocopiere" },
                    { 39, 3, "Rame foto din plastic, sticla si lemn sau rame foto cu ceas - un decor elegant pentru casa, birou, restaurante etc. Alegeti din multitudinea de modele si dimensiuni prezentate in fiecare categorie.", "/images/subcategory/fotografii-si-rame/rame-foto.jpg", "Rame Foto", "rame-foto" },
                    { 40, 3, "Printam fotografii pe hartie foto glossy superioara, conferind fotografiilor dumeavostra claritate, vivacitate si rezistenta in timp. Puteti astfel sa va afisati cele mai frumoase amintiri in culori vii, sa faceti cadouri pentru persoanele dragi, marturii pentru nunti si botezuri etc. Dimensiuni standard: 9x13 cm, 10 x 15 cm, 13x18 cm, 15x21 cm, 210x297 cm (A4), 297x420 cm (A3). Puteti solicita si orice alta dimensiune in functie de nevoiele Dvs.", "/images/subcategory/fotografii-si-rame/fotografii-digitale.jpg", "Fotografii Digitale", "fotografii-digitale" },
                    { 41, 4, "Va punem la dispozitie o gama larga de sisteme de afisaj pentru a va promova afacerea sau diferite evenimente: People Stopper, Roll-up, Rame Aluminiu Click.", "/images/subcategory/productie-publicitara/sisteme-de-afisaj.jpg", "Sisteme De Afisaj", "sisteme-de-afisaj" },
                    { 42, 4, "Autocolantul este baza productiei publicitare. Acesta poate fii taiat la cutter plotter, printat sau folosit ca atare. Ideal pentru decorarea vitrinelor, orare, etc. Pretul se calculeza in functie de dimensiune la 20 Euro mp.", "/images/subcategory/productie-publicitara/autocolant.jpg", "Autocolant", "autocolant" },
                    { 43, 4, "Realizam bannere de orice dimensiuni. Acestea sunt printate pe utilaj Mutoch la 1440 dpi, Eco-Solvent. Se printeaza pe Frontlit, Backlit sau panza de steag. Cereti oferta de pret in functie de dimensiune.", "/images/subcategory/productie-publicitara/bannere.jpg", "Bannere", "bannere" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Url" },
                values: new object[] { 44, 4, "Casete luminoase simpla fata sau dubla fata realizate cu profil de aluminiu, fata de styplex, colate cu print pe autocolant translucent si ranforsate cu cadru metalic. Pretul de lista este de 180euro/mp (acesta poate fluctua in functie de comanda).", "/images/subcategory/productie-publicitara/casete-luminoase.jpg", "Casete Luminoase", "casete-luminoase" });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Url" },
                values: new object[] { 45, 4, "Realizam inscriptionari auto pentru orice tip de autovehicul. Inscriptionarea se realizeaza cu autocolant taiat la cutter sau cu autocolant printat. Windows graphics ofera geamurilor o nuanta aparte, iar masina dumneavoastra se va bucura de un nou look. Pretul se realizeaza pentru fiecare lucrare in parte.", "/images/subcategory/productie-publicitara/inscriptionari-auto.jpg", "Inscriptionari Auto", "inscriptionari-auto" });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Url" },
                values: new object[] { 192, 1, "Sacose textile personalizate cu poza sau textul favorite ideale pentru un cadou unic pentru persoanele dragi sau un mijloc de promovare pentru afacerea ta.", "/images/subcategory/articole-personalizate/sacose-personalizate.jpg", "Sacose Shopping Panza Personalizate", "sacose-shopping-panza-personalizate" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "OriginalPrice", "Price", "Stock", "SubCategoryId", "View" },
                values: new object[,]
                {
                    { 1, 2, "Carti de vizita color fata-verso print carton special super-glossy. Realizam fonte excelente. Pentru a veni in sprijinul Dvs. in Bucuresti livrarea este gratuita! Tel/WhatsApp 0723896370. E-mail: contact@drprint.ro", "/images/products/27255-scaled-800x800-min.jpg", "Carti de vizita fata-verso", 0.80000000000000004, 0.0, 100, 28, 0 },
                    { 2, 2, "Carti de vizita color o fata print, carton special ultra glossy. Realizam fonte excelente. Putem adauga gratuit cod QR. Pentru a veni in sprijinul Dvs. in Bucuresti livrarea este gratuita! Tel/WhatsApp 0723896370. E-mail: contact@drprint.ro", "/images/products/buisness-card-800x800-min.jpg", "Carti de vizita o fata", 0.5, 0.0, 100, 28, 0 },
                    { 3, 2, "Etichete autoadezive A4 dreptunghiulare, 52.5 x 29.7 mm. Sunt etichete in coala, coli autocolante A4 cu finisare mata pentru imprimarea etichetelor de produs, a codurilor de bare, sau a altor informatii necesare identificarii si gestionarii produselor.", "/images/products/etichete-autoadezive-800x800-min.jpg", "Coli autoadezive A4, printate", 5.0, 0.0, 100, 29, 0 },
                    { 4, 2, "Etichete autoadezive 24/A4 dreptunghiulare. Sunt etichete in coala, coli autocolante A4 cu finisare mata pentru imprimarea etichetelor de produs, a codurilor de bare, sau a altor informatii necesare identificarii si gestionarii produselor.", "/images/products/etichete-autoadezive-800x800-min.jpg", "Etichete autoadezive 24/A4 Business", 0.29999999999999999, 0.0, 100, 29, 0 },
                    { 5, 2, "Etichete autoadezive 65/A4 dreptunghiulare. Sunt etichete in coala, coli autocolante A4 cu finisare mata pentru imprimarea etichetelor de produs, a codurilor de bare, sau a altor informatii necesare identificarii si gestionarii produselor.", "/images/products/etichete-autoadezive-800x800-min.jpg", "Etichete autoadezive 65/A4", 0.20000000000000001, 0.0, 100, 29, 0 },
                    { 6, 2, "Etichete autoadezive A5 dreptunghiulare. Sunt etichete in coala, coli autocolante A4 cu finisare mata pentru imprimarea etichetelor de produs, a codurilor de bare, sau a altor informatii necesare identificarii si gestionarii produselor.", "/images/products/etichete-autoadezive-800x800-min.jpg", "Etichete autoadezive A5", 2.5, 0.0, 100, 29, 0 },
                    { 7, 1, "De culoare neagra, canile magice se personalizeaza cu poza favorita care nu este vizibila atunci cand cana este rece. Dupa ce se toarna un lichid fierbinte (ceai, cafea, etc), aceasta devine alba permitand vizualizarea fotografiei si creand o surpriza placuta.", "/images/products/c870x524-800x800-min.jpg", "Cana 'Magica' personalizata cu poza si textul favorit", 45.0, 0.0, 100, 4, 0 },
                    { 8, 3, "Fa un cadou minunat sau decor pentru amintirile tale foto. Rama foto cu 6 poze. Pozele personalizate sunt incluse in pret. Dimensiune: 48 x 36 cm", "/images/products/youandi-frame.jpg", "Rama foto colaj bleu YOU & ME cu 6 poze personalizate", 79.0, 0.0, 100, 39, 0 },
                    { 9, 4, "People Stopper/Panou pietonal, confectionat din aluminiu anodizat cu colturi taiate la 45grade, profile clip-clap, folie transparenta reflectorizanta.", "/images/products/A-board_people_stopper-1-1-800x800-min.png", "People Stopper 50x70 cm", 490.0, 0.0, 10, 41, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
