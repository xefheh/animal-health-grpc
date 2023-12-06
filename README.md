# animal-health-grpc

## 1. URL Контейнеров:

| URL | Описание |
|--|---|
| https://localhost:5432 | Контейнер СУБД PostgreSQL |
| https://localhost:5050 | Контейнер pgAdmin 4 |
| https://localhost:8080 | Контейнер приложения ASP.NET |
## 2. Запуск:

- В IDE (Visual Studio/Rider) выбрать как исходно-запускаемый файл - docker-compose.yml.
![изображение](https://github.com/xefheh/animal-health-grpc/assets/114147391/7d600978-d9ef-406e-9b7a-c93f4c8a1972)


### Структура docker-compose.yml:
<details>
  <summary>Раскрыть</summary>
  
```yml
services:
  animalhealth.api:
    image: animalhealth.api
    build:
      context: .
      dockerfile: AnimalHealth.API/Dockerfile
    depends_on: 
      - postgres.db
    environment:
      ConnectionStrings__DefaultConnection: "Host=postgres.db;Database=animalHealth;Username=postgres;Password=123"
    networks:
      - network
      
  postgres.db:
    restart: always
    image: postgres:latest
    environment:
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=123
      - POSTGRES_DB=animalHealth
    ports:
      - "5432:5432"
    networks:
      - network
    volumes:
      - ./AnimalHealth.API/SqlInitialize/sql_initialize.sql:/docker-entrypoint-initdb.d/init.sql
  
  pgadmin:
    image: dpage/pgadmin4:latest
    restart: always
    environment:
      PGADMIN_DEFAULT_EMAIL: admin@admin.com
      PGADMIN_DEFAULT_PASSWORD: 123
    ports:
      - "5050:80"
    depends_on: 
      - postgres.db
    networks:
      - network

networks:
  network:
```
</details>

## 3. Подключение к серверу в pgAdmin 4:
### Шаг 1:
- После запуска контейнеров переходим по URL: https://localhost:5050
  
  ![изображение](https://github.com/xefheh/animal-health-grpc/assets/114147391/d57c4952-328d-4907-9583-94655f0a352b)

- В поле email address вводится: admin@admin.com
- В поле password вводится: 123
  
 ![изображение](https://github.com/xefheh/animal-health-grpc/assets/114147391/969b0d45-4033-4e67-8f52-3b89bf30ea2a)

- На вкладку Servers нажимается ПКМ -> Register -> Server..
  
 ![изображение](https://github.com/xefheh/animal-health-grpc/assets/114147391/c1872765-3da3-47fa-a3e2-d993ddb4ab55)

- В поле name вводится любое слово (в примере: server)
  
 ![изображение](https://github.com/xefheh/animal-health-grpc/assets/114147391/9ffae3d4-23c4-4a7d-aba3-9270db7533d4)

- Переходим на вкладку Connection
- В поле host name вводится название контейнера с postgreSQL (postgres.db)
- В поле username вводится: postgres
- В поле password вводится: 123
- Нажимается кнопка save

![изображение](https://github.com/xefheh/animal-health-grpc/assets/114147391/c32a0471-bd19-4853-b88b-3280d497364c)

- Сервер появляется в списке серверов

![изображение](https://github.com/xefheh/animal-health-grpc/assets/114147391/7376293f-d39a-4301-a4bf-ace579d5879b)

## 4. Тестовые таблицы:


