using System.Diagnostics;
using System.Text;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EmployeeManager.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;

namespace EmployeeManager;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    private int pos { get; set; }

    private Team TeamMembers { get; set; }

    private string? Contract;

    public MainWindow() {
        InitializeComponent();
        TeamMembers = new Team();
        Contract = "Umowa na czas nieokreślony";
        EmployeesList.ItemsSource = TeamMembers;
    }

    private void AddEmployee(object sender, RoutedEventArgs e) {
        string contractType = (string) ContractType.Children.OfType<RadioButton>().FirstOrDefault(r => (bool)r.IsChecked).Content;
        TeamMembers.AddEmployee(new Employee(FirstName.Text, LastName.Text, DateOnly.Parse(DateOfBirth.Text), Salary.Text, Position.Text, contractType));
        Debug.WriteLine($"Imie: {FirstName.Text}, Nazwisko: {LastName.Text}, DateOfBirth: {DateOfBirth.Text},  Salary: {Salary.Text}, Position: {Position.Text}, Contract: {contractType}");
        Debug.WriteLine("Employee has been added");
    }
    private void EditEmployee(object sender, RoutedEventArgs e) {
        Button btn = (Button) sender;
        pos = TeamMembers.IndexOf(FindEmployee((string) btn.Tag));
        FirstName.Text = TeamMembers[pos].FirstName;
        LastName.Text = TeamMembers[pos].LastName;
        DateOfBirth.Text = TeamMembers[pos].DateOfBirth.ToString();
        Salary.Text = TeamMembers[pos].Salary;
        Position.Text = TeamMembers[pos].Position;
        ContractType.Children.OfType<RadioButton>().FirstOrDefault(r => (string)r.Content == TeamMembers[pos].Contract).IsChecked = true;


        Debug.WriteLine("Employee has been saved");
    }
    private void SaveChangedEmployee(object sender, RoutedEventArgs e) {
        TeamMembers[pos].FirstName = FirstName.Text;
        TeamMembers[pos].LastName = LastName.Text;
        TeamMembers[pos].DateOfBirth = DateOnly.Parse(DateOfBirth.Text);
        TeamMembers[pos].Salary = Salary.Text;
        TeamMembers[pos].Contract = Contract;
        TeamMembers[pos].Position = Position.Text;
        EmployeesList.Items.Refresh();

        Debug.WriteLine("Employee has been saved");
    }
    private void DeleteEmployee(object sender, RoutedEventArgs e) {
        Button btn = (Button) sender;
        int index = TeamMembers.IndexOf(FindEmployee((string) btn.Tag));
        TeamMembers.RemoveAt(index);
        Debug.WriteLine($"Employee with id:{btn.Tag} on index: {index}  has been deleted");
    }

    private Employee FindEmployee(string id) {
        Debug.WriteLine("Finding pos");
        return TeamMembers.Where(e => e.Id == id).FirstOrDefault();
    }
    private void SaveEmployees(object sender, RoutedEventArgs e) {
        string fileName = "Data.json";
        string jsonString = JsonSerializer.Serialize(TeamMembers, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, jsonString);
        Debug.WriteLine("Employees has been saved");
    }
    private void LoadEmployees(object sender, RoutedEventArgs e) {
        string fileName = "Data.json";
        TeamMembers = JsonSerializer.Deserialize<Team>(File.ReadAllText(fileName), new JsonSerializerOptions { IncludeFields = true });
        EmployeesList.Items.Refresh();
        EmployeesList.ItemsSource = TeamMembers;
        Debug.WriteLine("Employees has been loaded");
    }

    private void SetEmployeeContract(object sender, RoutedEventArgs e) {
        RadioButton option = (RadioButton) sender;
        Contract = option.Content?.ToString();
    }
}