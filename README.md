English:

This code retrieves data from the "Citizens" table in the SSMS MySQL server, formats it for insertion in phpMyAdmin,
and saves it to a file named "data1.sql". This enables performing table insert operations in phpMyAdmin by using the import feature.
A notable advantage is avoiding the limitation where SQL structures often cannot handle more than 1000 insert operations simultaneously.
For instance, I successfully tested it with 15,000 data entries. To use this code, customize it according to your table structure in phpMyAdmin.

Türkçe:

Bu kod, SSMS MySQL sunucusundaki "Citizens" adlı tablodan veri çeker, phpMyAdmin'e eklemek için uygun bir formatta düzenler ve "data1.sql"
adlı bir dosyaya kaydeder. Bu sayede, phpMyAdmin'de import özelliğini kullanarak tablo ekleme işlemleri gerçekleştirilebilir.
Dikkat çeken bir avantaj, genellikle SQL yapılarının aynı anda 1000'den fazla ekleme işlemiyle başa çıkamaması sorununu aşmaktadır.
Örneğin, ben 15,000 veri girişi eklemeyi başarıyla test ettim. Bu kodu kullanmak için, phpMyAdmin'deki tablo yapınıza göre özelleştirmeniz gerekmektedir.
