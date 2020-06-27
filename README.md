# RestFul-API
In this repository, I developed a RestFul API with Asp .Net Core technology. In this project, you can find the implementation of Swashbuckle, AutoMapper and Autofact structures.

#### Automapper Implementation

AutoMapper, karmaşık bir sournu çözmek için tasarlanmış basit ve küçük bir kütüphanedir. Temelde üstlendiği görev gayet yalındır, bir nesneyi diğer bir nesne ile eşleyen koddan kurtulmak için tercih ettiğimiz bir yapıdır. Bu tür bir eşleşme kodunu yazmak oldukça zahmetli ve sıkıcıdır, örneğin 2 ayrı tabloda tutuğumuz category ve product bilgilerini getirmek istiyoruz. Bu işlemi yerine getirebilmek için bir veri taransfer nesnesi (DTO)  oluşturduğumuzu ve ihtiyacımızı karşılayacak özellikleri burada hayata geçirdiğimizi düşünelim. Bu senaryoda yapmamız gereken diğer bir işlem ise Linq to Entitiy sorgusu yazmak ve ihtiyacımız olan alanlar ile DTO'muzdaki alanları birbirlerine eşitlememiz gerekmektedir. Burada işlem kulağa gayet basit gelebilir özellikle örneğini verdiğimiz senaryoda lakin yaptığımız işler büyüdükçe select kısmında elle tek tek yazacağımız kodlarda yapılan iş ile eş zamanlı olarak büyükmektedir ve bu işlem epey zahmetli olacak ve kod okunabilirliği zafiyeti oluşarak, sorgu anlaşılabilirliği kaybolacaktır. Bu gibi durumlarda yapılan işeleme özel Data Transfer Object'leri yazıp bu objeleri entity'lerimiz ile automap aracı ile mapping işlemini yaparak işlerimizi kolaylaştırabilriz. <br>

*Doc:* https://automapper.org

**Implemantation:**
1. İlk önce Nuget Package Mager aracılığı ile ihtiyaç duyulan paketleri yükelyelim: <br>
  1.1. AutoMapper - Version: 9.0.0 <br>
  1.2. AutoMapper.Extensions.Microsoft.DependencyInjection - Version: 7.0.0 <br>
1. Models altına bir *"Dtos"* klasörü açılır.
2. Yapmak istediğimiz işlemde ihtiyacımız olan alanları karşıalmak üzere bir DTO nesnesi oluşturuşlur.
3. Mimaride uygun bir yere *"Mapper"* klasörü açılır.
4. Açılan "Mapper" klasörünün altına ise ilgili entityNameMapping.cs file açılır ve bu class içerisinde mapping işlemi gerçekleştirilir.

#### Swasbuckle Implementation

Swagger: eliştiriciler için RestFul web hizmetlerini tasarlamasına, oluşturmasına, belgelemesine yardımcı olan bir araçtır. Geniş bir ekosistem tarafından desteklenen açık kaynak bir uazılım framework'tür. Swagger üç ana alanda geliştiricilere yardımcı olur. Bunlar:

1. API Geliştirme : API oluşturulurken, Swager aracı kodun kendisine göre otomatik bir açık API belgesi oluşturmak için kullanılıyor. Bu Apı açıklaması bir projenin kaynak koduna gömülür. Alternatif olarak Swagger Codegen kullanılarak geliştiriciler kaynak kodun Open API belgesinden ayırabilir ve doğrudan istemci kodu ve dokumantasyonu oluşturulabilir.

2. API'lerle Etkileşim Kurma: Swagger Codegen projesini kullanarak son kullanıcılar istemci SDK'larını doğrudan Open Apı belgelsinden oluşturur ve istemci tarafından gelen yada oluşturulan istemci kodu gereksinimleri azaltırılır.

3. API'ler İçin Dokümantasyon: Bir API için OpenApı belgesi tanımlandığındai Swagger açık kaynak aracı,i Swagger kullanıcı arayüzü üzerinden API ile doğrudan etkileşim kurmak için kullanılır. Html tabanlı bir kullanıcı arabirimi aracılığı iloe doğudan canlı API'lere bağlantı kurulmasını sağlar. Client istekleri doğrudan Swagger UI üzerinden gerçekeştirebilir.

*Swagger Aracını Nuget Package Manager yada GitHub üzerinden erişebilir ve projenize entegre edebilirilsniz.* <br>
*Github Link:* https://github.com/swagger-api/swagger-ui <br>
*Nuget Package Name:* Swashbuckle.AspNetCore <br>
*Doc:* https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio <br>
