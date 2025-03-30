﻿using System.Diagnostics;
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
using System.Text.RegularExpressions;

namespace EmployeeManager;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{

    private readonly string NamePattern = @"^[a-zA-ZÀ-ÖØ-öø-ÿ'’.-]+(?: [a-zA-ZÀ-ÖØ-öø-ÿ'’.-]+)*$";
    private int pos { get; set; }

    private bool _isEdited;
    public bool IsEdited { 
        get { return _isEdited; }
        set { 
            _isEdited = value;
            OnPropertyChanged(nameof(IsEdited));
        }
    }

    private Team TeamMembers { get; set; }
    public string? Contract { 
        set
        {
            ContractType.Children.OfType<RadioButton>().FirstOrDefault(r => (string)r.Content == value).IsChecked = true;
        }
        get
        {
            return (string)ContractType.Children.OfType<RadioButton>().FirstOrDefault(r => (bool)r.IsChecked!)!.Content;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public MainWindow() {
        InitializeComponent();
        TeamMembers = new Team();
        EmployeesList.ItemsSource = TeamMembers;
        IsEdited = false;
    }

    private void AddEmployee(object sender, RoutedEventArgs e) {
        try {
            string[] controlValues = { FirstName.Text, LastName.Text, DateOfBirth.Text, Salary.Text, Position.Text, Contract };
            foreach (var control in controlValues) {
                if (String.IsNullOrEmpty(control)) throw new Exception("The form is not filled out correctly");
            }
            TeamMembers.AddEmployee(new Employee(FirstName.Text, LastName.Text, DateOnly.Parse(DateOfBirth.Text), Salary.Text, Position.Text, Contract!));
            IsEdited = false;
            ClearControls();

            Debug.WriteLine($"Imie: {FirstName.Text}, Nazwisko: {LastName.Text}, DateOfBirth: {DateOfBirth.Text},  Salary: {Salary.Text}, Position: {Position.Text}, Contract: {Contract}");
            Debug.WriteLine("Employee has been added");
        } catch {
        }
        
    }
    private void EditEmployee(object sender, RoutedEventArgs e) {
        Button btn = (Button) sender;
        pos = TeamMembers.IndexOf(FindEmployee((string) btn.Tag));
        FirstName.Text = TeamMembers[pos].FirstName;
        LastName.Text = TeamMembers[pos].LastName;
        DateOfBirth.Text = TeamMembers[pos].DateOfBirth.ToString();
        Salary.Text = TeamMembers[pos].Salary;
        Position.Text = TeamMembers[pos].Position;
        Contract = TeamMembers[pos].Contract;
        IsEdited = true;


        Debug.WriteLine("Employee has been saved");
    }
    private void SaveChangedEmployee(object sender, RoutedEventArgs e) {
        TeamMembers[pos].FirstName = FirstName.Text;
        TeamMembers[pos].LastName = LastName.Text;
        TeamMembers[pos].DateOfBirth = DateOnly.Parse(DateOfBirth.Text);
        TeamMembers[pos].Salary = Salary.Text;
        TeamMembers[pos].Contract = Contract;
        TeamMembers[pos].Position = Position.Text;
        IsEdited = false;
        ClearControls();
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

    private void IsFirstNameValid(object sender, RoutedEventArgs e)
    {
        TextBox firstName = (TextBox) sender;
        SetMessageVisibility(firstName, Regex.IsMatch(firstName.Text, NamePattern));
    }

    private void IsLastNameValid(object sender, RoutedEventArgs e)
    {
        TextBox lastName = (TextBox) sender;
        SetMessageVisibility(lastName, Regex.IsMatch(lastName.Text, NamePattern));
    }

    private void IsDateOfBirthValid(object sender, RoutedEventArgs e)
    {
        DatePicker dateOfBirth = (DatePicker) sender;
        if (!String.IsNullOrEmpty(dateOfBirth.Text)) SetMessageVisibility(dateOfBirth, DateOnly.Parse(dateOfBirth.Text) < DateOnly.FromDateTime(DateTime.Now));
    }

    private void IsSalaryValid(object sender, RoutedEventArgs e)
    {
        TextBox salary = (TextBox) sender;
        SetMessageVisibility(salary, salary.Text.All(char.IsDigit) && !String.IsNullOrEmpty(salary.Text));
    }

    private void SetMessageVisibility(Control control, bool isHidden)
    {
        (VisualTreeHelper.GetChild(LogicalTreeHelper.GetParent(LogicalTreeHelper.GetParent(control)), 0) as TextBlock).Visibility = isHidden ? Visibility.Hidden : Visibility.Visible;
    }

    private void ClearControls()
    {
        FirstName.Clear();
        LastName.Clear();
        DateOfBirth.SelectedDate = null;
        DateOfBirth.DisplayDate = DateTime.Today;
        Position.SelectedIndex = 0;
        Salary.Clear();
        Contract = "Umowa na czas nieokreślony";
    }
}