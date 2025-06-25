---

### ✅ README – Backend (.NET C#)

```markdown
# Soil Contaminant Guideline Manager – Backend

This is the backend for the **Soil Contaminant Guideline Manager App**, developed using **ASP.NET Core Web API** and **C#**. It provides endpoints for managing contaminants and analyzing measured values against environmental guidelines.

---

## 🗂️ Project Structure

```bash
SoilGuidelineManagerAPI/
│
├── Controllers/         # API endpoints (/manage, /analyze)
├── Models/              # Entity and DTO classes
├── Services/            # Business logic and helpers
├── Data/                # Database context
├── Program.cs           # Main entry point
└── appsettings.json     # Configuration settings

---

## 📌 Features

- **/manage route**:
  - `POST /manage/contaminant`: Add contaminant guideline values
  - `PUT /manage/contaminant/{id}`: Update contaminant data
  - `DELETE /manage/contaminant/{id}`: Delete contaminant

- **/analyze route**:
  - `POST /analyze`: Accepts a measured value and returns:
    - `IsCompliant` (bool)
    - `GuidelineValue` (lowest value)
    - `ExceedingPathways` (list of pathways)

---

## 🧪 Sample API Payload

**Request**
```json
{
  "Contaminant": "Benzene",
  "MeasuredValue": 1.1,
  "SoilType": "Coarse",
  "Pathways": ["Direct Soil Contact", "Protection of Wildlife Water"]
}

**Response***
```json
{
  "IsCompliant": false,
  "GuidelineValue": 0.078,
  "ExceedingPathways": [
    "Protection of Wildlife Water",
    "Protection of Freshwater Aquatic Life",
    "Protection of Domestic Use Aquifer"
  ]
}

🛠️ Technologies Used
	•	ASP.NET Core 7.0
	•	C#
	•	Entity Framework Core (Code-First)
	•	SQL Server (or SQLite for lightweight dev)
	•	Swagger (for API testing/documentation)

📦 Setup Instructions
	1.	Clone the repository:
    git clone https://github.com/your-username/soil-contaminant-manager-backend.git
    cd soil-contaminant-manager-backend

	2.	Configure your database connection in appsettings.json.

	3.	Run database migrations:
    dotnet ef database update

	4.	Start the API:
    dotnet run

	5.	API will be available at: https://localhost:5001 (or as configured)

🧪 Testing
	•	Use Swagger UI at /swagger to test API endpoints.

☁️ Hosting
	•	Deployment-ready for AWS
