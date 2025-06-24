# 📦 Full Stack Engineer Technical Assessment

This project is a full-stack web application for managing products and categories using:

- **Frontend**: Angular
- **Backend (Products)**: .NET 8 Web API
- **Backend (Categories)**: Django REST API

---

## 🗂️ Project Structure

```
/TechnicalAssessment
├── TechnicalAssessment.Web          # .NET Web API (Product Service)
├── TechnicalAssessment.Core         # Class Library for Models, Services, Interfaces
├── TechnicalAssessment.Tests        # xUnit Test Project

/category-api                        # Django Category API
/frontend                            # Angular Frontend
```


---

## ⚙️ Setup Instructions

### 1. Backend - ASP.NET Core API (`TechnicalAssessment`)

- Navigate to the root directory:
  ```bash
  cd TechnicalAssessment
  ```

- Restore dependencies and run the API:
  ```bash
  dotnet restore
  dotnet run --project TechnicalAssessment.Web
  ```

#### 📌 Notes

- SQLite used for simplicity (`ProductsDb.sqlite`)
- Swagger available at: `https://localhost:<port>/swagger`

---

### 2. 🐍 Django REST API (Python) – `category-api/`

**Tech Stack**: Django, Django REST Framework, SQLite

#### ✅ Setup

```bash
cd category-api
python -m venv env
source env/bin/activate        # On Windows: env\Scripts\activate
pip install -r requirements.txt

# Apply migrations and seed data
python manage.py seed

# Start server
python manage.py runserver
```

#### 📌 Notes

- Category data is pre-seeded using a fixture or custom management command.
- API available at: `http://127.0.0.1:8000/api/categories`

---

### 🟢 Angular Frontend 

**Tech Stack**: Angular 17+, SCSS (custom styling)

#### ✅ Setup

```bash
npm install
ng serve
```

- App will be available at `http://localhost:4200`.


#### 🔗 API Endpoints (Expected)

Update `environment.ts` with:

```ts
export const environment = {
  production: false,
  productApi: 'https://localhost:<product-api-port>/api/product',
  categoryApi: 'http://127.0.0.1:8000/api/categories/'
};
```

---

## ✅ Features

- 🌐 Product list with category mapping
- ➕ Product creation form (with category dropdown)
- 🔁 Real-time updates after adding product

---

## 🚀 Running the Full App

1. Start the `.NET API` (`dotnet run`)
2. Start the `Django API` (`python manage.py runserver`)
3. Start the `Angular Frontend` (`ng serve`)
4. Open `http://localhost:4200`

---

## 📥 API Contract (Sample Response)

### 🟢 Product API

```json
{
  "payload": [
    {
      "id": "uuid",
      "name": "Laptop",
      "description": "Gaming laptop",
      "price": 1500.0,
      "categoryId": "uuid"
    }
  ],
  "totalCount": 0,
  "errors": [],
  "hasErrors": false,
  "code": 1,
  "description": "SUCCESS"
}
```

### 🔵 Category API

```json
[
  {
    "id": "uuid",
    "name": "Electronics",
    "description": "Gadgets and devices"
  }
]
```