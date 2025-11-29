# Simple POS (Point-of-Sale) System – C# WinForms

A basic Point-of-Sale (POS) application created using C# and Windows Forms (WinForms).  
This project is developed as a requirement for the *Programming Languages* subject and demonstrates GUI programming, event handling, and simple computations in C#.

---

## 📖 Project Overview

This POS System allows users to:

- Select a product from a dropdown menu
- Enter quantity
- Compute total price automatically (including 12% VAT)
- Display the result on the main window
- Generate a text receipt
- Open the receipt in a separate popup window

The project uses common WinForms components such as `ComboBox`, `TextBox`, `Button`, and additional Form windows.

---

## 🛠️ Technologies Used

| Component              | Details                    |
|------------------------|----------------------------|
| Programming Language   | C#                         |
| Framework              | .NET (Windows Forms)       |
| IDE                    | Visual Studio / VS Code    |
| Platform               | Windows Desktop            |

---

## 🎮 How the Program Works

### 1. Select a Product  
Choose from sample products such as Coffee, Milk Tea, Burger, Fries, etc.

### 2. Enter Quantity  
User inputs the number of items to purchase.

### 3. Calculate Total  
The program computes:

`Total = (Unit Price × Quantity) + VAT (12%)`

### 4. Generate Receipt  
A new window displays the formatted receipt including:

- Item name  
- Unit price  
- Quantity  
- VAT amount  
- Total amount  
- Date & time  

---

## 📁 Project Structure

📦 SimplePOS
-├── ▶️ Program.cs
-├── 📝 Form1.cs
-├── 🧩 Form1.Designer.cs
-├── 🧾 ReceiptForm.cs
-├── 🧱 ReceiptForm.Designer.cs
-└── 📘 README.md


---

## 🎯 Learning Objectives

This project demonstrates:

- Basic C# syntax and structure  
- GUI development using WinForms  
- Event handling for button clicks  
- Using multiple forms  
- Passing data between forms  
- Applying arithmetic operations  
- Displaying formatted output  
- Understanding object-oriented programming concepts  

---

## 🚀 How to Run the Project

### **Option 1 — Visual Studio (Recommended)**
1. Open the solution or project folder.
2. Click **Start / Run**.
3. The POS window will appear.

### **Option 2 — Command Line**
dotnet build
dotnet run


---

## 👤 Author

**Charisse Priego**  
BS Computer Science
Programming Languages — Final Requirement  

---

## ✔️ Status

The project is complete and ready for submission.

