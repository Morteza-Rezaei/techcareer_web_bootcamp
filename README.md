# Kitaplık Yönetim Sistemi (Library Management System)

projeyi çalıştırmadan önce lüffen bilgisayarınızda .NET SDK'nın yüklü olduğundan emin olunuz.

Projeyi çalıştırmak için aşağıdaki adımları takip edebilirsiniz.

### 1. Veritabanı Oluşturma

SQL Server üzerinde bir veritabanı oluşturun ve aşağıdaki sorguyu çalıştırarak `Books` tablosunu ekleyin:

```sql
CREATE TABLE Books
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    Author NVARCHAR(100),
    Category NVARCHAR(200),
    PublishedYear INT,
    IsAvailable BIT,
    Description NVARCHAR(MAX)
);
```

sonra connection stringi düzenleyin.
şuan ki connection stringi:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=projects;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 2. Projeyi Çalıştırma

Editörünüzü açın ve terminali açın. Aşağıdaki komutu çalıştırarak projeyi çalıştırabilirsiniz:

```bash
dotnet run
```

Projeyi çalıştırdıktan sonra tarayıcınızda size verilen linki açarak projeyi kullanmaya başlayabilirsiniz.

# 👋✌️
