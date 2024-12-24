# Expense Tracker CLI Application

## Overview
The Expense Tracker is a command-line application written in C# that helps you manage your personal finances. You can add, delete, and view your expenses and summaries of your spending. The data is persisted in a JSON file, allowing for easy retrieval and updates.

## Features
1. **Add Expenses**: Add an expense with a description and amount.
2. **List Expenses**: View all the stored expenses.
3. **Summary**:
   - View total expenses.
   - View expenses for a specific month.
4. **Delete Expenses**: Remove an expense by its ID.
5. **Data Persistence**: Expenses are saved in a JSON file for future use.

## Prerequisites
- .NET SDK (Version 5.0 or later)
- Newtonsoft.Json library
  - Install using NuGet: `Install-Package Newtonsoft.Json`

## Usage
### Add an Expense
```bash
expense-tracker add --description "Lunch" --amount 20
```
- Adds an expense with the given description and amount.
- Returns the ID of the added expense.

### List All Expenses
```bash
expense-tracker list
```
- Displays all expenses in a tabular format.

### View Summary
#### Total Summary
```bash
expense-tracker summary
```
- Displays the total amount of all expenses.

#### Monthly Summary
```bash
expense-tracker summary --month <month_number>
```
- Displays the total expenses for a specific month of the current year.
- Example: `expense-tracker summary --month 8` (for August).

### Delete an Expense
```bash
expense-tracker delete --id <expense_id>
```
- Deletes the expense with the specified ID.

## Example Commands
```bash
# Add expenses
expense-tracker add --description "Lunch" --amount 15
expense-tracker add --description "Groceries" --amount 50

# List expenses
expense-tracker list

# View total summary
expense-tracker summary

# View August summary
expense-tracker summary --month 8

# Delete an expense by ID
expense-tracker delete --id 1
```

## File Structure
- **Program.cs**: Main application logic.
- **expenses.json**: JSON file used to store expenses data.

## Error Handling
- Invalid inputs (e.g., negative amounts, invalid IDs) are handled with appropriate error messages.
- Missing arguments will prompt the user with the correct usage.

## Future Enhancements
- Add expense categories and filtering by category.
- Set monthly budgets with warnings when exceeded.
- Export expenses to a CSV file.

## Dependencies
- Newtonsoft.Json: For JSON serialization and deserialization.

## Installation
1. Clone the repository.
2. Install the required dependencies using NuGet:
   ```bash
   Install-Package Newtonsoft.Json
   ```
3. Build and run the application using the .NET CLI or Visual Studio.

