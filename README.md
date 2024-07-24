# FinanceClient

FinanceClient is a powerful WPF application designed to track and manage currency data. It uses Syncfusion for data visualization and the CommunityToolkit for MVVM support. The application features a clean architecture with well-defined services, view models, and data management components.
![first-preview](https://github.com/Egxr41k/FinanceClient/blob/master/FinanceClient/Assets/first-preview.jpg?raw=true)
![second-preview](https://github.com/Egxr41k/FinanceClient/blob/master/FinanceClient/Assets/second-preview.jpg?raw=true)

## Features

### 1. Data Visualization
- **Syncfusion WPF:** Utilizes Syncfusion controls for rich and interactive data visualization.

### 2. MVVM Architecture
- **CommunityToolkit MVVM:** Employs the CommunityToolkit MVVM framework for a robust and maintainable application structure.

### 3. Logging
- **Content of Log.txt on MainWindow:** all actual application log is available in SettingsPage or via FileExplorer

### 4. Data Storage and Retrieval
- **XML, JSON, CSV Data Storage:** The application supports storing and retrieving currency data in XML, JSON, and CSV formats, ensuring flexibility and ease of use.

### 5. API Integration
- **CoinCap and NBU APIs:** The application can fetch currency data from CoinCap and NBU APIs, providing real-time updates.

### 6. Configuration Handling
```json
{
  "UsedApi": "CoinCap",
  "AvailableCurrencyCount": 10,
  "FetchingIntervalMin": 5,
  "FormatOfSaving": "JSON"
}
```
### 7. Custom Control Codes: 
```csharp
public readonly Dictionary<string, SettingsDTO> CustomControlCodes = new()
{
    { "DEFAULT", new SettingsDTO() },
    { "FAST_FETCH", new SettingsDTO 
        { 
            UsedApi = "CoinCap", 
            AvailableCurrencyCount = 5, 
            FetchingIntervalMin = 1, 
            FormatOfSaving = "JSON" 
        } 
    },
    { "CSV_STORAGE", new SettingsDTO 
        { 
            UsedApi = "NBU_Exchacnge", 
            AvailableCurrencyCount = 10, 
            FetchingIntervalMin = 5, 
            FormatOfSaving = "CSV" 
        } 
    },
    { "XML_STORAGE", new SettingsDTO 
        { 
            UsedApi = "NBU_Exchacnge", 
            AvailableCurrencyCount = 10, 
            FetchingIntervalMin = 3, 
            FormatOfSaving = "XML" 
        } 
    },
    { "EXTENDED_FETCH", new SettingsDTO 
        { 
            UsedApi = "CoinCap", 
            AvailableCurrencyCount = 10, 
            FetchingIntervalMin = 10, 
            FormatOfSaving = "JSON" 
        } 
    }
};
```
## Folder Structure

### FinanceClient
- **Stores:** Contains classes that manage application state.
  - `SelectedModelStore.cs`
  - `FinanceClientStore.cs`

- **ViewModels:** Contains view model classes that handle the presentation logic.
  - `DetailsViewModel.cs`
  - `FinanceClientViewModel.cs`
  - `InfoViewModel.cs`
  - `ListingItemViewModel.cs`
  - `ListingViewModel.cs`
  - `MainViewModel.cs`
  - `SettingsViewModel.cs`

- **Views:** Contains view files that represents the UI
  - `DetailsView.xaml`
  - `FinanceClientView.xaml`
  - `InfoView.xaml`
  - `ListingView.xaml`
  - `MainView.xaml`
  - `SettingsView.xaml`

### FinanceClient.Data
- **ApiClients:** Contains classes that interact with external APIs.
  - `CoinCapClient.cs` Fetches currency data from the CoinCap API.
  - `IApiClient.cs` Interface for API clients, ensuring a consistent contract for fetching currency data.
  - `NbuClient.cs` - Fetches currency data from the NBU (National Bank of Ukraine) API.

- **Logging:** 
  - `LoggingService.cs` - Handles logging operations, capturing application events and errors.

- **Models:**
  - `CurrencyModel` - class represents the core data structure used to store information about each currency. The properties include:
    - **Id (int):** Unique identifier for the currency.
    - **Symbol (string):** Currency symbol (e.g., USD, EUR).
    - **Name (string):** Full name of the currency.
    - **Price (double):** Current price of the currency.
    - **ChangePercent (double):** Percentage change in the currency's value.
    - **Link (string):** Optional link for additional information.
    - **History (Dictionary<DateTime, double>):** Historical data of the currency's value over time.

- **Serializers:** 
  - `CsvSerializer.cs` - Handles serialization and deserialization of data in CSV format.
  - `ISerializer.cs` - Interface for serializers, ensuring a consistent contract for data serialization.
  - `JsonSerializer.cs` - Handles serialization and deserialization of data in JSON format.
  - `XmlSerializer.cs` - Handles serialization and deserialization of data in XML format.

- **Services:** Contains service classes that handle business logic and data processing.
  - `IFetchService.cs` - Interface for services that fetch data from external sources.
  - `JsonService.cs` - Implementation of `IFetchService` for JSON data.
  - `XmlService.cs` - Implementation of `IFetchService` for XML data.

- **Storages:** Contains classes for data storage management.
  - `CurrencyStorage.cs` -  Manages the storage and retrieval of currency data, supports multiple formats (JSON, XML, CSV).
  - `SettingsStorage.cs` - Manages application settings storage, also supports multiple formats (JSON, XML, CSV).
  - `Storage.cs` - Abstract class with common logic for CurrencyStorage and SettingsStorage

## Getting Started

### Prerequisites
Ensure you have the following installed:
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- A code editor such as [Visual Studio](https://visualstudio.microsoft.com/)

## Installation and Running 
```
https://github.com/Egxr41k/CryptoClient.git
cd CryptoClient
dotnet run
```
## Uninstallation
```powershell
Remove-Item -Recurse -Force .\FinanceClient\
```

