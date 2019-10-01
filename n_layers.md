# Layers 

  UI Layer yada Prensatation Layer (MVC)
  
  Models Layer
  
  Business Logic Layer(BLL)
  
  Database Access Layer (DAL)
  
  Database


## Flow


    1)Model -->
  
    1.a)BLL
    
    1.b)MVC Project(Controller) --> BLL
    
    1.b.1)BLL --> DAL
    
    1.b.2)DAL --> Database
  
## Models Layer --> Controller or BLL
 
## Models Layer or Controller --> BLL  
  
  Dosya Adı : 
  ProductEntity
  
  Hem Controller hem Business Logic Layer aynı Modeli Kullanacak.
  
## Business Logic Layer --> DAL
  
  Dosya Adı : ProductBusiness
  
  ProductEntitynin CRUD işlemleri ve varsa diğer işlemlerinin business logic'ini oluşturur.
  Örneğin Bu ay ki satış miktarını getir gibi.
  
  
  
## Database Access Layer(DAL or DLL) --> DATABASE
  
  Business Logic Layer da ProductBusiness'ın INSERT,UPDATE,DELETE Gibi Crud işlemlerini veritabanı tarafında yapar.


