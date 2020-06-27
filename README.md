# RestFul-API
Bu depoda, Asp .Net Core Restful API bulunmaktadır. Projenin asıl amacı AutoMaper, Swasbuckle ve Jwt Token implemantasyopnları olduğu için mimariye dikakt edilmemiştir. Projeye dair bütün yapılar Infrastructure klasörü altında toplanmıştır.

## Automapper

AutoMapper, karmaşık bir sournu çözmek için tasarlanmış basit ve küçük bir kütüphanedir. Temelde üstlendiği görev gayet yalındır, bir nesneyi diğer bir nesne ile eşleyen koddan kurtulmak için tercih ettiğimiz bir yapıdır. Bu tür bir eşleşme kodunu yazmak oldukça zahmetli ve sıkıcıdır, örneğin 2 ayrı tabloda tutuğumuz category ve product bilgilerini getirmek istiyoruz. Bu işlemi yerine getirebilmek için bir veri taransfer nesnesi (DTO)  oluşturduğumuzu ve ihtiyacımızı karşılayacak özellikleri burada hayata geçirdiğimizi düşünelim. Bu senaryoda yapmamız gereken diğer bir işlem ise Linq to Entitiy sorgusu yazmak ve ihtiyacımız olan alanlar ile DTO'muzdaki alanları birbirlerine eşitlememiz gerekmektedir. Burada işlem kulağa gayet basit gelebilir özellikle örneğini verdiğimiz senaryoda lakin yaptığımız işler büyüdükçe select kısmında elle tek tek yazacağımız kodlarda yapılan iş ile eş zamanlı olarak büyükmektedir ve bu işlem epey zahmetli olacak ve kod okunabilirliği zafiyeti oluşarak, sorgu anlaşılabilirliği kaybolacaktır. Bu gibi durumlarda yapılan işeleme özel Data Transfer Object'leri yazıp bu objeleri entity'lerimiz ile automap aracı ile mapping işlemini yaparak işlerimizi kolaylaştırabilriz. <br>

#### AutoMapper Implemantation

1. İlk önce Nuget Package Mager aracılığı ile ihtiyaç duyulan paketleri yükelyelim: <br>
  1.1. AutoMapper - Version: 9.0.0 <br>
  1.2. AutoMapper.Extensions.Microsoft.DependencyInjection - Version: 7.0.0 <br>
1. Models altına bir *"Dtos"* klasörü açılır.
2. Yapmak istediğimiz işlemde ihtiyacımız olan alanları karşıalmak üzere bir DTO nesnesi oluşturuşlur.

        public class CategoryDto
        {
            [Required]
            public Guid Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string Description { get; set; }
        }

3. Mimaride uygun bir yere *"Mapper"* klasörü açılır.
4. Açılan "Mapper" klasörünün altına ise ilgili entityNameMapping.cs file açılır ve bu class içerisinde mapping işlemi gerçekleştirilir. *Dikkat: Açılan bu sınıf "Profile" sınıfıyla extend edilir.* Aşağıdaki kod bloğunu inceleyiniz:

        public class CategoryMapping:Profile
        {
            public CategoryMapping()
            {
                CreateMap<Category, CategoryDto>().ReverseMap();
            }
        }

5. Son olarak yapılan Mapping işlemi middleware oalrak eklenir.

        services.AddAutoMapper(typeof(CategoryMapping));

*Doc:* https://automapper.org

## Swasbuckle

Swagger: eliştiriciler için RestFul web hizmetlerini tasarlamasına, oluşturmasına, belgelemesine yardımcı olan bir araçtır. Geniş bir ekosistem tarafından desteklenen açık kaynak bir uazılım framework'tür. Swagger üç ana alanda geliştiricilere yardımcı olur. Bunlar:
1. API Geliştirme : API oluşturulurken, Swager aracı kodun kendisine göre otomatik bir açık API belgesi oluşturmak için kullanılıyor. Bu Apı açıklaması bir projenin kaynak koduna gömülür. Alternatif olarak Swagger Codegen kullanılarak geliştiriciler kaynak kodun Open API belgesinden ayırabilir ve doğrudan istemci kodu ve dokumantasyonu oluşturulabilir.
2. API'lerle Etkileşim Kurma: Swagger Codegen projesini kullanarak son kullanıcılar istemci SDK'larını doğrudan Open Apı belgelsinden oluşturur ve istemci tarafından gelen yada oluşturulan istemci kodu gereksinimleri azaltırılır.
3. API'ler İçin Dokümantasyon: Bir API için OpenApı belgesi tanımlandığındai Swagger açık kaynak aracı,i Swagger kullanıcı arayüzü üzerinden API ile doğrudan etkileşim kurmak için kullanılır. Html tabanlı bir kullanıcı arabirimi aracılığı iloe doğudan canlı API'lere bağlantı kurulmasını sağlar. Client istekleri doğrudan Swagger UI üzerinden gerçekeştirebilir.

