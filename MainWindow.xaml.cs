using System.Diagnostics;
using System.Text;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeManager;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    private string? Contract;

    public MainWindow() {
        InitializeComponent();
        Contract = "Umowa na czas nieokreślony";
    }

    private void AddEmployee(object sender, RoutedEventArgs e) {
        Debug.WriteLine($"Imie: {FirstName.Text}, Nazwisko: {LastName.Text}, DateOfBirth: {DateOfBirth.Text},  Salary: {Salary.Text}, Position: {Position.Text}, Contract: {Contract}");
        Debug.WriteLine("Employee has been added");
    }
    private void SaveChangedEmployee(object sender, RoutedEventArgs e) {
        Debug.WriteLine("Employee has been saved");
    }
    private void SaveEmployees(object sender, RoutedEventArgs e) {
        Debug.WriteLine("Employees has been saved");
    }
    private void LoadEmployees(object sender, RoutedEventArgs e) {
        Debug.WriteLine("Employees has been loaded");
    }

    private void SetEmployeeContract(object sender, RoutedEventArgs e) {
        RadioButton option = (RadioButton) sender;
        Contract = option.Content?.ToString();
    }
}