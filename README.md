# RabbitMQ Message Service

Basit düzeyde bir RabbitMQ uygulamasıdır.

Projeyi ayağa kaldırırken aynı anda "Consumer" ve "Api" projelerini ayağa kaldırmanız gerekmektedir.

"Api" projesi içerisindeki "appsettings.json" içerisinde gönderici e-posta ayarlarınızı girebilirsiniz.

"Consumer" projesi içerisindeki "MessageServiceExample.Consumer\bin\Debug\net6.0" içerisinde "settings.json" dosyası yer almalıdır. Eğer bu dosya yok ise siz oluşturabilirsiniz.
  - Dosya içerisinde yer alması gereken değerler arasındaki e-posta ayarlarını yine aynı şekilde yukarıda belirtilen "appsettings.json" içerisine tanımlayabilirsiniz.

RabbitMQ ayarları default olarak olan değerler ile bırakılmıştır. Dilerseniz değiştirebilirsiniz.

# settings.json
{
    "MailConfiguration": {
        "Host": "",
        "Port": "",
        "User": "",
        "Password": "",
        "SSL": false
    },
    "RabbitMqConfig": {
        "HostName": "localhost",
        "UserName": "guest",
        "Password": "guest"
    }
}