#### Swasbuckle Implementation

1. **Package Name:** Swashbuckle.AspNetCore - **Version:** 5.5.0 ilgili paket *Nuget Pacake Manager* aracılığı ile indirilir.
2. Aşağıdaki ayarlamalar Startup.Cs üzerinede yapılır. 
  2.1. **ConfigureServices** metoduan aşağıdaki kodlar eklenir. Aşağıdaki kod bloğunu incelediğimizde oluşturduğumuz API ile ilgili documantation ayarlamalarının yapıldığının farkına varacaksınız.
  
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("RestFulAPISpec", new OpenApiInfo()
                {
                    Title = "Restful API",
                    Version = "V.1",
                    Description = "Restful API",
                    Contact = new OpenApiContact()
                    {
                        Email = "burak.yilmaz@bilgeadam.com",
                        Name = "Burak Yilmaz",
                        Url = new Uri("https://github.com/Burakkylmz"),
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }
                });
                
                // API'ın sahib olduğu yetenekler yani Controller içerisindeki Action Metodlarımıza yazdığımız summary yani özet bilgilerin Swagger UI aracında gözümesi için yapılan bir konfigurasyon.
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommnetFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(xmlCommnetFullPath);
             }
             
  2.2. Aşağıdaki kodları **Configure** metoduna uygulayınız. Aşağıdaki ko
            
            app.UseSwagger();
            
            //Proje çalıştırıldığında Swagger UI'ın gelemsi için yapılmıştır.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/RestfulAPISpec/swagger.json", "Restful API");
                //options.RoutePrefix = String.Empty;
            });
  

*Swagger Aracını Nuget Package Manager yada GitHub üzerinden erişebilir ve projenize entegre edebilirilsniz.* <br>
*Github Link:* https://github.com/swagger-api/swagger-ui <br>
*Nuget Package Name:* Swashbuckle.AspNetCore <br>
*Doc:* https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio <br>

## Json Web Token (Jwt)

JSON Web Token (JWT), taraflar arasında bilgileri JSON nesnesi olarak güvenli bir şekilde iletmenin kompakt ve bağımsız bir yolunu tanımlayan açık bir standarttır (RFC 7519). Bu bilgiler dijital olarak imzalandığından doğrulanabilir ve güvenilebilirdir. JWT'ler bir secret (HMAC algoritması ile) veya RSA veya ECDSA kullanılarak bir genel / özel anahtar çifti kullanılarak imzalanabilir.

JWT'ler taraflar arasında gizlilik sağlamak için şifrelenebilse de, bu uygulamada imzalı tokenlere odaklanacağız ve onları authentication esnasında üreteceğiz. İmzalı belirteçler, içerdiği istemlerin bütünlüğünü doğrularken, şifreli belirteçler bu talepleri diğer taraflardan gizler. Belirteçler genel / özel anahtar çiftleri kullanılarak imzalandığında, imza yalnızca özel anahtarı tutan tarafın onu imzalayan taraf olduğunu da onaylar. 

#### Jwt Implementation

1. AppSettings.cs açılır.

        public class AppSettings
        {
            public string SecretKey { get; set; }
        }
        
2. appsettings.json dosyasına aşağıdaki kod bloğunu ekleyin

          "AppSettings": { "SecretKey": "This is used to sign in and verify jwt tokens"}

3. Startup.cs dosyası altında bulunan ConfigureService metodunu içerisinde gerekli düzenlemeleri yapın

          var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });
 4. Son olarak Swagger UI üzerinde kullandığımız security bilgilerinin gözükmesi ve gerekli açıklamaların API kullanıcılarına dokümantre edilmesi için aşağıdaki kodları *"AddSwaggerGen()"* middleware ekleyin. Bu middleware üzerinde bazı eklendiler Swagger implemantation esnasında yapmıştık. Şİmdi son halini vereceğiz.
 
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("RestFulAPISpec", new OpenApiInfo()
                {
                    Title = "Restful API",
                    Version = "V.1",
                    Description = "Restful API",
                    Contact = new OpenApiContact()
                    {
                        Email = "burak.yilmaz@bilgeadam.com",
                        Name = "Burak Yilmaz",
                        Url = new Uri("https://github.com/Burakkylmz"),
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Autherization header using Bearer scheme",
                    Name = "Autharization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Brear"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth 2.0",
                            Name = "bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommnetFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(xmlCommnetFullPath);
            });
 
*Doc:* https://jwt.io/introduction/
