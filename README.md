# Magazine Conveyor

A WPF application for managing and finding free positions in a piece magazine with configurable parameters.

## Problem Description

The application solves the problem of finding available positions in a magazine with "n" positions where some positions are occupied. The magazine can operate in two modes:

- **Linear**: Positions are arranged in a straight line (positions 0 to n-1)
- **Rotary**: Positions form a circle where the last position is adjacent to position 0

The core functionality is to find the first available position that can accommodate a workpiece of a specified size (requiring multiple consecutive positions).

## Features

### Core Functionality
- **FindFreePlace Algorithm**: Efficiently finds the first available position for a workpiece requiring multiple consecutive positions
- **Linear and Rotary Modes**: Support for both linear and rotary magazine configurations
- **Interactive UI**: WPF application for visualizing and testing the algorithm

### Technical Implementation
- ✅ **MVVM Pattern**: Clean separation of concerns using Model-View-ViewModel architecture
- ✅ **Dependency Injection**: Service-based architecture with `FindFreePlaceService` injected as a dependency
- ✅ **Unit Tests**: Comprehensive test coverage with NUnit
- ✅ **Custom WPF Controls**: `CircularPanel` for visual representation of rotary magazines

## Algorithm Examples

With a 12-position magazine where positions 1, 2, and 3 are occupied:

| Configuration | Result |
|--------------|--------|
| `isRotary = false, neededPlaces = 6` | Returns `-1` (not enough consecutive free positions) |
| `isRotary = false, neededPlaces = 4` | Returns `5` (positions 5-8 are available) |
| `isRotary = false, neededPlaces = 5` | Returns `-1` (not enough consecutive free positions) |
| `isRotary = true, neededPlaces = 5` | Returns `10` (positions 10-11 and 0 wrap around) |

## Building and Running

### Prerequisites
- .NET 8.0 SDK
- Windows OS (for WPF support)

### Build
```bash
dotnet build Converyor_magazine.sln
```

### Run
```bash
dotnet run --project Magazine-Conveyor/Magazine-Conveyor.csproj
```

### Run Tests
```bash
dotnet test
```

## Usage

1. **Configure Magazine**:
   - Set the number of positions
   - Toggle "Is Rotary" checkbox for rotary mode
   - Click "Update magazine" to apply changes

2. **Select Occupied Positions**:
   - Click on positions in the visual representation to mark them as occupied/free
   - Dark positions indicate occupied, light positions indicate free

3. **Find Free Place**:
   - Enter the number of needed places
   - Click "Find free place" to execute the algorithm
   - The first available position will be highlighted in green

## Testing

The project includes comprehensive unit tests covering:
- ✅ Single free place scenarios
- ✅ Multiple free places
- ✅ All free places / all needed
- ✅ Rotary mode with border wrapping
- ✅ Negative scenarios (no solution found)
- ✅ Edge cases (empty arrays, invalid inputs)

Run all tests with:
```bash
dotnet test Magazine-Conveyor/tests/UnitTests.csproj
```

## Architecture

### MVVM Pattern (Properly Implemented)

The application follows a strict **Model-View-ViewModel (MVVM)** pattern with clear separation of concerns:

#### **Model Layer** (`src/`)
- **`Magazine.cs`**: Pure business logic and data model
  - No UI concerns (no `INotifyPropertyChanged`, no WPF references)
  - Contains the core `FindFreePlace` algorithm
  - Manages position data and magazine state
  - Provides methods like `UpdatePositionsVisibility()` and `UpdatePossitionsOccupancy()`

- **`Position.cs`**: Represents individual magazine positions with UI binding capability
  - Implements `INotifyPropertyChanged` for view updates
  - Properties: `IsVisible` (visibility), `IsChecked` (occupancy status)

#### **ViewModel Layer** (`src/`)
- **`MagazineViewModel.cs`**: Mediates between View and Model
  - Implements `INotifyPropertyChanged` for data binding
  - Exposes properties: `PositionCount`, `IsRotary`, `NeededPlaces`, `LastFoundPosition`
  - Implements `ICommand` properties:
    - `UpdateMagazineCommand`: Updates magazine configuration
    - `FindFreePlaceCommand`: Executes the find algorithm
  - Contains all business logic orchestration (ViewModel responsibility)
  - Creates and manages the Model and Service instances

- **`RelayCommand.cs`**: Implementation of `ICommand` interface
  - Generic and non-generic versions for flexible command binding
  - Enables buttons to execute commands without code-behind event handlers

#### **View Layer**
- **`MainWindow.xaml`**: Pure XAML UI definition
  - Data bindings to ViewModel properties
  - Command bindings to ViewModel commands
  - Contains only presentation logic
  - No code-behind event handlers (replaced by commands)

- **`MainWindow.xaml.cs`**: Minimal code-behind
  - Sets `DataContext` to `MagazineViewModel` instance
  - No business logic

#### **Service Layer** (`src/`)
- **`FindFreePlaceService.cs`**: Implements `IService` interface
  - Injected into ViewModel
  - Encapsulates the algorithm execution
  - Acts as a bridge between ViewModel and Model

### Dependency Injection Flow

```
MainWindow
    ↓
MagazineViewModel (DataContext)
    ├─→ Magazine (Model)
    └─→ FindFreePlaceService (Service)
            └─→ Magazine (via IMagazine interface)
```

### Data Binding Flow

1. **View** binds to **ViewModel** properties
2. **ViewModel** notifies View of property changes via `INotifyPropertyChanged`
3. **ViewModel** delegates business logic to **Model** and **Service**
4. **Model** remains independent of UI concerns

### Command Execution Flow

1. **User** clicks button in **View**
2. **Button** executes **ViewModel Command**
3. **ViewModel** orchestrates operation by calling **Service**
4. **Service** executes logic on **Model**
5. **ViewModel** updates properties
6. **View** automatically updates via data binding

## License

See [LICENSE](Magazine-Conveyor/LICENSE) file for details.
